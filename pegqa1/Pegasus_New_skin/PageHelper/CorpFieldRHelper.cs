/* Documented by Khalil Shakir
* 
* This helper method is used in conjunction with CorpFieldRelationships.cs
* and connnects with the CorpFieldRealtionships.xml file.
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
    public class CorpFieldRHelper : DriverHelper
    {
        public LocatorReader locatorReader;

        public CorpFieldRHelper(IWebDriver idriver) : base(idriver)
        {
            locatorReader = new LocatorReader("CorpFieldRelationships.xml");
        }

        //Click to given xml node
        public void ClickElement(string XmlNode)
        {
            String locator = locatorReader.ReadLocator(XmlNode);
            Click(locator);
        }

        //Click to given xml node
        public void clickJS(string XmlNode)
        {
            String locator = locatorReader.ReadLocator(XmlNode);
            ClickViaJavaScript(locator);
        }

        //Select Dropdown text
        //Click to given xml node
        public void selectByText(string XmlNode, string text)
        {
            String locator = locatorReader.ReadLocator(XmlNode);
            SelectDropDownByText(locator,text);
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
    }
}
