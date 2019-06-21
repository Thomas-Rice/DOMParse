using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumTester
{
    class SeleniumTest
    {
        IWebDriver driver;

        [SetUp]
        public void startBrowser()
        {
            driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
        }

        [Test]
        public void test()
        {
            driver.Url = "http://www.google.com";
            var element = driver.FindElement(By.XPath(""));
            element.Click();
        }

        [TearDown]
        public void closeBrowser()
        {
            driver.Close();
        }

    }
}
