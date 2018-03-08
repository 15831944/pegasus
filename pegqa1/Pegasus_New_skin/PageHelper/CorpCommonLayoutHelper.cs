/* Documented by Khalil Shakir
* 
* This helper method is used in conjunction with JTable_Common_Layout.cs
* and connnects with the CorpCommonLayout.xml file.
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
    class CorpCommonLayoutHelper : DriverHelper
    {
        public LocatorReader locatorReader;

        public CorpCommonLayoutHelper(IWebDriver idriver): base(idriver)
        {
            locatorReader = new LocatorReader("CorpCommonLayout.xml");
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
            CorpCommonLayoutHelper commonLayoutHelper = new CorpCommonLayoutHelper(GetWebDriver());
            ExecutionLog executionLog = new ExecutionLog();

            string[] layout = { "GridView", "JQGridHdiv", "HBox", "HTable", "ToolBar", "BDiv", "Bottom" };

            for (int i = 0; i < layout.Length - 1; i++)
            {
                //commonLayoutHelper.WaitForWorkAround(2000);

                executionLog.Log(" JTable_Common_Layout_Corp", " Testing " + layout[i]);
                Assert.IsTrue(commonLayoutHelper.ElementVisible(layout[i]));

            }
        }

    }
}
