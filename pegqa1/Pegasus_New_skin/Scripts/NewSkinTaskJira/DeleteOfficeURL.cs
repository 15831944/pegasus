using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class DeleteOfficeURL : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS4")]
        [TestCategory("NewSkinTaskJira")]
        public void deleteOfficeURL()
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
            var corpOffice_OfficeHelper = new CorpOffice_OfficeHelper(GetWebDriver());

            // Variable random
            var usernme = "TESTUSER" + RandomNumber(44, 777);
            var name = "Test" + RandomNumber(99, 999);
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("DeleteOfficeURL", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("DeleteOfficeURL", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("DeleteOfficeURL", "Redirect at offices page.");
                VisitCorp("offices");

                executionLog.Log("DeleteOfficeURL", "Verify title");
                VerifyTitle("Offices");

                executionLog.Log("DeleteOfficeURL", "Try to delete an office via URL");
                VisitCorp("offices/delete/1");

                executionLog.Log("DeleteOfficeURL", "Verify User get priviledge message");
                corpOffice_OfficeHelper.WaitForText("You don't have privileges to delete this office.", 10);

                executionLog.Log("DeleteOfficeURL", "Verify title");
                VerifyTitle("Offices");

                executionLog.Log("DeleteOfficeURL", "Try to delete an office via URL");
                VisitCorp("offices/delete/2");

                executionLog.Log("DeleteOfficeURL", "Verify User get privilage message");
                corpOffice_OfficeHelper.WaitForText("You don't have privileges to delete this office.", 10);

                executionLog.Log("DeleteOfficeURL", "Verify title");
                VerifyTitle("Offices");

                executionLog.Log("DeleteOfficeURL", "Try to delete an office via URL");
                VisitCorp("offices/delete/3");

                executionLog.Log("DeleteOfficeURL", "Verify User get privilage message");
                corpOffice_OfficeHelper.WaitForText("You don't have privileges to delete this office.", 10);

            }

            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("DeleteOfficeURL");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Delete Office URL");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Delete Office URL", "Bug", "Medium", "Office page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Delete Office URL");
                        TakeScreenshot("DeleteOfficeURL");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\DeleteOfficeURL.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("DeleteOfficeURL");
                        string id = loginHelper.getIssueID("Delete Office URL");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\DeleteOfficeURL.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Delete Office URL"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Delete Office URL");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("DeleteOfficeURL");
                executionLog.WriteInExcel("Delete Office URL", Status, JIRA, "Corp Office");
            }
        }
    }
}