using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MediatR;
using sunny_dn_01.DataContext;
using Microsoft.EntityFrameworkCore;
using sunny_dn_01.Repository;
using sunny_dn_01.Domains;
using sunny_dn_01.Service.UserService;
using System.Collections.Generic;
using AutoMapper;
using sunny_dn_01.Service.KafkaService;
using System.Reflection;
//using FluentValidation;

namespace sunny_dn_01
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        readonly string AllowedCorsOrigins = "myAllowOrigins";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.AddCors(options =>
            {
                options.AddPolicy(name: AllowedCorsOrigins, builder => {
                    builder.WithOrigins("http://localhost:4200")
                    .AllowAnyHeader()
                    .AllowAnyMethod(); ;
                });
            });
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("SQLConnection")));

            services.AddControllersWithViews();

            services.AddAutoMapper(typeof(Startup));
            services.AddMediatR(typeof(Startup));
            services.AddMediatR(Assembly.GetExecutingAssembly(),
                typeof(IKafListener).Assembly,
                typeof(CreateUserCommand).Assembly);

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IVotingRepository, VotingRepository>();

            //services.AddTransient<IValidator<User>, UserModelValidator>();

            services.AddTransient<IRequestHandler<GetUserQuery, List<User>>, GetUserQueryHandler>();
            services.AddTransient<IRequestHandler<GetUserByIdQuery, User>, GetUserByIdQueryHandler>();
            services.AddTransient<IRequestHandler<GetUserByEmailQuery, User>, GetUserByEmailQueryHandler>();
            services.AddTransient<IRequestHandler<GetVotingsQuery, List<Voting>>, GetVotingsHandler>();
            services.AddTransient<IRequestHandler<CreateUserCommand, User>, CreateUserCommandHandler>();
            services.AddTransient<IRequestHandler<CancelCandidateCommand, int>, CancelCandidateCommandHandler>();
            services.AddTransient<IKafListener, KafkaConsumerHostedService>();

            services.AddSingleton<IKafPublisher, KafPublisher>();
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(AllowedCorsOrigins);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });
            /*
            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
            */
        }
    }
}
