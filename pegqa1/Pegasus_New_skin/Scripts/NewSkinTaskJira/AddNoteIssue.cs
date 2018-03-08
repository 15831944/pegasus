using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class AddNoteIssue : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS4")]
        [TestCategory("NewSkinTaskJira")]
        public void addNoteIssue()
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
            var office_ClientsHelper = new Office_ClientsHelper(GetWebDriver());
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("AddNoteIssue", "Login with valid username and password");
                Login(username[0], password[0]);

                executionLog.Log("AddNoteIssue", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("AddNoteIssue", "Go to client page");
                VisitOffice("clients");

                executionLog.Log("AddNoteIssue", "Verify title");
                VerifyTitle();

                executionLog.Log("AddNoteIssue", "Open a client");
                office_ClientsHelper.ClickElement("Client1");

                executionLog.Log("AddNoteIssue", "Verify title");
                VerifyTitle(" Details");

                executionLog.Log("AddNoteIssue", "Click on 'Add Note'button");
                office_ClientsHelper.ClickElement("ClcikAddNote");

                executionLog.Log("AddNoteIssue", "Verify Add note button working");
                office_ClientsHelper.WaitForWorkAround(4000);
            }

            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("AddNoteIssue");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Add Note Issue");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Add Note Issue", "Bug", "Medium", "Client page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Add Note Issue");
                        TakeScreenshot("AddNoteIssue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AddNoteIssue.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("AddNoteIssue");
                        string id = loginHelper.getIssueID("Add Note Issue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AddNoteIssue.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Add Note Issue"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Add Note Issue");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("AddNoteIssue");
                executionLog.WriteInExcel("Add Note Issue", Status, JIRA, "Office Activities");
            }
        }
    }
}