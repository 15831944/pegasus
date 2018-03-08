using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class AdminTicketEmailTemplateURLChange : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Url")]
        [TestCategory("TS3")]
        [TestCategory("ChangeUrl")]
        public void adminTicketEmailTemplateURLChange()
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
            var tickets_EmailTemplateHelper = new Tickets_EmailTemplateHelper(GetWebDriver());


            // Variable
            var FirstName = "Test" + GetRandomNumber();
            var LastName = "Tester" + GetRandomNumber();
            var Number = "12345678" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("AdminTicketEmailTemplateURLChange", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("AdminTicketEmailTemplateURLChange", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("AdminTicketEmailTemplateURLChange", "Goto User  Ticket >> Email Template");
                VisitOffice("tickets/email_templates");

                executionLog.Log("AdminTicketEmailTemplateURLChange", "Click On any Ticket >>  Email Template");
                tickets_EmailTemplateHelper.ClickElement("ClickOnTicketEmailTemplate");
                tickets_EmailTemplateHelper.WaitForWorkAround(1000);

                executionLog.Log("AdminTicketEmailTemplateURLChange", "Change the url with the url number of another office");
                VisitOffice("tickets/email_templates/view/988");
                tickets_EmailTemplateHelper.WaitForWorkAround(1000);

                executionLog.Log("AdminTicketEmailTemplateURLChange", "Verify Validation");
                tickets_EmailTemplateHelper.WaitForText("You don't have privilege.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("AdminTicketEmailTemplateURLChange");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Admin Ticket Email Template URL Change");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Admin Ticket Email Template URL Change", "Bug", "Medium", "Admin ticket", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Admin Ticket Email Template URL Change");
                        TakeScreenshot("AdminTicketEmailTemplateURLChange");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AdminTicketEmailTemplateURLChange.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("AdminTicketEmailTemplateURLChange");
                        string id = loginHelper.getIssueID("Admin Ticket Email Template URL Change");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AdminTicketEmailTemplateURLChange.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Admin Ticket Email Template URL Change"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Admin Ticket Email Template URL Change");
               // executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("AdminTicketEmailTemplateURLChange");
                executionLog.WriteInExcel("Admin Ticket Email Template URL Change", Status, JIRA, "Admin Tickets");
            }
        }
    }
}
