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
    public class PDFImportWizard : DriverTestCase
    {
        [TestMethod]
        [TestCategory("Admin1")]
        [TestCategory("All")]
        [TestCategory("Fail")]
        [TestCategory("TS7")]
        [TestCategory("Pegasus_Admin1")]
        public void pDFImportWizard()
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
            var name = "Test" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("PDFImportWizard", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("PDFImportWizard", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("PDFImportWizard", "Click On  Admin");
                VisitOffice("admin");

                executionLog.Log("PDFImportWizard", "Redirect");
                VisitOffice("pdf_templates/import");

                executionLog.Log("PDFImportWizard", "Verify title");
                VerifyTitle("PDF Import Wizard");

                executionLog.Log("PDFImportWizard", "Choose Module");
                pDFTemplate_ImportWizardHelper.Select("SelectModule", "20");

                executionLog.Log("PDFImportWizard", "Upload PDF File");
                String filename = GetPathToFile() + "2.pdf";
                pDFTemplate_ImportWizardHelper.upload("SelectFile", filename);
                pDFTemplate_ImportWizardHelper.WaitForWorkAround(1000);

                executionLog.Log("PDFImportWizard", "Click On Import");
                pDFTemplate_ImportWizardHelper.ClickElement("Import");
                pDFTemplate_ImportWizardHelper.WaitForWorkAround(5000);

                executionLog.Log("PDFImportWizard", "Click on next button.");
                pDFTemplate_ImportWizardHelper.ClickElement("Next");
                pDFTemplate_ImportWizardHelper.WaitForWorkAround(3000);

                executionLog.Log("PDFImportWizard", "verify title");
                VerifyTitle("PDF Import Wizard");

                executionLog.Log("PDFImportWizard", "Select Category");
                pDFTemplate_ImportWizardHelper.SelectByText("Category", "Check Processing");

                executionLog.Log("PDFImportWizard", "Click on Save button");
                pDFTemplate_ImportWizardHelper.ClickElement("Save1");

                executionLog.Log("PDFImportWizard", "wait for text");
                pDFTemplate_ImportWizardHelper.WaitForText("PDF Template options saved successfully.", 10);

                executionLog.Log("PDFImportWizard", "Redirect Import Wizard");
                VisitOffice("pdf_templates");
                pDFTemplate_ImportWizardHelper.WaitForWorkAround(3000);

                executionLog.Log("PDFImportWizard", "Click Delete btn  ");
                pDFTemplate_ImportWizardHelper.ClickElement("CheckBox1");

                executionLog.Log("PDFImportWizard", "Click Delete btn  ");
                pDFTemplate_ImportWizardHelper.ClickElement("DeletePDF");

                executionLog.Log("PDFImportWizard", "Accept alert message. ");
                pDFTemplate_ImportWizardHelper.AcceptAlert();

                executionLog.Log("PDFImportWizard", "Wait for message ");
                pDFTemplate_ImportWizardHelper.WaitForText("PDF Template Deleted Successfully.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("PDFImportWizard");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("PDF Import Wizard");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("PDF Import Wizard", "Bug", "Medium", "Pdf Import page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("PDF Import Wizard");
                        TakeScreenshot("PDFImportWizard");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\PDFImportWizard.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("PDFImportWizard");
                        string id = loginHelper.getIssueID("PDF Import Wizard");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\PDFImportWizard.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("PDF Import Wizard"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("PDF Import Wizard");
             //   executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("PDFImportWizard");
                executionLog.WriteInExcel("PDF Import Wizard", Status, JIRA, "PDF Import");
            }
        }
    }
}
