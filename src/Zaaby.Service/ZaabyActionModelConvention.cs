﻿using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Routing;

namespace Zaaby.Service
{
    internal class ZaabyActionModelConvention : IActionModelConvention
    {
        private readonly Type _serviceType;

        public ZaabyActionModelConvention(Type serviceType) => _serviceType = serviceType;

        public void Apply(ActionModel action)
        {
            if (action is null) throw new ArgumentNullException(nameof(action));
            if (!_serviceType.IsAssignableFrom(action.Controller.ControllerType)) return;

            action.Selectors.Clear();
            var route = _serviceType.GetCustomAttribute(typeof(RouteAttribute), false) as RouteAttribute;
            var template = $"{_serviceType.FullName?.Replace('.', '/')}/[action]";
            action.Selectors.Add(CreateSelector(route ?? new RouteAttribute(template) {Name = template}));

            foreach (var parameter in action.Parameters)
            {
                parameter.BindingInfo ??= new BindingInfo();
                parameter.BindingInfo.BindingSource = BindingSource.Body;
            }
        }

        private static SelectorModel CreateSelector(IRouteTemplateProvider routeTemplateProvider)
        {
            var selectorModel = new SelectorModel();
            if (routeTemplateProvider is not null)
                selectorModel.AttributeRouteModel = new AttributeRouteModel(routeTemplateProvider);

            selectorModel.ActionConstraints.Add(new HttpMethodActionConstraint(new List<string> {"POST"}));

            return selectorModel;
        }
    }
}