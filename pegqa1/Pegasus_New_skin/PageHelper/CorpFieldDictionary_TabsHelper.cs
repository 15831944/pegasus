using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using PegasusTests.Locators;
using PegasusTests.PageHelper.Comm;
using System.Collections.Generic;
using System;

namespace PegasusTests.PageHelper
{
    public class CorpFieldDictionary_TabsHelper : DriverHelper
    {
        public LocatorReader locatorReader;

        public CorpFieldDictionary_TabsHelper(IWebDriver idriver)
            : base(idriver)
        {
            locatorReader = new LocatorReader("CorpFieldDictionary_Tabs.xml");
        }

        // Type into given xml node
        public void TypeText(string Field, string text)
        {
            var locator = locatorReader.ReadLocator(Field);
            WaitForElementPresent(locator, 20);
            SendKeys(locator, text);
        }

        // Click on first displayed element.
        public void ClickOnDisplayed(string xml)
        {
            var loc = locatorReader.ReadLocator(xml);
            WaitForElementPresent(loc, 20);
            ClickDisplayed(loc);
        }

        // Verify text of given xml node
        public void VerifyText(string XmlNode, string text)
        {
            var locator = locatorReader.ReadLocator(XmlNode);
            var value = GetText(locator);
            Assert.IsTrue(value.Contains(text));
        }

        // Select by value
        public void Select(string xmlNode, string value)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            SelectDropDown(locator, value);
        }

        //Delete tabs corp.
        public void DeletesTabsCorp()
        {
            var l = "//ol[@id='sortable']/li/div/span[1]/a[3]/i";
            var cnt = XpathCount("//ol[@id='sortable']/li");
            Console.WriteLine("Xath Count is " + cnt);
            for (int i = 20; i < cnt; i++)
            {
                var ll = "//ol[@id='sortable']/li[" + i + "]/div/span[1]/a[3]/i";
                Console.WriteLine("loc path is " + ll);
                Click(ll);
                AcceptAlert();
                WaitForWorkAround(3000);
            }
        }

        // Click on xml node.

        public void ClickElement(string xmlNode)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(locator, 20);
            Click(locator);
        }
    }
}