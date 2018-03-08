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
    public class EditDocumentSubjectValidation : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS1")]
        [TestCategory("BugTest")]
        public void editDocumentSubjectValidation()
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
            var corp_Office_DocumentHelper = new Corp_Office_DocumentHelper(GetWebDriver());

            // Variable random
            var File = GetPathToFile() + "Up.jpg";
            String name = "Test" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("EditDocumentSubjectValidation", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("EditDocumentSubjectValidation", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("EditDocumentSubjectValidation", "Redirect To Offices");
                VisitCorp("offices");
                corp_Office_DocumentHelper.WaitForWorkAround(5000);

                executionLog.Log("EditDocumentSubjectValidation", "Click On any Client");
                corp_Office_DocumentHelper.ClickElement("ClickOnClientANY");
                corp_Office_DocumentHelper.WaitForWorkAround(4000);

                executionLog.Log("EditDocumentSubjectValidation", "Click On Add Document");
                corp_Office_DocumentHelper.ClickElement("ClickOnAddDocument");
                corp_Office_DocumentHelper.WaitForWorkAround(3000);

                executionLog.Log("EditDocumentSubjectValidation", "Enter Document Name");
                corp_Office_DocumentHelper.TypeText("EnterSubjectDocCorp", "Document Test");

                executionLog.Log("EditDocumentSubjectValidation", "Upload File");
                corp_Office_DocumentHelper.Upload("ClickAttachmentDoc", File);
                corp_Office_DocumentHelper.WaitForWorkAround(3000);

                executionLog.Log("EditDocumentSubjectValidation", "Click Save");
                corp_Office_DocumentHelper.ClickDisplayed("//a[@title='Save']");
                corp_Office_DocumentHelper.WaitForWorkAround(3000);

                executionLog.Log("EditDocumentSubjectValidation", "Verify Confirmation");
                corp_Office_DocumentHelper.WaitForText("Documents successfully Added.", 20);

                executionLog.Log("EditDocumentSubjectValidation", "Select Activity Document");
                corp_Office_DocumentHelper.Select("SelectTicketInMerchnatHistory", "Documents");
                corp_Office_DocumentHelper.WaitForWorkAround(5000);

                executionLog.Log("EditDocumentSubjectValidation", "Click On Edit Meeting Icon");
                corp_Office_DocumentHelper.ClickElement("ClickOnEditMeetingIcon");
                corp_Office_DocumentHelper.WaitForWorkAround(5000);

                executionLog.Log("EditDocumentSubjectValidation", "Reset subject to blank");
                corp_Office_DocumentHelper.TypeText("CorporateNameDocumentCorp", "");
                corp_Office_DocumentHelper.WaitForWorkAround(4000);

                executionLog.Log("EditDocumentSubjectValidation", "Click Save");
                corp_Office_DocumentHelper.ClickDisplayed("//button[@title='Save']");
                corp_Office_DocumentHelper.WaitForWorkAround(4000);

                executionLog.Log("EditDocumentSubjectValidation", "Verify");
                corp_Office_DocumentHelper.VerifyText("VerifyValidationForName", "This field is required.");
                corp_Office_DocumentHelper.WaitForWorkAround(3000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("EditDocumentSubjectValidation");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Edit Document Subject Validation");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Edit Document Subject Validation", "Bug", "Medium", "Document page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Edit Document Subject Validation");
                        TakeScreenshot("EditDocumentSubjectValidation");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditDocumentSubjectValidation.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("EditDocumentSubjectValidation");
                        string id = loginHelper.getIssueID("Edit Document Subject Validation");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditDocumentSubjectValidation.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Edit Document Subject Validation"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Edit Document Subject Validation");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("EditDocumentSubjectValidation");
                executionLog.WriteInExcel("Edit Document Subject Validation", Status, JIRA, "Office Activities");
            }
        }
    }
}