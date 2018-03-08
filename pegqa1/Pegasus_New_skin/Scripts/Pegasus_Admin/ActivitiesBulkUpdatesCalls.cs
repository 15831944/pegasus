using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class ActivitiesBulkUpdatesCalls : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin")]
        [TestCategory("Fail")]
        [TestCategory("TS6")]
        [TestCategory("Pegasus_Admin")]
        public void activitiesBulkUpdatesCalls()
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
            var officeActivities_CallsHelper = new OfficeActivities_CallsHelper(GetWebDriver());
            var office_ClientsHelper = new Office_ClientsHelper(GetWebDriver());
            var office_LeadsHelper = new Office_LeadsHelper(GetWebDriver());
            var office_OpportunitiesHelper = new Office_OpportunitiesHelper(GetWebDriver());
            var ticket_CreateATicketHelper = new OfficeTickets_CreateTicketsHelper(GetWebDriver());

            // Random Variable
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("ActivitiesBulkUpdatesCalls", " Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("ActivitiesBulkUpdatesCalls", " Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("ActivitiesBulkUpdatesCalls", "Redirect at calls page.");
                VisitOffice("calls");
                officeActivities_CallsHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesBulkUpdatesCalls", " verify title");
                VerifyTitle("Calls");

                executionLog.Log("ActivitiesBulkUpdatesCalls", "Click on Bulk Update");
                officeActivities_CallsHelper.ClickElement("BulkUpdate");
                officeActivities_CallsHelper.WaitForWorkAround(1000);

                executionLog.Log("ActivitiesBulkUpdatesCalls", "Click on Change Status");
                officeActivities_CallsHelper.ClickElement("ChangeCategory");
                officeActivities_CallsHelper.WaitForWorkAround(2000);

                executionLog.Log("ActivitiesBulkUpdatesCalls", "Verify alert text for selecting task.");
                officeActivities_CallsHelper.VerifyAlertText("Please select atleast one record to proceed.");

                executionLog.Log("ActivitiesBulkUpdatesCalls", "Accept alert message by clickin ok.");
                officeActivities_CallsHelper.AcceptAlert();
                officeActivities_CallsHelper.WaitForWorkAround(1000);

                executionLog.Log("ActivitiesBulkUpdatesCalls", "Click on first task.");
                officeActivities_CallsHelper.ClickElement("ChkBox1");

                executionLog.Log("ActivitiesBulkUpdatesCalls", "Click on second task.");
                officeActivities_CallsHelper.ClickElement("ChkBox2");

                executionLog.Log("ActivitiesBulkUpdatesCalls", "Click on bulk update.");
                officeActivities_CallsHelper.ClickElement("BulkUpdate");
                officeActivities_CallsHelper.WaitForWorkAround(1000);

                executionLog.Log("ActivitiesBulkUpdatesCalls", "Click on change status.");
                officeActivities_CallsHelper.ClickForce("ChangeCategory");
                officeActivities_CallsHelper.WaitForWorkAround(2000);

                executionLog.Log("ActivitiesBulkUpdatesCalls", "Select status to be updated.");
                officeActivities_CallsHelper.SelectByText("SelectCategory", "other");

                executionLog.Log("ActivitiesBulkUpdatesCalls", "Click on update button.");
                officeActivities_CallsHelper.ClickElement("UpdateCategory");
                officeActivities_CallsHelper.AcceptAlert();

                executionLog.Log("ActivitiesBulkUpdatesCalls", "Wait for success text.");
                officeActivities_CallsHelper.WaitForText("Call categories updated successfully.", 05);
                officeActivities_CallsHelper.WaitForWorkAround(2000);

                executionLog.Log("ActivitiesBulkUpdatesCalls", "Redirect at tasks page.");
                VisitOffice("calls");
                officeActivities_CallsHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesBulkUpdatesCalls", "Verify Page title as tasks.");
                VerifyTitle("Calls");

                executionLog.Log("ActivitiesBulkUpdatesCalls", "Click on first task.");
                officeActivities_CallsHelper.ClickElement("ChkBox1");

                executionLog.Log("ActivitiesBulkUpdatesCalls", "Click on second task.");
                officeActivities_CallsHelper.ClickElement("ChkBox2");

                executionLog.Log("ActivitiesBulkUpdatesCalls", "Click on bulk update.");
                officeActivities_CallsHelper.ClickElement("BulkUpdate");
                officeActivities_CallsHelper.WaitForWorkAround(1000);

                executionLog.Log("ActivitiesBulkUpdatesCalls", "Click on change user group.");
                officeActivities_CallsHelper.ClickElement("ChangeEmployee");
                officeActivities_CallsHelper.WaitForWorkAround(2000);

                executionLog.Log("ActivitiesBulkUpdatesCalls", "Select group to be updated.");
                officeActivities_CallsHelper.SelectByText("SelectEmployee", "Howard Tang");

                executionLog.Log("ActivitiesBulkUpdatesCalls", "Click on update button.");
                officeActivities_CallsHelper.ClickForce("UpdateOwner");
                officeActivities_CallsHelper.AcceptAlert();
                officeActivities_CallsHelper.WaitForWorkAround(2000);

                executionLog.Log("ActivitiesBulkUpdatesCalls", "Wait for success text.");
                officeActivities_CallsHelper.WaitForText("Call owner updated successfully.", 05);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("ActivitiesBulkUpdatesCalls");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Activities Bulk Updates Calls");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Activities Bulk Updates Calls", "Bug", "Medium", "Calls page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Activities Bulk Updates Calls");
                        TakeScreenshot("ActivitiesBulkUpdatesCalls");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ActivitiesBulkUpdatesCalls.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("ActivitiesBulkUpdatesCalls");
                        string id = loginHelper.getIssueID("Activities Bulk Updates Calls");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ActivitiesBulkUpdatesCalls.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Activities Bulk Updates Calls"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Activities Bulk Updates Calls");
                //   executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("ActivitiesBulkUpdatesCalls");
                executionLog.WriteInExcel("Activities Bulk Updates Calls", Status, JIRA, "Office Activities");
            }
        }
    }
}
