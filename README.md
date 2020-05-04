# YouZack.ASPNetCore
A set of toolkit for ASP.Net Core

```
Install-Package YouZack.ASPNetCore
```

1)TransactionScopeFilter: if TransactionScopeFilter is added, all the Actions will be auto-transactional, except the method that with [NotTransactionalAttribute]