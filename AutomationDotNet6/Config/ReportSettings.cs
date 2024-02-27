using AventStack.ExtentReports;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationDotNet6.Config
{
    public class ReportSettings
    {
        public ExtentTest? TestCase { get; set; }
        public IWebDriver? Driver { get; set; }
    }
}
