using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class EditPDFNameIssue : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS4")]
        [TestCategory("NewSkinTaskJira")]
        public void editPDFNameIssue()
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
                executionLog.Log("EditPDFNameIssue", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("EditPDFNameIssue", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("EditPDFNameIssue", "Go To Admin page");
                VisitOffice("admin");

                executionLog.Log("EditPDFNameIssue", "Go To Pdf template page");
                VisitOffice("pdf_templates");

                executionLog.Log("EditPDFNameIssue", "Verify title");
                VerifyTitle("PDF Templates");

                executionLog.Log("EditPDFNameIssue", "Edit First Template");
                pDFTemplate_PDFTemplateHelper.ClickElement("EditFirstTemplate");

                executionLog.Log("EditPDFNameIssue", "Verify title");
                VerifyTitle("Edit PDF Template");

                executionLog.Log("EditPDFNameIssue", "Remove name");
                pDFTemplate_PDFTemplateHelper.removeText("PdfName");

                executionLog.Log("EditPDFNameIssue", "Click on Save button");
                pDFTemplate_PDFTemplateHelper.ClickElement("SavebuttonEDit");

                executionLog.Log("EditPDFNameIssue", "Verify Pdf not save");
                VerifyTitle("Edit PDF Template");

                executionLog.Log("EditPDFNameIssue", "Verify validation message displayed");
                pDFTemplate_PDFTemplateHelper.verifyElementPresent("NameError");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";
                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("EditPDFNameIssue");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Edit PDF Name Issue");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Edit PDF Name Issue", "Bug", "Medium", "Pdf Template page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Edit PDF Name Issue");
                        TakeScreenshot("EditPDFNameIssue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditPDFNameIssue.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("EditPDFNameIssue");
                        string id = loginHelper.getIssueID("Edit PDF Name Issue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditPDFNameIssue.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Edit PDF Name Issue"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Edit PDF Name Issue");
                executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("EditPDFNameIssue");
                executionLog.WriteInExcel("Edit PDF Name Issue", Status, JIRA, "PDF Template");
            }
        }
    }
}