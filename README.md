## About
This is a result pattern library that handles success and error flows in services and CQRS-based architectures for ASP.NET Core. It abstracts HTTP status codes into result and error classes such as `ExecOk<TValue>`, `ExecNoContent<TValue>`, `ExecBadRequest`, `ExecInternalServerError` etc. and supports implicit default success (`200 OK`) and default error (`400 Bad Request`) handling.

## How to Use
After installing the package, you can use `ExecResult<TValue>` for returning types of your services or CQRS handlers.

- Implicit Success and Implicit Error Handling in Services:
```csharp
using Cayd.AspNetCore.ExecutionResult;

public class MyService : IMyService
{
    // ...

    public ExecResult<User> GetUser(Guid id)
    {
        var user = _dbContext.Users.Find(id);
        if (user == null)
            return ExecErrorDetail("Message here if needed", "Message code here if needed (can be used for translation keys for instance)"); // -> This implicit usage utilizes ExecBadRequest, which returns 'error' with the HTTP status code of 400.

        return user; // -> This implicit usage utilizes ExecOk, which returns 'success' with the HTTP status code of 200.
    }
}
```
- Explicit Success and Explicit Error Handling in Services:
```csharp
using Cayd.AspNetCore.ExecutionResult;
using Cayd.AspNetCore.ExecutionResult.ClientError;
using Cayd.AspNetCore.ExecutionResult.Success;

public class MyService : IMyService
{
    // ...

    public ExecResult<MyClass> GetData(string? search)
    {
        if (CheckIfAuthorized())
            return ExecUnauthorized("Message here if needed", "Message code here if needed (can be used for translation keys for instance)");

        // Explicit usage of Bad Request. 'ExecErrorDetail' could be used directly as well.
        if (search == null)
            return ExecBadRequest("Message here if needed", "Message code here if needed (can be used for translation keys for instance)");

        var data = _dbContext.Data.Where(x => x.Property1 == search).ToList();
        if (data.Count == 0)
            return ExecNoContent();

        return data; // -> This implicit usage utilizes ExecOk, which returns 'success' with the HTTP status code of 200.
    }
}
```
- Handling The Result:
```csharp
// By using the Match method's overloads, you can handle success, redirection or error of the execution.

var result = _myService.GetData();
result.Match(
    (code, value, metadata) => { /* Success */ },
    (code, errors, metadata) => { /* Error */ }
);

var result = _myService.GetData();
result.Match(
    (code, metadata) => { /* Redirection */ },
    (code, errors, metadata) => { /* Error */ }
);

var result = _myService.GetData();
result.Match(
    (code, value, metadata) => { /* Success */ },
    (code, metadata) => { /* Redirection */ },
    (code, errors, metadata) => { /* Error */ }
);

// Since the returned result also includes the HTTP code, the response can be returned in action methods by using the 'ObjectResult' class.
```

For CQRS handlers utilizing the `Cayd.AspNetCore.ExecResult` library:
- Request:
```csharp
public class GetDataRequest : IAsyncRequest<ExecResult<GetDataResponse>>
{
    // ...
}
```
- Handler:
```csharp
public class GetDataHandler : IAsyncHandler<GetDataRequest, ExecResult<GetDataResponse>>
{
    // ...
}
```
- Response:
```csharp
public class GetDataResponse
{
    // ...
}
```

## Result Classes
The library includes all result classes representing `2xx`, `3xx`, `4xx` and `5xx` HTTP status codes. These classes start with the `Exec` prefix. For instance, the class representing `204 No Content` is called `ExecNoContent` and is under the `Cayd.AspNetCore.ExecutionResult.Success` namespace.

Type               | Namespace 
-------------------|---------------------------------------------
Success (2xx)      | Cayd.AspNetCore.ExecutionResult.Success     
Redirection (3xx)  | Cayd.AspNetCore.ExecutionResult.Redirection 
Client error (4xx) | Cayd.AspNetCore.ExecutionResult.ClientError 
Server error (5xx) | Cayd.AspNetCore.ExecutionResult.ServerError 

## Extras
For CQRS handlers utilizing the `Cayd.AspNetCore.ExecResult` and `FluentValidation` libraries, the validation flow can be set up as follows to use ExecBadRequest automatically when validations fail:
- Validation Flow:
```csharp
using Cayd.AspNetCore.ExecutionResult;
using Cayd.AspNetCore.ExecutionResult.ClientError;
using Cayd.AspNetCore.Mediator.Abstractions;
using Cayd.AspNetCore.Mediator.Flows;
using FluentValidation;

public class MediatorValidationFlow<TRequest, TResponse> : IMediatorFlow<TRequest, TResponse>
    where TRequest : IAsyncRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public MediatorValidationFlow(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> InvokeAsync(TRequest request, AsyncHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        foreach (var validator in _validators)
        {
            if (validator == null)
                continue;

            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (validationResult.Errors.Count > 0)
            {
                var errorDetails = validationResult.Errors
                    .Select(e => new ExecErrorDetail(e.ErrorMessage, e.ErrorCode))
                    .ToList();

                return (dynamic)new ExecBadRequest(errorDetails);
            }
        }

        return await next();
    }
}
```
- Example Validation in CQRS:
```csharp
public class GetDataValidation : AbstractValidator<GetDataRequest>
{
    public GetDataValidation()
    {
        RuleFor(r => r.Property1)
            .NotEmpty()
                .WithMessage("Error message here")
                .WithErrorCode("Error message code here (can be used for translation keys for instance)");
    }
}
```
- Adding The Flow:
```csharp
services.AddMediator(config =>
{
    // ...

    config.AddTransientFlow(typeof(MediatorValidationFlow<,>));
});
```
