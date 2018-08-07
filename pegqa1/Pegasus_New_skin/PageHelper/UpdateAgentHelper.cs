using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using PegasusTests.Locators;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.PageHelper {
    class UpdateAgentHelper : DriverHelper {
        public LocatorReader locatorReader;
         
        public UpdateAgentHelper(IWebDriver idriver)
            : base(idriver) {
            locatorReader = new LocatorReader("UpdateFour.xml");
        }
       
        //Type into given xml node
        public void TypeText(string Field, string text) {
            var locator = locatorReader.ReadLocator(Field);
            WaitForElementPresent(locator, 20);
            SendKeys(locator, text);
        }

        public int tableRow() {
            return TableRowCount();
        }

        public void ClickID(string text) {
            clickByText(text);
            WaitForText(text, 3000);
        }

        public void waitID(string text) {
            waitForLink(text, 4000);
        }

        // Select element by text
        public void SelectByText(string xmlNode, string text) {
            var locator = locatorReader.ReadLocator(xmlNode);
            SelectDropDownByText(locator, text);
        }

        public void Select(string xmlNode, string value) {
            var locator = locatorReader.ReadLocator(xmlNode);
            SelectDropDown(locator, value);
        }

        // Verify element present.
        public void verifyElementPresent(string field) {
            var locator = locatorReader.ReadLocator(field);
            WaitForElementPresent(locator, 30);
            Assert.IsTrue(IsElementPresent(locator));
        }

        public bool checkPresent(string field) {
            var locator = locatorReader.ReadLocator(field);
            WaitForElementPresent(locator, 30);
            return (IsElementPresent(locator));
        }

        public void ClearText(string Field) {
            var locator = locatorReader.ReadLocator(Field);
            ClearTextBoxValue(locator);

        }
        
        //Verify text of given xml node
        public void ClickElement(string xmlNode) {
            WaitForWorkAround(3000);
            var locator = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(locator, 50);
            Click(locator);
            WaitForWorkAround(3000);
        }

        public void doubleClickElement(string xmlNode) {
            WaitForWorkAround(3000);
            var locator = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(locator, 50);
            DoubleClick(locator);
            WaitForWorkAround(3000);
        }
    }
}
