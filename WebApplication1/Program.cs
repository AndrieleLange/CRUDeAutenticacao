using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Repositorios.Interfaces;
using WebApplication1.Repositorios;
using WebApplication1;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace SistemaUser
{
    public class Program
    {
        public static void Main(string[] args) 
        { 
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            builder.Services.AddDbContext<SistemadeUsers>(options => options.UseInMemoryDatabase("InMemoryDb"));

            builder.Services.AddScoped<IUserRepo, UserRepo>();

            ConfigureAuthentication(builder.Services);

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            // populando banco de dados para testes
            using (var scope = app.Services.CreateScope())
            {
               var services = scope.ServiceProvider;
                var context = services.GetRequiredService<SistemadeUsers>();

                if(!context.Usuarios.Any())
                {
                    var users = new List<User>
                    {
                        new User { name = "Andriele", email = "andriele.lange@edu.pucrs.br", password = "exemplepass123", level = 5},
                        new User { name = "Bruno", email = "bruno.neves@exemple.com", password = "exemplepass123", level = 1 }
                    };

                    context.Usuarios.AddRange(users);
                    context.SaveChanges();
                }
            }

                app.Run();
        }

        public static void ConfigureAuthentication(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers();

            var key = Encoding.ASCII.GetBytes(Settings.Secret);
            services.AddAuthentication(x =>
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
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }
    }
}