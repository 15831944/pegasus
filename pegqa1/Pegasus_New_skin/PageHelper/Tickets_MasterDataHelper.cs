using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using PegasusTests.Locators;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.PageHelper
{
    public class Tickets_MasterDataHelper : DriverHelper
    {
        public LocatorReader locatorReader;

        public Tickets_MasterDataHelper(IWebDriver idriver)
            : base(idriver)
        {
            locatorReader = new LocatorReader("Ticket_MasterData.xml");
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

        //Delete Category.
        public void DeleteCategory(string category)
        {
            var loc = "//*[@id='TicketMasterDataDeleteItems']/option[text()='" + category + "']";
            Click(loc);

        }

        // Select element by text
        public void SelectByText(string xml, string text)
            {
            var loc = locatorReader.ReadLocator(xml);
            WaitForElementPresent(loc, 10);
            SelectDropDownByText(loc ,text);

            }

        // Click on given xml node.
        public void ClickElement(string xmlNode)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(locator, 20);
            Click(locator);
        }

        public void ClickJS(string xmlNode)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(locator, 20);
            ClickViaJavaScript(locator);
        }
    }
}