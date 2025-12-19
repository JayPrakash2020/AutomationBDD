using System;
using NUnit.Framework;
using OpenQA.Selenium;
using BDDAutomation.Utils;

namespace BDDAutomation
{
    public class NUnitTestItem1
    {
        private SeleniumHelper _helper;

        [SetUp]
        public void Setup()
        {
            _helper = new SeleniumHelper();
        }

        [TearDown]
        public void TearDown()
        {
            _helper?.QuitAllDrivers();
        }

        [Test]
        public void FacebookRegistrationPage()
        {
            // Set to true only if you intend to actually submit the form.
            const bool submitForm = false;

            IWebDriver driver = _helper.CreateChromeDriver(headless: false);

            try
            {
                driver.Navigate().GoToUrl("https://www.facebook.com/");

                // Accept cookies if shown (common on EU pages). Non-throwing.
                try
                {
                    var accept = _helper.WaitUntilVisible(driver, By.XPath("//button[contains(., 'Allow essential and optional cookies') or contains(., 'Accept All') or contains(., 'Allow all cookies')]"), 3);
                    if (accept != null)
                    {
                        accept.Click();
                    }
                }
                catch
                {
                    // ignore if not present
                }

                // Open registration modal
                var createBtn = _helper.WaitUntilVisible(driver, By.XPath("//a[contains(text(),'Create new account') or contains(text(),'Create New Account')]"), 10)
                                ?? _helper.WaitUntilVisible(driver, By.XPath("//div[contains(text(),'Create new account') or contains(text(),'Create New Account')]"), 5);
                Assert.That(createBtn, Is.Not.Null, "Create account button not found");
                createBtn.Click();

                // Fill registration fields
                var firstName = _helper.WaitUntilVisible(driver, By.Name("firstname"), 10);
                Assert.That(firstName, Is.Not.Null, "First name input not found");
                firstName.SendKeys("TestFirst");

                var lastName = _helper.WaitUntilVisible(driver, By.Name("lastname"), 5);
                Assert.That(lastName, Is.Not.Null, "Last name input not found");
                lastName.SendKeys("TestLast");

                // Use a timestamped email to reduce collisions
                var email = $"test.user.{DateTime.UtcNow.Ticks}@example.com";
                var emailInput = _helper.WaitUntilVisible(driver, By.Name("reg_email__"), 5);
                if (emailInput != null)
                {
                    emailInput.SendKeys(email);

                    // Facebook sometimes requires re-entering email in a confirmation field
                    var emailConfirm = _helper.WaitUntilVisible(driver, By.Name("reg_email_confirmation__"), 3);
                    if (emailConfirm != null)
                    {
                        emailConfirm.SendKeys(email);
                    }
                }

                var password = _helper.WaitUntilVisible(driver, By.Name("reg_passwd__"), 5);
                Assert.That(password, Is.Not.Null, "Password input not found");
                password.SendKeys("Str0ngP@ssw0rd!"); // replace with secure value for real tests

                // Birthday selects
                var day = _helper.WaitUntilVisible(driver, By.Name("birthday_day"), 3);
                var month = _helper.WaitUntilVisible(driver, By.Name("birthday_month"), 3);
                var year = _helper.WaitUntilVisible(driver, By.Name("birthday_year"), 3);
                if (day != null && month != null && year != null)
                {
                    _helper.SelectByValue(day, "15");
                    _helper.SelectByValue(month, "6");   // June
                    _helper.SelectByValue(year, "1990");
                }

                // Select gender (1 = female, 2 = male, 3 = custom)
                var gender = _helper.WaitUntilVisible(driver, By.XPath("//input[@name='sex' and (@value='2' or @value='1')]"), 3);
                if (gender != null)
                {
                    gender.Click();
                }

                // Verify submit button present
                var signUpBtn = _helper.WaitUntilVisible(driver, By.Name("websubmit"), 5);
                Assert.That(signUpBtn, Is.Not.Null, "Sign up button not found");

                if (submitForm)
                {
                    signUpBtn.Click();

                    // Optionally: wait for next step or confirmation
                    var nextStep = _helper.WaitUntilVisible(driver, By.XPath("//*[contains(text(),'Find friends') or contains(text(),'Confirm your email') or contains(@id,'reg_errors')]"), 10);
                    Assert.That(nextStep, Is.Not.Null, "Expected next step or confirmation not detected after submit");
                }
                else
                {
                    // Keep test deterministic and avoid creating accounts in CI by default.
                    Console.WriteLine("Form filled. submitForm is false so the form was not submitted.");
                }
            }
            finally
            {
                _helper.QuitDriver(driver);
            }
        }
    }
}
