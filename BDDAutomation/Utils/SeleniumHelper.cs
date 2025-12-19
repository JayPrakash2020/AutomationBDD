using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace BDDAutomation.Utils
{
    public class SeleniumHelper
    {
        // Simple short-lived driver list to ensure clean up in TearDown
        private readonly System.Collections.Generic.List<IWebDriver> _drivers = new System.Collections.Generic.List<IWebDriver>();

        public IWebDriver CreateChromeDriver(bool headless = false)
        {
            var options = new ChromeOptions();
            options.AddArgument("--disable-dev-shm-usage");
            options.AddArgument("--no-sandbox");
            if (headless)
            {
                options.AddArgument("--headless=new");
            }

            var driver = new ChromeDriver(options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
            driver.Manage().Window.Maximize();

            _drivers.Add(driver);
            return driver;
        }

        public IWebElement WaitUntilVisible(IWebDriver driver, By by, int timeoutSeconds = 10)
        {
            if (driver == null) throw new ArgumentNullException(nameof(driver));
            if (by == null) throw new ArgumentNullException(nameof(by));
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutSeconds));
                return wait.Until(d =>
                {
                    try
                    {
                        var el = d.FindElement(by);
                        return (el != null && el.Displayed) ? el : null;
                    }
                    catch
                    {
                        return null;
                    }
                });
            }
            catch
            {
                return null;
            }
        }

        public void SelectByValue(IWebElement selectElement, string value)
        {
            if (selectElement == null) throw new ArgumentNullException(nameof(selectElement));
            var select = new SelectElement(selectElement);
            select.SelectByValue(value);
        }

        public void QuitDriver(IWebDriver driver)
        {
            if (driver == null) return;
            try
            {
                driver.Quit();
            }
            catch
            {
                // ignore
            }
            finally
            {
                _drivers.Remove(driver);
            }
        }

        public void QuitAllDrivers()
        {
            foreach (var d in _drivers.ToArray())
            {
                try
                {
                    d.Quit();
                }
                catch
                {
                    // ignore
                }
            }

            _drivers.Clear();
        }
    }
}
