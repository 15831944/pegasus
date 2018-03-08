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
    public class OfficeActivities_MeetingHelper : DriverHelper
    {
        public LocatorReader locatorReader;

        public OfficeActivities_MeetingHelper(IWebDriver idriver)
            : base(idriver)
        {
            locatorReader = new LocatorReader("OfficeActivities_Meeting.xml");
        }

        // Type into given xml node
        public void TypeText(string Field, string text)
        {
            String locator = locatorReader.ReadLocator(Field);
            WaitForElementPresent(locator, 20);
            SendKeys(locator, text);
        }

        // Select element by name.
        public void SelectByText(string xmlNode, string text)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            SelectDropDownByText(locator, text);
        }

        // Click Via Enter key
        public void PressEnter(string xmlNode)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(locator, 20);
            ClickViaEnter(locator);
        }

        // Verify text of given xml node
        public void VerifyText(string XmlNode, string text)
        {
            String locator = locatorReader.ReadLocator(XmlNode);
            String value = GetText(locator);
            Assert.IsTrue(value.Contains(text));
        }

        //Delete meetings
        public void DeleteMeetings()
        {
            var l = "//table[@id='list1']/tbody/tr[2]/td[8]/span/a[1]/span";
            var cnt = XpathCount("//table[@id='list1']/tbody/tr");
            Console.WriteLine("Xath Count is " + cnt);
            for (int i = 12; i < cnt; i++)
            {
                var ll = "//table[@id='list1']/tbody/tr[" + i + "]/td[8]/span/a[1]/span";
                Console.WriteLine("loc path is " + ll);
                Click(ll);
                AcceptAlert();
                WaitForWorkAround(3000);
            }
        }

        // Upload File
        public void Upload(string Field, string file)
        {
            String locator = locatorReader.ReadLocator(Field);
            WaitForElementPresent(locator, 20);
            UploadFile(locator, file);
        }
        // Method for selecting related to client.
        public void ClickOnPagination()
        {
            var Loc1 = "//a[@id='assignParent']";
            var Loc2 = "//div[@id='clientpaginationdiv']/span[2]/a";
            var Loc3 = "//div[@id='clientpaginationdiv']/span[3]/a";
            Click(Loc1);
            WaitForWorkAround(2000);
            if (IsElementPresent(Loc2))
            {
                Click(Loc2);
                WaitForWorkAround(4000);
            }

            if (IsElementPresent(Loc3))
            {
                Click(Loc3);
                WaitForWorkAround(4000);
            }
        }

        // Select by value
        public void Select(string xmlNode, string value)
        {
            String locator = locatorReader.ReadLocator(xmlNode);
            SelectDropDown(locator, value);
        }

        // Click forcefully on any element
        public void DblClick(string xmlNode)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            DoubleClick(locator);
        }

        // Click on any element using java script method.
        public void ClickForce(string xmlNode)
        {
            var loc = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(loc ,10);
            ClickViaJavaScript(loc);
        }


        // Click on given xml node.
        public void ClickElement(string xmlNode)
        {
            String locator = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(locator, 20);
            Click(locator);
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
