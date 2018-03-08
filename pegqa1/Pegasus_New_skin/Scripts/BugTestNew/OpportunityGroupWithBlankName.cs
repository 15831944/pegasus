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
    public class OpportunityGroupWithBlankName : DriverTestCase
    {
        [TestMethod]
        [TestCategory("Bug")]
        [TestCategory("TS2")]
        [TestCategory("BugTestNew")]
        public void opportunityGroupWithBlankName()
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

            //try
            //{
                executionLog.Log("OpportunityGroupWithBlankName", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("OpportunityGroupWithBlankName", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("OpportunityGroupWithBlankName", "Redirect at opportunities group page.");
                VisitOffice("opportunities/manage_groups");

                executionLog.Log("OpportunityGroupWithBlankName", "Click on Edit group.");
                office_OpportunitiesHelper.ClickElement("EditGroup");

                executionLog.Log("OpportunityGroupWithBlankName", "Remove the group name.");
                office_OpportunitiesHelper.TypeText("GroupName", "");

                executionLog.Log("OpportunityGroupWithBlankName", "Click on Save button.");
                office_OpportunitiesHelper.ClickElement("SaveGroup");

                executionLog.Log("OpportunityGroupWithBlankName", "Wait for locator to be present.");
                office_OpportunitiesHelper.WaitForElementPresent("GroupError", 10);

                executionLog.Log("OpportunityGroupWithBlankName", "Verify  validation is displayed.");
                office_OpportunitiesHelper.VerifyText("GroupError", "Name: Field is required");

            //}
            //catch (Exception e)
            //{
            //    executionLog.Log("Error", e.StackTrace);
            //    Status = "Fail";

            //    String counter = executionLog.readLastLine("counter");
            //    String Description = executionLog.GetAllTextFile("OpportunityGroupWithBlankName");
            //    String Error = executionLog.GetAllTextFile("Error");
            //    if (counter == "")
            //    {
            //        counter = "0";
            //    }
            //    bool result = loginHelper.CheckExstingIssue("Opportunity Group With Blank Name");
            //    if (!result)
            //    {
            //        if (Int16.Parse(counter) < 9)
            //        {
            //            executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
            //            loginHelper.CreateIssue("Opportunity Group With Blank Name", "Bug", "Medium", "Opportunities page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
            //            string id = loginHelper.getIssueID("Opportunity Group With Blank Name");
            //            TakeScreenshot("OpportunityGroupWithBlankName");
            //            string directoryName = loginHelper.GetnewDirectoryName(GetPath());
            //            var location = directoryName + "\\OpportunityGroupWithBlankName.png";
            //            loginHelper.AddAttachment(location, id);
            //        }
            //    }
            //    else
            //    {
            //        if (Int16.Parse(counter) < 9)
            //        {
            //            executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
            //            TakeScreenshot("OpportunityGroupWithBlankName");
            //            string id = loginHelper.getIssueID("Opportunity Group With Blank Name");
            //            string directoryName = loginHelper.GetnewDirectoryName(GetPath());
            //            var location = directoryName + "\\OpportunityGroupWithBlankName.png";
            //            loginHelper.AddAttachment(location, id);
            //            loginHelper.AddComment(loginHelper.getIssueID("Opportunity Group With Blank Name"), "This issue is still occurring");
            //        }
            //    }
            //    JIRA = loginHelper.getIssueID("Opportunity Group With Blank Name");
            ////    executionLog.DeleteFile("Error");
            //    throw;

            //}
            //finally
            //{
            //    executionLog.DeleteFile("OpportunityGroupWithBlankName");
            //    executionLog.WriteInExcel("Opportunity Group With Blank Name", Status, JIRA, "Opportunities Management");
            //}
        }
    }
}