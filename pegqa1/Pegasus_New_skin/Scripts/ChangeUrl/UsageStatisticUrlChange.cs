using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class UsageStatisticUrlChange : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Url")]
        [TestCategory("TS4")]
        [TestCategory("ChangeUrl")]
        public void usageStatisticUrlChange()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_corp");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var corp_Statistics_UsageStatisticsHelper = new Corp_Statistics_UsageStatisticsHelper(GetWebDriver());

            // Random variables
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("UsageStatisticUrlChange", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("UsageStatisticUrlChange", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("UsageStatisticUrlChange", "Go To Usage Statistics");
                VisitCorp("usage_statistics");
               
                executionLog.Log("UsageStatisticUrlChange", "Click On any  User");
                corp_Statistics_UsageStatisticsHelper.ClickElement("ClickOnAnyActiveUser");
                corp_Statistics_UsageStatisticsHelper.WaitForWorkAround(2000);

                executionLog.Log("UsageStatisticUrlChange", "Change the url with the url number of another Corp");
                VisitCorp("usage_statsbycorp/3/2015-10-16");
                
                executionLog.Log("UsageStatisticUrlChange", "Verify Validation");
                corp_Statistics_UsageStatisticsHelper.WaitForText("You don't have privileges to view this statistics." ,10);
                
            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("UsageStatisticUrlChange");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0"; 
                }
                bool result = loginHelper.CheckExstingIssue("Usage Statistic Url Change");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Usage Statistic Url Change", "Bug", "Medium", "Usage Page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Usage Statistic Url Change");
                        TakeScreenshot("UsageStatisticUrlChange");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\UsageStatisticUrlChange.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("UsageStatisticUrlChange");
                        string id = loginHelper.getIssueID("Usage Statistic Url Change");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\UsageStatisticUrlChange.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Usage Statistic Url Change"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Usage Statistic Url Change");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("UsageStatisticUrlChange");
                executionLog.WriteInExcel("Usage Statistic Url Change", Status, JIRA, "Office Statistics");
            }
        }
    }
}
