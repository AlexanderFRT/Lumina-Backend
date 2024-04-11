
using Lumina_Backend.Data;
// using Lumina_Backend.Repository.User;
using Microsoft.EntityFrameworkCore;


namespace Lumina_Backend;

public class Program
{
    public static void Main(string[] args)
    {

        var builder = WebApplication.CreateBuilder(args);
        var connectionString = Environment.GetEnvironmentVariable("DATABASE_URL");
     

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddEntityFrameworkNpgsql()
            .AddDbContext<ApiDbContext>(opt =>
                opt.UseNpgsql(connectionString));

        //  Cuando ya se vaya a hacer el despliegue local en Docker se elimina la env variable connectionString y se modifica el servicio nuevamente con el c�digo de abajo, y se habilita el string del appsettings.json
        //      opt.UseNpgsql(builder.Configuration.GetConnectionString("LuminaConnection")));

        //builder.Services.AddTransient<IUserRepository, UserRepository>();
        builder.Services.AddAuthorization();
        builder.Services.AddAuthentication("Bearer").AddJwtBearer();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}
