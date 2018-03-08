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
    public class FilterForAgent : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("Fail")]
        [TestCategory("TS1")]
        [TestCategory("BugTest")]
        public void filterForAgent()
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
            var agent_AllAgentHelper = new Agents_AllAgentsHelper(GetWebDriver());
            String JIRA = "";
            String Status = "Pass";


            try
            {
                executionLog.Log("FilterForAgent", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("FilterForAgent", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("FilterForAgent", "Visit agents pge.");
                VisitOffice("agents");
                agent_AllAgentHelper.WaitForWorkAround(1000);

                executionLog.Log("FilterForAgent", "Select Agent Type");
                agent_AllAgentHelper.Select("SelectUserType", "");
                agent_AllAgentHelper.WaitForWorkAround(2000);

                executionLog.Log("FilterForAgent", "Select sattus as Active");
                agent_AllAgentHelper.Select("SelectStatus", "Active");
                agent_AllAgentHelper.WaitForWorkAround(2000);

                executionLog.Log("FilterForAgent", "Select Role");
                agent_AllAgentHelper.Select("SelectRole", "");
                agent_AllAgentHelper.WaitForWorkAround(2000);

                executionLog.Log("FilterForAgent", "Select Agent Department");
                agent_AllAgentHelper.Select("SelectDepartment", "");
                agent_AllAgentHelper.WaitForWorkAround(2000);

                executionLog.Log("FilterForAgent", "Select Agent Team");
                agent_AllAgentHelper.Select("SelectTeam", "");
                agent_AllAgentHelper.WaitForWorkAround(2000);

            }


            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("FilterForAgent");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Filter For Agent");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Filter For Agent", "Bug", "Medium", "Agent page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Filter For Agent");
                        TakeScreenshot("FilterForAgent");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\FilterForAgent.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("FilterForAgent");
                        string id = loginHelper.getIssueID("Filter For Agent");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\FilterForAgent.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Filter For Agent"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Filter For Agent");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("FilterForAgent");
                executionLog.WriteInExcel("Filter For Agent", Status, JIRA, "Agents Portal");
            }
        }
    }
}