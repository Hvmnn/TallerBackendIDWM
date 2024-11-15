using Microsoft.EntityFrameworkCore;
using tallerBackendIDWM.Src.Models;

namespace tallerBackendIDWM.Src.Data
{
    public class DataSeeder
    {
        private readonly DataContext _context;

        public DataSeeder(DataContext context)
        {
            _context = context;
        }
        public async Task Seed()
        {
            if (_context.Products.Any())
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

            if (!await _context.Users.AnyAsync(u => u.Email == "admin@idwm.cl"))
            {
                var administrador = new User
                {
                    Rut = "20.416.699-4",
                    Name = "Ignacio Mancilla",
                    Birthdate = new DateTime(2000, 10, 25),
                    Email = "admin@idwm.cl",
                    Gender = "Masculino",
                    Password = "P4ssw0rd",
                    Rol = "Administrador"
                };

                _context.Users.Add(administrador);
                await _context.SaveChangesAsync();

            }
        }
    }
}
