using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class TicketsAdvanceFilterRelatedTo : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin1")]
        [TestCategory("TS7")]
        [TestCategory("Pegasus_Admin1")]
        public void ticketsAdvanceFilterRelatedTo()
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
            var officeTickets_AllTicketsHelper = new OfficeTickets_AllTicketsHelper(GetWebDriver());

            // Variable
            String JIRA = "";
            String Status = "Pass";

            try
            {
            executionLog.Log("TicketsAdvanceFilterRelatedTo", "Login with valid username and password");
            Login(username[0], password[0]);
            Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

            executionLog.Log("TicketsAdvanceFilterRelatedTo", "Verify Page title");
            VerifyTitle("Dashboard");
            Console.WriteLine("Redirected at Dashboard screen.");
                    


            // Verify tickets with notes.

            executionLog.Log("TicketsAdvanceFilterRelatedTo", "Redirect To URL");
            VisitOffice("tickets");
            officeTickets_AllTicketsHelper.WaitForWorkAround(3000);

            executionLog.Log("TicketsAdvanceFilterRelatedTo", "Verify page title.");
            VerifyTitle("Tickets");
            //officeTickets_AllTicketsHelper.WaitForElementVisible("AdvanceFilter", 10);

            executionLog.Log("TicketsAdvanceFilterRelatedTo", "Click on advance filter.");
            officeTickets_AllTicketsHelper.ClickElement("AdvanceFilter");
            officeTickets_AllTicketsHelper.WaitForWorkAround(2000);

            executionLog.Log("TicketsAdvanceFilterRelatedTo", "click ticket with activity type.");
            officeTickets_AllTicketsHelper.ClickForce("TicketswithNotes");
            //officeTickets_AllTicketsHelper.WaitForWorkAround(3000);

            executionLog.Log("TicketsAdvanceFilterRelatedTo", "Click on apply button.");
            officeTickets_AllTicketsHelper.ClickForce("Apply");
            officeTickets_AllTicketsHelper.WaitForWorkAround(3000);

            officeTickets_AllTicketsHelper.ClickElement("ClickTicket1");
            officeTickets_AllTicketsHelper.WaitForWorkAround(3000);

            executionLog.Log("TicketsAdvanceFilterRelatedTo", "Select activity type as notes.");
            officeTickets_AllTicketsHelper.SelectByText("SelectActivity", "Notes");
            officeTickets_AllTicketsHelper.WaitForWorkAround(3000);

            executionLog.Log("TicketsAdvanceFilterRelatedTo", "Verify note present for ticket.");
            officeTickets_AllTicketsHelper.VerifyText("NoteTicket", "Notes");
            //officeTickets_AllTicketsHelper.WaitForWorkAround(3000);


            //Verify tickets with documents.

            executionLog.Log("TicketsAdvanceFilterRelatedTo", "Redirect To URL");
            VisitOffice("tickets");
            officeTickets_AllTicketsHelper.WaitForWorkAround(3000);

            executionLog.Log("TicketsAdvanceFilterRelatedTo", "Verify page title.");
            VerifyTitle("Tickets");
            //officeTickets_AllTicketsHelper.WaitForElementVisible("AdvanceFilter", 10);

            executionLog.Log("TicketsAdvanceFilterRelatedTo", "Click on advance filter.");
            officeTickets_AllTicketsHelper.ClickElement("AdvanceFilter");
            officeTickets_AllTicketsHelper.WaitForWorkAround(2000);

            executionLog.Log("TicketsAdvanceFilterRelatedTo", "click ticket with activity type.");
            officeTickets_AllTicketsHelper.ClickForce("TicketWithDocs");
            //officeTickets_AllTicketsHelper.WaitForWorkAround(3000);

            executionLog.Log("TicketsAdvanceFilterRelatedTo", "Click on apply button.");
            officeTickets_AllTicketsHelper.ClickForce("Apply");
            officeTickets_AllTicketsHelper.WaitForWorkAround(3000);

            officeTickets_AllTicketsHelper.ClickElement("ClickTicket1");
            officeTickets_AllTicketsHelper.WaitForWorkAround(3000);

            executionLog.Log("TicketsAdvanceFilterRelatedTo", "Select activity type as documents .");
            officeTickets_AllTicketsHelper.SelectByText("SelectActivity", "Documents");
            officeTickets_AllTicketsHelper.WaitForWorkAround(3000);

            executionLog.Log("TicketsAdvanceFilterRelatedTo", "Verify ticket present has document.");
            officeTickets_AllTicketsHelper.VerifyText("NoteTicket", "Documents");
            //officeTickets_AllTicketsHelper.WaitForWorkAround(3000);


            //Verify ticket with meetings.

            executionLog.Log("TicketsAdvanceFilterRelatedTo", "Redirect To URL");
            VisitOffice("tickets");
            officeTickets_AllTicketsHelper.WaitForWorkAround(3000);

            executionLog.Log("TicketsAdvanceFilterRelatedTo", "Verify page title.");
            VerifyTitle("Tickets");
            //officeTickets_AllTicketsHelper.WaitForElementVisible("AdvanceFilter", 10);

            executionLog.Log("TicketsAdvanceFilterRelatedTo", "Click on advance filter.");
            officeTickets_AllTicketsHelper.ClickElement("AdvanceFilter");
            officeTickets_AllTicketsHelper.WaitForWorkAround(2000);

            executionLog.Log("TicketsAdvanceFilterRelatedTo", "Click ticket with activity type.");
            officeTickets_AllTicketsHelper.ClickForce("TicketsWithMeetings");
            //officeTickets_AllTicketsHelper.WaitForWorkAround(3000);

            executionLog.Log("TicketsAdvanceFilterRelatedTo", "Click on apply button.");
            officeTickets_AllTicketsHelper.ClickForce("Apply");
            officeTickets_AllTicketsHelper.WaitForWorkAround(3000);

            officeTickets_AllTicketsHelper.ClickElement("ClickTicket1");
            officeTickets_AllTicketsHelper.WaitForWorkAround(3000);

            executionLog.Log("TicketsAdvanceFilterRelatedTo", "Select activity type as meetings");
            officeTickets_AllTicketsHelper.SelectByText("SelectActivity", "Meetings");
            officeTickets_AllTicketsHelper.WaitForWorkAround(3000);

            executionLog.Log("TicketsAdvanceFilterRelatedTo", "Verify meeting present for ticket.");
            officeTickets_AllTicketsHelper.VerifyText("NoteTicket", "Meeting");
            //officeTickets_AllTicketsHelper.WaitForWorkAround(3000);


            // Verify ticket with tasks .

            executionLog.Log("TicketsAdvanceFilterRelatedTo", "Redirect To URL");
            VisitOffice("tickets");
            officeTickets_AllTicketsHelper.WaitForWorkAround(3000);

            executionLog.Log("TicketsAdvanceFilterRelatedTo", "Verify page title.");
            VerifyTitle("Tickets");
            //officeTickets_AllTicketsHelper.WaitForElementVisible("AdvanceFilter", 10);

            executionLog.Log("TicketsAdvanceFilterRelatedTo", "Click on advance filter.");
            officeTickets_AllTicketsHelper.ClickElement("AdvanceFilter");
            officeTickets_AllTicketsHelper.WaitForWorkAround(2000);

            executionLog.Log("TicketsAdvanceFilterRelatedTo", "click ticket with activity type.");
            officeTickets_AllTicketsHelper.ClickForce("TicketWithTasks");
            officeTickets_AllTicketsHelper.WaitForWorkAround(3000);

            executionLog.Log("TicketsAdvanceFilterRelatedTo", "Click on apply button.");
            officeTickets_AllTicketsHelper.ClickForce("Apply");
            officeTickets_AllTicketsHelper.WaitForWorkAround(3000);

            officeTickets_AllTicketsHelper.ClickElement("ClickTicket1");
            officeTickets_AllTicketsHelper.WaitForWorkAround(3000);

            executionLog.Log("TicketsAdvanceFilterRelatedTo", "Select activity type as tasks.");
            officeTickets_AllTicketsHelper.SelectByText("SelectActivity", "Tasks");
            officeTickets_AllTicketsHelper.WaitForWorkAround(3000);

            executionLog.Log("TicketsAdvanceFilterRelatedTo", "Verify task present for the ticket.");
            officeTickets_AllTicketsHelper.VerifyText("NoteTicket", "Task");
            //officeTickets_AllTicketsHelper.WaitForWorkAround(3000);


            // Verify ticket with calls .

            executionLog.Log("TicketsAdvanceFilterRelatedTo", "Redirect To URL");
            VisitOffice("tickets");
            officeTickets_AllTicketsHelper.WaitForWorkAround(3000);

            executionLog.Log("TicketsAdvanceFilterRelatedTo", "Verify page title.");
            VerifyTitle("Tickets");
            //officeTickets_AllTicketsHelper.WaitForElementVisible("AdvanceFilter", 10);

            executionLog.Log("TicketsAdvanceFilterRelatedTo", "Click on advance filter.");
            officeTickets_AllTicketsHelper.ClickElement("AdvanceFilter");
            officeTickets_AllTicketsHelper.WaitForWorkAround(2000);

            executionLog.Log("TicketsAdvanceFilterRelatedTo", "click ticket with activity type.");
            officeTickets_AllTicketsHelper.ClickForce("TicketWithCalls");
            //officeTickets_AllTicketsHelper.WaitForWorkAround(3000);

            executionLog.Log("TicketsAdvanceFilterRelatedTo", "Click on apply button.");
            officeTickets_AllTicketsHelper.ClickForce("Apply");
            officeTickets_AllTicketsHelper.WaitForWorkAround(3000);

            officeTickets_AllTicketsHelper.ClickElement("ClickTicket1");
            officeTickets_AllTicketsHelper.WaitForWorkAround(3000);

            executionLog.Log("TicketsAdvanceFilterRelatedTo", "Select activity type as calls.");
            officeTickets_AllTicketsHelper.SelectByText("SelectActivity", "Calls");
            officeTickets_AllTicketsHelper.WaitForWorkAround(3000);

            executionLog.Log("TicketsAdvanceFilterRelatedTo", "Verify calls present for ticket.");
            officeTickets_AllTicketsHelper.VerifyText("NoteTicket", "Calls");
            //officeTickets_AllTicketsHelper.WaitForWorkAround(3000);

            // Verify tickets with attachments .

            executionLog.Log("TicketsAdvanceFilterRelatedTo", "Redirect To URL");
            VisitOffice("tickets");
            officeTickets_AllTicketsHelper.WaitForWorkAround(3000);

            executionLog.Log("TicketsAdvanceFilterRelatedTo", "Verify page title.");
            VerifyTitle("Tickets");
            //officeTickets_AllTicketsHelper.WaitForElementVisible("AdvanceFilter", 10);

            executionLog.Log("TicketsAdvanceFilterRelatedTo", "Click on advance filter.");
            officeTickets_AllTicketsHelper.ClickElement("AdvanceFilter");
            officeTickets_AllTicketsHelper.WaitForWorkAround(2000);

            executionLog.Log("TicketsAdvanceFilterRelatedTo", "click ticket with activity type.");
            officeTickets_AllTicketsHelper.ClickForce("TicketWithCalls");
            //officeTickets_AllTicketsHelper.WaitForWorkAround(3000);

            executionLog.Log("TicketsAdvanceFilterRelatedTo", "Click on apply button.");
            officeTickets_AllTicketsHelper.ClickForce("Apply");
            officeTickets_AllTicketsHelper.WaitForWorkAround(3000);

            officeTickets_AllTicketsHelper.ClickElement("ClickTicket1");
            officeTickets_AllTicketsHelper.WaitForWorkAround(3000);

            executionLog.Log("TicketsAdvanceFilterRelatedTo", "Verify document present is related to clients");
            officeTickets_AllTicketsHelper.SelectByText("SelectActivity", "Calls");
            officeTickets_AllTicketsHelper.WaitForWorkAround(3000);

            executionLog.Log("TicketsAdvanceFilterRelatedTo", "Verify document present is related to clients");
            officeTickets_AllTicketsHelper.VerifyText("NoteTicket", "Calls");
            //officeTickets_AllTicketsHelper.WaitForWorkAround(3000);


        }
       catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("TicketsAdvanceFilterRelatedTo");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("TicketsAdvanceFilterRelatedTo");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("TicketsAdvanceFilterRelatedTo", "Bug", "Medium", "Opportunities page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("TicketsAdvanceFilterRelatedTo");
                        TakeScreenshot("TicketsAdvanceFilterRelatedTo");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\TicketsAdvanceFilterRelatedTo.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("TicketsAdvanceFilterRelatedTo");
                        string id = loginHelper.getIssueID("TicketsAdvanceFilterRelatedTo");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\TicketsAdvanceFilterRelatedTo.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("TicketsAdvanceFilterRelatedTo"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("TicketsAdvanceFilterRelatedTo");
                executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("TicketsAdvanceFilterRelatedTo");
                executionLog.WriteInExcel("TicketsAdvanceFilterRelatedTo", Status, JIRA, "Opportunities Management");
            }
        }
    }
} 