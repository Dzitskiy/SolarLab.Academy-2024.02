using Bogus;
using SolarLab.Academy.Domain.Categories.Entity;

namespace SolarLab.Academy.ApiTests.Categories;

public static class CategoriesHelper
{
    private static Faker<Category> _categoryFaker = new Faker<Category>()
        .StrictMode(false)
        .RuleFor(c => c.Id, _ => Guid.NewGuid())
        .RuleFor(c => c.Name, (f, c) => f.Commerce.Categories(1)[0]);
    
    public static IEnumerable<Category> GetRandomCategories(int number)
    {
        return _categoryFaker.Generate(number);
    }

    public static IEnumerable<Category> GetRandomCategoriesWithFaker(int number)
    {
        for (int i = 0; i < number; i++)
        {
            yield return new Category()
            {
                Id = Guid.NewGuid(),
                Name = Faker.Company.Name(),
            };
        }
    }
}