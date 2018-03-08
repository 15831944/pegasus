using System;
using System.IO;
using LinqToExcel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class LoginError1 : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS5")]
        [TestCategory("NewSkinTaskJira")]
        public void loginError1()
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
            var office_MyProfileHelper = new Office_MyProfileHelper(GetWebDriver());

            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("LoginError1", "Verify title");
                VerifyTitle("Login");
                office_MyProfileHelper.WaitForWorkAround(2000);

                executionLog.Log("LoginError1", "Click on Verify my account");
                office_MyProfileHelper.ClickElement("VerifyAccount");
                office_MyProfileHelper.WaitForWorkAround(2000);

                executionLog.Log("LoginError1", "Enter username");
                office_MyProfileHelper.TypeText("VerifyUsername", "aslamKhan");
                office_MyProfileHelper.WaitForWorkAround(2000);

                executionLog.Log("LoginError1", "Click anywhere");
                office_MyProfileHelper.ClickElement("VerifyBody");
                office_MyProfileHelper.WaitForWorkAround(2000);

                executionLog.Log("LoginError1", "Refresh the page");
                RefreshPage();
                office_MyProfileHelper.WaitForWorkAround(2000);

                executionLog.Log("LoginError1", "Verify title");
                VerifyTitle("Login");
                office_MyProfileHelper.WaitForWorkAround(2000);

                executionLog.Log("LoginError1", "Click on Verify my account");
                office_MyProfileHelper.ClickElement("VerifyAccount");
                office_MyProfileHelper.WaitForWorkAround(2000);

                executionLog.Log("LoginError1", "Verify field is blank");
                office_MyProfileHelper.VerifyTextNotPresent("aslamKhan");
                office_MyProfileHelper.WaitForWorkAround(2000);

                executionLog.Log("LoginError1", "Enter username");
                office_MyProfileHelper.TypeText("VerifyUsername", "aslamKhan");
                office_MyProfileHelper.WaitForWorkAround(2000);

                executionLog.Log("LoginError1", "Click on Send email button");
                office_MyProfileHelper.ClickElement("VerifySend");
                office_MyProfileHelper.WaitForWorkAround(2000);

                executionLog.Log("LoginError1", "Wait for text");
                office_MyProfileHelper.WaitForText("Your email is already verified", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("LoginError1");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Login Error 1");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Login Error 1", "Bug", "Medium", "Login page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Login Error 1");
                        TakeScreenshot("LoginError1");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\LoginError1.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("LoginError1");
                        string id = loginHelper.getIssueID("Login Error 1");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\LoginError1.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Login Error 1"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Login Error 1");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("LoginError1");
                executionLog.WriteInExcel("Login Error 1", Status, JIRA, "Office");
            }
        }
    }
}