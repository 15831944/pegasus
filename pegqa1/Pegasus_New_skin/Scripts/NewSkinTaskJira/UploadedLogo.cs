using System;
using System.IO;
using LinqToExcel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class UploadedLogo : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS5")]
        [TestCategory("NewSkinTaskJira")]
        public void uploadedLogo()
        {
            string[] username = null;
            string[] password = null;
            string[] log = null;
            String JIRA = "";
            String Status = "Pass";

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_corp");
            password = oXMLData.getData("settings/Credentials", "password");
            log = oXMLData.getData("settings/URL", "logout");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var corp_ProfileHelper = new Corp_ProfileHelper(GetWebDriver());

            try
            {
                executionLog.Log("UploadedLogo", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("UploadedLogo", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("UploadedLogo", "Click on Edit profile button");
                corp_ProfileHelper.ClickElement("CorpProfile");
                corp_ProfileHelper.WaitForWorkAround(3000);

                executionLog.Log("UploadedLogo", "Verify title");
                VerifyTitle("Edit ");
                corp_ProfileHelper.WaitForWorkAround(3000);

                executionLog.Log("UploadedLogo", "Upload image");
                string path = GetPathToFile() + "index2.png";
                corp_ProfileHelper.UploadImage("UploadLogo", path);
                corp_ProfileHelper.WaitForWorkAround(3000);

                executionLog.Log("UploadedLogo", "Click on Save button");
                corp_ProfileHelper.ClickElement("Save");
                corp_ProfileHelper.WaitForWorkAround(5000);

                executionLog.Log("UploadedLogo", "Verify title");
                VerifyTitle("Dashboard");

                executionLog.Log("UploadedLogo", "Check default image not displayed");
                corp_ProfileHelper.verifyElementNotPresent("LogoDefault");

                executionLog.Log("UploadedLogo", "Logout from the application");
                VisitCorp("logout");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("UploadedLogo");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Uploaded Logo");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Uploaded Logo", "Bug", "Medium", "Profile Corp page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Uploaded Logo");
                        TakeScreenshot("UploadedLogo");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\UploadedLogo.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("UploadedLogo");
                        string id = loginHelper.getIssueID("Uploaded Logo");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\UploadedLogo.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Uploaded Logo"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Uploaded Logo");
               // executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("UploadedLogo");
                executionLog.WriteInExcel("Uploaded Logo", Status, JIRA, "Corp Profile");
            }
        }
    }
}