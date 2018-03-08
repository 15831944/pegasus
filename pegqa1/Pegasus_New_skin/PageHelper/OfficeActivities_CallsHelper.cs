using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using PegasusTests.Locators;
using PegasusTests.PageHelper.Comm;
using System;
using System.IO;
using OpenQA.Selenium.Support.UI;

namespace PegasusTests.PageHelper
{
    public class OfficeActivities_CallsHelper : DriverHelper
    {
        public LocatorReader locatorReader;

        public OfficeActivities_CallsHelper(IWebDriver idriver)
            : base(idriver)
        {
            locatorReader = new LocatorReader("OfficeActivities_Calls.xml");
        }

     
        // Type into given xml node
        public void TypeText(string Field, string text)
        {
            var locator = locatorReader.ReadLocator(Field);
            WaitForElementPresent(locator, 20);
            SendKeys(locator, text);
        }

        // Click Via Enter key
        public void PressEnter(string xmlNode)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(locator, 20);
            ClickViaEnter(locator);
        }

        // Verify text of given xml node
        public void VerifyText(string XmlNode, string text)
        {
            var locator = locatorReader.ReadLocator(XmlNode);
            var value = GetText(locator);
            WaitForWorkAround(1000);
            Assert.IsTrue(value.Contains(text));   
            //WaitForWorkAround(1000);
        }

        // Verify text of given xml node
        public void VerifyTextEqual(string tt)
        {
            var locator = "//table[@id='list1']/tbody/tr[2]/td[14]";
            var value = GetText(locator);
            WaitForWorkAround(2000);
            Assert.AreEqual(value, tt);
            WaitForWorkAround(10000);
        }

        // Select by value
        public void Select(string xmlNode, string value)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            SelectDropDown(locator, value);
        }

        // Select element by text
        public void SelectByText(string xmlNode, string text)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(locator, 20);
            SelectDropDownByText(locator, text);
        }

        // Delete All notes
        public void DeleteNotes()
        {
            var l = "//table[@id='list1']/tbody/tr[2]/td[4]/span/a[1]/span";
            var cnt = XpathCount("//table[@id='list1']/tbody/tr");
            Console.WriteLine("Xath Count is " + cnt);
            for (int i = 2; i < cnt; i++)
            {
                var ll = "//table[@id='list1']/tbody/tr[" + i + "]/td[4]/span/a[1]/span";
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

        // Click using java script method.
        public void ClickForce(string xmlNode)
        {
            var loc = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(loc ,10);
            ClickViaJavaScript(loc);
        }

        // DOuble Click on any given element.
        public void DblClick(string field)
        {
            var locator = locatorReader.ReadLocator(field);
            WaitForElementPresent(locator ,10);
            DoubleClick(locator);
        }


        // Verify Note History 
        public void VerifyNoteEdited(string editname)
        {
            int x = XpathCount("//table[@class='table table-bordered']/tbody/tr");
            for (int i = 1; i <= x; i++)
            {
                var verLoc = "//table[@class='table table-bordered']/tbody/tr[" + i + "]/td[3]";
                if (GetText(verLoc).Contains(editname))
                    Assert.IsTrue(GetText(verLoc).Contains(editname));
            }

        }

        // Upload file.
        public void Upload(string xmlNode,string fileName)
        {
            var loc = locatorReader.ReadLocator(xmlNode);
            UploadFile(loc,fileName);
            WaitForWorkAround(4000);
        }

        // Get selected text
        public string GetSelectdTxt(string xmlNode)
        {
            string loc = locatorReader.ReadLocator(xmlNode);
            string text = GetSelectedText(loc);
            return text;
        }

        // Verify selected option
        public void VerifySelectdTxt(string xmlNode, string text)
        {
            string loc = locatorReader.ReadLocator(xmlNode);
            VerifySelectedOption(loc,text);
           
        }

    }

}