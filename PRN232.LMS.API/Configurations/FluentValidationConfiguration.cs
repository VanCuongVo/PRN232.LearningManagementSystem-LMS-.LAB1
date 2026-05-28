using FluentValidation;
using FluentValidation.AspNetCore;
using PRN232.LMS.Services.Validators;


namespace PRN232.LMS.API.Configurations
{
    public static class FluentValidationConfiguration
    {
        public static IServiceCollection AddFluentValidationConfig(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();

            services.AddValidatorsFromAssemblyContaining<CreateStudentRequestValidator>();

            return services;
        }
    }
}