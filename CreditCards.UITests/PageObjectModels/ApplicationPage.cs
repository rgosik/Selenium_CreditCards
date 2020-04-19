using OpenQA.Selenium;
using System;

namespace CreditCards.UITests.PageObjectModels
{
    class ApplicationPage : BasePage
    {
        public override string PageUrl => "http://localhost:44108/Apply";
        public override string PageTitle => "Credit Card Application - Credit Cards";

        public ApplicationPage(IWebDriver driver) 
            : base(driver)
        {
        }

    }
}