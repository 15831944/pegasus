using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using PegasusTests.Locators;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.PageHelper
{
    public class Office_GroupHelper : DriverHelper
    {
        public LocatorReader locatorReader;

        public Office_GroupHelper(IWebDriver idriver)
            : base(idriver)
        {
            locatorReader = new LocatorReader("Office_Group.xml");
        }

        
        // Type into given xml node
        public void TypeText(string Field, string text)
        {
            var locator = locatorReader.ReadLocator(Field);
            WaitForElementPresent(locator, 20);
            SendKeys(locator, text);
        }

        // Verify if  the element is visible.
        public void verifyElementVisible(string field)
        {
            var locator = locatorReader.ReadLocator(field);
            WaitForElementPresent(locator, 30);
            WaitForElementVisible(locator, 30);
            Assert.IsTrue(IsElementVisible(locator));
        }

        // Verify text of given xml node
        public void VerifyText(string XmlNode, string text)
        {
            var locator = locatorReader.ReadLocator(XmlNode);
            var value = GetText(locator);
            Assert.IsTrue(value.Contains(text));
        }

        // TO remove any text.
        public void removeText(string field)
        {
            var locator = locatorReader.ReadLocator(field);
            WaitForElementPresent(locator, 30);
            GetWebDriver().FindElement(ByLocator(locator)).Clear();
        }

        //Delete All Gropus
        public void DeleteGroups()
        {
            var l = "//table[@id='list1']/tbody//td[2]/span[2]/a";
            var cnt = XpathCount("//table[@id='list1']/tbody/tr");
            System.Console.WriteLine("Xath Count is " + cnt);
            for (int i = 12; i < cnt; i++)
            {
                var ll = "//table[@id='list1']/tbody/tr[" + i + "]/td[2]/span[2]/a";
                System.Console.WriteLine("loc path is " + ll);
                Click(ll);
                AcceptAlert();
                WaitForWorkAround(3000);
            }
        }

        // Select by value
        public void Select(string xmlNode, string value)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            SelectDropDown(locator, value);
        }

        //Click on given xml node.
        public void ClickElement(string xmlNode)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(locator, 20);
            Click(locator);
        }
    }
}