using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using ServerSentEvents.Core.Data;
using ServerSentEvents.Domain.Features.Orders;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;

namespace ServerSentEvents.Api
{
    public static class Dependencies
    {
        public static void Configure(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "",
                    Description = "",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "",
                        Email = ""
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under MIT",
                        Url = new Uri("https://opensource.org/licenses/MIT"),
                    }
                });

                options.CustomSchemaIds(x => x.FullName);
            });

            services.AddCors(options => options.AddPolicy("CorsPolicy",
                builder => builder
                .WithOrigins("http://localhost:4200")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(isOriginAllowed: _ => true)
                .AllowCredentials()));

            services.AddHttpContextAccessor();

            services.AddMediatR(typeof(GetOrders));

            services.AddTransient<IServerSentEventsDbContext, ServerSentEventsDbContext>();

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler
            {
                InboundClaimTypeMap = new Dictionary<string, string>()
            };

            services.AddDbContext<ServerSentEventsDbContext>(options =>
            {
                options.UseSqlServer(configuration["Data:DefaultConnection:ConnectionString"],
                    builder => builder.MigrationsAssembly("ServerSentEvents.Api")
                        .EnableRetryOnFailure())
                .UseLoggerFactory(ServerSentEventsDbContext.ConsoleLoggerFactory)
                .EnableSensitiveDataLogging();
            });

            services.AddControllers();
        }
    }
}
