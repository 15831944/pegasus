using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class ClientStatisticsChangeURL : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("Fail")]
        [TestCategory("TS1")]
        [TestCategory("BugTest")]
        public void clientStatisticsChangeURL()
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
            var Corp_StatisticsHelper = new Corp_Statistics_UserStatisticsHelper(GetWebDriver());


            // Variable

            var FirstName = "Test" + GetRandomNumber();
            var LastName = "Tester" + GetRandomNumber();
            var Number = "12345678" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("ClientStatisticsChangeURL", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("ClientStatisticsChangeURL", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("ClientStatisticsChangeURL", "Goto User Statistics");
                VisitCorp("user_statistics");
                Corp_StatisticsHelper.WaitForWorkAround(1000);

                executionLog.Log("ClientStatisticsChangeURL", "Select the date");
                Corp_StatisticsHelper.Select("SelectDate", "Last 12 Months");
                Corp_StatisticsHelper.WaitForWorkAround(3000);

                executionLog.Log("ClientStatisticsChangeURL", "Click On Date");
                Corp_StatisticsHelper.ClickElement("ClickOnTheDate");
                Corp_StatisticsHelper.WaitForWorkAround(4000);

                executionLog.Log("ClientStatisticsChangeURL", "Go to details of Any Other Merchant");
                VisitCorp("user_statsbycorp/33/2016-01-11");
                Corp_StatisticsHelper.WaitForWorkAround(4000);

                executionLog.Log("ClientStatisticsChangeURL", "Verify Validation");
                Corp_StatisticsHelper.VerifyPageText("You don't have privileges to view this statistics.");
                Corp_StatisticsHelper.WaitForWorkAround(4000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("ClientStatisticsChangeURL");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Client Statistics Change URL");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Client Statistics Change URL", "Bug", "Medium", "Client page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Client Statistics Change URL");
                        TakeScreenshot("ClientStatisticsChangeURL");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ClientStatisticsChangeURL.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("ClientStatisticsChangeURL");
                        string id = loginHelper.getIssueID("Client Statistics Change URL");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ClientStatisticsChangeURL.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Client Statistics Change URL"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Client Statistics Change URL");
               // executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("ClientStatisticsChangeURL");
                executionLog.WriteInExcel("Client Statistics Change URL", Status, JIRA, "Corp Statistics");
            }
        }
    }
}
