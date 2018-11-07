using System;
using DocuSign.eSign.Api;
using DocuSign.eSign.Client;
using DocuSign.eSign.Model;
using System.Text;
using System.Collections.Generic;

namespace eg_01_csharp_jwt
{
    /// <summary>
    /// Send an envelope using a template.
    /// </summary>
    internal class SendEnvelope : ExampleBase
    {
        /// <summary>
        /// This class create and send envelope
        /// </summary>
        /// <param name="apiClient"></param>
        public SendEnvelope(ApiClient apiClient) : base(apiClient)
        {
        }
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        internal EnvelopeSummary Send()
        {
            CheckToken();

            EnvelopeDefinition envelope = this.CreateEvelope();
            EnvelopesApi envelopeApi = new EnvelopesApi(ApiClient.Configuration);
            EnvelopeSummary results = envelopeApi.CreateEnvelope(AccountID, envelope);
            return results;
        }
        /// <summary>
        /// This method creates the envelope request body 
        /// </summary>
        /// <returns></returns>
        private EnvelopeDefinition CreateEvelope()
        {
            EnvelopeDefinition envelopeDefinition = new EnvelopeDefinition
            {
                EmailSubject = "Please sign the order form sent from the C# SDK"
            };

            // Assign recipients to template roles by setting name, email, and role name.  Note that the
            TemplateRole tRoleSigner = new TemplateRole();
            tRoleSigner.Email = DSConfig.Signer1Email;
            tRoleSigner.Name = DSConfig.Signer1Name;
            // template role name must match the placeholder role name saved in your envelope template.  
            tRoleSigner.RoleName = "Signer1";

            TemplateRole tRoleCC = new TemplateRole();
            tRoleCC.Email = DSConfig.Cc1Email;
            tRoleCC.Name = DSConfig.Cc1Name;
            // template role name must match the placeholder role name saved in your envelope template.  
            tRoleCC.RoleName = "CC1";

            List<TemplateRole> rolesList = new List<TemplateRole>() { tRoleSigner, tRoleCC };

            // add the role to the envelope and assign valid templateId from your account
            envelopeDefinition.TemplateRoles = rolesList;
            envelopeDefinition.TemplateId = DSConfig.TemplateID;

            // Request that the envelope be sent by setting |status| to "sent".
            // To request that the envelope be created as a draft, set to "created"
            envelopeDefinition.Status = "sent";

            return envelopeDefinition;
        }
    }
}
