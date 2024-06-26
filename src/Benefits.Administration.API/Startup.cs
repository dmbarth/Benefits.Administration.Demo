using Benefits.Administration.API.Middleware;
using Benefits.Administration.Application.Extensions;
using Benefits.Administration.Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Benefits.Administration.Demo
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
      services
        .AddMvcCore()
        .AddApiExplorer();

      services
        .AddApplication(Configuration)
        .AddInfrastructure(Configuration)
        .AddCors(options => options.AddDefaultPolicy(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()))
        .AddControllers();

      services.AddSwaggerGen();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app
        .UseSwagger()
        .UseSwaggerUI(setup => setup.SwaggerEndpoint("/swagger/v1/swagger.json", "Benefits Administration API"))
        .UseHttpsRedirection()
        .UseRouting()
        .UseCors()
        .UseMiddleware<GlobalExceptionMiddleware>()
        .UseAuthorization()
        .UseEndpoints(endpoints =>
        {
          endpoints.MapControllers();
        })
        .UseWelcomePage();
    }
  }
}
