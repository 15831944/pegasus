using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using PegasusTests.Locators;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.PageHelper
{
    
    public class CorpOffice_OfficeHelper : DriverHelper
    {
        public LocatorReader locatorReader;

        public CorpOffice_OfficeHelper(IWebDriver idriver)
            : base(idriver)
        {
            locatorReader = new LocatorReader("CorpOffice_Office.xml");
        }
        
        
        // Type into given xml node
        public void TypeText(string Field, string text)
        {
            var locator = locatorReader.ReadLocator(Field);
            WaitForElementPresent(locator, 20);
            SendKeys(locator, text);
        }

        // Verify absentness of the element.
        public void verifyElementNotPresent(string field)
        {
            var locator = locatorReader.ReadLocator(field);
            WaitForWorkAround(2000);
            Assert.IsFalse(IsElementPresent(locator));
        }

        // Verify presence of the element
        public void verifyElementPresent(string field)
        {
            var locator = locatorReader.ReadLocator(field);
            WaitForElementPresent(locator, 30);
            Assert.IsTrue(IsElementPresent(locator));
        }

        // Verify presence of the element
        public void VerifyElement(string locator)
        {
            //var locator = locatorReader.ReadLocator(field);
            WaitForElementPresent(locator, 30);
            Assert.IsTrue(IsElementPresent(locator));
        }

        // Upload a file
        internal void Upload(string Field, string FileName)
        {
            var locator = locatorReader.ReadLocator(Field);
            WaitForElementVisible(locator, 20);
            UploadFile(locator, FileName);
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

        // Click on xml node
        public void ClickElement(string xmlNode)
        {
            WaitForWorkAround(3000);
            var locator = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(locator, 50);
            Click(locator);
            WaitForWorkAround(3000);
        }

        // Click the element via Java Script

        public void ClickJS(string xmlNode)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            ClickViaJavaScript(locator);
        }

        // Verify eAddressl label options contains social media options.
        public void VerifySocialMedia()
        {
            var content = GetTextByXpath("//*[@id='EmployeeElectronicAddress1ElectronicContentLabel']");
            Assert.IsTrue(content.Contains("Facebook"));
            WaitForWorkAround(3000);

        }

        // Verify eAddressl Label for added address.
        public void VerifyeLabel(string xmlNode)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            WaitForWorkAround(2000);
            IsElementPresent(locator);
        }

 


    }
}