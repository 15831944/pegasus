using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class EditPDFTemplateNewSkin : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS4")]
        [TestCategory("NewSkinTaskJira")]
        public void editPDFTemplateNewSkin()
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
            var name = "TESTCLIENT" + RandomNumber(1, 999);
            String Status = "Pass";
            String JIRA = "";


            try
            {
                executionLog.Log("EditPDFTemplateNewSkin", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("EditPDFTemplateNewSkin", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("EditPDFTemplateNewSkin", "Redirect To Admin");
                VisitOffice("admin");

                executionLog.Log("EditPDFTemplateNewSkin", "Redirect To import page");
                VisitOffice("pdf_templates");
                pDFTemplate_PDFTemplateHelper.WaitForWorkAround(3000);

                executionLog.Log("EditPDFTemplateNewSkin", "Click on PDF Package Button");
                pDFTemplate_PDFTemplateHelper.ClickElement("PDFPackageButton");
                pDFTemplate_PDFTemplateHelper.WaitForWorkAround(3000);

                executionLog.Log("EditPDFTemplateNewSkin", "Enter the PDF name");
                pDFTemplate_PDFTemplateHelper.TypeText("PackagePDFName", "Replace Name");

                executionLog.Log("EditPDFTemplateNewSkin", "ChooseModulePackage");
                pDFTemplate_PDFTemplateHelper.SelectByText("ChooseModulePackage", "Clients");
                pDFTemplate_PDFTemplateHelper.WaitForWorkAround(1000);

                var path = GetPathToFile() + "2.pdf";
                pDFTemplate_PDFTemplateHelper.UploadFile("//*[@id='PdfTemplatePdfTemplateId']", path);
                pDFTemplate_PDFTemplateHelper.WaitForWorkAround(3000);

                executionLog.Log("EditPDFTemplateNewSkin", "Click on AddPDF Icon");
                pDFTemplate_PDFTemplateHelper.ClickElement("AddPDFIcon");

                executionLog.Log("EditPDFTemplateNewSkin", "Select Category");
                pDFTemplate_PDFTemplateHelper.SelectDropDownByText("//*[@id='PdfTemplatePdfCategoryId']", "Card Service Agreements");

                executionLog.Log("EditPDFTemplateNewSkin", "ClickOnSave");
                pDFTemplate_PDFTemplateHelper.ClickElement("SaveButton");
                pDFTemplate_PDFTemplateHelper.WaitForWorkAround(5000);

                executionLog.Log("EditPDFTemplateNewSkin", "Verify message");
                pDFTemplate_PDFTemplateHelper.WaitForText("PDF Package Template Created Successfully.", 10);

                executionLog.Log("EditPDFTemplateNewSkin", "Enter Pdf To Search");
                pDFTemplate_PDFTemplateHelper.TypeText("EnterPDFToSearch", "2.pdf");
                pDFTemplate_PDFTemplateHelper.WaitForWorkAround(3000);

                executionLog.Log("EditPDFTemplateNewSkin", "Click on Edit");
                pDFTemplate_PDFTemplateHelper.ClickElement("ClickOnEdit");
                pDFTemplate_PDFTemplateHelper.WaitForWorkAround(3000);

                executionLog.Log("EditPDFTemplateNewSkin", "Enter Template Name");
                pDFTemplate_PDFTemplateHelper.TypeText("EnterTemplateName", "Replace Name");

                executionLog.Log("EditPDFTemplateNewSkin", "Click On Save");
                pDFTemplate_PDFTemplateHelper.ClickElement("SavebuttonEDit");

                executionLog.Log("EditPDFTemplateNewSkin", "Redirect to templates page");
                VisitOffice("pdf_templates");
                pDFTemplate_PDFTemplateHelper.WaitForWorkAround(3000);

                executionLog.Log("EditPDFTemplateNewSkin", "Verify title");
                VerifyTitle("PDF Templates");

                executionLog.Log("EditPDFTemplateNewSkin", "Search pdf template ");
                pDFTemplate_PDFTemplateHelper.TypeText("SearchPDF", "Replace Name");
                pDFTemplate_PDFTemplateHelper.WaitForWorkAround(4000);

                executionLog.Log("EditPDFTemplateNewSkin", "Select template type pakage ");
                pDFTemplate_PDFTemplateHelper.Select("SelectType", "1");
                pDFTemplate_PDFTemplateHelper.WaitForWorkAround(2000);

                executionLog.Log("EditPDFTemplateNewSkin", "Select first pdf");
                pDFTemplate_PDFTemplateHelper.ClickElement("Checkbox1");

                executionLog.Log("EditPDFTemplateNewSkin", "Click on delete button ");
                pDFTemplate_PDFTemplateHelper.ClickElement("DeletePdf");

                executionLog.Log("EditPDFTemplateNewSkin", "Accept alert message. ");
                pDFTemplate_PDFTemplateHelper.AcceptAlert();
                pDFTemplate_PDFTemplateHelper.WaitForWorkAround(5000);

                executionLog.Log("EditPDFTemplateNewSkin", "Wait for message ");
                pDFTemplate_PDFTemplateHelper.WaitForText("PDF Template Deleted Successfully.", 10);

            }

            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";
                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("EditPDFTemplateNewSkin");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Edit PDF Template New Skin");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Edit PDF Template New Skin", "Bug", "Medium", "Partner page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Edit PDF Template New Skin");
                        TakeScreenshot("EditPDFTemplateNewSkin");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditPDFTemplateNewSkin.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("EditPDFTemplateNewSkin");
                        string id = loginHelper.getIssueID("Edit PDF Template New Skin");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditPDFTemplateNewSkin.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Edit PDF Template New Skin"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Edit PDF Template New Skin");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("EditPDFTemplateNewSkin");
                executionLog.WriteInExcel("Edit PDF Template New Skin", Status, JIRA, "PDF Template");
            }
        }
    }
}
