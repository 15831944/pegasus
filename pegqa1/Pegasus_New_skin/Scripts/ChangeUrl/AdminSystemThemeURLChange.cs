using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class AdminSystemThemeURLChange : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Url")]
        [TestCategory("TS3")]
        [TestCategory("ChangeUrl")]
        public void adminSystemThemeURLChange()
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
            var system_ThemesHelper = new System_ThemesHelper(GetWebDriver());


            // Variable
            var FirstName = "Test" + GetRandomNumber();
            var LastName = "Tester" + GetRandomNumber();
            var Number = "12345678" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("AdminSystemThemeURLChange", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("AdminSystemThemeURLChange", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("AdminSystemThemeURLChange", "Goto User  System >> Themes");
                VisitCorp("themes");

                var loc = "//table[@id='list1']/tbody/tr[2]/td[5]/a[4]/i";
                if (system_ThemesHelper.IsElementPresent(loc))
                {
                    executionLog.Log("AdminSystemThemeURLChange", "Click On Edit Theme Icon");
                    system_ThemesHelper.ClickElement(loc);
                    system_ThemesHelper.WaitForWorkAround(1000);

                    executionLog.Log("AdminSystemThemeURLChange", "Change the url with the url number of another office");
                    VisitCorp("themes/customize/1497");
                    system_ThemesHelper.WaitForWorkAround(1000);

                    executionLog.Log("AdminSystemThemeURLChange", "Verify Validation");
                    system_ThemesHelper.WaitForText("The Theme is does not exists.", 10);
                }
                else
                {
                    executionLog.Log("AdminSystemThemeURLChange", "Click On Edit Theme Icon");
                    system_ThemesHelper.Click("//table[@id='list1']/tbody/tr[5]/td[5]/a[4]/i");
                    system_ThemesHelper.WaitForWorkAround(1000);

                    executionLog.Log("AdminSystemThemeURLChange", "Change the url with the url number of another office");
                    VisitCorp("themes/customize/1497");
                    system_ThemesHelper.WaitForWorkAround(1000);

                    executionLog.Log("AdminSystemThemeURLChange", "Verify Validation");
                    system_ThemesHelper.WaitForText("The Theme is does not exists.", 10);

                }
            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("AdminSystemThemeURLChange");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Admin System Theme URL Change");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Admin System Theme URL Change", "Bug", "Medium", "System Theme page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Admin System Theme URL Change");
                        TakeScreenshot("AdminSystemThemeURLChange");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AdminSystemThemeURLChange.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("AdminSystemThemeURLChange");
                        string id = loginHelper.getIssueID("Admin System Theme URL Change");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AdminSystemThemeURLChange.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Admin System Theme URL Change"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Admin System Theme URL Change");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("AdminSystemThemeURLChange");
                executionLog.WriteInExcel("Admin System Theme URL Change", Status, JIRA, "System Themes");
            }
        }
    }
}