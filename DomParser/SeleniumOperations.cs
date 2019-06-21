using System.IO;
using System.Reflection;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace DomParser
{
    public class SeleniumOperations
    {
        private readonly ChromeDriver _driver;

        public SeleniumOperations()
        {
            _driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
            {
                Url = "file:///C:/Users/trice/Desktop/new2.html"
            };
        }

        public void ClickOnElementId(string xpath)
        {
            var test = xpath.Replace(' ', '.');
            var element = _driver.FindElement(By.Id(test));
            element.Click();
            _driver.Close();
        }

        public void ClickOnElementClassName(string xpath)
        {
            var test = xpath.Replace(' ', '.');
            var element = _driver.FindElement(By.CssSelector(test));
            element.Click();
            _driver.Close();
        }

        public void CloseDriver()
        {
            _driver.Close();
        }
    }
}