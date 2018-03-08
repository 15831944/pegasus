using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class VerifyTicketAutoAssignmentSettings : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("BugTestNew")]
        public void verifyTicketAutoAssignmentSettings()
        {
            string[] username = null;
            string[] password = null;

            XMLParse oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var ticket_SettingsHelper = new Ticket_SettingsHelper(GetWebDriver());
            var officeTickets_CreateTicketsHelper = new OfficeTickets_CreateTicketsHelper(GetWebDriver());

            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("VerifyTicketAutoAssignmentSettings", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyTicketAutoAssignmentSettings", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyTicketAutoAssignmentSettings", "Redirect at ticket topic page.");
                VisitOffice("tickets/settings");
                ticket_SettingsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTicketAutoAssignmentSettings", "Select category under Auto Assignment");
                ticket_SettingsHelper.ClickElement("TickDefCategory");
                ticket_SettingsHelper.Click("//*[@id='assign0Category']/option[2]");

                executionLog.Log("VerifyTicketAutoAssignmentSettings", "Select set as Department");
                ticket_SettingsHelper.ClickElement("SetDept");
                ticket_SettingsHelper.Click("//*[@id='assign0Department']/option[2]");

                executionLog.Log("VerifyTicketAutoAssignmentSettings", "Select set as Priority");
                ticket_SettingsHelper.ClickElement("SetPriority");
                ticket_SettingsHelper.Click("//*[@id='assign0Priority']/option[2]");

                executionLog.Log("VerifyTicketAutoAssignmentSettings", "Select set as Assigned To");
                ticket_SettingsHelper.ClickElement("SetAssignto");
                ticket_SettingsHelper.Click("//*[@id='assign0Owner']/option[2]");

                executionLog.Log("VerifyTicketAutoAssignmentSettings", "Select set as Assigned Manager");
                ticket_SettingsHelper.ClickElement("SetAssignMgr");
                ticket_SettingsHelper.Click("//*[@id='assign0Manager']/option[3]");

                executionLog.Log("VerifyTicketAutoAssignmentSettings", "Click on Save button.");
                ticket_SettingsHelper.ClickElement("SaveBtn");
                ticket_SettingsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyTicketAutoAssignmentSettings", "Redirect to Create Ticket page");
                VisitOffice("tickets/create");
                officeTickets_CreateTicketsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTicketAutoAssignmentSettings", "Verify Assignments are not selected");
                officeTickets_CreateTicketsHelper.VerifySlctdOptn("TickPriority", "Select Priority");
                officeTickets_CreateTicketsHelper.VerifySlctdOptn("TickDepartment", "Select Department");
                officeTickets_CreateTicketsHelper.VerifySlctdOptn("Assignedto", "Select Assigned To");
                officeTickets_CreateTicketsHelper.VerifySlctdOptn("AssignedManager", "Select Assigned Manager");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyTicketAutoAssignmentSettings");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Ticket Auto Assignment Settings");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Ticket Auto Assignment Settings", "Bug", "Medium", "Ticket Admin page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Ticket Auto Assignment Settings");
                        TakeScreenshot("VerifyTicketAutoAssignmentSettings");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyTicketAutoAssignmentSettings.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyTicketAutoAssignmentSettings");
                        string id = loginHelper.getIssueID("Verify Ticket Auto Assignment Settings");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyTicketAutoAssignmentSettings.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Ticket Auto Assignment Settings"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Ticket Auto Assignment Settings");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyTicketAutoAssignmentSettings");
                executionLog.WriteInExcel("Verify Ticket Auto Assignment Settings", Status, JIRA, "Admin Ticket settings");
            }
        }
    }
}