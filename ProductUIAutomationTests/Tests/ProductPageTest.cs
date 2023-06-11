using FluentAssertions;
using ProductAPI.Repository;
using ProductUIAutomationTests.Model;
using ProductUIAutomationTests.Pages;
using ProductUIAutomationTests.TestData;
using WebUIAutomationTestFramework.Helpers;
using Xunit;
using Xunit.Priority;

namespace ProductUIAutomationTests.Tests;

[TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
public class ProductPageTest
{
    private readonly IShared _shared;
    private readonly IProductPage _productPage;
    private readonly IProductRepository _productRepository;
    private readonly ILogger _logger;

    public ProductPageTest(IShared shared, IProductPage productPage, IProductRepository productRepository, ILogger logger)
    {
        _shared = shared;
        _productPage = productPage;
        _productRepository = productRepository;
        _logger = logger;
    }

    [Theory, Priority(1)]
    [MemberData(nameof(ProductDataGenerator.NewProduct), MemberType = typeof(ProductDataGenerator))]
    public void ValidateSuccessfulProductCreationTest(Product product)
    {
        _shared.NavigateToProductPage();

        _productPage.NavigateToProductCreationPage();

        _productPage.EnterProductDetails(product);

        _productPage.PerformClickOnSpecialValue(product.Name, "Details");

        var actualProduct = _productPage.GetProductDetails();

        // Assertion
        actualProduct
            .Should()
            .BeEquivalentTo(product, option => option.Excluding(x => x.Id));

        _logger.Info($"ValidateSuccessfulProductCreationTest passed.");
    }

    [Theory, Priority(2)]
    [MemberData(nameof(ProductDataGenerator.UpdateProduct), MemberType = typeof(ProductDataGenerator))]
    public void ValidateSuccessfulProductUpdationTest(Product product)
    {
        _shared.NavigateToProductPage();

        _productPage.PerformClickOnSpecialValue(product.Name, "Edit");

        _productPage.EditProduct(product);

        _productPage.PerformClickOnSpecialValue(product.Name, "Details");

        var actualProduct = _productPage.GetProductDetails();

        // Assertion
        actualProduct
            .Should()
            .BeEquivalentTo(product, option => option.Excluding(x => x.Id));

        _logger.Info($"ValidateSuccessfulProductUpdationTest passed.");
    }

    [Theory, Priority(3)]
    [MemberData(nameof(ProductDataGenerator.NewProduct), MemberType = typeof(ProductDataGenerator))]
    public void ValidateSuccessfulProductDeletionTest(Product product)
    {
        _shared.NavigateToProductPage();

        _productPage.PerformClickOnSpecialValue(product.Name, "Delete");

        _productPage.DeleteProduct(product);

        var actualProduct = _productRepository.GetProductByName(product.Name);

        // Assertion
        actualProduct.Should().BeNull();

        _logger.Info($"ValidateSuccessfulProductDeletionTest passed.");
    }
}
