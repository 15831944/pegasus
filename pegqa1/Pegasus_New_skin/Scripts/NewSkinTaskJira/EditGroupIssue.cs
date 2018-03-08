using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class EditGroupIssue : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS4")]
        [TestCategory("NewSkinTaskJira")]
        public void editGroupIssue()
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
            var office_GroupHelper = new Office_GroupHelper(GetWebDriver());

            // VARIABLE
            var Group = "Group" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("EditGroupIssue", "Login with valid username and password");
                Login(username[0], password[0]);

                executionLog.Log("EditGroupIssue", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("EditGroupIssue", "Redirect  to Group page");
                VisitOffice("groups");

                executionLog.Log("EditGroupIssue", "Verify Page title");
                VerifyTitle("Groups");

                executionLog.Log("EditGroupIssue", "Click on Create button");
                office_GroupHelper.ClickElement("Create");

                executionLog.Log("EditGroupIssue", "Verify Page title");
                VerifyTitle("Create a Group");

                executionLog.Log("EditGroupIssue", "Enter Group name");
                office_GroupHelper.TypeText("Name", Group);

                executionLog.Log("EditGroupIssue", "Click on Save button");
                office_GroupHelper.ClickElement("Save");

                executionLog.Log("EditGroupIssue", "Verify Page title");
                VerifyTitle("Group Details");

                executionLog.Log("EditGroupIssue", "Click on Edit button");
                office_GroupHelper.ClickElement("GroupEdit");

                executionLog.Log("EditGroupIssue", "Verify Page title");
                VerifyTitle("Groups");

                executionLog.Log("EditGroupIssue", "Remove group name");
                office_GroupHelper.removeText("Name");

                executionLog.Log("EditGroupIssue", "Click on Save button");
                office_GroupHelper.ClickElement("SaveBtnAdjustmnet");

                executionLog.Log("EditGroupIssue", "Verify User is not able to save group without name");
                office_GroupHelper.verifyElementVisible("GroupNameError");

                executionLog.Log("EditGroupIssue", "Redirect To Group page");
                VisitOffice("groups");

                executionLog.Log("EditGroupIssue", "Verify title");
                VerifyTitle("Groups");

                executionLog.Log("EditGroupIssue", "Enter Name to search");
                office_GroupHelper.TypeText("SearchName", Group);
                office_GroupHelper.WaitForWorkAround(2000);

                executionLog.Log("EditGroupIssue", "cLICK Delete btn  ");
                office_GroupHelper.ClickElement("DeleteIcon");

                executionLog.Log("EditGroupIssue", "Accept alert message. ");
                office_GroupHelper.AcceptAlert();

                executionLog.Log("EditGroupIssue", "Wait for delete message. ");
                office_GroupHelper.WaitForText("Group Deleted Successfully.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("EditGroupIssue");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Edit Group Issue");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Edit Group Issue", "Bug", "Medium", "Group page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Edit Group Issue");
                        TakeScreenshot("EditGroupIssue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditGroupIssue.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("EditGroupIssue");
                        string id = loginHelper.getIssueID("Edit Group Issue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditGroupIssue.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Edit Group Issue"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Edit Group Issue");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("EditGroupIssue");
                executionLog.WriteInExcel("Edit Group Issue", Status, JIRA, "Office Admin");
            }
        }
    }
}