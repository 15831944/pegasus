using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using PegasusTests.PageHelper.Comm;
using OpenQA.Selenium.Support.UI;
using PegasusTests.Locators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PegasusTests.PageHelper
{
    public class CreateNewLeadGroupHelper : DriverHelper
    {
        public LocatorReader locatorReader;
        public CreateNewLeadGroupHelper(IWebDriver idriver) : base(idriver)
        {
            locatorReader = new LocatorReader("CreateNewLeadGroup.xml");
        }
        public void ClickElement(string XmlNode)
        {
            String locator = locatorReader.ReadLocator(XmlNode);
            Click(locator);
        }
        public void TypeText(string Field, string text)
        {
            var locator = locatorReader.ReadLocator(Field);
            WaitForElementPresent(locator, 20);
            SendKeys(locator, text);
        }
        public void SelectByText(string xmlNode, string text)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            SelectDropDownByText(locator, text);
        }
        public void Select(string Field, string value)
        {
            var locator = locatorReader.ReadLocator(Field);
            SelectDropDown(locator, value);


        }
        public void clickText(string Field)
        {
            var locator = locatorReader.ReadLocator(Field);
            clickByText(locator);
        }
        public void scrollToElement(string field)
        {
            var locator = locatorReader.ReadLocator(field);
            WaitForElementPresent(locator, 30);
            WaitForWorkAround(2000);
            ScrollDown(locator);
        }
        public void jsClick(string field)
        {
            var locator = locatorReader.ReadLocator(field);
            ClickViaJavaScript(locator);

        }
    }
}
