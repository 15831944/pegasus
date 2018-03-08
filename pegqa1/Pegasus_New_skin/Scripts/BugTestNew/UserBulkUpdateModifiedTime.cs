using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class UserBulkUpdateModifiedTime : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS3")]
        [TestCategory("BugTestNew")]
        public void userBulkUpdateModifiedTime()
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
            var office_UserHelper = new Office_UserHelper(GetWebDriver());

            // Variable
            var DocName = "Test Exe" + GetRandomNumber();
            var fileUpl = GetPathToFile() + "chrome.exe";
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("UserBulkUpdateModifiedTime", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("UserBulkUpdateModifiedTime", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("UserBulkUpdateModifiedTime", "Redirect at users page.");
                VisitOffice("users");
                office_UserHelper.WaitForWorkAround(3000);

                //executionLog.Log("UserBulkUpdateModifiedTime", "Wait for locator to be present.");
                //office_UserHelper.WaitForElementPresent("Status", 4);

                executionLog.Log("UserBulkUpdateModifiedTime", "Select status as All");
                office_UserHelper.SelectByText("SelectStatus1", "All");
                office_UserHelper.WaitForWorkAround(5000);

                executionLog.Log("UserBulkUpdateModifiedTime", "Select user type as partner agent.");
                office_UserHelper.SelectByText("SearchUserType", "Partner Agent");
                office_UserHelper.WaitForWorkAround(3000);

                executionLog.Log("UserBulkUpdateModifiedTime", "Click on any partner agent.");
                office_UserHelper.ClickElement("ClickUser");
                office_UserHelper.WaitForWorkAround(3000);

                //executionLog.Log("UserBulkUpdateModifiedTime", "Wait for locator to be present.");
                //office_UserHelper.WaitForElementPresent("ViewButton", 10);

                executionLog.Log("UserBulkUpdateModifiedTime", "Verify view button displayed on the page.");
                office_UserHelper.IsElementVisible("//a[text()='View']");

                executionLog.Log("UserBulkUpdateModifiedTime", "Redirect to users page.");
                VisitOffice("users");
                office_UserHelper.WaitForWorkAround(3000);

                //executionLog.Log("UserBulkUpdateModifiedTime", "Wait for locator to be present.");
                //office_UserHelper.WaitForElementPresent("Status", 10);

                executionLog.Log("UserBulkUpdateModifiedTime", "Select status as Verify");
                office_UserHelper.SelectByText("SelectStatus1", "Verify");
                office_UserHelper.WaitForWorkAround(5000);

                executionLog.Log("UserBulkUpdateModifiedTime", "Select user type as partner association.");
                office_UserHelper.SelectByText("SearchUserType", "Partner Agent");
                office_UserHelper.WaitForWorkAround(3000);

                executionLog.Log("UserBulkUpdateModifiedTime", "Click on any partner association.");
                office_UserHelper.ClickElement("ClickUser");
                office_UserHelper.WaitForWorkAround(3000);

                //executionLog.Log("UserBulkUpdateModifiedTime", "Wait for locator to be present.");
                //office_UserHelper.WaitForElementPresent("ViewButton", 10);

                executionLog.Log("UserBulkUpdateModifiedTime", "Verify view button displayed on the page.");
                office_UserHelper.IsElementVisible("//a[text()='View']");

                executionLog.Log("UserBulkUpdateModifiedTime", "Logout from the application.");
                VisitOffice("logout");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("UserBulkUpdateModifiedTime");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("User Bulk Update Modified Time");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("User Bulk Update Modified Time", "Bug", "Medium", "Opportunities page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("User Bulk Update Modified Time");
                        TakeScreenshot("UserBulkUpdateModifiedTime");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\UserBulkUpdateModifiedTime.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("UserBulkUpdateModifiedTime");
                        string id = loginHelper.getIssueID("User Bulk Update Modified Time");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\UserBulkUpdateModifiedTime.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("User Bulk Update Modified Time"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("User Bulk Update Modified Time");
         //       executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("UserBulkUpdateModifiedTime");
                executionLog.WriteInExcel("User Bulk Update Modified Time", Status, JIRA, "Opportunities Management");
            }
        }
    }
}