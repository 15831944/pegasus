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
    public class EmailVerifyTo : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("Fail")]
        [TestCategory("TS1")]
        [TestCategory("BugTest")]
        public void emailVerifyTo()
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
            var officeActivities_EmailHelper = new OfficeActivities_EmailsHelper(GetWebDriver());
            String JIRA = "";
            String Status = "Pass";
            try
            {

                executionLog.Log("EmailVerifyTo", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("EmailVerifyTo", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("EmailVerifyTo", "Activeities >> Email");
                VisitOffice("mails/sent");
                officeActivities_EmailHelper.WaitForWorkAround(1000);

                executionLog.Log("EmailVerifyTo", "Click On Any Sent Email");
                officeActivities_EmailHelper.ClickElement("ClickonAnySentEmail");
                officeActivities_EmailHelper.WaitForWorkAround(1000);

                executionLog.Log("EmailVerifyTo", "Verify the page text");
                officeActivities_EmailHelper.VerifyPageText("View Mail");
                officeActivities_EmailHelper.WaitForWorkAround(3000);

                executionLog.Log("EmailVerifyTo", "Verify the Email Sent To");
                officeActivities_EmailHelper.VerifyText("EmailSentTo", "To:");
                officeActivities_EmailHelper.WaitForWorkAround(3000);
            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("EmailVerifyTo");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Email Verify To");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Email Verify To", "Bug", "Medium", "Client page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Email Verify To");
                        TakeScreenshot("EmailVerifyTo");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EmailVerifyTo.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("EmailVerifyTo");
                        string id = loginHelper.getIssueID("Email Verify To");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EmailVerifyTo.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Email Verify To"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Email Verify To");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("EmailVerifyTo");
                executionLog.WriteInExcel("Email Verify To", Status, JIRA, "Office Activities");
            }
        }
    }
}