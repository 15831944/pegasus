using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class DeleteTicket : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS4")]
        [TestCategory("NewSkinTaskJira")]
        public void deleteTicket()
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
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("DeleteTicket", "Login with valid username and password");
                Login(username[0], password[0]);

                executionLog.Log("DeleteTicket", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("DeleteTicket", "Go to Ticket page");
                VisitOffice("tickets");

                executionLog.Log("DeleteTicket", "Verify title");
                VerifyTitle("Tickets");

                executionLog.Log("DeleteTicket", "Select first ticket");
                officeTickets_AllTicketsHelper.ClickElement("FirstCheckBox");

                executionLog.Log("DeleteTicket", "Click on Delete button");
                officeTickets_AllTicketsHelper.ClickElement("DelTick");

                executionLog.Log("DeleteTicket", "Accept alert");
                officeTickets_AllTicketsHelper.AcceptAlert();

                executionLog.Log("DeleteTicket", "Verify success message");
                officeTickets_AllTicketsHelper.WaitForText("0 Records deleted successfully and 1 don't have permissions", 30);

                executionLog.Log("DeleteTicket", "Verify No error message displayed");
                officeTickets_AllTicketsHelper.VerifyTextNotPresent("An Internal Error Occurred");

                VisitOffice("logout");
            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("DeleteTicket");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Delete Ticket");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Delete Ticket", "Bug", "Medium", "Ticket page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Delete Ticket");
                        TakeScreenshot("DeleteTicket");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\DeleteTicket.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("DeleteTicket");
                        string id = loginHelper.getIssueID("Delete Ticket");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\DeleteTicket.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Delete Ticket"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Delete Ticket");
             //   executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("DeleteTicket");
                executionLog.WriteInExcel("Delete Ticket", Status, JIRA, "Office Ticket");
            }
        }
    }
}