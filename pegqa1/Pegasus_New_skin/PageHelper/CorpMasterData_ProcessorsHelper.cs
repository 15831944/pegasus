using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using PegasusTests.Locators;
using PegasusTests.PageHelper.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;

namespace PegasusTests.PageHelper
{
    public class CorpMasterData_ProcessorsHelper : DriverHelper
    {
        public LocatorReader locatorReader;

        public CorpMasterData_ProcessorsHelper(IWebDriver idriver)
            : base(idriver)
        {
            locatorReader = new LocatorReader("CorpMasterData_Processors.xml");
        }

        // Type into given xml node
        public void TypeText(string Field, string text)
        {
            String locator = locatorReader.ReadLocator(Field);
            WaitForElementPresent(locator, 20);
            WaitForElementVisible(locator, 20);
            SendKeys(locator, text);
        }

        // Verify text of given xml node
        public void VerifyText(string XmlNode, string text)
        {
            String locator = locatorReader.ReadLocator(XmlNode);
            String value = GetText(locator);
            Assert.IsTrue(value.Contains(text));
        }

        // Verify availability of avtar
        public bool verifyAvatarAvailable(string text)
        {
            WaitForWorkAround(5000);
            bool result = GetWebDriver().PageSource.Contains(text);

            return result;

        }

        // For deleting any processor.

        public void deleteProcessor(string ProcessName)
        {
            string locator = "//table[@id='list1']/tbody/tr";
            WaitForElementVisible("//table[@id='list1']/tbody/tr[2]", 50);
            TypeText("ProceNameFilter", ProcessName);
            WaitForWorkAround(5000);
            string newlocator = "//table[@id='list1']/tbody/tr/td/a[@title='Delete Processor']";
            Click(newlocator);
            AcceptAlert();
            WaitForText("The processor is successfully deleted!!", 10);
        }

        //Select by value
        public void Select(string xmlNode, string value)
        {
            String locator = locatorReader.ReadLocator(xmlNode);
            SelectDropDown(locator, value);
        }

        //Select by text
        public void SelectByText(string xmlNode, string text)
        {
            String locator = locatorReader.ReadLocator(xmlNode);
            SelectDropDownByText(locator, text);
        }

        //Click element
        public void ClickElement(string xmlNode)
        {
            String locator = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(locator, 20);
            WaitForElementVisible(locator, 20);
            Click(locator);
        }

        public string GetPathToFile()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var ab = currentDirectory.Split(new[] { "bin" }, StringSplitOptions.None);
            var a = ab[0];
            var fPath = a + "Files\\";
            Console.Write("file path: " + fPath);
            return fPath;
        }

        //Read Data 
        public void ReadClient_Excel()
        {
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            Excel.Range xlrange;

            string xlString;
            string xlString1;
            double xlDouble;
            int xlRowCnt = 0;
            int xlColCnt = 0;

            xlApp = new Excel.Application();
            //Open Excel file
            var locFile = GetPathToFile() + "CorporateProcessor.xlsx";
            //var updated_path = locFile.Replace("Files", "bin");
            xlWorkBook = xlApp.Workbooks.Open(locFile);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            //This gives the used cells in the sheet
            xlrange = xlWorkSheet.UsedRange;
            Console.WriteLine("Row count is  " + xlrange.Rows.Count);

            for (xlRowCnt = 2; xlRowCnt <= xlrange.Rows.Count; xlRowCnt++)
            {
                //Navigate to Create Processor page
                //WaitForWorkAround(2000);
                //GetWebDriver().Navigate().GoToUrl("https://www.mypegasuscrm.com/newthemecorp/masterdata/manage_processors");
                //WaitForWorkAround(3000);

                //Processor Name
                xlString = (string)(xlrange.Cells[xlRowCnt, 1] as Excel.Range).Value2;
                if (xlString == null)
                {
                    xlString1 = (string)(xlrange.Cells[(xlRowCnt + 1), 1] as Excel.Range).Value2;
                    if (xlString1 != null)
                    {
                        xlRowCnt++;
                    }
                    continue;
                }

                //Check for already exists
                SendKeys("//*[@id='gs_processor_name']", xlString);
                WaitForWorkAround(3000);
                if (IsElementPresent("//table[@id='list1']/tbody/tr[2]/td[4]/a") == true)
                    continue;
                else
                {
                    GetWebDriver().Navigate().GoToUrl("https://www.mypegasuscrm.com/newthemecorp/masterdata/manage_processors");
                    WaitForWorkAround(3000);
                    SendKeys("//*[@id='CorporateMasterProcessorNewProcessorName']", xlString);

                    //Processor Code      
                    xlDouble = (double)(xlrange.Cells[xlRowCnt, 2] as Excel.Range).Value2;
                    xlString = "" + xlDouble;
                    SendKeys("//*[@id='CorporateMasterProcessorProcessorCode']", xlString);

                    if (xlrange.Columns.Count == 3)
                    {
                        //Processor Fetch Field
                        xlString = (string)(xlrange.Cells[xlRowCnt, 3] as Excel.Range).Value2;
                        if (xlString != null)
                            SelectDropDownByText("//select[@id='CorporateMasterProcessorFieldFromProcessor']", xlString);
                        else
                        { }
                    }
                }

                //Click on Save button
                Click("//a[@title='Save']");
                WaitForWorkAround(3000);

            }
        }
    }
}
