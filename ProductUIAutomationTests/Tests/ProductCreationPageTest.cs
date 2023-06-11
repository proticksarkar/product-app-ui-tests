using AutoFixture.Xunit2;
using FluentAssertions;
using ProductUIAutomationTests.Model;
using ProductUIAutomationTests.Pages;
using WebUIAutomationTestFramework.Helpers;
using Xunit;

namespace ProductUIAutomationTests.Tests;
public class ProductCreationPageTest
{
    private readonly IShared _shared;
    private readonly IProductPage _productPage;
    private readonly ILogger _logger;

    public ProductCreationPageTest(IShared shared, IProductPage createProductPage, ILogger logger)
    {
        _shared = shared;
        _productPage = createProductPage;
        _logger = logger;
    }

    [Theory, AutoData]
    public void ProductCreationTest(Product product)
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

        _logger.Info($"ProductCreationTest passed.");
    }
}
