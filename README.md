## About
This is a result pattern library for ASP.NET Core that handles success and error flows in services and CQRS-based architectures. It abstracts HTTP status codes into result and error classes in the library such as `ExecOk<TValue>`, `ExecNoContent<TValue>`, `ExecBadRequest`, `ExecInternalServerError` etc., and supports implicit default success (`200 OK`) and default error (`400 Bad Request`) handling.

## How to Use
After installing the package, you can use `ExecResult<TValue>` for returning types of your services or CQRS handlers.

- Implicit Success and Implicit Error Handling in Services:
```csharp
using Cayd.AspNetCore.ExecutionResult;

public class MyService : IMyService
{
    // ...

    public ExecResult<User> GetUser(int id)
    {
        var user = _dbContext.Find(id);
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

    public ExecResult<MyClass> GetData()
    {
        if (CheckIfAuthorized())
            return ExecUnauthorized("Message here if needed", "Message code here if needed (can be used for translation keys for instance)");

        var data = GetData();
        if (data == null)
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

// Since the returned result also includes the HTTP code, the corresponding response can be returned
// in action methods by using the 'ObjectResult' class.
```

For CQRS handlers utilizing the `MediatR` library:
- Request:
```csharp
public class GetDataRequest : IRequest<ExecResult<GetDataResponse>>
{
    // ...
}
```
- Handler:
```csharp
public class GetDataHandler : IRequestHandler<GetDataRequest, ExecResult<GetDataResponse>>
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
For CQRS handlers utilizing the `MediatR` library as well as `FluentValidation`, the validation pipeline can be set up as follows to use this library:
- Validation Behavior:
```csharp
using Cayd.AspNetCore.ExecutionResult;
using Cayd.AspNetCore.ExecutionResult.ClientError;
using FluentValidation;
using MediatR;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validators.Any())
        {
            foreach (var validator in _validators)
            {
                if (validator != null)
                {
                    var validationResult = await validator.ValidateAsync(request, cancellationToken);
                    if (validationResult.Errors.Count > 0)
                    {
                        var errorDetails = validationResult.Errors
                            .Select(e => new ExecErrorDetail(e.ErrorMessage, e.ErrorCode))
                            .ToList();

                        return (dynamic)new ExecBadRequest(errorDetails);
                    }
                }
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
    public LoginValidation()
    {
        RuleFor(r => r.Property1)
            .NotEmpty()
                .WithMessage("Error message here")
                .WithErrorCode("Error message code here (can be used for translation keys for instance)");
    }
}
```
- Registering The Pipeline:
```csharp
builder.Services.AddMediatR(config =>
{
    // ...
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
});
```
