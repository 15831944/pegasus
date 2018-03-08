using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class EditPickListStatuses : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS1")]
        [TestCategory("BugTest")]
        public void editPickListStatuses()
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
            var System_PicklistHelper = new System_PicklistsHelper(GetWebDriver());

            // Random Variables
            var name = "Test Pick" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("EditPickListStatuses", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("EditPickListStatuses", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("EditPickListStatuses", "Redirect To Admin");
                VisitOffice("admin");

                executionLog.Log("EditPickListStatuses", "Redirect To PickList");
                VisitOffice("pick-lists");

                executionLog.Log("EditPickListStatuses", "Verify page title");
                VerifyTitle("Picklists");

                executionLog.Log("EditPickListStatuses", "Enter Picklist To Seacrch");
                System_PicklistHelper.TypeText("EnterNamePicklist", "Statuses");
                System_PicklistHelper.WaitForWorkAround(2000);

                executionLog.Log("EditPickListStatuses", "Enter Module to search");
                System_PicklistHelper.TypeText("EnterModule", "Merchants");
                System_PicklistHelper.WaitForWorkAround(3000);

                executionLog.Log("EditPickListStatuses", "Click On Statuses");
                System_PicklistHelper.ClickElement("ClickOnStatuses");

                executionLog.Log("EditPickListStatuses", "Clcik on Add button");
                System_PicklistHelper.ClickElement("AddPick");
                System_PicklistHelper.WaitForWorkAround(3000);

                executionLog.Log("EditPickListStatuses", "Enter PickList Value");
                System_PicklistHelper.TypeText("EnterPickListValue", name);

                executionLog.Log("EditPickListStatuses", "Click PickList Save Button");
                System_PicklistHelper.ClickElement("PickListSaveBtn");

                executionLog.Log("EditPickListStatuses", "Click PickList Save Button");
                System_PicklistHelper.ClickElement("Cancel");
                System_PicklistHelper.WaitForWorkAround(3000);

                executionLog.Log("EditPickListStatuses", "Verify page title");
                VerifyTitle("Add/Edit Picklist Items");

                executionLog.Log("EditPickListStatuses", "Click delete Button");
                System_PicklistHelper.ClickElement("DeletePick");

                executionLog.Log("EditPickListStatuses", "Click on item to deleted");
                System_PicklistHelper.DeletePickList(name);

                executionLog.Log("EditPickListStatuses", "Selelct replace with item.");
                System_PicklistHelper.SelectText("ReplacePiclist", "Declined");

                executionLog.Log("EditPickListStatuses", "Click PickList Save Button");
                System_PicklistHelper.ClickElement("PickListSaveBtn");

                executionLog.Log("EditPickListStatuses", "Accept alert message.");
                System_PicklistHelper.AcceptAlert();
                System_PicklistHelper.WaitForWorkAround(4000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("EditPickListStatuses");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Edit Pick List Statuses");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Edit Pick List Statuses", "Bug", "Medium", "Pick Lists Page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Edit Pick List Statuses");
                        TakeScreenshot("EditPickListStatuses");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditPickListStatuses.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("EditPickListStatuses");
                        string id = loginHelper.getIssueID("Edit Pick List Statuses");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditPickListStatuses.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Edit Pick List Statuses"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Edit Pick List Statuses");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("EditPickListStatuses");
                executionLog.WriteInExcel("Edit Pick List Statuses", Status, JIRA, "Office Systems");
            }
        }
    }
}
