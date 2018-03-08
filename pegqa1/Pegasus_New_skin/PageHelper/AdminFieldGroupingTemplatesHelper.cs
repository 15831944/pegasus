/* Documented by Khalil Shakir
* 
* This helper method is used in conjunction with AdminCheckFieldGroupingTemplates.cs
* and connnects with the AdminFieldFieldGroupingTemplates.xml file.
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
    public class AdminFieldGroupingTemplatesHelper : DriverHelper
    {
        public LocatorReader locatorReader;

        public AdminFieldGroupingTemplatesHelper(IWebDriver idriver) : base(idriver)
        {
            locatorReader = new LocatorReader("AdminFieldGroupingTemplates.xml");
        }

        //Click to given xml node
        public void ClickElement(string XmlNode)
        {
            String locator = locatorReader.ReadLocator(XmlNode);
            Click(locator);
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

        // click the element via java script
        public void ClickJs(string XmlNode)
        {
            String locator = locatorReader.ReadLocator(XmlNode);
            ClickViaJavaScript(locator);
        }


        // Select by value
        public void SelectText(string xmlNode, string value)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            SelectDropDownByText(locator, value);
        }
                
        // Type into given xml node
        public void TypeText(string Field, string text)
        {
            var locator = locatorReader.ReadLocator(Field);
            WaitForElementPresent(locator, 20);
            SendKeys(locator, text);
        }

        public void AlertOK()
        {
            AcceptAlert();
        }
    }
}
