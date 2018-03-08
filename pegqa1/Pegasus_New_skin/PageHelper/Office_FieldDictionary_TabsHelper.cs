using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using PegasusTests.Locators;
using PegasusTests.PageHelper.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PegasusTests.PageHelper
{
    public class Office_FieldDictionary_TabsHelper : DriverHelper
    {
        public LocatorReader locatorReader;

        public Office_FieldDictionary_TabsHelper(IWebDriver idriver)
            : base(idriver)
        {
            locatorReader = new LocatorReader("Office_FieldDictionary_Tabs.xml");
        }


        // Type into given xml node
        public void TypeText(string Field, string text)
        {
            String locator = locatorReader.ReadLocator(Field);
            WaitForElementPresent(locator, 20);
            SendKeys(locator, text);
        }

        // Verify text of given xml node
        public void VerifyText(string XmlNode, string text)
        {
            String locator = locatorReader.ReadLocator(XmlNode);
            String value = GetText(locator);
            Assert.IsTrue(value.Contains(text));
        }

        // Select by value
        public void Select(string xmlNode, string value)
        {
            String locator = locatorReader.ReadLocator(xmlNode);
            WaitForElementVisible(locator, 30);
            SelectDropDown(locator, value);
        }

        // Click on the displayed element
        public void ClickOnDisplayed(string xml)
        {
            var loc = locatorReader.ReadLocator(xml);
            WaitForElementPresent(loc, 20);
            ClickDisplayed(loc);

        }

        // Select by text
        public void SelectByText(string xmlNode, string text)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            SelectDropDownByText(locator, text);
        }
      
        //Delete All client tabs
        public void DeleteTabsClient()
        {
            var l =  "//ol[@id='sortable']/li[20]/div/span[1]/a[3]/i";
            var cnt = XpathCount("//ol[@id='sortable']/li");
            Console.WriteLine("Xath Count is " + cnt);
            for (int i = 25; i < cnt; i++)
            {
                var ll = "//ol[@id='sortable']/li[" + i + "]/div/span[1]/a[3]/i";
                Console.WriteLine("loc path is " + ll);
                Click(ll);
                AcceptAlert();
                WaitForWorkAround(3000);
            }
        }

        //Delete lead tabs
        public void DeltetabLead()
        {
            var l = "//ol[@id='sortable']/li[20]/div/span[1]/a[3]";
            var cnt = XpathCount("//ol[@id='sortable']/li");
            Console.WriteLine("Xath Count is " + cnt);
            for (int i = 15; i < cnt; i++)
            {
                var ll = "//ol[@id='sortable']/li[" + i + "]/div/span[1]/a[3]";
                Console.WriteLine("loc path is " + ll);
                Click(ll);
                AcceptAlert();
                WaitForWorkAround(3000);
            }
        }

        // Click Element via xml node.
        public void ClickElement(string xmlNode)
        {
            String locator = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(locator, 20);
            Click(locator);
        }

        // Select element by text.
        public void Selectbytext(string xmlNode, string text)
        {
            String locator = locatorReader.ReadLocator(xmlNode);
            WaitForElementVisible(locator, 30);
            SelectDropDownByText(locator, text);
        }
    }
}
