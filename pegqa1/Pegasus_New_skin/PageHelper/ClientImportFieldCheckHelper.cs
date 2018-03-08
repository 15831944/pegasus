using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using PegasusTests.Locators;
using PegasusTests.PageHelper.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace PegasusTests.PageHelper
{
    public class ClientImportFieldCheckHelper : DriverHelper
    {
        public LocatorReader locatorReader;

        public ClientImportFieldCheckHelper(IWebDriver idriver)
            : base(idriver)
        {
            locatorReader = new LocatorReader("ClientImportFieldCheck.xml");
        }

        // Type into given xml node
        public void TypeText(string Field, string text)
        {
            String locator = locatorReader.ReadLocator(Field);
            WaitForElementPresent(locator, 20);
            WaitForElementVisible(locator, 20);
            SendKeys(locator, text);
        }

        // Verify text of given xml node
        public void VerifyText(string XmlNode, string text)
        {
            String locator = locatorReader.ReadLocator(XmlNode);
            String value = getInputText(locator);
            Assert.IsTrue(value == text);
        }
        //vertiry value of the given xml node
        public void VerifyValue(string XmlNode, string value)
        {
            String locator = locatorReader.ReadLocator(XmlNode);
            String cuValue = GetValue(locator);
            Assert.IsTrue(cuValue == value);
        }

        // to verify entered email on client page.
        public void VerifyEmail(string email, string text)
        {
            var loc = "//span[text()='" + email + "']";
            string el = GetText(loc);
            Assert.IsTrue(el.Contains(text));
        }

        //Select by value
        public void Select(string xmlNode, string value)
        {
            String locator = locatorReader.ReadLocator(xmlNode);
            SelectDropDown(locator, value);
        }

        //Click Via Enter key
        public void PressEnter(string xmlNode)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(locator, 20);
            ClickViaEnter(locator);
        }

        //Delete All Clients
        public void DelteAllClients()
        {
            var cnt = XpathCount("//table[@id='list1']/tbody/tr");
            Console.WriteLine("Xath Count is " + cnt);
            for (int i = 2; i < cnt; i++)
            {
                var ll = "//table[@id='list1']/tbody/tr[" + i + "]/td[7]/div/a[1]";
                Console.WriteLine("loc path is " + ll);
                Click(ll);
                AcceptAlert();
                WaitForWorkAround(3000);
            }
        }

        //Select by text
        public void SelectByText(string xmlNode, string text)
        {
            String locator = locatorReader.ReadLocator(xmlNode);
            SelectDropDownByText(locator, text);
        }

        //Click Dispalyed
        public void ClickOnDisplayed(string xmlNode)
        {
            String locator = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(locator, 20);
            ClickDisplayed(locator);
        }


        // Scroll to element
        public void scrollToElement(string field)
        {
            var locator = locatorReader.ReadLocator(field);
            WaitForElementPresent(locator, 30);
            WaitForWorkAround(2000);
            ScrollDown(locator);
        }

        // Verify Added Column
        public void verifyColumnAdded(string xmlNode)
        {
            String locator = locatorReader.ReadLocator(xmlNode);
            WaitForWorkAround(2000);
            Assert.IsTrue(IsElementPresent(locator));
        }

        // FOr exportin file
        public void ExportAs(string type)
        {
            GetWebDriver().Navigate().Refresh();
            String locator = "//div[@title='Export']/button";
            WaitForElementVisible(locator, 30);
            Click(locator);
            string newlocator = "";
            if (type == "CSV")
            {
                newlocator = "//*[@id='export_csv']";
                WaitForElementVisible(newlocator, 30);
            }
            else
            {
                newlocator = "//*[@id='export_excel']";
                WaitForElementVisible(newlocator, 30);
            }
            Click(newlocator);
        }


        // Verify partner association.
        public bool VerifyPartnerAssociation(string xmlNode)
        {
            String locator = locatorReader.ReadLocator(xmlNode);
            WaitForWorkAround(3000);
            bool result = IsElementPresent(locator);
            if (result)
            {
                WaitForElementVisible(locator, 30);
            }
            return result;
        }
        // To verify location
        public void verifyLocationSaved(string field, int rand)
        {
            var locator = locatorReader.ReadLocator(field);
            WaitForElementPresent(locator, 30);
            string value = GetAtrributeByLocator(locator, "value");
            Assert.IsTrue(value.Contains(rand.ToString()));
        }

        // Mouse hover
        internal void MouseHover(string field)
        {
            var locator = locatorReader.ReadLocator(field);
            WaitForElementPresent(locator, 20);
            WaitForWorkAround(2000);
            MouseOver(locator);
        }

        //Click Element

        public void ClickElement(string xmlNode)
        {
            String locator = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(locator, 20);
            WaitForElementVisible(locator, 20);
            Click(locator);
        }


        // Upload a file.
        public void Upload(string Field, string Filename)
        {
            String locator = locatorReader.ReadLocator(Field);
            GetWebDriver().FindElement(ByLocator(locator)).SendKeys(Filename);
            WaitForWorkAround(3000);

        }

        // Verify Not present element
        public void verifyElementNotPresent(string field)
        {
            var locator = locatorReader.ReadLocator(field);
            WaitForWorkAround(2000);
            Assert.IsFalse(IsElementPresent(locator));
        }

        // Verify element Visible
        public void verifyElementVisible(string field)
        {
            var locator = locatorReader.ReadLocator(field);
            WaitForElementPresent(locator, 30);
            WaitForElementVisible(locator, 30);
            Assert.IsTrue(IsElementVisible(locator));
        }

        // Verify present element
        public void verifyElementPresent(string field)
        {
            var locator = locatorReader.ReadLocator(field);
            WaitForElementPresent(locator, 30);
            Assert.IsTrue(IsElementPresent(locator));
        }

    }
}

