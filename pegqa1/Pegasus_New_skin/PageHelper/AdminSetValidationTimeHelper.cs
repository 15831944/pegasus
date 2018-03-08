/*
* AdminSetValidationTimeHelper.cs is linked AdminSetValidationTime.cs and
* AdminSetValidationTime.xml
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
    public class AdminSetValidationTimeHelper : DriverHelper
    {
        public LocatorReader locatorReader;

        public AdminSetValidationTimeHelper(IWebDriver idriver)
            : base(idriver)
        {
            locatorReader = new LocatorReader("AdminSetValidationTime.xml");
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

        public void VerifyIfNumeric(string getlocator)
        {
            //  var locator = locatorReader.ReadLocator(getlocator);
            string text = GetAtrributeByLocator("//*[@id='ClientDetailTaxID']", "class");
            Console.WriteLine(text);
            Assert.IsTrue(text.ToLower().Contains(""));

        }

        // Select form dropdown by text
        public void SelectByText(string xmlnode, string text)
        {
            var loc = locatorReader.ReadLocator(xmlnode);
            WaitForElementPresent(loc, 10);
            SelectDropDownByText(loc, text);
        }

        // Click the element via JavaScript
        public void ClickJs(string XmlNode)
        {
            String locator = locatorReader.ReadLocator(XmlNode);
            WaitForElementPresent(locator, 10);
            ClickViaJavaScript(locator);
        }

        // Click on element using javascript
        public void ClickForce(string xmlNode)
        {
            var loc = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(loc, 10);
            ClickViaJavaScript(loc);

        }

        //Check checkbox 
        public void checkAndClick(string XmlNode)
        {
            String loc = locatorReader.ReadLocator(XmlNode);
            IWebElement el1 = GetWebDriver().FindElement(By.XPath(loc));
            if (el1.Selected)
            {
                //Do noting
            }
            else
            {
                Click(loc);
            }
        }

    }
}