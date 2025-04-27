using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Shared.Filters
{
    public class ValidationFilter<T> : IEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            var validator = context.HttpContext.RequestServices.GetService<IValidator<T>>();

            if(validator is null)
                return await next(context);

            var firstParameter = context.Arguments.OfType<T>().FirstOrDefault();

            if (firstParameter is null)
                return await next(context);

            var resultValid = await validator.ValidateAsync(firstParameter);

            if (!resultValid.IsValid)
            {
                return Results.ValidationProblem(resultValid.ToDictionary());
            }

            return await next(context);
        }
    }
}
