using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace finalProject
{
    public class TopicPreUpdate : IPlugin
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
            Entity target = ((Entity)context.InputParameters["Target"]); // entity we recieve from the event

            //create query for retrieving topic name colum set
            QueryExpression query = new QueryExpression();
            query.EntityName = "new_topic";
            query.ColumnSet = new ColumnSet("new_name");
            query.Criteria = new FilterExpression(LogicalOperator.And);
            query.Criteria.AddCondition("new_name", ConditionOperator.Equal, target["new_name"]);
            EntityCollection topics = service.RetrieveMultiple(query);
            // if this topic name exists throw exception
            if (topics.Entities.Count > 0)
            {
                throw new InvalidPluginExecutionException("Found duplicate! choose differant name. this one already exists");
            }
        }
    }
}


