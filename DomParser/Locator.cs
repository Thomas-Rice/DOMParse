using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;

namespace DomParser
{
    public class Locator
    {
        private SeleniumOperations _seleniumOperations;

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
        

        public void TryFindElementFromList(ElementLocator elementLocators)
        {
            
            foreach (var locator in elementLocators.XpathAttributes.GetAttributes)
            {
                if (locator != null)
                {
                    try
                    {
                        _seleniumOperations = new SeleniumOperations();
                        if (string.Equals(locator.Name, "class", StringComparison.CurrentCultureIgnoreCase))
                        {
                            _seleniumOperations.ClickOnElementClassName("k");;
                            Console.WriteLine($"Getting By Class {locator.Value}");
                            break;
                        }

                        if (locator.Name.ToLower() == "id".ToLower())
                        {
                            _seleniumOperations.ClickOnElementId(locator.Value);
                            Console.WriteLine($"Getting By ID {locator.Value}");
                            break;
                        }

                        if (locator.Name.ToLower() == "name".ToLower())
                        {
                            _seleniumOperations.ClickOnElementClassName(locator.Value);
                        }

                        if (locator.Name.ToLower() == "link".ToLower())
                        {
                            _seleniumOperations.ClickOnElementClassName(locator.Value);
                        }
                        
                    }
                    catch (NoSuchElementException e)
                    {
                        Console.WriteLine(e);
                        _seleniumOperations.CloseDriver();
                    }
                }

            }
        }
    }
}
