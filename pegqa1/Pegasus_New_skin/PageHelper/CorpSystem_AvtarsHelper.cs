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
    public class CorpSystem_AvtarsHelper : DriverHelper
    {
        public LocatorReader locatorReader;

        public CorpSystem_AvtarsHelper(IWebDriver idriver)
            : base(idriver)
        {
            locatorReader = new LocatorReader("CorpSystem_Avtars.xml");
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
            String value = GetText(locator);
            Assert.IsTrue(value.Contains(text));
        }

        // Verify Availability of avtar
        public bool verifyAvatarAvailable(string text)
        {
            WaitForWorkAround(5000);
            bool result = GetWebDriver().PageSource.Contains(text);

            return result;

        }


        // Select by value
        public void Select(string xmlNode, string value)
        {
            String locator = locatorReader.ReadLocator(xmlNode);
            SelectDropDown(locator, value);
        }

        // Select by text
        public void SelectByText(string xmlNode, string text)
        {
            String locator = locatorReader.ReadLocator(xmlNode);
            SelectDropDownByText(locator, text);
        }


        // Click on given xml node.

        public void ClickElement(string xmlNode)
        {
            String locator = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(locator, 20);
            WaitForElementVisible(locator, 20);
            Click(locator);
        }

        // Click the element via Java script

        public void ClickJS(string xmlNode)
        {
            String locator = locatorReader.ReadLocator(xmlNode);
            ClickViaJavaScript(locator);
        }

        }
}
