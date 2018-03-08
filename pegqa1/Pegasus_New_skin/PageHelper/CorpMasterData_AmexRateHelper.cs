using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using PegasusTests.Locators;
using PegasusTests.PageHelper.Comm;
using System;

namespace PegasusTests.PageHelper
{
    public class CorpMasterData_AmexRateHelper : DriverHelper
    {
        public LocatorReader locatorReader;

        public CorpMasterData_AmexRateHelper(IWebDriver idriver)
            : base(idriver)
        {
            locatorReader = new LocatorReader("CorpMasterData_AmexRate.xml");
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

        //Delete Amex rates
        public void DelteAmexRates()
        {
            var l = "//table[@id='list1']/tbody/tr[2]/td[2]/div/a[2]/span";
            var cnt = XpathCount("//table[@id='list1']/tbody/tr");
            Console.WriteLine("Xath Count is " + cnt);
            for (int i = 15; i < cnt; i++)
            {
                var ll = "//table[@id='list1']/tbody/tr[" + i + "]/td[2]/div/a[2]/span";
                Console.WriteLine("loc path is " + ll);
                Click(ll);
                AcceptAlert();
                WaitForWorkAround(3000);
            }
        }

        // Select by value
        public void Select(string xmlNode, string value)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            SelectDropDown(locator, value);
        }

        // Click on xml node.

        public void ClickElement(string xmlNode)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(locator, 20);
            Click(locator);
        }

        // Enter current date 
        public void CurrentDate(string XmlNode)
        {
            var locator = locatorReader.ReadLocator(XmlNode);
            DateTime today = DateTime.Today; // As DateTime
            string s_today = today.ToString("MM/dd/yyyy"); // As String
            Console.Write("Today date is" + s_today);
            SendKeys(locator, s_today);
        }
    }
}