using DocuSign.eSign.Client;
using DocuSign.eSign.Model;
using System;
using System.Collections.Generic;

namespace eg_01_csharp_jwt
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var apiClient = new ApiClient();

                Console.WriteLine("\nSending an envelope using the order form template. This takes about 10 seconds...");
                EnvelopeSummary result = new SendEnvelope(apiClient).Send();
                Console.WriteLine("\nDone. Envelope status: {0}. Envelope ID: {1}", result.Status, result.EnvelopeId);
            }
            catch (ApiException e)
            {
                Console.WriteLine("\nDocuSign Exception!");

                // Special handling for consent_required
                String message = e.Message;
                if (!String.IsNullOrWhiteSpace(message) && message.Contains("consent_required"))
                {
                    String consent_url = String.Format("\n    {0}/oauth/auth?response_type=code&scope={1}&client_id={2}&redirect_uri={3}",
                        DSConfig.AuthenticationURL, DSConfig.PermissionScopes, DSConfig.ClientID, DSConfig.OAuthRedirectURI);

                    Console.WriteLine("C O N S E N T   R E Q U I R E D");
                    Console.WriteLine("Ask the user who will be impersonated to run the following url: ");
                    Console.WriteLine(consent_url);
                    Console.WriteLine("\nIt will ask the user to login and to approve access by your application.");
                    Console.WriteLine("Alternatively, an Administrator can use Organization Administration to");
                    Console.WriteLine("pre-approve one or more users.");
                }
                else
                {
                    Console.WriteLine("    Reason: {0}", e.ErrorCode);
                    Console.WriteLine("    Error Reponse: {0}", e.ErrorContent);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Console.WriteLine("Done. Hit enter to exit...");
            Console.ReadKey();
        }
    }
}
