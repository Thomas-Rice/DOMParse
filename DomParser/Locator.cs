using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace DomParser
{
    public class Locator
    {
        private ChromeDriver _driver;

        public void GetAllLocatorsXpath(string xpathLocator, ElementLocators elementLocators)
        {
            var elements = elementLocators.Locators.Where(x => x.Xpath.Equals("/html/body/div/table"));

            //PrintAllResults(elements);

            foreach (var elementLocator in elements)
            {
                TryFindElementFromList(elementLocator);
            }
        }

        private static void PrintAllResults(IEnumerable<ElementLocator> elements)
        {
            foreach (var element in elements)
            {
                Console.WriteLine($"FOUND Xpath: {element.Xpath}");
                Console.WriteLine($"FOUND Name: {element.XpathAttributes.Name}");
                Console.WriteLine($"FOUND Class: {element.XpathAttributes.ClassAttribute}");
                Console.WriteLine($"FOUND Id: {element.XpathAttributes.Id}");
                Console.WriteLine($"FOUND Link: {element.XpathAttributes.Link}");
            }
        }

        public void Selenium()
        {
            _driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
            {
                Url = "file:///C:/Users/trice/Desktop/new2.html"
            };
        }

        public void SeleniumClassName(string xpath)
        {
            var test = xpath.Replace(' ', '.');
            var element = _driver.FindElement(By.CssSelector(test));
            element.Click();
            _driver.Close();
        }

        public void SeleniumId(string xpath)
        {
            var test = xpath.Replace(' ', '.');
            var element = _driver.FindElement(By.Id(test));
            element.Click();
            _driver.Close();
        }

        public void TryFindElementFromList(ElementLocator elementLocators)
        {
            
            foreach (var locator in elementLocators.XpathAttributes.GetAttributes)
            {
                if (locator != null)
                {
                    try
                    {
                        Selenium();
                        if (string.Equals(locator.Name, "class", StringComparison.CurrentCultureIgnoreCase))
                        {
                            SeleniumClassName("k");
                            Console.WriteLine($"Getting By Class {locator.Value}");
                            break;
                        }

                        if (locator.Name.ToLower() == "id".ToLower())
                        {
                            SeleniumId(locator.Value);
                            Console.WriteLine($"Getting By ID {locator.Value}");
                            break;
                        }

                        if (locator.Name.ToLower() == "name".ToLower())
                        {
                            SeleniumClassName(locator.Value);
                        }

                        if (locator.Name.ToLower() == "link".ToLower())
                        {
                            SeleniumClassName(locator.Value);
                        }
                        
                    }
                    catch (NoSuchElementException e)
                    {
                        Console.WriteLine(e);
                        _driver.Close();
                    }
                }

            }
        }
    }
}
