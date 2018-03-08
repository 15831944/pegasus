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
    public class ActivitiesBulkUpdatesDocs : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin")]
        [TestCategory("Fail")]
        [TestCategory("TS6")]
        [TestCategory("Pegasus_Admin")]
        public void activitiesBulkUpdatesDocs()
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
            var officeActivities_DocumentsHelper = new OfficeActivities_DocumentHelper(GetWebDriver());

            // Variable
            var name = "Doc" + RandomNumber(1, 9999);
            var email = "Test" + GetRandomNumber() + "@gmail.com";
            var ValidFile = GetPathToFile() + "index.jpg";
            var InvalidFile = GetPathToFile() + "chrome.exe";
            String JIRA = "";
            String Status = "Pass";


            try
            {
                executionLog.Log("ActivitiesBulkUpdatesDocs", " Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("ActivitiesBulkUpdatesDocs", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("ActivitiesBulkUpdatesDocs", "Redirect to document page");
                VisitOffice("documents");
                officeActivities_DocumentsHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesBulkUpdatesDocs", "verify title documents.");
                VerifyTitle("Documents");

                executionLog.Log("ActivitiesBulkUpdatesDocs", "Click on Bulk Update");
                officeActivities_DocumentsHelper.ClickElement("BulkUpdate");
                officeActivities_DocumentsHelper.WaitForWorkAround(1000);

                executionLog.Log("ActivitiesBulkUpdatesDocs", "Click on Change Status");
                officeActivities_DocumentsHelper.ClickElement("ChangeStatus");
                officeActivities_DocumentsHelper.WaitForWorkAround(2000);

                executionLog.Log("ActivitiesBulkUpdatesDocs", "Verify alert text for selecting document.");
                officeActivities_DocumentsHelper.VerifyAlertText("Please select atleast one record to proceed.");

                executionLog.Log("ActivitiesBulkUpdatesDocs", "Accept alert message by clicking ok.");
                officeActivities_DocumentsHelper.AcceptAlert();
                officeActivities_DocumentsHelper.WaitForWorkAround(1000);

                executionLog.Log("ActivitiesBulkUpdatesDocs", "Click on first document.");
                officeActivities_DocumentsHelper.ClickElement("ClickOnCheckBox");

                executionLog.Log("ActivitiesBulkUpdatesDocs", "Click on second document.");
                officeActivities_DocumentsHelper.ClickElement("ClickOnCheckBox2");

                executionLog.Log("ActivitiesBulkUpdatesDocs", "Click on bulk update.");
                officeActivities_DocumentsHelper.ClickElement("BulkUpdate");
                officeActivities_DocumentsHelper.WaitForWorkAround(1000);

                executionLog.Log("ActivitiesBulkUpdatesDocs", "Click on change status.");
                officeActivities_DocumentsHelper.ClickElement("ChangeStatus");
                officeActivities_DocumentsHelper.WaitForWorkAround(2000);

                executionLog.Log("ActivitiesBulkUpdatesDocs", "Select status to be updated.");
                officeActivities_DocumentsHelper.SelectByText("SelectStatus", "Inactive");

                executionLog.Log("ActivitiesBulkUpdatesDocs", "Click on update button.");
                officeActivities_DocumentsHelper.ClickElement("UpdateStatus");
                officeActivities_DocumentsHelper.WaitForWorkAround(2000);
                officeActivities_DocumentsHelper.AcceptAlert();

                executionLog.Log("ActivitiesBulkUpdatesDocs", "Wait for success text.");
                officeActivities_DocumentsHelper.WaitForText("Document status updated successfully.", 5);

                executionLog.Log("ActivitiesBulkUpdatesDocs", "Redirect to create document page");
                VisitOffice("documents");
                officeActivities_DocumentsHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesBulkUpdatesDocs", "verify title");
                VerifyTitle("Documents");

                executionLog.Log("ActivitiesBulkUpdatesDocs", "Click on first document.");
                officeActivities_DocumentsHelper.ClickElement("ClickOnCheckBox");

                executionLog.Log("ActivitiesBulkUpdatesDocs", "Click on second document.");
                officeActivities_DocumentsHelper.ClickElement("ClickOnCheckBox2");

                executionLog.Log("ActivitiesBulkUpdatesDocs", "Click on bulk update.");
                officeActivities_DocumentsHelper.ClickElement("BulkUpdate");
                officeActivities_DocumentsHelper.WaitForWorkAround(1000);

                executionLog.Log("ActivitiesBulkUpdatesDocs", "Click on change owner.");
                officeActivities_DocumentsHelper.ClickJs("ChangeOwner");
                officeActivities_DocumentsHelper.WaitForWorkAround(2000);

                executionLog.Log("ActivitiesBulkUpdatesDocs", "Select owner to be updated.");
                officeActivities_DocumentsHelper.SelectByText("SelectOwner", "Howard Tang");

                executionLog.Log("ActivitiesBulkUpdatesDocs", "Click on update button.");
                officeActivities_DocumentsHelper.ClickElement("UpdateOwner");
                officeActivities_DocumentsHelper.WaitForWorkAround(2000);
                officeActivities_DocumentsHelper.AcceptAlert();

                executionLog.Log("ActivitiesBulkUpdatesDocs", "Wait for success text.");
                officeActivities_DocumentsHelper.WaitForText("Document owner updated successfully.", 10);

                executionLog.Log("ActivitiesBulkUpdatesDocs", "Click on first document.");
                officeActivities_DocumentsHelper.ClickElement("ClickOnCheckBox");

                executionLog.Log("ActivitiesBulkUpdatesDocs", "Click on second document.");
                officeActivities_DocumentsHelper.ClickElement("ClickOnCheckBox2");

                executionLog.Log("ActivitiesBulkUpdatesDocs", "Click on bulk update.");
                officeActivities_DocumentsHelper.ClickElement("BulkUpdate");
                officeActivities_DocumentsHelper.WaitForWorkAround(1000);

                executionLog.Log("ActivitiesBulkUpdatesDocs", "Click on change user group.");
                officeActivities_DocumentsHelper.ClickJs("ChangeUserGroup");
                officeActivities_DocumentsHelper.WaitForWorkAround(2000);

                executionLog.Log("ActivitiesBulkUpdatesDocs", "Select group to be updated.");
                officeActivities_DocumentsHelper.SelectByText("SelectGroup", "Primary Group");

                executionLog.Log("ActivitiesBulkUpdatesDocs", "Click on update button.");
                officeActivities_DocumentsHelper.ClickForce("UpdateOwner");
                officeActivities_DocumentsHelper.WaitForWorkAround(2000);
                officeActivities_DocumentsHelper.AcceptAlert();
                officeActivities_DocumentsHelper.WaitForWorkAround(2000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("ActivitiesBulkUpdatesDocs");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Activities Bulk Updates Docs");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Activities Bulk Updates Docs", "Bug", "Medium", "Document page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Activities Bulk Updates Docs");
                        TakeScreenshot("ActivitiesBulkUpdatesDocs");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ActivitiesBulkUpdatesDocs.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("ActivitiesBulkUpdatesDocs");
                        string id = loginHelper.getIssueID("Activities Bulk Updates Docs");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ActivitiesBulkUpdatesDocs.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Activities Bulk Updates Docs"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Activities Bulk Updates Docs");
             //   executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("ActivitiesBulkUpdatesDocs");
                executionLog.WriteInExcel("Activities Bulk Updates Docs", Status, JIRA, "Office Activities");
            }
        }
    }
}