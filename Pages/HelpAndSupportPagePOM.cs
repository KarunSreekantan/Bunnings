using Bunnings.Utils;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bunnings.Pages
{
    class HelpAndSupportPagePOM
    {
        //Declare an instance variable of type IWebDriver to work on the elements present on the bunnings Help and Support page
        private IWebDriver _Driver;
        BunningsCustomControl CustomControl;
        WebDriverWait wait;
        int timeoutInSeconds = 5;

        public HelpAndSupportPagePOM(IWebDriver Driver)
        {
            this._Driver = Driver;
            CustomControl = new BunningsCustomControl(_Driver);
            wait = new WebDriverWait(_Driver, TimeSpan.FromSeconds(timeoutInSeconds));
        }

        //Required webElements on Bunnings Help and support page

        //custom banner H2 displayed at the top of page "//www.bunnings.com.au/help-support/flybuys"
        IWebElement primaryCustomBanner => CustomControl.BunningsFindElement(By.XPath("//h2[@data-locator='label_tTxt']"));


        //Flybus image displayed like a header on the help and support/flybujys page
        IWebElement imgFlybuys => CustomControl.BunningsFindElement(By.CssSelector("img[alt='Flybuys']"));


        //Validate help-support/flybuys page is displayed
        public void ValidateHelpSupportFlybuysPageDisplayed()
        {
            //Wait for flybuys image to load
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector("img[alt='Flybuys']")));

            //Validate URL
            Assert.AreEqual(Properties.Resources.HelpSupportFlyBuysURL, _Driver.Url, "FAIL: Expected URL: " + Properties.Resources.HelpSupportFlyBuysURL + " ACTUAL URL: " + _Driver.Url);

            //Validate Flybuys image displayed on the webpage
            Assert.IsTrue(imgFlybuys.Displayed, "FAIL: Flybuys article not displayed");


            //Validate custom Banner
            Assert.AreEqual(Properties.Resources.primaryCustomBannerText, primaryCustomBanner.Text, "FAIL: Expected Banner text " + Properties.Resources.primaryCustomBannerText + " ACTUAL Text displayed: " + primaryCustomBanner.Text);
        }

    }
}
