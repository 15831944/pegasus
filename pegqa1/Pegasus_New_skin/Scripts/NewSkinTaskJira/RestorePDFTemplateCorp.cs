using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class RestorePDFTemplateCorp : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("Fail")]
        [TestCategory("TS5")]
        [TestCategory("NewSkinTaskJira")]
        public void restorePDFTemplateCorp()
        {
            string[] username = null;
            string[] password = null;
            String JIRA = "";
            String Status = "Pass";

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


            try
            {
                executionLog.Log("RestorePDFTemplateCorp", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("RestorePDFTemplateCorp", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("RestorePDFTemplateCorp", "Redirect To Import");
                VisitCorp("pdf_templates/import");

                executionLog.Log("RestorePDFTemplateCorp", "Select Module");
                corpPDFTemplate_ImportWizardHelper.Select("SelectModule", "20");

                var path = GetPathToFile() + "2.pdf";
                executionLog.Log("RestorePDFTemplateCorp", "Uplaod file");
                corpPDFTemplate_ImportWizardHelper.UploadFile("//*[@id='PdfTemplatePdfFile']", path);

                executionLog.Log("RestorePDFTemplateCorp", "Click import");
                corpPDFTemplate_ImportWizardHelper.ClickElement("Import");
                corpPDFTemplate_ImportWizardHelper.WaitForWorkAround(4000);

                executionLog.Log("RestorePDFTemplateCorp", "Wait for text to hide.");
                corpPDFTemplate_ImportWizardHelper.WaitForTextHide("Your request is being processed.", 10);

                executionLog.Log("RestorePDFTemplateCorp", "ClickOnNext");
                corpPDFTemplate_ImportWizardHelper.ClickElement("Next");
                corpPDFTemplate_ImportWizardHelper.WaitForWorkAround(3000);

                executionLog.Log("RestorePDFTemplateCorp", "Select Category");
                corpPDFTemplate_ImportWizardHelper.SelectByText("SelectCatory", "Other");

                executionLog.Log("RestorePDFTemplateCorp", "Click On Save");
                corpPDFTemplate_ImportWizardHelper.ClickElement("Save");

                executionLog.Log("RestorePDFTemplateCorp", "Verify messge");
                corpPDFTemplate_ImportWizardHelper.WaitForText("PDF Template options saved successfully.", 10);

                executionLog.Log("RestorePDFTemplateCorp", "Enter PDF TO sEARCH");
                corpPDFTemplate_ImportWizardHelper.TypeText("EnterPDFToSearch", "2.pdf");
                corpPDFTemplate_ImportWizardHelper.WaitForWorkAround(2000);

                executionLog.Log("RestorePDFTemplateCorp", "Select Module To Search");
                corpPDFTemplate_ImportWizardHelper.Select("SelectModuleToSearch", "clients");
                corpPDFTemplate_ImportWizardHelper.WaitForWorkAround(2000);

                executionLog.Log("RestorePDFTemplateCorp", "Click on pdf");
                corpPDFTemplate_ImportWizardHelper.ClickElement("PDF1");

                executionLog.Log("RestorePDFTemplateCorp", "Click on Delete");
                corpPDFTemplate_ImportWizardHelper.ClickElement("DeletePDF");

                executionLog.Log("RestorePDFTemplateCorp", "Accept alert message");
                corpPDFTemplate_ImportWizardHelper.AcceptAlert();

                executionLog.Log("RestorePDFTemplateCorp", "Wait for delete message.");
                corpPDFTemplate_ImportWizardHelper.WaitForText("PDF Template Deleted Successfully.", 10);

                executionLog.Log("RestorePDFTemplateCorp", "Redirect template");
                VisitCorp("pdf_templates");

                executionLog.Log("RestorePDFTemplateCorp", "Click on recycle bin");
                corpPDFTemplate_ImportWizardHelper.ClickElement("ClickOnReCycleBin");

                executionLog.Log("RestorePDFTemplateCorp", "Click on restore icon ");
                corpPDFTemplate_ImportWizardHelper.ClickElement("RestoreThisTemplateCorp");

                executionLog.Log("RestorePDFTemplateCorp", "PDF Template Restored Successfully.");
                corpPDFTemplate_ImportWizardHelper.WaitForText("PDF Template Restored Successfully.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("RestorePDFTemplateCorp");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Restore PDF Template Corp");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Restore PDF Template Corp", "Bug", "Medium", "Corp PDFTemplate", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Restore PDF Template Corp");
                        TakeScreenshot("RestorePDFTemplateCorp");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\RestorePDFTemplateCorp.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("RestorePDFTemplateCorp");
                        string id = loginHelper.getIssueID("Restore PDF Template Corp");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\RestorePDFTemplateCorp.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Restore PDF Template Corp"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Restore PDF Template Corp");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("RestorePDFTemplateCorp");
                executionLog.WriteInExcel("Restore PDF Template Corp", Status, JIRA, "PDF Template");
            }
        }
    }
}