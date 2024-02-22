using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ShappingCartApi.Infrastructure;
using ShappingCartApi.Persistence;

namespace ShappingCardApi.Infrastructure
{
    public static class Config
    {
        public static void ConfigurationService(this IServiceCollection services)
        {

            IMapper mapper = MappingConfig.congigMaps().CreateMapper();
            services.AddSingleton(mapper);

            services.AddSwaggerGen(c =>
                {

                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "inCodityShoppingCartApi ", Version = "v1" });
                    c.AddSecurityDefinition("Bearer",new OpenApiSecurityScheme
                    {
                        Description = @"Enter 'Bearer' [space] and token",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer"
                    });

                    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type=ReferenceType.SecurityScheme,
                                    Id="Bearer"
                                },
                                Scheme="oauth2",
                                Name="Bearer",
                                In=ParameterLocation.Header
                            },
                            new List<string>()
                        }

                    });
                }
            );

            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiScope", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "incodity");
                });
            });
        }
        public static void Configuration(this IApplicationBuilder app,IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "InCodityShoppingCartApi"));
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
