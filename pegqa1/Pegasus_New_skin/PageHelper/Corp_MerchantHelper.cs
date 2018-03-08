using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using PegasusTests.Locators;
using PegasusTests.PageHelper.Comm;
using System;

namespace PegasusTests.PageHelper
{
    public class Corp_MerchantHelper : DriverHelper
    {
        public LocatorReader locatorReader;

        public Corp_MerchantHelper(IWebDriver idriver)
            : base(idriver)
        {
            locatorReader = new LocatorReader("Corp_Merchant.xml");
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
            Console.WriteLine(value);
            Assert.IsTrue(value.Contains(text));
        }

        // Scroll to element
        public void scrollToElement(string field)
        {
            var locator = locatorReader.ReadLocator(field);
            WaitForElementPresent(locator, 30);
            WaitForWorkAround(2000);
            ScrollDown(locator);
        }

        // Select by value
        public void Select(string xmlNode, string value)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(locator, 20);
            WaitForElementVisible(locator, 30);
            SelectDropDown(locator, value);
        }

        //Verify the No of showing page
        public void ShowResult(int Value)
        {
            int el4 = XpathCount("//table[@id='list1']//tbody//tr");
            Console.WriteLine("el4 is  " + el4);
            if (Value < el4)
            {
                Assert.IsTrue(Value < el4);
            }
            else
            {
                Assert.IsFalse(Value < el4);
            }
        }

        // Click element

        public void ClickElement(string xmlNode)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(locator, 20);
            Click(locator);
        }

        // Click the element via java Script

            public void ClickJS(string xmlNode)
        {
            var loc = locatorReader.ReadLocator(xmlNode);
            ClickViaJavaScript(loc);
        }

        // Click Via Enter key
        public void PressEnter(string xmlNode)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(locator, 20);
            ClickViaEnter(locator);
        }

        // Select element via text.
        public void SelectByText(string xmlNode, string text)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            SelectDropDownByText(locator, text);
        }      
    }
}