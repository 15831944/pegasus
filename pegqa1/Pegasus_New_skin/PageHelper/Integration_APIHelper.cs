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
    public class Integration_APIHelper : DriverHelper
    {
        public LocatorReader locatorReader;

        public Integration_APIHelper(IWebDriver idriver)
            : base(idriver)
        {
            locatorReader = new LocatorReader("Integration_API.xml");
        }

        // Type into given xml node
        public void TypeText(string Field, string text)
        {
            String locator = locatorReader.ReadLocator(Field);
            WaitForElementPresent(locator, 20);
            SendKeys(locator, text);
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
            WaitForElementVisible(locator, 30);
            SelectDropDown(locator, value);
        }


        //Delete All API Keys
        public void DelteAPIKeys()
        {
            var l = "//ul[@id='api_keys_list']/li[1]/span[2]/a/span";
            var cnt = XpathCount("//ul[@id='api_keys_list']/li");
            Console.WriteLine("Xath Count is " + cnt);
            for (int i = 12; i < cnt; i++)
            {
                var ll = "//ul[@id='api_keys_list']/li[" + i + "]/span[2]/a/span";
                Console.WriteLine("loc path is " + ll);
                Click(ll);
                AcceptAlert();
                WaitForWorkAround(3000);
            }
        }











        //Click on given xml node.
        public void ClickElement(string xmlNode)
        {
            String locator = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(locator, 20);
            Click(locator);
        }
        
        // Search and delete Api.
        public void SearchAndDelete()
        {
            Boolean flag = false;
            var _apikey = GetAtrributeByLocator("//*[@id='ApiCodeApiCode']", "value");
            var api_li_count = XpathCount("//ul[@id='api_keys_list']/li");
            for (int i = 1; i <= api_li_count; i++)
            {
                var _api_locator = "//ul[@id='api_keys_list']/li[" + i + "]/span";
                var _apitext = GetText(_api_locator);
                if (_apitext.Contains(_apikey))
                {
                    var _deleteapi_locator = "//ul[@id='api_keys_list']/li[" + i + "]/span[2]/a";
                    Click(_deleteapi_locator);
                    flag = true;
                    break;
                }
            }
            Assert.IsTrue(flag);
        }





        // Select element by text.
        public void SelectByText(string xmlNode, string text)
        {
            String locator = locatorReader.ReadLocator(xmlNode);
            WaitForElementVisible(locator, 30);
            SelectDropDownByText(locator, text);
        }
    }
}
