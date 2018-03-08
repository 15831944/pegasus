using System;
using System.IO;
using LinqToExcel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;


namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class TicketCommentPopup : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS5")]
        [TestCategory("NewSkinTaskJira")]
        public void ticketCommentPopup()
        {
            string[] username = null;
            string[] password = null;
            string[] log = null;
            String JIRA = "";
            String Status = "Pass";

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");
            log = oXMLData.getData("settings/URL", "logout");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var officeTickets_AllTicketsHelper = new OfficeTickets_AllTicketsHelper(GetWebDriver());

            try
            {
                executionLog.Log("TicketCommentPopup", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("TicketCommentPopup", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("TicketCommentPopup", "Go to Ticket open page");
                VisitOffice("ticket-search/open");

                executionLog.Log("TicketCommentPopup", "Verify title");
                VerifyTitle("Tickets");

                executionLog.Log("TicketCommentPopup", "Open a ticket");
                officeTickets_AllTicketsHelper.ClickElement("ClickTicket1");
                officeTickets_AllTicketsHelper.WaitForWorkAround(2000);

                executionLog.Log("TicketCommentPopup", "Verify title");
                VerifyTitle("Ticket View");

                executionLog.Log("TicketCommentPopup", "Click on 'Add Comment' button");
                officeTickets_AllTicketsHelper.ClickElement("TicketEdit");

                executionLog.Log("TicketCommentPopup", "wait");
                officeTickets_AllTicketsHelper.WaitForWorkAround(2000);

                executionLog.Log("TicketCommentPopup", "Verify text");
                officeTickets_AllTicketsHelper.VerifyPageText("Attach a File ");
                officeTickets_AllTicketsHelper.WaitForWorkAround(2000);

                executionLog.Log("TicketCommentPopup", "Log out from the application");
                VisitOffice("logout");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("TicketCommentPopup");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Ticket Comment Popup");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Ticket Comment Popup", "Bug", "Medium", "Ticket page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Ticket Comment Popup");
                        TakeScreenshot("TicketCommentPopup");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\TicketCommentPopup.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("TicketCommentPopup");
                        string id = loginHelper.getIssueID("Ticketing System");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\TicketCommentPopup.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Ticket Comment Popup"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Ticket Comment Popup");
           //     executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("TicketCommentPopup");
                executionLog.WriteInExcel("Ticket Comment Popup", Status, JIRA, "Ticketing System");
            }
        }
    }
}