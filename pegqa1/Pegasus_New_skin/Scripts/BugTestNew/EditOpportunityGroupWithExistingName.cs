using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class EditOpportunityGroupWithExistingName : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("Fail")]
        [TestCategory("TS2")]
        [TestCategory("BugTestNew")]
        public void editOpportunityGroupWithExistingName()
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
            var office_OpportunitiesHelper = new Office_OpportunitiesHelper(GetWebDriver());

            String JIRA = "";
            String Status = "Pass";
            try
            {
                executionLog.Log("EditOpportunityGroupWithExistingName", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("EditOpportunityGroupWithExistingName", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("EditOpportunityGroupWithExistingName", "Redirect at manage group page.");
                VisitOffice("opportunities/manage_groups");
                office_OpportunitiesHelper.WaitForWorkAround(3000);

                executionLog.Log("EditOpportunityGroupWithExistingName", "Click on edit opportunity group.");
                office_OpportunitiesHelper.ClickElement("EditGroup");
                office_OpportunitiesHelper.WaitForWorkAround(1000);

                executionLog.Log("EditOpportunityGroupWithExistingName", "Enter group name.");
                office_OpportunitiesHelper.TypeText("GroupName", "Required");

                executionLog.Log("EditOpportunityGroupWithExistingName", "Click on save group.");
                office_OpportunitiesHelper.ClickElement("SaveGroup");
                office_OpportunitiesHelper.WaitForWorkAround(2000);

                executionLog.Log("EditOpportunityGroupWithExistingName", "Verify validation for already existing.");
                office_OpportunitiesHelper.VerifyText("GroupError", "Name already exists");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("EditOpportunityGroupWithExistingName");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Edit Opportunity Group With Existing Name");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Edit Opportunity Group With Existing Name", "Bug", "Medium", "Opportunity page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Edit Opportunity Group With Existing Name");
                        TakeScreenshot("EditOpportunityGroupWithExistingName");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditOpportunityGroupWithExistingName.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("EditOpportunityGroupWithExistingName");
                        string id = loginHelper.getIssueID("Edit Opportunity Group With Existing Name");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditOpportunityGroupWithExistingName.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Edit Opportunity Group With Existing Name"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Edit Opportunity Group With Existing Name");
                executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("EditOpportunityGroupWithExistingName");
                executionLog.WriteInExcel("Edit Opportunity Group With Existing Name", Status, JIRA, "Opportunities Management");
            }
        }
    }
}