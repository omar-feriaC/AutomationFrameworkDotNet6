using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using System.Drawing.Text;
using AventStack.ExtentReports.Reporter.Config;
using System.Collections;
using AventStack.ExtentReports.Model;

namespace AutomationDotNet6.Config
{
    public class ReportFixture
    {
        //Local Fields
        private readonly ReportSettings _reportSettings;
        private ExtentReports Extent;
        private ExtentTest TestCase;
        private ExtentSparkReporter Spark;
        private IWebDriver? Driver;
        private Status Status;
        private string baseReportFolder = "";
        private string fullReportFolder = "";


        public string BaseReportFolder { get { return baseReportFolder; } }
        public string FullReportFolder { get { return fullReportFolder; } }

        public ReportFixture(ReportSettings reportSettings)
        {
            //Setup Driver
            _reportSettings = reportSettings;
            _reportSettings.TestCase = this.TestCase;
            _reportSettings.Driver = this.Driver;
            
        }

        public void SetupReport()
        {
            Extent = new ExtentReports();
            Extent.AddSystemInfo("Project", TestContext.Parameters["ProjectName"]);
            Extent.AddSystemInfo("Browser", TestContext.Parameters["BrowserName"]);
            Extent.AddSystemInfo("Env", TestContext.Parameters["TestEnvironment"]);
            Extent.AddSystemInfo("Executed By", Environment.UserName.ToString());
            Extent.AddSystemInfo("Executed Machine", Environment.MachineName.ToString());
            Extent.AddSystemInfo("Execution Start Time", DateTime.Now.ToString("MM/ddy/yyy hh:mm:ss"));
            Spark = new ExtentSparkReporter(GetFolderReportPath());
            Spark.Config.Theme = Theme.Dark;
            Spark.Config.ReportName = TestContext.Parameters["BrowserName"];
            Spark.Config.DocumentTitle = TestContext.Parameters["BrowserName"];
            Spark.Config.Encoding = "utf-8";
            Extent.AttachReporter(Spark);
        }

        private string GetFolderReportPath()
        {
            //Get Result Path
            string userID = $"_{Environment.UserName.ToString()}";
            baseReportFolder = TestContext.Parameters["ReportLocation"] + DateTime.Now.ToString("MMddyyyy_hhmmss") + userID + @"\";
            fullReportFolder = BaseReportFolder + TestContext.Parameters["ProjectName"] + @"\" + TestContext.Parameters["ReportName"] + ".html";
            CreateFolderPath();
            return fullReportFolder;
        }

        private void CreateFolderPath()
        {
            try
            {
                string[]? strSubFolders = new string[2] { "ScreenShots", TestContext.Parameters["ProjectName"] };
                bool blFExist = System.IO.Directory.Exists(BaseReportFolder);
                if (!blFExist)
                {
                    System.IO.Directory.CreateDirectory(BaseReportFolder);
                }
                else
                    blFExist = false;

                foreach (string strFolder in strSubFolders)
                {
                    blFExist = System.IO.Directory.Exists(BaseReportFolder + strFolder);

                    if (!blFExist)
                    {
                        System.IO.Directory.CreateDirectory(BaseReportFolder + strFolder);
                    }
                }
            }
            catch { }
        }

        public Media TakeScreenshot()
        {
            string ssName = $"SC_Image_{DateTime.Now.ToString("MMddyyyy_hhmmss")}.png";
            string strFileLocation = $@"{BaseReportFolder}Screenshots\{ssName}";
            Screenshot screenshot = (Driver as ITakesScreenshot).GetScreenshot();
            screenshot.SaveAsFile(strFileLocation);
            var ss = MediaEntityBuilder.CreateScreenCaptureFromPath(strFileLocation).Build();
            return ss;
        }

        public void Log(ExtentTest testCase, string description, Status status, bool screenshot = false, string addCategory = "")
        {
            Media? tempSS = null;
            if (screenshot)
            {
                tempSS = TakeScreenshot();
            }

            switch (status)
            {
                case Status.Pass:
                    testCase.Pass(description, tempSS).AssignCategory(addCategory);
                    break;
                case Status.Fail:
                    testCase.Fail(description, tempSS).AssignCategory(addCategory);
                    break;
                case Status.Info:
                    testCase.Info(description, tempSS).AssignCategory(addCategory);
                    break;
                case Status.Warning:
                    testCase.Warning(description, tempSS).AssignCategory(addCategory);
                    break;
            }
            var objStatus = TestContext.CurrentContext.Result.Outcome.Status;
        }

        public ExtentTest CreateTestCase(string testCase)
        {
            return Extent.CreateTest(testCase);
        }

        public void InitReportDriver(IWebDriver driver)
        {
            this.Driver = driver;
        }

        public void FlushReport() 
        {
            Extent.Flush();
        }

    }
}
