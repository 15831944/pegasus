/*
* AdminValidateDisplayNumesHelper.cs is linked to AdminValidateDisplayNumes.cs and
* AdminValidateDisplayNumes.xml
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
    public class AdminValidateDisplayNamesHelper : DriverHelper
    {
        public LocatorReader locatorReader;

        public AdminValidateDisplayNamesHelper (IWebDriver idriver)
            : base(idriver)
        {
            locatorReader = new LocatorReader("AdminValidateDisplayNames.xml");
        }

        //Click to given xml node
        public void ClickElement(string XmlNode)
        {
            String locator = locatorReader.ReadLocator(XmlNode);
            Click(locator);
        }

        // Click forcefully on any element.
        public void ClickForce(string xmlnode)
        {
            var loc = locatorReader.ReadLocator(xmlnode);
            WaitForElementPresent(loc ,10);
            ClickViaJavaScript(xmlnode);
        }

        internal void MouseHover(string field)
        {
            var locator = locatorReader.ReadLocator(field);
            WaitForElementPresent(locator, 05);
            MouseOver(locator);
        }
        public bool ElementVisible(string field)
        {
            var locator = locatorReader.ReadLocator(field);
            return IsElementPresent(locator);
        }

        // Type into given xml node
        public void TypeText(string Field, string text)
        {
            var locator = locatorReader.ReadLocator(Field);
            WaitForElementPresent(locator, 20);
            WaitForElementEnabled(locator, 20);
            SendKeys(locator, text);
        }

        // Select the text by drop down
        public void SelectByText(string xmlNode, string text)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            SelectDropDownByText(locator, text);
        }

        public void VerifyText(string XmlNode, string text)
        {
            var locator = locatorReader.ReadLocator(XmlNode);
            var value = GetText(locator);
            Assert.IsTrue(value.Contains(text));
        }
    }
}