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
    public class AdminTicketStatusUrlChange : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS1")]
        [TestCategory("BugTest")]
        public void adminTicketStatusUrlChange()
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
                executionLog.Log("AdminTicketStatusUrlChange", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("AdminTicketStatusUrlChange", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("AdminTicketStatusUrlChange", "Redirect To Admin");
                VisitOffice("admin");
                tickets_MasterDataHelper.WaitForWorkAround(1000);

                executionLog.Log("AdminTicketStatusUrlChange", "Goto Priority page.");
                VisitOffice("tickets/masterdata/priority");

                executionLog.Log("AdminTicketStatusUrlChange", "Click On any priority");
                tickets_MasterDataHelper.ClickElement("ClicKOnIssueResolved");
                tickets_MasterDataHelper.WaitForWorkAround(1000);

                executionLog.Log("AdminTicketStatusUrlChange", "Change the page url.");
                VisitOffice("tickets/masterdata/edit/priority/121");

                executionLog.Log("AdminTicketStatusUrlChange", "Verify Message You don't have privilege.");
                tickets_MasterDataHelper.WaitForText("You don't have privilege.", 20);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("AdminTicketStatusUrlChange");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Admin Ticket Status Url Change");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Admin Ticket Status Url Change", "Bug", "Medium", "Admin tickets", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Admin Ticket Status Url Change");
                        TakeScreenshot("AdminTicketStatusUrlChange");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AdminTicketStatusUrlChange.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("AdminTicketStatusUrlChange");
                        string id = loginHelper.getIssueID("Admin Ticket Status Url Change");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AdminTicketStatusUrlChange.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Admin Ticket Status Url Change"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Admin Ticket Status Url Change");
                //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("AdminTicketStatusUrlChange");
                executionLog.WriteInExcel("Admin Ticket Status Url Change", Status, JIRA, "Admin tickets");
            }
        }
    }
}
