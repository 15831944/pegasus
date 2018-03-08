using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class VerifyStartAndDueTimeOfTaskUnderTicket : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin")]
        [TestCategory("BugTestNew")]
        public void verifyStartAndDueTimeOfTaskUnderTicket()
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
            string todaydate = DateTime.Now.ToString("yyyy-MM-dd");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Variable
            var Subject = "Testtask" + RandomNumber(99, 99999);
            
            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("VerifyStartAndDueTimeOfTaskUnderTicket", "Login with valid credentials");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyStartAndDueTimeOfTaskUnderTicket", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyStartAndDueTimeOfTaskUnderTicket", "Go to All tickets page");
                VisitOffice("tickets");
                officeTickets_TicketsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyStartAndDueTimeOfTaskUnderTicket", "Verify page title.");
                VerifyTitle("Tickets");

                executionLog.Log("VerifyStartAndDueTimeOfTaskUnderTicket", "Click on a ticket");
                officeTickets_TicketsHelper.ClickElement("FirstTicket");
                officeTickets_TicketsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyStartAndDueTimeOfTaskUnderTicket", "Click on Add Note button");
                officeTickets_TicketsHelper.ClickElement("AddNote");
                officeTickets_TicketsHelper.WaitForWorkAround(3000);
                officeTickets_TicketsHelper.SwitchToWindow();

                executionLog.Log("VerifyStartAndDueTimeOfTaskUnderTicket", "Enter Subject of note");
                officeActivities_NotesHelper.TypeText("Subject", Subject);

                executionLog.Log("VerifyStartAndDueTimeOfTaskUnderTicket", "Select Add Task check box");
                officeActivities_NotesHelper.ClickElement("AddTaskChkBox");
                officeActivities_NotesHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyStartAndDueTimeOfTaskUnderTicket", "Enter Start Date of task");
                officeActivities_TasksHelper.TypeText("StartDate", "2017-10-10");

                executionLog.Log("VerifyStartAndDueTimeOfTaskUnderTicket", "Enter End Date of task");
                officeActivities_TasksHelper.TypeText("DueDate", "2017-10-10");

                executionLog.Log("VerifyStartAndDueTimeOfTaskUnderTicket", "Click on Save button");
                officeActivities_NotesHelper.ClickForce("Save");
                //officeActivities_TasksHelper.SwitchToWindow();
                officeActivities_TasksHelper.WaitForWorkAround(3000);
                officeActivities_TasksHelper.SwitchToWindow();
                officeActivities_TasksHelper.WaitForWorkAround(3000);
                officeTickets_TicketsHelper.WaitForText("Note successfully Created.", 05);

                executionLog.Log("VerifyStartAndDueTimeOfTaskUnderTicket", "Select Activity Type >> Tasks");
                officeTickets_TicketsHelper.WaitForWorkAround(3000);
                officeTickets_TicketsHelper.SelectByText("SelectActivityType", "Tasks");
                officeTickets_TicketsHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyStartAndDueTimeOfTaskUnderTicket", "Search Task by name");
                officeTickets_TicketsHelper.TypeText("SearchActivity", Subject);
                officeTickets_TicketsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyStartAndDueTimeOfTaskUnderTicket", "Open task");
                officeTickets_TicketsHelper.ClickElement("Activity1");
                officeTickets_TicketsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyStartAndDueTimeOfTaskUnderTicket", "Click on Edit button");
                officeActivities_TasksHelper.ClickElement("EditBtn");
                officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyStartAndDueTimeOfTaskUnderTicket", "Verify Start Time");
                officeActivities_TasksHelper.VerifySelectdOptn("StartHour", "01");
                officeActivities_TasksHelper.VerifySelectdOptn("StartMin", "00");
                officeActivities_TasksHelper.VerifySelectdOptn("StartAMPM", "AM");

                executionLog.Log("VerifyStartAndDueTimeOfTaskUnderTicket", "Verify Due Time");
                officeActivities_TasksHelper.VerifySelectdOptn("DueHour", "01");
                officeActivities_TasksHelper.VerifySelectdOptn("DueMin", "15");
                officeActivities_TasksHelper.VerifySelectdOptn("DueAMPM", "AM");
                Console.WriteLine(todaydate);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyStartAndDueTimeOfTaskUnderTicket");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Start And Due Time Of Task Under Ticket");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Start And Due Time Of Task Under Ticket", "Bug", "Medium", "Tickets page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Start And Due Time Of Task Under Ticket");
                        TakeScreenshot("VerifyStartAndDueTimeOfTaskUnderTicket");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyStartAndDueTimeOfTaskUnderTicket.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyStartAndDueTimeOfTaskUnderTicket");
                        string id = loginHelper.getIssueID("Verify Start And Due Time Of Task Under Ticket");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyStartAndDueTimeOfTaskUnderTicket.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Start And Due Time Of Task Under Ticket"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Start And Due Time Of Task Under Ticket");
                //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyStartAndDueTimeOfTaskUnderTicket");
                executionLog.WriteInExcel("Verify Start And Due Time Of Task Under Ticket", Status, JIRA, "Office Tickets");
            }
        }
    }
} 