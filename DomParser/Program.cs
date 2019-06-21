using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;
using HtmlAgilityPack;

namespace DomParser
{
    using System;

    public class Program
    {
        private static ElementLocators _elementLocators;
        private static HtmlAttributeCollection _attribs;

        public static void Main()
        {
            _elementLocators = new ElementLocators
            {
                Locators = new List<ElementLocator>()
            };

            var doc = new HtmlDocument();
            doc.Load("C:\\Users\\trice\\source\\repos\\DomParser\\DomParser\\new2.html");
            var allElements = doc.DocumentNode.SelectNodes("descendant::*").ToArray();


            foreach (var element in allElements)
            {
                var locator = new ElementLocator
                {
                    XpathAttributes = new XpathAttributes()
                };
                var elementAttributes = "";
                _attribs = element.Attributes;
                foreach (var xAttribute in _attribs)
                {
                    elementAttributes += $" {xAttribute.Name}: {xAttribute.Value},";
                    locator = GetCoreAttributes(xAttribute, locator);
                }

                locator.Xpath = GenerateXpath(element);
                Console.Write($"XPath: {GenerateXpath(element)}\n");
                locator.Element = $"{element.Name}-{elementAttributes}";
                Console.WriteLine($"Element: {element.Name}-{elementAttributes}\n");
                _elementLocators.Locators.Add(locator);
            }


            ////FOR TESTING
            var elementLocator = new Locator();
            elementLocator.GetAllLocatorsXpath(_elementLocators.Locators.LastOrDefault().Xpath, _elementLocators);

            Console.ReadLine();
        }

        private static ElementLocator GetCoreAttributes(HtmlAttribute xAttribute, ElementLocator locator)
        {

            if (xAttribute.Name == "class")
            {
                locator.XpathAttributes.ClassAttribute = new Attrib
                {
                    Name = xAttribute.Name.ToString(),
                    Value = xAttribute.Value
                };
                Console.WriteLine($"Class= {xAttribute.Value}");
            }

            if (xAttribute.Name == "id")
            {
                locator.XpathAttributes.Id = new Attrib
                {
                    Name = xAttribute.Name.ToString(),
                    Value = xAttribute.Value
                };
                Console.WriteLine($"Id = {xAttribute.Value}");
            }

            if (xAttribute.Name == "name")
            {
                locator.XpathAttributes.Name = new Attrib
                {
                    Name = xAttribute.Name.ToString(),
                    Value = xAttribute.Value
                };
                Console.WriteLine($"Name = {xAttribute.Value}");
            }

            if (xAttribute.Name == "link")
            {
                locator.XpathAttributes.Link = new Attrib
                {
                    Name = xAttribute.Name.ToString(),
                    Value = xAttribute.Value
                };
                Console.WriteLine($"Link Text = {xAttribute.Value}");
            }

            return locator;
        }

        private static string GenerateXpath(HtmlNode element)
        {
            var finalXpath = "";
            var xpathList = new List<string>();
            var ancestorPath = element.AncestorsAndSelf();

            foreach (var ancestor in ancestorPath)
            {
                var index = GetIndex(ancestor);
                if (index > 0)
                {
                    xpathList.Add($"/{ancestor.Name}[{index}]");
                }
                else
                {
                    xpathList.Add($"/{ancestor.Name}");
                }
            }
            
            xpathList.Reverse();
            xpathList.Remove("/#document");
            xpathList.ForEach(x => finalXpath += x);
            return finalXpath;
        }

        private static int GetIndex(HtmlNode element)
        {
            var i = 1;

            if (element.ParentNode == null)
            {
                return -1;
            }

            if (element.ParentNode.Elements(element.Name).Count() == 1)
            {
                // Element has no sibling elements
                return -2;
            }

            if (element.ParentNode != null)
                foreach (var sibling in element.ParentNode.Elements(element.Name))
                {
                    if (sibling == element)
                    {
                        return i;
                    }
                    i++;
                }
            throw new Exception("Index Not Found Error");
        }
    }
}