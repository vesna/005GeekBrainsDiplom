using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;
using StoreMarket004.BLL;
using StoreMarket004.BLL.Abstractions;
using StoreMarket004.DAL.Contexts;
using StoreMarket004.GraphQL;
using StoreMarket004.Mappers;
using StoreMarket004.Securities;

namespace StoreMarket004
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<StoreContext>(options =>
            {
                options.UseLazyLoadingProxies().UseNpgsql(builder.Configuration.GetConnectionString("Storedb"));
            });
            builder.Services.AddDbContext<AuthContext>(options =>
            {
                options.UseLazyLoadingProxies().UseNpgsql(builder.Configuration.GetConnectionString("Authdb"));
            });
            builder.Services.AddAutoMapper(typeof(MappingProfile));

            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            //builder.Host.ConfigureContainer<ContainerBuilder>(x => x.RegisterType<ProductService>().As<IProductService>());
            builder.Services.AddMemoryCache(m => m.TrackStatistics = true);

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var configuration = new ConfigurationBuilder();
            configuration.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            var cfg = configuration.Build();

            builder.Host.ConfigureContainer<ContainerBuilder>(x => x.Register(c => new StoreContext(cfg.GetConnectionString("db"))).InstancePerDependency());
           // builder.Services.AddDirectoryBrowser();
            //!!!!!

            var jwt = builder.Configuration.GetSection("JwtConfiguration").Get<JwtConfiguration>()
               ?? throw new Exception("JwtConfiguration not found");
            builder.Services
                .AddSingleton(provider => jwt)
                .AddSingleton<IProductService, ProductService>()
                .AddSingleton<ICategoryService, CategoryService>()
                .AddSingleton<IStoreService, StoreService>()
                .AddSingleton<IAuthService, AuthService>()
                .AddSingleton<IEncryptService, EncryptService>()
                .AddSingleton<ITokenService, TokenService>()
                .AddGraphQLServer()
                .AddQueryType<Query>()
                .AddMutationType<Mutation>();

            builder.Services.AddAuthorization();
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwt.Issuer,
                        ValidAudience = jwt.Audience,
                        IssuerSigningKey = jwt.GetSingingKey()
                    };

                });

            builder.Services.AddSwaggerGen(
                    options =>
                    {
                        options.AddSecurityDefinition(
                            JwtBearerDefaults.AuthenticationScheme,
                            new()
                            {
                                In = ParameterLocation.Header,
                                Description = "",
                                Name = "Authorization",
                                Type = SecuritySchemeType.Http,
                                BearerFormat = "Jwt Token",
                                Scheme = JwtBearerDefaults.AuthenticationScheme
                            });
                        options.AddSecurityRequirement(new() {
                            {
                                new OpenApiSecurityScheme
                                {
                                    Reference = new OpenApiReference
                                    {
                                        Type = ReferenceType.SecurityScheme,
                                        Id = JwtBearerDefaults.AuthenticationScheme
                                    }
                                },
                                new List<string>()
                            }
                        });
                    }
            );
            var app = builder.Build();
            app.MapGraphQL();
            
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();
            app.UseAuthentication();
            app.MapControllers();

            app.Run();
        }
    }
}
