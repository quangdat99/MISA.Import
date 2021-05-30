using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Import.Core.Exceptions
{
    public class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
    {
        public int Order { get; } = int.MaxValue - 10;

        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception != null)
            {
                if (context.Exception is ValidateException exception)
                {
                    var responseClient = new
                    {
                        userMsg = exception.Message,
                        devMsg = Properties.Resources.Error_Exception,
                        Data = exception.DataErr
                    };

                    context.Result = new ObjectResult(responseClient)
                    {
                        StatusCode = 400,
                    };
                    context.ExceptionHandled = true;
                }
                else
                {
                    var response = new
                    {
                        userMsg = Properties.Resources.Error_Exception,
                        devMsg = context.Exception.Message,
                        traceInfo = context.Exception.StackTrace
                    };

                    context.Result = new ObjectResult(response)
                    {
                        StatusCode = 500,
                    };
                    context.ExceptionHandled = true;
                }
            }
        }
    }

}
