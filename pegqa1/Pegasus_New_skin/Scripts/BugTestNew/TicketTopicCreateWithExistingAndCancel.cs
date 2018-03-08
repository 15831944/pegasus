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
    public class TicketTopicCreateWithExistingAndCancel : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("Fail")]
        [TestCategory("TS3")]
        [TestCategory("BugTestNew")]
        public void ticketTopicCreateWithExistingAndCancel()
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

            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("TicketTopicCreateWithExistingAndCancel", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("TicketTopicCreateWithExistingAndCancel", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("TicketTopicCreateWithExistingAndCancel", "Redirect at ticket topic page.");
                VisitOffice("tickets/masterdata/topic");

                executionLog.Log("TicketTopicCreateWithExistingAndCancel", "Click on create button");
                tickets_MasterDataHelper.ClickElement("Create");

                executionLog.Log("TicketTopicCreateWithExistingAndCancel", "Enter topic name");
                tickets_MasterDataHelper.TypeText("Name", "Test");

                executionLog.Log("TicketTopicCreateWithExistingAndCancel", "Click on Save button.");
                tickets_MasterDataHelper.ClickElement("Save");

                executionLog.Log("TicketTopicCreateWithExistingAndCancel", "Wait for locator to be present.");
                tickets_MasterDataHelper.WaitForElementPresent("TicketAlready_Validation", 10);

                executionLog.Log("TicketTopicCreateWithExistingAndCancel", "Verify validation for existing displayed.");
                tickets_MasterDataHelper.VerifyText("TicketAlready_Validation", "Name Already Exists");

                executionLog.Log("TicketTopicCreateWithExistingAndCancel", "Click on Cancel button.");
                tickets_MasterDataHelper.ClickElement("Cancel");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("TicketTopicCreateWithExistingAndCancel");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Ticket Topic Create With Existing And Cancel");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Ticket Topic Create With Existing And Cancel", "Bug", "Medium", "Ticket Admin page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Ticket Topic Create With Existing And Cancel");
                        TakeScreenshot("TicketTopicCreateWithExistingAndCancel");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\TicketTopicCreateWithExistingAndCancel.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("TicketTopicCreateWithExistingAndCancel");
                        string id = loginHelper.getIssueID("Ticket Topic Create With Existing And Cancel");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\TicketTopicCreateWithExistingAndCancel.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Ticket Topic Create With Existing And Cancel"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Ticket Topic Create With Existing And Cancel");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("TicketTopicCreateWithExistingAndCancel");
                executionLog.WriteInExcel("Ticket Topic Create With Existing And Cancel", Status, JIRA, "Admin Tickets");
            }
        }
    }
}