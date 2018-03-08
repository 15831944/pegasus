/* Documented by Khalil Shakir
* 
* This helper method is used in conjunction with AdminCheckFieldRelationships.cs
* and connnects with the AdminFieldRelationships.xml file.
*
*/

using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using PegasusTests.PageHelper.Comm;
using OpenQA.Selenium.Support.UI;
using PegasusTests.Locators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PegasusTests.PageHelper
{
    public class AdminCheckFieldRelationshipsHelper : DriverHelper
    {
        public LocatorReader locatorReader;

        public AdminCheckFieldRelationshipsHelper(IWebDriver idriver)
            : base(idriver)
        {
            locatorReader = new LocatorReader("AdminFieldRelationships.xml");
        }

        //Click to given xml node
        public void ClickElement(string XmlNode)
        {
            String locator = locatorReader.ReadLocator(XmlNode);
            Click(locator);
        }

        // Select from dropdown by text.
        public void SelectByText(string xmlnode ,string text)
        {
            var loc = locatorReader.ReadLocator(xmlnode);
            WaitForElementPresent(loc ,10);
            SelectDropDownByText(loc ,text);
        }

        internal void MouseHover(string field)
        {
            var locator = locatorReader.ReadLocator(field);
            WaitForElementPresent(locator, 20);
            MouseOver(locator);
        }
        public bool ElementVisible(string field)
        {
            var locator = locatorReader.ReadLocator(field);
            return IsElementPresent(locator);
        }
    }
}
