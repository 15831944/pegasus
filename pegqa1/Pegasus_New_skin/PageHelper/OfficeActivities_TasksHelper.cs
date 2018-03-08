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
    public class OfficeActivities_TasksHelper : DriverHelper
    {
        public LocatorReader locatorReader;

        public OfficeActivities_TasksHelper(IWebDriver idriver)
            : base(idriver)
        {
            locatorReader = new LocatorReader("OfficeActivities_Tasks.xml");
        }


        // Click Via Enter key
        public void PressEnter(string xmlNode)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(locator, 20);
            ClickViaEnter(locator);
        }

        // Type into given xml node
        public void TypeText(string Field, string text)
        {
            String locator = locatorReader.ReadLocator(Field);
            WaitForElementPresent(locator, 20);
            SendKeys(locator, text);
        }

        // Upload a file
        public void Upload(string XmlNode, string File)
        {
            var Locator = locatorReader.ReadLocator(XmlNode);
            WaitForElementPresent(Locator, 20);
            UploadFile(Locator, File);
        }

        // Verify text of given xml node
        public void VerifyText(string XmlNode, string text)
        {
            String locator = locatorReader.ReadLocator(XmlNode);
            String value = GetText(locator);
            Assert.IsTrue(value.Contains(text));
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

        // Select element by name.
        public void SelectByText(string xmlNode, string text)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            SelectDropDownByText(locator, text);
        }

        // Delete All tasks from recycle bin
        public void DeleteTasks()
        {
            var l = "//table[@id='list1']/tbody/tr[2]/td[7]/a[1]/span";
            var cnt = XpathCount("//table[@id='list1']/tbody/tr");
            Console.WriteLine("Xath Count is " + cnt);
            for (int i = 12; i < cnt; i++)
            {
                var ll = "//table[@id='list1']/tbody/tr[" + i + "]/td[7]/a[1]/span";
                Console.WriteLine("loc path is " + ll);
                Click(ll);
                AcceptAlert();
                WaitForWorkAround(3000);
            }
        }

        // Select by value
        public void Select(string xmlNode, string value)
        {
            String locator = locatorReader.ReadLocator(xmlNode);
            SelectDropDown(locator, value);
        }

        // Click using java script method
        public void ClickForce(string xmlnode)
        {
            var loc = locatorReader.ReadLocator(xmlnode);
            WaitForElementPresent(loc, 10);
            ClickViaJavaScript(loc);
        }

        // Verifies end date is auto filled.
        public void VerifyEndDate()
        {
            var text = GetAtrributeByLocator("//*[@id='TaskDueDate']", "value");
            WaitForWorkAround(3000);
            Assert.IsTrue(text.Contains("17/12/2017"));
            WaitForWorkAround(3000);
        }

        // Click on given xml node.
        public void ClickElement(string xmlNode)
        {
            String locator = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(locator, 20);
            Click(locator);
        }

        // Click on given xml node via Java Script
        public void ClickJs(string xmlNode)
        {
            String locator = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(locator, 20);
            ClickViaJavaScript(locator);
        }


        // Verify selected option
        public void VerifySelectdOptn(string xmlNode, string option)
        {
            String locator = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(locator, 20);
            VerifySelectedOption(locator, option);
        }

    }
}
