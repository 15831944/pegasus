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
    public class LabelDoubleClickToAddTopicTicket : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS2")]
        [TestCategory("BugTestNew")]
        public void labelDoubleClickToAddTopicTicket()
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
            var officeTickets_AllTicketsHelper = new OfficeTickets_AllTicketsHelper(GetWebDriver());

            // Random Variables
            String JIRA = "";
            String Status = "Pass";
            var Name = "Ticket" + RandomNumber(1, 500);


            try
            {
                executionLog.Log("LabelDoubleClickToAddTopicTicket", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("LabelDoubleClickToAddTopicTicket", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("LabelDoubleClickToAddTopicTicket", "Redirect at opportunities task");
                VisitOffice("tickets");
                officeTickets_AllTicketsHelper.WaitForWorkAround(5000);

                executionLog.Log("LabelDoubleClickToAddTopicTicket", "create a ticket");
                officeTickets_AllTicketsHelper.ClickElement("CreateBtn");
                officeTickets_AllTicketsHelper.WaitForWorkAround(2000);

                executionLog.Log("LabelDoubleClickToAddTopicTicket", "Enter the ticket name");
                officeTickets_AllTicketsHelper.TypeText("Subject", Name);

                executionLog.Log("LabelDoubleClickToAddTopicTicket", "Select a Client");
                officeTickets_AllTicketsHelper.ClickElement("SelectClientBtn");
                officeTickets_AllTicketsHelper.WaitForWorkAround(4000);

                executionLog.Log("LabelDoubleClickToAddTopicTicket", "Select the Company");
                officeTickets_AllTicketsHelper.ClickElement("FirstCompany");
                officeTickets_AllTicketsHelper.WaitForWorkAround(4000);

                executionLog.Log("LabelDoubleClickToAddTopicTicket", "Click on 'Save' button");
                officeTickets_AllTicketsHelper.ClickElement("SaveBtn");
                officeTickets_AllTicketsHelper.WaitForWorkAround(7000);

                executionLog.Log("LabelDoubleClickToAddTopicTicket", "Verify label for ticket displayed.");
                officeTickets_AllTicketsHelper.VerifyText("TOpicVerify", "Double click to add");

                VisitOffice("tickets");
                officeTickets_AllTicketsHelper.WaitForWorkAround(4000);

                executionLog.Log("LabelDoubleClickToAddTopicTicket", "Search the ticket");
                officeTickets_AllTicketsHelper.TypeText("SearchTicket", Name);
                officeTickets_AllTicketsHelper.WaitForWorkAround(4000);

                executionLog.Log("LabelDoubleClickToAddTopicTicket", "Click on check box");
                officeTickets_AllTicketsHelper.ClickElement("TicketInputIcon");
                officeTickets_AllTicketsHelper.WaitForWorkAround(2000);

                executionLog.Log("LabelDoubleClickToAddTopicTicket", "Click on delete button");
                officeTickets_AllTicketsHelper.ClickElement("DeleteIcon");
                officeTickets_AllTicketsHelper.WaitForWorkAround(2000);
                officeTickets_AllTicketsHelper.AcceptAlert();
            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("LabelDoubleClickToAddTopicTicket");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Label Double Click To Add Topic");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Label Double Click To Add Topic", "Bug", "Medium", "Tickets page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Label Double Click To Add Topic");
                        TakeScreenshot("LabelDoubleClickToAddTopicTicket");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\LabelDoubleClickToAddTopicTicket.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("LabelDoubleClickToAddTopicTicket");
                        string id = loginHelper.getIssueID("Label Double Click To Add Topic");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\LabelDoubleClickToAddTopicTicket.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Label Double Click To Add Topic"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Label Double Click To Add Topic");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("LabelDoubleClickToAddTopicTicket");
                executionLog.WriteInExcel("Label Double Click To Add Topic", Status, JIRA, "Office Tickets");
            }
        }
    }
}