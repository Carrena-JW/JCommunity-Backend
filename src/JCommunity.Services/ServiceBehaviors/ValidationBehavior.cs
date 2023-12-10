namespace JCommunity.Services.ServiceBehaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
        
    }

    public async Task<TResponse> Handle(
        TRequest request, 
        RequestHandlerDelegate<TResponse> next, 
        CancellationToken ct)
    {
        var responseType = typeof(TResponse).GetGenericTypeDefinition();

        //If you do not have validator, early return
        if (!(responseType == typeof(Result<>) || responseType == typeof(Result))) return await next();
        if (!_validators.Any()) return await next();

        var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(request)));
        var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

        if (failures.Count != 0) 
        {
            var genericType = typeof(TResponse).GetGenericArguments().FirstOrDefault();
            if(genericType != null)
            {
                var instanceType = typeof(Result<>).MakeGenericType(genericType);
                var errorInstance = Activator.CreateInstance(instanceType);
                var withErrorMethod = instanceType.GetMethod("WithError", new[] { typeof(Error) });
                withErrorMethod!.Invoke(errorInstance, new object[] { new ValidationError(failures) });
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

