using JCommunity.AppCore.Core.Errors;

namespace JCommunity.Services.ServiceBehaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly IValidator<TRequest> _validator;

    public ValidationBehavior(IValidator<TRequest> validator)
    {
        _validator = validator;  
    }

    public async Task<TResponse> Handle(
        TRequest request, 
        RequestHandlerDelegate<TResponse> next, 
        CancellationToken cancellationToken)
    {
        var responseType = typeof(TResponse).GetGenericTypeDefinition();

        //If you do not have validator, early return
        if (!(responseType == typeof(Result<>) || responseType == typeof(Result))) return await next();
        if (_validator == null) return await next();

        //ss
        var validation = await _validator.ValidateAsync(request, cancellationToken);
        if (!validation.IsValid)
        {
            var genericType = typeof(TResponse).GetGenericArguments().FirstOrDefault();
            if(genericType != null)
            {
                var instanceType = typeof(Result<>).MakeGenericType(genericType);
                var errorInstance = Activator.CreateInstance(instanceType);
                var withErrorMethod = instanceType.GetMethod("WithError", new[] { typeof(Error) });
                withErrorMethod!.Invoke(errorInstance, new object[] { new ValidationError(validation.Errors) });
                return (TResponse)errorInstance!;
            }
            else
            {
                return await next();
            }
        }
        else
        {
            return await next();
        }
    }



}

