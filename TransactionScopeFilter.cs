using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;
using System.Transactions;
using System.Linq;

namespace YouZack.ASPNetCore
{
    /// <summary>
    /// 自动对于Action启用TransactionScope事务，除非方法上标注NotTransactionalAttribute
    /// </summary>
    public class TransactionScopeFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //Action上是否有标注NotTransactionalAttribute
            bool hasNotTransactionalAttribute = false;
            if (context.ActionDescriptor is ControllerActionDescriptor)
            {
                ControllerActionDescriptor actionDesc = (ControllerActionDescriptor)context.ActionDescriptor;
                if(actionDesc.MethodInfo.GetCustomAttributes(typeof(NotTransactionalAttribute), false).Any())
                {
                    hasNotTransactionalAttribute = true;
                }
            }
            //如果标注了NotTransactionalAttribute，则不自动启用TransactionScope
            if (hasNotTransactionalAttribute)
            {
                await next();
            }
            else
            {
                using (var txScope =
                    new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    await next();
                    txScope.Complete();
                }
            }
        }
    }
}
