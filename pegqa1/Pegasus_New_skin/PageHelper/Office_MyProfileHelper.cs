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
    public class Office_MyProfileHelper : DriverHelper
    {
        public LocatorReader locatorReader;

        public Office_MyProfileHelper(IWebDriver idriver)
            : base(idriver)
        {
            locatorReader = new LocatorReader("Office_MyProfile.xml");
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

        // Click element via xml node.
        public void ClickElement(string xmlNode)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(locator, 20);
            Click(locator);
        }

        // Click the element via Java script

        public void ClickJS(string xmlNode)
        {
            var loc = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(loc, 20);
            ClickViaJavaScript(loc);
        }

        //Upload a file.
        public void Upload(string xmlNode, string file)
        {
            var loc = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(loc ,10);
            UploadFile(loc ,file);
        }

        // verify that element is present.
        public void verifyElementPresent(string field)
        {
            var locator = locatorReader.ReadLocator(field);
            WaitForElementPresent(locator, 30);
            Assert.IsTrue(IsElementPresent(locator));
        }

        // Double click on given node.
        public void ClickMultiple(string field)
        {
            var locator = locatorReader.ReadLocator(field);
            DoubleClick(locator);
        }
      
        // Mouse hover
        internal void MouseHover(string field)
        {
            var locator = locatorReader.ReadLocator(field);
            WaitForElementPresent(locator, 20);            
            MouseOver(locator);
            WaitForWorkAround(5000);
        }

        // Upload an image file.
        public void UploadImage(string field, string path)
        {
            var locator = locatorReader.ReadLocator(field);
            WaitForElementPresent(locator, 30);
            UploadFile(locator, path);
        }

        // Verify element in not present
        public void verifyElementNotPresent(string field)
        {
            var locator = locatorReader.ReadLocator(field);
            WaitForWorkAround(2000);
            Assert.IsFalse(IsElementPresent(locator));
        }

        // Verify EmailID Contains .com
        public void VerifyCom()
        {
            var email = GetAtrributeByXpath("//*[@id='EmailToAddrs']","value");
            WaitForWorkAround(3000);
            Assert.IsTrue(email.Contains(".com"));
        }

        // Select element by text.
        public void SelectByText(string xmlNode, string text)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(locator, 20);
            SelectDropDownByText(locator, text);
        }
        public String GetCurrentUrl()
        {
            return GetUrl();
        }
    }
}