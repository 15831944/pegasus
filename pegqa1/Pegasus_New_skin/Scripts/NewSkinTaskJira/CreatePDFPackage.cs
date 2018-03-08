using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class CreatePDFPackages : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS4")]
        [TestCategory("NewSkinTaskJira")]
        public void createPDFPackage()
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
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("CreatePDFPackages", "Login with valid username and password");
                Login(username[0], password[0]);

                executionLog.Log("CreatePDFPackages", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("CreatePDFPackages", "Redirect To Admin");
                VisitOffice("admin");
                pDFTemplate_PDFTemplateHelper.WaitForWorkAround(3000);

                executionLog.Log("CreatePDFPackages", "Click on Menu Icon");
                // pDFTemplate_PDFTemplateHelper.ClickElement("MenuIcon");
                pDFTemplate_PDFTemplateHelper.WaitForWorkAround(1000);

                executionLog.Log("CreatePDFPackages", "ClickOnPdfTab");
                pDFTemplate_PDFTemplateHelper.MouseOverAndWait("PdfTab", 2);
                pDFTemplate_PDFTemplateHelper.WaitForWorkAround(3000);

                executionLog.Log("CreatePDFPackages", "Click on PDF Template button");
                pDFTemplate_PDFTemplateHelper.ClickJs("PdfTempBtn");
                pDFTemplate_PDFTemplateHelper.WaitForWorkAround(3000);

                executionLog.Log("CreatePDFPackages", "Click on pdf pakage.");
                pDFTemplate_PDFTemplateHelper.ClickElement("PDFPackage");
                pDFTemplate_PDFTemplateHelper.WaitForWorkAround(2000);

                executionLog.Log("CreatePDFPackages", "Enter pakage name");
                pDFTemplate_PDFTemplateHelper.TypeText("PackagePDFName", "Test Pakage");

                executionLog.Log("CreatePDFPackages", "Select Module");
                pDFTemplate_PDFTemplateHelper.SelectByText("SelectModulePakage", "Clients");
                pDFTemplate_PDFTemplateHelper.WaitForWorkAround(3000);

                executionLog.Log("CreatePDFPackages", "Select");
                pDFTemplate_PDFTemplateHelper.SelectByText("SelectPDFTemplate", "2");

                executionLog.Log("CreatePDFPackages", "SelectCategoryPackage");
                pDFTemplate_PDFTemplateHelper.SelectByText("SelectCategoryPackage", "Other");

                executionLog.Log("CreatePDFPackages", "Save PDF Package");
                pDFTemplate_PDFTemplateHelper.ClickElement("SavePDFPakage");
                pDFTemplate_PDFTemplateHelper.WaitForWorkAround(3000);

                executionLog.Log("CreatePDFPackages", "Wait for success message");
                pDFTemplate_PDFTemplateHelper.WaitForText("PDF Package Template Created Successfully.", 10);

                executionLog.Log("CreatePDFPackages", "Redirect to templates page");
                VisitOffice("pdf_templates");

                executionLog.Log("CreatePDFPackages", "Verify title");
                VerifyTitle("PDF Templates");

                executionLog.Log("CreatePDFPackages", "Search pdf template ");
                pDFTemplate_PDFTemplateHelper.TypeText("SearchPDF", "Test Pakage");
                pDFTemplate_PDFTemplateHelper.WaitForWorkAround(3000);

                executionLog.Log("CreatePDFPackages", "Select template type pakage ");
                pDFTemplate_PDFTemplateHelper.Select("SelectType", "1");
                pDFTemplate_PDFTemplateHelper.WaitForWorkAround(3000);

                executionLog.Log("CreatePDFPackages", "Select first pdf");
                pDFTemplate_PDFTemplateHelper.ClickElement("Checkbox1");

                executionLog.Log("CreatePDFPackages", "Click on delete button ");
                pDFTemplate_PDFTemplateHelper.ClickElement("DeletePdf");

                executionLog.Log("CreatePDFPackages", "Accept alert message. ");
                pDFTemplate_PDFTemplateHelper.AcceptAlert();

                executionLog.Log("CreatePDFPackages", "Wait for message ");
                pDFTemplate_PDFTemplateHelper.WaitForText("PDF Template Deleted Successfully.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CreatePDFPackages");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Create PDF Packages");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Create PDF Packages", "Bug", "Medium", "Pdf Template page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Create PDF Packages");
                        TakeScreenshot("CreatePDFPackages");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreatePDFPackages.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CreatePDFPackages");
                        string id = loginHelper.getIssueID("Create PDF Packages");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreatePDFPackages.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Create PDF Packages"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Create PDF Packages");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("CreatePDFPackages");
                executionLog.WriteInExcel("Create PDF Packages", Status, JIRA, "PDF Template");
            }
        }
    }
}