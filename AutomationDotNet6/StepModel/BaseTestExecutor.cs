using AutomationDotNet6.Config;
using AutomationDotNet6.Drivers;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Model;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationDotNet6.StepModel
{
    public class BaseTestExecutor
    {
        TestSettings? testSettings = null;
        ReportSettings? reportSettings = null;
        ReportFixture? reportFixture = null;
        DriverFixture? driver = null;
        ExtentTest? testCase = null;


        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            //Configure report
            reportSettings = new ReportSettings()
            {
                TestCase = testCase,
                Driver = null
            };
            reportFixture = new ReportFixture(reportSettings);
            reportFixture.SetupReport();
        }

        public void SetUpTestCase(string testName)
        {
            testSettings = new TestSettings()
            {
                BrowserType = BrowserType.Chrome,
                BrowserManager = BrowserManager.Chrome,
                TimeoutInterval = TimeSpan.FromSeconds(5)
            };

            testCase = reportFixture.CreateTestCase(testName);
            driver = new DriverFixture(testSettings);
            driver.Driver.Navigate().GoToUrl(SmartlyEnvironment.GetEnvUrl("SmartlyUAT"));
            reportFixture.InitReportDriver(driver.Driver);
        }

        [Test]
        public void Test1()
        {
            SetUpTestCase(TestContext.CurrentContext.Test.MethodName);
            reportFixture.Log(testCase, "test1", Status.Pass, false);
            reportFixture.Log(testCase, "test1", Status.Fail, false);
            reportFixture.Log(testCase, "test1", Status.Warning, false);
            reportFixture.Log(testCase, "test1", Status.Info, true, "das");
            reportFixture.Log(testCase, "test1", Status.Pass, true, "das");
            reportFixture.Log(testCase, "test1", Status.Pass, false, "delta");
            Assert.Pass();
        }

        [TearDown]
        public void TearDown()
        {
            driver.CloseDriver();
            reportFixture.FlushReport();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            reportFixture.FlushReport();
        }

    }
}
