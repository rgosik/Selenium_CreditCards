using System;
using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace CreditCard.UITests
{
    [Trait("Category", "Applications")]
    public class CreditCardApplicationShould
    {
        private const string HomeUrl = "http://localhost:44108/";
        private const string ApplyUrl = "http://localhost:44108/Apply";

        private const string ApplyTitle = "Credit Card Application - Credit Cards";

        [Fact]
        public void BeInitiatedFromHomePage_NewLowRate()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomeUrl);

                IWebElement applyLink = driver.FindElement(By.Name("ApplyLowRate"));
                applyLink.Click();

                Assert.Equal(ApplyTitle, driver.Title);
                Assert.Equal(ApplyUrl, driver.Url);
            }
        }

        [Fact]
        public void BeInitiatedFromHomePage_EasyApplication()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomeUrl);

                IWebElement carouselNext =
                    driver.FindElement(By.CssSelector("[data-slide='next']"));
                carouselNext.Click();

                IWebElement applyLink = driver.FindElement(By.LinkText("Easy: Apply Now!"));
                applyLink.Click();

                Assert.Equal(ApplyTitle, driver.Title);
                Assert.Equal(ApplyUrl, driver.Url);
            }
        }

        [Fact]
        public void BeInitiatedFromHomePage_CustomerService()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomeUrl);

                IWebElement carouselNext =
                    driver.FindElement(By.CssSelector("[data-slide='next']"));
                carouselNext.Click();
                carouselNext.Click();

                IWebElement applyLink = driver.FindElement(By.ClassName("customer-service-apply-now"));
                applyLink.Click();

                Assert.Equal(ApplyTitle, driver.Title);
                Assert.Equal(ApplyUrl, driver.Url);
            }
        }
    }
}
