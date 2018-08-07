using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class VerifyMappingOfClone_of_CoCardEnhancedBillback2105_ia_Final_co_br : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        //[TestCategory("Bug")]
        //[TestCategory("TS9")]
        [TestCategory("PDFMapping")]
        public void verifyMappingOfClone_of_CoCardEnhancedBillback2105_ia_Final_co_br()
        {
            string[] username = null;
            string[] password = null;

            XMLParse oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var pDFTemplate_PDFTemplateHelper = new PDFTemplate_PDFTemplateHelper(GetWebDriver());
            var office_ClientsHelper = new Office_ClientsHelper(GetWebDriver());
            var pdffilesHelper = new PDFfilesHelper(GetWebDriver());

            // Variable 
            var name = "Test" + GetRandomNumber();
            var name2 = "Testlist" + GetRandomNumber();
            var Id = "12345" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("VerifyMappingOfClone_of_CoCardEnhancedBillback2105_ia_Final_co_br", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyMappingOfClone_of_CoCardEnhancedBillback2105_ia_Final_co_br", "Go to PDF templates page");
                VisitOffice("pdf_templates");
                pDFTemplate_PDFTemplateHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyMappingOfClone_of_CoCardEnhancedBillback2105_ia_Final_co_br", "Search for required pdf");
                pDFTemplate_PDFTemplateHelper.TypeText("EnterPDFToSearch", "Clone of CoCardEnhancedBillback2105_ia_Final co-br");
                pDFTemplate_PDFTemplateHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyMappingOfClone_of_CoCardEnhancedBillback2105_ia_Final_co_br", "Click on PDF Halo Edit");
                pDFTemplate_PDFTemplateHelper.ClickElement("PDFHaloEdit");
                pDFTemplate_PDFTemplateHelper.WaitForWorkAround(2000);
                pDFTemplate_PDFTemplateHelper.ClickElement("SelectVisualMap");
                pDFTemplate_PDFTemplateHelper.ClickElement("SubmitMapOptn");
                pDFTemplate_PDFTemplateHelper.WaitForElementPresent("//input[@name='visaPartialAuth']", 20);

                executionLog.Log("VerifyMappingOfClone_of_CoCardEnhancedBillback2105_ia_Final_co_br", "Check mapping");
                pdffilesHelper.ClickElement("VisaCredit_DiscRate");
                pdffilesHelper.WaitForWorkAround(3000);
                string tab1 = pdffilesHelper.getInputText("//*[@id='directmapdiv']/div[2]/div[1]/div[1]/div/button");
                string section1 = pdffilesHelper.getInputText("//*[@id='directmapdiv']/div[2]/div[1]/div[2]/div/button");
                string field1 = pdffilesHelper.getInputText("//*[@id='directmapdiv']/div[2]/div[1]/div[4]/div/button");

                GetWebDriver().Navigate().GoToUrl("https://www.mypegasuscrm.com/newthemecorp/pegasustestoffice/clients/view/200866");
                pdffilesHelper.WaitForWorkAround(2000);
                if(tab1=="Rates & Fees")
                office_ClientsHelper.ClickElement("RatesAndFee");
                else
                { }

                //Boolean test = Assert.IsTrue(pDFTemplate_PDFTemplateHelper.IsElementPresent("//*[@id='ClientRatesFeeAmexRate']"));
                if (office_ClientsHelper.IsElementPresent("//*[@id='ClientRatesFeeAmexRate']")==true)
                {
                    office_ClientsHelper.TypeText("Amexp", "61");
                    office_ClientsHelper.ClickElement("RandFSave");
                    office_ClientsHelper.WaitForWorkAround(2000);
                }
                else
                {

                }

                GetWebDriver().Navigate().GoToUrl("https://www.mypegasuscrm.com/newthemecorp/pegasustestoffice/clients/pdfs/200866/html/602450");
                office_ClientsHelper.WaitForWorkAround(4000);

                string value = pDFTemplate_PDFTemplateHelper.getInputText("//*[@id='qualRate']");

                Assert.AreEqual("61", value);







            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CustomColumnsLead");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Custom Columns Lead");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Custom Columns Lead", "Bug", "Medium", "Equipment page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Custom Columns Lead");
                        TakeScreenshot("CustomColumnsLead");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CustomColumnsLead.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CustomColumnsLead");
                        string id = loginHelper.getIssueID("Custom Columns Lead");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CustomColumnsLead.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Custom Columns Lead"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Custom Columns Lead");
                //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("CustomColumnsLead");
                executionLog.WriteInExcel("Custom Columns Lead", Status, JIRA, "List Management");
            }
        }
    }
}