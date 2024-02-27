using AutomationDotNet6.Config;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager;

namespace AutomationDotNet6.Drivers
{
    public class DriverFixture
    {
        private readonly TestSettings _testSettings;

        public IWebDriver Driver { get; }

        public DriverFixture(TestSettings testSettings) 
        {
            //Setup Driver
            _testSettings = testSettings;
            var DriverConfig = GetWebDriverManager();
            //Setup Driver Manager
            switch (_testSettings.BrowserType)
            {
                case BrowserType.Chrome:
                    ChromeOptions optionsChrome = new ChromeOptions();
                    optionsChrome.AddArgument("no-sandbox");
                    optionsChrome.AddArgument("start-maximized");
                    break;
                case BrowserType.Firefox:
                    FirefoxOptions optionFF = new FirefoxOptions();
                    optionFF.AddArgument("no-sandbox");
                    optionFF.AddArgument("start-maximized");
                    break;
                case BrowserType.Edge:
                    EdgeOptions optionsEdge = new EdgeOptions();
                    optionsEdge.AddArgument("no-sandbox");
                    optionsEdge.AddArgument("start-maximized");
                    break;
                default:
                    ChromeOptions optionsChrome2 = new ChromeOptions();
                    optionsChrome2.AddArgument("no-sandbox");
                    optionsChrome2.AddArgument("start-maximized");
                    break;
            }
            //Driver InstanceSetup
            Driver = GetWebDriver();
            Driver.Manage().Timeouts().ImplicitWait = _testSettings.TimeoutInterval;  //Max time to wait before throwing an exception
            Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(20);     //Max time to wait a page to be loaded before throwing an exception
            Driver.Manage().Window.Maximize();
            Driver.Manage().Cookies.DeleteAllCookies();
        }


        /// <summary>
        /// Returns an intance for selenium webdriver
        /// </summary>
        /// <param name="browserType"></param>
        /// <returns></returns>
        public IWebDriver GetWebDriver() 
        {
            return _testSettings.BrowserType switch
            {
                BrowserType.Chrome => new ChromeDriver(),
                BrowserType.Edge => new EdgeDriver(),
                BrowserType.Firefox => new FirefoxDriver(),
                _ => new ChromeDriver()
            };
        }

        /// <summary>
        /// Returns a string for WebDriver Manager
        /// </summary>
        /// <returns></returns>
        public string GetWebDriverManager()
        {
            return _testSettings.BrowserManager switch
            {
                BrowserManager.Chrome => new DriverManager().SetUpDriver(new ChromeConfig()),
                BrowserManager.Firefox => new DriverManager().SetUpDriver(new FirefoxConfig()),
                BrowserManager.Edge => new DriverManager().SetUpDriver(new EdgeConfig()),
                _ => new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig())
            };
        }


        public void CloseDriver()
        {
            Driver.Close();
            Driver.Quit();
        }



    }
}

public enum BrowserType
{
    Chrome,
    Firefox,
    Edge
}

public enum BrowserManager
{
    Chrome,
    Firefox,
    Edge
}