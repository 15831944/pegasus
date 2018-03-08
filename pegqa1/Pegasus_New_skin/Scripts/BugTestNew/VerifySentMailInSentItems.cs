using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class VerifySentMailInSentItems : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("BugTestNew")]
        public void verifySentMailInSentItems()
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
            var officeActivities_EmailsHelper = new OfficeActivities_EmailsHelper(GetWebDriver());


            // Variable
            var to_email = "Automation_sent" + GetRandomNumber() + "@yopmail.com";
            var subject = "Automation_subject" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("VerifySentMailInSentItems", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifySentMailInSentItems", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifySentMailInSentItems", "Navigate to Compose Email page");
                VisitOffice("mails/compose");
                officeActivities_EmailsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifySentMailInSentItems", "Select From Email");
                officeActivities_EmailsHelper.SelectByText("From", "Howard Tang (pegasustesthoward@yopmail.com)");
                //officeActivities_EmailsHelper.ClickElement("From");
                //officeActivities_EmailsHelper.ClickElement("//select[@id='EmailEmailAccounts']/optgroup/option[contains(text(),'Howard Tang')]");
                officeActivities_EmailsHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifySentMailInSentItems", "Enter To email");
                officeActivities_EmailsHelper.TypeText("To", to_email);

                executionLog.Log("VerifySentMailInSentItems", "Enter Subject");
                officeActivities_EmailsHelper.TypeText("Subject", subject);

                executionLog.Log("VerifySentMailInSentItems", "Click on Send button");
                officeActivities_EmailsHelper.ClickJS("Send");
                officeActivities_EmailsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifySentMailInSentItems", "Verify confirmation message");
                officeActivities_EmailsHelper.WaitForText("E-Mail Sent Successfully.", 05);

                executionLog.Log("VerifySentMailInSentItems", "Navigate to Sent Email page");
                VisitOffice("mails/sent");
                officeActivities_EmailsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifySentMailInSentItems", "Enter To email in search field");
                officeActivities_EmailsHelper.TypeText("Searchbox", to_email);

                executionLog.Log("VerifySentMailInSentItems", "Click on Search button");
                officeActivities_EmailsHelper.ClickJS("SearchBtn");
                officeActivities_EmailsHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifySentMailInSentItems", "Verify email present");
                officeActivities_EmailsHelper.VerifyText("SentEmail1", to_email);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifySentMailInSentItems");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Sent Mail In Sent Items");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Sent Mail In Sent Items", "Bug", "Medium", "Emails page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Sent Mail In Sent Items");
                        TakeScreenshot("VerifySentMailInSentItems");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifySentMailInSentItems.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifySentMailInSentItems");
                        string id = loginHelper.getIssueID("Verify Sent Mail In Sent Items");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifySentMailInSentItems.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Sent Mail In Sent Items"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Sent Mail In Sent Items");
                //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifySentMailInSentItems");
                executionLog.WriteInExcel("Verify Sent Mail In Sent Items", Status, JIRA, "Office Activities");
            }
        }
    }
}