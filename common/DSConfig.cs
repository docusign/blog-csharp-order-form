using System;
using System.Configuration;

namespace eg_01_csharp_jwt
{
    internal class DSConfig
    {
        private const string CLIENT_ID = "DS_CLIENT_ID";
        private const string IMPERSONATED_USER_GUID = "DS_IMPERSONATED_USER_GUID";
        private const string TARGET_ACCOUNT_ID = "DS_TARGET_ACCOUNT_ID";
        private const string SIGNER_1_EMAIL = "DS_SIGNER_1_EMAIL";
        private const string SIGNER_1_NAME = "DS_SIGNER_1_NAME";
        private const string CC_1_EMAIL = "DS_CC_1_EMAIL";
        private const string CC_1_NAME = "DS_CC_1_NAME";
        private const string PRIVATE_KEY = "DS_PRIVATE_KEY";
        private const string DS_AUTH_SERVER = "DS_AUTH_SERVER";
        private const string DS_TEMPLATE_ID = "DS_TEMPLATE_ID";

        static DSConfig()
        {
            ClientID = GetSetting(CLIENT_ID);
            ImpersonatedUserGuid = GetSetting(IMPERSONATED_USER_GUID);
            TargetAccountID = GetSetting(TARGET_ACCOUNT_ID);
            OAuthRedirectURI = "https://www.docusign.com";
            Signer1Email = GetSetting(SIGNER_1_EMAIL);
            Signer1Name = GetSetting(SIGNER_1_NAME);
            Cc1Email = GetSetting(CC_1_EMAIL);
            Cc1Name = GetSetting(CC_1_NAME);
            PrivateKey = GetSetting(PRIVATE_KEY);
            AuthServer = GetSetting(DS_AUTH_SERVER);
            AuthenticationURL = GetSetting(DS_AUTH_SERVER);
            TemplateID = GetSetting(DS_TEMPLATE_ID);
            API = "restapi/v2";
            PermissionScopes = "signature impersonation";
            JWTScope = "signature";
        }

        private static string GetSetting(string configKey)
        {
            string val = Environment.GetEnvironmentVariable(configKey)
                ?? ConfigurationManager.AppSettings.Get(configKey);

            if (PRIVATE_KEY.Equals(configKey) && "FALSE".Equals(val))
                return null;

            return val ?? "";
        }

        public static string ClientID { get; private set; }
        public static string ImpersonatedUserGuid { get; private set; }
        public static string TargetAccountID { get; private set; }
        public static string OAuthRedirectURI { get; private set; }
        public static string Signer1Email { get; private set; }
        public static string Signer1Name { get; private set; }
        public static string Cc1Email { get; private set; }
        public static string Cc1Name { get; private set; }
        public static string PrivateKey { get; private set; }
        public static string TemplateID { get; private set; }
        private static string authServer;
        public static string AuthServer
        {
            get { return authServer; }
            set
            {
                if (!String.IsNullOrWhiteSpace(value) && value.StartsWith("https://"))
                {
                    authServer = value.Substring(8);
                }
                else if (!String.IsNullOrWhiteSpace(value) && value.StartsWith("http://"))
                {
                    authServer = value.Substring(7);
                }
                else
                {
                    authServer = value;
                }
            }
        }
        public static string AuthenticationURL { get; private set; }
        public static string API { get; private set; }
        public static string PermissionScopes { get; private set; }
        public static string JWTScope { get; private set; }
    }
}
