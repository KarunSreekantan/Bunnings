using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Bunnings.Utils
{
    class BunningsCustomControl
    {
        private IWebDriver _Driver;
        int timeoutInSeconds = 5;
        WebDriverWait wait;

        //Constructor for BunningsCustomControl class
        public BunningsCustomControl(IWebDriver Driver)
        {
            this._Driver = Driver;
            wait = new WebDriverWait(_Driver, TimeSpan.FromSeconds(timeoutInSeconds));
        }

        //Custom method to Enter text to supplied web Element
        public void EnterText(IWebElement webElement,string value)
        {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(webElement));
            webElement.Click();
            webElement.Clear();
            webElement.SendKeys(value);
        }

        //Custom method to send Keys to supplied web Elements
        public void SendKeys(IWebElement webElement, string key)
        {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(webElement));
            webElement.SendKeys(key);
        }

        //Custom Method to click on supplied web Element
        public void Click(IWebElement webElement)
        {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(webElement));
            webElement.Click();
        }

        //Custom Method to select an value on a WebElement
        public void SelectByValue(IWebElement webElement, string value)
        {
            SelectElement selectElement = new SelectElement(webElement);
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeSelected(webElement));
            selectElement.SelectByValue(value);
        }

        //Custom Method to select a option with text on a Web Element 
        public void SelectByText(IWebElement webElement, string text)
        {
            SelectElement selectElement = new SelectElement(webElement);
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeSelected(webElement));
            selectElement.SelectByText(text);
        }

        //Custom method to find number of elements matching XPATh
        public int FindElementCount(By by, int timeoutInSeconds = 5)
        {
            for (int i = 0; i < timeoutInSeconds; i++)
            {
                try
                {
                    if (timeoutInSeconds > 0)
                    {
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(by));
                    }
                }
                catch
                { }
            }
            //List<IWebElement> xpath = suggestedForYouContainer.FindElement(By.TagName("a"));
            //List<IWebElement> xpath = _Driver.FindElement(by);
            //ReadOnlyCollection<IWebElement> childrenElements = parentElement.FindElements(By.XPath(" ../"));
            ReadOnlyCollection<IWebElement> xpath = _Driver.FindElements(by);
            int iCount = xpath.Count;
            return iCount;
        }

        //Method to find current URL
        public String getCurrentURL()
        {
            string url = _Driver.Url;
            return url;
        }

        //Custom method to find element with wait
        public IWebElement BunningsFindElement(By by, int timeoutInSeconds = 5)
        {
            for(int i=0;i < timeoutInSeconds; i++)
            {
                try
                {
                    if (timeoutInSeconds > 0)
                    {
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(by));
                    }
                }
                catch
                { }
            }
            return _Driver.FindElement(by);
        }

        //Custom wait for element to be clickable
        public void ExplicitWaitTillElementClickable(IWebElement ele, int timeoutInSeconds = 5)
        {
            for (int i = 0; i < timeoutInSeconds; i++)
            {
                try
                {
                    if (timeoutInSeconds > 0)
                    {
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(ele));
                    }
                }
                catch
                { }
            }
        }

    }
}
