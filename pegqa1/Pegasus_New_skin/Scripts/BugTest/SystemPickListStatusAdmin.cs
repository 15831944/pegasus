using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class SystemPickListStatusAdmin : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS1")]
        [TestCategory("BugTest")]
        public void systemPickListStatusAdmin()
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

            // Random Variables
            var Picklist = "TestPicklist" + RandomNumber(99, 99999); ;
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("SystemPickListStatusAdmin", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("SystemPickListStatusAdmin", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("SystemPickListStatusAdmin", "Redirect To Admin");
                VisitOffice("admin");

                executionLog.Log("SystemPickListStatusAdmin", "Verify title");
                VisitOffice("pick-lists");
                system_PicklistHelper.WaitForWorkAround(5000);

                executionLog.Log("SystemPickListStatusAdmin", "Enter Name To Seacrch");
                system_PicklistHelper.TypeText("EnterNamePicklist", "Statuses");
                system_PicklistHelper.WaitForWorkAround(2000);

                executionLog.Log("SystemPickListStatusAdmin", "Enter Module To Seacrch");
                system_PicklistHelper.TypeText("EnterModule", "Merchants");
                system_PicklistHelper.WaitForWorkAround(5000);

                executionLog.Log("SystemPickListStatusAdmin", "Click On Statuses");
                system_PicklistHelper.ClickElement("ClickOnStatuses");

                executionLog.Log("SystemPickListStatusAdmin", "Click on Add button");
                system_PicklistHelper.ClickElement("AddPick");
                system_PicklistHelper.WaitForWorkAround(3000);

                executionLog.Log("SystemPickListStatusAdmin", "Enter PickList Value");
                system_PicklistHelper.TypeText("EnterPickListValue", Picklist);

                executionLog.Log("SystemPickListStatusAdmin", "Click PickList Save Button");
                system_PicklistHelper.ClickElement("PickListSaveBtn");
                system_PicklistHelper.WaitForWorkAround(3000);

                executionLog.Log("EditPickListStatuses", "Click PickList Save Button");
                system_PicklistHelper.ClickElement("Cancel");
                system_PicklistHelper.WaitForWorkAround(3000);

                executionLog.Log("EditPickListStatuses", "Verify page title");
                VerifyTitle("Add/Edit Picklist Items");

                executionLog.Log("EditPickListStatuses", "Click delete Button");
                system_PicklistHelper.ClickElement("DeletePick");

                executionLog.Log("EditPickListStatuses", "Click on item to deleted");
                system_PicklistHelper.DeletePickList(Picklist);
                system_PicklistHelper.WaitForWorkAround(3000);

                executionLog.Log("EditPickListStatuses", "Selelct replace with item.");
                system_PicklistHelper.SelectText("ReplacePiclist", "Closed");
                system_PicklistHelper.WaitForWorkAround(1000);

                executionLog.Log("EditPickListStatuses", "Click PickList Save Button");
                system_PicklistHelper.ClickElement("PickListSaveBtn");
                system_PicklistHelper.WaitForWorkAround(3000);

                executionLog.Log("EditPickListStatuses", "Accept alert message.");
                system_PicklistHelper.AcceptAlert();
                system_PicklistHelper.WaitForWorkAround(4000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("SystemPickListStatusAdmin");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("System PickList Status Admin");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("System PickList Status Admin", "Bug", "Medium", "Pick list page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("System PickList Status Admin");
                        TakeScreenshot("SystemPickListStatusAdmin");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\SystemPickListStatusAdmin.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("SystemPickListStatusAdmin");
                        string id = loginHelper.getIssueID("System PickList Status Admin");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\SystemPickListStatusAdmin.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("System PickList Status Admin"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("System PickList Status Admin");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("SystemPickListStatusAdmin");
                executionLog.WriteInExcel("System PickList Status Admin", Status, JIRA, "Office Systems");
            }
        }
    }
}