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
    public class Office_OpportunitiesHelper : DriverHelper
    {
        public LocatorReader locatorReader;

        public Office_OpportunitiesHelper(IWebDriver idriver)
            : base(idriver)
        {
            locatorReader = new LocatorReader("Office_Opportunities.xml");
        }


        //Type into given xml node
        public void TypeText(string Field, string text)
        {
            var locator = locatorReader.ReadLocator(Field);
            WaitForElementPresent(locator, 20);
            WaitForElementEnabled(locator, 20);
            SendKeys(locator, text);
        }

        //Delete All
        public void DeleteOpportunity()
        {
            var l = "//table[@id='list1']/tbody/tr[2]/td[4]/div/a[1]";
            var cnt = XpathCount("//table[@id='list1']/tbody/tr");
            Console.WriteLine("Xath Count is " + cnt);
            for (int i = 12; i < cnt; i++)
            {
                var ll = "//table[@id='list1']/tbody/tr["+i+"]/td[4]/div/a[1]";
                Console.WriteLine("loc path is "+ll);
                Click(ll);
                AcceptAlert();
                WaitForWorkAround(3000);
            }
        }

        //Verify text of given xml node
        public void VerifyText(string XmlNode, string text)
        {
            var locator = locatorReader.ReadLocator(XmlNode);
            var value = GetText(locator);
            Console.WriteLine(value);
            Assert.IsTrue(value.Contains(text));
        }

        // Click on displayed element
        public void ClickOnDisplayed(string xml)
        {
            var loc = locatorReader.ReadLocator(xml);
            WaitForElementPresent(loc, 20);
            ClickDisplayed(loc);
        }

        // Verify opportunity name
        public void VerifyName(string text)
        {
            var loc = "//div[contains(text(),'"+text+"')]";
            Assert.IsTrue(IsElementPresent(loc));
        }

        // Verify opportunity company name.
        public void VerifyCompName(string text)
        {
            var loc = "//div[text()='" + text + "']";
            Assert.IsTrue(IsElementPresent(loc));
        }

        //Select by value
        public void Select(string xmlNode, string value)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            SelectDropDown(locator, value);
        }

        // Click on given xml node.
        public void ClickElement(string xmlNode)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(locator, 20);
            Click(locator);
        }

        // Click the elememy via java
        public void clickJS(string xmlNode)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(locator, 20);
            ClickViaJavaScript(locator);
        }

        // Click force fully on any element
        public void ClickForce(string xmlNode)
        {
            var loc = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(loc ,10);
            ClickViaJavaScript(loc);
        }

        // Decline Alert
        public void DeclineAlert()
        {
            WaitForWorkAround(3000);
            GetWebDriver().SwitchTo().Alert().Dismiss();
        }

        // Upload File
        public void Upload(string Field, string file)
        {
            String locator = locatorReader.ReadLocator(Field);
            WaitForElementPresent(locator, 20);
            UploadFile(locator, file);
        }

        // Get text of text box
        public void VerifySelectedValue(string xmlNode, string value)
        {
            
            String locator = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(locator, 20);
            VerifySelectedOption(locator,value);
            
        }

        // Mouse move over any given element
        internal void MouseHover(string field)
        {
            var locator = locatorReader.ReadLocator(field);
            WaitForElementPresent(locator, 20);
            WaitForWorkAround(2000);
            MouseOver(locator);
        }

        // Verify presence of given element
        public void verifyElementPresent(string field)
        {
            var locator = locatorReader.ReadLocator(field);
            WaitForElementPresent(locator, 30);
        Assert.IsTrue(IsElementPresent(locator));
        }

        // Select element by text.
        public void SelectByText(string xmlNode, string text)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(locator, 20);
            SelectDropDownByText(locator, text);
        }

        // Assert Given Element not tpresent for selection.
        public void EmployeeNotAvail()
        {
            var owner = GetTextByXpath("//*[@id='OpportunityAssignedUserId']");
            Console.WriteLine(owner);
            WaitForWorkAround(5000);
            Assert.IsFalse(owner.Contains("asl emp"));
        }

        // Assert Given Element not tpresent for selection.
        public void PAgentNotAvail()
        {
            var owner = GetTextByXpath("//*[@id='OpportunityPartnerAgentId']");
            Console.WriteLine(owner);
            WaitForWorkAround(2000);
            Assert.IsFalse(owner.Contains("Mark Menu"));
        }

        // Assert Given Element not tpresent for selection.
        public void PAssocNotAvail()
        {
            var owner = GetTextByXpath("//*[@id='OpportunityPartnerAssociationId']");
            Console.WriteLine(owner);
            WaitForWorkAround(5000);
            Assert.IsFalse(owner.Contains("TestAssociation"));
        }

        public void VerifyCallOption(string xmlNode)
        {
            int pass = 0, fail = 0;
            var locator = locatorReader.ReadLocator(xmlNode);
            for (var i = 1; i <= 7; i++)
            {
                var new1 = GetTextByXpath(locator + "/option[" + i + "]");
                if (new1.Contains("Calls"))
                    pass++;
                else
                    fail++;
            }
            Assert.IsTrue(pass == 1);
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

    }
}