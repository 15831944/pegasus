using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class TicketPopupIssue : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS5")]
        [TestCategory("NewSkinTaskJira")]
        public void ticketPopupIssue()
        {
            string[] username = null;
            string[] password = null;
            String JIRA = "";
            String Status = "Pass";

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var officeTickets_AllTicketsHelper = new OfficeTickets_AllTicketsHelper(GetWebDriver());

            var Name = "TicketQA" + RandomNumber(10, 100);

            try
            {
                executionLog.Log("TicketPopupIssue", "Login with valid username and password");
                Login(username[0], password[0]);

                executionLog.Log("TicketPopupIssue", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("TicketPopupIssue", "Go to Ticket page");
                VisitOffice("tickets");
                officeTickets_AllTicketsHelper.WaitForWorkAround(3000);

                executionLog.Log("TicketPopupIssue", "Verify title");
                VerifyTitle("Tickets");

                executionLog.Log("TicketPopupIssue", "Create the new ticket");
                officeTickets_AllTicketsHelper.ClickElement("CreateBtn");
                officeTickets_AllTicketsHelper.WaitForWorkAround(3000);

                executionLog.Log("TicketPopupIssue", "Enter the name");
                officeTickets_AllTicketsHelper.TypeText("Subject", Name);

                executionLog.Log("TicketPopupIssue", "select the status ");
                officeTickets_AllTicketsHelper.SelectByText("TicketStatus", "New");

                executionLog.Log("TicketPopupIssue", "select the client");
                officeTickets_AllTicketsHelper.ClickElement("SelectClientBtn");
                officeTickets_AllTicketsHelper.WaitForWorkAround(3000);
                officeTickets_AllTicketsHelper.ClickElement("FirstCompany");
                officeTickets_AllTicketsHelper.WaitForWorkAround(3000);

                executionLog.Log("TicketPopupIssue", "Click on Save button");
                officeTickets_AllTicketsHelper.ClickElement("SaveBtn");
                officeTickets_AllTicketsHelper.WaitForWorkAround(3000);

                VisitOffice("tickets");
                officeTickets_AllTicketsHelper.WaitForWorkAround(3000);

                executionLog.Log("TicketPopupIssue", "search the ticket");
                officeTickets_AllTicketsHelper.TypeText("SearchTicket", Name);
                officeTickets_AllTicketsHelper.WaitForWorkAround(2000);

                executionLog.Log("TicketPopupIssue", "Select first ticket");
                officeTickets_AllTicketsHelper.ClickElement("Ticket1");

                executionLog.Log("TicketPopupIssue", "Click on Delete button");
                officeTickets_AllTicketsHelper.ClickElement("DelTick");

                executionLog.Log("TicketPopupIssue", "Accept alert");
                officeTickets_AllTicketsHelper.AcceptAlert();

                executionLog.Log("TicketPopupIssue", "Verify title");
                VerifyTitle("Tickets");

                executionLog.Log("TicketPopupIssue", "Verify No Popup displayed");
                Console.WriteLine(officeTickets_AllTicketsHelper.VerifyAlertNotPresent());
                Assert.IsTrue(officeTickets_AllTicketsHelper.VerifyAlertNotPresent());

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("TicketPopupIssue");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Ticket Popup Issue");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Ticket Popup Issue", "Bug", "Medium", "Ticket page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Ticket Popup Issue");
                        TakeScreenshot("TicketPopupIssue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\TicketPopupIssue.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("TicketPopupIssue");
                        string id = loginHelper.getIssueID("Ticket Popup Issue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\TicketPopupIssue.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Ticket Popup Issue"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Ticket Popup Issue");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("TicketPopupIssue");
                executionLog.WriteInExcel("Ticket Popup Issue", Status, JIRA, "Ticketing System");
            }
        }
    }
}