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
    public class PDFImportWizardMaping : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS1")]
        [TestCategory("BugTest")]
        public void pDFImportWizardMaping()
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
            var pDFTemplate_ImportWizardHelper = new PDFTemplate_ImportWizardHelper(GetWebDriver());

            // Variable
            var filename = GetPathToFile() + "2.pdf";
            var name = "Test" + RandomNumber(1, 99);
            String JIRA = "";
            String Status = "Pass";


            try
            {
                executionLog.Log("PDFImportWizardMaping", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("PDFImportWizardMaping", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("PDFImportWizardMaping", "Redirect to Import PDF page.");
                VisitOffice("pdf_templates/import");

                executionLog.Log("PDFImportWizardMaping", "Select Module");
                pDFTemplate_ImportWizardHelper.Select("SelectModule", "20");

                executionLog.Log("PDFImportWizardMaping", "Upload pdf file.");
                pDFTemplate_ImportWizardHelper.upload("SelectFile", filename);

                executionLog.Log("PDFImportWizardMaping", "Click On Import");
                pDFTemplate_ImportWizardHelper.ClickElement("Import");
                pDFTemplate_ImportWizardHelper.WaitForWorkAround(2000);

                executionLog.Log("PDFImportWizardMaping", "Wait for element to present.");
                pDFTemplate_ImportWizardHelper.WaitForElementPresent("Next", 20);

                executionLog.Log("PDFImportWizardMaping", "Click On Next");
                pDFTemplate_ImportWizardHelper.ClickElement("Next");
                pDFTemplate_ImportWizardHelper.WaitForWorkAround(1000);

                executionLog.Log("PDFImportWizardMaping", "Select Category");
                pDFTemplate_ImportWizardHelper.SelectDropDownByText("//*[@id='PdfTemplatePdfCategoryId']", "Check Processing");

                executionLog.Log("PDFImportWizardMaping", "Click on Save button");
                pDFTemplate_ImportWizardHelper.ClickElement("Save1");
                pDFTemplate_ImportWizardHelper.WaitForWorkAround(1000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("PDFImportWizardMaping");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("PDF Import Wizard Maping");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("PDF Import Wizard Maping", "Bug", "Medium", "PDF Import Wizard page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("PDF Import Wizard Maping");
                        TakeScreenshot("PDFImportWizardMaping");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\PDFImportWizardMaping.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("PDFImportWizardMaping");
                        string id = loginHelper.getIssueID("PDF Import Wizard Maping");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\PDFImportWizardMaping.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("PDF Import Wizard Maping"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("PDF Import Wizard Maping");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("PDFImportWizardMaping");
                executionLog.WriteInExcel("PDF Import Wizard Maping", Status, JIRA, "Office PDF Templates");
            }
        }
    }
}