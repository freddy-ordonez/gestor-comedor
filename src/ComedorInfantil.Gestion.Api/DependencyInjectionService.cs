using ComedorInfantil.Gestion.Api.ActionFilters;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace ComedorInfantil.Gestion.Api
{
    public static class DependencyInjectionService
    {
        public static IServiceCollection AddWebApi(this IServiceCollection services) 
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            });

            services.AddScoped<GenericValidationFilter>();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo 
                { 
                    Version = "v1",
                    Title = "Gestion de Comedor API",
                    Description = "API para la Gestión Integral del Comedor" +
                    "\nEsta API permite administrar de forma centralizada y eficiente los diferentes procesos del comedor, incluyendo:" +
                    "\n- Registro y control de beneficiarios (niños y familias atendidas)" +
                    "\n- Gestión de inventario de alimentos y suministros" +
                    "\n- Registro de actividades y eventos organizados" +
                    "\n- Administración de donaciones y donantes" +
                    "\n- Gestión de voluntarios y su participación" +
                    "\n\nProporciona endpoints seguros y organizados para facilitar la integración con aplicaciones web o móviles, permitiendo una mejor coordinación, trazabilidad y transparencia de las operaciones del comedor." +
                    "\n\nAutenticación basada en JWT. Requiere autorización para acceder a la mayoría de los recursos.",
                    Contact = new OpenApiContact
                    {
                        Name = "Freddy Antonio Ordonez Aguilar",
                        Email = "freddy12aguilar@outlook.com",
                        Url = new Uri("https://github.com/freddy-ordonez")
                    }
                });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme 
                {
                    In = ParameterLocation.Header,
                    Description = "Ingrese un Token válido",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement 
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[]{}
                    }
                });

                var fileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, fileName));
            });
            return services;
        }
    }
}
