using WebUIAutomationTestFramework.Helpers;

namespace ProductUIAutomationBDDTests.Pages;

public interface IProductPage
{
    void DeleteProduct(Product product);
    void EditProduct(Product product);
    void EnterProductDetails(Product product);
    Product GetProductDetails();
    void NavigateToProductCreationPage();
    void PerformClickOnSpecialValue(string name, string operation);
}

public class ProductPage : IProductPage
{
    private readonly IWebDriver _driver;

    public ProductPage(IDriverFixture driverFixture)
    {
        _driver = driverFixture.Driver;
    }

    // Product Page
    private IWebElement tblList => _driver.FindWebElement(By.CssSelector(".table"));
    private IWebElement lnkCreate => _driver.FindWebElement(By.LinkText("Create"));

    // Product Create/Edit/Details/Delete Page
    private IWebElement txtName => _driver.FindWebElement(By.Id("Name"));
    private IWebElement txtDescription => _driver.FindWebElement(By.Id("Description"));
    private IWebElement txtPrice => _driver.FindWebElement(By.Id("Price"));
    private IWebElement ddlProductType => _driver.FindWebElement(By.Id("ProductType"));
    private IWebElement btnCreate => _driver.FindWebElement(By.Id("Create"));
    private IWebElement btnSave => _driver.FindWebElement(By.Id("Save"));
    private IWebElement btnDelete => _driver.FindWebElement(By.Id("Delete"));

    public void NavigateToProductCreationPage()
    {
        lnkCreate.Click();
    }

    public void EnterProductDetails(Product product)
    {
        txtName.ClearAndEnterText(product.Name);
        txtDescription.ClearAndEnterText(product.Description);
        txtPrice.ClearAndEnterText(product.Price.ToString());
        ddlProductType.SelectDropDownByText(product.ProductType.ToString());
        btnCreate.Click();
    }

    public void EditProduct(Product product)
    {
        txtDescription.ClearAndEnterText(product.Description);
        txtPrice.ClearAndEnterText(product.Price.ToString());
        btnSave.Click();
    }

    public Product GetProductDetails()
    {
        return new Product()
        {
            Name = txtName.Text,
            Description = txtDescription.Text,
            Price = int.Parse(txtPrice.Text),
            ProductType = (ProductType)Enum.Parse(typeof(ProductType),
                          ddlProductType.GetAttribute("innerText").ToString())
        };
    }

    public void DeleteProduct(Product product)
    {
        btnDelete.Click();
    }

    public void PerformClickOnSpecialValue(string name, string operation)
    {
        tblList.PerformActionOnCell("5", "Name", name, operation);
    }
}
