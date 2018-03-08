using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class MakeStatusInActivePDFTemplateCorp : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("Fail")]
        [TestCategory("TS5")]
        [TestCategory("NewSkinTaskJira")]
        public void makeStatusInActivePDFTemplateCorp()
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
            var corpPDFTemplate_TemplateHelper = new CorpPDFTemplate_TemplateHelper(GetWebDriver());


            // Variable random
            var name = "TESTCLIENT" + RandomNumber(1, 999);
            String Status = "Pass";
            String JIRA = "";

            try
            {
                executionLog.Log("MakeStatusInActivePDFTemplateCorp", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("MakeStatusInActivePDFTemplateCorp", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("MakeStatusInActivePDFTemplateCorp", "Redirect To template");
                VisitCorp("pdf_templates");
                corpPDFTemplate_TemplateHelper.WaitForWorkAround(3000);

                executionLog.Log("MakeStatusInActivePDFTemplateCorp", "Enter PDF TO sEARCH");
                corpPDFTemplate_TemplateHelper.TypeText("EnterPDFToSearch", "2.pdf");
                corpPDFTemplate_TemplateHelper.WaitForWorkAround(2000);

                executionLog.Log("MakeStatusInActivePDFTemplateCorp", "SelectModuleToSearch");
                corpPDFTemplate_TemplateHelper.Select("SelectModuleToSearch", "clients");
                corpPDFTemplate_TemplateHelper.WaitForWorkAround(2000);

                var loc = "//table[@id='list1']/tbody/tr[2]";
                if (corpPDFTemplate_TemplateHelper.IsElementPresent(loc))
                {

                    executionLog.Log("MakeStatusInActivePDFTemplateCorp", "Click on edit");
                    corpPDFTemplate_TemplateHelper.ClickElement("ClickEdit");

                    executionLog.Log("MakeStatusInActivePDFTemplateCorp", "Select status as inActive");
                    corpPDFTemplate_TemplateHelper.Select("SelectStatusCorp", "0");

                    executionLog.Log("MakeStatusInActivePDFTemplateCorp", "Click on Save");
                    corpPDFTemplate_TemplateHelper.ClickElement("ClickSaveEdit");

                    executionLog.Log("MakeStatusInActivePDFTemplateCorp", "Verify message");
                    corpPDFTemplate_TemplateHelper.WaitForText("PDF Template Updated Successfully.", 10);

                }
                else
                {

                    executionLog.Log("MakeStatusInActivePDFTemplateCorp", "Redirect To Import");
                    VisitCorp("pdf_templates/import");

                    executionLog.Log("MakeStatusInActivePDFTemplateCorp", "ChooseModule");
                    corpPDFTemplate_TemplateHelper.Select("ChooseModule", "20");

                    var path = @"D:\automation\testAutomation\Pegasus_New_skin\Files\2.pdf";
                    executionLog.Log("MakeStatusInActivePDFTemplateCorp", "Upload file");
                    corpPDFTemplate_TemplateHelper.UploadFile("//*[@id='PdfTemplatePdfFile']", path);
                    corpPDFTemplate_TemplateHelper.WaitForWorkAround(3000);

                    executionLog.Log("MakeStatusInActivePDFTemplateCorp", "Click import");
                    corpPDFTemplate_TemplateHelper.ClickElement("ClickOnImport");
                    corpPDFTemplate_TemplateHelper.WaitForWorkAround(3000);

                    executionLog.Log("MakeStatusInActivePDFTemplateCorp", "ClickOnNext");
                    corpPDFTemplate_TemplateHelper.ClickElement("ClickOnNext");
                    corpPDFTemplate_TemplateHelper.WaitForWorkAround(3000);

                    executionLog.Log("MakeStatusInActivePDFTemplateCorp", "Select Category");
                    corpPDFTemplate_TemplateHelper.SelectByText("SelectCategory", "Other");

                    executionLog.Log("MakeStatusInActivePDFTemplateCorp", "Click On Save");
                    corpPDFTemplate_TemplateHelper.ClickElement("ClickOnSave");

                    executionLog.Log("MakeStatusInActivePDFTemplateCorp", "Verify message");
                    corpPDFTemplate_TemplateHelper.WaitForText("PDF Template options saved successfully.", 10);
                    corpPDFTemplate_TemplateHelper.WaitForWorkAround(2000);

                    executionLog.Log("MakeStatusInActivePDFTemplateCorp", "Enter PDF TO search");
                    corpPDFTemplate_TemplateHelper.TypeText("EnterPDFToSearch", "2.pdf");
                    corpPDFTemplate_TemplateHelper.WaitForWorkAround(2000);

                    executionLog.Log("MakeStatusInActivePDFTemplateCorp", "Select Module To Search");
                    corpPDFTemplate_TemplateHelper.Select("SelectModuleToSearch", "clients");
                    corpPDFTemplate_TemplateHelper.WaitForWorkAround(2000);

                    executionLog.Log("MakeStatusInActivePDFTemplateCorp", "Click on edit");
                    corpPDFTemplate_TemplateHelper.ClickElement("ClickEdit");

                    executionLog.Log("MakeStatusInActivePDFTemplateCorp", "Select Status as inactive");
                    corpPDFTemplate_TemplateHelper.Select("SelectStatusCorp", "0");

                    executionLog.Log("MakeStatusInActivePDFTemplateCorp", "Click on Save");
                    corpPDFTemplate_TemplateHelper.ClickElement("ClickSaveEdit");

                    executionLog.Log("MakeStatusInActivePDFTemplateCorp", "Verify message");
                    corpPDFTemplate_TemplateHelper.WaitForText("PDF Template Updated Successfully.", 10);
                }
            }

            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";
                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("MakeStatusInActivePDFTemplateCorp");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Make Status In Active PDF Template Corp");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Make Status In Active PDF Template Corp", "Bug", "Medium", "Corp PDF page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Make Status In Active PDF Template Corp");
                        TakeScreenshot("MakeStatusInActivePDFTemplateCorp");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\MakeStatusInActivePDFTemplateCorp.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("MakeStatusInActivePDFTemplateCorp");
                        string id = loginHelper.getIssueID("Make Status In Active PDF Template Corp");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\MakeStatusInActivePDFTemplateCorp.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Make Status In Active PDF Template Corp"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Make Status In Active PDF Template Corp");
           //     executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("MakeStatusInActivePDFTemplateCorp");
                executionLog.WriteInExcel("Make Status In Active PDF Template Corp", Status, JIRA, "Corp PDF Template");
            }
        }
    }
}