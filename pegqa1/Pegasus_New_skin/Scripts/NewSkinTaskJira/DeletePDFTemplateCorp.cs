using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class DeletePDFTemplateCorp : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("Fail")]
        [TestCategory("TS4")]
        [TestCategory("NewSkinTaskJira")]
        public void deletePDFTemplateCorp()
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
            String JIRA = "";
            String Status = "Pass";
            try
            {

                executionLog.Log("DeletePDFTemplateCorp", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("DeletePDFTemplateCorp", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("DeletePDFTemplateCorp", "Redirect To Import");
                VisitCorp("pdf_templates/import");

                executionLog.Log("DeletePDFTemplateCorp", "ChooseModule");
                corpPDFTemplate_ImportWizardHelper.SelectByText("SelectModule", "Clients");

                executionLog.Log("DeletePDFTemplateCorp", "Uplaod a pdf file.");
                var path = GetPathToFile() + "2.pdf";
                corpPDFTemplate_ImportWizardHelper.UploadFile("//*[@id='PdfTemplatePdfFile']", path);

                executionLog.Log("DeletePDFTemplateCorp", "Click import after importing file");
                corpPDFTemplate_ImportWizardHelper.ClickElement("Import");
                corpPDFTemplate_ImportWizardHelper.WaitForWorkAround(4000);

                executionLog.Log("DeletePDFTemplateCorp", "ClickOnNext");
                corpPDFTemplate_ImportWizardHelper.ClickElement("Next");
                corpPDFTemplate_ImportWizardHelper.WaitForWorkAround(2000);

                executionLog.Log("DeletePDFTemplateCorp", "Select Category");
                corpPDFTemplate_ImportWizardHelper.SelectByText("SelectCatory", "Other");

                executionLog.Log("DeletePDFTemplateCorp", "ClickOnSave");
                corpPDFTemplate_ImportWizardHelper.ClickElement("Save");

                executionLog.Log("DeletePDFTemplateCorp", "Verify message");
                corpPDFTemplate_ImportWizardHelper.WaitForText("PDF Template options saved successfully.", 10);

                executionLog.Log("DeletePDFTemplateCorp", "Redirect to pdf templates.");
                VisitCorp("pdf_templates");
                corpPDFTemplate_ImportWizardHelper.WaitForWorkAround(3000);

                executionLog.Log("DeletePDFTemplateCorp", "Enter pdf to search.");
                corpPDFTemplate_ImportWizardHelper.TypeText("SearchPDF", "2.pdf");
                corpPDFTemplate_ImportWizardHelper.WaitForWorkAround(2000);

                executionLog.Log("DeletePDFTemplateCorp", "ModuleToSearch");
                corpPDFTemplate_ImportWizardHelper.Select("ModuleToSearch", "clients");
                corpPDFTemplate_ImportWizardHelper.WaitForWorkAround(2000);

                executionLog.Log("DeletePDFTemplateCorp", "Click on pdf");
                corpPDFTemplate_ImportWizardHelper.ClickElement("PDF1");

                executionLog.Log("DeletePDFTemplateCorp", "Click On Delete");
                corpPDFTemplate_ImportWizardHelper.ClickElement("DeletePDF");

                executionLog.Log("DeletePDFTemplateCorp", "Accept alert message.");
                corpPDFTemplate_ImportWizardHelper.AcceptAlert();

                executionLog.Log("DeletePDFTemplateCorp", "Wait for success message.");
                corpPDFTemplate_ImportWizardHelper.WaitForText("PDF Template Deleted Successfully.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("DeletePDFTemplateCorp");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Delete PDF Template Corp");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Delete PDF Template Corp", "Bug", "Medium", "PDF page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Delete PDF Template Corp");
                        TakeScreenshot("DeletePDFTemplateCorp");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\DeletePDFTemplateCorp.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("DeletePDFTemplateCorp");
                        string id = loginHelper.getIssueID("Delete PDF Template Corp");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\DeletePDFTemplateCorp.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Delete PDF Template Corp"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Delete PDF Template Corp");
                //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("DeletePDFTemplateCorp");
                executionLog.WriteInExcel("Delete PDF Template Corp", Status, JIRA, "Corp PDF template");
            }
        }
    }
}