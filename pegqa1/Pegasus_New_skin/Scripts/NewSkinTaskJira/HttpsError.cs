using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class HttpsError : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS5")]
        [TestCategory("NewSkinTaskJira")]
        public void httpsError()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());

            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("HttpsError", "Go to mypegasus.com");
                GetWebDriver().Navigate().GoToUrl("https://www.mypegasuscrm.com/login");

                executionLog.Log("HttpsError", "Veify title");
                VerifyTitle("Login");

                executionLog.Log("HttpsError", "Verify no error displayed");
                loginHelper.VerifyTextNotPresent("Your connection is not private");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("HttpsError");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Https Error");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Https Error", "Bug", "Medium", "Login page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Https Error");
                        TakeScreenshot("HttpsError");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\HttpsError.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("HttpsError");
                        string id = loginHelper.getIssueID("Https Error");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\HttpsError.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Https Error"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Https Error");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("HttpsError");
                executionLog.WriteInExcel("Https Error", Status, JIRA, "Office");
            }
        }
    }
}