using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using PegasusTests.Locators;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.PageHelper
{
    public class CorpMasterdata_LanguageHelper : DriverHelper
    {
        public LocatorReader locatorReader;

        public CorpMasterdata_LanguageHelper(IWebDriver idriver)
            : base(idriver)
        {
            locatorReader = new LocatorReader("CorpMasterdata_Language.xml");
        }

        // Type into given xml node
        public void TypeText(string Field, string text)
        {
            var locator = locatorReader.ReadLocator(Field);
            WaitForElementPresent(locator, 20);
            SendKeys(locator, text);
        }

        // Remove given text
        public void removeText(string field)
        {
            var locator = locatorReader.ReadLocator(field);
            WaitForWorkAround(2000);
            WaitForElementVisible(locator, 30);
            GetWebDriver().FindElement(ByLocator(locator)).Clear();
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


        // Click using java script method
        public void ClickForce(string xmlNode)
        {
            var loc = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(loc ,10);
            ClickViaJavaScript(loc);
        }
        // Click on given xml node

        public void ClickElement(string xmlNode)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(locator, 20);
            Click(locator);
        }
    }
}