using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finalProject
{
    public class AutoNumbering : IPlugin
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
            Entity target = ((Entity)context.InputParameters["Target"]);
            /*****/


            //create query and saved retrieved details to var autonumbers
            QueryExpression query1 = new QueryExpression("new_automatonnumber");
            query1.ColumnSet = new ColumnSet("new_currentnumber", "new_fieldname");
            query1.Criteria.AddCondition("new_entityname", ConditionOperator.Equal, target.LogicalName);
            var autonumbers = service.RetrieveMultiple(query1);

            //read the autonumber from the var, increasing it with ++ and sending it to the new case and update the last number in DB registry
            if (autonumbers.Entities.Count > 0)
            {
                Entity autonumber = autonumbers.Entities[0];
                int currentNumber = (int)autonumber["new_currentnumber"];
                currentNumber++;
                target[(string)autonumber["new_fieldname"]] = currentNumber.ToString();
                autonumber["new_currentnumber"] = currentNumber;
                service.Update(autonumber);
            }
        }
    }
}


