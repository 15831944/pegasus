using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class SaveThemeIssue : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS5")]
        [TestCategory("NewSkinTaskJira")]
        public void saveThemeIssue()
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
            var system_ThemesHelper = new System_ThemesHelper(GetWebDriver());

            // VARIABLE
            var Theme = "Theme" + GetRandomNumber();
            String Status = "Pass";
            String JIRA = "";
            try
            {
                executionLog.Log("SaveThemeIssue", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("SaveThemeIssue", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("SaveThemeIssue", "Redirect  To Theme");
                VisitOffice("themes");
                system_ThemesHelper.WaitForWorkAround(3000);

                executionLog.Log("SaveThemeIssue", "Verify title");
                VerifyTitle("Themes");

                executionLog.Log("SaveThemeIssue", "Enter the theme name");
                system_ThemesHelper.TypeText("EnterThemeSearch", "ThemeOffice");
                system_ThemesHelper.WaitForWorkAround(2000);

                executionLog.Log("SaveThemeIssue", "Click on edit icon");
                system_ThemesHelper.ClickElement("EditIcon");
                system_ThemesHelper.WaitForWorkAround(3000);
                                
                executionLog.Log("SaveThemeIssue", "Enter the new theme name");
                system_ThemesHelper.TypeText("ThemeName", Theme);

                executionLog.Log("SaveThemeIssue", "Click on Save as new button");
                system_ThemesHelper.ClickElement("CLickSaveButton");
                system_ThemesHelper.WaitForWorkAround(3000);

                executionLog.Log("SaveThemeIssue", "Verify theme saved");
                system_ThemesHelper.WaitForText("Theme Configuration has been updated.", 05);

                executionLog.Log("SaveThemeIssue", "Search the same themes");
                system_ThemesHelper.TypeText("EnterThemeSearch", Theme);
                system_ThemesHelper.WaitForWorkAround(2000);

                executionLog.Log("SaveThemeIssue", "Delete the theme");
                system_ThemesHelper.ClickElement("DeleteTheme");
                system_ThemesHelper.AcceptAlert();
                system_ThemesHelper.WaitForWorkAround(4000);
            }

            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";
                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("SaveThemeIssue");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Save Theme Issue");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Save Theme Issue", "Bug", "Medium", "Theme page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Save Theme Issue");
                        TakeScreenshot("SaveThemeIssue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\SaveThemeIssue.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("SaveThemeIssue");
                        string id = loginHelper.getIssueID("Save Theme Issue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\SaveThemeIssue.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Save Theme Issue"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Save Theme Issue");
                //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("SaveThemeIssue");
                executionLog.WriteInExcel("Save Theme Issue", Status, JIRA, "System Theme");
            }
        }
    }
}