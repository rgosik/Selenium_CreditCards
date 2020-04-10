using System;
using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;


namespace CreditCard.UITests
{
    public class CreditCardWebAppShould
    {
        const string HomeUrl = "http://localhost:44108/";
        const string AboutUrl = "http://localhost:44108/Home/About";

        const string HomeTitle = "Home Page - Credit Cards";

        [Fact]
        [Trait("Category", "Smoke")]
        public void LoadApplicationPage()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomeUrl);

                Assert.Equal(HomeTitle, driver.Title);
                Assert.Equal(HomeUrl, driver.Url);
            }
        }

        [Fact]
        [Trait("Category", "Smoke")]
        public void ReloadHomePage()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomeUrl);
                driver.Navigate().Refresh();

                Assert.Equal(HomeTitle, driver.Title);
                Assert.Equal(HomeUrl, driver.Url);
            }

        }

        [Fact]
        [Trait("Category", "Smoke")]
        public void ReloadHomePageOnBack()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomeUrl);

                //IWebElement generationTokenElement = driver.FindElement(By.Id("GenerationToken"));
                string initialToken = driver.FindElement(By.Id("GenerationToken")).Text;

                driver.Navigate().GoToUrl(AboutUrl);
                driver.Navigate().Back();

                Assert.Equal(HomeTitle, driver.Title);
                Assert.Equal(HomeUrl, driver.Url);

                string reloadedToken = driver.FindElement(By.Id("GenerationToken")).Text;
                Assert.NotEqual(initialToken, reloadedToken);
            }
        }

        [Fact]
        [Trait("Category", "Smoke")]
        public void ReloadHomePageOnForward()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(AboutUrl);
                driver.Navigate().GoToUrl(HomeUrl);

                string initalToken = driver.FindElement(By.Id("GenerationToken")).Text;

                driver.Navigate().Back();
                driver.Navigate().Forward();

                Assert.Equal(HomeTitle, driver.Title);
                Assert.Equal(HomeUrl, driver.Url);

                string reloadedToken = driver.FindElement(By.Id("GenerationToken")).Text;
                Assert.NotEqual(initalToken, reloadedToken);
            }
        }

    }
}
