# C# Selenium Test Automation Framework with Specflow
Test Automation Framework built for Bunnings Challenge 


<ul>
<h2>Instruction to clone and run Test Automation Framework </h2>

  <li>Clone the repo.</li>
  <li>Open the solution Bunnings.sln in Visual Studio 2019.</li>
  <li>Build the solution.</li>
  <li>If Test Explorer not enabled,please enable Test explorer selecting the option View->Test Explorer.</li> 
  <li>To run the tests, right click on any test in "Test Explorer" and click on "Run Selected Tests".</li>
  <li>You can view your test results on Visula Studio and an Extent Test Report is created and saved under the Reports Folder in the Bunnings.Sln,the file "index.html" consists the current test run report.This report can be opened using the chrome browser</li>
  
  
FYI: Primary browser used for automation is chrome Browser  

<h2>Soliution Folder Structure </h2>
  Features : Features Folder consits of Specfloe Test features

Hooks : Hooks.cs class consists of code that is executed after or before particular event like [BeforeTestRun],[BeforeFeature],[BeforeScenario],[AfterStep],[AfterScenario],  [AfterTestRun]

Pages : Concists of classes that represent PAGE object Models of automated web pages.

Reports : Extent report

Step Definitions: Contains Step definitions 

Utils : Consists of code for Config,Custom Controls , Driver helper and Extent Manger (Test reporting)


    
</ul>


