using Bogus;
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
                Console.WriteLine("Seeding data...");
                
                if(!context.Roles.Any()){
                    Console.WriteLine("Seeding roles...");
                    context.Roles.AddRange(
                        new Role {Type = "Admin"},
                        new Role { Type = "Usuario"}
                    );
                }

                if(!context.Genders.Any()){
                    Console.WriteLine("Seeding genders...");
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
                    Console.WriteLine("Seeding users...");
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

                    var faker = new Faker<User>()
                    .RuleFor(u => u.Rut, f => GenerateUniqueRandomRut(generatedRuts))
                    .RuleFor(u => u.Name, f => f.Person.FullName)
                    .RuleFor(u => u.Birthday, f => f.Date.Past(30, DateTime.Now.AddYears(-18)))
                    .RuleFor(u => u.Email, f => f.Internet.Email())
                    .RuleFor(u => u.Password, f => BCrypt.Net.BCrypt.HashPassword("password"))
                    .RuleFor(u => u.IsActive, f => f.Random.Bool())
                    .RuleFor(u => u.RoleId, f => 2)
                    .RuleFor(u => u.GenderId, f => f.Random.Number(1, 4));

                    var users = faker.Generate(20);

                    context.Users.AddRange(users);
                    context.Products.AddRange(products);

                    context.SaveChanges();
                    Console.WriteLine("Data seeded successfully!");
                }
            }
        }
        private static string GenerateUniqueRandomRut(HashSet<string> existingRuts)
        {
            string rut;
            do
            {
                rut = GenerateRandomRut();
            } while (existingRuts.Contains(rut));

            existingRuts.Add(rut);
            return rut;
        }

        private static string GenerateRandomRut()
        {
            Random random = new();
            int rutNumber = random.Next(1, 99999999);
            int verificator = CalculateRutVerification(rutNumber);
            string verificatorStr = verificator.ToString();
            if(verificator == 10){
                verificatorStr = "k";
            }

            return $"{rutNumber}-{verificatorStr}";
        }

        // Método para calcular el dígito verificador de un RUT
        private static int CalculateRutVerification(int rutNumber)
        {
            int[] coefficients = { 2, 3, 4, 5, 6, 7 };
            int sum = 0;
            int index = 0;

            while (rutNumber != 0)
            {
                sum += rutNumber % 10 * coefficients[index];
                rutNumber /= 10;
                index = (index + 1) % 6;
            }

            int verification = 11 - (sum % 11);
            return verification == 11 ? 0 : verification;
        }
    }
}
