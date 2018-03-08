/*
* AdminSetFormatEmailHelper.cs is linked AdminSetFormatEmail.cs and
* AdminSetFormatEmail.xml
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
    public class AdminSetFormatEmailHelper : DriverHelper
    {
        public LocatorReader locatorReader;

        public AdminSetFormatEmailHelper(IWebDriver idriver)
            : base(idriver)
        {
            locatorReader = new LocatorReader("AdminSetFormatEmail.xml");
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

        //Verify if the given field is required
        public void VerifyIfFormatEmail(string getlocator)
        {
            //  var locator = locatorReader.ReadLocator(getlocator);
      
            string text = GetAtrributeByLocator("//*[@id='ClientContact0ClientContactContactTitle']", "class");
            Console.WriteLine(text);
            Assert.IsTrue(text.ToLower().Contains("email"));

        }

              

    }
}