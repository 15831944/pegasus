using System;
using LinqToExcel;
using System.Linq;
using LinqToExcel.Query;
using LinqToExcel.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using PegasusTests.Locators;
using PegasusTests.PageHelper.Comm;
using System.IO;

namespace PegasusTests.PageHelper
{
    public class Office_ContactsHelper : DriverHelper
    {
        public LocatorReader locatorReader;

        public Office_ContactsHelper(IWebDriver idriver)
            : base(idriver)
        {
            locatorReader = new LocatorReader("Office_Contacts.xml");
        }

        // Type into given xml node
        public void TypeText(string Field, string text)
        {
            var locator = locatorReader.ReadLocator(Field);
            WaitForElementPresent(locator, 20);
            WaitForElementEnabled(locator, 20);
            SendKeys(locator, text);
        }

        // Verify text of given xml node
        public void VerifyText(string XmlNode, string text)
        {
            var locator = locatorReader.ReadLocator(XmlNode);
            var value = GetText(locator);
            Console.WriteLine(value);
            Assert.IsTrue(value.Contains(text));
        }

        // Select by value
        public void Select(string xmlNode, string value)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            SelectDropDown(locator, value);
        }

        // Select by
        public void selectByText(string xmlNode, string text)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            SelectDropDownByText(locator, text);
        }

        // Delete All Contacts.
        public void DelteAllContacts()
        {
            var l = "//table[@id='list1']/tbody/tr[2]/td[10]/div/a[1]/span";
            var cnt = XpathCount("//table[@id='list1']/tbody/tr");
            Console.WriteLine("Xath Count is " + cnt);
            for (int i = 12; i < cnt; i++)
            {
                var ll = "//table[@id='list1']/tbody/tr[" + i + "]/td[10]/div/a[1]/span";
                Console.WriteLine("loc path is " + ll);
                Click(ll);
                AcceptAlert();
                WaitForWorkAround(3000);
            }
        }

        // Click on given xml node.
        public void ClickElement(string xmlNode)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(locator, 20);
            Click(locator);
        }

        // Click on given xml node.
        public void ClickJs(string xmlNode)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(locator, 20);
            ClickViaJavaScript(locator);
        }


        // Double click on any given element.
        public void DobleClick(string xmlNode)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(locator ,10);
            DoubleClick(locator);
        }

        // Decline Alert
        public void DeclineAlert()
        {
            WaitForWorkAround(3000);
            GetWebDriver().SwitchTo().Alert().Dismiss();
        }
        // Upload a file
        public void Upload(string xmlNode, string file)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(locator ,20);
            UploadFile(locator, file);
        }

        // To verify Presence of element
        public void verifyElementPresent(string field)
        {
            var locator = locatorReader.ReadLocator(field);
            WaitForElementPresent(locator, 30);
            Assert.IsTrue(IsElementPresent(locator));
        }

        // Verify that eAddress label not changed.
        public void VerifyDropdownLabel()
        {
           var option1 = GetTextByXpath("//*[@id='ContactElectronicAddress0ElectronicContentLabel']/option[1]");
            WaitForWorkAround(3000);
            Console.Write("Option test is"+option1);
            Assert.IsTrue(option1.Contains("Work"));
        }
        // Verify Eaddress type not changed.
        public void VerifyDropdownType()
        {
            var option2 = GetTextByXpath("//*[@id='ContactElectronicAddress1ElectronicContentType']/option[1]");
            Console.Write("Option type is" +option2);
            WaitForWorkAround(3000);
            Assert.IsTrue(option2.Contains("E-Mail"));
        }

        // Select element by text
        public void SelectByText(string xmlNode ,string text)
        {
            var loc = locatorReader.ReadLocator(xmlNode);
            WaitForWorkAround(3000);
            SelectDropDownByText(loc ,text);
        }

        // Click and Check method
        public void CheckAndClick(string xmlNode)
        {
            var loc = locatorReader.ReadLocator(xmlNode);            
            if (IsElementPresent(loc))
            {
                Click(loc);
            }
            else
            {
                // Do nothing 
            }
        }

        //Verify Selected Options
        public void verifyselectedOptn(string xmlNode, string value)
        {
            String locator = locatorReader.ReadLocator(xmlNode);
            VerifySelectedOption(locator, value);
        }

    }
}