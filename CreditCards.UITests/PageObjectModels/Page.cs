using OpenQA.Selenium;
using System;

namespace CreditCards.UITests.PageObjectModels
{
    abstract class Page
    {
        protected abstract string PageUrl { get; }
        protected abstract string PageTitle { get; }
        protected readonly IWebDriver Driver;

        public Page(IWebDriver driver)
        {
            Driver = driver;
        }

        /// <summary>
        /// Checks that the URL and page title are as expected
        /// </summary>
        /// <param name="onlyCheckUrlStartsWithExpectedText">Set to false to do an exact match of URL.
        /// Set to true to ignore fragments, query string, etc at end of browser URL</param>

        public void EnsurePageLoaded(bool onlyCheckUrlStartsWithExpectedText = true)
        {
            bool urlIsCorrect;
            if (onlyCheckUrlStartsWithExpectedText)
            {
                urlIsCorrect = Driver.Url.StartsWith(PageUrl);
            }
            else
            {
                urlIsCorrect = Driver.Url == PageUrl;
            }

            bool pageHasLoaded = urlIsCorrect && (Driver.Title == PageTitle);
            if (!pageHasLoaded)
            {
                throw new Exception($"Failed to load page. Page URL = '{Driver.Url}' Page Source: \r\n {Driver.PageSource}");
            }

        }

        public void NavigateTo()
        {
            Driver.Navigate().GoToUrl(PageUrl);
            EnsurePageLoaded();
        }
    }
}
