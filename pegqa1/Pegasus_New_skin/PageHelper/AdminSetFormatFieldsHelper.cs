/*
* AdminSetFormatPhoneHelper.cs is linked AdminSetFormatPhone.cs and
* AdminSetFormatPhone.xml
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
    public class AdminSetFormatFieldsHelper : DriverHelper
    {
        public LocatorReader locatorReader;

        public AdminSetFormatFieldsHelper(IWebDriver idriver)
            : base(idriver)
        {
            locatorReader = new LocatorReader("AdminSetFormatFields.xml");
        }

        //Click to given xml node
        public void ClickElement(string XmlNode)
        {
            String locator = locatorReader.ReadLocator(XmlNode);
            WaitForElementPresent(locator ,10);
            Click(locator);
        }

        // Click the element via JavaScript
        public void ClickJs(string XmlNode)
        {
            String locator = locatorReader.ReadLocator(XmlNode);
            WaitForElementPresent(locator, 10);
            ClickViaJavaScript(locator);
        }



        //Check checkbox 
        public void checkAndClick(string XmlNode)
        {
            String loc = locatorReader.ReadLocator(XmlNode);     
            IWebElement el1 = GetWebDriver().FindElement(By.XPath(loc));
            if (el1.Selected)
            {
                //Do noting
            }
            else
            { 
                Click(loc);
            }
        }

        //uncheck the checkbox
        public void UncheckAndClick(string xmlNode)
        {
            String loc = locatorReader.ReadLocator(xmlNode);
            IWebElement el2 = GetWebDriver().FindElement(By.XPath(loc));
            if(el2.Selected)
            {
                Click(loc);
            }
            else
            {
                // Do nothing 
            }
        }

        // Click on element using javascript
        public void ClickForce(string xmlNode)
        {
            var loc = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(loc ,10);
            ClickViaJavaScript(loc);

        }

        // Select form dropdown by text
        public void SelectByText(string xmlnode ,string text)
        {
            var loc = locatorReader.ReadLocator(xmlnode);
            WaitForElementPresent(loc ,10);
            SelectDropDownByText(loc ,text);
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

        // Type into given xml node
        public void TypeText(string Field, string text)
        {
            var locator = locatorReader.ReadLocator(Field);
          //  WaitForElementPresent(locator, 20);
         //   WaitForElementEnabled(locator, 20);
            SendKeys(locator, text);
        }

        public void VerifyText(string XmlNode, string text)
        {
            var locator = locatorReader.ReadLocator(XmlNode);
            var value = GetText(locator);
            Assert.IsTrue(value.Contains(text));
        }

        //Verify if the given field is required
        public void VerifyIfFormatPhone(string textloc)
        {
          //  var locator = locatorReader.ReadLocator(xml);
           
            string text = GetAtrributeByLocator("//*[@id='ClientContact0ClientContactContactPhone0PhoneNumber']", "title");
            Console.WriteLine(text);
            Assert.IsTrue(text.ToLower().Contains(textloc));
        }

        public void VerifyIfFormatEmail(string getlocator)
        {
            //  var locator = locatorReader.ReadLocator(getlocator);
            string text = GetAtrributeByLocator("//*[@id='ClientContact0ClientContactContactTitle']", "class");
            Console.WriteLine(text);
            Assert.IsTrue(text.ToLower().Contains("email"));
        }

        public void VerifyIfFormatSSN(string getlocator)
        {
            //*[@id='ClientContact0ClientContactContactTitle']

            var locator = locatorReader.ReadLocator(getlocator);
            string text = GetAtrributeByLocator(locator, "class");
            Console.WriteLine(text);
            Assert.IsTrue(text.ToLower().Contains("ssn"));
        }

        public void VerifyIfFormatTaxID()
        {
            // Hardcoded to title field (Issues occured when trying to pass an xml node)
            string text = GetAtrributeByLocator("//*[@id='ClientContact0ClientContactContactTitle']", "class");
            WaitForWorkAround(3000);
            Console.WriteLine(text);
            Assert.IsTrue(text.ToLower().Contains("tax"));
        }

        public void VerifyIfDate(string getlocator)
        {
            //  var locator = locatorReader.ReadLocator(getlocator);
            string text = GetAtrributeByLocator("//*[@id='ClientContact0ClientContactContactTitle']", "class");
            Console.WriteLine(text);
            Assert.IsTrue(text.ToLower().Contains("date"));
        }

        public void VerifyIfDecimal(string getlocator)
        {
            //  var locator = locatorReader.ReadLocator(getlocator);
            string text = GetAtrributeByLocator("//*[@id='ClientContact0ClientContactContactTitle']", "class");
            Console.WriteLine(text);
            Assert.IsTrue(text.ToLower().Contains("number"));
        }

        public void VerifyIfNumeric(string getlocator)
        {
            //  var locator = locatorReader.ReadLocator(getlocator);
            string text = GetAtrributeByLocator("//*[@id='ClientContact0ClientContactContactTitle']", "class");
        Console.WriteLine(text);
            Assert.IsTrue(text.ToLower().Contains("digits"));
        }

        //Verify if the given field is required
        public void VerifyIfRequired(string getlocator)
        {
            //  var locator = locatorReader.ReadLocator(getlocator);
            string text = GetAtrributeByLocator("//*[@id='ClientDetailTaxID']", "class");
            Console.WriteLine(text);
            Assert.IsTrue(text.ToLower().Contains("require"));
        }
    }
}
