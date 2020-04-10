using System;
using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;


namespace CreditCard.UITests
{
    public class CreditCardWebAppShould
    {

        [Fact]
        [Trait("Category", "Smoke")]
        public void LoadApplicationPage()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                const string homeUrl = "http://localhost:44108/";

                driver.Navigate().GoToUrl(homeUrl);

                Assert.Equal("Home Page - Credit Cards", driver.Title);
                Assert.Equal(homeUrl, driver.Url);
            }
        }
    }
}
