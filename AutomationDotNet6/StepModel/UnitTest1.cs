using AutomationDotNet6.Config;
using AutomationDotNet6.Drivers;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Model;
using OpenQA.Selenium;


namespace AutomationDotNet6.StepModel
{
    public class Tests
    {
        TestSettings? testSettings;
        DriverFixture? driver;
        Reporting? Report;
        ExtentTest? TestCase;


        [OneTimeSetUp]
        public void OneTimeSetup() 
        {
            Report = new Reporting(TestCase);
            Report.SetupReport();
        }

        
        public void Setup()
        {
            testSettings = new TestSettings()
            {
                BrowserType = BrowserType.Chrome,
                BrowserManager = BrowserManager.Chrome,
                TimeoutInterval = TimeSpan.FromSeconds(5)
            };

            driver = new DriverFixture(testSettings);
            TestCase = Report.CreateTestCase(TestContext.CurrentContext.Test.MethodName);

            driver.Driver.Navigate().GoToUrl(SmartlyEnvironment.GetEnvUrl("SmartlyUAT"));
            Report.InitDriver(driver.Driver);
        }


        [Test]
        public void Test1()
        {
            Setup();
            Assert.Pass();
        }

        [TearDown]
        public void TearDown()
        {
            driver.CloseDriver();
            Report.Extent.Flush();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown() 
        {
            Report.Extent.Flush();
        }

    }
}