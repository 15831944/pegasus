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
    public class EditOmahaPDFCorp : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS1")]
        [TestCategory("BugTest")]
        public void editOmahaPDFCorp()
        {
            string[] username = null;
            string[] password = null;

            XMLParse oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_corp");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var corp_PDFTemplate_PDFTemplateHelper = new CorpPDFTemplate_TemplateHelper(GetWebDriver());

            // Variable random
            String name = "Test" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("EditOmahaPDFCorp", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("EditOmahaPDFCorp", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("EditOmahaPDFCorp", "Goto pdf templates page.");
                VisitCorp("pdf_templates");
                corp_PDFTemplate_PDFTemplateHelper.WaitForWorkAround(1000);

                executionLog.Log("EditOmahaPDFCorp", "Click on edit icon");
                corp_PDFTemplate_PDFTemplateHelper.ClickElement("ClickEdit");

                executionLog.Log("EditOmahaPDFCorp", "Wait for text box to present.");
                corp_PDFTemplate_PDFTemplateHelper.WaitForElementPresent("EnterName", 10);

                executionLog.Log("EditOmahaPDFCorp", " Enter Name");
                corp_PDFTemplate_PDFTemplateHelper.TypeText("NameEdit", "First Data - Omaha");

                executionLog.Log("EditOmahaPDFCorp", "Click On Save");
                corp_PDFTemplate_PDFTemplateHelper.ClickElement("ClickSaveEdit");
                corp_PDFTemplate_PDFTemplateHelper.WaitForWorkAround(4000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("EditOmahaPDFCorp");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Edit Omaha PDF Corp");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Edit Omaha PDF Corp", "Bug", "Medium", "Pdf template page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Edit Omaha PDF Corp");
                        TakeScreenshot("EditOmahaPDFCorp");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditOmahaPDFCorp.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("EditOmahaPDFCorp");
                        string id = loginHelper.getIssueID("Edit Omaha PDF Corp");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditOmahaPDFCorp.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Edit Omaha PDF Corp"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Edit Omaha PDF Corp");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("EditOmahaPDFCorp");
                executionLog.WriteInExcel("Edit Omaha PDF Corp", Status, JIRA, "Corp PDF Templates");
            }
        }
    }
}