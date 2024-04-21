using Bogus;
using ECommerce.Core.Models;
namespace ECommerce.Infrastructure.Seeder
{
    internal static class ProductSeeder
    {
        public static IEnumerable<Product> Seed(int countOfProdutsToGenerate)
        {
            if (countOfProdutsToGenerate <= 0)
            {
                throw new ArgumentException("The parameter 'countOfProdutsToGenerate' needs to be greater than 0. Provide a correct argument for the seeding process.");
            }

            var productFaker = new Faker<Product>()
                .RuleFor(ent => ent.Id, f => f.Random.Uuid())
                .RuleFor(ent => ent.Name, f => f.Commerce.ProductName())
                .RuleFor(ent => ent.Description, f => f.Commerce.ProductDescription())
                .RuleFor(ent => ent.Price, f => Decimal.Parse(f.Commerce.Price(1, 90000000)))
                .RuleFor(ent => ent.Quantity, f => f.Random.Number(1, 1000));

            var randomDate = new Bogus.DataSets.Date().Recent(10);

            productFaker.RuleFor(ent => ent.CreatedDate, randomDate)
                .RuleFor(ent => ent.ModifiedDate, randomDate);

            var products = productFaker.Generate(countOfProdutsToGenerate);

            return products;
        }
    }
}