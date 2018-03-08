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
    public class TicketTopicHeadingChangedToCreateIssue : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("Fail")]
        [TestCategory("TS3")]
        [TestCategory("BugTestNew")]
        public void ticketTopicHeadingChangedToCreateIssue()
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
            var tickets_MasterDataHelper = new Tickets_MasterDataHelper(GetWebDriver());

            // Random Variables
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("TicketTopicHeadingChangedToCreateIssue", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("TicketTopicHeadingChangedToCreateIssue", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("TicketTopicHeadingChangedToCreateIssue", "Redirect at ticket topic page.");
                VisitOffice("tickets/masterdata/topic");

                var loc = "//ul[@id='sortable']/li[1]/div[2]/a[text()='Topic98']";

                if (tickets_MasterDataHelper.IsElementPresent(loc))
                {

                    executionLog.Log("TicketTopicHeadingChangedToCreateIssue", "Click on any ticket topic.");
                    tickets_MasterDataHelper.ClickElement("OpenTicket2");
                    tickets_MasterDataHelper.WaitForWorkAround(2000);

                    executionLog.Log("TicketTopicHeadingChangedToCreateIssue", "Enter topic name.");
                    tickets_MasterDataHelper.TypeText("Name", "Topic98");

                    executionLog.Log("TicketTopicHeadingChangedToCreateIssue", "Click on save button.");
                    tickets_MasterDataHelper.ClickElement("Save");

                    executionLog.Log("TicketTopicHeadingChangedToCreateIssue", "Verify text for exisiting topic.");
                    tickets_MasterDataHelper.VerifyText("TicketAlready_Validation", "Name Already Exists");

                    executionLog.Log("TicketTopicHeadingChangedToCreateIssue", "Verify text on page.");
                    tickets_MasterDataHelper.VerifyPageText("Edit");
                    tickets_MasterDataHelper.WaitForWorkAround(2000);
                }

                else
                {

                    executionLog.Log("TicketTopicHeadingChangedToCreateIssue", "Click on any ticket topic.");
                    tickets_MasterDataHelper.ClickElement("OpenTicket");
                    tickets_MasterDataHelper.WaitForWorkAround(2000);

                    executionLog.Log("TicketTopicHeadingChangedToCreateIssue", "Enter topic name.");
                    tickets_MasterDataHelper.TypeText("Name", "Topic98");

                    executionLog.Log("TicketTopicHeadingChangedToCreateIssue", "Click on save button.");
                    tickets_MasterDataHelper.ClickElement("Save");

                    executionLog.Log("TicketTopicHeadingChangedToCreateIssue", "Verify text for exisiting topic.");
                    tickets_MasterDataHelper.VerifyText("TicketAlready_Validation", "Name Already Exists");

                    executionLog.Log("TicketTopicHeadingChangedToCreateIssue", "Verify text on page.");
                    tickets_MasterDataHelper.VerifyPageText("Edit");
                    tickets_MasterDataHelper.WaitForWorkAround(2000);

                }
            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("TicketTopicHeadingChangedToCreateIssue");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Ticket Topic Heading Changed To Create Issue");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Ticket Topic Heading Changed To Create Issue", "Bug", "Medium", "Ticket Topic page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Ticket Topic Heading Changed To Create Issue");
                        TakeScreenshot("TicketTopicHeadingChangedToCreateIssue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\TicketTopicHeadingChangedToCreateIssue.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("TicketTopicHeadingChangedToCreateIssue");
                        string id = loginHelper.getIssueID("Ticket Topic Heading Changed To Create Issue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\TicketTopicHeadingChangedToCreateIssue.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Ticket Topic Heading Changed To Create Issue"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Ticket Topic Heading Changed To Create Issue");
           //     executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("TicketTopicHeadingChangedToCreateIssue");
                executionLog.WriteInExcel("Ticket Topic Heading Changed To Create Issue", Status, JIRA, "Admin Tickets");
            }
        }
    }
}
