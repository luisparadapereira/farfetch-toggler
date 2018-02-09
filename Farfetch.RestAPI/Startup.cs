using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using Farfetch.Automapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;

namespace Farfetch.RestAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                if (c == null) return;

                c.SwaggerDoc("v1", new Info {Title = "Farfetch API", Version = "v1"});

                var basePath = AppContext.BaseDirectory;
                var xmlPath = Path.Combine(basePath, "Farfetch.RestAPI.xml");
                c.IncludeXmlComments(xmlPath);
            });
            
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true, // validates the server
                        ValidateAudience = true, // validates the recipient
                        ValidateLifetime = true, // valitade token expiration
                        ValidateIssuerSigningKey = true, // validates key is part of trusted keys
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience = Configuration["Jwt:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey
                        (
                            Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])
                        )
                    };
                });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
                        .Build());
            });

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c?.SwaggerEndpoint("/swagger/v1/swagger.json", "Farfetch API v1");
            });

            app.UseAuthentication();

            app.UseCors("CorsPolicy");

            app.UseMvc();

            Initializer automapperInit = new Initializer();
            automapperInit.RegisterMappings();
        }
    }
}
