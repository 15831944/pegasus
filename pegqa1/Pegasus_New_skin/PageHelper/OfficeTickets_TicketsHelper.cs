using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using PegasusTests.Locators;
using PegasusTests.PageHelper.Comm;
using System;

namespace PegasusTests.PageHelper
{
    public class OfficeTickets_TicketsHelper : DriverHelper
    {
        public LocatorReader locatorReader;

        public OfficeTickets_TicketsHelper(IWebDriver idriver)
            : base(idriver)
        {
            locatorReader = new LocatorReader("OfficeTickets_Tickets.xml");
        }

        // To verify element is present
        public void verifyElementPresent(string field)
        {
            var locator = locatorReader.ReadLocator(field);
            WaitForElementPresent(locator, 30);
            Assert.IsTrue(IsElementPresent(locator));
        }

        // Verify no. of comments.
        public void verifyCommentCount(string comment, int count)
        {
            string locator = "//div[contains(text(),'" + comment + "')]";
            WaitForElementVisible(locator, 30);
            Assert.IsTrue(XpathCount(locator) == count);
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

        //Select by value
        public void Select(string xmlNode, string value)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(locator, 20);
            WaitForElementVisible(locator, 30);
            SelectDropDown(locator, value);
        }


        // Delete All tickets from recyclebin
        public void DeleteTickets()
        {
            var l = "//table[@id='list1']/tbody/tr[2]/td[8]/span[1]/a/span";
            var cnt = XpathCount("//table[@id='list1']/tbody/tr");
            Console.WriteLine("Xath Count is " + cnt);
            for (int i = 12; i < cnt; i++)
            {
                var ll = "//table[@id='list1']/tbody/tr[" + i + "]/td[8]/span[1]/a/span";
                Console.WriteLine("loc path is " + ll);
                Click(ll);
                AcceptAlert();
                WaitForWorkAround(3000);
            }
        }

        // Select element by text
        public void SelectByText(string xmlNode ,string text)
        {
            var loc = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(loc ,10);
            SelectDropDownByText(loc ,text);
        }

        //Click on given xml node.
        public void ClickElement(string xmlNode)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(locator, 20);
            Click(locator);
        }

        // Click using java sript method.
        public void ClickForce(string xmlNode)
        {
            var loc = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(loc ,10);
            ClickViaJavaScript(loc);
        }

        // Get text of the given node.
        public string getText(string field)
        {
            var locator = locatorReader.ReadLocator(field);
            WaitForElementPresent(locator, 30);
            WaitForElementVisible(locator, 30);
            return GetText(locator);
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