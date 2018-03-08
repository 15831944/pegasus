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
    public class PDFImportWizard1 : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Corp")]
        [TestCategory("Fail")]
        [TestCategory("TS8")]
        [TestCategory("Pegasus_Corp")]
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
            var name = "Test" + RandomNumber(1, 99);
            var filename = GetPathToFile() + "2.pdf";
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("PDFImportWizard1", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("PDFImportWizard1", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("PDFImportWizard1", "Redirected at admin portal.");
                VisitOffice("admin");

                executionLog.Log("PDFImportWizard1", "Redirect at pdf import page.");
                VisitOffice("pdf_templates/import");
                pDFTemplate_ImportWizardHelper.WaitForWorkAround(5000);

                executionLog.Log("PDFImportWizard1", "Verify Page title");
                VerifyTitle("PDF Import Wizard");

                executionLog.Log("PDFImportWizard1", "Click on Import  button");
                pDFTemplate_ImportWizardHelper.ClickElement("Import");
                pDFTemplate_ImportWizardHelper.WaitForWorkAround(5000);

                executionLog.Log("PDFImportWizard1", "Verify validation");
                pDFTemplate_ImportWizardHelper.VerifyPageText("This field is required.");
                pDFTemplate_ImportWizardHelper.WaitForWorkAround(1000);

                executionLog.Log("PDFImportWizard1", "Choose Module");
                pDFTemplate_ImportWizardHelper.SelectByText("SelectModule", "Clients");
                pDFTemplate_ImportWizardHelper.WaitForWorkAround(1000);

                executionLog.Log("PDFImportWizard1", "Upload PDF File");
                pDFTemplate_ImportWizardHelper.upload("SelectFile", filename);
                pDFTemplate_ImportWizardHelper.WaitForWorkAround(1000);

                executionLog.Log("PDFImportWizard1", "Click On Import");
                pDFTemplate_ImportWizardHelper.ClickElement("Import");
                pDFTemplate_ImportWizardHelper.WaitForWorkAround(5000);

                executionLog.Log("PDFImportWizard1", "Click On Next");
                pDFTemplate_ImportWizardHelper.ClickElement("Next");
                pDFTemplate_ImportWizardHelper.WaitForWorkAround(4000);

                executionLog.Log("PDFImportWizard1", "Select Category");
                pDFTemplate_ImportWizardHelper.SelectByText("Category", "Other");
                pDFTemplate_ImportWizardHelper.WaitForWorkAround(1000);

                executionLog.Log("PDFImportWizard1", "Click on Save button");
                pDFTemplate_ImportWizardHelper.ClickElement("Save");
                pDFTemplate_ImportWizardHelper.WaitForWorkAround(5000);

                executionLog.Log("PDFImportWizard1", "Search the same file");
                pDFTemplate_ImportWizardHelper.TypeText("SearchPDF", "2.pdf");
                pDFTemplate_ImportWizardHelper.WaitForWorkAround(5000);

                executionLog.Log("PDFImportWizard1", "Click Delete btn  ");
                pDFTemplate_ImportWizardHelper.ClickElement("CheckBox1");
                pDFTemplate_ImportWizardHelper.WaitForWorkAround(3000);

                executionLog.Log("PDFImportWizard1", "Click Delete btn  ");
                pDFTemplate_ImportWizardHelper.ClickElement("DeletePDF");
                pDFTemplate_ImportWizardHelper.WaitForWorkAround(4000);

                executionLog.Log("PDFImportWizard1", "Accept alert message. ");
                pDFTemplate_ImportWizardHelper.AcceptAlert();
                pDFTemplate_ImportWizardHelper.WaitForWorkAround(3000);

                executionLog.Log("PDFImportWizard1", "Wait for message ");
                pDFTemplate_ImportWizardHelper.WaitForText("PDF Template Deleted Successfully.", 10);
                pDFTemplate_ImportWizardHelper.WaitForWorkAround(2000);
            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("PDFImportWizard1");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("PDF Import Wizard 1");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("PDF Import Wizard 1", "Bug", "Medium", "Import Wizard page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("PDF Import Wizard 1");
                        TakeScreenshot("PDFImportWizard1");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\PDFImportWizard1.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("PDFImportWizard1");
                        string id = loginHelper.getIssueID("PDF Import Wizard 1");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\PDFImportWizard1.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("PDF Import Wizard 1"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("PDF Import Wizard 1");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("PDFImportWizard1");
                executionLog.WriteInExcel("PDF Import Wizard 1", Status, JIRA, "PDF Import");
            }
        }
    }
}
