using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bunnings.Utils
{
    //Driver helper class that Supplys an instance of the driver to the Framewrok.This avoids duplicate driver creations.
    public class DriverHelper
    {
        public IWebDriver Driver { get; set; }
    }
}
