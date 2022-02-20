using Bunnings.Utils;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using OpenQA.Selenium.Firefox;

namespace Bunnings.Hooks
{
    [Binding]
    public sealed class Hooks
    {
        //Hooks is the statement of code that is executed after or before particular event.
        private DriverHelper _driverHelper;
        private static ExtentTest featureName;
        private static ExtentTest scenario;
        private static ExtentReports extent;
        public static string ReportPath;
        private readonly ScenarioContext _scenarioContext;
        private static ExtentManager _extentManager;



        //Hooks constructor
        public Hooks(DriverHelper driverHelper, ScenarioContext scenarioContext)
        {
            _driverHelper = driverHelper;
            _scenarioContext = scenarioContext;

        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            //Code to instanciate extent manager to log steps
            _extentManager = new ExtentManager();
            extent = _extentManager.getInstance();
        }

        [BeforeFeature]
        public static void BeforeFeature()
        {
            //Create dynamic feature name
            featureName = extent.CreateTest<Feature>(FeatureContext.Current.FeatureInfo.Title);
            Console.WriteLine("BeforeFeature");
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            Array Browsers = Array.CreateInstance(typeof(String), 3);
            var tags = _scenarioContext.ScenarioInfo.Tags;
            var val = _scenarioContext.ScenarioInfo.Arguments.Values;
            val.CopyTo(Browsers, 0);

            string browser = Browsers.GetValue(0).ToString();

            //Code to choose browser for execution based on browser selection passed from the step in a feature file
            switch (browser)
            {
                case "chrome":
                    ChromeOptions option = new ChromeOptions();
                    option.AddArgument("start-maximized");
                    option.AddArguments("--disable-gpu");

                    new DriverManager().SetUpDriver(new ChromeConfig());
                    Console.WriteLine("Setup");
                    _driverHelper.Driver = new ChromeDriver(option);

                    Console.WriteLine("BeforeScenario");
                    break;
                case "firefox":
                    FirefoxOptions foption = new FirefoxOptions();
                    foption.AddArgument("start-maximized");
                    foption.AddArguments("--disable-gpu");
                    new DriverManager().SetUpDriver(new FirefoxConfig());
                    Console.WriteLine("Setup");
                    _driverHelper.Driver = new FirefoxDriver(foption);
                    break;
                default:
                    ChromeOptions coption = new ChromeOptions();
                    coption.AddArgument("start-maximized");
                    coption.AddArguments("--disable-gpu");

                    new DriverManager().SetUpDriver(new ChromeConfig());
                    Console.WriteLine("Setup");
                    _driverHelper.Driver = new ChromeDriver(coption);

                    Console.WriteLine("BeforeScenario");
                    break;
            }

            //Create dynamic scenario name
            scenario = featureName.CreateNode<Scenario>(_scenarioContext.ScenarioInfo.Title);
        }


        [AfterStep]
        public void InsertReportingSteps()
        {
            //Logging Step ststus pass/Fail in Extent report
            var stepType = ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString();
            if (_scenarioContext.TestError == null)
            {
                if (stepType == "Given")
                {
                    scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text);
                }

                else if (stepType == "When")
                {
                    scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text);
                }

                else if (stepType == "Then")
                {
                    scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text);
                }

                else if (stepType == "And")
                {
                    scenario.CreateNode<And>(ScenarioStepContext.Current.StepInfo.Text);
                }
            }
            else if (_scenarioContext.TestError != null)
            {
                if (stepType == "Given")
                {
                    scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text).Fail(_scenarioContext.TestError.Message);
                }
                else if (stepType == "When")
                {
                    scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text).Fail(_scenarioContext.TestError.Message);
                }
                else if (stepType == "Then")
                {
                    scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text).Fail(_scenarioContext.TestError.Message);
                }
                else if (stepType == "And")
                {
                    scenario.CreateNode<And>(ScenarioStepContext.Current.StepInfo.Text).Fail(_scenarioContext.TestError.Message);
                }
            }
        }

        [AfterScenario]
        public void AfterScenario()
        {
            //Clean up
            Console.WriteLine("AfterScenario");

            //kill the browser
            _driverHelper.Driver.Quit();
            _driverHelper.Driver.Dispose();
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            //Flush report once test completes
            extent.Flush();
            extent = null;
        }
    }
}

