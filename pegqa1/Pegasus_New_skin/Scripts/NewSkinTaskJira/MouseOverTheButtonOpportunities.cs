using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class MouseOverTheButtonOpportunities : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS5")]
        [TestCategory("NewSkinTaskJira")]
        public void mouseOverTheButtonOpportunities()
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
            var office_OpportunitiesHelper = new Office_OpportunitiesHelper(GetWebDriver());

            // VARIABLE
            String Status = "Pass";
            String JIRA = "";

            try
            {
                executionLog.Log("MouseOverTheButtonOpportunities", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("MouseOverTheButtonOpportunities", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("MouseOverTheButtonOpportunities", " Redirect To Opportunities");
                VisitOffice("opportunities");
               
                executionLog.Log("MouseOverTheButtonOpportunities", "Mouse Hover All Opportunities");
                office_OpportunitiesHelper.MouseHover("AllOpportunities");
               
                executionLog.Log("MouseOverTheButtonOpportunities", "Mouse Hover All Opportunities");
                office_OpportunitiesHelper.MouseHover("MouserOverAllOpp");
               
                executionLog.Log("MouseOverTheButtonOpportunities", "Mouse Hover Opportunities without meeting.");
                office_OpportunitiesHelper.MouseHover("MouseOverWithOpenMeeting");
               
            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";
                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("MouseOverTheButtonOpportunities");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Mouse Over The Button Opportunities");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Mouse Over The Button Opportunities", "Bug", "Medium", "Opportunities page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Mouse Over The Button Opportunities");
                        TakeScreenshot("MouseOverTheButtonOpportunities");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\MouseOverTheButtonOpportunities.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("MouseOverTheButtonOpportunities");
                        string id = loginHelper.getIssueID("Mouse Over The Button Opportunities");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\MouseOverTheButtonOpportunities.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Mouse Over The Button Opportunities"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Mouse Over The Button Opportunities");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("MouseOverTheButtonOpportunities");
                executionLog.WriteInExcel("Mouse Over The Button Opportunities", Status, JIRA, "Opportunities Management");
            }
        }
    }
}