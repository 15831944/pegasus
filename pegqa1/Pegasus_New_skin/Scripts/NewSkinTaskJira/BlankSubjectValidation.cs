using System;
using System.IO;
using LinqToExcel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;


namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class BlankSubjectValidation : DriverTestCase
    {
        [TestMethod]

        [TestCategory("NewSkin_Task")]
        [TestCategory("All")]
        [TestCategory("Fail")]
        [TestCategory("TS4")]
        [TestCategory("NewSkinTaskJira")]
        public void blankSubjectValidation()
        {
            string[] username = null;
            string[] password = null;
            string[] log = null;
            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");
            log = oXMLData.getData("settings/URL", "logout");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var office_OpportunitiesHelper = new Office_OpportunitiesHelper(GetWebDriver());
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("BlankSubjectValidation", "Login with valid username and password");
                Login(username[0], password[0]);

                executionLog.Log("BlankSubjectValidation", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("BlankSubjectValidation", "Visit to Opportunities page");
                VisitOffice("opportunities");
                office_OpportunitiesHelper.WaitForWorkAround(5000);

                executionLog.Log("BlankSubjectValidation", "Verify title");
                VerifyTitle("Opportunities");

                executionLog.Log("BlankSubjectValidation", "Open an opportunities");
                office_OpportunitiesHelper.ClickElement("Opportunities1");
                office_OpportunitiesHelper.WaitForWorkAround(5000);

                executionLog.Log("BlankSubjectValidation", "Verify title");
                VerifyTitle("Details");
                office_OpportunitiesHelper.WaitForWorkAround(2000);

                executionLog.Log("BlankSubjectValidation", "Click on Add note button");
                office_OpportunitiesHelper.ClickElement("AddNote");
                office_OpportunitiesHelper.WaitForWorkAround(1000);

                executionLog.Log("BlankSubjectValidation", "Wait for text");
                office_OpportunitiesHelper.WaitForText("Create a Note", 05);

                executionLog.Log("BlankSubjectValidation", "Click on Save button without subject");
                office_OpportunitiesHelper.ClickElement("Save");
                office_OpportunitiesHelper.WaitForWorkAround(1000);

                executionLog.Log("BlankSubjectValidation", "Verify validation message");
                office_OpportunitiesHelper.WaitForText("This field is required", 10);

                executionLog.Log("BlankSubjectValidation", "Log out from the application");
                VisitOffice("logout");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("BlankSubjectValidation");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Blank Subject Validation");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Blank Subject Validation", "Bug", "Medium", "Opportunity page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Blank Subject Validation");
                        TakeScreenshot("BlankSubjectValidation");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\BlankSubjectValidation.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("BlankSubjectValidation");
                        string id = loginHelper.getIssueID("Blank Subject Validation");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\BlankSubjectValidation.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Blank Subject Validation"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Blank Subject Validation");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("BlankSubjectValidation");
                executionLog.WriteInExcel("Blank Subject Validation", Status, JIRA, "Office Opportunities");
            }
        }
    }
}