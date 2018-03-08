using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class ForwardEmailFromSent : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS1")]
        [TestCategory("BugTest")]
        public void forwardEmailFromSent()
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
            var OfficeActivities_EmailHelper = new OfficeActivities_EmailsHelper(GetWebDriver());
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("ForwardEmailFromSent", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("ForwardEmailFromSent", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("ForwardEmailFromSent", "Redirect to URL");
                VisitOffice("mails/compose");

                executionLog.Log("ForwardEmailFromSent", "Enter details in to field.");
                OfficeActivities_EmailHelper.TypeText("To", "Test@yopmail.com");

                executionLog.Log("ForwardEmailFromSent", "Enter Subject ");
                OfficeActivities_EmailHelper.TypeText("EmailName", "This is Subject");

                executionLog.Log("ForwardEmailFromSent", "Click Send Button");
                OfficeActivities_EmailHelper.ClickElement("Send");

                executionLog.Log("ForwardEmailFromSent", "Verify Email sent successfully");
                OfficeActivities_EmailHelper.WaitForText("E-Mail Sent Successfully.", 10);

                executionLog.Log("ForwardEmailFromSent", "Redirect Mails");
                VisitOffice("mails/sent");
                OfficeActivities_EmailHelper.WaitForWorkAround(1000);

                executionLog.Log("ForwardEmailFromSent", "Click On Sent To Forward");
                OfficeActivities_EmailHelper.ClickElement("ClickAnyEmail");
                OfficeActivities_EmailHelper.WaitForWorkAround(1000);

                executionLog.Log("ForwardEmailFromSent", "Click on Forward");
                OfficeActivities_EmailHelper.ClickElement("ClickOnForward");
                OfficeActivities_EmailHelper.WaitForWorkAround(4000);

                executionLog.Log("ForwardEmailFromSent", "Enter Email To Search ");
                OfficeActivities_EmailHelper.TypeText("EnterToEmailRI", "Test@yopmail.com");

                executionLog.Log("ForwardEmailFromSent", "Click on Send Button");
                OfficeActivities_EmailHelper.ClickElement("Send");
                OfficeActivities_EmailHelper.WaitForWorkAround(3000);

                executionLog.Log("ForwardEmailFromSent", "Wait for success message.");
                OfficeActivities_EmailHelper.WaitForText("E-Mail Sent Successfully.", 10);
                OfficeActivities_EmailHelper.WaitForWorkAround(2000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("ForwardEmailFromSent");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Forward Email From Sent");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Forward Email From Sent", "Bug", "Medium", "Email page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Forward Email From Sent");
                        TakeScreenshot("ForwardEmailFromSent");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ForwardEmailFromSent.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("ForwardEmailFromSent");
                        string id = loginHelper.getIssueID("Forward Email From Sent");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ForwardEmailFromSent.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Forward Email From Sent"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Forward Email From Sent");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("ForwardEmailFromSent");
                executionLog.WriteInExcel("Forward Email From Sent", Status, JIRA, "Office Activities");
            }
        }
    }
}