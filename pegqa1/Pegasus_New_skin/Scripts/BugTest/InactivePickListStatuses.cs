using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class InactivePickListStatuses : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS1")]
        [TestCategory("BugTest")]
        public void inactivePickListStatuses()
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
            var system_PicklistHelper = new System_PicklistsHelper(GetWebDriver());

            // Variables
            var name = "Test" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";
            try
            {
                executionLog.Log("InactivePickListStatuses", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("InactivePickListStatuses", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("InactivePickListStatuses", "Redirect To Admin");
                VisitOffice("admin");

                executionLog.Log("InactivePickListStatuses", "Redirect To PickList");
                VisitOffice("pick-lists");
                system_PicklistHelper.WaitForWorkAround(5000);

                executionLog.Log("InactivePickListStatuses", "Verify page title.");
                VerifyTitle("Picklists");

                executionLog.Log("InactivePickListStatuses", "Enter Name To Seacrch");
                system_PicklistHelper.TypeText("EnterNamePicklist", "Statuses");
                system_PicklistHelper.WaitForWorkAround(2000);

                executionLog.Log("InactivePickListStatuses", "Enter Module To Seacrch");
                system_PicklistHelper.TypeText("EnterModule", "Merchants");
                system_PicklistHelper.WaitForWorkAround(2000);

                executionLog.Log("InactivePickListStatuses", "Click On first Statuses");
                system_PicklistHelper.ClickElement("ClickOnStatuses");

                executionLog.Log("InactivePickListStatuses", "Verify page title.");
                VerifyTitle("Add/Edit Picklist Items");

                executionLog.Log("InactivePickListStatuses", "Clcik on Add button");
                system_PicklistHelper.ClickElement("AddPick");

                executionLog.Log("InactivePickListStatuses", "Enter PickList name");
                system_PicklistHelper.TypeText("EnterPickListValue", name);

                executionLog.Log("InactivePickListStatuses", "Click PickList Save Button");
                system_PicklistHelper.ClickElement("PickListSaveBtn");
                system_PicklistHelper.WaitForWorkAround(2000);

                executionLog.Log("InactivePickListStatuses", "Refresh Page");
                GetWebDriver().Navigate().Refresh();
                system_PicklistHelper.WaitForWorkAround(5000);

                executionLog.Log("InactivePickListStatuses", "Click on Inactive button");
                system_PicklistHelper.PicInactive(name);
                system_PicklistHelper.WaitForWorkAround(2000);

                executionLog.Log("InactivePickListStatuses", "Replace Picklist");
                system_PicklistHelper.SelectText("ReplacePiclist", "New");
                system_PicklistHelper.WaitForWorkAround(2000);

                executionLog.Log("InactivePickListStatuses", "Picklist SaveBtn Replace");
                system_PicklistHelper.ClickElement("PicklistSaveBtnReplace");

                executionLog.Log("InactivePickListStatuses", "Accep Alert message.");
                system_PicklistHelper.AcceptAlert();

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("InactivePickListStatuses");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Inactive PickList Statuses");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Inactive PickList Statuses", "Bug", "Medium", "Pick List page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Inactive PickList Statuses");
                        TakeScreenshot("InactivePickListStatuses");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\InactivePickListStatuses.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("InactivePickListStatuses");
                        string id = loginHelper.getIssueID("Inactive PickList Statuses");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\InactivePickListStatuses.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Inactive PickList Statuses"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Inactive PickList Statuses");
        //        executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("InactivePickListStatuses");
                executionLog.WriteInExcel("Inactive PickList Statuses", Status, JIRA, "Office Systems");
            }
        }
    }
}