using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class AdminTicketResolutionURLChange : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Url")]
        [TestCategory("Temp")]
        [TestCategory("TS3")]
        [TestCategory("ChangeUrl")]
        public void adminTicketResolutionURLChange()
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
            var office_TicketHelper = new Tickets_MasterDataHelper(GetWebDriver());

            // Variable
            var FirstName = "Test" + GetRandomNumber();
            var LastName = "Tester" + GetRandomNumber();
            var Number = "12345678" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("AdminTicketResolutionURLChange", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("AdminTicketResolutionURLChange", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("AdminTicketResolutionURLChange", "Goto tickets >> Resolution");
                VisitOffice("tickets/masterdata/resolution");

                executionLog.Log("AdminTicketResolutionURLChange", "Click On any Ticket >> Master Data Resolution");
                office_TicketHelper.ClickElement("ClickOnAyTicketCategory");
                office_TicketHelper.WaitForWorkAround(2000);

                executionLog.Log("AdminTicketResolutionURLChange", "Change the url with the url number of another office");
                VisitOffice("tickets/masterdata/edit/priority/31");
                office_TicketHelper.WaitForWorkAround(1000);

                executionLog.Log("AdminTicketResolutionURLChange", "Verify Validation");
                office_TicketHelper.WaitForText("You don't have privilege.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("AdminTicketResolutionURLChange");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Admin Ticket Resolution URL Change");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Admin Ticket Resolution URL Change", "Bug", "Medium", "Ticket resolution page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Admin Ticket Resolution URL Change");
                        TakeScreenshot("AdminTicketResolutionURLChange");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AdminTicketResolutionURLChange.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("AdminTicketResolutionURLChange");
                        string id = loginHelper.getIssueID("Admin Ticket Resolution URL Change");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AdminTicketResolutionURLChange.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Admin Ticket Resolution URL Change"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Admin Ticket Resolution URL Change");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("AdminTicketResolutionURLChange");
                executionLog.WriteInExcel("Admin Ticket Resolution URL Change", Status, JIRA, "Admin Tickets");
            }
        }
    }
}
