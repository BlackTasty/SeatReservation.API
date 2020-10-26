using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SeatReservation.Api.Authentication;
using SeatReservation.Api.Common;
using SeatReservation.Api.Configuration;
using SeatReservation.Api.Database;
using SeatReservation.Api.Mapping;
using SeatReservation.Api.Repositories.Implementation;
using SeatReservation.Api.Repositories.Interface;
using SeatReservation.Api.Services.Implementation;
using SeatReservation.Api.Services.Interface;
using SeatReservation.Api.Util;
using Swashbuckle.AspNetCore.Swagger;

namespace SeatReservation.Api
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
            #region Repositories
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IRoomRepository, RoomRepository>();
            services.AddScoped<IScheduleRepository, ScheduleRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ISeatTypeRepository, SeatTypeRepository>();
            services.AddScoped<IReservationRepository, ReservationRepository>();
            services.AddScoped<ILocationRepository, LocationRepository>();
            #endregion
            #region Services
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IScheduleService, ScheduleService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ISeatTypeService, SeatTypeService>();
            services.AddScoped<IReservationService, ReservationService>();
            services.AddScoped<ILocationService, LocationService>();
            #endregion
            #region Parser
            services.AddScoped<IParser, Parser>();
            #endregion
            #region Application settings
            var settings = Configuration.GetSection("Database").Get<DatabaseConfiguration>();
            string connectionString = String.Format("Server={0};Database={1};User={2};Password={3};", settings.Host, settings.Name, settings.User, settings.Password);
            services.AddDbContext<DatabaseContext>();
            #endregion
            #region Swagger
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "{tasty.apps.api}", Version = "{0.0.1}" });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //c.IncludeXmlComments(xmlPath);
            });
            #endregion

            services.AddAutoMapper();
            // Add if required
            services.AddCors();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1).AddJsonOptions(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            #region Authentication
            services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (string.IsNullOrWhiteSpace(env.WebRootPath))
            {
                env.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            }

            // Add if required
            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            #region Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Seat Reservation API v1");
            });
            #endregion

            app.UseAuthentication();
            app.UseMvc();

            this.MigrateDatabase(app);
        }

        private void MigrateDatabase(IApplicationBuilder app)
        {
            var genreConfiguration = Configuration.GetSection("Genre").Get<GenreConfiguration>();
            var userConfiguration = Configuration.GetSection("User").Get<UserConfiguration>();
            var movieConfiguration = Configuration.GetSection("Movie").Get<MovieConfiguration>();
            //var movieGenreConfiguration = Configuration.GetSection("MovieGenre").Get<MovieGenreConfiguration>();
            var permissionConfiguration = Configuration.GetSection("Permission").Get<PermissionConfiguration>();
            var reservationConfiguration = Configuration.GetSection("Reservation").Get<ReservationConfiguration>();
            var roomConfiguration = Configuration.GetSection("Room").Get<RoomConfiguration>();
            var roomPlanConfiguration = Configuration.GetSection("RoomPlan").Get<RoomPlanConfiguration>();
            var scheduleConfiguration = Configuration.GetSection("Schedule").Get<ScheduleConfiguration>();
            var scheduleSlotConfiguration = Configuration.GetSection("ScheduleSlot").Get<ScheduleSlotConfiguration>();
            var seatTypeConfiguration = Configuration.GetSection("SeatType").Get<SeatTypeConfiguration>();
            var seatPositionConfiguration = Configuration.GetSection("SeatPosition").Get<SeatPositionConfiguration>();
            var locationConfiguration = Configuration.GetSection("Location").Get<LocationConfiguration>();
            var roomAssignmentConfiguration = Configuration.GetSection("RoomAssignment").Get<RoomAssignmentConfiguration>();
            var roomTechnologyConfiguration = Configuration.GetSection("RoomTechnology").Get<RoomTechnologyConfiguration>();
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<DatabaseContext>();
                context.Database.Migrate();

                GenreSeed.Seed(context, genreConfiguration.Genres);
                UserSeed.Seed(context, userConfiguration.Users);
                PermissionSeed.Seed(context, permissionConfiguration.Permissions);
                SeatTypeSeed.Seed(context, seatTypeConfiguration.SeatTypes);
                LocationSeed.Seed(context, locationConfiguration.Locations);
                RoomTechnologySeed.Seed(context, roomTechnologyConfiguration.RoomTechnologies);
            }
        }
    }
}
