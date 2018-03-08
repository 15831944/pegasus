using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class LeadsAdvanceFilterActivities : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS6")]
        [TestCategory("Pegasus_Admin")]
        public void leadsAdvanceFilterActivities()
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
            var office_LeadsHelper = new Office_LeadsHelper(GetWebDriver());

            // Variable
            var DocName = "Test Exe" + GetRandomNumber();
            var fileUpl = GetPathToFile() + "chrome.exe";
            String JIRA = "";
            String Status = "Pass";

            try
            {
            executionLog.Log("LeadsAdvanceFilterActivities", "Login with valid username and password");
            Login(username[0], password[0]);
            Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

            executionLog.Log("LeadsAdvanceFilterActivities", "Verify Page title");
            VerifyTitle("Dashboard");
            Console.WriteLine("Redirected at Dashboard screen.");


            // Verify leads with notes.

            executionLog.Log("LeadsAdvanceFilterActivities", "Redirect To URL");
            VisitOffice("leads");
            office_LeadsHelper.WaitForWorkAround(3000);

            executionLog.Log("LeadsAdvanceFilterActivities", "Verify page title.");
            VerifyTitle("Leads");

            executionLog.Log("LeadsAdvanceFilterActivities", "Click on advance filter.");
            office_LeadsHelper.ClickForce("AdvanceFilter");
            office_LeadsHelper.WaitForWorkAround(2000);

            executionLog.Log("LeadsAdvanceFilterActivities", "Click on show activiities button.");
            office_LeadsHelper.ClickForce("ShowActivities");
            office_LeadsHelper.WaitForWorkAround(1000);

            executionLog.Log("LeadsAdvanceFilterActivities", "Selct leads activity type.");
            office_LeadsHelper.ClickForce("LeadWithNote");
            //office_LeadsHelper.WaitForWorkAround(3000);

            executionLog.Log("LeadsAdvanceFilterActivities", "Click on apply button.");
            office_LeadsHelper.ClickForce("Apply");
            office_LeadsHelper.WaitForWorkAround(3000);

            executionLog.Log("LeadsAdvanceFilterActivities", "Click on any leads.");
            office_LeadsHelper.ClickForce("ClickAnyLead");
            office_LeadsHelper.WaitForWorkAround(3000);

            executionLog.Log("LeadsAdvanceFilterActivities", "Select actitivity type as notes.");
            office_LeadsHelper.SelectByText("SelectActivityType", "Notes");
            office_LeadsHelper.WaitForWorkAround(3000);

            executionLog.Log("LeadsAdvanceFilterActivities", "Verify notes present for leads.");
            //office_LeadsHelper.IsElementVisible("LeadNotees.");


            // Verify leads with Open meetings.

            executionLog.Log("LeadsAdvanceFilterActivities", "Redirect To URL");
            VisitOffice("leads");
            office_LeadsHelper.WaitForWorkAround(3000);

            executionLog.Log("LeadsAdvanceFilterActivities", "Verify page title.");
            VerifyTitle("Leads");

            executionLog.Log("LeadsAdvanceFilterActivities", "Click on advance filter.");
            office_LeadsHelper.ClickForce("AdvanceFilter");
            office_LeadsHelper.WaitForWorkAround(2000);

            executionLog.Log("LeadsAdvanceFilterActivities", "Click on show activiities button.");
            office_LeadsHelper.ClickForce("ShowActivities");
            office_LeadsHelper.WaitForWorkAround(1000);

            executionLog.Log("LeadsAdvanceFilterActivities", "Selct leads activity type.");
            office_LeadsHelper.ClickForce("LeadOpenMeet");
            //office_LeadsHelper.WaitForWorkAround(3000);

            executionLog.Log("LeadsAdvanceFilterActivities", "Click on apply button.");
            office_LeadsHelper.ClickForce("Apply");
            office_LeadsHelper.WaitForWorkAround(3000);

            executionLog.Log("LeadsAdvanceFilterActivities", "Click on any leads.");
            office_LeadsHelper.ClickForce("ClickAnyLead");
            office_LeadsHelper.WaitForWorkAround(3000);

            executionLog.Log("LeadsAdvanceFilterActivities", "Select actitivity type as meetings.");
            office_LeadsHelper.SelectByText("SelectActivityType", "Meetings");
            office_LeadsHelper.WaitForWorkAround(3000);

            executionLog.Log("LeadsAdvanceFilterActivities", "Verify meeting present for leads.");
            //office_LeadsHelper.IsElementVisible("LeadsMeetings.");


            // Verify leads with Open tasks.

            executionLog.Log("LeadsAdvanceFilterActivities", "Redirect To URL");
            VisitOffice("leads");
            office_LeadsHelper.WaitForWorkAround(3000);

            executionLog.Log("LeadsAdvanceFilterActivities", "Verify page title.");
            VerifyTitle("Leads");

            executionLog.Log("LeadsAdvanceFilterActivities", "Click on advance filter.");
            office_LeadsHelper.ClickForce("AdvanceFilter");
            office_LeadsHelper.WaitForWorkAround(2000);

            executionLog.Log("LeadsAdvanceFilterActivities", "Click on show activiities button.");
            office_LeadsHelper.ClickForce("ShowActivities");
            office_LeadsHelper.WaitForWorkAround(1000);

            executionLog.Log("LeadsAdvanceFilterActivities", "Selct leads activity type.");
            office_LeadsHelper.ClickForce("LeadsWithOpenTaks");
            //office_LeadsHelper.WaitForWorkAround(3000);

            executionLog.Log("LeadsAdvanceFilterActivities", "Click on apply button.");
            office_LeadsHelper.ClickForce("Apply");
            office_LeadsHelper.WaitForWorkAround(3000);

            executionLog.Log("LeadsAdvanceFilterActivities", "Click on any leads.");
            office_LeadsHelper.ClickForce("ClickAnyLead");
            office_LeadsHelper.WaitForWorkAround(3000);

            executionLog.Log("LeadsAdvanceFilterActivities", "Select actitivity type as tasks.");
            office_LeadsHelper.SelectByText("SelectActivityType", "Tasks");
            office_LeadsHelper.WaitForWorkAround(3000);

            executionLog.Log("LeadsAdvanceFilterActivities", "Verify task present for leads.");
            //  office_LeadsHelper.IsElementVisible("LeadsTasks.");


            // Verify leads with documents.

            executionLog.Log("LeadsAdvanceFilterActivities", "Redirect To URL");
            VisitOffice("leads");
            office_LeadsHelper.WaitForWorkAround(3000);

            executionLog.Log("LeadsAdvanceFilterActivities", "Verify page title.");
            VerifyTitle("Leads");

            executionLog.Log("LeadsAdvanceFilterActivities", "Click on advance filter.");
            office_LeadsHelper.ClickForce("AdvanceFilter");
            office_LeadsHelper.WaitForWorkAround(1000);

            executionLog.Log("LeadsAdvanceFilterActivities", "Click on show activiities button.");
            office_LeadsHelper.ClickForce("ShowActivities");
            office_LeadsHelper.WaitForWorkAround(1000);

            executionLog.Log("LeadsAdvanceFilterActivities", "Selct leads activity type.");
            office_LeadsHelper.ClickForce("LeadsWithDocs");
            //office_LeadsHelper.WaitForWorkAround(3000);

            executionLog.Log("LeadsAdvanceFilterActivities", "Click on apply button.");
            office_LeadsHelper.ClickForce("Apply");
            office_LeadsHelper.WaitForWorkAround(3000);

            executionLog.Log("LeadsAdvanceFilterActivities", "Click on any leads.");
            office_LeadsHelper.ClickForce("ClickAnyLead");
            office_LeadsHelper.WaitForWorkAround(3000);

            executionLog.Log("LeadsAdvanceFilterActivities", "Select actitivity type as documents.");
            office_LeadsHelper.SelectByText("SelectActivityType", "Documents");
            office_LeadsHelper.WaitForWorkAround(3000);

            executionLog.Log("LeadsAdvanceFilterActivities", "Verify document present for lead.");
            // office_LeadsHelper.IsElementVisible("LeadsDOcs.");


            // Verify leads with E-Mails.

            executionLog.Log("LeadsAdvanceFilterActivities", "Redirect To URL");
            VisitOffice("leads");
            office_LeadsHelper.WaitForWorkAround(3000);

            executionLog.Log("LeadsAdvanceFilterActivities", "Verify page title.");
            VerifyTitle("Leads");

            executionLog.Log("LeadsAdvanceFilterActivities", "Click on advance filter.");
            office_LeadsHelper.ClickForce("AdvanceFilter");
            office_LeadsHelper.WaitForWorkAround(2000);

            executionLog.Log("LeadsAdvanceFilterActivities", "Click on show activiities button.");
            office_LeadsHelper.ClickForce("ShowActivities");
            office_LeadsHelper.WaitForWorkAround(1000);

            executionLog.Log("LeadsAdvanceFilterActivities", "Selct leads activity type.");
            office_LeadsHelper.ClickForce("LeadsWithEmails");
            //office_LeadsHelper.WaitForWorkAround(3000);

            executionLog.Log("LeadsAdvanceFilterActivities", "Click on apply button.");
            office_LeadsHelper.ClickForce("Apply");
            office_LeadsHelper.WaitForWorkAround(3000);

            executionLog.Log("LeadsAdvanceFilterActivities", "Click on any lead.");
            office_LeadsHelper.ClickForce("ClickAnyLead");
            office_LeadsHelper.WaitForWorkAround(3000);

            executionLog.Log("LeadsAdvanceFilterActivities", "Select actitivity type as emails.");
            office_LeadsHelper.SelectByText("SelectActivityType", "E-Mails");
            office_LeadsHelper.WaitForWorkAround(3000);

            executionLog.Log("LeadsAdvanceFilterActivities", "Verify email present for leads.");
            // office_LeadsHelper.IsElementVisible("LEadsEmails.");

        }
      catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("LeadsAdvanceFilterActivities");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Leads Advance Filter Activities");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Leads Advance Filter Activities", "Bug", "Medium", "Opportunities page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Leads Advance Filter Activities");
                        TakeScreenshot("LeadsAdvanceFilterActivities");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\LeadsAdvanceFilterActivities.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("LeadsAdvanceFilterActivities");
                        string id = loginHelper.getIssueID("Leads Advance Filter Activities");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\LeadsAdvanceFilterActivities.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Leads Advance Filter Activities"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Leads Advance Filter Activities");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("LeadsAdvanceFilterActivities");
                executionLog.WriteInExcel("Leads Advance Filter Activities", Status, JIRA, "Opportunities Management");
            }
        }
    }
}
