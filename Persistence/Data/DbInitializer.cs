using Domain.Interfaces;
using Domain.Models.IdentityModule;
using Domain.Models.Products;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence.Data
{
    public class DbInitializer(StoreDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,StoreIdentityDbContext identityContext) : IDbInitializer
    {
        public async Task IdentityInitializeAsync()
        {
            try
            {
                //if there is no roles
                if (!roleManager.Roles.Any())
                {
                    await roleManager.CreateAsync(new IdentityRole("Admin"));
                    await roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                }

                if (!userManager.Users.Any())
                {
                    var User1 = new ApplicationUser()
                    {
                        Email = "Reem@gmail.com",
                        DisplayName = "Reem",
                        PhoneNumber = "01155887741",
                        UserName = "Reem",
                    };
                    var User2 = new ApplicationUser()
                    {
                        Email = "Ali@gmail.com",
                        DisplayName = "Ali",
                        PhoneNumber = "01258897481",
                        UserName = "Ali",

                    };
                    await userManager.CreateAsync(User1, "P@ssw0rd");
                    await userManager.CreateAsync(User2, "P@ssw0rd");


                    await userManager.AddToRoleAsync(User1, "Admin");
                    await userManager.AddToRoleAsync(User1, "SuperAdmin");

                    await identityContext.SaveChangesAsync();

                }

                await identityContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task InitializeAsync()
        {
            if((await context.Database.GetPendingMigrationsAsync()).Any())
            {
                await context.Database.MigrateAsync();
            }
            try
            {
                if (!context.Set<ProductBrand>().Any())
                {
                    var data = await File.ReadAllTextAsync(@"..\Persistence\Data\Seeds\brands.json");
                    var objects = JsonSerializer.Deserialize<List<ProductBrand>>(data);

                    if (objects != null && objects.Any())
                    {
                        context.Set<ProductBrand>().AddRange(objects);
                        await context.SaveChangesAsync();

                    }

                }
                if (!context.Set<ProductType>().Any())
                {
                    var data = await File.ReadAllTextAsync(@"..\Persistence\Data\Seeds\types.json");
                    var objects = JsonSerializer.Deserialize<List<ProductType>>(data);

                    if (objects != null && objects.Any())
                    {
                        context.Set<ProductType>().AddRange(objects);
                                await context.SaveChangesAsync();

                    }

                }
                if (!context.Set<Product>().Any())
                {
                    var data = await File.ReadAllTextAsync(@"..\Persistence\Data\Seeds\products.json");
                    var objects = JsonSerializer.Deserialize<List<Product>>(data);

                    if (objects != null && objects.Any())
                    {
                        context.Set<Product>().AddRange(objects);
                        await context.SaveChangesAsync();

                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

