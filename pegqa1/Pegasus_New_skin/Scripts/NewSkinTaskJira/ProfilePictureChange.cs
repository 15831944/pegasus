using System;
using System.IO;
using LinqToExcel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class ProfilePictureChange : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS5")]
        [TestCategory("NewSkinTaskJira")]
        public void profilePictureChange()
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
                executionLog.Log("ProfilePictureChange", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("ProfilePictureChange", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("ProfilePictureChange", "Go to Profile page");
                VisitOffice("myprofile");
                office_MyProfileHelper.WaitForWorkAround(6000);

                executionLog.Log("ProfilePictureChange", "Verify title");
                VerifyTitle("My Profile");

                executionLog.Log("ProfilePictureChange", "Click on Edit profile button");
                office_MyProfileHelper.ClickElement("EditProfile");
                office_MyProfileHelper.WaitForWorkAround(4000);

                executionLog.Log("ProfilePictureChange", "Verify title");
                VerifyTitle("Edit Profile");

                executionLog.Log("ProfilePictureChange", "Upload image");
                string path = GetPathToFile() + "index.jpg";
                office_MyProfileHelper.UploadImage("UploadImage", path);

                executionLog.Log("ProfilePictureChange", "Click on Save button");
                office_MyProfileHelper.ClickElement("Save");
                office_MyProfileHelper.WaitForWorkAround(6000);

                executionLog.Log("ProfilePictureChange", "Verify title");
                VerifyTitle("My Profile");

                executionLog.Log("ProfilePictureChange", "Check default image not displayed");
                office_MyProfileHelper.verifyElementNotPresent("ProfileDefault");

                executionLog.Log("ProfilePictureChange", "Logout from the application");
                VisitOffice("logout");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("ProfilePictureChange");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Profile Picture Change");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Profile Picture Change", "Bug", "Medium", "My Profile page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Profile Picture Change");
                        TakeScreenshot("ProfilePictureChange");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ProfilePictureChange.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("ProfilePictureChange");
                        string id = loginHelper.getIssueID("Profile Picture Change");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ProfilePictureChange.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Profile Picture Change"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Profile Picture Change");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("ProfilePictureChange");
                executionLog.WriteInExcel("Profile Picture Change", Status, JIRA, "My Profile");
            }
        }
    }
}