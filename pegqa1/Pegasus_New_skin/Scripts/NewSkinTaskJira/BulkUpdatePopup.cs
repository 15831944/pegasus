using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class BulkUpdatePopup : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS4")]
        [TestCategory("NewSkinTaskJira")]
        public void bulkUpdatePopup()
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

            // VARIABLE
            var name = "TestEmployee" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("BulkUpdatePopup", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("BulkUpdatePopup", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("BulkUpdatePopup", "Go to ticket page");
                VisitOffice("tickets");

                executionLog.Log("BulkUpdatePopup", "Verify title");
                VerifyTitle("Tickets");

                executionLog.Log("BulkUpdatePopup", "Select First ticket");
                officeTickets_AllTicketsHelper.ClickElement("Ticket1");

                executionLog.Log("BulkUpdatePopup", "Click on Bulk Update button");
                officeTickets_AllTicketsHelper.ClickElement("BulkUpdate");

                executionLog.Log("BulkUpdatePopup", "Click on Change status");
                officeTickets_AllTicketsHelper.ClickElement("ChangeStatus");
                officeTickets_AllTicketsHelper.WaitForWorkAround(2000);

                executionLog.Log("BulkUpdatePopup", "Click on Update button");
                officeTickets_AllTicketsHelper.ClickElement("UpdateBulk");
                officeTickets_AllTicketsHelper.WaitForWorkAround(2000);

                executionLog.Log("BulkUpdatePopup", "Verify permission popup displayed");
                Assert.IsTrue(officeTickets_AllTicketsHelper.VerifyAlertPresent());
                officeTickets_AllTicketsHelper.WaitForWorkAround(2000);

            }

            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("BulkUpdatePopup");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Bulk Update Popup");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Bulk Update Popup", "Bug", "Medium", "Ticket page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Bulk Update Popup");
                        TakeScreenshot("BulkUpdatePopup");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\BulkUpdatePopup.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("BulkUpdatePopup");
                        string id = loginHelper.getIssueID("Bulk Update Popup");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\BulkUpdatePopup.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Bulk Update Popup"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Bulk Update Popup");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("BulkUpdatePopup");
                executionLog.WriteInExcel("Bulk Update Popup", Status, JIRA, "Office ticket");
            }
        }
    }
}