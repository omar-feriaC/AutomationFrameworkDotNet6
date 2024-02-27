using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager;

namespace AutomationDotNet6.Config
{
    public class SmartlyEnvironment
    {
        public static string GetEnvUrl(string AppEnvironment)
        {
            return AppEnvironment switch
            {
                "SmartlyQA1" => "https://intake-qa1.sedgwick.com/",
                "SmartlyQA2" => "https://intake-qa2.sedgwick.com/",
                "SmartlyUAT" => "https://intake-uat.sedgwick.com/",
                "SmartlyE2E" => "https://intake-e2e.sedgwick.com/",
                "SmartlyDemo" => "https://intake-demo.sedgwick.com/",
                "SmartlyTesting" => "https://intake-uat.sedgwick.com/",
                "MegaIntakePreProd" => "https://intake-uat.sedgwick.com/",
                _ => "https://intake-uat.sedgwick.com/"
            };
        }

    }



}
