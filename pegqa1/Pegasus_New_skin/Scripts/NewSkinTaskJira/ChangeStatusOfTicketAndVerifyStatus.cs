using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class ChangeStatusOfTicketAndVerifyStatus : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS4")]
        [TestCategory("NewSkinTaskJira")]
        public void changeStatusOfTicketAndVerifyStatus()
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
            var officeTickets_CreateTicketsHelper = new OfficeTickets_CreateTicketsHelper(GetWebDriver());


            // Variable random
            var name = "Ticket" + RandomNumber(1, 999);
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("ChangeStatusOfTicketAndVerifyStatus", "Login with valid username and password");
                Login(username[0], password[0]);

                executionLog.Log("ChangeStatusOfTicketAndVerifyStatus", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("ChangeStatusOfTicketAndVerifyStatus", "Click on ticket tab.");
                officeTickets_CreateTicketsHelper.ClickJs("TicketTab");
                officeTickets_CreateTicketsHelper.WaitForWorkAround(3000);

                executionLog.Log("ChangeStatusOfTicketAndVerifyStatus", "Click On Create");
                officeTickets_CreateTicketsHelper.ClickElement("Create");
                officeTickets_CreateTicketsHelper.WaitForWorkAround(3000);

                executionLog.Log("ChangeStatusOfTicketAndVerifyStatus", "ClickOnCreate");
                officeTickets_CreateTicketsHelper.TypeText("TicketName", name);

                executionLog.Log("ChangeStatusOfTicketAndVerifyStatus", "Click on Client icon");
                officeTickets_CreateTicketsHelper.ClickElement("ClientDisplayIcon");
                officeTickets_CreateTicketsHelper.WaitForWorkAround(3000);

                executionLog.Log("ChangeStatusOfTicketAndVerifyStatus", "ClickOnClient");
                officeTickets_CreateTicketsHelper.ClickElement("Client");
                officeTickets_CreateTicketsHelper.WaitForWorkAround(2000);

                executionLog.Log("ChangeStatusOfTicketAndVerifyStatus", "Select the Status");
                officeTickets_CreateTicketsHelper.SelectByText("TickStatus", "New");

                executionLog.Log("ChangeStatusOfTicketAndVerifyStatus", "Click on Save");
                officeTickets_CreateTicketsHelper.ClickJs("Save");

                executionLog.Log("ChangeStatusOfTicketAndVerifyStatus", "Verify result");
                officeTickets_CreateTicketsHelper.WaitForText("Ticket Created Successfully.", 10);

                executionLog.Log("ChangeStatusOfTicketAndVerifyStatus", "Redirect To Document");
                VisitOffice("tickets");
                officeTickets_CreateTicketsHelper.WaitForWorkAround(3000);

                executionLog.Log("ChangeStatusOfTicketAndVerifyStatus", "Search ticket by name. ");
                officeTickets_CreateTicketsHelper.TypeText("SearchTicket", name);
                officeTickets_CreateTicketsHelper.WaitForWorkAround(3000);

                executionLog.Log("ChangeStatusOfTicketAndVerifyStatus", "Select All in Assigned to field");
                officeTickets_CreateTicketsHelper.SelectByText("AssignedField", "All");
                officeTickets_CreateTicketsHelper.WaitForWorkAround(2000);

                executionLog.Log("ChangeStatusOfTicketAndVerifyStatus", "Click on ticket to open it.");
                officeTickets_CreateTicketsHelper.ClickElement("ClickOnTicket");
                officeTickets_CreateTicketsHelper.WaitForWorkAround(3000);

                executionLog.Log("ChangeStatusOfTicketAndVerifyStatus", "Click on resolved");
                officeTickets_CreateTicketsHelper.ClickElement("ResolvedBtn");
                officeTickets_CreateTicketsHelper.WaitForWorkAround(2000);

                executionLog.Log("ChangeStatusOfTicketAndVerifyStatus", "Select ticket resolved status.");
                officeTickets_CreateTicketsHelper.SelectByText("SelectTicketResolution", "Resolved");

                executionLog.Log("ChangeStatusOfTicketAndVerifyStatus", "Click Resolved SaveBtn");
                officeTickets_CreateTicketsHelper.ClickElement("ClickResolvedSaveBtn");

                executionLog.Log("ChangeStatusOfTicketAndVerifyStatus", "Ticket Resolved Successfully");
                officeTickets_CreateTicketsHelper.WaitForText("Ticket Resolved Successfully.", 10);
                officeTickets_CreateTicketsHelper.WaitForWorkAround(3000);

                executionLog.Log("ChangeStatusOfTicketAndVerifyStatus", "Search ticket by name ");
                officeTickets_CreateTicketsHelper.TypeText("SearchTicket", name);
                officeTickets_CreateTicketsHelper.WaitForWorkAround(2000);

                executionLog.Log("ChangeStatusOfTicketAndVerifyStatus", "Select All in Assigned to field");
                officeTickets_CreateTicketsHelper.SelectByText("AssignedField", "All");
                officeTickets_CreateTicketsHelper.WaitForWorkAround(3000);

                executionLog.Log("ChangeStatusOfTicketAndVerifyStatus", "Verify Status");
                officeTickets_CreateTicketsHelper.VerifyPageText("Resolved");

                executionLog.Log("ChangeStatusOfTicketAndVerifyStatus", "Select ticket");
                officeTickets_CreateTicketsHelper.ClickElement("Ticket1");
                officeTickets_CreateTicketsHelper.WaitForWorkAround(3000);

                executionLog.Log("ChangeStatusOfTicketAndVerifyStatus", "Click On Closed ");
                officeTickets_CreateTicketsHelper.ClickElement("ClosedTicket");
                officeTickets_CreateTicketsHelper.WaitForWorkAround(3000);

                executionLog.Log("ChangeStatusOfTicketAndVerifyStatus", "Verify Ticket Closed Successfully.");
                officeTickets_CreateTicketsHelper.WaitForText("Ticket Closed Successfully.", 10);

                executionLog.Log("ChangeStatusOfTicketAndVerifyStatus", "Search ticket ");
                officeTickets_CreateTicketsHelper.TypeText("SearchTicket", name);
                officeTickets_CreateTicketsHelper.WaitForWorkAround(3000);

                executionLog.Log("ChangeStatusOfTicketAndVerifyStatus", "Select All in Assigned to field");
                officeTickets_CreateTicketsHelper.SelectByText("AssignedField", "All");
                officeTickets_CreateTicketsHelper.WaitForWorkAround(3000);

                executionLog.Log("ChangeStatusOfTicketAndVerifyStatus", "Verify Status closed");
                officeTickets_CreateTicketsHelper.VerifyPageText("Closed");


            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("ChangeStatusOfTicketAndVerifyStatus");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Change Status Of Ticket And Verify Status");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Change Status Of Ticket And Verify Status", "Bug", "Medium", "Ticket page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Change Status Of Ticket And Verify Status");
                        TakeScreenshot("ChangeStatusOfTicketAndVerifyStatus");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ChangeStatusOfTicketAndVerifyStatus.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("ChangeStatusOfTicketAndVerifyStatus");
                        string id = loginHelper.getIssueID("Change Status Of Ticket And Verify Status");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ChangeStatusOfTicketAndVerifyStatus.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Change Status Of Ticket And Verify Status"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Change Status Of Ticket And Verify Status");
           //     executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("ChangeStatusOfTicketAndVerifyStatus");
                executionLog.WriteInExcel("Change Status Of Ticket And Verify Status", Status, JIRA, "Ticketing System");
            }
        }
    }
}