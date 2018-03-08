using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using PegasusTests.Locators;
using PegasusTests.PageHelper.Comm;
using System.Collections.Generic;
using System;

namespace PegasusTests.PageHelper
{
    public class CorpResidualIncome_ImportHelper : DriverHelper
    {
        public LocatorReader locatorReader;

        public CorpResidualIncome_ImportHelper(IWebDriver idriver)
            : base(idriver)
        {
            locatorReader = new LocatorReader("CorpResidualIncome_Import.xml");
        }


        // Type into given xml node
        public void TypeText(string Field, string text)
        {
            var locator = locatorReader.ReadLocator(Field);
            WaitForElementPresent(locator, 20);
            SendKeys(locator, text);
        }

        // Verify text of given xml node
        public void VerifyText(string XmlNode, string text)
        {
            var locator = locatorReader.ReadLocator(XmlNode);
            var value = GetText(locator);
            Assert.IsTrue(value.Contains(text));
        }

        // Upload Any File
        public void Upload(string Field, string Filename)
        {
            string locator = locatorReader.ReadLocator(Field);
            GetWebDriver().FindElement(ByLocator(locator)).SendKeys(Filename);
            WaitForWorkAround(3000);

        }

        // Select by text
        public void SelectByText(string xmlNode, string text)
        {
            string locator = locatorReader.ReadLocator(xmlNode);
            SelectDropDownByText(locator, text);
        }


        // Select by value
        public void Select(string xmlNode, string value)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            SelectDropDown(locator, value);
        }

        // Click on given xml node.

        public void ClickElement(string xmlNode)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(locator, 20);
            Click(locator);
        }

        public void ClickOnDisplayed(string xmlNode)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(locator, 20);
            ClickDisplayed(locator);
        }

        public void GetCurrentDate(string txtbox)
        {
            var query = GetWebDriver().FindElement(By.XPath(txtbox));
            var y = DateTime.Now.ToString("MM-dd-yyyy");
            WaitForWorkAround(4000);
            Console.WriteLine("Date is" +y );
            query.SendKeys(y.ToString());
            WaitForWorkAround(4000);
        }




    }
}