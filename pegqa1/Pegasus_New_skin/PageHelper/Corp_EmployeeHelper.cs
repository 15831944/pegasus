using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using PegasusTests.Locators;
using PegasusTests.PageHelper.Comm;
using System;

namespace PegasusTests.PageHelper
{
    public class Corp_EmployeeHelper : DriverHelper
    {
        public LocatorReader locatorReader;

        public Corp_EmployeeHelper(IWebDriver idriver)
            : base(idriver)
        {
            locatorReader = new LocatorReader("Corp_Employee.xml");
        }

        // Type into given xml node
        public void TypeText(string Field, string text)
        {
            var locator = locatorReader.ReadLocator(Field);
            WaitForElementPresent(locator, 20);
            SendKeys(locator, text);
        }

        // Select element by text
        public void SelectByText(string xmlNode, string text)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            SelectDropDownByText(locator, text);
        }

        // Verify element present.
        public void verifyElementPresent(string field)
        {
            var locator = locatorReader.ReadLocator(field);
            WaitForElementPresent(locator, 30);
            Assert.IsTrue(IsElementPresent(locator));
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

        // Verify displayed column value name
        public void VerifyColumn(string text)
        {
            var Col = GetTextByXpath("//*[@id='display_cols']");
            System.Console.WriteLine(Col);
            WaitForWorkAround(3000);
            Assert.IsTrue(Col.Contains(text));
            WaitForWorkAround(3000);
        }

        // Verify email not present on page
        public void EmailRemoved(string text)
        {
            var Email = "//input[@value="+text+"]";
            Assert.IsFalse(IsElementPresent(Email));
        }

        // Verify phone not present on page
        public void PhoneRemoved(string text)
        {
            var phone = "//input[@value=" + text + "]";
            Assert.IsFalse(IsElementPresent(phone));
        }

        //Element Present
        public void ElementPresent(string xmlNode)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            IsElementPresent(locator);
        }

        //Click on element
        public void ClickElement(string xmlNode)
        {
            WaitForWorkAround(3000);
            var locator = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(locator, 50);
            Click(locator);
            WaitForWorkAround(3000);
        }

        //Click on element via javaScript
        public void ClickJS(string xmlNode)
        {
            WaitForWorkAround(3000);
            var locator = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(locator, 50);
            ClickViaJavaScript(locator);
            WaitForWorkAround(3000);
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


        //Verify if the given field is required
        public void VerifyIfRequired(string getlocator)
        {
            var locator = locatorReader.ReadLocator(getlocator);
            string text = GetText(locator);
            Assert.IsTrue(text.ToLower().Contains("require"));
        }
    }
}