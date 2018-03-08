using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class SaleAgent2 : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin")]
        [TestCategory("TS8")]
        [TestCategory("Pegasus_NewSkin")]
        public void saleAgent2()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            // Initializing the objects5
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var officeActivities_MeetingHelper = new OfficeActivities_MeetingHelper(GetWebDriver());
            var officeTickets_CreateTicketsHelper = new OfficeTickets_CreateTicketsHelper(GetWebDriver());
            var officeActivities_TasksHelper = new OfficeActivities_TasksHelper(GetWebDriver());

            username = oXMLData.getData("settings/Credentials", "usernameSale");
            password = oXMLData.getData("settings/Credentials", "PasswordSale");

            // Variable
            var Meeting = "Meeting" + RandomNumber(1, 99999);
            var Task = "New" + RandomNumber(99, 99999);
            String JIRA = "";
            String Status = "Pass";


            try
            {
            executionLog.Log("SaleAgent2", "Login with valid credential  Username");
            Login(username[0], password[0]);

            executionLog.Log("SaleAgent2", "Verify Page title");
            VerifyTitle("Dashboard");
            Console.WriteLine("Redirected at Dashboard screen.");

            executionLog.Log("SaleAgent2", "Goto Ticket");
            VisitOffice("tickets");
            officeTickets_CreateTicketsHelper.WaitForWorkAround(3000);

            executionLog.Log("SaleAgent2", "Verify Ticket");
            officeTickets_CreateTicketsHelper.VerifyText("ViewHeadingTicket", "Tickets");

            executionLog.Log("SaleAgent2", "Goto create Ticket");
            VisitOffice("tickets/create");
            officeTickets_CreateTicketsHelper.WaitForWorkAround(3000);

            executionLog.Log("SaleAgent2", "Enter Ticket Subject");
            officeTickets_CreateTicketsHelper.TypeText("TicketName", "Test Ticket");

            executionLog.Log("SaleAgent2", "Click Assigned Icon");
            officeTickets_CreateTicketsHelper.ClickElement("ClientDisplayIcon");
            officeTickets_CreateTicketsHelper.WaitForWorkAround(2000);

            executionLog.Log("SaleAgent2", "Select Client Ticket");
            officeTickets_CreateTicketsHelper.ClickElement("Client");
            officeTickets_CreateTicketsHelper.WaitForWorkAround(2000);

            executionLog.Log("SaleAgent2", "Click on Save");
            officeTickets_CreateTicketsHelper.ClickElement("Save");
            officeTickets_CreateTicketsHelper.WaitForWorkAround(2000);

            executionLog.Log("SaleAgent2", "Verify Confirmation");
            officeTickets_CreateTicketsHelper.WaitForText("Ticket Created Successfully.", 10);

            executionLog.Log("SaleAgent2", "Click Edit Ticket Button");
            officeTickets_CreateTicketsHelper.ClickElement("ClickEditTicketBtn");
            officeTickets_CreateTicketsHelper.WaitForWorkAround(3000);

            executionLog.Log("SaleAgent2", "Click on Save");
            officeTickets_CreateTicketsHelper.ClickElement("Save");
            officeTickets_CreateTicketsHelper.WaitForWorkAround(3000);

            executionLog.Log("SaleAgent2", "Redirect at Create Task");
            VisitOffice("tasks/create");
            officeTickets_CreateTicketsHelper.WaitForWorkAround(3000);

            executionLog.Log("SaleAgent2", "Click on Save");
            officeActivities_TasksHelper.ClickElement("Save");
            //officeTickets_CreateTicketsHelper.WaitForWorkAround(3000);

            executionLog.Log("SaleAgent2", "Verify Validation text on page.");
            officeActivities_TasksHelper.VerifyText("ValdatiponForSujectField", "This field is required.");

            executionLog.Log("SaleAgent2", "Enter Subject");
            officeActivities_TasksHelper.TypeText("Subjuct1", Task);

            executionLog.Log("SaleAgent2", " Click Save");
            officeActivities_TasksHelper.ClickElement("Save");
            //officeTickets_CreateTicketsHelper.WaitForWorkAround(3000);

            executionLog.Log("SaleAgent2", "Verify Validation for date.");
            officeActivities_TasksHelper.VerifyText("Valdationforstartdate", "This field is required.");

            executionLog.Log("SaleAgent2", "Enter Start date");
            officeActivities_TasksHelper.TypeText("StartDate", "08/08/2017");

            executionLog.Log("SaleAgent2", "Enter End date");
            officeActivities_TasksHelper.TypeText("DueDate", "09/09/2017");

            executionLog.Log("SaleAgent2", "Select Related TO");
            officeActivities_TasksHelper.SelectByText("SelectRelatedTo", "Client");

            executionLog.Log("SaleAgent2", "Click on Assigned To");
            officeActivities_TasksHelper.ClickElement("SelectAssignedTo");
            officeTickets_CreateTicketsHelper.WaitForWorkAround(2000);

            executionLog.Log("SaleAgent2", "Select Client");
            officeActivities_TasksHelper.ClickElement("ClickToSelectClient");
            officeTickets_CreateTicketsHelper.WaitForWorkAround(2000);

            executionLog.Log("SaleAgent2", "Click on save.");
            officeActivities_TasksHelper.ClickElement("Save");
            officeTickets_CreateTicketsHelper.WaitForWorkAround(2000);

            executionLog.Log("SaleAgent2", "Wait for Confrmation");
            officeActivities_TasksHelper.WaitForText("Task saved successfully.", 10);

            executionLog.Log("SaleAgent2", "Rediret to task");
            VisitOffice("tasks");
            officeTickets_CreateTicketsHelper.WaitForWorkAround(3000);

            executionLog.Log("SaleAgent2", "Search Task");
            officeActivities_TasksHelper.TypeText("EnterSubject", Task);
            officeTickets_CreateTicketsHelper.WaitForWorkAround(2000);

            executionLog.Log("SaleAgent2", "Edit Task");
            officeActivities_TasksHelper.ClickElement("Edit");
            officeTickets_CreateTicketsHelper.WaitForWorkAround(3000);

            executionLog.Log("SaleAgent2", "Save");
            officeActivities_TasksHelper.ClickElement("Save");
            officeTickets_CreateTicketsHelper.WaitForWorkAround(2000);

            executionLog.Log("SaleAgent2", "Verify Confrmation");
            officeActivities_TasksHelper.WaitForText("Task Updated Success.", 10);

            executionLog.Log("SaleAgent2", "Rediected to task ");
            VisitOffice("meetings/create");
            officeTickets_CreateTicketsHelper.WaitForWorkAround(3000);

            executionLog.Log("SaleAgent2", "Enter Subject");
            officeActivities_MeetingHelper.TypeText("Subject", Meeting);

            executionLog.Log("SaleAgent2", "Enter Start date");
            officeActivities_MeetingHelper.TypeText("StartDate", "07/07/2017");
            //officeTickets_CreateTicketsHelper.WaitForWorkAround(3000);

            executionLog.Log("SaleAgent2", "Enter End date");
            officeActivities_MeetingHelper.TypeText("EndDate", "08/08/2017");

            executionLog.Log("SaleAgent2", "Select Client");
            officeActivities_MeetingHelper.SelectByText("RelatedTo", "Client");
            //officeTickets_CreateTicketsHelper.WaitForWorkAround(3000);

            executionLog.Log("SaleAgent2", "Click Assigned To");
            officeActivities_MeetingHelper.ClickElement("AssignedOwner");
            //officeTickets_CreateTicketsHelper.WaitForWorkAround(3000);

            executionLog.Log("SaleAgent2", "Click on find list icon.");
            officeActivities_MeetingHelper.ClickElement("FindListIcon");
            officeTickets_CreateTicketsHelper.WaitForWorkAround(2000);

            executionLog.Log("SaleAgent2", "Click on client");
            officeActivities_MeetingHelper.ClickElement("SelectClient");
            officeTickets_CreateTicketsHelper.WaitForWorkAround(3000);

            executionLog.Log("SaleAgent2", "Click on Save");
            officeActivities_MeetingHelper.Click("//button[@title='Save']");
            officeTickets_CreateTicketsHelper.WaitForWorkAround(2000);

            executionLog.Log("SaleAgent2", "Verify");
            officeTickets_CreateTicketsHelper.WaitForText("Meeting saved successfully. ", 10);

            executionLog.Log("SaleAgent2", "Redirect to meeting");
            VisitOffice("meetings");
            officeTickets_CreateTicketsHelper.WaitForWorkAround(3000);

            executionLog.Log("SaleAgent2", "Search Meeting");
            officeActivities_MeetingHelper.TypeText("SearchSubject", Meeting);
            officeTickets_CreateTicketsHelper.WaitForWorkAround(2000);

            executionLog.Log("SaleAgent2", "Click Edit");
            officeActivities_MeetingHelper.ClickElement("Edit");
            officeTickets_CreateTicketsHelper.WaitForWorkAround(3000);

            executionLog.Log("SaleAgent2", "Click Save");
            officeActivities_MeetingHelper.ClickElement("Save");
            officeTickets_CreateTicketsHelper.WaitForWorkAround(2000);

            executionLog.Log("SaleAgent2", "Wait for success message.");
            officeActivities_MeetingHelper.WaitForText("Meeting updated successfully", 10);

        }
      catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("SaleAgent2");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("SaleAgent2");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("SaleAgent2", "Bug", "Medium", "SaleA gent1", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("SaleAgent2");
                        TakeScreenshot("SaleAgent2");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\Iframe.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("SaleAgent2");
                        string id = loginHelper.getIssueID("SaleAgent2");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\SaleAgent2.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("SaleAgent2"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("SaleAgent2");
                executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("SaleAgent2");
                executionLog.WriteInExcel("SaleAgent2", Status, JIRA, "Sale Agent Activities");
            }
        }
    }
}
