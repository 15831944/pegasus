/*
* AdminSetFormatTAXIDHelper.cs works in conjunction with 
* AdminSetFormatTAXID.cs and AdminSetFormatTAXID.xml
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
    public class AdminSetFormatTAXIDHelper : DriverHelper
    {
        public LocatorReader locatorReader;

        public AdminSetFormatTAXIDHelper(IWebDriver idriver)
            : base(idriver)
        {
            locatorReader = new LocatorReader("AdminSetFormatTAXID.xml");
        }

        // Select element by text
        public void SelectByText(string xmlnode ,string text)
        {
            var loc = locatorReader.ReadLocator(xmlnode);
            WaitForElementPresent(loc ,10);
            SelectDropDownByText(loc, text);
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

        // Type into given xml node
        public void TypeText(string Field, string text)
        {
            var locator = locatorReader.ReadLocator(Field);
            WaitForElementPresent(locator, 20);
            WaitForElementEnabled(locator, 20);
            SendKeys(locator, text);
        }

        public void VerifyText(string XmlNode, string text)
        {
            var locator = locatorReader.ReadLocator(XmlNode);
            var value = GetText(locator);
            Assert.IsTrue(value.Contains(text));
        }

        //Verify if the given field is a tax ID formated field

        public void VerifyIfFormatTaxID()
        {
            // Hardcoded to title field (Issues occured when trying to pass an xml node)
            string text = GetAtrributeByLocator("//*[@id='ClientContact0ClientContactContactTitle']", "class");
            WaitForWorkAround(3000);
            Console.WriteLine(text);
            Assert.IsTrue(text.ToLower().Contains("tax"));

        }

    }
}

