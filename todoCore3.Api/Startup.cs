using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using todoCore3.Api.Models;
using todoCore3.Api.Services;

namespace todoCore3.Api
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
      services.AddDbContext<TodoContext>(opt => opt.UseSqlServer("Data Source=sql;Database=todos;Integrated Security=false;User ID=sa;Password=p@ssw0rd"));
      services.AddCors();
      services.AddControllers();
      services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

      var appSettingsSection = Configuration.GetSection("AppSettings");
      services.Configure<AppSettings>(appSettingsSection);

      var appSettings = appSettingsSection.Get<AppSettings>();
      var key = Encoding.ASCII.GetBytes(appSettings.Secret);
      services.AddAuthentication(s =>
      {
        s.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        s.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      })
      .AddJwtBearer(j =>
      {
        j.Events = new JwtBearerEvents
        {
          OnTokenValidated = context =>
          {
            var userService = context.HttpContext.RequestServices.GetRequiredService<IUserService>();
            var userId = int.Parse(context.Principal.Identity.Name);
            var user = userService.GetBy(userId);
            if (user == null)
            {
              context.Fail("Unauthorized");
            }

            return Task.CompletedTask;
          }
        };

        j.RequireHttpsMetadata = false;
        j.SaveToken = true;
        j.TokenValidationParameters = new TokenValidationParameters
        {
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = new SymmetricSecurityKey(key),
          ValidateIssuer = false,
          ValidateAudience = false
        };
      });

      services.AddScoped<IUserService, UserService>();

      //services.AddSwaggerGen(c =>
      //{
      //	c.SwaggerDoc("v1", new OpenApiInfo { Title = "Todo API", Version = "v1" });
      //});
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
          Version = "v1",
          Title = "ToDo API",
          Description = "Todo App 을 만드는 꽤 복잡한 방법에 대한 ASP.NET Core WebAPI",
          TermsOfService = new Uri("http://todo.shockz.io/terms"),
          Contact = new OpenApiContact
          {
            Name = "shockz",
            Email = string.Empty, // 스팸은 먹는겁니다.
            Url = new Uri("https://twitter.com/somebody"), // 트위터를 안써봐서...
          },
          License = new OpenApiLicense
          {
            Name = "MIT",
            Url = new Uri("https://github.com/shockzinfinity/todo-app-complicated/blob/2c4c937fa9ecfca72e37ba4e79581e2eabe4e9b8/LICENSE#L1")
          }
        });

        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        c.IncludeXmlComments(xmlPath);
      });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      //app.UseHttpsRedirection();

      app.UseForwardedHeaders(new ForwardedHeadersOptions
      {
        ForwardedHeaders = Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedFor | Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedProto
      });

      app.UseSerilogRequestLogging();

      app.UseStaticFiles(); // Swagger UI 가 Static files를 사용하므로 추가

      app.UseSwagger(); // JSON endpoint 로 생성된 Swagger 활성화
      app.UseSwaggerUI(c =>
      {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Todo API v1");
        c.RoutePrefix = string.Empty;
      });

      app.UseRouting();

      app.UseCors(c => c.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

      app.UseAuthentication();
      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
