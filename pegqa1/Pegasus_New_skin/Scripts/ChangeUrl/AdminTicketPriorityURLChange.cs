using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class AdminTicketPriorityURLChange : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Url")]
        [TestCategory("Fail")]
        [TestCategory("TS3")]
        [TestCategory("ChangeUrl")]
        public void adminTicketPriorityURLChange()
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
            var tickets_MasterDataHelper = new Tickets_MasterDataHelper(GetWebDriver());


            // Variable
            var FirstName = "Test" + GetRandomNumber();
            var LastName = "Tester" + GetRandomNumber();
            var Number = "12345678" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("AdminTicketPriorityURLChange", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("AdminTicketPriorityURLChange", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("AdminTicketPriorityURLChange", "Go to Tickets >>  Priority");
                VisitOffice("tickets/masterdata/priority");
                tickets_MasterDataHelper.WaitForWorkAround(4000);

                executionLog.Log("AdminTicketPriorityURLChange", "Click On any Ticket >> Master Data Priority");
                tickets_MasterDataHelper.ClickJS("ClickOnAyTicketCategory");
                tickets_MasterDataHelper.WaitForWorkAround(4000);

                executionLog.Log("AdminTicketPriorityURLChange", "Change the url with the url number of another office");
                VisitOffice("tickets/masterdata/edit/priority/31");
                tickets_MasterDataHelper.WaitForWorkAround(3000);

                executionLog.Log("AdminTicketPriorityURLChange", "Verify Validation");
                tickets_MasterDataHelper.WaitForText("You don't have privilege.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("AdminTicketPriorityURLChange");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Admin Ticket Priority URL Change");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Admin Ticket Priority URL Change", "Bug", "Medium", "Ticket Priority page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Admin Ticket Priority URL Change");
                        TakeScreenshot("AdminTicketPriorityURLChange");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AdminTicketPriorityURLChange.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("AdminTicketPriorityURLChange");
                        string id = loginHelper.getIssueID("Admin Ticket Priority URL Change");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AdminTicketPriorityURLChange.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Admin Ticket Priority URL Change"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Admin Ticket Priority URL Change");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("AdminTicketPriorityURLChange");
                executionLog.WriteInExcel("Admin Ticket Priority URL Change", Status, JIRA, "Admin Tickets");
            }
        }
    }
}
