using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using PegasusTests.Locators;
using PegasusTests.PageHelper.Comm;
using System;

namespace PegasusTests.PageHelper
{
    public class Equipment_VendorsHelper : DriverHelper
    {
        public LocatorReader locatorReader;

        public Equipment_VendorsHelper(IWebDriver idriver)
            : base(idriver)
        {
            locatorReader = new LocatorReader("Equipment_Vendors.xml");
        }

        // Type into given xml node
        public void TypeText(string Field, string text)
        {
            var locator = locatorReader.ReadLocator(Field);
            WaitForElementPresent(locator, 20);
            SendKeys(locator, text);
        }

        // Select element by text.
        public void SelectByText(string xmlNode, string text)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            SelectDropDownByText(locator, text);
        }

        //Select by value
        public void Select(string xmlNode, string value)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            SelectDropDown(locator, value);
        }

        // Scroll to given locator
        public void scrollToElement(string field)
        {
            var locator = locatorReader.ReadLocator(field);
            WaitForElementPresent(locator, 30);
            WaitForWorkAround(2000);
            ScrollDown(locator);
        }

        // Delete All Calls
        public void DeleteVendors()
        {
            var l = "//table[@id='list1']/tbody/tr[2]/td[3]/span[2]/a/i";
            var cnt = XpathCount("//table[@id='list1']/tbody/tr");
            Console.WriteLine("Xath Count is " + cnt);
            for (int i = 20; i < cnt; i++)
            {
                var ll = "//table[@id='list1']/tbody/tr[" + i + "]/td[3]/span[2]/a/i";
                Console.WriteLine("loc path is " + ll);
                Click(ll);
                AcceptAlert();
                WaitForWorkAround(3000);
            }
        }

        // Verify text of given xml node
        public void VerifyText(string XmlNode, string text)
        {
            var locator = locatorReader.ReadLocator(XmlNode);
            var value = GetText(locator);
            Assert.IsTrue(value.Contains(text));
        }

        // Verify value of given xml node
        public void VerifyValue(string XmlNode, string value)
        {
            var locator = locatorReader.ReadLocator(XmlNode);
            var value1 = GetValue(locator);
            Assert.IsTrue(value1.Contains(value));
        }


        // To verify locator is displayed .
        public void verifyElementDisplayed(string field)
        {
            var locator = locatorReader.ReadLocator(field);
            WaitForElementPresent(locator, 20);
            Assert.IsTrue(IsElementVisible(locator));

        }

        // Verify presence of the element.
        public void verifyElementPresent(string field)
        {
            var locator = locatorReader.ReadLocator(field);
            WaitForElementPresent(locator, 30);
            Assert.IsTrue(IsElementPresent(locator));
        }

        //Click on given xml node.
        public void ClickElement(string xmlNode)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(locator, 20);
            Click(locator);
        }

        //Click on given xml node.
        public void ClickJs(string xmlNode)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(locator, 20);
            ClickViaJavaScript(locator);
        }


    }
}