using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class DeletePDFTemplate : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("Fail")]
        [TestCategory("TS4")]
        [TestCategory("NewSkinTaskJira")]
        public void deletePDFTemplate()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var pDFTemplate_ImportWizardHelper = new PDFTemplate_ImportWizardHelper(GetWebDriver());


            // Variable random
            var name = "TESTCLIENT" + RandomNumber(1, 999);
            String JIRA = "";
            String Status = "Pass";

            try
            {
            executionLog.Log("DeletePDFTemplate", "Login with valid username and password");
            Login(username[0], password[0]);
            Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

            executionLog.Log("DeletePDFTemplate", "Verify Page title");
            VerifyTitle("Dashboard");
            Console.WriteLine("Redirected at Dashboard screen.");

            executionLog.Log("DeletePDFTemplate", "Redirect To Import");
            VisitOffice("pdf_templates/import");

            executionLog.Log("DeletePDFTemplate", "ChooseModule");
            pDFTemplate_ImportWizardHelper.Select("SelectModule", "20");

            executionLog.Log("DeletePDFTemplate", "Upload a pdf file.");
            var path = GetPathToFile() + "2.pdf";
            pDFTemplate_ImportWizardHelper.UploadFile("//*[@id='PdfTemplatePdfFile']", path);

            executionLog.Log("DeletePDFTemplate", "Import file and Click import");
            pDFTemplate_ImportWizardHelper.ClickElement("Import");
            pDFTemplate_ImportWizardHelper.WaitForWorkAround(4000);

            executionLog.Log("DeletePDFTemplate", "ClickOnNext");
            pDFTemplate_ImportWizardHelper.ClickElement("Next");
            pDFTemplate_ImportWizardHelper.WaitForWorkAround(2000);

            executionLog.Log("DeletePDFTemplate", "Select Category");
            pDFTemplate_ImportWizardHelper.SelectByText("Category", "Other");

            executionLog.Log("DeletePDFTemplate", "ClickOnSave");
            pDFTemplate_ImportWizardHelper.ClickElement("Save");

            executionLog.Log("DeletePDFTemplate", "Verify Message");
            pDFTemplate_ImportWizardHelper.WaitForText("PDF Template options saved successfully.", 10);

            executionLog.Log("DeletePDFTemplate", "Redirect to templates page.");
            VisitOffice("pdf_templates");
            pDFTemplate_ImportWizardHelper.WaitForWorkAround(3000);

            executionLog.Log("DeletePDFTemplate", "Enter pdf to search");
            pDFTemplate_ImportWizardHelper.TypeText("SearchPDF", "2.pdf");
            pDFTemplate_ImportWizardHelper.WaitForWorkAround(2000);

            executionLog.Log("DeletePDFTemplate", "SelectModuleToSearch");
            pDFTemplate_ImportWizardHelper.Select("SearchModule", "merchants");
            pDFTemplate_ImportWizardHelper.WaitForWorkAround(2000);

            executionLog.Log("DeletePDFTemplate", "Click on pdf");
            pDFTemplate_ImportWizardHelper.ClickElement("PDF1");

            executionLog.Log("DeletePDFTemplate", "cLICK On Delete");
            pDFTemplate_ImportWizardHelper.ClickElement("DeletePDF");

            executionLog.Log("DeletePDFTemplate", "Accept alert message");
            pDFTemplate_ImportWizardHelper.AcceptAlert();

            executionLog.Log("DeletePDFTemplate", "Wait for delete message.");
            pDFTemplate_ImportWizardHelper.WaitForText("PDF Template Deleted Successfully.", 10);

        }
       catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("DeletePDFTemplate");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Delete PDF Template");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Delete PDF Template", "Bug", "Medium", "PDF template page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Delete PDF Template");
                        TakeScreenshot("DeletePDFTemplate");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\DeletePDFTemplate.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("DeletePDFTemplate");
                        string id = loginHelper.getIssueID("Delete PDF Template");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\DeletePDFTemplate.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Delete PDF Template"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Delete PDF Template");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("DeletePDFTemplate");
                executionLog.WriteInExcel("Delete PDF Template", Status, JIRA, "PDF Template");
            }
        }
    }
} 