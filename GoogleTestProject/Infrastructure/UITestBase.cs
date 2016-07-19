using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coypu;
using TechTalk.SpecFlow;

namespace GoogleTestProject.Infrastructure
{
    [Binding]
    public class UITestBase
    {
        private static BrowserSession _browser;
        protected BrowserSession Browser { get; private set; }

        public UITestBase()
        {
            if (_browser == null)
            {
                _browser = Initialisation.CreateBrowserSession();
            }
            Browser = _browser;
        }

        public string BaseURL()
        {
            return ConfigurationManager.AppSettings["BaseUrl"];
        }

        [BeforeScenario]
        private static void BeforeScenario()
        {
        }

        [AfterScenario]
        private static void TearDown()
        {
            // Tear down after scenario, clean up, take screenshots if failed etc...

        }

        [AfterTestRun]
        private static void ShutDown()
        {           
            // Close the browser
            _browser.Dispose();
            _browser = null;
        }
    }
}
