using System;
using System.IO;
using LinqToExcel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;


namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class TicketComment : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS5")]
        [TestCategory("NewSkinTaskJira")]

        public void ticketComment()
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
            string comment = "My Comment " + RandomNumber(1, 9999);



            try
            {
                executionLog.Log("TicketComment", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("TicketComment", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("TicketComment", "Go to Ticket open page");
                VisitOffice("tickets");

                executionLog.Log("TicketComment", "Verify title");
                VerifyTitle("Tickets");

                executionLog.Log("TicketComment", "Open a ticket");
                officeTickets_AllTicketsHelper.ClickElement("ClickTicket1");

                executionLog.Log("TicketComment", "Verify title");
                VerifyTitle("Ticket View");

                executionLog.Log("TicketComment", "wait for 2 seconds");
                officeTickets_AllTicketsHelper.WaitForWorkAround(2000);

                executionLog.Log("TicketComment", "Click on 'Add Comment' button");
                officeTickets_AllTicketsHelper.ClickElement("TicketEdit");

                executionLog.Log("TicketComment", "wait");
                officeTickets_AllTicketsHelper.WaitForWorkAround(2000);

                executionLog.Log("TicketComment", "Type commment");
                officeTickets_AllTicketsHelper.TypeText("TicketComment", comment);

                executionLog.Log("TicketComment", "Click on save button");
                officeTickets_AllTicketsHelper.ClickElement("TicketCommentSave");

                executionLog.Log("TicketComment", "Verify No duplicate comment displayed");
                officeTickets_AllTicketsHelper.verifyCommentCount(comment, 1);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("TicketComment");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Ticket Comment");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Ticket Comment", "Bug", "Medium", "Tickets page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Ticket Comment");
                        TakeScreenshot("TicketComment");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\TicketComment.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("TicketComment");
                        string id = loginHelper.getIssueID("Ticketing System");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\TicketComment.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Ticket Comment"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Ticket Comment");
                //      executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("TicketComment");
                executionLog.WriteInExcel("Ticket Comment", Status, JIRA, "Ticketing System");
            }
        }
    }
}