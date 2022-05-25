using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.Client;
using Microsoft.VisualStudio.Services.WebApi;

namespace ClientLibraryConsoleAppSample
{
    class Program
    {
        //============= Config [Edit these with your settings] =====================
        internal const string azureDevOpsOrganizationUrl = "https://spsprodwcus0.vssps.visualstudio.com/A850a26fd-8300-ce32-bb6e-28e032a3a0fd";
            //"https://dev.azure.com/henglu0339"; //change to the URL of your Azure DevOps account; NOTE: This must use HTTPS
        // internal const string vstsCollectioUrl = "http://myserver:8080/tfs/DefaultCollection" alternate URL for a TFS collection
        //==========================================================================

        //Console application to execute a user defined work item query
        static async Task Main(string[] args)
        {
            //Prompt user for credential
            VssConnection connection = new VssConnection(new Uri(azureDevOpsOrganizationUrl), new VssClientCredentials());

            await connection.ConnectAsync();

            var tokenClient = connection.GetClient<Microsoft.VisualStudio.Services.DelegatedAuthorization.WebApi.TokenHttpClient>();
            var token = new Microsoft.VisualStudio.Services.DelegatedAuthorization.SessionToken()
            {
                TargetAccounts = new List<Guid>() { Guid.Parse("2663b13f-50e3-a655-a159-22f6f4725fab") },
                Scope = "vso.gallery_publish",
                DisplayName = "Market place CloudTest",
                ValidTo = DateTime.Now.AddMonths(3)
            };

            var result = await tokenClient.CreateSessionTokenAsync(token, Microsoft.VisualStudio.Services.DelegatedAuthorization.SessionTokenType.Compact, false);
           // result.Dump();
        }
    }
}
