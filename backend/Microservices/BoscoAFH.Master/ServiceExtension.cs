using BoscoAFH.Base;
using BoscoAFH.CommonService;
using BoscoAFH.DBEngine;
using BoscoAFH.Entities.Data;
using BoscoAFH.Entities.Models;
using BoscoAFH.MasterInfrastructure.Interfaces;
using BoscoAFH.MasterInfrastructure.Models.Input;
using BoscoAFH.MasterInfrastructure.Repositorys;
using BoscoAFH.MasterService.Interfaces;
using BoscoAFH.MasterService.Service;
using Microsoft.EntityFrameworkCore;


namespace BoscoAFH.Master
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddAutoMappingSetup(this IServiceCollection services)
        {
            //// Register AutoMapper
            //var mappingConfig = MappingConfig.MasterRegisterMaps();
            //IMapper mapper = mappingConfig.CreateMapper();
            //services.AddSingleton(mapper);
            //services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddAutoMapper(cfg =>
            {
                // Add specific type mappings using the generic profile
                cfg.AddProfile(new GenericMappingProfile<Role, RoleDTO>());
                cfg.AddProfile(new GenericMappingProfile<User, UserDTO>());
            });
            return services;
        }

        public static IServiceCollection AddDIServicesSetup(this IServiceCollection services)
        {
            // Take the Token Values
            services.AddScoped<ICurrentUserService, CurrentUserService>();

            // Take the Token Values


            services.AddScoped<DbContext, BoscoAFHDbContext>();


            services.AddControllers();

            // To access the files in web Browsers
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Sql Server Repository
            services.AddScoped<IDapperHandler, DapperHandler>();
            services.AddTransient<IJwtTokenGenerator, JwtTokenGenerator>();

            // Repository
            services.AddScoped(typeof(IDBRepository<>), typeof(DBRepository<>));
            services.AddScoped(typeof(IRoleRepository), typeof(RoleRepository));
            services.AddScoped(typeof(ICategoryRepository), typeof(CategoryRepository)); 



            // Services
            services.AddTransient<IWeatherForecastService, WeatherForecastService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ICategoryService, CategoryService>();


            return services;
        }
    }
}
