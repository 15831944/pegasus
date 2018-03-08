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
    public class AdminTicketEmailTemplateChangeURL : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS1")]
        [TestCategory("BugTest")]
        public void adminTicketEmailTemplateChangeURL()
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
            var tickets_EmailTemplateHelper = new Tickets_EmailTemplateHelper(GetWebDriver());
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("AdimTicketEmailTemplateChangeURL", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("AdimTicketEmailTemplateChangeURL", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("AdimTicketEmailTemplateChangeURL", "Redirect To Admin");
                VisitOffice("admin");
                tickets_EmailTemplateHelper.WaitForWorkAround(1000);

                executionLog.Log("AdimTicketEmailTemplateChangeURL", "Goto Ingration >> IFrame");
                VisitOffice("tickets/email_templates");

                executionLog.Log("AdimTicketEmailTemplateChangeURL", "Click On any Email Template");
                tickets_EmailTemplateHelper.ClickElement("ClickOnTicketEmailTemplate");
                tickets_EmailTemplateHelper.WaitForWorkAround(3000);

                executionLog.Log("AdimTicketEmailTemplateChangeURL", "Change Url of the page.");
                VisitOffice("tickets/email_templates/view/1012");

                executionLog.Log("AdimTicketEmailTemplateChangeURL", "Verify Message You don't have privilege");
                tickets_EmailTemplateHelper.WaitForText("You don't have privilege.", 20);


            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("AdimTicketEmailTemplateChangeURL");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Adim Ticket Email Template Change URL");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Adim Ticket Email Template Change URL", "Bug", "Medium", "Admin tickets", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Adim Ticket Email Template Change URL");
                        TakeScreenshot("AdimTicketEmailTemplateChangeURL");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AdimTicketEmailTemplateChangeURL.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("AdimTicketEmailTemplateChangeURL");
                        string id = loginHelper.getIssueID("Adim Ticket Email Template Change URL");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AdimTicketEmailTemplateChangeURL.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Adim Ticket Email Template Change URL"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Adim Ticket Email Template Change URL");
                //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("AdimTicketEmailTemplateChangeURL");
                executionLog.WriteInExcel("Adim Ticket Email Template Change URL", Status, JIRA, "Admin tickets");
            }
        }
    }
}