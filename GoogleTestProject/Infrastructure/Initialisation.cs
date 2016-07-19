using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coypu;
using Coypu.Drivers.Selenium;
using TechTalk.SpecFlow;

namespace GoogleTestProject.Infrastructure
{
    public class Initialisation
    {
        public static BrowserSession CreateBrowserSession()
        {
            var sessionConfiguration = new SessionConfiguration
            {
                AppHost = ConfigurationManager.AppSettings["BaseUrl"],

                Browser = Coypu.Drivers.Browser.Chrome,
                Driver = typeof(SeleniumWebDriver),
            Timeout = TimeSpan.FromSeconds(15)
            };

            switch (ConfigurationManager.AppSettings["TargetBrowser"].ToLower().Trim())
            {
                case "firefox":
                    sessionConfiguration.Browser = Coypu.Drivers.Browser.Firefox;
                    break;
                case "ie":
                    sessionConfiguration.Browser = Coypu.Drivers.Browser.InternetExplorer;
                    break;
                default:
                    sessionConfiguration.Browser = Coypu.Drivers.Browser.Chrome;
                    break;
            }

            var browser = new BrowserSession(sessionConfiguration);
            browser.MaximiseWindow();
            return browser;
        }
    }
}
