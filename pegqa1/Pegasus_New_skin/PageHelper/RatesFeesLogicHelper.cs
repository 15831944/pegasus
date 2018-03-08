using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using PegasusTests.Locators;
using PegasusTests.PageHelper.Comm;
namespace PegasusTests.PageHelper
{
    class RatesFeesLogicHelper : DriverHelper
    {
        public LocatorReader locatorReader;
        public RatesFeesLogicHelper(IWebDriver idriver)
            : base(idriver)
        {
            locatorReader = new LocatorReader("RatesFeesLogic.xml");
        }
        public void TypeText(string Field, string text)
        {
            var locator = locatorReader.ReadLocator(Field);
            WaitForElementPresent(locator, 20);
            SendKeys(locator, text);
        }
        public void ClearText(string Field)
        {
            var locator = locatorReader.ReadLocator(Field);
            ClearTextBoxValue(locator);

        }
        public string GetTextContent(string xmlnode)
        {
            var locator = locatorReader.ReadLocator(xmlnode);
            return getInputText(locator);
        }
        
        
        public void Select(string xmlNode, string value)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            SelectDropDown(locator, value);
        }
        public void ClickElement(string xmlNode)
        {
            WaitForWorkAround(3000);
            var locator = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(locator, 50);
            Click(locator);
            WaitForWorkAround(3000);
        }
        public void SelectByText(string xmlNode, string text)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            SelectDropDownByText(locator, text);
        }
        public void AlertOK()
        {
            AcceptAlert();
        }
    }
}