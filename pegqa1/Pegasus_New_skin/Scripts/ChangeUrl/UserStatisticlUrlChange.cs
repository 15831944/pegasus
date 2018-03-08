using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class UserStatisticlUrlChange : DriverTestCase
    {
        [TestMethod]
        [TestCategory("Url")]
        [TestCategory("All")]
        [TestCategory("TS4")]
        [TestCategory("ChangeUrl")]
        public void userStatisticlUrlChange()
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
            var corp_Statistics_UserStatisticsHelper = new Corp_Statistics_UserStatisticsHelper(GetWebDriver());

            // Variable
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("UserStatisticlUrlChange", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("UserStatisticlUrlChange", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("UserStatisticlUrlChange", "Go To User Statistics");
                VisitCorp("user_statistics");
                
                executionLog.Log("UserStatisticlUrlChange", "Click On any active user");
                corp_Statistics_UserStatisticsHelper.ClickElement("ClickOnAnyActiveUser");
                corp_Statistics_UserStatisticsHelper.WaitForWorkAround(2000);

                executionLog.Log("UserStatisticlUrlChange", "Change the url with the url number of another Corp");
                VisitCorp("user_statsbycorp/186/2015-10-16");
               
                executionLog.Log("UserStatisticlUrlChange", "Verify Validation");
                corp_Statistics_UserStatisticsHelper.WaitForText("You don't have privileges to view this statistics." ,10);
               
            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("UserStatisticlUrlChange");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0"; 
                }
                bool result = loginHelper.CheckExstingIssue("User Statistic Url Change");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("User Statistic Url Change", "Bug", "Medium", "Statistics page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("User Statistic Url Change");
                        TakeScreenshot("UserStatisticlUrlChange");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\UserStatisticlUrlChange.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("UserStatisticlUrlChange");
                        string id = loginHelper.getIssueID("User Statistic Url Change");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\UserStatisticlUrlChange.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("User Statistic Url Change"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("User Statistic Url Change");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("UserStatisticlUrlChange");
                executionLog.WriteInExcel("User Statistic Url Change", Status, JIRA, "Office Statistics");
            }
        }
    }
}
