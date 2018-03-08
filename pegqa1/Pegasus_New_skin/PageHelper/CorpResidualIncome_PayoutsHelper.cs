using System;
using LinqToExcel;
using System.Linq;
using LinqToExcel.Query;
using LinqToExcel.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using PegasusTests.Locators;
using PegasusTests.PageHelper.Comm;
using System.IO;

namespace PegasusTests.PageHelper
{
    public class CorpResidualIncome_PayoutsHelper : DriverHelper
    {
        public LocatorReader locatorReader;

        public CorpResidualIncome_PayoutsHelper(IWebDriver idriver)
            : base(idriver)
        {
            locatorReader = new LocatorReader("CorpResidualIncome_Payouts.xml");
        }

        // Type into given xml node
        public void TypeText(string Field, string text)
        {
            var locator = locatorReader.ReadLocator(Field);
            WaitForElementPresent(locator, 20);
            WaitForElementEnabled(locator, 20);
            SendKeys(locator, text);
        }

        // Verify text of given xml node
        public void VerifyText(string XmlNode, string text)
        {
            var locator = locatorReader.ReadLocator(XmlNode);
            var value = GetText(locator);
            Console.WriteLine(value);
            Assert.IsTrue(value.Contains(text));
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

        // Select element by text
        public void SelectByText(string xmlNode, string text)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(locator, 20);
            SelectDropDownByText(locator, text);
        }

        // Check that file is not blank.
        public void FileIsNotBlank(string filepath)
        {
            FileInfo file = new FileInfo(filepath);
            if (filepath.Substring(filepath.LastIndexOf('.') + 1) == "xls")
            {
                long size = file.Length;
                Console.WriteLine("Size  = " + file.Length);
                Assert.IsTrue(size > 3);

            }
        }
    }
}