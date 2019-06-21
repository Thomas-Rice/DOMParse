using System.Collections.Generic;

namespace DomParser
{
    public class ElementLocators
    {
        public List<ElementLocator> Locators { get; set; }
    }

    public class ElementLocator
    {
        public string Xpath { get; set; }
        public XpathAttributes XpathAttributes { get; set; }
        public string Element { get; set; }
    }

    public class XpathAttributes
    {
        public Attrib ClassAttribute { get; set; }
        public Attrib Id { get; set; }
        public Attrib Name { get; set; }
        public Attrib Link { get; set; }

        public List<Attrib> GetAttributes =>
            new List<Attrib>
            {
                Name, ClassAttribute, Id, Link
            };
    }

    public class Attrib
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
