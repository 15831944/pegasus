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
using OpenQA.Selenium.Support.UI;
using System.Threading;

namespace PegasusTests.PageHelper
{
    public class Agents_PartnerAgentsHelper : DriverHelper
    {
        public LocatorReader locatorReader;

        public Agents_PartnerAgentsHelper(IWebDriver idriver)
            : base(idriver)
        {
            locatorReader = new LocatorReader("Agents_PartnerAgents.xml");
        }


        //Type into given xml node
        public void TypeText(string Field, string text)
        {
            var locator = locatorReader.ReadLocator(Field);
            WaitForElementPresent(locator, 20);
            WaitForElementEnabled(locator, 20);
            SendKeys(locator, text);
        }

        //Verify text of given xml node
        public void VerifyText(string XmlNode, string text)
        {
            var locator = locatorReader.ReadLocator(XmlNode);
            var value = GetText(locator);
            WaitForWorkAround(3000);
            Console.WriteLine(value);
            Assert.IsTrue(value.Contains(text));
            WaitForWorkAround(3000);
        }

        // Click on displayed element
        public void ClickOnDisplayed(string xml)
        {
            var loc = locatorReader.ReadLocator(xml);
            WaitForElementPresent(loc, 20);
            ClickDisplayed(loc);
        }

        // Select by value
        public void Select(string xmlNode, string value)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            SelectDropDown(locator, value);
        }

        // Click on given xml node

        public void ClickElement(string xmlNode)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(locator, 20);
            Click(locator);
        }

        // Verify present element
        public void verifyElementPresent(string field)
        {
            var locator = locatorReader.ReadLocator(field);
            WaitForElementPresent(locator, 30);
            Assert.IsTrue(IsElementPresent(locator));
        }

        // Scroll And click on element
        public void scrollToElement(string field)
        {
            var locator = locatorReader.ReadLocator(field);
            WaitForElementPresent(locator, 30);
            WaitForWorkAround(2000);
            ScrollDown(locator);
        }

        // Verify element is not present 
        public void verifyElementNotPresent(string field)
        {
            var locator = locatorReader.ReadLocator(field);
            WaitForWorkAround(2000);
            Assert.IsFalse(IsElementPresent(locator));
        }
        // Select element by text
        public void SelectByText(string xmlNode, string text)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(locator, 20);
            SelectDropDownByText(locator, text);
        }

        // Click on element using java script method
        public void ClickJava(string xmlNode)
        {
            var loc = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(loc ,10);
            ClickViaJavaScript(loc);
        }


        // Verify selected label is weblink
        public void VerifyLabel(string xmlNode)
        {
            var loc = locatorReader.ReadLocator(xmlNode);
            WaitForWorkAround(3000);
            IsElementPresent(loc);
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

        // Verify Check and click method

        public void CheckAndClick(string xmlNode)
        {
            var loc = locatorReader.ReadLocator(xmlNode);
            if (IsElementPresent(loc))
            {
                Click(loc);
                AcceptAlert();
            }
            else
            {
                // Do nothing
            }
        }

        // Clear value from text box using xml node

        public void ClearText(string xmlNode)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            ClearTextBoxValue(locator);
        }

        //Verify Selected Option
        // Select an element by text.
        public void selectedOption(string xmlNode, string text)
        {
            var loc = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(loc, 10);
            VerifySelectedOption(loc, text);
        }

        public void VerifyFieldValue(string xmlNode, string expctdTxt)
        {
            var loc = locatorReader.ReadLocator(xmlNode);
            verifyFieldText(loc, expctdTxt);
        }
    }
}