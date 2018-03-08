using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using PegasusTests.Locators;
using PegasusTests.PageHelper.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PegasusTests.PageHelper
{
    public class CorpPDFTemplate_CategoriesHelper : DriverHelper
    {
        public LocatorReader locatorReader;

        public CorpPDFTemplate_CategoriesHelper(IWebDriver idriver)
            : base(idriver)
        {
            locatorReader = new LocatorReader("CorpPDFTemplate_Categories.xml");
        }

        // Type into given xml node
        public void TypeText(string Field, string text)
        {
            String locator = locatorReader.ReadLocator(Field);
            WaitForElementPresent(locator, 20);
            SendKeys(locator, text);
        }

        // Search and click given element   
        public void SearchAndClick(String name)
        {
            int x = XpathCount("//div[@class='dd']/ul[2]/li");
            for (int i = 1; i <= x; i++)
            {
                var TextLoc = "//div[@class='dd']/ul[2]/li[" + i + "]/div/span[2]";
                var LocToClick = "//div[@class='dd']/ul[2]/li[" + i + "]/div/span[1]/a[@title = 'Delete Category']";
                if (GetText(TextLoc).Contains(name))
                    Click(LocToClick);
                //WaitForWorkAround(1000);
                AcceptAlert();               
            }
        }

        // Verify text of given xml node
        public void VerifyText(string XmlNode, string text)
        {
            String locator = locatorReader.ReadLocator(XmlNode);
            String value = GetText(locator);
            Assert.IsTrue(value.Contains(text));
        }

        // Select by value
        public void Select(string xmlNode, string value)
        {
            String locator = locatorReader.ReadLocator(xmlNode);
            SelectDropDown(locator, value);
        }

        //Delete PDF Corp
        public void DeltePDFCorp()
        {
            var l = "//table[@id='list1']/tbody/tr[2]/td[2]/span[1]/a/i";
            var cnt = XpathCount("//table[@id='list1']/tbody/tr");
            Console.WriteLine("Xath Count is " + cnt);
            for (int i = 12; i < cnt; i++)
            {
                var ll = "//table[@id='list1']/tbody/tr[" + i + "]/td[2]/span[1]/a/i";
                Console.WriteLine("loc path is " + ll);
                Click(ll);
                AcceptAlert();
                WaitForWorkAround(3000);
            }
        }

        // Click on given xml node.
        public void ClickElement(string xmlNode)
        {
            String locator = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(locator, 20);
            Click(locator);
        }

        // Click the element via JavaScript
        // Click on given xml node.
        public void ClickJS(string xmlNode)
        {
            String locator = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(locator, 20);
            ClickViaJavaScript(locator);
        }


    }
}
