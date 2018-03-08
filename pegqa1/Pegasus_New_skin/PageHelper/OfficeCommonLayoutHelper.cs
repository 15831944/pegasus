
﻿/* Documented by Khalil Shakir
* 
* This helper method is used in conjunction with JTable_Common_Layout_OfficeMain.cs
* and connnects with the OfficeMainCommonLayout.xml file.
*
*/

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
    public class OfficeCommonLayoutHelper : DriverHelper
    {
        public LocatorReader locatorReader;

        public OfficeCommonLayoutHelper(IWebDriver idriver)
            : base(idriver)
        {

            locatorReader = new LocatorReader("OfficeMainCommonLayout.xml");

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
            WaitForWorkAround(3000);
        }
        public bool ElementVisible(string field)
        {
            var locator = locatorReader.ReadLocator(field);
            return IsElementPresent(locator);
        }

        public void ClickForceFully(string xmlNode)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(locator ,10);
            ClickViaJavaScript(locator);
        }
        // method to check for common layout in all tables 

        public void checkIfCommon()
        {


            OfficeCommonLayoutHelper commonLayoutHelper = new OfficeCommonLayoutHelper(GetWebDriver());

            string[] layout = { "GridView", "JQGridHdiv", "HBox", "HTable", "ToolBar", "BDiv", "Bottom" };

            for (int i = 0; i < layout.Length - 1; i++)
            {
                commonLayoutHelper.WaitForWorkAround(2000);
                Assert.IsTrue(commonLayoutHelper.ElementVisible(layout[i]));

            }
        }

    }
}