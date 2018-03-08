using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class EditThemeAdmin : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("Fail")]
        [TestCategory("TS4")]
        [TestCategory("NewSkinTaskJira")]
        public void editThemeAdmin()
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
            var name = "Testtheme" + GetRandomNumber();
            String Status = "Pass";
            String JIRA = "";

            try
            {
                executionLog.Log("EditThemeAdmin", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("EditThemeAdmin", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("EditThemeAdmin", "Redirect to admin");
                VisitOffice("admin");

                executionLog.Log("EditThemeAdmin", "Redirect  To Theme");
                VisitOffice("themes");
                system_ThemesHelper.WaitForWorkAround(6000);

                executionLog.Log("EditThemeAdmin", "Search the theme");
                system_ThemesHelper.TypeText("EnterThemeSearch", "Pegasus New");
                system_ThemesHelper.WaitForWorkAround(5000);

                executionLog.Log("EditThemeAdmin", "Click on Edit icon");
                system_ThemesHelper.ClickElement("EditIconTheme");
                system_ThemesHelper.WaitForWorkAround(4000);

                executionLog.Log("EditThemeAdmin", "Enter the name of theme");
                system_ThemesHelper.TypeText("ThemeName", name);
                system_ThemesHelper.WaitForWorkAround(2000);

                executionLog.Log("EditThemeAdmin", "click on save button");
                system_ThemesHelper.ClickElement("CLickSaveButton");
                system_ThemesHelper.WaitForWorkAround(7000);

                executionLog.Log("EditThemeAdmin", "Search the same theme");
                system_ThemesHelper.TypeText("EnterThemeSearch", name);
                system_ThemesHelper.WaitForWorkAround(5000);

                executionLog.Log("EditThemeAdmin", "click on edit icon");
                system_ThemesHelper.ClickElement("EditIcon");
                system_ThemesHelper.WaitForWorkAround(1000);

                executionLog.Log("EditThemeAdmin", "SelectFontSize");
                system_ThemesHelper.SelectByText("SelectFont", "10px");
                system_ThemesHelper.WaitForWorkAround(1000);

                executionLog.Log("EditThemeAdmin", "CLickSaveButton");
                system_ThemesHelper.ClickElement("SaveBtn");
                system_ThemesHelper.WaitForWorkAround(2000);

                executionLog.Log("EditThemeAdmin", "Accept alert message.");
                system_ThemesHelper.AcceptAlert();
                system_ThemesHelper.WaitForWorkAround(5000);

                executionLog.Log("EditThemeAdmin", "search the name");
                system_ThemesHelper.TypeText("EnterThemeSearch", name);
                system_ThemesHelper.WaitForWorkAround(3000);

                executionLog.Log("EditThemeAdmin", "CLickSaveButton");
                system_ThemesHelper.ClickElement("DeleteIcon");
                system_ThemesHelper.AcceptAlert();
                system_ThemesHelper.WaitForWorkAround(3000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";
                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("EditThemeAdmin");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Edit Theme Admin");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Edit Theme Admin", "Bug", "Medium", "Theme page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Edit Theme Admin");
                        TakeScreenshot("EditThemeAdmin");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditThemeAdmin.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("EditThemeAdmin");
                        string id = loginHelper.getIssueID("Edit Theme Admin");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditThemeAdmin.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Edit Theme Admin"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Edit Theme Admin");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("EditThemeAdmin");
                executionLog.WriteInExcel("Edit Theme Admin", Status, JIRA, "System Themes");
            }
        }
    }
}