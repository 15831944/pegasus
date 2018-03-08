using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class MakeStatusActivePDFTemplateCorp : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("Fail")]
        [TestCategory("TS5")]
        [TestCategory("NewSkinTaskJira")]
        public void makeStatusActivePDFTemplateCorp()
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

            //try
            //{

                executionLog.Log("MakeStatusActivePDFTemplateCorp", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("MakeStatusActivePDFTemplateCorp", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("MakeStatusActivePDFTemplateCorp", "Redirect To template page");
                VisitCorp("pdf_templates");
                corpPDFTemplate_TemplateHelper.WaitForWorkAround(3000);

                executionLog.Log("MakeStatusActivePDFTemplateCorp", "Enter PDF TO sEARCH");
                corpPDFTemplate_TemplateHelper.TypeText("EnterPDFToSearch", "2.pdf");
                corpPDFTemplate_TemplateHelper.WaitForWorkAround(2000);

                executionLog.Log("MakeStatusActivePDFTemplateCorp", "SelectModuleToSearch");
                corpPDFTemplate_TemplateHelper.Select("SelectModuleToSearch", "clients");
                corpPDFTemplate_TemplateHelper.WaitForWorkAround(2000);

                var loc = "//table[@id='list1']/tbody/tr[2]";
                if (corpPDFTemplate_TemplateHelper.IsElementPresent(loc))
                {

                    executionLog.Log("MakeStatusActivePDFTemplateCorp", "Click on edit icon.");
                    corpPDFTemplate_TemplateHelper.ClickElement("ClickEdit");

                    executionLog.Log("MakeStatusActivePDFTemplateCorp", "Select status");
                    corpPDFTemplate_TemplateHelper.Select("SelectStatusCorp", "1");

                    executionLog.Log("MakeStatusActivePDFTemplateCorp", "Select Category");
                    corpPDFTemplate_TemplateHelper.Select("CategoryPdf", "53");

                    executionLog.Log("MakeStatusActivePDFTemplateCorp", "Click on Save");
                    corpPDFTemplate_TemplateHelper.ClickElement("ClickSaveEdit");

                    executionLog.Log("MakeStatusActivePDFTemplateCorp", "Verify message");
                    corpPDFTemplate_TemplateHelper.WaitForText("PDF Template Updated Successfully.", 10);

                }
                else
                {

                    executionLog.Log("MakeStatusActivePDFTemplateCorp", "Redirect To Import");
                    VisitCorp("pdf_templates/import");

                    executionLog.Log("MakeStatusActivePDFTemplateCorp", "ChooseModule");
                    corpPDFTemplate_TemplateHelper.Select("ChooseModule", "20");

                    var path = GetPathToFile() + "2.pdf";
                    executionLog.Log("MakeStatusActivePDFTemplateCorp", "Upload file");
                    corpPDFTemplate_TemplateHelper.UploadFile("//*[@id='PdfTemplatePdfFile']", path);

                    executionLog.Log("MakeStatusActivePDFTemplateCorp", "Click import");
                    corpPDFTemplate_TemplateHelper.ClickElement("ClickOnImport");
                    corpPDFTemplate_TemplateHelper.WaitForWorkAround(5000);

                    executionLog.Log("MakeStatusActivePDFTemplateCorp", "ClickOnNext");
                    corpPDFTemplate_TemplateHelper.ClickElement("ClickOnNext");

                    executionLog.Log("MakeStatusActivePDFTemplateCorp", "Select Category");
                    corpPDFTemplate_TemplateHelper.SelectByText("SelectCategory", "Other");

                    executionLog.Log("MakeStatusActivePDFTemplateCorp", "Click On Save");
                    corpPDFTemplate_TemplateHelper.ClickElement("ClickOnSave");

                    executionLog.Log("MakeStatusActivePDFTemplateCorp", "Verify message");
                    corpPDFTemplate_TemplateHelper.WaitForText("PDF Template options saved successfully.", 10);
                    corpPDFTemplate_TemplateHelper.WaitForWorkAround(2000);

                    executionLog.Log("MakeStatusActivePDFTemplateCorp", "Enter PDF TO sEARCH");
                    corpPDFTemplate_TemplateHelper.TypeText("EnterPDFToSearch", "2.pdf");
                    corpPDFTemplate_TemplateHelper.WaitForWorkAround(2000);

                    executionLog.Log("MakeStatusActivePDFTemplateCorp", "SelectModuleToSearch");
                    corpPDFTemplate_TemplateHelper.Select("SelectModuleToSearch", "clients");
                    corpPDFTemplate_TemplateHelper.WaitForWorkAround(2000);

                    executionLog.Log("MakeStatusActivePDFTemplateCorp", "Click on edit icon");
                    corpPDFTemplate_TemplateHelper.ClickElement("ClickEdit");

                    executionLog.Log("MakeStatusActivePDFTemplateCorp", "cLICK On Delete");
                    corpPDFTemplate_TemplateHelper.Select("SelectStatusCorp", "1");

                    executionLog.Log("MakeStatusActivePDFTemplateCorp", "Click Save Edit");
                    corpPDFTemplate_TemplateHelper.ClickElement("ClickSaveEdit");

                    executionLog.Log("MakeStatusActivePDFTemplateCorp", "Verify Message");
                    corpPDFTemplate_TemplateHelper.VerifyPageText("PDF Template Updated Successfully.");


                }
            //}
            //catch (Exception e)
            //{
            //    executionLog.Log("Error", e.StackTrace);
            //    Status = "Fail";
            //    String counter = executionLog.readLastLine("counter");
            //    String Description = executionLog.GetAllTextFile("MakeStatusActivePDFTemplateCorp");
            //    String Error = executionLog.GetAllTextFile("Error");
            //    if (counter == "")
            //    {
            //        counter = "0";
            //    }
            //    bool result = loginHelper.CheckExstingIssue("Make Status Active PDF Template Corp");
            //    if (!result)
            //    {
            //        if (Int16.Parse(counter) < 9)
            //        {
            //            executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
            //            loginHelper.CreateIssue("Make Status Active PDF Template Corp", "Bug", "Medium", "Corp PDF page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
            //            string id = loginHelper.getIssueID("Make Status Active PDF Template Corp");
            //            TakeScreenshot("MakeStatusActivePDFTemplateCorp");
            //            string directoryName = loginHelper.GetnewDirectoryName(GetPath());
            //            var location = directoryName + "\\MakeStatusActivePDFTemplateCorp.png";
            //            loginHelper.AddAttachment(location, id);
            //        }
            //    }
            //    else
            //    {
            //        if (Int16.Parse(counter) < 9)
            //        {
            //            executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
            //            TakeScreenshot("MakeStatusActivePDFTemplateCorp");
            //            string id = loginHelper.getIssueID("Make Status Active PDF Template Corp");
            //            string directoryName = loginHelper.GetnewDirectoryName(GetPath());
            //            var location = directoryName + "\\MakeStatusActivePDFTemplateCorp.png";
            //            loginHelper.AddAttachment(location, id);
            //            loginHelper.AddComment(loginHelper.getIssueID("Make Status Active PDF Template Corp"), "This issue is still occurring");
            //        }
            //    }
            //    JIRA = loginHelper.getIssueID("Make Status Active PDF Template Corp");
            // //   executionLog.DeleteFile("Error");
            //    throw;

            //}
            //finally
            //{
            //    executionLog.DeleteFile("MakeStatusActivePDFTemplateCorp");
            //    executionLog.WriteInExcel("Make Status Active PDF Template Corp", Status, JIRA, "PDF Corp");
            //}
        }
    }
}