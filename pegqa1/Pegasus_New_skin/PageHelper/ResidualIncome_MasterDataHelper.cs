using System;
using System.Linq;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using LinqToExcel;
using System.IO;
using PegasusTests.PageHelper.Comm;
using PegasusTests.Locators;

namespace PegasusTests.PageHelper
{
    public class ResidualIncome_MasterDataHelper : DriverHelper
    {
        public LocatorReader locatorReader;

        public ResidualIncome_MasterDataHelper(IWebDriver idriver)
            : base(idriver)
        {
            locatorReader = new LocatorReader("ResidualIncome_MasterData.xml");
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

        // Verify given text via value.
        public void VerifyValue(string Field, string Text)
        {
            var locator = locatorReader.ReadLocator(Field);
            var value = GetValue(locator);
            Assert.IsTrue(value.Contains(Text));
        }

        // Select by value
        public void Select(string xmlNode, string value)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            SelectDropDown(locator, value);
        }

        // Click on the given xml node.
        public void ClickElement(string xmlNode)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(locator, 20);
            Click(locator);
        }

        // Select element by text.
        public void SelectByText(string xmlNode, string text)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            SelectDropDownByText(locator, text);
        }

        // Clean Residual Adjustments
        public void CleanAdjustment()
        {
            var count = XpathCount("//table[@id='list1']/tbody/tr");
            Console.WriteLine("Count is" + count);
            for (int i = 4; i < count; i++)
            {
                var del = "//table[@id='list1']/tbody/tr[" + i + "]/td[2]/a/i";
                WaitForElementPresent(del, 10);
                Click(del);
                AcceptAlert();
                WaitForWorkAround(5000);
            }
        }
    }
}