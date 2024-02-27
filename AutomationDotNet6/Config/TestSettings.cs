using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationDotNet6.Config
{
    public class TestSettings
    {
        public BrowserType BrowserType { get; set; }
        public BrowserManager BrowserManager { get; set; }
        public TimeSpan TimeoutInterval { get; set; }

    }
}
