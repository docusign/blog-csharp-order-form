using DocuSign.eSign.Client;
using DocuSign.eSign.Client.Auth;
using DocuSign.eSign.Model;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using static DocuSign.eSign.Client.Auth.OAuth.UserInfo;

namespace eg_01_csharp_jwt
{
    public class ExampleBase
    {
        private const int TOKEN_EXPIRATION_IN_SECONDS = 3600;
        private const int TOKEN_REPLACEMENT_IN_MILLISECONDS = 10 * 60 * 1000;

        private static string AccessToken { get; set; }
        private static int expiresIn;
        private static Account Account { get; set; }

        protected static ApiClient ApiClient { get; private set; }

        protected static string AccountID
        {
            get { return Account.AccountId; }
        }

        public ExampleBase(ApiClient client)
        {
            ApiClient = client;
        }

        public void CheckToken()
        {
            if (AccessToken == null
                || (DateTime.Now.Millisecond + TOKEN_REPLACEMENT_IN_MILLISECONDS) > expiresIn)
            {
                Console.WriteLine("Obtaining a new access token...");
                UpdateToken();
            }
        }

        private void UpdateToken()
        {
            OAuth.OAuthToken authToken = ApiClient.RequestJWTUserToken(
                            DSConfig.ClientID,
                            DSConfig.ImpersonatedUserGuid,
                            DSConfig.AuthServer,
                            Encoding.UTF8.GetBytes(DSConfig.PrivateKey),
                            1);

            AccessToken = authToken.access_token;

            if (Account == null)
                Account = GetAccountInfo(authToken);

            ApiClient = new ApiClient(Account.BaseUri + "/restapi");
            expiresIn = DateTime.Now.Millisecond + ((int)authToken.expires_in * 1000);
        }

        private Account GetAccountInfo(OAuth.OAuthToken authToken)
        {
            ApiClient.SetOAuthBasePath(DSConfig.AuthServer);
            OAuth.UserInfo userInfo = ApiClient.GetUserInfo(authToken.access_token);
            Account acct = null;

            var accounts = userInfo.Accounts;

            if (!string.IsNullOrEmpty(DSConfig.TargetAccountID) && !DSConfig.TargetAccountID.Equals("FALSE"))
            {
                acct = accounts.FirstOrDefault(a => a.AccountId == DSConfig.TargetAccountID);

                if (acct == null)
                {
                    throw new Exception("The user does not have access to account " + DSConfig.TargetAccountID);
                }
            }
            else
            {
                acct = accounts.FirstOrDefault(a => a.IsDefault == "true");
            }

            return acct;
        }
    }
}
