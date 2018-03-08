using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class ProfilePicUplaodField : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("Temp")]
        [TestCategory("TS5")]
        [TestCategory("NewSkinTaskJira")]
        public void profilePicUplaodField()
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
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("ProfilePicUplaodField", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("ProfilePicUplaodField", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("ProfilePicUplaodField", "Go to Edit profile page");
                VisitOffice("editProfile");

                executionLog.Log("ProfilePicUplaodField", "Verify page title.");
                VerifyTitle("Edit Profile");

                executionLog.Log("ProfilePicUplaodField", "Verify upload file field availabe");
                Assert.IsTrue(loginHelper.IsElementVisible("//*[@id='EmployeeProfileImage']"));

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("ProfilePicUplaodField");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Profile Pic Uplaod Field");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Profile Pic Uplaod Field", "Bug", "Medium", "Profile page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Profile Pic Uplaod Field");
                        TakeScreenshot("ProfilePicUplaodField");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ProfilePicUplaodField.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("ProfilePicUplaodField");
                        string id = loginHelper.getIssueID("Profile Pic Uplaod Field");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ProfilePicUplaodField.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Profile Pic Uplaod Field"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Profile Pic Uplaod Field");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("ProfilePicUplaodField");
                executionLog.WriteInExcel("Profile Pic Uplaod Field", Status, JIRA,"My Profile");
            }
        }
    }
}