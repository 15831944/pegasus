using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using PegasusTests.Locators;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.PageHelper
{
    public class Ticket_SettingsHelper : DriverHelper
    {
        public LocatorReader locatorReader;

        public Ticket_SettingsHelper(IWebDriver idriver)
            : base(idriver)
        {
            locatorReader = new LocatorReader("Ticket_Settings.xml");
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

        // Select element by text.
        public void SelectByText(string xmlNode ,string text)
        {
            var Locator = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(Locator ,10);
            SelectDropDownByText(Locator ,text);
        }
        // Verify presence of element
        public void verifyElementPresent(string field)
        {
            var locator = locatorReader.ReadLocator(field);
            WaitForElementPresent(locator, 30);
            Assert.IsTrue(IsElementPresent(locator));
        }

        // Click on given xml node.
        public void ClickElement(string xmlNode)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(locator, 20);
            Click(locator);
        }

        //verify selected option
        public void VerifySlctdOptn(string xmlNode, string option)
        {
            var loc = locatorReader.ReadLocator(xmlNode);
            VerifySelectedOption(loc, option);
        }
    }
}