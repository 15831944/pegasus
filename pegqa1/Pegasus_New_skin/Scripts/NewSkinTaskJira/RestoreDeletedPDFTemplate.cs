using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class RestoreDeletedPDFTemplate : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("Fail")]
        [TestCategory("TS5")]
        [TestCategory("NewSkinTaskJira")]
        public void restoreDeletedPDFTemplate()
        {
            string[] username = null;
            string[] password = null;
            String JIRA = "";
            String Status = "Pass";

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

            try
            {
                executionLog.Log("RestoreDeletedPDFTemplate", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("RestoreDeletedPDFTemplate", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("RestoreDeletedPDFTemplate", "Redirect To Import");
                VisitOffice("pdf_templates/import");

                executionLog.Log("RestoreDeletedPDFTemplate", "Select Module");
                pDFTemplate_ImportWizardHelper.Select("SelectModule", "20");

                var path = GetPathToFile() + "2.pdf";
                executionLog.Log("RestoreDeletedPDFTemplate", "Upload file");
                pDFTemplate_ImportWizardHelper.UploadFile("//*[@id='PdfTemplatePdfFile']", path);

                executionLog.Log("RestoreDeletedPDFTemplate", "Click import");
                pDFTemplate_ImportWizardHelper.ClickElement("Import");
                pDFTemplate_ImportWizardHelper.WaitForWorkAround(3000);

                executionLog.Log("RestoreDeletedPDFTemplate", "Wait for text to hide.");
                pDFTemplate_ImportWizardHelper.WaitForTextHide("Your request is being processed.", 10);

                executionLog.Log("RestoreDeletedPDFTemplate", "ClickOnNext");
                pDFTemplate_ImportWizardHelper.ClickElement("Next");

                executionLog.Log("RestoreDeletedPDFTemplate", "Select Category");
                pDFTemplate_ImportWizardHelper.SelectByText("Category", "Other");

                executionLog.Log("RestoreDeletedPDFTemplate", "Click On Save");
                pDFTemplate_ImportWizardHelper.ClickElement("Save");

                executionLog.Log("RestoreDeletedPDFTemplate", "Verify messaeg");
                pDFTemplate_ImportWizardHelper.WaitForText("PDF Template options saved successfully.", 10);

                executionLog.Log("RestoreDeletedPDFTemplate", "Enter PDF to search.");
                pDFTemplate_ImportWizardHelper.TypeText("SearchPDF", "2.pdf");
                pDFTemplate_ImportWizardHelper.WaitForWorkAround(2000);

                executionLog.Log("RestoreDeletedPDFTemplate", "SelectModuleToSearch");
                pDFTemplate_ImportWizardHelper.Select("SearchModule", "merchants");
                pDFTemplate_ImportWizardHelper.WaitForWorkAround(2000);

                executionLog.Log("RestoreDeletedPDFTemplate", "Click on pdf");
                pDFTemplate_ImportWizardHelper.ClickElement("PDF1");

                executionLog.Log("RestoreDeletedPDFTemplate", "cLICK On Delete");
                pDFTemplate_ImportWizardHelper.ClickElement("ClickOnDelete");

                executionLog.Log("RestoreDeletedPDFTemplate", "Accept alert message.");
                pDFTemplate_ImportWizardHelper.AcceptAlert();

                executionLog.Log("RestoreDeletedPDFTemplate", "Wait for delete message..");
                pDFTemplate_ImportWizardHelper.WaitForText("PDF Template Deleted Successfully.", 10);

                executionLog.Log("RestoreDeletedPDFTemplate", "Redirect template");
                VisitOffice("pdf_templates");

                executionLog.Log("RestoreDeletedPDFTemplate", "Click on recycle bin link");
                pDFTemplate_ImportWizardHelper.ClickElement("ClickOnReCycleBin");

                executionLog.Log("RestoreDeletedPDFTemplate", "Click On Restore Template icon");
                pDFTemplate_ImportWizardHelper.ClickElement("ClickOnRestoreTemplate");
                pDFTemplate_ImportWizardHelper.WaitForWorkAround(3000);

                executionLog.Log("RestoreDeletedPDFTemplate", "PDF Template Restored Successfully.");
                pDFTemplate_ImportWizardHelper.WaitForText("PDF Template Restored Successfully.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("RestoreDeletedPDFTemplate");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Restore Deleted PDF Template");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Restore Deleted PDF Template", "Bug", "Medium", "PDF Template page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Restore Deleted PDF Template");
                        TakeScreenshot("RestoreDeletedPDFTemplate");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\RestoreDeletedPDFTemplate.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("RestoreDeletedPDFTemplate");
                        string id = loginHelper.getIssueID("Restore Deleted PDF Template");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\RestoreDeletedPDFTemplate.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Restore Deleted PDF Template"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Restore Deleted PDF Template");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("RestoreDeletedPDFTemplate");
                executionLog.WriteInExcel("Restore Deleted PDF Template", Status, JIRA, "PDF Template");
            }
        }
    }
}