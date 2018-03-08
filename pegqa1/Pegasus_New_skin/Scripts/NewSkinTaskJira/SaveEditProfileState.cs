using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class SaveEditProfileState : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS5")]
        [TestCategory("NewSkinTaskJira")]
        public void saveEditProfileState()
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
                executionLog.Log("SaveEditProfileState", "Login with valid username and password");
                Login(username[0], password[0]);

                executionLog.Log("SaveEditProfileState", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("SaveEditProfileState", "Redirect  to my profile");
                VisitOffice("editProfile");
                office_MyProfileHelper.WaitForWorkAround(5000);

                executionLog.Log("SaveEditProfileState", "Verify Page title");
                VerifyTitle("Edit Profile");

                executionLog.Log("SaveEditProfileState", "Select Country");
                office_MyProfileHelper.SelectByText("SelectCountryMP", "Canada");
                office_MyProfileHelper.WaitForWorkAround(3000);

                executionLog.Log("SaveEditProfileState", "Select State");
                office_MyProfileHelper.SelectByText("SelectStateMP", "BC");

                executionLog.Log("SaveEditProfileState", "Click on 'Save' button");
                office_MyProfileHelper.ClickElement("Save");
                office_MyProfileHelper.WaitForWorkAround(6000);

                executionLog.Log("SaveEditProfileState", "Verify the confirmation message");
                office_MyProfileHelper.VerifyPageText("Your profile has been successfully updated.");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("SaveEditProfileState");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Save Edit Profile State");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Save Edit Profile State", "Bug", "Medium", "Profile page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Save Edit Profile State");
                        TakeScreenshot("SaveEditProfileState");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\SaveEditProfileState.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("SaveEditProfileState");
                        string id = loginHelper.getIssueID("Save Edit Profile State");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\SaveEditProfileState.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Save Edit Profile State"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Save Edit Profile State");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("SaveEditProfileState");
                executionLog.WriteInExcel("Save Edit Profile State", Status, JIRA, "My Profile");
            }
        }
    }
}
