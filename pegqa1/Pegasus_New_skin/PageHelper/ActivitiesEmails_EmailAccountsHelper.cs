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
using OpenQA.Selenium.Interactions;

namespace PegasusTests.PageHelper
{
    public class ActivitiesEmails_EmailAccountsHelper : DriverHelper
    {
        public LocatorReader locatorReader;

        public ActivitiesEmails_EmailAccountsHelper(IWebDriver idriver)
            : base(idriver)
        {
            locatorReader = new LocatorReader("ActivitiesEmails_EmailAccount.xml");
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

        // Click on any element.
        public void ClickElement(string xmlNode)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(locator, 20);
            Click(locator);
        }

        // Select by value.
        public void SelectByText(string xmlNode, string text)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(locator, 20);
            SelectDropDownByText(locator, text);
        }

        // Verify element present
        public void verifyElementAvailable(String field)
        {
            var locator = locatorReader.ReadLocator(field);
            WaitForElementVisible(locator, 20);
            Assert.IsTrue(IsElementVisible(locator));
        }

        public void DragCardFromColumnToColumn(int p0, int p1)
        {
            var columns = GetWebDriver().FindElements(By.ClassName("column"));
            var cardHeader = GetWebDriver().FindElement(By.ClassName("portlet-header"));

            Actions builder = new Actions(GetWebDriver());

            IAction dragAndDrop = builder.ClickAndHold(cardHeader)
               .MoveToElement(columns[0])
               .Release(columns[1])
               .Build();

            dragAndDrop.Perform();      
    }
}
}