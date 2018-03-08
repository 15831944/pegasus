using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class PermanentlyDeletePDFTemplateCorp : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("Fail")]
        [TestCategory("TS5")]
        [TestCategory("NewSkinTaskJira")]
        public void permanentlyDeletePDFTemplateCorp()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_corp");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var corpPDFTemplate_ImportWizardHelper = new CorpPDFTemplate_ImportWizardHelper(GetWebDriver());


            // Variable random
            var name = "TESTCLIENT" + RandomNumber(1, 999);
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("PermanentlyDeletePDFTemplateCorp", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("PermanentlyDeletePDFTemplateCorp", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("PermanentlyDeletePDFTemplateCorp", "Redirect To Import");
                VisitCorp("pdf_templates/import");
                corpPDFTemplate_ImportWizardHelper.WaitForWorkAround(3000);

                executionLog.Log("PermanentlyDeletePDFTemplateCorp", "ChooseModule");
                corpPDFTemplate_ImportWizardHelper.SelectByText("SelectModule", "Clients");

                var path = GetPathToFile() + "2.pdf";
                executionLog.Log("PermanentlyDeletePDFTemplateCorp", "Uplaod file");
                corpPDFTemplate_ImportWizardHelper.UploadFile("//*[@id='PdfTemplatePdfFile']", path);

                executionLog.Log("PermanentlyDeletePDFTemplateCorp", "Click import");
                corpPDFTemplate_ImportWizardHelper.ClickElement("Import");
                corpPDFTemplate_ImportWizardHelper.WaitForWorkAround(3000);

                executionLog.Log("PermanentlyDeletePDFTemplateCorp", "ClickOnNext");
                corpPDFTemplate_ImportWizardHelper.ClickElement("ClickOnNext");
                corpPDFTemplate_ImportWizardHelper.WaitForWorkAround(2000);

                executionLog.Log("PermanentlyDeletePDFTemplateCorp", "Select Category");
                corpPDFTemplate_ImportWizardHelper.SelectDropDownByText("//*[@id='PdfTemplatePdfCategoryId']", "Card Service Agreements");

                executionLog.Log("PermanentlyDeletePDFTemplateCorp", "ClickOnSave");
                corpPDFTemplate_ImportWizardHelper.ClickElement("Save");

                executionLog.Log("PermanentlyDeletePDFTemplateCorp", "Verify message");
                corpPDFTemplate_ImportWizardHelper.WaitForText("PDF Template options saved successfully.", 10);

                executionLog.Log("PermanentlyDeletePDFTemplateCorp", "Redirect at templated page.");
                VisitCorp("pdf_templates");
                corpPDFTemplate_ImportWizardHelper.WaitForWorkAround(3000);

                executionLog.Log("PermanentlyDeletePDFTemplateCorp", "Enter PDF TO sEARCH");
                corpPDFTemplate_ImportWizardHelper.TypeText("SearchPDF", "2.pdf");
                corpPDFTemplate_ImportWizardHelper.WaitForWorkAround(2000);

                executionLog.Log("PermanentlyDeletePDFTemplateCorp", "SelectModuleToSearch");
                corpPDFTemplate_ImportWizardHelper.Select("ModuleToSearch", "clients");
                corpPDFTemplate_ImportWizardHelper.WaitForWorkAround(2000);

                executionLog.Log("PermanentlyDeletePDFTemplateCorp", "cLICK ON pdf");
                corpPDFTemplate_ImportWizardHelper.ClickElement("PDF1");
                corpPDFTemplate_ImportWizardHelper.WaitForWorkAround(2000);

                executionLog.Log("PermanentlyDeletePDFTemplateCorp", "cLICK On Delete");
                corpPDFTemplate_ImportWizardHelper.ClickElement("DeletePDF");

                executionLog.Log("PermanentlyDeletePDFTemplateCorp", "Accept alert message.");
                corpPDFTemplate_ImportWizardHelper.AcceptAlert();

                executionLog.Log("PermanentlyDeletePDFTemplateCorp", "Redirect to template");
                VisitCorp("pdf_templates");

                executionLog.Log("PermanentlyDeletePDFTemplateCorp", "Click on recycle bin");
                corpPDFTemplate_ImportWizardHelper.ClickElement("ClickOnReCycleBin");
                corpPDFTemplate_ImportWizardHelper.WaitForWorkAround(2000);

                executionLog.Log("PermanentlyDeletePDFTemplateCorp", "Click on delete item.");
                corpPDFTemplate_ImportWizardHelper.ClickElement("PermanentlyDeleteFrmRecycleBin");

                executionLog.Log("PermanentlyDeletePDFTemplateCorp", "Accept alert message.");
                corpPDFTemplate_ImportWizardHelper.AcceptAlert();

                executionLog.Log("PermanentlyDeletePDFTemplateCorp", "PDF Template Restored Successfully.");
                corpPDFTemplate_ImportWizardHelper.WaitForText("PDF Template Permanently Deleted.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("PermanentlyDeletePDFTemplateCorp");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Permanently Delete PDF Template Corp");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Permanently Delete PDF Template Corp", "Bug", "Medium", "Pdf Template page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Permanently Delete PDF Template Corp");
                        TakeScreenshot("PermanentlyDeletePDFTemplateCorp");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\PermanentlyDeletePDFTemplateCorp.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("PermanentlyDeletePDFTemplateCorp");
                        string id = loginHelper.getIssueID("Permanently Delete PDF Template Corp");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\PermanentlyDeletePDFTemplateCorp.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Permanently Delete PDF Template Corp"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Permanently Delete PDF Template Corp");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("PermanentlyDeletePDFTemplateCorp");
                executionLog.WriteInExcel("Permanently Delete PDF Template Corp", Status, JIRA, "PDF Template");
            }
        }
    }
}