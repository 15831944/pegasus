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
    public class DocumentBulkUpdateIssue : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin")]
        [TestCategory("Fail")]
        [TestCategory("TS2")]
        [TestCategory("BugTestNew")]
        public void documentBulkUpdateIssue()
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

            // Random Variable
            String JIRA = "";
            String Status = "Pass";


            try
            {
                executionLog.Log("DocumentBulkUpdateIssue", " Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("DocumentBulkUpdateIssue", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("DocumentBulkUpdateIssue", "Click On  Admin");
                VisitOffice("admin");

                executionLog.Log("DocumentBulkUpdateIssue", "Redirect to document page");
                VisitOffice("documents");
                officeActivities_DocumentsHelper.WaitForWorkAround(4000);

                executionLog.Log("DocumentBulkUpdateIssue", "verify title documents.");
                VerifyTitle("Documents");

                executionLog.Log("DocumentBulkUpdateIssue", "Click on Bulk Update");
                officeActivities_DocumentsHelper.ClickElement("BulkUpdate");
                officeActivities_DocumentsHelper.WaitForWorkAround(2000);

                executionLog.Log("DocumentBulkUpdateIssue", "Click on Change Status");
                officeActivities_DocumentsHelper.ClickElement("ChangeStatus");
                officeActivities_DocumentsHelper.WaitForWorkAround(5000);

                executionLog.Log("DocumentBulkUpdateIssue", "Verify alert text for selecting document.");
                officeActivities_DocumentsHelper.VerifyAlertText("Please select atleast one record to proceed.");

                executionLog.Log("DocumentBulkUpdateIssue", "Accept alert message by clicking ok.");
                officeActivities_DocumentsHelper.AcceptAlert();
                officeActivities_DocumentsHelper.WaitForWorkAround(3000);

                executionLog.Log("DocumentBulkUpdateIssue", "Click on first document.");
                officeActivities_DocumentsHelper.ClickElement("ClickOnCheckBox");

                executionLog.Log("DocumentBulkUpdateIssue", "Click on second document.");
                officeActivities_DocumentsHelper.ClickElement("ClickOnCheckBox2");

                executionLog.Log("DocumentBulkUpdateIssue", "Click on bulk update.");
                officeActivities_DocumentsHelper.ClickElement("BulkUpdate");
                officeActivities_DocumentsHelper.WaitForWorkAround(2000);

                executionLog.Log("DocumentBulkUpdateIssue", "Click on change status.");
                officeActivities_DocumentsHelper.ClickElement("ChangeStatus");
                officeActivities_DocumentsHelper.WaitForWorkAround(4000);

                executionLog.Log("DocumentBulkUpdateIssue", "Select status to be updated.");
                officeActivities_DocumentsHelper.SelectByText("SelectStatus", "Inactive");

                executionLog.Log("DocumentBulkUpdateIssue", "Click on update button.");
                officeActivities_DocumentsHelper.ClickElement("UpdateStatus");
                officeActivities_DocumentsHelper.AcceptAlert();
                officeActivities_DocumentsHelper.WaitForWorkAround(4000);

                executionLog.Log("DocumentBulkUpdateIssue", "Wait for success text.");
                officeActivities_DocumentsHelper.WaitForText("Document status updated successfully.", 10);

                executionLog.Log("DocumentBulkUpdateIssue", "Redirect to create document page");
                VisitOffice("documents");
                officeActivities_DocumentsHelper.WaitForWorkAround(3000);

                executionLog.Log("DocumentBulkUpdateIssue", "verify title");
                VerifyTitle("Documents");

                executionLog.Log("DocumentBulkUpdateIssue", "Click on first document.");
                officeActivities_DocumentsHelper.ClickElement("ClickOnCheckBox");

                executionLog.Log("DocumentBulkUpdateIssue", "Click on second document.");
                officeActivities_DocumentsHelper.ClickElement("ClickOnCheckBox2");

                executionLog.Log("DocumentBulkUpdateIssue", "Click on bulk update.");
                officeActivities_DocumentsHelper.ClickElement("BulkUpdate");
                officeActivities_DocumentsHelper.WaitForWorkAround(3000);

                executionLog.Log("DocumentBulkUpdateIssue", "Click on change user group.");
                officeActivities_DocumentsHelper.ClickElement("ChangeUserGroup");
                officeActivities_DocumentsHelper.WaitForWorkAround(2000);

                executionLog.Log("DocumentBulkUpdateIssue", "Select group to be updated.");
                officeActivities_DocumentsHelper.SelectByText("SelectGroup", "Primary Group");
                //officeActivities_DocumentsHelper.WaitForWorkAround(3000);

                executionLog.Log("DocumentBulkUpdateIssue", "Click on update button.");
                officeActivities_DocumentsHelper.ClickForce("UpdateOwner");
                officeActivities_DocumentsHelper.AcceptAlert();
                officeActivities_DocumentsHelper.WaitForWorkAround(3000);

                executionLog.Log("DocumentBulkUpdateIssue", "Wait for success text.");
                officeActivities_DocumentsHelper.WaitForText("Document owner updated successfully.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("DocumentBulkUpdateIssue");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Document Bulk Update Issue");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Document Bulk Update Issue", "Bug", "Medium", "Document page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Document Bulk Update Issue");
                        TakeScreenshot("DocumentBulkUpdateIssue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\DocumentBulkUpdateIssue.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("DocumentBulkUpdateIssue");
                        string id = loginHelper.getIssueID("Document Bulk Update Issue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\DocumentBulkUpdateIssue.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Document Bulk Update Issue"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Document Bulk Update Issue");
             //   executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("DocumentBulkUpdateIssue");
                executionLog.WriteInExcel("Document Bulk Update Issue", Status, JIRA, "Office Activities");
            }
        }
    }
}