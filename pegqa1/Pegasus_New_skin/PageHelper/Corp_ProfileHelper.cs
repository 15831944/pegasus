using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using PegasusTests.Locators;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.PageHelper
{
    public class Corp_ProfileHelper : DriverHelper
    {
        public LocatorReader locatorReader;

        public Corp_ProfileHelper(IWebDriver idriver)
            : base(idriver)
        {
            locatorReader = new LocatorReader("Corp_Profile.xml");
        }

        // Type into given xml node
        public void TypeText(string Field, string text)
        {
            var locator = locatorReader.ReadLocator(Field);
            WaitForElementPresent(locator, 20);
            SendKeys(locator, text);
        }

        // Upload any image file.
        public void UploadImage(string field, string path)
        {
            var locator = locatorReader.ReadLocator(field);
            WaitForElementPresent(locator, 30);
            UploadFile(locator, path);
        }

        // Verify absentness of the element.
        public void verifyElementNotPresent(string field)
        {
            var locator = locatorReader.ReadLocator(field);
            WaitForWorkAround(2000);
            Assert.IsFalse(IsElementPresent(locator));
        }

        //Verify text of given xml node
        public void VerifyText(string XmlNode, string text)
        {
            var locator = locatorReader.ReadLocator(XmlNode);
            var value = GetText(locator);
            Assert.IsTrue(value.Contains(text));
        }

        //Select by value
        public void Select(string xmlNode, string value)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            SelectDropDown(locator, value);
        }

        //Click element

        public void ClickElement(string xmlNode)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(locator, 20);
            Click(locator);
        }
    }
}