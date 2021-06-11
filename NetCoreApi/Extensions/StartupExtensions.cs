using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NetCoreApi.Cqrs.Handlers.Items;
using NetCoreApi.Data.Access.DAL;
using NetCoreApi.Filters;
using NetCoreApi.Helpers;
using NetCoreApi.Queries;
using NetCoreApi.Security;
using NetCoreApi.Security.Token;

namespace NetCoreApi
{
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Register validate model filter.
        /// </summary>
        /// <param name="services">Services</param>
        public static void AddValidateModelFilter(this IServiceCollection services)
        {
            services.AddScoped<ValidateModelAttribute>();
        }

        /// <summary>
        /// Register DB Context and Data Access classes.
        /// </summary>
        /// <param name="services">Services</param>
        public static void AddUow(this IServiceCollection services)
        {
            services.AddDbContext<MainDbContext>(options =>
                options.UseInMemoryDatabase("NetCoreApi"));

            services.AddScoped<IUnitOfWork>(ctx => new EFUnitOfWork(ctx.GetRequiredService<MainDbContext>()));

            services.AddScoped<IActionTransactionHelper, ActionTransactionHelper>();
            services.AddScoped<UnitOfWorkFilterAttribute>();
        }

        /// <summary>
        /// Register authentication and authorization classes.
        /// </summary>
        /// <param name="services">Services</param>
        public static void AddAuth(this IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, (o) =>
            {
                o.TokenValidationParameters = new TokenValidationParameters()
                {
                    IssuerSigningKey = TokenOptions.Key,
                    ValidAudience = TokenOptions.Audience,
                    ValidIssuer = TokenOptions.Issuer,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ClockSkew = TimeSpan.FromMinutes(0)
                };
            });

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy(JwtBearerDefaults.AuthenticationScheme, new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ITokenBuilder, TokenBuilder>();
            services.AddScoped<ISecurityContext, SecurityContext>();
        }

        /// <summary>
        /// Register as scoped all commands, queries, handlers from CQRS assembly.
        /// </summary>
        /// <param name="services">Services</param>
        public static void AddCqrs(this IServiceCollection services)
        {
            foreach (var type in GetAllClassesFromTypeAssembly(typeof(CreateItemHandler), false))
            {
                var interfaceQ = type.GetTypeInfo().GetInterfaces().First();
                services.AddScoped(interfaceQ, type);
            }
        }

        /// <summary>
        /// Register as scoped all query processors from queries assembly.
        /// </summary>
        /// <param name="services">Services</param>
        public static void AddQueries(this IServiceCollection services)
        {
            foreach (var type in GetAllClassesFromTypeAssembly(typeof(ItemsQueryProcessor), true))
            {
                var interfaceQ = type.GetTypeInfo().GetInterfaces().First();
                services.AddScoped(interfaceQ, type);
            }
        }

        /// <summary>
        /// Gets all classes from type assembly.
        /// </summary>
        /// <param name="type">Type from assembly</param>
        /// <param name="onlyFromTypeNamespace">Return all types only from type namespace</param>
        /// <returns>Returns all classes from type assembly.</returns>
        private static Type[] GetAllClassesFromTypeAssembly(Type type, bool onlyFromTypeNamespace)
        {
            return (from t in type.GetTypeInfo().Assembly.GetTypes()
                    where (t.Namespace == type.Namespace || !onlyFromTypeNamespace)
                          && t.GetTypeInfo().IsClass
                          && !t.GetTypeInfo().IsAbstract
                          && t.GetTypeInfo().GetCustomAttribute<CompilerGeneratedAttribute>() == null
                    select t).ToArray();
        }

        /// <summary>
        /// Configure swagger.
        /// </summary>
        /// <param name="app">Application builder</param>
        public static void ConfigureSwagger(this IApplicationBuilder app)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }

        /// <summary>
        /// Ensure database is seed.
        /// </summary>
        /// <param name="app">Application builder</param>
        public static void InitDatabase(this IApplicationBuilder app)
        {
            var serviceScopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();

            using var serviceScope = serviceScopeFactory.CreateScope();
            var dbContext = serviceScope.ServiceProvider.GetService<MainDbContext>();

            dbContext.Database.EnsureCreated();
        }
    }
}
