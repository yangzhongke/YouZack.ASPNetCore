using System;

namespace YouZack.ASPNetCore
{
    /// <summary>
    /// 被修饰的方法不自动启用TransactionScopeFilter
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class NotTransactionalAttribute:Attribute
    {
    }
}
