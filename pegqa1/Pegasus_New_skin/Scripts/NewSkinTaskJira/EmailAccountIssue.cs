using System;
using System.IO;
using LinqToExcel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;


namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class EmailAccountIssue : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS4")]
        [TestCategory("NewSkinTaskJira")]
        public void emailAccountIssue()
        {
            string[] username = null;
            string[] password = null;
            string[] log = null;
            String JIRA = "";
            String Status = "Pass";

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");
            log = oXMLData.getData("settings/URL", "logout");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var activitiesEmails_EmailAccountsHelper = new ActivitiesEmails_EmailAccountsHelper(GetWebDriver());

            try
            {
                executionLog.Log("EmailAccountIssue", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("EmailAccountIssue", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("EmailAccountIssue", "Visit create Email account page");
                VisitOffice("emailaccounts/create");

                executionLog.Log("EmailAccountIssue", "Verify title");
                VerifyTitle("Create an E-Mail Account");

                executionLog.Log("EmailAccountIssue", "Click on 'Save' button without entering details");
                activitiesEmails_EmailAccountsHelper.ClickElement("Save");

                executionLog.Log("EmailAccountIssue", "Verify Validation message displayed for email address");
                activitiesEmails_EmailAccountsHelper.verifyElementAvailable("EmailValidationMsg");

                executionLog.Log("EmailAccountIssue", "Log out from the application");
                VisitOffice("logout");

            }    
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("EmailAccountIssue");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Email Account Issue");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Email Account Issue", "Bug", "Medium", "Email page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Email Account Issue");
                        TakeScreenshot("EmailAccountIssue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EmailAccountIssue.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("EmailAccountIssue");
                        string id = loginHelper.getIssueID("Email Account Issue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EmailAccountIssue.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Email Account Issue"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Email Account Issue");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("EmailAccountIssue");
                executionLog.WriteInExcel("Email Account Issue", Status, JIRA, " Activities Email");
            }
        }
    }
}
