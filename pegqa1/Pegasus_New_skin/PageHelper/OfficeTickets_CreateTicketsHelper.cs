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
    public class OfficeTickets_CreateTicketsHelper : DriverHelper
    {
        public LocatorReader locatorReader;

        public OfficeTickets_CreateTicketsHelper(IWebDriver idriver)
            : base(idriver)
        {
            locatorReader = new LocatorReader("OfficeTickets_CreateTickets.xml");
        }

        // Click to given xml node
        public void ClickElement(string XmlNode)
        {
            String locator = locatorReader.ReadLocator(XmlNode);
            Click(locator);
        }

        // Click the element via Java script

        public void ClickJs(string xmlNode)
        {
            string loc = locatorReader.ReadLocator(xmlNode);
            ClickViaJavaScript(loc);

        }

        // Select element by text
        public void SelectByText(string xmlNode, string text)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            SelectDropDownByText(locator, text);
        }

        // Verify element is visible via node..
        public void verifyElementVisible(string field)
        {
            var locator = locatorReader.ReadLocator(field);
            WaitForElementPresent(locator, 30);
            WaitForElementVisible(locator, 30);
            Assert.IsTrue(IsElementVisible(locator));
        }

        // Type into given xml node
        public void TypeText(string XmlNode, string text)
        {
            String locator = locatorReader.ReadLocator(XmlNode);
            SendKeys(locator, text);
        }

        // Verify text of given xml node
        public void VerifyText(string XmlNode, string text)
        {
            String locator = locatorReader.ReadLocator(XmlNode);
            Thread.Sleep(3000);
            String value = GetText(locator);
            Console.WriteLine(text + "\t" + value);
            Assert.IsTrue(value.Contains(text));
        }

        // Select element by value.
        public void Select(string Field, string value)
        {
            var locator = locatorReader.ReadLocator(Field);
            SelectDropDown(locator, value);
        }

        public void VerifySlctdOptn(string xmlNode, string option)
        {
            var loc = locatorReader.ReadLocator(xmlNode);
            VerifySelectedOption(loc, option);
        }
        
    }
}