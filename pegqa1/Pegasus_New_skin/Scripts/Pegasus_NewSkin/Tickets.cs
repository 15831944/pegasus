using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class Tickets : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin")]
        [TestCategory("TS8")]
        [TestCategory("Pegasus_NewSkin")]
        public void tickets()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var officeTickets_CreateTicketsHelper = new OfficeTickets_CreateTicketsHelper(GetWebDriver());

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Variable
            var FName = "Test" + RandomNumber(99, 99999);
            var LName = "Test" + RandomNumber(99, 99999);
            var CDBA = "New" + RandomNumber(99, 99999);
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("Tickets", "Login with valid credential  Username");
                Login(username[0], password[0]);

                executionLog.Log("Tickets", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("Tickets", "Vist Tickets page.");
                VisitOffice("tickets");

                executionLog.Log("Tickets", "Verify Heading Tickets");
                officeTickets_CreateTicketsHelper.VerifyText("ViewHeadingTicket", "Tickets");

                executionLog.Log("Tickets", "Goto Create Tickets");
                VisitOffice("tickets/create");

                executionLog.Log("Tickets", "Enter Tickets Subjects");
                officeTickets_CreateTicketsHelper.TypeText("TicketName", "Test Ticket");

                executionLog.Log("Tickets", "Click Assigned icon");
                officeTickets_CreateTicketsHelper.ClickElement("ClientDisplayIcon");
                officeTickets_CreateTicketsHelper.WaitForWorkAround(3000);

                executionLog.Log("Tickets", "Click on Tickets");
                officeTickets_CreateTicketsHelper.ClickElement("Client");
                officeTickets_CreateTicketsHelper.WaitForWorkAround(3000);

                executionLog.Log("Tickets", "Save");
                officeTickets_CreateTicketsHelper.ClickElement("Save");

                executionLog.Log("Tickets", "Ticket created successfully");
                officeTickets_CreateTicketsHelper.WaitForText("Ticket Created Successfully.", 10);

                executionLog.Log("Tickets", "Click on edit icon.");
                officeTickets_CreateTicketsHelper.ClickElement("ClickEditTicketBtn");
                officeTickets_CreateTicketsHelper.WaitForWorkAround(3000);

                executionLog.Log("Tickets", "Save");
                officeTickets_CreateTicketsHelper.ClickElement("Save");
                officeTickets_CreateTicketsHelper.WaitForWorkAround(3000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("Tickets");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Tickets");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Tickets", "Bug", "Medium", "Tickets page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Tickets");
                        TakeScreenshot("Tickets");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\Tickets.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("Tickets");
                        string id = loginHelper.getIssueID("Tickets");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\Tickets.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Tickets"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Tickets");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("Tickets");
                executionLog.WriteInExcel("Tickets", Status, JIRA, "Office Tickets");
            }
        }
    }
}