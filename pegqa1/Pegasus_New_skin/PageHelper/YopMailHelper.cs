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
    public class YopMailHelper : DriverHelper
    {
        public LocatorReader locatorReader;

        public YopMailHelper(IWebDriver idriver)
            : base(idriver)
        {
            locatorReader = new LocatorReader("YopMail.xml");
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

        // Switch between frames. 
        public void switchFrame(string frameid)
        {
            String frame = "//iframe[@id='" + frameid + "']";
            WaitForElementPresent(frame, 30);
            GetWebDriver().SwitchTo().Frame(GetWebDriver().FindElement(ByLocator(frame)));
        }

        // Redirect fron out of frame.
        public void outFrame()
        {
            GetWebDriver().SwitchTo().DefaultContent();
        }
    }
}