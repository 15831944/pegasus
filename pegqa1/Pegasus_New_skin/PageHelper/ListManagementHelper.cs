using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using PegasusTests.Locators;
using PegasusTests.PageHelper.Comm;
using System;
using OpenQA.Selenium.Interactions;

namespace PegasusTests.PageHelper
{
    public class ListManagementHelper : DriverHelper
    {
        public LocatorReader locatorReader;

        public ListManagementHelper(IWebDriver idriver)
            : base(idriver)
        {
            locatorReader = new LocatorReader("ListManagement.xml");
        }


        // Click on duplicate link
        public void DudlicateClick()
        {
            var Save = "//button[@title='Save']";
            string Dudlicate = "//button[text()='Create Duplicate']";
            Click(Save);
            WaitForWorkAround(3000);
            if (IsElementPresent(Dudlicate))
            {
                Click(Dudlicate);
            }
        }

        // Click on the displayed element.
        public void ClickOnDisplayed(string xml)
        {
            var loc = locatorReader.ReadLocator(xml);
            WaitForElementPresent(loc, 20);
            ClickDisplayed(loc);

        }

        // Click Via Enter key
        public void PressEnter(string xmlNode)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(locator, 20);
            ClickViaEnter(locator);
        }

        // Delete leads from recyclebin.
        public void DeletesLeads()
        {
            var l = "//table[@id='list1']/tbody/tr[2]/td[6]/div/a[1]";
            var cnt = XpathCount("//table[@id='list1']/tbody/tr");
            Console.WriteLine("Xath Count is " + cnt);
            for (int i = 19; i < cnt; i++)
            {
                var ll = "//table[@id='list1']/tbody/tr[" + i + "]/td[6]/div/a[1]";
                Console.WriteLine("loc path is " + ll);
                WaitForElementPresent(ll, 10);
                Click(ll);
                AcceptAlert();
                WaitForWorkAround(6000);
            }
        }

        // Type into given xml node
        public void TypeText(string Field, string text)
        {
            var locator = locatorReader.ReadLocator(Field);
            WaitForElementPresent(locator, 20);
            ClearTextBoxValue(locator);
            SendKeys(locator, text);
        }

        // Select element by text.
        public void SelectByText(string xmlNode, string text)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            SelectDropDownByText(locator, text);
        }

        // Verify text of given xml node
        public void VerifyText(string XmlNode, string text)
        {
            var locator = locatorReader.ReadLocator(XmlNode);
            var value = GetText(locator);
            Console.WriteLine(value);
            WaitForWorkAround(3000);
            Assert.IsTrue(value.Contains(text));
            WaitForWorkAround(2000);
        }

        // Verify value of given xml node
        public void VerifyValue(string XmlNode, string value)
        {
            var locator = locatorReader.ReadLocator(XmlNode);
            var value1 = GetValue(locator);
            WaitForWorkAround(3000);
            Assert.IsTrue(value1.Contains(value));
            WaitForWorkAround(2000);
        }


        // Select by value
        public void Select(string xmlNode, string value)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            SelectDropDown(locator, value);
        }

        // Click element via xml node. 
        public void ClickElement(string xmlNode)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(locator, 20);
            Click(locator);
            WaitForWorkAround(3000);
        }

        // Click element via Java Script. 
        public void ClickJS(string xmlNode)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(locator, 20);
            ClickViaJavaScript(locator);
            WaitForWorkAround(3000);
        }


        public void ClickDouble(string xmlNode)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(locator, 20);
            DoubleClick(locator);
            WaitForWorkAround(3000);
        }

        //
        public void verifyAddress1()
        {
            var val = GetAtrributeByXpath("//*[@id='LeadDetailMailingAddressLine1']", "Value");
            WaitForWorkAround(3000);
            Assert.IsTrue(val.Contains("test line 1"));
        }

        //
        public void verifyAddress2()
        {
            var val = GetAtrributeByXpath("//*[@id='LeadDetailLocationAddressLine2']", "Value");
            WaitForWorkAround(3000);
            Assert.IsTrue(val.Contains("line 2"));
        }
        // Upload a file
        public void Upload(string XmlNode, string File)
        {
            var locator = locatorReader.ReadLocator(XmlNode);
            WaitForElementPresent(locator, 20);
            UploadFile(locator, File);
        }

        // Decline Alert
        public void DeclineAlert()
        {
            WaitForWorkAround(3000);
            GetWebDriver().SwitchTo().Alert().Dismiss();
        }

        // Vereify whether the element is present.
        public void verifyElementVisible(string field)
        {
            var locator = locatorReader.ReadLocator(field);
            WaitForElementPresent(locator, 30);
            WaitForElementVisible(locator, 30);
            Assert.IsTrue(IsElementVisible(locator));
        }

        // export an excel file as csv.
        public void ExportAs(string type)
        {
            GetWebDriver().Navigate().Refresh();
            string locator = "//div[@title='Export']/button";
            WaitForElementVisible(locator, 30);
            Click(locator);
            string newlocator = "";
            if (type == "CSV")
            {
                newlocator = "//*[@id='export_csv']";
                WaitForElementVisible(newlocator, 30);
            }
            else
            {
                newlocator = "//*[@id='export_excel']";
                WaitForElementVisible(newlocator, 30);
            }
            Click(newlocator);
        }

        // Assert Given Element not tpresent for selection.
        public void EmployeeNotAvail()
        {
            var owner = GetTextByXpath("//*[@id='LeadAssignedUserId']");
            Console.WriteLine(owner);
            WaitForWorkAround(5000);
            Assert.IsFalse(owner.Contains("New EMployee"));
        }

        // Click Forcefully on any element.
        public void ClickForce(string XmlNode)
        {
            var Locator = locatorReader.ReadLocator(XmlNode);
            WaitForWorkAround(2000);
            ClickViaJavaScript(Locator);
        }

        // Assert Given Element not tpresent for selection.
        public void PAgentNotAvail()
        {
            var owner = GetTextByXpath("//*[@id='LeadPartnerAgentId']");
            Console.WriteLine(owner);
            WaitForWorkAround(5000);
            Assert.IsFalse(owner.Contains("Partner Test"));
        }

        // Assert Given Element not tpresent for selection.
        public void PAssocNotAvail()
        {
            var owner = GetTextByXpath("//*[@id='LeadPartnerAssociationId']");
            Console.WriteLine(owner);
            WaitForWorkAround(5000);
            Assert.IsFalse(owner.Contains("PAssoTester"));
        }

        // Verify Adree copied from location to mailing.
        public void VerifyCheckBox(string text)
        {
            var location = GetAtrributeByLocator("//*[@id='LeadDetailMailingAddressLine1']", "value");
            WaitForWorkAround(5000);
            Assert.IsTrue(location.Contains(text));
        }

        // Verify lead time updated on editing lead
        public void VerifyTime()
        {
            var aa = GetTextByXpath("//table[@id='list1']/tbody/tr[2]/td[7]");
            Console.WriteLine(aa);
        }

        public void VerifyTextNotVisible(string text)
        {
            string locator = "//*[contains(text(),'" + text + "')]";
            bool b = IsElementVisible(locator);
            if(b == true)
            {
                Console.WriteLine("Text Present");
            }
            else{
                Console.WriteLine("Text Not Present");
            }
        }

        // verify text box value
        public void VerifyTextBoxValue(string xmlNode, string text)
        {
            var loc = locatorReader.ReadLocator(xmlNode);
            var inputtxt = getInputText(loc);
            Assert.AreEqual(text,inputtxt);
        }

        // verify selected option
        public void verifyselectedoptn(string xmlNode, string text)
        {
            var loc = locatorReader.ReadLocator(xmlNode);
            VerifySelectedOption(loc,text);
        }

        // Verify presence of any element.
        public void verifyElementPresent(string field)
        {
            var locator = locatorReader.ReadLocator(field);
            WaitForElementPresent(locator, 30);
            Assert.IsTrue(IsElementPresent(locator));
        }
 
    }
}