using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using PegasusTests.Locators;
using PegasusTests.PageHelper.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PegasusTests.PageHelper
{
    public class PDFTemplate_ImportWizardHelper : DriverHelper
    {
        public LocatorReader locatorReader;

        public PDFTemplate_ImportWizardHelper(IWebDriver idriver)
            : base(idriver)
        {
            locatorReader = new LocatorReader("PDFTemplate_ImportWizard.xml");
        }

        // Type into given xml node
        public void TypeText(string Field, string text)
        {
            String locator = locatorReader.ReadLocator(Field);
            WaitForElementPresent(locator, 20);
            SendKeys(locator, text);
        }

        // Verify text of given xml node
        public void VerifyText(string XmlNode, string text)
        {
            String locator = locatorReader.ReadLocator(XmlNode);
            String value = GetText(locator);
            Assert.IsTrue(value.Contains(text));
        }

        // Select by value
        public void Select(string xmlNode, string value)
        {
            String locator = locatorReader.ReadLocator(xmlNode);
            SelectDropDown(locator, value);
        }

        // Select element by text.
        public void SelectByText(string xmlNode,string text)
        {
            string locator = locatorReader.ReadLocator(xmlNode);
            SelectDropDownByText(locator,text);
            WaitForWorkAround(1000);
        }

        // Click on given xml node.
        public void ClickElement(string xmlNode)
        {
            String locator = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(locator, 20);
            WaitForElementVisible(locator, 20);
            Click(locator);
        }

        // Click using java script method.
        public void ClickForce(string xmlNode)
        {
            var loc = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(loc ,10);
            ClickViaJavaScript(loc);
        }

        // Verify typo error in rule office details
        public void TypoError(String text)
        {
            var loc = GetTextByXpath("//*[@id='ImportIndexPopOfficeField']");
            WaitForWorkAround(3000);
            Console.WriteLine(loc);
            WaitForWorkAround(3000);
            Assert.IsTrue(loc.Contains(text));
        }
        // Upload a file.
        public void upload(string Field, string FileName)
        {
            String locator = locatorReader.ReadLocator(Field);
            Console.WriteLine(FileName);
            GetWebDriver().FindElement(ByLocator(locator)).SendKeys(FileName);
            WaitForWorkAround(3000);
        }
    }
}
