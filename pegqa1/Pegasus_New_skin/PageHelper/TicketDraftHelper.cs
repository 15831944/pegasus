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
    public class TicketDraftHelper : DriverHelper
    {
        public LocatorReader locatorReader;
        public TicketDraftHelper(IWebDriver idriver) : base(idriver)
        {
            locatorReader = new LocatorReader("TicketDraft.xml");
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

    }
}
