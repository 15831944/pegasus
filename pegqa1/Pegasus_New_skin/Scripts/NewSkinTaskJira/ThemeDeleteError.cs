using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class ThemeDeleteError : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("Fail")]
        [TestCategory("TS5")]
        [TestCategory("NewSkinTaskJira")]
        public void themeDeleteError()
        {
            string[] username = null;
            string[] password = null;
            String JIRA = "";
            String Status = "Pass";

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


            try
            {
                executionLog.Log("ThemeDeleteError", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("ThemeDeleteError", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("ThemeDeleteError", "Redirect  To Theme");
                VisitOffice("themes");
                system_ThemesHelper.WaitForWorkAround(3000);

                executionLog.Log("ThemeDeleteError", "Verify title");
                VerifyTitle("Themes");

                executionLog.Log("ThemeDeleteError", "Search the theme");
                system_ThemesHelper.TypeText("EnterThemeSearch", "ThemeOffice");
                system_ThemesHelper.WaitForWorkAround(2000);

                var loc = "//table[@id='list1']//tr[2]//td[6]";
                if (system_ThemesHelper.IsElementPresent(loc))
                {

                    executionLog.Log("ThemeDeleteError", "Search the theme");
                    system_ThemesHelper.TypeText("EnterThemeSearch", "ThemeOffice");
                    system_ThemesHelper.WaitForWorkAround(4000);

                    executionLog.Log("ThemeDeleteError", "Click on first theme delete icon");
                    system_ThemesHelper.ClickElement("DeleteTheme");
                    system_ThemesHelper.WaitForWorkAround(5000);

                    executionLog.Log("ThemeDeleteError", "Verify Alert text");
                    system_ThemesHelper.VerifyAlertText("Are you sure want to delete this theme permanently?");
                }
                else
                {
                    VisitOffice("themes");
                    system_ThemesHelper.WaitForWorkAround(4000);

                    executionLog.Log("ThemeDeleteError", "Click on edit icon of the theme");
                    system_ThemesHelper.ClickEditIcon();
                    system_ThemesHelper.WaitForWorkAround(4000);

                    executionLog.Log("ThemeDeleteError", "Enter new theme name");
                    system_ThemesHelper.TypeText("ThemeName", "ThemeOffice");

                    executionLog.Log("ThemeDeleteError", "Click on Save as new button");
                    system_ThemesHelper.ClickElement("CLickSaveButton");

                    executionLog.Log("ThemeDeleteError", "Verify theme saved");
                    system_ThemesHelper.WaitForText("Theme Configuration has been updated.", 10);

                    executionLog.Log("ThemeDeleteError", "Click on first theme delete icon");
                    system_ThemesHelper.ClickElement("DeleteTheme");
                    system_ThemesHelper.WaitForWorkAround(3000);

                    executionLog.Log("ThemeDeleteError", "Verify Alert text");
                    system_ThemesHelper.VerifyAlertText("Are you sure want to delete this theme permanently?");
                }
            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("ThemeDeleteError");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Theme Delete Error");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Theme Delete Error", "Bug", "Medium", "Theme page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Theme Delete Error");
                        TakeScreenshot("ThemeDeleteError");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ThemeDeleteError.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("ThemeDeleteError");
                        string id = loginHelper.getIssueID("Theme Delete Error");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ThemeDeleteError.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Theme Delete Error"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Theme Delete Error");
           //     executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("ThemeDeleteError");
                executionLog.WriteInExcel("Theme Delete Error", Status, JIRA, "System themes");
            }
        }
    }
}