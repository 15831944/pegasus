using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using PegasusTests.PageHelper.Comm;
using OpenQA.Selenium.Support.UI;
using PegasusTests.Locators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PegasusTests.PageHelper
{
    public class OfficeAdminCommonLayoutHelper : DriverHelper
    {
        public LocatorReader locatorReader;

        public OfficeAdminCommonLayoutHelper(IWebDriver idriver)
            : base(idriver)
        {
            locatorReader = new LocatorReader("OfficeAdminCommonLayout.xml");
        }

        //Click to given xml node
        public void ClickElement(string XmlNode)
        {
            String locator = locatorReader.ReadLocator(XmlNode);
            Click(locator);
        }
        internal void MouseHover(string field)
        {
            var locator = locatorReader.ReadLocator(field);
            WaitForElementPresent(locator, 20);
            MouseOver(locator);
        }
        public bool ElementVisible(string field)
        {
            var locator = locatorReader.ReadLocator(field);
            return IsElementPresent(locator);
        }

        // method to check for common layout in all tables 

        public void checkIfCommon()
        {
            OfficeAdminCommonLayoutHelper commonLayoutHelper = new OfficeAdminCommonLayoutHelper(GetWebDriver());

            string[] layout = { "GridView", "JQGridHdiv", "HBox", "HTable", "ToolBar", "BDiv", "Bottom" };

            for (int i = 0; i < layout.Length - 1; i++)
            {
                commonLayoutHelper.WaitForWorkAround(2000);
                Assert.IsTrue(commonLayoutHelper.ElementVisible(layout[i]));

            }
        }
    }
}
