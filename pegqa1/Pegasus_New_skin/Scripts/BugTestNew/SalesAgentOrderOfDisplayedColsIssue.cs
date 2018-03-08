using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class SalesAgentOrderOfDisplayedColsIssue : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS3")]
        [TestCategory("BugTestNew")]
        public void salesAgentOrderOfDisplayedColsIssue()
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
            var agent_1099SalesAgentHelper = new Agent_1099SalesAgentHelper(GetWebDriver());

            // Variable Random
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("SalesAgentOrderOfDisplayedColsIssue", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("SalesAgentOrderOfDisplayedColsIssue", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("SalesAgentOrderOfDisplayedColsIssue", "Redirect To sales agent page");
                VisitOffice("sales_agents");
                agent_1099SalesAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("SalesAgentOrderOfDisplayedColsIssue", "Verify default position of modified column.");
                agent_1099SalesAgentHelper.VerifyElementDisplayed("Modified8");
                //agent_1099SalesAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("SalesAgentOrderOfDisplayedColsIssue", "Verify default position of department column.");
                agent_1099SalesAgentHelper.VerifyElementDisplayed("HeadDepartment6");
                //agent_1099SalesAgentHelper.WaitForWorkAround(3000);

                //executionLog.Log("SalesAgentOrderOfDisplayedColsIssue", "Redirect at sales agents page.");
                //VisitOffice("sales_agents");
                //agent_1099SalesAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("SalesAgentOrderOfDisplayedColsIssue", "Click on advance filter.");
                agent_1099SalesAgentHelper.ClickElement("AdvanceFilter");
                agent_1099SalesAgentHelper.WaitForWorkAround(1000);

                executionLog.Log("SalesAgentOrderOfDisplayedColsIssue", "Select modified in displayed column.");
                agent_1099SalesAgentHelper.SelectByText("DisplayedCols", "Modified");
                //agent_1099SalesAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("SalesAgentOrderOfDisplayedColsIssue", "Move modified 1 step up.");
                agent_1099SalesAgentHelper.ClickElement("MoveUp");
                //agent_1099SalesAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("SalesAgentOrderOfDisplayedColsIssue", "Move modified 1 step up.");
                agent_1099SalesAgentHelper.ClickElement("MoveUp");
                //agent_1099SalesAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("SalesAgentOrderOfDisplayedColsIssue", "Select department in displayed column.");
                agent_1099SalesAgentHelper.SelectByText("DisplayedCols", "Department");
                //agent_1099SalesAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("SalesAgentOrderOfDisplayedColsIssue", "Move department 1 step up.");
                agent_1099SalesAgentHelper.ClickElement("MoveUp");
                //agent_1099SalesAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("SalesAgentOrderOfDisplayedColsIssue", "Click on Apply button.");
                agent_1099SalesAgentHelper.ClickElement("ApplyButton");
                agent_1099SalesAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("SalesAgentOrderOfDisplayedColsIssue", "Verify changed position of modified column.");
                agent_1099SalesAgentHelper.VerifyElementDisplayed("Modified5");
                //agent_1099SalesAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("SalesAgentOrderOfDisplayedColsIssue", "Verify changed position of department column.");
                agent_1099SalesAgentHelper.VerifyElementDisplayed("HeadDepartment7");
                //agent_1099SalesAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("SalesAgentOrderOfDisplayedColsIssue", "Logout from the application.");
                VisitOffice("logout");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("SalesAgentOrderOfDisplayedColsIssue");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Sales Agent Order Of Displayed Cols Issue");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Sales Agent Order Of Displayed Cols Issue", "Bug", "Medium", "Activities page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Sales Agent Order Of Displayed Cols Issue");
                        TakeScreenshot("SalesAgentOrderOfDisplayedColsIssue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\SalesAgentOrderOfDisplayedColsIssue.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("SalesAgentOrderOfDisplayedColsIssue");
                        string id = loginHelper.getIssueID("Sales Agent Order Of Displayed Cols Issue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\SalesAgentOrderOfDisplayedColsIssue.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Sales Agent Order Of Displayed Cols Issue"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Sales Agent Order Of Displayed Cols Issue");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("SalesAgentOrderOfDisplayedColsIssue");
                executionLog.WriteInExcel("Sales Agent Order Of Displayed Cols Issue", Status, JIRA, "Meetings Management");
            }
        }
    }
}