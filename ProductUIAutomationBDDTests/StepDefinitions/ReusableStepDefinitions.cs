using ProductAPI.Repository;
using WebUIAutomationTestFramework.Helpers;

namespace ProductUIAutomationBDDTests.StepDefinitions
{
    [Binding]
    public sealed class ReusableStepDefinitions
    {
        private readonly ScenarioContext scenarioContext;
        private readonly IProductRepository productRepository;
        private readonly ILogger logger;

        public ReusableStepDefinitions(ScenarioContext scenarioContext, IProductRepository productRepository, ILogger logger)
        {
            this.scenarioContext = scenarioContext;
            this.productRepository = productRepository;
            this.logger = logger;
        }

        [Given(@"Cleanup following data")]
        public void GivenCleanupFollowingData(Table table)
        {
            var products = table.CreateSet<Product>();

            foreach (var product in products)
            {
                var prod = productRepository.GetProductByName(product.Name);

                if (prod != null)
                    productRepository.DeleteProduct(product.Name);
            }

            logger.Info("Cleanup is done.");
        }

        [Given(@"Ensure the following product is created")]
        public void GivenEnsureTheFollowingProductIsCreated(Table table)
        {
            var product = table.CreateInstance<Product>();

            productRepository.AddProduct(product);

            // Store the product details
            scenarioContext.Set(product);

            logger.Info("Product is created and present.");
        }

        [Then(@"Delete the product (.*) for cleanup")]
        public void ThenDeleteTheProductForCleanup(string productName)
        {
            productRepository.DeleteProduct(productName);

            logger.Info("Product is deleted as a part of cleanup.");
        }
    }
}
