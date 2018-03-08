using System;
using System.IO;
using LinqToExcel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class DeletePickItemIssue : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS4")]
        [TestCategory("NewSkinTaskJira")]
        public void deletePickItemIssue()
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

            // Variables
            var Picklist = "Pick" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("DeletePickItemIssue", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("DeletePickItemIssue", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("DeletePickItemIssue", "Go to picklist page");
                VisitOffice("pick-lists");

                executionLog.Log("DeletePickItemIssue", "Verify title");
                VerifyTitle("Picklists");

                executionLog.Log("DeletePickItemIssue", " Open the Shipping picklist");
                system_PicklistsHelper.ClickElement("ShippingPick");

                executionLog.Log("DeletePickItemIssue", "Veirfy title");
                VerifyTitle("Add/Edit Picklist Items");

                executionLog.Log("DeletePickItemIssue", "Click on Add button");
                system_PicklistsHelper.ClickElement("AddPick");
                system_PicklistsHelper.WaitForWorkAround(2000);

                executionLog.Log("DeletePickItemIssue", "Enter name");
                system_PicklistsHelper.TypeText("PickType", Picklist);

                executionLog.Log("DeletePickItemIssue", "Click on Save button");
                system_PicklistsHelper.ClickElement("Save");

                executionLog.Log("DeletePickItemIssue", "Wait for text");
                //            system_PicklistsHelper.WaitForText("The picklist value is added successfully", 10);

                executionLog.Log("DeletePickItemIssue", "Veirfy title");
                VerifyTitle("Add/Edit Picklist Items");
                system_PicklistsHelper.WaitForWorkAround(3000);

                executionLog.Log("DeletePickItemIssue", "Click on Delete button");
                system_PicklistsHelper.ClickElement("Cancel");

                executionLog.Log("DeletePickItemIssue", "Logout from the application");
                VisitOffice("logout");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("DeletePickItemIssue");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Delete Pick Item Issue");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Delete Pick Item Issue", "Bug", "Medium", "Pick List page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Delete Pick Item Issue");
                        TakeScreenshot("DeletePickItemIssue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\DeletePickItemIssue.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("DeletePickItemIssue");
                        string id = loginHelper.getIssueID("Delete Pick Item Issue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\DeletePickItemIssue.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Delete Pick Item Issue"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Delete Pick Item Issue");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("DeletePickItemIssue");
                executionLog.WriteInExcel("Delete Pick Item Issue", Status, JIRA, "System Picklist");
            }
        }
    }
}
