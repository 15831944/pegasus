using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class AdminCorpTaskURLChange : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Url")]
        [TestCategory("TS3")]
        [TestCategory("ChangeUrl")]
        public void adminCorpTaskURLChange()
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
            var officeActivities_TasksHelper = new OfficeActivities_TasksHelper(GetWebDriver());


            // Variable
            var FirstName = "Test" + GetRandomNumber();
            var LastName = "Tester" + GetRandomNumber();
            var Number = "12345678" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("AdminCorpTaskURLChange", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("AdminCorpTaskURLChange", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("AdminCorpTaskURLChange", "Goto User Admin >> Corporate");
                VisitOffice("mycorp");
                officeActivities_TasksHelper.WaitForWorkAround(1000);

                executionLog.Log("AdminCorpTaskURLChange", "Select Activity >> Tasks");
                officeActivities_TasksHelper.Select("SelectActivityType", "Tasks");
                officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("AdminCorpTaskURLChange", "Click On Subject");
                officeActivities_TasksHelper.ClickElement("OpenTask");
                officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("AdminCorpTaskURLChange", "Change the url with the url number of another office");
                VisitOffice("viewactivity/task/27");
                officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("AdminCorpTaskURLChange", "Verify Validation");
                officeActivities_TasksHelper.WaitForText("You don't have privileges to view this office activity.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("AdminCorpTaskURLChange");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Admin Corp Task URL Change");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Admin Corp Task URL Change", "Bug", "Medium", "Corporate page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Admin Corp Task URL Change");
                        TakeScreenshot("AdminCorpTaskURLChange");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AdminCorpTaskURLChange.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("AdminCorpTaskURLChange");
                        string id = loginHelper.getIssueID("Admin Corp Task URL Change");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AdminCorpTaskURLChange.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Admin Corp Task URL Change"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Admin Corp Task URL Change");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("AdminCorpTaskURLChange");
                executionLog.WriteInExcel("Admin Corp Task URL Change", Status, JIRA, "My Corp");
            }
        }
    }
}
