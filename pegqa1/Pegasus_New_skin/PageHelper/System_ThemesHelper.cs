using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using PegasusTests.Locators;
using PegasusTests.PageHelper.Comm;
using System;

namespace PegasusTests.PageHelper
{
    public class System_ThemesHelper : DriverHelper
    {
        public LocatorReader locatorReader;

        public System_ThemesHelper(IWebDriver idriver)
            : base(idriver)
        {
            locatorReader = new LocatorReader("System_Themes.xml");
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

        // Verify presence of the element
        public void verifyElementPresent(string field)
        {
            var locator = locatorReader.ReadLocator(field);
            WaitForElementPresent(locator, 30);
            Assert.IsTrue(IsElementPresent(locator));
        }

        // Select by value
        public void Select(string xmlNode, string value)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            SelectDropDown(locator, value);
        }

        // Delete All Themes
        public void DeleteTheme()
        {
            var l = "//table[@id='list1']/tbody/tr[2]/td[5]/a[3]/i";
            var cnt = XpathCount("//table[@id='list1']/tbody/tr");
            Console.WriteLine("Xath Count is " + cnt);
            for (int i = 12; i < cnt; i++)
            {
                var ll = "//table[@id='list1']/tbody/tr[" + i + "]/td[5]/a[3]/i";
                Console.WriteLine("loc path is " + ll);
                Click(ll);
                AcceptAlert();
                WaitForWorkAround(3000);
            }
        }

        // Click on the given xml node.
        public void ClickElement(string xmlNode)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(locator, 20);
            Click(locator);
        }
       
        // Select element by text.
        public void SelectByText(string xmlNode, string text)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(locator, 20);
            SelectDropDownByText(locator, text);
        }

        // Search the edit icon of theme
        public void ClickEditIcon()
        {              
            for (int i = 2; i< 90; i++)
            {
                var loc = "//table[@id='list1']//tr[" + i + "]//td[5]/a[1]/i";
                //   var locator = _driver.FindElement(By.XPath(loc));
                if (IsElementPresent(loc))                   
                {
                    WaitForWorkAround(5000);
                    Click(loc);
                    break;
                }
                else
                {
                    // Do nothing 
                }
            }
        }
    }
}