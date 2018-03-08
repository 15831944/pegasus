using System;
using System.IO;
using LinqToExcel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class CreateUserError1 : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS4")]
        [TestCategory("NewSkinTaskJira")]
        public void createUserError1()
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

            // Variable
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("CreateUserError1", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("CreateUserError1", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("CreateUserError1", "Go to Create user page");
                VisitOffice("users/create");
                office_UserHelper.WaitForWorkAround(7000);

                executionLog.Log("CreateUserError1", "Verify title");
                VerifyTitle("Create User");

                executionLog.Log("CreateUserError1", "Select User type");
                office_UserHelper.SelectByText("UserType", "Employee");

                executionLog.Log("CreateUserError1", "Click on Create new");
                office_UserHelper.ClickElement("CreateNew");
                office_UserHelper.WaitForWorkAround(4000);

                executionLog.Log("CreateUserError1", "Enter first name");
                office_UserHelper.TypeText("FirstName", "Aslam");

                executionLog.Log("CreateUserError1", "Enter last Name");
                office_UserHelper.TypeText("LastName", "Khan");

                executionLog.Log("CreateUserError1", "Enter existing user name");
                office_UserHelper.TypeText("UserName", username[0]);

                executionLog.Log("CreateUserError1", "Enter primary email");
                office_UserHelper.TypeText("PrimaryEmail", "Test@yopmail.com");

                executionLog.Log("CreateUserError1", "Click Avtar");
                office_UserHelper.ClickElement("AvtarEmployeeAdmin");

                executionLog.Log("CreateUserError1", "Click on 'Save' button");
                office_UserHelper.ClickElement("Save1099");
                office_UserHelper.WaitForWorkAround(4000);

                executionLog.Log("CreateUserError1", "Wait for text");
                office_UserHelper.WaitForText("This username already taken", 5);

                executionLog.Log("CreateUserError1", "Logout from the application");
                VisitOffice("logout");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CreateUserError1");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Create User Error1");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Create User Error1", "Bug", "Medium", "User page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Create User Error1");
                        TakeScreenshot("CreateUserError1");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateUserError1.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CreateUserError1");
                        string id = loginHelper.getIssueID("Create User Error1");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateUserError1.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Create User Error1"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Create User Error1");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("CreateUserError1");
                executionLog.WriteInExcel("Create User Error1", Status, JIRA, "Office Admin");
            }
        }
    }
}