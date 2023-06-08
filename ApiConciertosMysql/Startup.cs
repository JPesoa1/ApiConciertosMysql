using ApiConciertosMysql.Data;
using ApiConciertosMysql.Helpers;
using ApiConciertosMysql.Models;
using ApiConciertosMysql.Repositories;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ApiConciertosMysql;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container
    public  void ConfigureServices(IServiceCollection services)
    {
        

     

        string miSecreto = HelperSecretManager.GetSecretAsync().Result;
        KeysModel model = JsonConvert.DeserializeObject<KeysModel>(miSecreto);
        services.AddSingleton<KeysModel>(model);



        services.AddTransient<RepositoryConcierto>();
        services.AddDbContext<ConciertoContext>
            (options => options.UseMySql(model.MySql, ServerVersion.AutoDetect(model.MySql)));

        services.AddCors(options =>
        {
            options.AddPolicy("AllowOrigin", x => x.AllowAnyOrigin());
        });


        services.AddControllers();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        app.UseCors(options => options.AllowAnyOrigin());

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapGet("/", async context =>
            {
                await context.Response.WriteAsync("Welcome to running ASP.NET Core on AWS Lambda");
            });
        });
    }
}