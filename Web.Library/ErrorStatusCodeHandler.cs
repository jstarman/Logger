using Nancy;
using Nancy.ErrorHandling;
using Nancy.Responses;
using Nancy.Responses.Negotiation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Library
{
    public sealed class ErrorStatusCodeHandler : IStatusCodeHandler
    {
        private readonly IResponseNegotiator responseNegotiator;

        public ErrorStatusCodeHandler(IResponseNegotiator responseNegotiator)
        {
            this.responseNegotiator = responseNegotiator;
        }
        public bool HandlesStatusCode(HttpStatusCode statusCode, NancyContext context)
        {
            return statusCode == HttpStatusCode.InternalServerError;
        }

        public void Handle(HttpStatusCode statusCode, NancyContext context)
        {           
        }
    }
}
