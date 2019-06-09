﻿using NServiceBus.AcceptanceTesting.Support;
using NServiceBus.ObjectBuilder;
using System;

namespace NServiceBus.AttributeRouting.AcceptanceTests
{
    public static class ConfigureExtensions
    {
        public static void RegisterComponentsAndInheritanceHierarchy(this EndpointConfiguration builder, RunDescriptor runDescriptor)
        {
            builder.RegisterComponents(r => { RegisterInheritanceHierarchyOfContextOnContainer(runDescriptor, r); });
        }

        static void RegisterInheritanceHierarchyOfContextOnContainer(RunDescriptor runDescriptor, IConfigureComponents r)
        {
            var type = runDescriptor.ScenarioContext.GetType();
            while (type != typeof(object))
            {
                r.RegisterSingleton(type, runDescriptor.ScenarioContext);
                type = type.BaseType;
            }
        }

        public static void CustomizeRouting(this RunDescriptor runDescriptor, Action<RoutingSettings> routingConfig)
        {
            runDescriptor.Settings.Set(routingConfig);
        }
    }
}
