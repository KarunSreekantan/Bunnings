using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bunnings.Utils
{
    public class ExtentManager
    {
        public static ExtentHtmlReporter htmlReporter;
        private static ExtentReports extent;

        public ExtentManager()
        {

        }

        public ExtentReports getInstance()
        {
            if(extent == null)
            {
                string path1 = AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin\\Debug\\netcoreapp3.1\\", "");
                string path = path1 + "\\Reports\\index.html";
                ExtentHtmlReporter htmlReporter = new ExtentHtmlReporter(path);
                htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Dark;
                extent = new ExtentReports();
                extent.AttachReporter(htmlReporter);
            }
            return extent;
        }
    }
}
