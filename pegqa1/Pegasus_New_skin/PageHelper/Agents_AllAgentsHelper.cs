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
    public class Agents_AllAgentsHelper : DriverHelper
    {
        public LocatorReader locatorReader;

        public Agents_AllAgentsHelper(IWebDriver idriver)
            : base(idriver)
        {
            locatorReader = new LocatorReader("Agents_AllAgents.xml");
        }

        // Click to given xml node
        public void ClickElement(string XmlNode)
        {
            String locator = locatorReader.ReadLocator(XmlNode);
            Click(locator);
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
            String value = GetText(locator);
            Assert.IsTrue(value.Contains(text));
        }

        // Select element by text
        public void SelectByText(string xmlNode ,string text)
        {
            var loc = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(loc ,10);
            SelectDropDownByText(loc ,text);
        }
        // Select element by value
        public void Select(string Field, string value)
    {
        var locator = locatorReader.ReadLocator(Field);
        SelectDropDown(locator, value);

    }
   }
}