using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using PegasusTests.Locators;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.PageHelper
{
    public class Corp_Statistics_UsageStatisticsHelper : DriverHelper
    {
        public LocatorReader locatorReader;

        public Corp_Statistics_UsageStatisticsHelper(IWebDriver idriver)
            : base(idriver)
        {
            locatorReader = new LocatorReader("Corp_Statistics_UsageStatistics.xml");
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

        // Click on xml node
        public void ClickElement(string xmlNode)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(locator, 20);
            Click(locator);
            WaitForWorkAround(3000);

        }
        // If Duplicate value present then click duplicate button
        public void DudlicateClick()
        {
            var Save = "//*[@id='LeadCreateForm']/div[2]/div[3]/a[1]/span[1]";
            var Dublicate = "//*[@id='message']/div[4]/div/a[1]/span[2]";
            Click(Save);
            WaitForWorkAround(3000);

            if (IsElementPresent(Dublicate))
            {
                Click(Dublicate);
                WaitForWorkAround(3000);
            }
        }
    }
}