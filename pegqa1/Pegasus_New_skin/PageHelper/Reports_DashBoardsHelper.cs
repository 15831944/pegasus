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
    public class Reports_DashBoardsHelper : DriverHelper
    {
        public LocatorReader locatorReader;

        public Reports_DashBoardsHelper(IWebDriver idriver)
            : base(idriver)
        {
            locatorReader = new LocatorReader("Reports_DashBoards.xml");
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

      // Select by text
      public void SelectByText(string xmlNode, string text)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            SelectDropDownByText(locator, text);
        }

        // Click on given xml node.

        public void ClickElement(string xmlNode)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(locator, 20);
            Click(locator);
        }

        //Delete All DashBoards.
        public void DeleteDashBoard()
        {
            var l = "//table[@id='list1']/tbody/tr[2]/td[4]/a[2]/span";
            var cnt = XpathCount("//table[@id='list1']/tbody/tr");
            Console.WriteLine("Xath Count is " + cnt);
            for (int i = 15; i < cnt; i++)
            {
                var ll = "//table[@id='list1']/tbody/tr[" + i + "]/td[4]/a[2]/span";
                Console.WriteLine("loc path is " + ll);
                Click(ll);
                AcceptAlert();
                WaitForWorkAround(3000);
            }
        }

        // Verify availability of element via xml node
        public void verifyElementAvailable(String field)
        {
            var locator = locatorReader.ReadLocator(field);
            WaitForElementVisible(locator, 20);
            Assert.IsTrue(IsElementVisible(locator));
        }

        // Check the element and then click on same
         public void CheckAndClick(string xmlNode)
        {
            var loc = locatorReader.ReadLocator(xmlNode);
            if (IsElementPresent(loc))
            {
                Click(loc);
            }
            else
            {
                // Do Nothing
            }

         
        }


    }
}