using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finalProject
{
    public class CasePreUpdate : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            // Obtain the tracing service from the service provider
            ITracingService tracingService = (ITracingService)serviceProvider.GetService(typeof(ITracingService));
            // Obtain the execution context from the service provider
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            // obtain the organization service reference which you will need for web service calls
            IOrganizationServiceFactory serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);
            /*****/


            //retrieve the target and the pre image from DB
            new_case target = ((Entity)context.InputParameters["Target"]).ToEntity<new_case>();
            new_case preImage = context.PreEntityImages["preImage"].ToEntity<new_case>();
            

         // updates the treatment lines and cleares the input of the field
            if (target.Contains("new_status")) {

                new_treatmentline line = new new_treatmentline();

                line.new_newcase = target.ToEntityReference();
                line.new_linedescription = target.new_treatmentdescription;
                line.new_linestatus = target.new_status;

                service.Create(line);
                target.new_treatmentdescription = null;
            }

            //if the case is closed shows if exceeded SLA

            if (target.Contains("new_status") && target.new_status.Value == 3) {
                target.new_slaexceeded = DateTime.Now > preImage.new_endsla.Value;
            }
        }
    }
}


