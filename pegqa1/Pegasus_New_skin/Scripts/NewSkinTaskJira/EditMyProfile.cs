using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class EditMyProfile : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS4")]
        [TestCategory("NewSkinTaskJira")]
        public void editMyProfile()
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
            var office_MyProfileHelper = new Office_MyProfileHelper(GetWebDriver());

            // VARIABLE
            String Status = "Pass";
            String JIRA = "";
            try
            {
                executionLog.Log("EditMyProfile", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("EditMyProfile", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("EditMyProfile", "Redirect to my profile");
                VisitOffice("myprofile");
                office_MyProfileHelper.WaitForWorkAround(5000);

                executionLog.Log("EditMyProfile", "Click On EditProfile");
                office_MyProfileHelper.ClickElement("EditProfile");
                office_MyProfileHelper.WaitForWorkAround(5000);

                executionLog.Log("EditMyProfile", "Enter Zip Code");
                office_MyProfileHelper.TypeText("ZipCode", "60601");
                office_MyProfileHelper.WaitForWorkAround(3000);

                executionLog.Log("EditMyProfile", "Click on save button");
                office_MyProfileHelper.ClickElement("Save");

                executionLog.Log("EditMyProfile", "Verify Your profile has been successfully updated. ");
                office_MyProfileHelper.WaitForText("Your profile has been successfully updated.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";
                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("EditMyProfile");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Edit My Profile");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Edit My Profile", "Bug", "Medium", "Profile page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Edit My Profile");
                        TakeScreenshot("EditMyProfile");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditMyProfile.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("EditMyProfile");
                        string id = loginHelper.getIssueID("Edit My Profile");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditMyProfile.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Edit My Profile"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Edit My Profile");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("EditMyProfile");
                executionLog.WriteInExcel("Edit My Profile", Status, JIRA, "My Profile");
            }
        }
    }
}