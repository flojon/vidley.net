using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using vidley.net.Data;

namespace vidley.net
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.Configure<Settings>(options => 
            {
                options.ConnectionString
                    = Configuration.GetSection("MongoConnection:ConnectionString").Value;
                options.Database
                    = Configuration.GetSection("MongoConnection:Database").Value;
                options.JwtSettings = new JwtSettings
                {
                    Key = Configuration.GetSection("Jwt:Key").Value,
                    Issuer = Configuration.GetSection("Jwt:Issuer").Value,
                    Audience = Configuration.GetSection("Jwt:Audience").Value,
                    TimeSpanMinutes = Configuration.GetValue<int>("Jwt:TimeSpanMinutes"),
                };
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options => 
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = Configuration.GetSection("Jwt:Issuer").Value,
                            ValidAudience = Configuration.GetSection("Jwt:Audience").Value,
                            IssuerSigningKey = new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(Configuration.GetSection("Jwt:Key").Value)
                            ),
                        };
                    });

            services.AddCors();

            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors(builder => builder.WithOrigins("https://localhost:4200")
                                    .AllowAnyMethod()
                                    .WithHeaders(new []{"authorization",  "content-type"})
            );

            app.UseAuthentication();

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
