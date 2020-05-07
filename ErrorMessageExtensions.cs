using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;

namespace YouZack.ASPNetCore
{
    public static class ErrorMessageExtensions
    {
        public static string GetErrorsString(this ModelStateDictionary modelState)
        {
            var allErrors = modelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
            return string.Join("\r\n", allErrors);
        }

        public static string GetErrorsString(this IEnumerable<IdentityError> errors)
        {
            var allErrors = errors.Select(e => e.Description);
            return string.Join("\r\n", allErrors);
        }

        public static void CheckIdentityResult(this HttpContext httpContext, IdentityResult result)
        {
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.GetErrorsString());
            }
        }
    }
}
