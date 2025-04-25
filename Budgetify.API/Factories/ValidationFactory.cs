using FluentValidation;

namespace budgetifyAPI.Factories
{
    public class ValidationFactory : IValidationFactory
    {
        private readonly IServiceProvider _serviceProvider;
        public ValidationFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public IValidator<T> GetValidator<T>() where T : class
        {
            var validator = _serviceProvider.GetRequiredService<IValidator<T>>();
            return validator;
        }
    }
}
