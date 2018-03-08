using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class MakeStatusInActivePDFTemplate : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("Fail")]
        [TestCategory("TS5")]
        [TestCategory("NewSkinTaskJira")]
        public void makeStatusInactivePDFTemplate()
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
                executionLog.Log("MakeStatusInActivePDFTemplate", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("MakeStatusInActivePDFTemplate", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("MakeStatusInActivePDFTemplate", "Redirect To template");
                VisitOffice("pdf_templates");
                pDFTemplate_PDFTemplateHelper.WaitForWorkAround(3000);

                executionLog.Log("MakeStatusInActivePDFTemplate", "Enter PDF TO sEARCH");
                pDFTemplate_PDFTemplateHelper.TypeText("EnterPDFToSearch", "2.pdf");
                pDFTemplate_PDFTemplateHelper.WaitForWorkAround(2000);

                executionLog.Log("MakeStatusInActivePDFTemplate", "SelectModuleToSearch");
                pDFTemplate_PDFTemplateHelper.Select("SelectModuleToSearch", "merchants");
                pDFTemplate_PDFTemplateHelper.WaitForWorkAround(2000);

                var loc = "//table[@id='list1']/tbody/tr[2]";
                if (pDFTemplate_PDFTemplateHelper.IsElementPresent(loc))
                {

                    executionLog.Log("MakeStatusInActivePDFTemplate", "Click on edit icon");
                    pDFTemplate_PDFTemplateHelper.ClickElement("ClickOnEdit");

                    executionLog.Log("MakeStatusInActivePDFTemplate", "Select status as inactive.");
                    pDFTemplate_PDFTemplateHelper.Select("SelectStatusDeactive", "0");

                    executionLog.Log("MakeStatusInActivePDFTemplate", "Click Save button");
                    pDFTemplate_PDFTemplateHelper.ClickElement("SaveEdit");

                    executionLog.Log("MakeStatusInActivePDFTemplate", "Verify message");
                    pDFTemplate_PDFTemplateHelper.WaitForText("PDF Template Updated Successfully.", 10);

                }
                else
                {

                    executionLog.Log("MakeStatusInActivePDFTemplate", "Redirect To Import");
                    VisitOffice("pdf_templates/import");

                    executionLog.Log("MakeStatusInActivePDFTemplate", "ChooseModule");
                    pDFTemplate_PDFTemplateHelper.Select("ChooseModule", "20");

                    var path = @"D:\NEWPEG\TestAutomationProject\PegasusTests\Files\2.pdf";
                    executionLog.Log("MakeStatusInActivePDFTemplate", "upload file");
                    pDFTemplate_PDFTemplateHelper.UploadFile("//*[@id='PdfTemplatePdfFile']", path);

                    executionLog.Log("MakeStatusInActivePDFTemplate", "Click on import");
                    pDFTemplate_PDFTemplateHelper.ClickElement("Import");
                    pDFTemplate_PDFTemplateHelper.WaitForWorkAround(3000);

                    executionLog.Log("MakeStatusInActivePDFTemplate", "ClickOnNext");
                    pDFTemplate_PDFTemplateHelper.ClickElement("ClickOnNext");

                    executionLog.Log("MakeStatusInActivePDFTemplate", "Select Category");
                    pDFTemplate_PDFTemplateHelper.SelectByText("SelectCategory", "Other");

                    executionLog.Log("MakeStatusInActivePDFTemplate", "ClickOnSave");
                    pDFTemplate_PDFTemplateHelper.ClickElement("Save");

                    executionLog.Log("MakeStatusInActivePDFTemplate", "Verify message");
                    pDFTemplate_PDFTemplateHelper.WaitForText("PDF Template options saved successfully.", 10);
                    pDFTemplate_PDFTemplateHelper.WaitForWorkAround(2000);

                    executionLog.Log("MakeStatusInActivePDFTemplate", "Enter PDF TO sEARCH");
                    pDFTemplate_PDFTemplateHelper.TypeText("EnterPDFToSearch", "2.pdf");
                    pDFTemplate_PDFTemplateHelper.WaitForWorkAround(2000);

                    executionLog.Log("MakeStatusInActivePDFTemplate", "Select Module To Search");
                    pDFTemplate_PDFTemplateHelper.Select("SelectModuleToSearch", "merchants");
                    pDFTemplate_PDFTemplateHelper.WaitForWorkAround(2000);

                    executionLog.Log("MakeStatusInActivePDFTemplate", "Click on edit");
                    pDFTemplate_PDFTemplateHelper.ClickElement("ClickOnEdit");

                    executionLog.Log("MakeStatusInActivePDFTemplate", "Select status as inactive.");
                    pDFTemplate_PDFTemplateHelper.Select("SelectStatusDeactive", "0");

                    executionLog.Log("MakeStatusInActivePDFTemplate", "Click on Save");
                    pDFTemplate_PDFTemplateHelper.ClickElement("SaveEdit");

                    executionLog.Log("MakeStatusInActivePDFTemplate", "Verify message");
                    pDFTemplate_PDFTemplateHelper.WaitForText("PDF Template Updated Successfully.", 10);

                }
            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";
                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("MakeStatusInActivePDFTemplate");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Make Status In Active PDF Template");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Make Status In Active PDF Template", "Bug", "Medium", "PDF page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Make Status In Active PDF Template");
                        TakeScreenshot("MakeStatusInActivePDFTemplate");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\MakeStatusInActivePDFTemplate.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("MakeStatusInActivePDFTemplate");
                        string id = loginHelper.getIssueID("Make Status In Active PDF Template");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\MakeStatusInActivePDFTemplate.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Make Status In Active PDF Template"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Make Status In Active PDF Template");
                //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("MakeStatusInActivePDFTemplate");
                executionLog.WriteInExcel("Make Status In Active PDF Template", Status, JIRA, "PDF Template");
            }
        }
    }
}