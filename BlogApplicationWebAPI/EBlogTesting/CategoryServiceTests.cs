using NUnit.Framework;
using BlogApplicationWebAPI.Entitys;
using BlogApplicationWebAPI.Services;
using BlogApplicationWebAPI.Database;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;

namespace EBlogTesting
{
    [TestFixture]
    public class CategoryServiceTests
    {
        private BlogContext context;
        private ICategoryService categoryService;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<BlogContext>()
                .UseInMemoryDatabase("InnMemoryCategoryDatabase")
                .Options;

            context = new BlogContext((Microsoft.Extensions.Configuration.IConfiguration)options);
            categoryService = new CategoryService(context);
        }

        [Test]
        public void AddCategory_ShouldAddCategoryToDatabase()
        {
            // Arrange
            var category = new Category
            {
                CategoryName = "TestCategory",
                Description = "TestDescription"
            };

            // Act
            categoryService.AddCategory(category);

            // Assert
            object value = Assert.AreEqual(1, context.Categories.Count());
            Assert.AreEqual("TestCategory", context.Categories.First().CategoryName);
        }

        [Test]
        public void GetCategories_ShouldReturnAllCategories()
        {
            // Arrange
            context.Categories.Add(new Category { CategoryName = "Category1", Description = "Description1" });
            context.Categories.Add(new Category { CategoryName = "Category2", Description = "Description2" });
            context.SaveChanges();

            // Act
            var result = categoryService.GetCategories();

            // Assert
            Assert.AreEqual(2, result.Count);
        }

        [Test]
        public void GetCategoryById_ShouldReturnCorrectCategory()
        {
            // Arrange
            var category1 = new Category { CategoryName = "Category1", Description = "Description1" };
            var category2 = new Category { CategoryName = "Category2", Description = "Description2" };
            context.Categories.Add(category1);
            context.Categories.Add(category2);
            context.SaveChanges();

            // Act
            var result = categoryService.GetCategoryById(category2.CategoryId);

            // Assert
            Assert.AreEqual("Category2", result.CategoryName);
        }

        [Test]
        public void UpdateCategory_ShouldUpdateCategoryInDatabase()
        {
            // Arrange
            var category = new Category { CategoryName = "OriginalName", Description = "OriginalDescription" };
            context.Categories.Add(category);
            context.SaveChanges();

            // Act
            category.CategoryName = "UpdatedName";
            categoryService.UpdateCategory(category);

            // Assert
            var updatedCategory = context.Categories.Find(category.CategoryId);
            Assert.AreEqual("UpdatedName", updatedCategory.CategoryName);
        }

        [Test]
        public void DeleteCategory_ShouldRemoveCategoryFromDatabase()
        {
            // Arrange
            var category = new Category { CategoryName = "ToDelete", Description = "DeleteMe" };
            context.Categories.Add(category);
            context.SaveChanges();

            // Act
            categoryService.DeleteCategory(category.CategoryId);

            // Assert
            Assert.AreEqual(0, context.Categories.Count());
        }
    }
}

