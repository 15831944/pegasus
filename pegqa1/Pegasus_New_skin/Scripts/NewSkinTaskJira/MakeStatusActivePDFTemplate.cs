using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class MakeStatusActivePDFTemplate : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("Fail")]
        [TestCategory("TS5")]
        [TestCategory("NewSkinTaskJira")]
        public void makeStatusActivePDFTemplate()
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
            var pDFTemplate_PDFTemplateHelper = new PDFTemplate_PDFTemplateHelper(GetWebDriver());


            // Variable random
            String Status = "Pass";
            String JIRA = "";

            try
            {
                executionLog.Log("MakeStatusActivePDFTemplate", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("MakeStatusActivePDFTemplate", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("MakeStatusActivePDFTemplate", "Redirect To pdf template");
                VisitOffice("pdf_templates");
                pDFTemplate_PDFTemplateHelper.WaitForWorkAround(3000);

                executionLog.Log("MakeStatusActivePDFTemplate", "Enter PDF TO sEARCH");
                pDFTemplate_PDFTemplateHelper.TypeText("EnterPDFToSearch", "2.pdf");
                pDFTemplate_PDFTemplateHelper.WaitForWorkAround(2000);

                executionLog.Log("MakeStatusActivePDFTemplate", "SelectModuleToSearch");
                pDFTemplate_PDFTemplateHelper.Select("SelectModuleToSearch", "merchants");
                pDFTemplate_PDFTemplateHelper.WaitForWorkAround(2000);

                var loc = "//table[@id='list1']/tbody/tr[2]";
                if (pDFTemplate_PDFTemplateHelper.IsElementPresent(loc))
                {

                    executionLog.Log("MakeStatusActivePDFTemplate", "Click on edit icon");
                    pDFTemplate_PDFTemplateHelper.ClickElement("ClickOnEdit");
                    pDFTemplate_PDFTemplateHelper.WaitForWorkAround(2000);

                    executionLog.Log("MakeStatusActivePDFTemplate", "Select status as Inactive.");
                    pDFTemplate_PDFTemplateHelper.Select("SelectStatusDeactive", "0");
                    pDFTemplate_PDFTemplateHelper.WaitForWorkAround(2000);

                    executionLog.Log("MakeStatusActivePDFTemplate", "Click Save button");
                    pDFTemplate_PDFTemplateHelper.ClickElement("SaveEdit");

                    executionLog.Log("MakeStatusActivePDFTemplate", "Verify message");
                    pDFTemplate_PDFTemplateHelper.WaitForText("PDF Template Updated Successfully.", 10);

                }
                else
                {

                    executionLog.Log("MakeStatusActivePDFTemplate", "Redirect To Import");
                    VisitOffice("pdf_templates/import");
                    pDFTemplate_PDFTemplateHelper.WaitForWorkAround(2000);

                    executionLog.Log("MakeStatusActivePDFTemplate", "ChooseModule");
                    pDFTemplate_PDFTemplateHelper.Select("SelectModule", "20");
                    pDFTemplate_PDFTemplateHelper.WaitForWorkAround(2000);

                    var path = GetPathToFile() + "2.pdf";
                    executionLog.Log("MakeStatusActivePDFTemplate", "Upload a pdf file.");
                    pDFTemplate_PDFTemplateHelper.UploadFile("//*[@id='PdfTemplatePdfFile']", path);

                    executionLog.Log("MakeStatusActivePDFTemplate", "Click import");
                    pDFTemplate_PDFTemplateHelper.ClickElement("Import");
                    pDFTemplate_PDFTemplateHelper.WaitForWorkAround(4000);

                    executionLog.Log("MakeStatusActivePDFTemplate", "ClickOnNext");
                    pDFTemplate_PDFTemplateHelper.ClickElement("ClickOnNext");

                    executionLog.Log("MakeStatusActivePDFTemplate", "Select Category");
                    pDFTemplate_PDFTemplateHelper.SelectByText("SelectCategory", "Other");

                    executionLog.Log("MakeStatusActivePDFTemplate", "Save");
                    pDFTemplate_PDFTemplateHelper.ClickElement("Save");

                    executionLog.Log("MakeStatusActivePDFTemplate", "Verify message");
                    pDFTemplate_PDFTemplateHelper.WaitForText("PDF Template options saved successfully.", 10);

                    executionLog.Log("MakeStatusActivePDFTemplate", "Enter PDF TO sEARCH");
                    pDFTemplate_PDFTemplateHelper.TypeText("EnterPDFToSearch", "2.pdf");
                    pDFTemplate_PDFTemplateHelper.WaitForWorkAround(2000);

                    executionLog.Log("MakeStatusActivePDFTemplate", "SelectModuleToSearch");
                    pDFTemplate_PDFTemplateHelper.Select("SelectModuleToSearch", "merchants");
                    pDFTemplate_PDFTemplateHelper.WaitForWorkAround(2000);

                    executionLog.Log("MakeStatusActivePDFTemplate", "Click on pdf");
                    pDFTemplate_PDFTemplateHelper.ClickElement("EditFirstTemplate");
                    pDFTemplate_PDFTemplateHelper.WaitForWorkAround(2000);

                    executionLog.Log("MakeStatusActivePDFTemplate", "Select status as active.");
                    pDFTemplate_PDFTemplateHelper.Select("SelectStatusDeactive", "1");

                    executionLog.Log("MakeStatusActivePDFTemplate", "Click on save button.");
                    pDFTemplate_PDFTemplateHelper.ClickElement("SaveEdit");

                    executionLog.Log("MakeStatusActivePDFTemplate", "Verify message");
                    pDFTemplate_PDFTemplateHelper.WaitForText("PDF Template Updated Successfully.", 10);

                }
            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";
                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("MakeStatusActivePDFTemplate");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Make Status Active PDF Template");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Make Status Active PDF Template", "Bug", "Medium", "Pdf page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Make Status Active PDF Template");
                        TakeScreenshot("MakeStatusActivePDFTemplate");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\MakeStatusActivePDFTemplate.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("MakeStatusActivePDFTemplate");
                        string id = loginHelper.getIssueID("Make Status Active PDF Template");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\MakeStatusActivePDFTemplate.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Make Status Active PDF Template"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Make Status Active PDF Template");
                //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("MakeStatusActivePDFTemplate");
                executionLog.WriteInExcel("Make Status Active PDF Template", Status, JIRA, "PDF Template.");
            }
        }
    }
}