using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class CancelEditProfile : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS4")]
        [TestCategory("NewSkinTaskJira")]
        public void cancelEditProfile()
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
            var name = "TestEmployee" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("CancelEditProfile", "Login with valid username and password");
                Login(username[0], password[0]);

                executionLog.Log("CancelEditProfile", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("CancelEditProfile", "Redirect  to my profile");
                VisitOffice("myprofile");

                executionLog.Log("CancelEditProfile", "Click On EditProfile");
                office_MyProfileHelper.ClickElement("EditProfile");
                office_MyProfileHelper.WaitForWorkAround(2000);

                executionLog.Log("CancelEditProfile", "Click on camcel button.");
                office_MyProfileHelper.ClickElement("Cancel");
                office_MyProfileHelper.WaitForWorkAround(4000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CancelEditProfile");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Cancel Edit Profile");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Cancel Edit Profile", "Bug", "Medium", "Profile page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Cancel Edit Profile");
                        TakeScreenshot("CancelEditProfile");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CancelEditProfile.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CancelEditProfile");
                        string id = loginHelper.getIssueID("Cancel Edit Profile");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CancelEditProfile.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Cancel Edit Profile"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Cancel Edit Profile");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("CancelEditProfile");
                executionLog.WriteInExcel("Cancel Edit Profile", Status, JIRA,"My profile");
            }
        }
    }
}
  