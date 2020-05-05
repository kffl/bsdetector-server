using System;
using BSDetector.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BSDetector.Analysis.Exceptions
{
    /// <summary>
    /// Represents exceptions that may occur during repository analysis
    /// </summary>
    public class AnalysisException : Exception
    {
        public virtual int HTTPCode
        {
            get
            {
                return 400;
            }
        }
        public virtual string ErrorName
        {
            get
            {
                return "ANALYSIS_ERROR";
            }
        }
        public override string Message
        {
            get
            {
                return "Could not analyze repo";
            }
        }
    }

    public class AnalysisExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            if (exception is AnalysisException e)
            {
                var result = new JsonResult(
                    new ErrorResponse()
                    {
                        errorName = e.ErrorName,
                        message = e.Message
                    });
                result.StatusCode = e.HTTPCode;
                context.Result = result;
            }
        }
    }
}