using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class LeadsEmailUrlChange : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Url")]
        [TestCategory("Fail")]
        [TestCategory("TS3")]
        [TestCategory("ChangeUrl")]
        public void leadsEmailUrlChange()
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
            var officeActivities_EmailHelper = new OfficeActivities_EmailsHelper(GetWebDriver());
            var office_LeadsHelper = new Office_LeadsHelper(GetWebDriver());

            // Variable
            var FirstName = "Test" + GetRandomNumber();
            var LastName = "Tester" + GetRandomNumber();
            var Number = "12345678" + GetRandomNumber();
            var SendTo = "Test" + GetRandomNumber() + "@yopmail.com";
            String JIRA = "";
            String Status = "Pass";

            try
            {
            executionLog.Log("LeadsEmailUrlChange", "Login with valid username and password");
            Login(username[0], password[0]);
            Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

            executionLog.Log("LeadsEmailUrlChange", "Verify Page title");
            VerifyTitle("Dashboard");
            Console.WriteLine("Redirected at Dashboard screen.");

            executionLog.Log("LeadsEmailUrlChange", "Go to Leads page");
            VisitOffice("leads");
            officeActivities_EmailHelper.WaitForWorkAround(3000);

            executionLog.Log("LeadsEmailUrlChange", "Click On Any Lead");
            office_LeadsHelper.ClickElement("ClickAnyLead");
            office_LeadsHelper.WaitForWorkAround(2000);

            executionLog.Log("LeadsEmailUrlChange", "Click On Send Email");
            office_LeadsHelper.ClickElement("SendEmail");
            officeActivities_EmailHelper.WaitForWorkAround(2000);

            executionLog.Log("LeadsEmailUrlChange", "Enter Email Id");
            officeActivities_EmailHelper.TypeText("Sendto", SendTo);
            officeActivities_EmailHelper.WaitForWorkAround(2000);

            executionLog.Log("LeadsEmailUrlChange", "Enter  Subject");
            officeActivities_EmailHelper.TypeText("EmailName", "Email Subject");
            officeActivities_EmailHelper.WaitForWorkAround(1000);

            executionLog.Log("LeadsEmailUrlChange", "Click on Send button");
            officeActivities_EmailHelper.ClickElement("SendEmailActivity");
            officeActivities_EmailHelper.WaitForWorkAround(2000);

            //executionLog.Log("LeadsEmailUrlChange", "Wait for element to present.");
            //officeActivities_EmailHelper.WaitForText("Description", 10);

            executionLog.Log("LeadsEmailUrlChange", "Select Activity >> Emails");
            officeActivities_EmailHelper.Select("SelectActivityType", "E-Mails");
            officeActivities_EmailHelper.WaitForWorkAround(2000);

            executionLog.Log("LeadsEmailUrlChange", "Click On E-mail");
            officeActivities_EmailHelper.PressEnter("ClickOnNoteOppSubj");
            officeActivities_EmailHelper.WaitForWorkAround(3000);

            executionLog.Log("LeadsEmailUrlChange", "Change the url with the url number of another office");
            VisitOffice("mails/view/57");
            officeActivities_EmailHelper.WaitForWorkAround(4000);

            executionLog.Log("LeadsEmailUrlChange", "Verify Validation");
            officeActivities_EmailHelper.WaitForText("You don't have privileges to view this E-Mail.", 10);
            officeActivities_EmailHelper.WaitForWorkAround(1000);

            executionLog.Log("LeadsEmailUrlChange", "Redirect to URL");
            VisitOffice("mails/sent");
            officeActivities_EmailHelper.WaitForWorkAround(3000);

            executionLog.Log("LeadsEmailUrlChange", "Verify page title");
            VerifyTitle("Sent");
            //officeActivities_EmailHelper.WaitForWorkAround(2000);

            executionLog.Log("LeadsEmailUrlChange", "Enter Send to in search ");
            officeActivities_EmailHelper.TypeText("SearchMailInput", SendTo);
            officeActivities_EmailHelper.WaitForWorkAround(1000);

            executionLog.Log("LeadsEmailUrlChange", "Click on search btn");
            officeActivities_EmailHelper.ClickElement("SearchBtn");
            officeActivities_EmailHelper.WaitForWorkAround(2000);

            executionLog.Log("LeadsEmailUrlChange", "Select searched email.");
            officeActivities_EmailHelper.ClickJS("CheckBox1");
            officeActivities_EmailHelper.WaitForWorkAround(1000);

            executionLog.Log("LeadsEmailUrlChange", "Click on delete btn");
            officeActivities_EmailHelper.ClickElement("Delete");
            officeActivities_EmailHelper.WaitForWorkAround(3000);

            executionLog.Log("LeadsEmailUrlChange", "Verify Email Deleted successfully");
            officeActivities_EmailHelper.WaitForText("E-Mail has been moved to the Recycle Bin.", 05);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("LeadsEmailUrlChange");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Leads Email Url Change");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Leads Email Url Change", "Bug", "Medium", "Leads page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Leads Email Url Change");
                        TakeScreenshot("LeadsEmailUrlChange");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\LeadsEmailUrlChange.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("LeadsEmailUrlChange");
                        string id = loginHelper.getIssueID("Leads Email Url Change");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\LeadsEmailUrlChange.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Leads Email Url Change"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Leads Email Url Change");
                //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("LeadsEmailUrlChange");
                executionLog.WriteInExcel("Leads Email Url Change", Status, JIRA, "Leads Email");
            }
        }
    }
}