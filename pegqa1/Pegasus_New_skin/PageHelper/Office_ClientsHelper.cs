/* Documented by Khalil Shakir 

This helper class is used in conjucntion with the OfficeMainFieldRelationship.cs file
as well as the Office_Clients.xml file. 

*/

using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using PegasusTests.Locators;
using PegasusTests.PageHelper.Comm;
using System;
using System.Collections.Generic;
using Excel = Microsoft.Office.Interop.Excel;
using System.Linq;
using System.Text;
using System.IO;

namespace PegasusTests.PageHelper
{
    public class Office_ClientsHelper : DriverHelper
    {
        public LocatorReader locatorReader;

        public Office_ClientsHelper(IWebDriver idriver)
            : base(idriver)
        {
            locatorReader = new LocatorReader("Office_Clients.xml");
        }

        //Type into given xml node
        public void TypeText(string Field, string text)
        {
            String locator = locatorReader.ReadLocator(Field);
            WaitForElementPresent(locator, 20);
            WaitForElementVisible(locator, 20);
            SendKeys(locator, text);
        }

        //Verify text of given xml node
        public void VerifyText(string XmlNode, string text)
        {
            String locator = locatorReader.ReadLocator(XmlNode);
            String value = GetText(locator);
            Assert.IsTrue(value.Contains(text));
        }

        //Verify text of given xml node
        public void VerifyTextBoxValue(string XmlNode, string text)
        {
            String locator = locatorReader.ReadLocator(XmlNode);
            String value = getInputText(locator);
            Assert.IsTrue(value.Contains(text));
        }

        //Select by value
        public void Select(string xmlNode, string value)
        {
            String locator = locatorReader.ReadLocator(xmlNode);
            SelectDropDown(locator, value);
        }

        //Verify Selected Options
        public void verifyselectedOptn(string xmlNode, string value)
        {
            String locator = locatorReader.ReadLocator(xmlNode);
            VerifySelectedOption(locator, value);
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

        //Click Via Enter key
        public void PressEnter(string xmlNode)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(locator, 20);
            ClickViaEnter(locator);
        }

        //Delete All Clients
        public void DelteAllClients()
        {
            var cnt = XpathCount("//table[@id='list1']/tbody/tr");
            Console.WriteLine("Xath Count is " + cnt);
            for (int i = 10; i < cnt; i++)
            {
                var ll = "//table[@id='list1']/tbody/tr[" + i + "]/td[7]/div/a[1]";
                Console.WriteLine("loc path is " + ll);
                Click(ll);
                AcceptAlert();
                WaitForWorkAround(3000);
            }
        }

        //Select by text
        public void SelectByText(string xmlNode, string text)
        {
            String locator = locatorReader.ReadLocator(xmlNode);
            SelectDropDownByText(locator, text);
        }

        // to verify entered email on client page.
        public void VerifyEmail(string email, string text)
        {
            var loc = "//span[text()='" + email + "']";
            string el = GetText(loc);
            Assert.IsTrue(el.Contains(text));
        }

        //Element Present
        public void ElementPresent(string xmlNode)
        {
            String locator = locatorReader.ReadLocator(xmlNode);
            IsElementPresent(locator);
        }


        //Click Dispalyed
        public void ClickOnDisplayed(string xmlNode)
        {
            String locator = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(locator, 20);
            ClickDisplayed(locator);
        }


        // Scroll to element
        public void scrollToElement(string field)
        {
            var locator = locatorReader.ReadLocator(field);
            WaitForElementPresent(locator, 30);
            WaitForWorkAround(2000);
            ScrollDown(locator);
        }

        // Verify Added Column
        public void verifyColumnAdded(string xmlNode)
        {
            String locator = locatorReader.ReadLocator(xmlNode);
            WaitForWorkAround(2000);
            Assert.IsTrue(IsElementPresent(locator));
        }

        // FOr exportin file
        public void ExportAs(string type)
        {
            GetWebDriver().Navigate().Refresh();
            String locator = "//div[@title='Export']/button";
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


        // Verify partner association.
        public bool VerifyPartnerAssociation(string xmlNode)
        {
            String locator = locatorReader.ReadLocator(xmlNode);
            WaitForWorkAround(3000);
            bool result = IsElementPresent(locator);
            if (result)
            {
                WaitForElementVisible(locator, 30);
            }
            return result;
        }
        // To verify location
        public void verifyLocationSaved(string field, int rand)
        {
            var locator = locatorReader.ReadLocator(field);
            WaitForElementPresent(locator, 30);
            string value = GetAtrributeByLocator(locator, "value");
            Assert.IsTrue(value.Contains(rand.ToString()));
        }

        // Mouse hover
        internal void MouseHover(string field)
        {
            var locator = locatorReader.ReadLocator(field);
            WaitForElementPresent(locator, 20);
            WaitForWorkAround(2000);
            MouseOver(locator);
        }

        //Click Element

        public void ClickElement(string xmlNode)
        {
            String locator = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(locator, 20);
            WaitForElementVisible(locator, 20);
            Click(locator);
        }

        public void ClickElements(string xmlNode, int index)
        {
            String locator = locatorReader.ReadLocator(xmlNode);
            IList<IWebElement> options = _driver.FindElements(By.XPath(locator));
            Console.WriteLine("testing" + options.Count());
        }

        // Click using javascript method.
        public void ClickForce(string xmlNode)
        {
            var loc = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(loc, 10);
            ClickViaJavaScript(loc);
        }
        // Assert Given Element not tpresent for selection.
        public void EmployeeNotAvail()
        {
            var owner = GetTextByXpath("//*[@id='ClientAssignedUserId']");
            Console.WriteLine(owner);
            WaitForWorkAround(5000);
            Assert.IsFalse(owner.Contains("aslam employee"));
        }

        // Assert Given Element not tpresent for selection.
        public void PAgentNotAvail()
        {
            var owner = GetTextByXpath("//*[@id='ClientPartnerAgentId']");
            Console.WriteLine(owner);
            WaitForWorkAround(5000);
            Assert.IsFalse(owner.Contains("Aslam P.Agent"));
        }

        // Assert Given Element not tpresent for selection.
        public void PAssocNotAvail()
        {
            var owner = GetTextByXpath("//*[@id='ClientPartnerAssociationId']");
            Console.WriteLine(owner);
            WaitForWorkAround(5000);
            Assert.IsFalse(owner.Contains("AslamP.Association."));
        }

        // Upload a file.

        public void Upload(string Field, string Filename)
        {
            String locator = locatorReader.ReadLocator(Field);
            GetWebDriver().FindElement(ByLocator(locator)).SendKeys(Filename);
            WaitForWorkAround(3000);

        }

        // Verify Not present element
        public void verifyElementNotPresent(string field)
        {
            var locator = locatorReader.ReadLocator(field);
            WaitForWorkAround(2000);
            Assert.IsFalse(IsElementPresent(locator));
        }

        // Verify element Visible
        public void verifyElementVisible(string field)
        {
            var locator = locatorReader.ReadLocator(field);
            WaitForElementPresent(locator, 30);
            WaitForElementVisible(locator, 30);
            Assert.IsTrue(IsElementVisible(locator));
        }

        // Verify present element
        public void verifyElementPresent(string field)
        {
            var locator = locatorReader.ReadLocator(field);
            WaitForElementPresent(locator, 30);
            Assert.IsTrue(IsElementPresent(locator));
        }

        // Verify element is displayed on the page
        public void VerifyElementDisplayed(string xmlnode)
        {
            var loc = locatorReader.ReadLocator(xmlnode);
            WaitForWorkAround(2000);
            IsElementVisible(loc);
        }

        // Verify Product details not removed when click save.
        public void ProductData(string text)
        {
            var data = GetAtrributeByLocator("//*[@id='ProductCustomField1167Value']", "value");
            Assert.IsTrue(data.Contains(text));
        }

        //Read Data 

        public void ReadData12()
        {
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            Excel.Range xlrange;

            string xlString;
            int xlRowCnt = 0;
            int xlColCnt = 0;

            xlApp = new Excel.Application();
            //Open Excel file
            xlWorkBook = xlApp.Workbooks.Open(@"D:\client");
            //xlWorkBook = xlApp.Workbooks.Open(@"D:\client1", 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            //This gives the used cells in the sheet

            xlrange = xlWorkSheet.UsedRange;
            Console.WriteLine("Row count is  " + xlrange.Rows.Count);

            Console.WriteLine("Column count is  " + xlrange.Columns.Count);


            for (xlRowCnt = 1; xlRowCnt <= xlrange.Rows.Count; xlRowCnt++)
            {
                //   for (xlColCnt = 1; xlColCnt <= xlrange.Columns.Count; xlColCnt++)
                //  {


                //DBA Name
                xlString = (string)(xlrange.Cells[xlRowCnt, 1] as Excel.Range).Value2;
                GetWebDriver().FindElement(By.Id("ClientDetailCompanyDbaName")).SendKeys(xlString);
                Console.WriteLine(xlString);

                //Status
                xlString = (string)(xlrange.Cells[xlRowCnt, 2] as Excel.Range).Value2;
                SelectDropDownByText("//*[@id='ClientStatus']", xlString);
                Console.WriteLine(xlString);

                //Responsibity
                xlString = (string)(xlrange.Cells[xlRowCnt, 3] as Excel.Range).Value2;
                SelectDropDownByText("//*[@id='ClientAssignedUserId']", xlString);
                Console.WriteLine(xlString);

                //Save
                Click("//button[@title='Save']");
                WaitForWorkAround(10000);

                GetWebDriver().Navigate().GoToUrl("https://www.mypegasuscrm.com/newthemecorp/newthemeoffice/clients/create");
                WaitForWorkAround(6000);
            }

            //   }
        }






        // Verify State,Country Auto Populating.
        public void VerifyAutoPopulation()
        {
            WaitForWorkAround(3000);
            var cd = GetAtrributeByXpath("//*[@id='ClientDetailMailingCity']", "value");
            WaitForWorkAround(3000);
            Console.WriteLine("value is" + cd);
            Assert.IsTrue(cd.Contains("Chicago"));
            WaitForWorkAround(5000);
     
        }

        // Verify 9 digits SSN
        public void verify9digtsSSN(string xmlNode)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(locator, 30);
            Assert.IsTrue(IsElementPresent(locator));
            var text = getInputText(locator);
            Assert.IsTrue(text.Length==11);
        }

        // Click via JavaScript
        public void ClickJS(string xmlNode)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(locator, 30);
            ClickViaJavaScript(locator);
        }

    }
}
