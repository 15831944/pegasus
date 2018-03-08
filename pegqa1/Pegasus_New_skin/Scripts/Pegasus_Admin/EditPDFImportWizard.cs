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
    public class EditPDFImportWizard : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin")]
        [TestCategory("TS6")]
        [TestCategory("Pegasus_Admin")]
        public void editPDFImportWizard()
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
            String pdf = "sample" + GetRandomNumber() + ".pdf";
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("EditPDFImportWizard", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("EditPDFImportWizard", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("EditPDFImportWizard", "Click On  Admin");
                VisitOffice("admin");

                executionLog.Log("EditPDFImportWizard", "Redirect Import Wizard");
                VisitOffice("pdf_templates/import");
                pDFTemplate_ImportWizardHelper.WaitForWorkAround(4000);

                executionLog.Log("EditPDFImportWizard", "Verify title");
                VerifyTitle("PDF Import Wizard");

                executionLog.Log("EditPDFImportWizard", "Choose Module");
                pDFTemplate_ImportWizardHelper.SelectByText("SelectModule", "Clients");

                executionLog.Log("EditPDFImportWizard", "Upload PDF File");
                String filename = GetPathToFile() + "2.PDF";
                pDFTemplate_ImportWizardHelper.upload("SelectFile", filename);

                executionLog.Log("EditPDFImportWizard", "Click On Import");
                pDFTemplate_ImportWizardHelper.ClickElement("Import");
                pDFTemplate_ImportWizardHelper.WaitForWorkAround(5000);

                executionLog.Log("EditPDFImportWizard", "Click On Next");
                pDFTemplate_ImportWizardHelper.ClickElement("Next");
                pDFTemplate_ImportWizardHelper.WaitForWorkAround(5000);

                executionLog.Log("EditPDFImportWizard", "verify title");
                VerifyTitle("PDF Import Wizard");

                executionLog.Log("EditPDFImportWizard", "Select Category");
                pDFTemplate_ImportWizardHelper.SelectByText("Category", "Other");
                pDFTemplate_ImportWizardHelper.WaitForWorkAround(2000);

                executionLog.Log("EditPDFImportWizard", "Click on Save button");
                pDFTemplate_ImportWizardHelper.ClickElement("Save1");

                executionLog.Log("EditPDFImportWizard", "wait for text");
                pDFTemplate_ImportWizardHelper.WaitForText("PDF Template options saved successfully.", 10);

                executionLog.Log("EditPDFImportWizard", "Click On Edit");
                pDFTemplate_ImportWizardHelper.ClickElement("Edit");
                pDFTemplate_ImportWizardHelper.WaitForWorkAround(4000);

                executionLog.Log("EditPDFImportWizard", "Verify title");
                VerifyTitle("Edit PDF Template");

                executionLog.Log("EditPDFImportWizard", "Enter Name");
                pDFTemplate_ImportWizardHelper.TypeText("Name", "Test.pdf");

                executionLog.Log("EditPDFImportWizard", "Click Edit Save");
                pDFTemplate_ImportWizardHelper.ClickElement("SaveEdit");

                executionLog.Log("EditPDFImportWizard", "Wait for updation message.");
                pDFTemplate_ImportWizardHelper.WaitForText("PDF Template Updated Successfully.", 10);

                executionLog.Log("EditPDFImportWizard", "Redirect Import Wizard");
                VisitOffice("pdf_templates");
                pDFTemplate_ImportWizardHelper.WaitForWorkAround(4000);

                executionLog.Log("EditPDFImportWizard", "Click Delete btn  ");
                pDFTemplate_ImportWizardHelper.ClickElement("CheckBox1");

                executionLog.Log("EditPDFImportWizard", "Click Delete btn  ");
                pDFTemplate_ImportWizardHelper.ClickElement("DeletePDF");

                executionLog.Log("EditPDFImportWizard", "Accept alert message. ");
                pDFTemplate_ImportWizardHelper.AcceptAlert();

                executionLog.Log("EditPDFImportWizard", "Wait for message ");
                pDFTemplate_ImportWizardHelper.WaitForText("PDF Template Deleted Successfully.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("EditPDFImportWizard");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Edit PDF Import Wizard");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Edit PDF Import Wizard", "Bug", "Medium", "PDF Import page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Edit PDF Import Wizard");
                        TakeScreenshot("EditPDFImportWizard");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditPDFImportWizard.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("EditPDFImportWizard");
                        string id = loginHelper.getIssueID("Edit PDF Import Wizard");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditPDFImportWizard.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Edit PDF Import Wizard"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Edit PDF Import Wizard");
           //     executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("EditPDFImportWizard");
                executionLog.WriteInExcel("Edit PDF Import Wizard", Status, JIRA, "PDF Import");
            }
        }
    }
}