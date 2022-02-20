using Bunnings.Utils;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using AventStack.ExtentReports;
using System.Collections.ObjectModel;
using OpenQA.Selenium.Support.UI;

namespace Bunnings.Pages
{
    class BunningsHomePagePOM
    {
        //Declare class objects whose Elements and functions we require in the BunningsHomePage
        BunningsCustomControl CustomControl;
        HelpAndSupportPagePOM helpAndSupportPagePOM;
        WebDriverWait wait;
        int timeoutInSeconds = 5;

        //Declare an instance variable of type IWebDriver to work on the elements present on the bunnings Home Page
        private IWebDriver _Driver;

        //Bunnings home page constructor , Driver is received from the hooks class at runtime
        public BunningsHomePagePOM(IWebDriver Driver)
        {
            this._Driver = Driver;
            CustomControl = new BunningsCustomControl(_Driver);
            helpAndSupportPagePOM = new HelpAndSupportPagePOM(_Driver);
            wait = new WebDriverWait(_Driver, TimeSpan.FromSeconds(timeoutInSeconds));
        }

        

        //Required webElements on Bunnings home Page

        //Bunnings Title
        IWebElement titleHomeBunnings => CustomControl.BunningsFindElement(By.CssSelector("a[title='Home']"));

        //Search Field
        IWebElement searchField => CustomControl.BunningsFindElement(By.Id("custom-css-outlined-input")); //#custom-css-outlined-input
        
        
        //Search button
        IWebElement btnSearch => CustomControl.BunningsFindElement(By.CssSelector("button[title='Search']"));

        //Popular recent search sugesstions container
        IWebElement containerRecentSearchSuggestions => CustomControl.BunningsFindElement(By.XPath("//div[contains(@class, 'PopularRecentSuggestionsstyle__PopularRecentSuggestionsContainer-sc-')]"));

        //Search result , number of matching products found
        //.MuiTypography-root.resultCountLabal.MuiTypography-body2
        IWebElement totalResults => CustomControl.BunningsFindElement(By.CssSelector(".totalResults"));

        //Search Term
        //.searchTermContainer
        IWebElement searchTerm => CustomControl.BunningsFindElement(By.CssSelector(".searchTermContainer"));

        //First Item on the search result
        IWebElement firstProductInTheSearchresult => CustomControl.BunningsFindElement(By.XPath("(//article[contains(@class,'')])[1]"));

        //Suggested for you label
        //IWebElement suggestedForyouLabel => CustomControl.BunningsFindElement(By.XPath("//p[contains(@class,'PopularSearchResultsstyle__TextContainer')]"));
        ////div[contains(@class,'MuiGrid-root rightSection MuiGrid-item')]/p
        IWebElement suggestedForyouLabel => CustomControl.BunningsFindElement(By.XPath("//div[contains(@class,'MuiGrid-root rightSection MuiGrid-item')]/p"));

        //Suggested for you container
        IWebElement suggestedForYouContainer => CustomControl.BunningsFindElement(By.XPath("//div[contains(@class,'PopularSearchResultsstyle__PopularSearchCardsContainer')]"));

        //list of Hyper links of suggested for you products
        IWebElement listOfSuggestedForyouItems => CustomControl.BunningsFindElement(By.XPath("//div[contains(@class,'PopularSearchResultsstyle__PopularSearchCardsContainer')]/a"));


        //Product page
        //Product title
        IWebElement productTitle => CustomControl.BunningsFindElement(By.CssSelector("h1[data-locator='product-title']"));

        //Article banner caption
        IWebElement articleCaption => CustomControl.BunningsFindElement(By.XPath("//span[contains(@class,'MuiTypography-root ArticleCardstyle__StyledCaption-sc')]"));

        //Article banner title
        IWebElement articleTitle => CustomControl.BunningsFindElement(By.XPath("//p[@data-locator='psr-article-banner-title']"));

        //Page-Help-Support flybuys





        //Method to launch bunnings home page
        public void LaunchBunningsWebsite()
        {
            //Navigate to bunnings website
            _Driver.Navigate().GoToUrl(Config.BaseUrl);


            //Validate bunnings website is displayed to user
            Assert.IsTrue(titleHomeBunnings.Displayed, "Bunnings home page not displayed");           
        }


        //Method to enter search term
        public void EnterSearchterm(string searchvalue)
        {

            //Validate if search Filed is displayed on the webpage with default place holder text "What can we help you find today? "
            Assert.IsTrue(searchField.Displayed, "Search Fieled not displayed");
            Assert.AreEqual(Properties.Resources.SearchDefaultText, (searchField.GetAttribute("placeholder")), "Messages do not match");


            //Step to enter search text in the search field
            CustomControl.EnterText(searchField, searchvalue);

            //validate the popular search container is displayed
            Assert.IsTrue(containerRecentSearchSuggestions.Displayed, "Popular search options not displayed to user");

            ValidateProductsSuggestedForCustomer(searchvalue);            
        }


        //Method to search for a product
        public void SearchForProduct(string searchvalue)
        {
            //Enter search Term
            EnterSearchterm(searchvalue);

            // Click on the search button
            CustomControl.Click(btnSearch);            

            //Wait till search result is displayed            
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector(".totalResults")));
            

            //Validate search result
            ValidateSearchResults(searchvalue);
            
        }

        //Function to validate search return relavent products
        public void ValidateSearchResults(string searchvalue)
        {
            // Click on the search button
            CustomControl.Click(btnSearch);

            //Wait till search result is displayed            
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector(".totalResults")));


            //Validate search results are displayed to user
            Assert.IsTrue(totalResults.Displayed, "Search Result page not displayed");

            //Assert.That(xpaths.Count, Is.EqualTo(Int32.Parse(Properties.Resources.DefaultCountOFSuggestedForYouItems)).After(1).Seconds, "FAIL: Expecteed Count is " + Properties.Resources.DefaultCountOFSuggestedForYouItems + " Actual value displayed is " + xpaths.Count);
                       

            //Validate search term is displayed in the Search Term Container
            Assert.IsTrue(searchTerm.Displayed, "Search term not displayed");

            string searchTermDisplayed = searchTerm.FindElement(By.CssSelector("h2[class='MuiTypography-root MuiTypography-h2'] span")).Text;

            //Validate the search term displayed in the search result matches the search value
            Assert.AreEqual(searchvalue.ToLower(), searchTermDisplayed.ToLower(), "FAIL: Expected Search Term : " + searchvalue + " Actual displayed value: " + searchTermDisplayed);

            //first product title            
            string title = firstProductInTheSearchresult.FindElement(By.TagName("a")).GetAttribute("href");

            //Validate the title of the first product displayed in the search result contains the search term
            string sample = firstProductInTheSearchresult.FindElement(By.TagName("p")).Text;            

            char[] delimiterChars = { ' ', ',', '.', ':', '\t' };
            string[] words = searchvalue.Split(delimiterChars);
            bool containsString = false;

            foreach (var word in words)
            {
                containsString = sample.ToLower().Contains(word.ToLower());
            }

            Assert.IsTrue(containsString == true, "The product does not contain entered search key");
        }

        //Function to validate customer is suggested products that match the search key
        public void ValidateProductsSuggestedForCustomer(string searchvalue)
        {
            //Enter search Term
            //EnterSearchterm(searchvalue);

            //Validate suggested for you header is displayed
            Assert.IsTrue(suggestedForyouLabel.Displayed, Properties.Resources.LabelSuggestedForYou + " not displayed");
            

            //Validate products suggested for customer
            Assert.IsTrue(suggestedForYouContainer.Displayed, "Customer is not suggested any products during the search");


            //Validate customer is suggested 4 products 
            ReadOnlyCollection<IWebElement> xpaths = _Driver.FindElements(By.XPath("//div[contains(@class,'PopularSearchResultsstyle__PopularSearchCardsContainer')]/a"));
            
            
            Assert.That(xpaths.Count, Is.EqualTo(Int32.Parse(Properties.Resources.DefaultCountOFSuggestedForYouItems)).After(1).Seconds, "FAIL: Expecteed Count is " + Properties.Resources.DefaultCountOFSuggestedForYouItems + " Actual value displayed is " + xpaths.Count);

           
            char[] delimiterChars = { ' ', ',', '.', ':', '\t' };
            string[] words = searchvalue.Split(delimiterChars);
            bool containsString = false;                       

            //Validate suggested products contain searched term
            foreach (IWebElement element in xpaths)
            {
                string textProductTitle = element.FindElement(By.TagName("p")).Text;

                foreach (var word in words)
                {
                    containsString = textProductTitle.ToLower().Contains(word.ToLower());
                }
                Assert.IsTrue(containsString == true, "FAIL: The product "+ textProductTitle + " does not contain entered search key");
            }
        }
        
        //Function to validate Flybuys PSR article
        public void validateFlybuysPSRArticle()
        {
            //click on the search field
            CustomControl.Click(searchField);

            //validate article banner
            Assert.AreEqual(Properties.Resources.flybuysArticleCaption, articleCaption.Text,"FAIL: Expected caption: "+ Properties.Resources.flybuysArticleCaption+" "+ articleCaption.Text);

            //Validate article Title
            Assert.AreEqual(Properties.Resources.flybuysArticleTitleSearchDialogue, articleTitle.Text, "FAIL: Expected Title: " + Properties.Resources.flybuysArticleTitleSearchDialogue + " " + articleTitle.Text);

            //Click on article Title
            CustomControl.Click(articleTitle);

            //Validate HelpSupport/Flybuys page displayed
            helpAndSupportPagePOM.ValidateHelpSupportFlybuysPageDisplayed();

        }

    }
}
