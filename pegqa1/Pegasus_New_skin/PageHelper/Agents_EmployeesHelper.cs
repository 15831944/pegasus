using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using PegasusTests.Locators;
using PegasusTests.PageHelper.Comm;
using System;

namespace PegasusTests.PageHelper
{
    public class Agents_EmployeesHelper : DriverHelper
    {
        public LocatorReader locatorReader;

        public Agents_EmployeesHelper(IWebDriver idriver)
            : base(idriver)
        {
            locatorReader = new LocatorReader("Agents_Employees.xml");
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
            Assert.IsTrue(value.Contains(text));
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
            WaitForWorkAround(3000);
        }

        //Java script click
        public void clickJS(string xmlNode)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(locator, 20);
            ClickViaJavaScript(locator);
            WaitForWorkAround(3000);
        }

        // Select an element by text.
        public void SelectByText(string xmlNode ,string text)
        {
            var loc = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(loc ,10);
            SelectDropDownByText(loc ,text);
        }

        //Verify Selected Option
        // Select an element by text.
        public void selectedOption(string xmlNode, string text)
        {
            var loc = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(loc, 10);
            VerifySelectedOption(loc, text);
        }

        // Scroll down and click element
        public void scrollToElement(string field)
        {
            var locator = locatorReader.ReadLocator(field);
            WaitForElementPresent(locator, 30);
            WaitForWorkAround(2000);
            ScrollDown(locator);
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

        public void VerifyFieldValue(string xmlNode, string expctdTxt)
        {
            var loc = locatorReader.ReadLocator(xmlNode);
            verifyFieldText(loc, expctdTxt);
        }
    }
}