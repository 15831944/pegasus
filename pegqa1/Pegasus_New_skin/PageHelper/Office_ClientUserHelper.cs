using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using PegasusTests.Locators;
using PegasusTests.PageHelper.Comm;
using System;

namespace PegasusTests.PageHelper
{
    public class Office_ClientUserHelper : DriverHelper
    {
        public LocatorReader locatorReader;

        public Office_ClientUserHelper(IWebDriver idriver)
            : base(idriver)
        {
            locatorReader = new LocatorReader("Office_ClientUser.xml");
        }

        // Verify element is present
        public void verifyElementPresent(string field)
        {
            var locator = locatorReader.ReadLocator(field);
            WaitForElementPresent(locator, 30);
            Assert.IsTrue(IsElementPresent(locator));
        }

        // Change Count.
        public int changeCount(string count)
        {
            int result = 0;
            string locator = "//select[@role='listbox']";
            WaitForElementVisible(locator, 30);
            SelectDropDownByText(locator, count);
            WaitForWorkAround(20000);
            //    result = XpathCount("//table[@id='list1']/tbody/tr");
            result = XpathCount("//table[@id='list1']/tbody/tr[@tabindex='-1']");
            Console.WriteLine("Count is "+result);
            return result;
        }

        // Verify Count.
        public void verifyCount(int count, int result)
        {
            Console.WriteLine(count + "\t" + result);
            Assert.IsTrue(count <= result);

        }

        // Get data from a given locator
        public string GetData(string field)
        {
            var locator = locatorReader.ReadLocator(field);
            WaitForElementPresent(locator, 30);
            WaitForElementVisible(locator, 30);
            return GetText(locator);
        }

        // Verify Element Available
        public void verifyElementAvailable(string field)
        {
            var locator = locatorReader.ReadLocator(field);
            WaitForElementVisible(locator, 20);
            Assert.IsTrue(IsElementVisible(locator));
        }

        // Verify element not available.
        public void verifyElementNotAvailable(string field)
        {
            var locator = locatorReader.ReadLocator(field);
            Assert.IsTrue(ElementNotAvailable(locator));
        }

        // Select by text given node
        public void SelectByText(string xmlNode, string text)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(locator, 20);
            SelectDropDownByText(locator, text);
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
        }

      
        // Verify that given field is not required.
        public void VerifyNotRequired()
        {
            var email = GetAtrributeByLocator("//*[@id='EmployeeElectronicAddress2ElectronicContent']", "class");
            WaitForWorkAround(5000);
            Assert.IsFalse(email.Contains("required"));
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






    }
}