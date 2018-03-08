using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using PegasusTests.Locators;
using PegasusTests.PageHelper.Comm;
using System;
using System.IO;
using OpenQA.Selenium.Support.UI;

namespace PegasusTests.PageHelper
{
    public class OfficeActivities_NotesHelper : DriverHelper
    {
        public LocatorReader locatorReader;

        public OfficeActivities_NotesHelper(IWebDriver idriver)
            : base(idriver)
        {
            locatorReader = new LocatorReader("OfficeActivities_Notes.xml");
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
            Assert.IsTrue(value.Contains(text));
        }

        // Select by value
        public void Select(string xmlNode, string value)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            SelectDropDown(locator, value);
        }

        //Verify the No of showing page
        public void ShowResult(int Value)
        {
            int el4 = XpathCount("//table[@id='list1']//tbody//tr");
            Console.WriteLine("el4 is  " + el4);
            if (Value < el4)
            {
                Assert.IsTrue(Value < el4);
            }
            else
            {
                Assert.IsFalse(Value < el4);
            }
        }

        // Select element by text
        public void SelectByText(string xmlNode, string text)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            SelectDropDownByText(locator ,text);
        }

        // Delete All notes
        public void DeleteNotes()
        {
            var l = "//table[@id='list1']/tbody/tr[2]/td[4]/span/a[1]/span";
            var cnt = XpathCount("//table[@id='list1']/tbody/tr");
            Console.WriteLine("Xath Count is " + cnt);
            for (int i = 12; i < cnt; i++)
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

        // Click using Java script method.
        public void ClickForce(string xmlNode)
        {
            var loc = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(loc ,10);
            ClickViaJavaScript(loc);
        }

      
        // Verify Note History 
        public void VerifyNoteEdited(string editname)
        {
            int x = XpathCount("//table[@class='table table-bordered']/tbody/tr");
            for (int i = 1; i <= x; i++)
            {
                Console.WriteLine(i);
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
    }

}