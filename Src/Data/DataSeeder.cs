using Microsoft.EntityFrameworkCore;
using TallerBackendIDWM.Src.Data;
using TallerBackendIDWM.Src.Models;

namespace TallerBackendIDWMallerBackendIDWM.Src.Data
{
    public class DataSeeder
    {
        public static void Initialize(IServiceProvider serviceProvider){
            using (var scope = serviceProvider.CreateScope()){

                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<DataContext>();
                
                if(!context.Roles.Any()){
                    context.Roles.AddRange(
                        new Role {Type = "Admin"},
                        new Role { Type = "Usuario"}
                    );
                }

                if(!context.Genders.Any()){
                    context.Genders.AddRange(
                        new Gender { Type = "Masculino"},
                        new Gender { Type = "Femenino"},
                        new Gender { Type = "Prefiero no decirlo"},
                        new Gender { Type = "Otros"}
                    );
                }
                
                if (context.Products.Any())
                {
                    return;
                }

                var products = new List<Product>
                {
                    new Product
                    {
                        Name = "Polera de Algodón Blanca",
                        Type = "Poleras",
                        Price = 19990,
                        Stock = 150,
                        Image = "https://ejemplo.com/imagen1.jpg"
                    },
                    new Product
                    {
                        Name = "Gorro de Lana Azul",
                        Type = "Gorros",
                        Price = 14990,
                        Stock = 200,
                        Image = "https://ejemplo.com/imagen2.jpg"
                    },
                    new Product
                    {
                        Name = "Muñeco de Juguete",
                        Type = "Jugueteria",
                        Price = 29990,
                        Stock = 75,
                        Image = "https://ejemplo.com/imagen3.jpg"
                    },
                    new Product
                    {
                        Name = "Pack de Comida Saludable",
                        Type = "Alimentacion",
                        Price = 49990,
                        Stock = 50,
                        Image = "https://ejemplo.com/imagen4.jpg"
                    },
                    new Product
                    {
                        Name = "Libro de Programación en C#",
                        Type = "Libros",
                        Price = 34990,
                        Stock = 120,
                        Image = "https://ejemplo.com/imagen5.jpg"
                    }
                };

                var generatedRuts = new HashSet<string>();

                if (!context.Users.Any())
                {
                    var administrador = new User
                    {
                        Rut = "20.416.699-4",
                        Name = "Ignacio Mancilla",
                        Birthday = new DateTime(2000, 10, 25),
                        Email = "admin@idwm.cl",
                        GenderId = 1,
                        Password = BCrypt.Net.BCrypt.HashPassword("P4ssw0rd"),
                        IsActive = true,
                        RoleId = 1
                    };

                    generatedRuts.Add(administrador.Rut);
                    context.Users.Add(administrador);

                    context.SaveChanges();
                }
            }
        }
    }
}
