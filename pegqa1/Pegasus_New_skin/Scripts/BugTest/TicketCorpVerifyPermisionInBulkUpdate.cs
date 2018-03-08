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
    public class TicketCorpVerifyPermisionInBulkUpdate : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS1")]
        [TestCategory("BugTest")]
        public void ticketCorpVerifyPermisionInBulkUpdate()
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
            var office_TicketHelper = new OfficeTickets_AllTicketsHelper(GetWebDriver());

            // Variable
            var TicketName = "Test" + RandomNumber(1, 99);
            string Loc = "//a[text()=" + "[']" + TicketName + "[']" + "]";
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("TicketCorpVerifyPermisionInBulkUpdate", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("TicketCorpVerifyPermisionInBulkUpdate", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("TicketCorpVerifyPermisionInBulkUpdate", "Redirect To  Tickets");
                VisitOffice("tickets");

                executionLog.Log("TicketCorpVerifyPermisionInBulkUpdate", "Wait for element to present.");
                office_TicketHelper.WaitForElementPresent("BulkUpdate", 08);

                executionLog.Log("TicketCorpVerifyPermisionInBulkUpdate", "Click on Bulk Update button");
                office_TicketHelper.ClickElement("BulkUpdate");
                office_TicketHelper.WaitForWorkAround(3000);

                executionLog.Log("TicketCorpVerifyPermisionInBulkUpdate", "Verify Permissions");
                office_TicketHelper.VerifyText("ClickOnPermision", "Change Permissions");
                office_TicketHelper.WaitForWorkAround(4000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("TicketCorpVerifyPermisionInBulkUpdate");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue(" TicketCorp Verify Permision In BulkUpdate");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue(" TicketCorp Verify Permision In BulkUpdate", "Bug", "Medium", "Tickets page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID(" TicketCorp Verify Permision In BulkUpdate");
                        TakeScreenshot("TicketCorpVerifyPermisionInBulkUpdate");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\TicketCorpVerifyPermisionInBulkUpdate.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("TicketCorpVerifyPermisionInBulkUpdate");
                        string id = loginHelper.getIssueID(" TicketCorp Verify Permision In BulkUpdate");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\TicketCorpVerifyPermisionInBulkUpdate.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID(" TicketCorp Verify Permision In BulkUpdate"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID(" TicketCorp Verify Permision In BulkUpdate");
               // executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("TicketCorpVerifyPermisionInBulkUpdate");
                executionLog.WriteInExcel(" TicketCorp Verify Permision In BulkUpdate", Status, JIRA, "Office Tickets");
            }
        }
    }
}