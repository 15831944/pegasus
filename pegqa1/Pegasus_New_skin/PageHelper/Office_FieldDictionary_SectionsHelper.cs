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
    public class Office_FieldDictionary_SectionsHelper : DriverHelper
    {
        public LocatorReader locatorReader;

        public Office_FieldDictionary_SectionsHelper(IWebDriver idriver)
            : base(idriver)
        {
            locatorReader = new LocatorReader("Office_FieldDictionary_Sections.xml");
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

        // Click on displayed element
        public void ClickOnDisplayed(string xml)
        {
            var loc = locatorReader.ReadLocator(xml);
            WaitForElementPresent(loc, 20);
            ClickDisplayed(loc);
        }

        //Delete all Section Client
        public void DeleteSectionsClient()
        {
            var l = "//ol[@class='class-defined']/li[8]/div/span[5]/a/i";
            var cnt = XpathCount("//ol[@class='class-defined']/li");
            Console.WriteLine("Xath Count is " + cnt);
            for (int i = 15; i < cnt; i++)
            {
                var ll = "//ol[@class='class-defined']/li[" + i + "]/div/span[5]/a/i";
                Console.WriteLine("loc path is " + ll);
                Click(ll);
                AcceptAlert();
                WaitForWorkAround(3000);
            }
        }

        //Delete All lead sections
        public void DelteSectionLead()
        {
            var l = "//ol[@class='class-defined']/li[8]/div/span[5]/a/i";
            var cnt = XpathCount("//ol[@class='class-defined']/li");
            Console.WriteLine("Xath Count is " + cnt);
            for (int i = 15; i < cnt; i++)
            {
                var ll = "//ol[@class='class-defined']/li[" + i + "]/div/span[5]/a/i";
                Console.WriteLine("loc path is " + ll);
                Click(ll);
                AcceptAlert();
                WaitForWorkAround(3000);
            }
        }

        // Delete Created sections
        public void DeleteSection(String text)
        {
            var count = XpathCount("//*[contains(@id, 'list')]//ol/li[contains(@class,'sortable_true parent')]");
            Console.WriteLine("Count is "+count);
            var LocToDel = "//*[contains(@id, 'list')]//ol/li[" + count + "]//a[@class='delete_section']";
            Click(LocToDel);
            WaitForWorkAround(2000);
        }

        // Select element by text.
        public void SelectByText(string xmlNode, string text)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            SelectDropDownByText(locator, text);
        }


        // Select by value
        public void Select(string xmlNode, string value)
        {
            String locator = locatorReader.ReadLocator(xmlNode);
            WaitForElementVisible(locator, 30);
            SelectDropDown(locator, value);
        }

        // Click element via xml node.
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
