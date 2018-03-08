using System;
using System.IO;
using LinqToExcel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class PickListPopup : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS5")]
        [TestCategory("NewSkinTaskJira")]
        public void pickListPopup()
        {
            string[] username = null;
            string[] password = null;
            string[] log = null;
            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");
            log = oXMLData.getData("settings/URL", "logout");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var system_PicklistsHelper = new System_PicklistsHelper(GetWebDriver());


            // Variable
            var Picklist = "Pick" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("PickListPopup", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("PickListPopup", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("PickListPopup", "Go to picklist page");
                VisitOffice("pick-lists");
                system_PicklistsHelper.WaitForWorkAround(3000);

                executionLog.Log("PickListPopup", "Verify title");
                VerifyTitle("Picklists");

                executionLog.Log("PickListPopup", " Open the first picklist");
                system_PicklistsHelper.ClickElement("EditPick");
                system_PicklistsHelper.WaitForWorkAround(3000);

                executionLog.Log("PickListPopup", "Veirfy title");
                VerifyTitle("Add/Edit Picklist Items");

                executionLog.Log("PickListPopup", "Click on Add button");
                system_PicklistsHelper.ClickElement("AddPick");
                system_PicklistsHelper.WaitForWorkAround(2000);

                executionLog.Log("PickListPopup", "Enter pick list name");
                system_PicklistsHelper.TypeText("PickType", Picklist);

                executionLog.Log("PickListPopup", "Click on Save button");
                system_PicklistsHelper.ClickElement("Save");

                executionLog.Log("PickListPopup", "Wait for text");
                //       system_PicklistsHelper.WaitForText("The picklist value is added successfully", 10);

                executionLog.Log("PickListPopup", "Verify 'Add Another message' is present");
                system_PicklistsHelper.verifyElementPresent("AddAnoth");

                executionLog.Log("PickListPopup", "Click on Cancel");
                system_PicklistsHelper.ClickElement("Cancel");
                system_PicklistsHelper.WaitForWorkAround(2000);

                executionLog.Log("PickListPopup", "Verfiy Text");
                system_PicklistsHelper.VerifyPageText(Picklist);

                executionLog.Log("PickListPopup", "Click delete Button");
                system_PicklistsHelper.ClickElement("DeletePick");
                system_PicklistsHelper.WaitForWorkAround(3000);

                executionLog.Log("PickListPopup", "Click on item to deleted");
                system_PicklistsHelper.DeletePickList(Picklist);

                executionLog.Log("PickListPopup", "Selelct replace with item.");
                system_PicklistsHelper.SelectText("ReplacePiclist", "Other");

                executionLog.Log("PickListPopup", "Click PickList Save Button");
                system_PicklistsHelper.ClickElement("PickListSaveBtn");

                executionLog.Log("PickListPopup", "Accept alert message.");
                system_PicklistsHelper.AcceptAlert();
                system_PicklistsHelper.WaitForWorkAround(3000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("PickListPopup");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Pick List Popup");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Pick List Popup", "Bug", "Medium", "Pick List page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Pick List Popup");
                        TakeScreenshot("PickListPopup");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\PickListPopup.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("PickListPopup");
                        string id = loginHelper.getIssueID("Pick List Popup");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\PickListPopup.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Pick List Popup"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Pick List Popup");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("PickListPopup");
                executionLog.WriteInExcel("Pick List Popup", Status, JIRA, "System");
            }
        }
    }
}