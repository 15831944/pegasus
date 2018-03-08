using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using PegasusTests.Locators;
using PegasusTests.PageHelper.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace PegasusTests.PageHelper
{
    public class TicketAddDocumentHelper : DriverHelper
    {
        public LocatorReader locatorReader;

        public TicketAddDocumentHelper(IWebDriver idriver)
            : base(idriver)
        {
            locatorReader = new LocatorReader("TicketAddDocument.xml");
        }

        // Type into given xml node
        public void TypeText(string Field, string text)
        {
            String locator = locatorReader.ReadLocator(Field);
            WaitForElementPresent(locator, 20);
            WaitForElementVisible(locator, 20);
            SendKeys(locator, text);
        }

        // Verify text of given xml node
        public void VerifyText(string XmlNode, string text)
        {
            String locator = locatorReader.ReadLocator(XmlNode);
            String value = getInputText(locator);
            Assert.IsTrue(value == text);
        }
        

        //Select by value
        public void Select(string xmlNode, string value)
        {
            String locator = locatorReader.ReadLocator(xmlNode);
            SelectDropDown(locator, value);
        }




        //Select by text
        public void SelectByText(string xmlNode, string text)
        {
            String locator = locatorReader.ReadLocator(xmlNode);
            SelectDropDownByText(locator, text);
        }


        //Click Element

        public void ClickElement(string xmlNode)
        {
            String locator = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(locator, 20);
            WaitForElementVisible(locator, 20);
            Click(locator);
        }


        // Upload a file.
        public void Upload(string Field, string Filename)
        {
            String locator = locatorReader.ReadLocator(Field);
            GetWebDriver().FindElement(ByLocator(locator)).SendKeys(Filename);
            WaitForWorkAround(3000);

        }


    }
}

