using System;
using System.IO;
using LinqToExcel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class DeleteShaEmployee : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS4")]
        [TestCategory("NewSkinTaskJira")]
        public void deleteShaEmployee()
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

            String Status = "Pass";
            String JIRA = "";

            try
            {
                executionLog.Log("DeleteShaEmployee", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("DeleteShaEmployee", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("DeleteShaEmployee", "Go to User page");
                VisitOffice("users");
                office_UserHelper.WaitForWorkAround(3000);

                executionLog.Log("DeleteShaEmployee", "Select User Type");
                office_UserHelper.SelectByText("SearchUserType", "Employee");
                office_UserHelper.WaitForWorkAround(2000);

                executionLog.Log("DeleteShaEmployee", "Enter User name");
                office_UserHelper.TypeText("SearchAgentName", "Shannon");
                office_UserHelper.WaitForWorkAround(4000);

                executionLog.Log("DeleteShaEmployee", "Verify User deleted");
                office_UserHelper.VerifyPageText("No records to view");

                executionLog.Log("DeleteShaEmployee", "Logout from the application");
                VisitOffice("logout");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";
                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("DeleteShaEmployee");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Delete Sha Employee");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Delete Sha Employee", "Bug", "Medium", "User page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Delete Sha Employee");
                        TakeScreenshot("DeleteShaEmployee");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\DeleteShaEmployee.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("DeleteShaEmployee");
                        string id = loginHelper.getIssueID("Delete Sha Employee");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\DeleteShaEmployee.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Delete Sha Employee"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Delete Sha Employee");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("DeleteShaEmployee");
                executionLog.WriteInExcel("Delete Sha Employee", Status, JIRA, "Office Admin");
            }
        }
    }
}