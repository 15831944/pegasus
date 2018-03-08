using System;
using System.IO;
using LinqToExcel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class LastLoginTime : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS5")]
        [TestCategory("NewSkinTaskJira")]
        public void lastLoginTime()
        {
            string[] username = null;
            string[] password = null;
            string[] log = null;
            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");
            log = oXMLData.getData("settings/URL", "logout");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var office_UserHelper = new Office_UserHelper(GetWebDriver());

            // Variables
            //var lastlogon = office_UserHelper.GetData("LastLogon");
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("LastLoginTime", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("LastLoginTime", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("LastLoginTime", "Go to User user page");
                VisitOffice("users");              
               
                executionLog.Log("LastLoginTime", "Logout from application");
                VisitOffice("logout");

                executionLog.Log("LastLoginTime", "Wait for 1 minute");
                office_UserHelper.WaitForWorkAround(60000);

                executionLog.Log("LastLoginTime", "Login with valid username and password");
                Login(username[0], password[0]);

                executionLog.Log("LastLoginTime", "Verify title dashboard.");
                VerifyTitle("Dashboard");

                executionLog.Log("LastLoginTime", "Go to User user page");
                VisitOffice("users");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("LastLoginTime");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Last Login Time");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Last Login Time", "Bug", "Medium", "User page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Last Login Time");
                        TakeScreenshot("LastLoginTime");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\LastLoginTime.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("LastLoginTime");
                        string id = loginHelper.getIssueID("Last Login Time");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\LastLoginTime.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Last Login Time"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Last Login Time");
                //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("LastLoginTime");
                executionLog.WriteInExcel("Last Login Time", Status, JIRA, "Office");
            }
        }
    }
}