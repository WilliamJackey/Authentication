using System;
using System.Net;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using AutoMapper;
using FluentValidation.AspNetCore;
using AcmeWidget.Authentication;
using AcmeWidget.Data;
using AcmeWidget.Extensions;
using AcmeWidget.Helpers;
using AcmeWidget.Models;
using AcmeWidget.Models.Entities;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace AcmeWidget
{
    public class Startup
    {
        private const string SecretKey = "erSFFSF2343isfsEieIEFKLJ4049FDFsfs";//to do: save it in somewhere
        private readonly SymmetricSecurityKey _symmetricSecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));
        public Startup(IConfiguration configuration)
        {
          Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
          // Add framework services.
          services.AddDbContext<AcmeDbContext>(options =>
              options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                  b => b.MigrationsAssembly("AcmeWidget")));

          services.AddSingleton<IJwtFactory, JwtFactory>();

          services.TryAddTransient<IHttpContextAccessor, HttpContextAccessor>();

          //// jwt wire up
          //// Get options from app settings
          var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuerOptions));

          //// Configure JwtIssuerOptions
          services.Configure<JwtIssuerOptions>(options =>
          {
            options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
            options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
            options.SigningCredentials = new SigningCredentials(_symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
          });

          var corsBuilder = new CorsPolicyBuilder();
          corsBuilder.AllowAnyHeader();
          corsBuilder.AllowAnyMethod();
          corsBuilder.AllowAnyOrigin(); // For anyone access.
          corsBuilder.AllowCredentials();

          services.AddCors(options =>
          {
            options.AddPolicy("SiteCorsPolicy", corsBuilder.Build());
          });

          var tokenValidationParameters = new TokenValidationParameters
          {
            ValidateIssuer = true,
            ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],

            ValidateAudience = true,
            ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = _symmetricSecurityKey,

            RequireExpirationTime = false,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
          };

          services.AddAuthentication(options =>
          {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

          }).AddJwtBearer(configureOptions =>
          {
            configureOptions.ClaimsIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
            configureOptions.TokenValidationParameters = tokenValidationParameters;
            configureOptions.SaveToken = true;
          });

          // api user claim policy
          services.AddAuthorization(options =>
          {
            options.AddPolicy("ApiUser", policy => policy.RequireClaim(Constants.Strings.JwtClaimIdentifiers.Rol, Constants.Strings.JwtClaims.AcmeSubscription));
          });

          // add identity
          var builder = services.AddIdentityCore<AuthenticationUser>(o =>
          {
            // configure identity options
            o.Password.RequireDigit = false;
            o.Password.RequireLowercase = false;
            o.Password.RequireUppercase = false;
            o.Password.RequireNonAlphanumeric = false;
            o.Password.RequiredLength = 6;
          });
          builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), builder.Services);
          builder.AddEntityFrameworkStores<AcmeDbContext>().AddDefaultTokenProviders();

          services.AddAutoMapper();
          services.AddMvc().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());

    }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
          if (env.IsDevelopment())
          {
            app.UseDeveloperExceptionPage();
          }

          app.UseExceptionHandler(
              builder =>
              {
                builder.Run(
                          async context =>
                          {
                            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                            context.Response.Headers.Add("Access-Control-Allow-Origin", "*");

                            var error = context.Features.Get<IExceptionHandlerFeature>();
                            if (error != null)
                            {
                              context.Response.AddApplicationError(error.Error.Message);
                              await context.Response.WriteAsync(error.Error.Message).ConfigureAwait(false);
                            }
                          });
              });

          app.UseAuthentication();
          app.UseDefaultFiles();
          app.UseStaticFiles();
          app.UseMvc();
        }
    }
}
