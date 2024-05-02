using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeleniumExtras.WaitHelpers;
using AngleSharp.Dom;
using OpenQA.Selenium.Interactions;

namespace AutomationDotNet6.Extensions
{
    public class WebElements
    {
        // Global Settings
        private IWebDriver driver;
        private TimeSpan defaultTimeout = TimeSpan.FromSeconds(10);
        
        /// <summary>
        /// Inits driver into WE Class
        /// </summary>
        /// <param name="Driver"></param>
        public WebElements(IWebDriver Driver) 
        { this.driver = Driver; }


        #region Common Actions

        public object GetFluentWait(IWebElement element, string actionToDo, string textValue = "", TimeSpan? timeout = null) 
        {
            if (element == null) throw new NullReferenceException("Please make sure to send a not null WebWelement.");
            DefaultWait<IWebDriver> fluentWait = new DefaultWait<IWebDriver>(driver);
            fluentWait.Timeout = timeout ?? defaultTimeout;
            fluentWait.PollingInterval = TimeSpan.FromMilliseconds(250);
            fluentWait.IgnoreExceptionTypes(typeof(WebDriverTimeoutException), typeof(SuccessException));

            switch (actionToDo.ToUpper()) 
            {
                case "DISPLAYED":
                    fluentWait.Until(x => element.Displayed);
                    break;
                case "CLICK":
                    fluentWait.Until(x => element).Click();
                    break;
                case "CLEAR":
                    fluentWait.Until(x => element).Clear();
                    break;
                case "SENDKEYS":
                    fluentWait.Until(x => element).SendKeys(textValue);
                    break;
                case "CUSTOMSENDKEYS":
                    Actions action = new Actions(driver);
                    element.Click();
                    action.KeyDown(Keys.Control).SendKeys(Keys.Home).Perform();
                    element.Clear();
                    Thread.Sleep(TimeSpan.FromMilliseconds(500));
                    element.SendKeys(Keys.Delete);
                    element.SendKeys(textValue);
                    break;
            }

            return fluentWait;
        }



        #endregion


        #region Expected Conditions

        /// <summary>
        /// Waits until alert is present
        /// </summary>
        /// <returns></returns>
        public bool WaitUntilAlertIsPresent(TimeSpan? timeout = null)
        {
            try 
            {
                WebDriverWait wait = new WebDriverWait(driver, timeout ?? defaultTimeout);
                wait.Until(ExpectedConditions.AlertIsPresent());
                return true;
            }
            catch 
            { return false; }
        }

        /// <summary>
        /// Waits until elemtn exist
        /// </summary>
        /// <param name="by"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public bool WaitUntilElementExist(By by, TimeSpan? timeout = null)
        {
            try 
            {
                WebDriverWait wait = new WebDriverWait(driver, timeout ?? defaultTimeout);
                wait.Until(ExpectedConditions.ElementExists(by));
                return true;
            }
            catch { return false; }
            
        }

        /// <summary>
        /// Waits until element is visible
        /// </summary>
        /// <param name="by"></param>
        /// <returns></returns>
        public bool WaitUntilElementIsvisible(By by, TimeSpan? timeout = null)
        {
            try 
            {
                WebDriverWait wait = new WebDriverWait(driver, timeout ?? defaultTimeout);
                wait.Until(ExpectedConditions.ElementIsVisible(by));
                return true;
            }
            catch { return false; }
            
        }

        /// <summary>
        /// Waits until element is selected, example: checkboxes selected
        /// </summary>
        /// <param name="by"></param>
        /// <param name="selected"></param>
        public void WaitUntilElementStateToBe(By by, bool selected = true,  TimeSpan? timeout = null)
        {
            WebDriverWait wait = new WebDriverWait(driver, timeout ?? defaultTimeout);
            wait.Until(ExpectedConditions.ElementSelectionStateToBe(by, selected));
        }

        /// <summary>
        /// Waits until element is clickable
        /// </summary>
        /// <param name="by"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public bool WaitUntilElementToBeClickable(By by, TimeSpan? timeout = null)
        {
            try 
            {
                WebDriverWait wait = new WebDriverWait(driver, timeout ?? defaultTimeout);
                wait.Until(ExpectedConditions.ElementToBeClickable(by));
                return true;
            }
            catch { return false; }
        }

        /// <summary>
        /// Waits until element to be selected or not
        /// </summary>
        /// <param name="by"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public bool WaitUntilElementToBeSelected(By by, TimeSpan ? timeout = null)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, timeout ?? defaultTimeout);
                wait.Until(ExpectedConditions.ElementToBeSelected(by));
                return true;
            }
            catch { return false; }
        }

        /// <summary>
        /// Waits until element is hidden
        /// </summary>
        /// <param name="by"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public bool WaitUntilElementHidden(By by, TimeSpan? timeout = null)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, timeout ?? defaultTimeout);
                wait.Until(ExpectedConditions.InvisibilityOfElementLocated(by));
                return true;
            }
            catch { return false; }
        }

        /// <summary>
        /// Waits precence of elements
        /// </summary>
        /// <param name="by"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public bool WaitUntilElementPresenceOfAll(By by, TimeSpan? timeout = null)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, timeout ?? defaultTimeout);
                wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(by));
                return true;
            }
            catch { return false; }
        }

        /// <summary>
        /// Waits until title change or contains
        /// </summary>
        /// <param name="textTitle"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public bool WaitUntilTitleContains(string textTitle, TimeSpan? timeout = null)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, timeout ?? defaultTimeout);
                wait.Until(ExpectedConditions.TitleContains(textTitle));
                return true;
            }
            catch { return false; }
        }

        /// <summary>
        /// Waits until url change or contains
        /// </summary>
        /// <param name="textTitle"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public bool WaitUntilUrlContains(string textTitle, TimeSpan? timeout = null)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, timeout ?? defaultTimeout);
                wait.Until(ExpectedConditions.UrlContains(textTitle));
                return true;
            }
            catch { return false; }
        }
        
        #endregion Expected Conditions

    }
}
