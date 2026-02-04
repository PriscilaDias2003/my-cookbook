using Microsoft.Extensions.DependencyInjection;
using MyCookBook.Application.Services.AutoMapper;
using MyCookBook.Application.Services.Cryptography;
using MyCookBook.Application.UseCases.User.Register;
using Microsoft.Extensions.Configuration;

namespace MyCookBook.Application
{
    public static class DependencyInjectionExtension
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration) {
            AddAutoMapper(services);
            AddUseCases(services);
            AddPasswordEncripter(services, configuration);
        }

        private static void AddUseCases(IServiceCollection services)
        {
            services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
        }

        private static void AddAutoMapper(IServiceCollection services)
        {
            var autoMapper =
            services.AddScoped(option => new AutoMapper.MapperConfiguration(options => { options.AddProfile(new AutoMapping()); }).CreateMapper());
        }

        private static void AddPasswordEncripter(IServiceCollection services, IConfiguration configuration)
        {
            var additionaKey = configuration.GetValue<string>("Settings:Password:AdditionalKey");
            services.AddScoped(option => new PasswordEncripter(additionaKey!));
        }
    }
}
