# C# Selenium Test Automation Framework with Specflow
I have spent 1-2 hrs a day for 5 days to complete this challenge.

As part of the challenge i have uploaded teh following documents
<li>Bunnings Test strategy document uploaded in the repo</li>
<li>Bunnings Test Approach in an Agile development environment</li>
<li>Bunnings Test Cases</li>
<li>Bunings Scenario</li>
<li>Bunnings Automation Framewrok</li>

Test Automation Framework built for Bunnings Challenge 


<ul>
<h2>Instruction to clone and run Test Automation Framework </h2>

  <li>Clone the repo.</li>
  <li>Open the solution Bunnings.sln in Visual Studio 2019.</li>
  <li>Build the solution.</li>
  <li>If Test Explorer not enabled,please enable Test explorer selecting the option View->Test Explorer.</li> 
  <li>To run the tests, right click on any test in "Test Explorer" and click on "Run Selected Tests".</li>
  <li>You can view your test results on Visula Studio and an Extent Test Report is created and saved under the Reports Folder in the Bunnings.Sln,the file "index.html" consists the current test run report.This report can be opened using the chrome browser</li>
  
FYI: MAJORITY OF VALIDATIONS ARE ADED IN THE FUNCTIONS IMPLEMENTED IN THE PAGE OBJECT MODEL.
  
  
FYI: Primary browser used for automation is chrome Browser  

<h2>Soliution Folder Structure </h2>

  <li>Features : Features Folder consits of Specfloe Test features.</li>

  <li>Hooks : Hooks.cs class consists of code that is executed after or before particular event like [BeforeTestRun],[BeforeFeature],[BeforeScenario],[AfterStep],                              [AfterScenario],[AfterTestRun]</li>

  <li>Pages : Concists of classes that represent PAGE object Models of automated web pages.</li>

  <li>Reports : Extent report</li>

  <li>Step Definitions: Contains Step definitions</li>

  <li>Utils : Consists of code for Config,Custom Controls , Driver helper and Extent Manger (Test reporting)</li>

    
</ul>


