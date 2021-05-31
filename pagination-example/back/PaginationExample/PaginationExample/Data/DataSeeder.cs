using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PaginationExample.Models;

namespace PaginationExample.Data
{
    public class DataSeeder
    {
        public static void InitDb(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var context = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();

            var vendor1 = new Vendor() { Name = "Сыровая Реальность" };
            var vendor2 = new Vendor() { Name = "ООО Т-Мороженое" };

            context.Vendors.AddRange(vendor1, vendor2);

            context.Products.AddRange(
                new Product
                {
                    Name = "Сыр Российский",
                    Cost = 150,
                    ExpirationDate = DateTime.Today,
                    Vendor = vendor1,
                },
                new Product
                {
                    Name = "Сыр Бри",
                    Cost = 250,
                    ExpirationDate = DateTime.Today.AddDays(5),
                    Vendor = vendor1,
                },
                new Product
                {
                    Name = "Мороженое Фруктовый Лёд",
                    Cost = 50,
                    ExpirationDate = DateTime.Today.AddDays(10),
                    Vendor = vendor2,
                }
            );

            context.SaveChanges();
        }
    }
}
