using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class ThemeDefaultColor : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS5")]
        [TestCategory("NewSkinTaskJira")]
        public void themeDefaultColor()
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
            String Status = "Pass";
            String JIRA = "";
            try
            {
                executionLog.Log("ThemeDefaultColor", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("ThemeDefaultColor", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("ThemeDefaultColor", "Redirect  To Theme");
                VisitOffice("themes");
                system_ThemesHelper.WaitForWorkAround(3000);

                executionLog.Log("SaveThemeIssue", "Enter the theme name");
                system_ThemesHelper.TypeText("EnterThemeSearch", "ThemeOffice");
                system_ThemesHelper.WaitForWorkAround(2000);

                executionLog.Log("ThemeDefaultColor", "Click on Edit icon");
                system_ThemesHelper.ClickElement("EditIcon");
                system_ThemesHelper.WaitForWorkAround(2000);

                executionLog.Log("ThemeDefaultColor", "Verify title");
                VerifyTitle("Themes");

                executionLog.Log("ThemeDefaultColor", "Verify Defualt Color is available");
                system_ThemesHelper.verifyElementPresent("DefaultColor");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";
                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("ThemeDefaultColor");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Theme Default Color");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Edit Theme Admin", "Bug", "Medium", "Theme page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Edit Theme Admin");
                        TakeScreenshot("ThemeDefaultColor");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ThemeDefaultColor.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("ThemeDefaultColor");
                        string id = loginHelper.getIssueID("Edit Theme Admin");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ThemeDefaultColor.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Edit Theme Admin"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Edit Theme Admin");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("ThemeDefaultColor");
                executionLog.WriteInExcel("Edit Theme Admin", Status, JIRA, "System Themes");
            }
        }
    }
}