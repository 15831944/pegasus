using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class ResetPasswordLinkIssue : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        public void resetPasswordLinkIssue()
        {

            string[] username = null;
            string[] password = null;


            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username2");
            password = oXMLData.getData("settings/Credentials", "password2");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var corp_Office_OfficeHelper = new CorpOffice_OfficeHelper(GetWebDriver());

            // Variable random
            var Office = "Test" + GetRandomNumber();
            var User = "User" + GetRandomNumber();
            var Email = "addressTest" + RandomNumber(1, 99) + "@yop.com";
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("ResetPasswordLinkIssue", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("ResetPasswordLinkIssue", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("ResetPasswordLinkIssue", "Redirect at Craete Office page");
                VisitCorp("offices");

                executionLog.Log("ResetPasswordLinkIssue", "Verify page title");
                VerifyTitle("Offices");

                executionLog.Log("ResetPasswordLinkIssue", "Click on any office");
                corp_Office_OfficeHelper.ClickElement("ClickOnAnyCorp");
                corp_Office_OfficeHelper.WaitForWorkAround(3000);

                executionLog.Log("ResetPasswordLinkIssue", "Click on reset password link.");
                corp_Office_OfficeHelper.ClickElement("ResetPassword");
                corp_Office_OfficeHelper.WaitForWorkAround(3000);

                executionLog.Log("ResetPasswordLinkIssue", "Verify success text for link sent.");
                corp_Office_OfficeHelper.VerifyPageText("Reset password link sent to");
                corp_Office_OfficeHelper.WaitForWorkAround(3000);

                executionLog.Log("ResetPasswordLinkIssue", "Verify error message not present on page");
                corp_Office_OfficeHelper.VerifyTextNot("Error while sending mail.");
                corp_Office_OfficeHelper.WaitForWorkAround(3000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("ResetPasswordLinkIssue");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Reset Password Link Issue");
                if (!result)
                {
                    if (Int16.Parse(counter) < 5)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Reset Password Link Issue", "Bug", "Medium", "Office corp", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Reset Password Link Issue");
                        TakeScreenshot("ResetPasswordLinkIssue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ResetPasswordLinkIssue.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 5)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("ResetPasswordLinkIssue");
                        string id = loginHelper.getIssueID("Reset Password Link Issue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ResetPasswordLinkIssue.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Reset Password Link Issue"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Reset Password Link Issue");
                executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("ResetPasswordLinkIssue");
                executionLog.WriteInExcel("Reset Password Link Issue", Status, JIRA, "Corp Office");
            }
        }
    }
}