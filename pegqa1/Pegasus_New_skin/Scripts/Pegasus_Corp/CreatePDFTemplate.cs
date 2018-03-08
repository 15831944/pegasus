using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class CreatePDFTemplate : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Corp")]
        [TestCategory("TS7")]
        [TestCategory("Pegasus_Corp")]
        public void createPDFTemplate()
        {
            string[] username = null;
            string[] password = null;

            XMLParse oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var pDFTemplate_PDFTemplateHelper = new PDFTemplate_PDFTemplateHelper(GetWebDriver());

            // Variable
            String name = "Test" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
            executionLog.Log("CreatePDFTemplate", "Login with valid username and password");
            Login(username[0], password[0]);
            Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

            executionLog.Log("CreatePDFTemplate", "Verify Page title");
            VerifyTitle("Dashboard");

            executionLog.Log("CreatePDFTemplate", "Click On  Admin");
            VisitOffice("admin");

            executionLog.Log("CreatePDFTemplate", "Redirect to PDF Template");
            VisitOffice("pdf_templates");
            pDFTemplate_PDFTemplateHelper.WaitForWorkAround(3000);

            executionLog.Log("CreatePDFTemplate", "Verify page title.");
            VerifyTitle("PDF Templates");

            executionLog.Log("CreatePDFTemplate", "Click On PDF Package");
            pDFTemplate_PDFTemplateHelper.ClickElement("PDFPackage");
            pDFTemplate_PDFTemplateHelper.WaitForWorkAround(3000);

            executionLog.Log("CreatePDFTemplate", "Verify page title.");
            VerifyTitle("PDF Packaging");

            executionLog.Log("CreatePDFTemplate", "Enter PDF Name");
            pDFTemplate_PDFTemplateHelper.TypeText("PackagePDFName", name);

            executionLog.Log("CreatePDFTemplate", "Select Module");
            pDFTemplate_PDFTemplateHelper.Select("PakageModule", "20");
            pDFTemplate_PDFTemplateHelper.WaitForWorkAround(3000);

            executionLog.Log("CreatePDFTemplate", "SelectCatory");
            pDFTemplate_PDFTemplateHelper.SelectByText("PakageCategory", "Card Service Agreements");

            executionLog.Log("CreatePDFTemplate", "Click Checkbox");
            pDFTemplate_PDFTemplateHelper.ClickElement("DisplayInTabs");

            executionLog.Log("CreatePDFTemplate", "Click on  Can Share");
            pDFTemplate_PDFTemplateHelper.ClickElement("CanShare");

            executionLog.Log("CreatePDFTemplate", "CanEmail");
            pDFTemplate_PDFTemplateHelper.ClickElement("CanEmail");

            executionLog.Log("CreatePDFTemplate", "Click on Save button");
            pDFTemplate_PDFTemplateHelper.ClickElement("SavePDFPakage");

            executionLog.Log("CreatePDFTemplate", "Wait for success message");
            pDFTemplate_PDFTemplateHelper.WaitForText("PDF Package Template Created Successfully.", 10);

            executionLog.Log("CreatePDFTemplate", "Redirect to templates page");
            VisitOffice("pdf_templates");
            pDFTemplate_PDFTemplateHelper.WaitForWorkAround(3000);

            executionLog.Log("CreatePDFTemplate", "Verify title");
            VerifyTitle("PDF Templates");

            executionLog.Log("CreatePDFTemplate", "Search pdf template ");
            pDFTemplate_PDFTemplateHelper.TypeText("SearchPDF", name);
            pDFTemplate_PDFTemplateHelper.WaitForWorkAround(4000);

            executionLog.Log("CreatePDFTemplate", "Select template type pakage ");
            pDFTemplate_PDFTemplateHelper.SelectByText("SelectType", "Package");

            executionLog.Log("CreatePDFTemplate", "Select first pdf");
            pDFTemplate_PDFTemplateHelper.ClickElement("Checkbox1");

            executionLog.Log("CreatePDFTemplate", "Click on delete button ");
            pDFTemplate_PDFTemplateHelper.ClickElement("DeletePdf");

            executionLog.Log("CreatePDFTemplate", "Accept alert message. ");
            pDFTemplate_PDFTemplateHelper.AcceptAlert();

            executionLog.Log("CreatePDFTemplate", "Wait for message ");
            pDFTemplate_PDFTemplateHelper.WaitForText("PDF Template Deleted Successfully.", 10);

        }

            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CreatePDFTemplate");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Create PDF Template");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Create PDF Template", "Bug", "Medium", "PDF page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Create PDF Template");
                        TakeScreenshot("CreatePDFTemplate");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreatePDFTemplate.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CreatePDFTemplate");
                        string id = loginHelper.getIssueID("Create PDF Template");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreatePDFTemplate.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Create PDF Template"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Create PDF Template");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("CreatePDFTemplate");
                executionLog.WriteInExcel("Create PDF Template", Status, JIRA, "PDF Import");
            }
        }
    }
}
      