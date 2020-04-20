using System;
using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Xunit.Abstractions;
using CreditCards.UITests.PageObjectModels;

namespace CreditCards.UITests
{
    [Trait("Category", "Applications")]
    public class CreditCardApplicationShould : IClassFixture<ChromeDriverFixture>
    {
        //private readonly ChromeDriverFixture ChromeDriverFixture;

        private readonly ITestOutputHelper output;

        public CreditCardApplicationShould(ITestOutputHelper output)
        {
            this.output = output;
        }

      /*public CreditCardApplicationShould(ChromeDriverFixture chromeDriverFixture)
        {
            ChromeDriverFixture = chromeDriverFixture;
            ChromeDriverFixture.Driver.Manage().Cookies.DeleteAllCookies();
            ChromeDriverFixture.Driver.Navigate().GoToUrl("about:blank");
        }*/
        /// <summary>
        /// instead of suing() 
        /// {
        /// }         
        /// in each test, just put: 
        /// var homePage = new HomePage(ChromeDriverFixture.Driver);
        /// </summary>


        [Fact]
        public void BeInitiatedFromHomePage_NewLowRate()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                var homePage = new HomePage(driver);
                homePage.NavigateTo();

                ApplicationPage applicationPage = homePage.ClickApplyLowRateLink();

                applicationPage.EnsurePageLoaded();
            }
        }

        [Fact]
        public void BeInitiatedFromHomePage_EasyApplication()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                var homePage = new HomePage(driver);
                homePage.NavigateTo();

                homePage.WaitForEasyApplicationCarosuelPage();

                ApplicationPage applicationPage = homePage.ClickApplyEasyApplicationLink();

                applicationPage.EnsurePageLoaded();
            }
        }

        [Fact]
        public void BeInitiatedFromHomePage_CustomerService()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                var homePage = new HomePage(driver);
                homePage.NavigateTo();

                homePage.WaitForCustomerServiceCarouselPage();

                ApplicationPage applicationPage = homePage.ClickCustomerServiceApplicationLink();

                applicationPage.EnsurePageLoaded();
            }
        }

        [Fact]
        public void BeInitiatedFromHomePage_RandomGreeting()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                var homePage = new HomePage(driver);
                homePage.NavigateTo();

                ApplicationPage applicationPage = homePage.ClickRandomGreetingApplyLink();

                applicationPage.EnsurePageLoaded();
            }
        }

        [Fact]
        public void BeInitiatedFromHomePage_RandomGreeting_XPATH()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                var homePage = new HomePage(driver);
                homePage.NavigateTo();

                ApplicationPage applicationPage = homePage.ClickRandomGreetingApplyLinkXPath();

                applicationPage.EnsurePageLoaded();
            }
        }

        [Fact]
        public void BeSubmittedWhenValid()
        {
            const string FirstName = "Sarah";
            const string LastName = "Smith";
            const string Number = "123456-A";
            const string Age = "18";
            const string Income = "50000";

            using (IWebDriver driver = new ChromeDriver())
            {
                var applicationPage = new ApplicationPage(driver);
                applicationPage.NavigateTo();

                applicationPage.EnterFirstName(FirstName);
                applicationPage.EnterLastName(LastName);
                applicationPage.EnterFrequentFlyerNumber(Number);
                applicationPage.EnterAge(Age);
                applicationPage.EnterGrossAnnualIncome(Income);
                applicationPage.ChooseMaritalStatusSingle();
                applicationPage.ChooseBusinessSourceTV();
                applicationPage.AcceptTerms();
                ApplicationCompletePage applicationCompletePage =
                    applicationPage.SubmitApplication();

                applicationCompletePage.EnsurePageLoaded();

                Assert.Equal("ReferredToHuman", applicationCompletePage.Decision);
                Assert.NotEmpty(applicationCompletePage.ReferenceNumber);
                Assert.Equal($"{FirstName} {LastName}", applicationCompletePage.FullName);
                Assert.Equal(Age, applicationCompletePage.Age);
                Assert.Equal(Income, applicationCompletePage.Income);
                Assert.Equal("Single", applicationCompletePage.RelationshipStatus);
                Assert.Equal("TV", applicationCompletePage.BusinessSource);
            }
        }

        [Fact]
        public void BeSubmittedWhenValidationErrorsCorrected()
        {
            const string firstName = "Sarah";
            const string invalidAge = "17";
            const string validAge = "18";

            using (IWebDriver driver = new ChromeDriver())
            {
                var applicationPage = new ApplicationPage(driver);
                applicationPage.NavigateTo();

                applicationPage.EnterFirstName(firstName);
                // Don't enter lastname
                applicationPage.EnterFrequentFlyerNumber("123456-A");
                applicationPage.EnterAge(invalidAge);
                applicationPage.EnterGrossAnnualIncome("50000");
                applicationPage.ChooseMaritalStatusSingle();
                applicationPage.ChooseBusinessSourceTV();
                applicationPage.AcceptTerms();
                applicationPage.SubmitApplication();

                // Assert that validation failed                                
                Assert.Equal(2, applicationPage.ValidationErrorMessages.Count);
                Assert.Contains("Please provide a last name", applicationPage.ValidationErrorMessages);
                Assert.Contains("You must be at least 18 years old", applicationPage.ValidationErrorMessages);

                // Fix errors
                applicationPage.EnterLastName("Smith");
                applicationPage.ClearAge();
                applicationPage.EnterAge(validAge);

                // Resubmit form
                ApplicationCompletePage applicationCompletePage = applicationPage.SubmitApplication();

                // Check form submitted
                applicationCompletePage.EnsurePageLoaded();
            }
        }
    }
}