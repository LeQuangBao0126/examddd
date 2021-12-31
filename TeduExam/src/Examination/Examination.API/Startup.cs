using Examination.Application.Commands.StartExam;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Examination.Application.Mapping;
using Examination.Application.Queries;
using Examination.Domain.AggregateModels.ExamAggregate;
using Examination.Domain.AggregateModels.ExamResultAggregate;
using Examination.Domain.AggregateModels.UserAggregate;
using Examination.Infrastructure.Repositories;
using Examination.Infrastructure.SeedWork;
using MediatR;
using Microsoft.AspNetCore.Cors.Infrastructure;
using MongoDB.Driver;

namespace Examination.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApiVersioning(c =>
            {
                c.ReportApiVersions = true;
            });
            services.AddVersionedApiExplorer(
                options =>
                {
                    options.GroupNameFormat = "'v'VVV";
                    options.SubstituteApiVersionInUrl = true;
                } );
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Examination.API", Version = "v1" });
                c.SwaggerDoc("v2", new OpenApiInfo { Title = "Examination.API", Version = "v2" });
            });
            services.Configure<ExamSettings>(Configuration);
            services.AddSingleton<IMongoClient>(c =>
            {
                var user = Configuration.GetValue<string>("DatabaseSettings:User");
                var password = Configuration.GetValue<string>("DatabaseSettings:Password");
                var server = Configuration.GetValue<string>("DatabaseSettings:Server");
                var databaseName = Configuration.GetValue<string>("DatabaseSettings:DatabaseName");
                var connStr = "mongodb://" + user + ":" + password + "@" + server + "/" + databaseName + "?authSource=admin";
                return new MongoClient(connStr);
            });
            services.AddScoped(c=>c.GetService<IMongoClient>()?.StartSession());
            //cho phep api này cors 
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",builder => 
                    builder.SetIsOriginAllowed(_ => true)
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });
            
            services.AddAutoMapper(c => c.AddProfile(new MappingProfile()));
            services.AddMediatR(typeof(GetHomeExamListQueryHandler).Assembly );

         
            
            services.AddTransient<IExamRepository, ExamRepository>();
            services.AddTransient<IExamResultRepository ,ExamResultRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
         
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Examination.API v1");
                    c.SwaggerEndpoint("/swagger/v2/swagger.json", "Examination.API v2");
                });
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("CorsPolicy");
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
