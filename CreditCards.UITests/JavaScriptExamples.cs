using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;

namespace CreditCards.UITests
{
    public class JavaScriptExamples
    {

        [Fact]
        public void ClickOverlayedLink()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl("http://localhost:44108/jsoverlay.html");

                string script = "document.getElementById('HiddenLink').click();";
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                js.ExecuteScript(script);

                //driver.FindElement(By.Id("HiddenLink")).Click();

                Assert.Equal("https://www.pluralsight.com/", driver.Url);
            }
        }

        [Fact]
        public void GetOverlayedLinkText()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl("http://localhost:44108/jsoverlay.html");

                string script = "return document.getElementById('HiddenLink').innerHTML;";

                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                string linkText = (string)js.ExecuteScript(script);

                Assert.Equal("Go to Pluralsight", linkText);
            }
        }
    }
}