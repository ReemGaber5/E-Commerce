using Domain.Interfaces;
using Domain.Models.Products;
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
    public class DbInitializer : IDbInitializer
    {
        private readonly StoreDbContext _dbContext;
        public DbInitializer(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task InitializeAsync()
        {
            if((await _dbContext.Database.GetPendingMigrationsAsync()).Any())
            {
                await _dbContext.Database.MigrateAsync();
            }
            try
            {
                if (!_dbContext.Set<ProductBrand>().Any())
                {
                    var data = await File.ReadAllTextAsync(@"..\Persistence\Data\Seeds\brands.json");
                    var objects = JsonSerializer.Deserialize<List<ProductBrand>>(data);

                    if (objects != null && objects.Any())
                    {
                        _dbContext.Set<ProductBrand>().AddRange(objects);
                        await _dbContext.SaveChangesAsync();

                    }

                }
                if (!_dbContext.Set<ProductType>().Any())
                {
                    var data = await File.ReadAllTextAsync(@"..\Persistence\Data\Seeds\types.json");
                    var objects = JsonSerializer.Deserialize<List<ProductType>>(data);

                    if (objects != null && objects.Any())
                    {
                        _dbContext.Set<ProductType>().AddRange(objects);
                        await _dbContext.SaveChangesAsync();

                    }

                }
                if (!_dbContext.Set<Product>().Any())
                {
                    var data = await File.ReadAllTextAsync(@"..\Persistence\Data\Seeds\products.json");
                    var objects = JsonSerializer.Deserialize<List<Product>>(data);

                    if (objects != null && objects.Any())
                    {
                        _dbContext.Set<Product>().AddRange(objects);
                        await _dbContext.SaveChangesAsync();

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

