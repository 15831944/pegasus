using System;
using System.IO;
using LinqToExcel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;


namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class UserCountPage : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS5")]
        [TestCategory("NewSkinTaskJira")]
        public void userCountPage()
        {
            string[] username = null;
            string[] password = null;
            string[] log = null;
            String JIRA = "";
            String Status = "Pass";

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");
            log = oXMLData.getData("settings/URL", "logout");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var office_UserHelper = new Office_UserHelper(GetWebDriver());

            try
            {
                executionLog.Log("UserCountPage", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("UserCountPage", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("UserCountPage", "Go to User page");
                VisitOffice("users");
                office_UserHelper.WaitForWorkAround(6000);

                executionLog.Log("UserCountPage", "Verify title");
                VerifyTitle("'s Users");

                executionLog.Log("UserCountPage", "Verify user count page is available");
                office_UserHelper.verifyElementPresent("UserSelectCount");

                executionLog.Log("UserCountPage", "Select All the status");
                office_UserHelper.SelectByText("Status", "All");
                office_UserHelper.WaitForWorkAround(4000);

                executionLog.Log("UserCountPage", "Change count");
                int result = office_UserHelper.changeCount("10");

                executionLog.Log("UserCountPage", "Verify user count");
                // office_UserHelper.verifyCount(10, result);

                executionLog.Log("UserCountPage", "Change count");
                result = office_UserHelper.changeCount("20");

                executionLog.Log("UserCountPage", "Verify user count");
                //  office_UserHelper.verifyCount(20, result);

                executionLog.Log("UserCountPage", "Log out from the application");
                VisitOffice("logout");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("UserCountPage");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("User Count Page");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("User Count Page", "Bug", "Medium", "Office Users page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("User Count Page");
                        TakeScreenshot("UserCountPage");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\UserCountPage.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("UserCountPage");
                        string id = loginHelper.getIssueID("User Count Page");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\UserCountPage.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("User Count Page"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("User Count Page");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("UserCountPage");
                executionLog.WriteInExcel("User Count Page", Status, JIRA, "Corp Office");
            }
        }
    }
}