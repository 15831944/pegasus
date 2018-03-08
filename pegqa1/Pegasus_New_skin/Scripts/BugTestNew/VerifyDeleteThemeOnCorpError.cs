using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class VerifyDeleteThemeOnCorpError : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("BugTestNew")]
        public void verifyDeleteThemeOnCorpError()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_corp");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var corpSystem_ThemesHelper = new CorpSystem_ThemesHelper(GetWebDriver());


            // Variable
            var ThemeName = "Test" + GetRandomNumber();
            var LastName = "Tester" + GetRandomNumber();
            var Number = "12345678" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("VerifyDeleteThemeOnCorpError", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyDeleteThemeOnCorpError", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyDeleteThemeOnCorpError", "Go to Themes page");
                VisitCorp("themes");
                corpSystem_ThemesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyDeleteThemeOnCorpError", "Search theme");
                corpSystem_ThemesHelper.TypeText("SearchTheme", "Pegasus New");
                corpSystem_ThemesHelper.WaitForWorkAround(2000);

                var loc = "//table[@id='list1']/tbody/tr[2]/td[5]/a[3]/i";
                if (corpSystem_ThemesHelper.IsElementPresent(loc))
                {
                    executionLog.Log("VerifyDeleteThemeOnCorpError", "Click On Edit Theme Icon");
                    corpSystem_ThemesHelper.ClickElement(loc);
                    corpSystem_ThemesHelper.WaitForWorkAround(3000);

                    executionLog.Log("VerifyDeleteThemeOnCorpError", "Enter Theme name");
                    corpSystem_ThemesHelper.TypeText("ThemeName",ThemeName );

                    executionLog.Log("VerifyDeleteThemeOnCorpError", "Click on Save as New");
                    corpSystem_ThemesHelper.ClickElement("SaveAsNew");
                    corpSystem_ThemesHelper.WaitForWorkAround(3000);

                    executionLog.Log("VerifyDeleteThemeOnCorpError", "Verify creation Validation");
                    corpSystem_ThemesHelper.WaitForText("Theme Configuration has been updated.", 05);
                    corpSystem_ThemesHelper.WaitForWorkAround(2000);

                    executionLog.Log("VerifyDeleteThemeOnCorpError", "Search created theme");
                    corpSystem_ThemesHelper.TypeText("SearchTheme", ThemeName);
                    corpSystem_ThemesHelper.WaitForWorkAround(2000);

                    executionLog.Log("VerifyDeleteThemeOnCorpError", "Delete created theme");
                    corpSystem_ThemesHelper.ClickElement("DeleteNormal");
                    corpSystem_ThemesHelper.AcceptAlert();
                    corpSystem_ThemesHelper.WaitForWorkAround(2000);

                    executionLog.Log("VerifyDeleteThemeOnCorpError", "Verify deletion Validation");
                    corpSystem_ThemesHelper.WaitForText("Theme Permanently Deleted.", 10);
                }
                else
                {
                    executionLog.Log("VerifyDeleteThemeOnCorpError", "Click On Edit Theme Icon");
                    corpSystem_ThemesHelper.Click("//table[@id='list1']/tbody/tr[2]/td[5]/a/i");
                    corpSystem_ThemesHelper.WaitForWorkAround(3000);

                    executionLog.Log("VerifyDeleteThemeOnCorpError", "Enter Theme name");
                    corpSystem_ThemesHelper.TypeText("ThemeName", ThemeName);

                    executionLog.Log("VerifyDeleteThemeOnCorpError", "Click on Save as New");
                    corpSystem_ThemesHelper.ClickElement("SaveAsNew");
                    corpSystem_ThemesHelper.WaitForWorkAround(3000);

                    executionLog.Log("VerifyDeleteThemeOnCorpError", "Verify creation Validation");
                    corpSystem_ThemesHelper.WaitForText("Theme Configuration has been updated.", 05);
                    corpSystem_ThemesHelper.WaitForWorkAround(2000);

                    executionLog.Log("VerifyDeleteThemeOnCorpError", "Search created theme");
                    corpSystem_ThemesHelper.TypeText("SearchTheme", ThemeName);
                    corpSystem_ThemesHelper.WaitForWorkAround(2000);

                    executionLog.Log("VerifyDeleteThemeOnCorpError", "Delete created theme");
                    corpSystem_ThemesHelper.ClickElement("DeleteNormal");
                    corpSystem_ThemesHelper.AcceptAlert();
                    corpSystem_ThemesHelper.WaitForWorkAround(2000);

                    executionLog.Log("VerifyDeleteThemeOnCorpError", "Verify deletion Validation");
                    corpSystem_ThemesHelper.WaitForText("Theme Permanently Deleted.", 10);

                }
            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyDeleteThemeOnCorpError");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Delete Theme On Corp Error");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Delete Theme On Corp Error", "Bug", "Medium", "Corp System Theme page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Delete Theme On Corp Error");
                        TakeScreenshot("VerifyDeleteThemeOnCorpError");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyDeleteThemeOnCorpError.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyDeleteThemeOnCorpError");
                        string id = loginHelper.getIssueID("Verify Delete Theme On Corp Error");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyDeleteThemeOnCorpError.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Delete Theme On Corp Error"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Delete Theme On Corp Error");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyDeleteThemeOnCorpError");
                executionLog.WriteInExcel("Verify Delete Theme On Corp Error", Status, JIRA, "Corp System Themes");
            }
        }
    }
}