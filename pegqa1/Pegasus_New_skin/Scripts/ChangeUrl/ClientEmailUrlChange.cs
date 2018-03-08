using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class ClientEmailUrlChange : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Url")]
        [TestCategory("Fail")]
        [TestCategory("TS3")]
        [TestCategory("ChangeUrl")]
        public void clientEmailUrlChange()
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
            var office_ClientsHelper = new Office_ClientsHelper(GetWebDriver());
            var officeActivities_EmailHelper = new OfficeActivities_EmailsHelper(GetWebDriver());

            // Variable
            var SendTo = "Test" + GetRandomNumber() + "@yopmail.com";
            String JIRA = "";
            String Status = "Pass";

            try
            {

            executionLog.Log("ClientEmailUrlChange", "Login with valid username and password");
            Login(username[0], password[0]);
            Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

            executionLog.Log("ClientEmailUrlChange", "Verify Page title");
            VerifyTitle("Dashboard");
            Console.WriteLine("Redirected at Dashboard screen.");

            executionLog.Log("ClientEmailUrlChange", "Goto User Agent >> Client");
            VisitOffice("clients");
            office_ClientsHelper.WaitForWorkAround(3000);

            executionLog.Log("ClientEmailUrlChange", "Click On Any Client");
            office_ClientsHelper.ClickElement("ClickOnAnyClient");
            office_ClientsHelper.WaitForWorkAround(3000);

            executionLog.Log("ClientEmailUrlChange", "Click On Send Email button");
            office_ClientsHelper.ClickElement("AddEmail");
            officeActivities_EmailHelper.WaitForWorkAround(2000);

            executionLog.Log("ClientEmailUrlChange", "Enter Email Id");
            officeActivities_EmailHelper.TypeText("To", SendTo);

            var EmailName = "Email Subj Test" + GetRandomNumber();
            executionLog.Log("ClientEmailUrlChange", "Enter  Subject");
            officeActivities_EmailHelper.TypeText("EmailName", EmailName);
            //officeActivities_EmailHelper.WaitForWorkAround(4000);

            executionLog.Log("ClientEmailUrlChange", "Click on Send button");
            officeActivities_EmailHelper.ClickElement("SendEmailActivity");
            officeActivities_EmailHelper.WaitForWorkAround(3000);

            executionLog.Log("ClientEmailUrlChange", "Select Activity >> Email");
            officeActivities_EmailHelper.Select("SelectActivityType", "E-Mails");

            executionLog.Log("ClientEmailUrlChange", "Click on Email in Activity");
            officeActivities_EmailHelper.ClickJS("ClickEmail1");
            officeActivities_EmailHelper.WaitForWorkAround(3000);

            executionLog.Log("ClientEmailUrlChange", "Change the url with the url number of another office");
            VisitOffice("mails/view/ ");
            //officeActivities_EmailHelper.WaitForWorkAround(4000);

            executionLog.Log("ClientEmailUrlChange", "Verify Validation");
            officeActivities_EmailHelper.WaitForText("You don't have privileges to view this E-Mail.", 05);

            executionLog.Log("SendEmailFromActivity", "Redirect to URL");
            VisitOffice("mails/sent");
            officeActivities_EmailHelper.WaitForWorkAround(3000);

            executionLog.Log("SendEmailFromActivity", "Verify page title");
            VerifyTitle("Sent");

            executionLog.Log("SendEmailFromActivity", "Enter Send to in search ");
            officeActivities_EmailHelper.TypeText("SearchMailInput", SendTo);

            executionLog.Log("SendEmailFromActivity", "Click on search btn");
            officeActivities_EmailHelper.ClickElement("SearchBtn");
            officeActivities_EmailHelper.WaitForWorkAround(3000);

            executionLog.Log("SendEmailFromActivity", "Select searched email.");
            officeActivities_EmailHelper.ClickJS("CheckBox1");

            executionLog.Log("SendEmailFromActivity", "Click on delete btn");
            officeActivities_EmailHelper.ClickElement("Delete");
            officeActivities_EmailHelper.WaitForWorkAround(3000);

            executionLog.Log("SendEmailFromActivity", "Verify Email Deleted successfully");
            officeActivities_EmailHelper.WaitForText("E-Mail has been moved to the Recycle Bin.", 05);

        }
       catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("ClientEmailUrlChange");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Client Email Url Change");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Client Email Url Change", "Bug", "Medium", "Client page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Client Email Url Change");
                        TakeScreenshot("ClientEmailUrlChange");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ClientEmailUrlChange.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("ClientEmailUrlChange");
                        string id = loginHelper.getIssueID("Client Email Url Change");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ClientEmailUrlChange.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Client Email Url Change"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Client Email Url Change");
           //     executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("ClientEmailUrlChange");
                executionLog.WriteInExcel("Client Email Url Change", Status, JIRA, "Client management");
            }
        }
    }
} 
