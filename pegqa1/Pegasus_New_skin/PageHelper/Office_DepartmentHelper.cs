using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using PegasusTests.Locators;
using PegasusTests.PageHelper.Comm;
using System;

namespace PegasusTests.PageHelper
{
    public class Office_DepartmentHelper : DriverHelper
    {
        public LocatorReader locatorReader;

        public Office_DepartmentHelper(IWebDriver idriver)
            : base(idriver)
        {
            locatorReader = new LocatorReader("Office_Department.xml");
        }
       public string EditDeprtLoc="//table[@id='list1']/tbody/tr[2]/td[@aria-describedby='list1_name']/a[text()='Edit Department']";

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

        // Click on given xml  node.
        public void ClickElement(string xmlNode)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(locator, 20);
            Click(locator);
        }

        // Redirect To URL
        public void RedirectToPage()
        {
            GetWebDriver().Navigate().GoToUrl("https://www.mypegasuscrm.com/newthemecorp/newthemeoffice/departments");
        }

        //Delete All Calls
        public void DeletesDepartment()
        {
            var l = "//table[@id='list1']/tbody/tr[2]/td[2]/a[2]/span";
            var cnt = XpathCount("//table[@id='list1']/tbody/tr");
            Console.WriteLine("Xath Count is " + cnt);
            for (int i = 12; i < cnt; i++)
            {
                var ll = "//table[@id='list1']/tbody/tr[" + i + "]/td[2]/a[2]/span";
                Console.WriteLine("loc path is " + ll);
                Click(ll);
                AcceptAlert();
                WaitForWorkAround(3000);
            }
        }

        // Redirect To Url
        public void RedirectToAdmin()
        {
            GetWebDriver().Navigate().GoToUrl("https://www.mypegasuscrm.com/newthemecorp/newthemeoffice/admin");
        }
    }
}