using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Admin
{
    [TestClass]
    public class EmailNotification : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin")]
        [TestCategory("TS6")]
        [TestCategory("Pegasus_Admin")]
        public void emailNotification()
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
            var tickets_EmailNotificationHelper = new Tickets_EmailNotificationHelper(GetWebDriver());

            // Variable
            var name = "Test" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("EmailNotification", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("EmailNotification", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("EmailNotification", "Click On  Admin");
                VisitOffice("admin");

                executionLog.Log("EmailNotification", "Redirect To Alert page");
                VisitOffice("tickets/settings/alerts");

                executionLog.Log("EmailNotification", "verify title");
                VerifyTitle("E-Mail Notifications");

                executionLog.Log("EmailNotification", "Click on When A Ticket Created");
                tickets_EmailNotificationHelper.ClickElement("WhenATicketIsCreated");

                executionLog.Log("EmailNotification", "Click on Created Assigned To");
                tickets_EmailNotificationHelper.ClickElement("CreatedAssignedTo");

                executionLog.Log("EmailNotification", "Click on Created Assigned Depatment");
                tickets_EmailNotificationHelper.ClickElement("CreatedAssignedDepatment");

                executionLog.Log("EmailNotification", "Enter Email ");
                tickets_EmailNotificationHelper.TypeText("EmailCreated", "test@yopmail.com");

                executionLog.Log("EmailNotification", "Click on When Ticket Update");
                tickets_EmailNotificationHelper.ClickElement("WhenTicketUpdate");

                executionLog.Log("EmailNotification", "Click on Assigned To Update");
                tickets_EmailNotificationHelper.ClickElement("AssignedToUpdate");

                executionLog.Log("EmailNotification", "Click on Assigned Manager Update");
                tickets_EmailNotificationHelper.ClickElement("AssignedManagerUpdate");

                executionLog.Log("EmailNotification", "Enter Email ");
                tickets_EmailNotificationHelper.TypeText("EmailUpdate", "test@yopmail.com");

                executionLog.Log("EmailNotification", "Click on When Comment Posted");
                tickets_EmailNotificationHelper.ClickElement("WhenCommentPosted");

                executionLog.Log("EmailNotification", "Click on Assigned To Posted");
                tickets_EmailNotificationHelper.ClickElement("AssignedToPosted");

                executionLog.Log("EmailNotification", "Click on Assigned Department Posted");
                tickets_EmailNotificationHelper.ClickElement("AssignedDepartmentPosted");

                executionLog.Log("EmailNotification", "Enter Email ");
                tickets_EmailNotificationHelper.TypeText("EmailPosetd", "test@yopmail.com");

                executionLog.Log("EmailNotification", "Click on Save");
                tickets_EmailNotificationHelper.ClickElement("Save");

                executionLog.Log("EmailNotification", "wait for text");
                tickets_EmailNotificationHelper.WaitForText("Alerts Saved Successfully.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("EmailNotification");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Email Notification");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Email Notification", "Bug", "Medium", "Alert page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Email Notification");
                        TakeScreenshot("EmailNotification");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EmailNotification.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("EmailNotification");
                        string id = loginHelper.getIssueID("Email Notification");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EmailNotification.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Email Notification"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Email Notification");
           //     executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("EmailNotification");
                executionLog.WriteInExcel("Email Notification", Status, JIRA, "Admin Tickets");
            }
        }
    }
}