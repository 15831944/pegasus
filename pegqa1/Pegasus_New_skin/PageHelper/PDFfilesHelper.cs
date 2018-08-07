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
    public class PDFfilesHelper : DriverHelper
    {
        public LocatorReader locatorReader;

        public PDFfilesHelper(IWebDriver idriver)
            : base(idriver)
        {
            //locatorReader = new LocatorReader("PDFTemplate_PDFTemplate.xml");
            locatorReader = new LocatorReader("PDFfields.xml");
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

        //Delete All PDFs
        public void DeletePDFTemplates()
        {
            var l = "//table[@id='list1']/tbody/tr[2]/td[2]/a[1]/i";
            var cnt = XpathCount("//table[@id='list1']/tbody/tr");
            Console.WriteLine("Xath Count is " + cnt);
            for (int i = 15; i < cnt; i++)
            {
                var ll = "//table[@id='list1']/tbody/tr[" + i + "]/td[2]/a[1]/i";
                Console.WriteLine("loc path is " + ll);
                Click(ll);
                AcceptAlert();
                WaitForWorkAround(3000);
            }
        }

        //Delete All Calls
        public void DeletePDFCategory()
        {
            var l = "//ul[@id='sortable']/li[1]/div/span/a[3]";
            var cnt = XpathCount("//ul[@id='sortable']/li");
            Console.WriteLine("Xath Count is " + cnt);
            for (int i = 18; i < cnt; i++)
            {
                var ll = "//ul[@id='sortable']/li[" + i + "]/div/span/a[3]";
                Console.WriteLine("loc path is " + ll);
                Click(ll);
                AcceptAlert();
                WaitForWorkAround(3000);
                SelectDropDown("//*[@id='CategoryReplaceCategory']", "53");
                ClickDisplayed("//a[@title='Save']");
            }
        }

        // Click on given xml node.
        public void ClickElement(string xmlNode)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(locator, 20);
            Click(locator);
        }

        //Click element via java script
        public void ClickJs(string xmlNode)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(locator, 20);
            ClickViaJavaScript(locator);
        }

        // Select element by text.
        public void SelectByText(string xmlNode, string text)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(locator, 20);
            SelectDropDownByText(locator, text);
        }

        // Remove given text.
        public void removeText(string field)
        {
            var locator = locatorReader.ReadLocator(field);
            WaitForWorkAround(2000);
            WaitForElementVisible(locator, 30);
            GetWebDriver().FindElement(ByLocator(locator)).Clear();
        }

        // Verify that element is present.
        public void verifyElementPresent(string field)
        {
            var locator = locatorReader.ReadLocator(field);
            WaitForElementPresent(locator, 30);
            Assert.IsTrue(IsElementPresent(locator));
        }
    }
}