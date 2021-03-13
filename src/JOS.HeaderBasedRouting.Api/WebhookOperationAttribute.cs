using System;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace JOS.HeaderBasedRouting.Api
{
    public class WebhookOperationAttribute : Attribute, IActionConstraint
    {
        private readonly string _header;
        private readonly string _value;

        public WebhookOperationAttribute(string header, string value, int order = 0)
        {
            _header = header;
            _value = value;
            Order = order;
        }

        public bool Accept(ActionConstraintContext context)
        {
            if (!context.RouteContext.HttpContext.Request.Headers.ContainsKey(_header))
            {
                return false;
            }

            var headerValue = context.RouteContext.HttpContext.Request.Headers[_header];
            return headerValue.Equals(_value);
        }

        public int Order { get; }
    }
}
