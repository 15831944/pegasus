using System;
using System.IO;
using LinqToExcel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;


namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class AddressTypePickList : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS4")]
        [TestCategory("NewSkinTaskJira")]
        public void addressTypePickList()
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
            var office_MyProfileHelper = new Office_MyProfileHelper(GetWebDriver());
            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("AddressTypePickList", "Login with valid username and password");
                Login(username[0], password[0]);

                executionLog.Log("AddressTypePickList", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("AddressTypePickList", "Go to profile page");
                VisitOffice("myprofile");

                executionLog.Log("AddressTypePickList", "Verify title");
                VerifyTitle("My Profile");

                executionLog.Log("AddressTypePickList", "Click on 'Edit' profile button");
                office_MyProfileHelper.ClickElement("AddressEdit");

                executionLog.Log("AddressTypePickList", "Verify Title");
                VerifyTitle("Edit Profile");

                executionLog.Log("AddressTypePickList", "Verify Address type not a pick list");
                office_MyProfileHelper.verifyElementPresent("AddressTypeDropdown");

                executionLog.Log("AddressTypePickList", "Log out from the application");
                VisitOffice("logout");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("AddressTypePickList");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Address Type Pick List");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Address Type Pick List", "Bug", "Medium", "Profile page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Address Type Pick List");
                        TakeScreenshot("AddressTypePickList");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AddressTypePickList.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("AddressTypePickList");
                        string id = loginHelper.getIssueID("Address Type Pick List");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AddressTypePickList.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Address Type Pick List"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Address Type Pick List");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("AddressTypePickList");
                executionLog.WriteInExcel("Address Type Pick List", Status, JIRA, "My Profile");
            }
        }
    }
}