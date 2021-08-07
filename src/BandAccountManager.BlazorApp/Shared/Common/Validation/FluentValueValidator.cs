using System;
using System.Collections.Generic;
using System.Linq;

using FluentValidation;

namespace BandAccountManager.BlazorApp.Shared.Common.Validation
{
    public class FluentValueValidator<T> : AbstractValidator<T>
    {
        public FluentValueValidator(Action<IRuleBuilderInitial<T, T>> rule)
        {
            rule(RuleFor(x => x));
        }

        private IEnumerable<string> ValidateValue(T arg)
        {
            var result = Validate(arg);

            if (result.IsValid)
            {
                return Array.Empty<string>();
            }

            return result.Errors.Select(e => e.ErrorMessage);
        }

        public Func<T, IEnumerable<string>> ValidateValueFunc => ValidateValue;
    }
}
