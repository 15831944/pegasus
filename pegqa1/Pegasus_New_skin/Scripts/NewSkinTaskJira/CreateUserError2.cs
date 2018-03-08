using System;
using System.IO;
using LinqToExcel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class CreateUserError2 : DriverTestCase
    {
        [TestMethod]
        [TestCategory("NewSkin_Task")]
        [TestCategory("All")]
        [TestCategory("TS4")]
        [TestCategory("NewSkinTaskJira")]
        public void createUserError2()
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
                executionLog.Log("CreateUserError2", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("CreateUserError2", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("CreateUserError2", "Go to Create user page");
                VisitOffice("users/create");

                executionLog.Log("CreateUserError2", "Verify title");
                VerifyTitle("Create User");

                executionLog.Log("CreateUserError2", "Select User type");
                office_UserHelper.SelectByText("UserType", "Employee");

                executionLog.Log("CreateUserError2", "Click on Create new");
                office_UserHelper.ClickElement("CreateNew");

                executionLog.Log("CreateUserError2", "Enter first name");
                office_UserHelper.TypeText("FirstName", "Aslam");

                executionLog.Log("CreateUserError2", "Enter last Name");
                office_UserHelper.TypeText("LastName", "Khan");

                executionLog.Log("CreateUserError2", "Enter existing user name");
                office_UserHelper.TypeText("UserName", username[0]);

                executionLog.Log("CreateUserError2", "Click on Password checkbox");
                office_UserHelper.ClickElement("UserAutoGenerate");

                executionLog.Log("CreateUserError2", "Enter password");
                office_UserHelper.TypeText("UserPassword", "1");

                executionLog.Log("CreateUserError2", "Enter primary email");
                office_UserHelper.TypeText("PrimaryEmail", "Test@yopmail.com");

                executionLog.Log("CreateUserError2", "Click on 'Save' button");
                office_UserHelper.ClickElement("Save1099");

                executionLog.Log("CreateUserError2", "Verify Checkbox not checked");
                office_UserHelper.verifyElementNotAvailable("UserCheckNotCheck");

                executionLog.Log("CreateUserError2", "Logout from the application");
                VisitOffice("logout");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CreateUserError2");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Create User Error 2");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Create User Error 2", "Bug", "Medium", "Create User page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Create User Error 2");
                        TakeScreenshot("CreateUserError2");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateUserError2.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CreateUserError2");
                        string id = loginHelper.getIssueID("Create User Error 2");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateUserError2.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Create User Error 2"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Create User Error 2");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("CreateUserError2");
                executionLog.WriteInExcel("Create User Error 2", Status, JIRA, "Office Admin");
            }
        }
    }
}