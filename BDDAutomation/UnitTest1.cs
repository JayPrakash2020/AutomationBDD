using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Threading;

namespace BDDAutomation
{
    [TestFixture]   
    public class UnitTest1
    {
        [Test]
        public void BrowserLaunch()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://schooltide.udeshatechnology.com/login.html");
            driver.Manage().Window.Maximize();

            Thread.Sleep(2000);

            IWebElement username = driver.FindElement(By.Name("username"));
            username.SendKeys("ravijha");

            IWebElement password = driver.FindElement(By.Name("password"));
            password.SendKeys("ravijha");

            Thread.Sleep(2000);

            IWebElement loginBtn = driver.FindElement(By.Name("submit"));
            loginBtn.Click();

            Thread.Sleep(3000);

            driver.Quit();
        }
        [Test]
        public void RegisterFacebookAccount()
        {
            IWebDriver driver=new ChromeDriver();
            driver.Navigate().GoToUrl("https://www.facebook.com/reg/");
            driver.Manage().Window.Maximize();
            // implementing implicit wait
            driver.Manage().Timeouts().ImplicitWait = System.TimeSpan.FromSeconds(10);

            IWebElement firstName = driver.FindElement(By.Name("firstname"));
            firstName.SendKeys("Ravi");
            Thread.Sleep(2000);

            IWebElement lastName = driver.FindElement(By.Name("lastname"));
            lastName.SendKeys("Jha");
            Thread.Sleep(2000);

            IWebElement date = driver.FindElement(By.Id("day"));
            SelectElement birthdate=new SelectElement(date);
            birthdate.SelectByValue("10");
            Thread.Sleep(2000);

            IWebElement month = driver.FindElement(By.Name("birthday_month"));
            SelectElement birthmonth = new SelectElement(month);
            birthmonth.SelectByText("Mar");
            Thread.Sleep(2000);

            IWebElement year = driver.FindElement(By.Name("birthday_year"));
            SelectElement birthyear = new SelectElement(year);
            birthyear.SelectByIndex(18);
            Thread.Sleep(2000);

            IWebElement gender = driver.FindElement(By.XPath("//input[@value='2']"));
            gender.Click();
            Thread.Sleep(2000);

            IWebElement email= driver.FindElement(By.Name("reg_email__"));
            email.SendKeys("jp@gmail.com");
            Thread.Sleep(2000);

            IWebElement password = driver.FindElement(By.Id("password_step_input"));
            password.SendKeys("Ravi@1234");
            Thread.Sleep(2000);

            IWebElement signUpBtn = driver.FindElement(By.Name("websubmit"));
            signUpBtn.Click();
            Thread.Sleep(5000);

            driver.Quit();


        }
    }
}
