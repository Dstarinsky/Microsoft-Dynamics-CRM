using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finalProject
{
    public class CasePreCreate : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            IOrganizationServiceFactory serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);
            /*************/


            // retrieve the case and SLA time from topic
            new_case targetCase = ((Entity)context.InputParameters["Target"]).ToEntity<new_case>();
            var slaRetrieve = service.Retrieve("new_topic", targetCase.new_newtopic.Id, new ColumnSet("new_sla"));

            //updating the SLA end date in the case by retrieving the SLA record from topic and adding it to the current date
            var sla = DateTime.Now.AddDays((int)slaRetrieve["new_sla"]);
            targetCase.new_endsla = sla;
        }
    }
}


