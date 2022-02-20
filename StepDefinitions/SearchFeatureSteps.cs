using Bunnings.Utils;
using Bunnings.Pages;
using System;
using TechTalk.SpecFlow;
using NUnit.Framework;
using AventStack.ExtentReports.Gherkin.Model;


namespace Bunnings.StepDefinitions
{
    [Binding]
    public class SearchFeatureSteps
    {
        private DriverHelper _driverHelper;
        BunningsHomePagePOM bunningsHomePagePOM;
        string _browser;


        public SearchFeatureSteps(DriverHelper driverHelper, ScenarioContext scenarioContext)
        {
            _driverHelper = driverHelper;
            bunningsHomePagePOM = new BunningsHomePagePOM(_driverHelper.Driver);
        }

        [Given(@"Customer launches Bunnings website")]
        public void GivenCustomerLaunchesBunningsWebsiteUsing()
        {
            bunningsHomePagePOM.LaunchBunningsWebsite();
        }

        [Given(@"customer enters the search term ""(.*)"" on the bunnings website search field and click on the search button")]
        public void GivenCustomerEntersTheSearchTermOnTheBunningsWebsiteSearchFieldAndClickOnTheSearchButton(string searchTerm)
        {
            //bunningsHomePagePOM.SearchForProduct(searchTerm);
            bunningsHomePagePOM.EnterSearchterm(searchTerm);
        }

        [Then(@"Customer is displayed Products that matches the search term ""(.*)""")]
        public void ThenCustomerIsDisplayedProductsThatMatchesTheSearchTerm(string searchTerm)
        {
            bunningsHomePagePOM.ValidateSearchResults(searchTerm);
        }

        

        [Given(@"customer enters the search term ""(.*)"" on the bunnings website search field")]
        public void GivenCustomerEntersTheSearchTermOnTheBunningsWebsiteSearchField(string searchTerm)
        {
            bunningsHomePagePOM.EnterSearchterm(searchTerm);
        }

        [Then(@"Customer is displayed a list of product sugesstions for the search term ""(.*)""")]
        public void ThenCustomerIsDisplayedAListOfProductSugesstionsForTheSearchTerm(string searchTerm)
        {
            bunningsHomePagePOM.ValidateProductsSuggestedForCustomer(searchTerm);
        }

        [Then(@"Customer can find out more information regarding Flybuys points by clicking on the Find out Banner")]
        public void ThenCustomerCanFindOutMoreInformationRegardingFlybuysPointsByClickingOnTheFindOutBanner()
        {
            bunningsHomePagePOM.validateFlybuysPSRArticle();
        }



    }
}
