using ProductAPI.Repository;
using WebUIAutomationTestFramework.Helpers;

namespace ProductUIAutomationBDDTests.StepDefinitions
{
    [Binding]
    public sealed class ProductStepDefinitions
    {
        private readonly ScenarioContext scenarioContext;
        private readonly IShared shared;
        private readonly IProductPage productPage;
        private readonly IProductRepository productRepository;
        private readonly ILogger logger;

        public ProductStepDefinitions(ScenarioContext scenarioContext, IShared shared, IProductPage productPage, IProductRepository productRepository, ILogger logger)
        {
            this.scenarioContext = scenarioContext;
            this.shared = shared;
            this.productPage = productPage;
            this.productRepository = productRepository;
            this.logger = logger;
        }

        [Given(@"Click on Product menu")]
        public void GivenClickOnProductMenu()
        {
            shared.NavigateToProductPage();
        }

        [Given(@"Click on Create link")]
        public void GivenClickOnCreateLink()
        {
            productPage.NavigateToProductCreationPage();

            logger.Info("Navigated to Product creation page.");
        }

        [When(@"Create product with following details")]
        public void WhenCreateProductWithFollowingDetails(Table table)
        {
            // Automatically map all the Specflow Tables row data to the actual Product Type 
            var product = table.CreateInstance<Product>();

            productPage.EnterProductDetails(product);

            // Store the product details
            scenarioContext.Set(product);

            logger.Info("Product details are entered and Product is created.");
        }

        [When(@"Click on (.*) link of the newly created product")]
        public void WhenClickOnLinkOfTheNewlyCreatedProduct(string operation)
        {
            var product = scenarioContext.Get<Product>();
            productPage.PerformClickOnSpecialValue(product.Name, operation);
        }

        [Then(@"All the product details are created as expected")]
        public void ThenAllTheProductDetailsAreCreatedAsExpected()
        {
            var product = scenarioContext.Get<Product>();

            var actualProduct = productPage.GetProductDetails();

            // Assertion
            actualProduct
                .Should()
                .BeEquivalentTo(product, option => option.Excluding(x => x.Id));

            logger.Info("Test Scenario passed.");
        }

        [When(@"Edit the product with following details")]
        public void WhenEditTheProductWithFollowingDetails(Table table)
        {
            var product = table.CreateInstance<Product>();

            productPage.EditProduct(product);

            // Store the product details
            scenarioContext.Set(product);

            logger.Info("Product details are edited and saved.");
        }
    }
}
