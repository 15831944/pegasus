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
    public class AdminTicketPriorityUrlChange : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS1")]
        [TestCategory("BugTest")]
        public void adminTicketPriorityUrlChange()
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
                executionLog.Log("AdminTicketPriorityUrlChange", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("AdminTicketPriorityUrlChange", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("AdminTicketPriorityUrlChange", "Redirect To Admin");
                VisitOffice("admin");
                tickets_MasterDataHelper.WaitForWorkAround(1000);

                executionLog.Log("AdminTicketPriorityUrlChange", "Goto ticket priority page");
                VisitOffice("tickets/masterdata/priority");

                executionLog.Log("AdminTicketPriorityUrlChange", "Click On Any priority.");
                tickets_MasterDataHelper.ClickElement("ClicKOnIssueResolved");
                tickets_MasterDataHelper.WaitForWorkAround(3000);

                executionLog.Log("AdminTicketPriorityUrlChange", "Change Url of the page.");
                VisitOffice("tickets/masterdata/edit/priority/121");
                tickets_MasterDataHelper.WaitForWorkAround(3000);

                executionLog.Log("AdminTicketPriorityUrlChange", "Verify text You don't have privilege");
                tickets_MasterDataHelper.WaitForText("You don't have privilege.", 05);


            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("AdminTicketPriorityUrlChange");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Admin Ticket Priority Url Change");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Admin Ticket Priority Url Change", "Bug", "Medium", "Admin tickets", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Admin Ticket Priority Url Change");
                        TakeScreenshot("AdminTicketPriorityUrlChange");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AdminTicketPriorityUrlChange.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("AdminTicketPriorityUrlChange");
                        string id = loginHelper.getIssueID("Admin Ticket Priority Url Change");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AdminTicketPriorityUrlChange.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Admin Ticket Priority Url Change"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Admin Ticket Priority Url Change");
                //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("AdminTicketPriorityUrlChange");
                executionLog.WriteInExcel("Admin Ticket Priority Url Change", Status, JIRA, "Admin tickets");
            }
        }
    }
}
