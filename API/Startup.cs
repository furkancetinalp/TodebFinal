using API.Configuration.Filters.Log;
using BackgroundJobs.Abstract;
using BackgroundJobs.Concrete;
using BackgroundJobs.Concrete.HangfireJobs;
using Business.Abstract;
using Business.Concrete;
using Business.Configuration.Cache;
using Business.Configuration.Mapper;
using DAL.Abstract;
using DAL.Concrete.EF;
using DAL.Concrete.Mongo;
using DAL.DbContexts;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace API
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

            services.AddDbContext<ApartmentSystemDbContext>(ServiceLifetime.Transient);

            //Redis implementation
            var redisConfigInfo = Configuration.GetSection("RedisEndpointInfo").Get<RedisEndpointInfo>();
            services.AddStackExchangeRedisCache(opt =>
            {
                opt.ConfigurationOptions = new ConfigurationOptions()
                {
                    EndPoints =
                    {
                        {redisConfigInfo.EndPoint,redisConfigInfo.Port}
                    },
                    Password = redisConfigInfo.Password,
                    User = redisConfigInfo.UserName,
                };
            });
            //In Memory Cache Use Case 
            services.AddMemoryCache();

            //Auto mapper
            services.AddAutoMapper(config =>
            {
                config.AddProfile(new MapperProfile());
            });
            //DI
            services.AddScoped<IHouseRepository, HouseRepository>();
            services.AddScoped<IHouseService, HouseService>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IHouseRepository, HouseRepository>();

            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IAuthService, AuthService>();

            services.AddScoped<IBillRepository, BillRepository>();
            services.AddScoped<IBillService, BillService>();

            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IMessageService, MessageService>();
            //MongoDb
            services.AddSingleton<MongoClient>(x => new MongoClient("mongodb://localhost:27017"));
            services.AddScoped<ICreditCardRepository, CreditCardRepository>();
            services.AddScoped<ICreditCardService, CreditCardService>();

            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IPaymentService, PaymentService>();
            //Mail service and background jobs
            services.AddScoped<IJobs, HangfireJobs>();
            services.AddScoped<ISendMailService, SendMailService>();

            //Logger
            services.AddSingleton<MsSqlLogger>();


            //Redis dependency
            services.AddScoped<ICacheExample, CacheExample>();


            // Adding Hangfire services.
            var hangfireDb = Configuration.GetConnectionString("HangfireConnection");
            services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(hangfireDb, new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    DisableGlobalLocks = true
                }));
            services.AddHangfireServer();


            //TOKEN INITIALIZATON
            var tokenOptions = Configuration.GetSection("TokenOptions").Get<Business.Configuration.Auth.TokenOption>();
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = tokenOptions.Issuer,
                        ValidAudience = tokenOptions.Audience,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SecurityKey))
                    };
                });

            //Exception addition
            services.AddControllers(opt=>
            {

            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });
            });

        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
            }

            //Exception
            app.UseExceptionHandler(c => c.Run(async context =>
            {
                var exception = context.Features.Get<IExceptionHandlerFeature>().Error;
                var jsonResult = new JsonResult(
                   new
                   {
                       error = exception.Message,
                       innerException = exception.InnerException,
                       statusCode = HttpStatusCode.InternalServerError
                   }
               );
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await context.Response.WriteAsJsonAsync(jsonResult);
            }));
            //Hangfire
            app.UseHangfireDashboard("/hangfire",new DashboardOptions()
            {
            });
            //Recurring Job
            RecurringJob.AddOrUpdate<IJobs>(x => x.RecurringJob(), Cron.Daily);

            app.UseRouting();

            //After token implementation, app.UseAuthentication must be included here
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
