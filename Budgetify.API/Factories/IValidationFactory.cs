using FluentValidation;

namespace budgetifyAPI.Factories
{
    public interface IValidationFactory
    {
        public IValidator<T> GetValidator<T>() where T : class;
    }
}
