namespace NBUCareers.Web.Config
{
    using System.Text;

    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Azure;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;
    using Microsoft.OpenApi.Models;

    using NBUCareers.Data;
    using NBUCareers.Infrastructure.Mapping;
    using NBUCareers.Infrastucture.Services;
    using NBUCareers.Models;
    using NBUCareers.Services.Contracts;
    using NBUCareers.Services.Implementations;

    public static class IoC
    {
        public static void Configure(IServiceCollection services, IConfiguration configuration)
        {
            AddAutoMapper(services);
            AddDatabase(services, configuration);
            AddIdentity(services);
            AddJwtAuthentication(services, configuration);
            AddInfrastructureServices(services);
            AddDataServices(services);
            AddSwagger(services);
            // TODO: 
            //AddBlobService(services, configuration);
        }

        private static void AddAutoMapper(IServiceCollection services)
            => services.AddAutoMapper(typeof(AutoMapperProfile));

        private static void AddDatabase(IServiceCollection services, IConfiguration configuration)
            => services
                .AddDbContext<AppDbContext>(options => options
                       .UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        private static void AddIdentity(IServiceCollection services)
            => services
                .AddIdentity<User, IdentityRole>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 6;
                    options.Password.RequiredUniqueChars = 1;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;
                })
                .AddEntityFrameworkStores<AppDbContext>();

        private static void AddJwtAuthentication(IServiceCollection services, IConfiguration configuration)
            => services
                .AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["AppSecret"])),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

        private static void AddSwagger(IServiceCollection services)
            => services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "NBU Careers API", Version = "v1" });
                });

        private static void AddBlobService(IServiceCollection services, IConfiguration configuration)
        {
            services.AddAzureClients(builder =>
            {
                builder.AddBlobServiceClient(configuration.GetConnectionString("BlobStorage"));
            });
        }

        private static void AddInfrastructureServices(IServiceCollection services)
            => services.AddTransient<IUserService, UserService>();

        private static void AddDataServices(IServiceCollection services)
        {
            services.AddTransient<IApplicationService, ApplicationService>();
            services.AddTransient<ICompanyService, CompanyService>();
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddTransient<IJobOfferService, JobOfferService>();
        }
    }
}
