using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class VerifyTicketStatusOfTaskUnderTicketHistory : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin")]
        [TestCategory("BugTestNew")]
        public void verifyTicketStatusOfTaskUnderTicketHistory()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            //Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var officeTickets_TicketsHelper = new OfficeTickets_TicketsHelper(GetWebDriver());
            var officeActivities_TasksHelper = new OfficeActivities_TasksHelper(GetWebDriver());
            var officeActivities_NotesHelper = new OfficeActivities_NotesHelper(GetWebDriver());

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Variable
            var Subject = "Testtask" + RandomNumber(99, 99999);
            
            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("VerifyTicketStatusOfTaskUnderTicketHistory", "Login with valid credentials");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyTicketStatusOfTaskUnderTicketHistory", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyTicketStatusOfTaskUnderTicketHistory", "Go to All tickets page");
                VisitOffice("tickets");
                officeTickets_TicketsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTicketStatusOfTaskUnderTicketHistory", "Verify page title.");
                VerifyTitle("Tickets");

                executionLog.Log("VerifyTicketStatusOfTaskUnderTicketHistory", "Select Status >> Active");
                officeTickets_TicketsHelper.SelectByText("SelectStatus", "Active");
                officeTickets_TicketsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTicketStatusOfTaskUnderTicketHistory", "Click on a ticket");
                officeTickets_TicketsHelper.ClickElement("FirstTicket");
                officeTickets_TicketsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTicketStatusOfTaskUnderTicketHistory", "Click on Add Note button");
                officeTickets_TicketsHelper.ClickElement("AddNote");
                officeTickets_TicketsHelper.WaitForWorkAround(3000);
                officeTickets_TicketsHelper.SwitchToWindow();     

                executionLog.Log("VerifyTicketStatusOfTaskUnderTicketHistory", "Enter Subject of note");
                officeActivities_NotesHelper.TypeText("Subject", Subject);

                executionLog.Log("VerifyTicketStatusOfTaskUnderTicketHistory", "Select Add Task check box");
                officeActivities_NotesHelper.ClickElement("AddTaskChkBox");
                officeActivities_NotesHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyTicketStatusOfTaskUnderTicketHistory", "Enter Start Date of task");
                officeActivities_TasksHelper.TypeText("StartDate", "2017-11-17");

                executionLog.Log("VerifyTicketStatusOfTaskUnderTicketHistory", "Enter End Date of task");
                officeActivities_TasksHelper.TypeText("DueDate", "2017-11-17");

                executionLog.Log("VerifyTicketStatusOfTaskUnderTicketHistory", "Click on Save button");
                officeActivities_NotesHelper.ClickForce("Save");
                //officeActivities_TasksHelper.SwitchToWindow();
                officeActivities_TasksHelper.WaitForWorkAround(3000);
                officeActivities_TasksHelper.SwitchToWindow();
                officeActivities_TasksHelper.WaitForWorkAround(3000);
                officeTickets_TicketsHelper.WaitForText("Note successfully Created.", 05);

                executionLog.Log("VerifyTicketStatusOfTaskUnderTicketHistory", "Select Activity Type >> Tasks");
                officeTickets_TicketsHelper.WaitForWorkAround(3000);
                officeTickets_TicketsHelper.SelectByText("SelectActivityType", "Tasks");
                officeTickets_TicketsHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyTicketStatusOfTaskUnderTicketHistory", "Search Task by name");
                officeTickets_TicketsHelper.TypeText("SearchActivity", Subject);
                officeTickets_TicketsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyTicketStatusOfTaskUnderTicketHistory", "Verify Contact Status is appearing");
                officeTickets_TicketsHelper.VerifyText("FirstActvtyTcktStatus", "Active");
                Console.WriteLine("Ticket Status is appearing in front of task under Ticket History ");


            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyTicketStatusOfTaskUnderTicketHistory");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Ticket Status Of Task Under Ticket History");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Ticket Status Of Task Under Ticket History", "Bug", "Medium", "Tickets page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Ticket Status Of Task Under Ticket History");
                        TakeScreenshot("VerifyTicketStatusOfTaskUnderTicketHistory");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyTicketStatusOfTaskUnderTicketHistory.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyTicketStatusOfTaskUnderTicketHistory");
                        string id = loginHelper.getIssueID("Verify Ticket Status Of Task Under Ticket History");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyTicketStatusOfTaskUnderTicketHistory.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Ticket Status Of Task Under Ticket History"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Ticket Status Of Task Under Ticket History");
                //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyTicketStatusOfTaskUnderTicketHistory");
                executionLog.WriteInExcel("Verify Ticket Status Of Task Under Ticket History", Status, JIRA, "Office Tickets");
            }
        }
    }
} 