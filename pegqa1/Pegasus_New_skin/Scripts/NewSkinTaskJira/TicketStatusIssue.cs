using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class TicketStatusIssue : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS5")]
        [TestCategory("NewSkinTaskJira")]
        public void ticketStatusIssue()
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
            var officeTickets_CreateTicketsHelper = new OfficeTickets_CreateTicketsHelper(GetWebDriver());

            try
            {
                executionLog.Log("TicketStatusIssue", "Login with valid username and password");
                Login(username[0], password[0]);

                executionLog.Log("TicketStatusIssue", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("TicketStatusIssue", "Go to Create Ticket page");
                VisitOffice("tickets/create");
                officeTickets_CreateTicketsHelper.WaitForWorkAround(3000);

                executionLog.Log("TicketStatusIssue", "Verify title");
                VerifyTitle("Create a Ticket");

                executionLog.Log("TicketStatusIssue", "Set blank ticket status");
                officeTickets_CreateTicketsHelper.SelectByText("TickStatus", "Select Status");
                officeTickets_CreateTicketsHelper.WaitForWorkAround(3000);

                executionLog.Log("TicketStatusIssue", "Collapse the Assignment section");
                officeTickets_CreateTicketsHelper.ClickElement("TickAssi");
                officeTickets_CreateTicketsHelper.WaitForWorkAround(2000);

                executionLog.Log("TicketStatusIssue", "Enter ticket name");
                officeTickets_CreateTicketsHelper.TypeText("TicketName", "Other");

                executionLog.Log("TicketStatusIssue", "Click on Assign link");
                officeTickets_CreateTicketsHelper.ClickElement("Assign");
                officeTickets_CreateTicketsHelper.WaitForWorkAround(3000);

                executionLog.Log("TicketStatusIssue", "Click on Assign user");
                officeTickets_CreateTicketsHelper.ClickElement("AssignUser");

                executionLog.Log("TicketStatusIssue", "Wait for save button to be visible.");
                officeTickets_CreateTicketsHelper.WaitForElementPresent("Save", 10);

                executionLog.Log("TicketStatusIssue", "Click on 'Save' button");
                officeTickets_CreateTicketsHelper.ClickElement("Save");
                officeTickets_CreateTicketsHelper.WaitForWorkAround(3000);

                executionLog.Log("TicketStatusIssue", "Verify validation for required status field displayed");
                officeTickets_CreateTicketsHelper.verifyElementVisible("TickAssiErr");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("TicketStatusIssue");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Ticket Status Issue");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Ticket Status Issue", "Bug", "Medium", "Ticket page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Ticket Status Issue");
                        TakeScreenshot("TicketStatusIssue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\TicketStatusIssue.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("TicketStatusIssue");
                        string id = loginHelper.getIssueID("Ticket Status Issue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\TicketStatusIssue.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Ticket Status Issue"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Ticket Status Issue");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("TicketStatusIssue");
                executionLog.WriteInExcel("Ticket Status Issue", Status, JIRA, "Ticketing System");
            }
        }
    }
}