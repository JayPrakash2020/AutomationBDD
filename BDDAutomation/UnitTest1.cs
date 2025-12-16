using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;

namespace BDDAutomation
{
    public class SchoolProgram
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void BrowserLaunch()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://schooltide.udeshatechnology.com/login.html");
            driver.Manage().Window.Maximize();
            Thread.Sleep(2000);
            IWebElement username=driver.FindElement(By.Name("username"));
            username.SendKeys("ravijha");
            Thread.Sleep(2000);
            IWebElement password = driver.FindElement(By.Name("password"));
            password.SendKeys("ravijha");
            Thread.Sleep(3000);
            IWebElement loginBtn = driver.FindElement(By.Name("submit"));
            loginBtn.Click();
            Thread.Sleep(3000);
            driver.Close();
            driver.Quit();
        }
    }
}