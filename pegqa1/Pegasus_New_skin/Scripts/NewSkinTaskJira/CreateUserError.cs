using System;
using System.IO;
using LinqToExcel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class CreateUserError : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS4")]
        [TestCategory("NewSkinTaskJira")]
        public void createUserError()
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

            String JIRA = "";
            String Status = "Pass";
            try
            {
                executionLog.Log("CreateUserError", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("CreateUserError", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("CreateUserError", "Go to Create user page");
                VisitOffice("users/create");

                executionLog.Log("CreateUserError", "Verify title");
                VerifyTitle("Create User");

                executionLog.Log("CreateUserError", "Select User type");
                office_UserHelper.SelectByText("Usertype", "Employee");

                executionLog.Log("CreateUserError", "Click on Create new");
                office_UserHelper.ClickElement("CreateNew");

                executionLog.Log("CreateUserError", "Enter primary email");
                office_UserHelper.TypeText("PrimaryEmail", "INVALID");

                executionLog.Log("CreateUserError", "Click on 'Save' button");
                office_UserHelper.ClickElement("Save1099");

                executionLog.Log("CreateUserError", "Wait for text");
                office_UserHelper.WaitForText("Please enter a valid email address.", 10);

                executionLog.Log("CreateUserError", "Verify error not displayed");
                office_UserHelper.VerifyTextNotPresent("Internal server error page");

                executionLog.Log("CreateUserError", "Logout from the application");
                VisitOffice("logout");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CreateUserError");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Create User Error");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Create User Error", "Bug", "Medium", "User page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Create User Error");
                        TakeScreenshot("CreateUserError");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateUserError.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CreateUserError");
                        string id = loginHelper.getIssueID("Create User Error");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateUserError.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Create User Error"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Create User Error");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("CreateUserError");
                executionLog.WriteInExcel("Create User Error", Status, JIRA, "Office Admin");
            }
        }
    }
}