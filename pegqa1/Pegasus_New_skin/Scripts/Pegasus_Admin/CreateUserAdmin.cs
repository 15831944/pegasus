using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Admin
{
    [TestClass]
    public class CreateUserAdmin : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin")]
        [TestCategory("TS6")]
        [TestCategory("Pegasus_Admin")]
        public void createUserAdmin()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var office_UserHelper = new Office_UserHelper(GetWebDriver());

            // Variable
            var name = "TestTester" + GetRandomNumber();
            var email = "Test" + GetRandomNumber() + "@gmail.com";
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("CreateUserAdmin", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("CreateUserAdmin", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("CreateUserAdmin", "Click On  Admin");
                VisitOffice("admin");

                executionLog.Log("CreateUserAdmin", "Redirect To User");
                VisitOffice("users");
                office_UserHelper.WaitForWorkAround(4000);

                executionLog.Log("CreateUserAdmin", "Verify title");
                VerifyTitle("Users");

                executionLog.Log("CreateUserAdmin", " Click On Create");
                office_UserHelper.ClickElement("CreateButton");
                office_UserHelper.WaitForWorkAround(4000);

                executionLog.Log("CreateUserAdmin", "Verify title");
                VerifyTitle("Create User");

                executionLog.Log("CreateUserAdmin", "Select user Type");
                office_UserHelper.Select("UserType", "Employee");

                executionLog.Log("CreateUserAdmin", "Click on Create New");
                office_UserHelper.ClickElement("CreateNew");
                office_UserHelper.WaitForWorkAround(4000);

                executionLog.Log("CreateUserAdmin", "Enter USER NAME");
                office_UserHelper.TypeText("UserName", name);

                executionLog.Log("CreateUserAdmin", "Select Salutation");
                office_UserHelper.Select("Salutation", "Mr");

                executionLog.Log("CreateUserAdmin", "Enter FIRST NAME");
                office_UserHelper.TypeText("FirstName", "Test");

                executionLog.Log("CreateUserAdmin", "Enter LastName");
                office_UserHelper.TypeText("LastName", "  Tester");

                executionLog.Log("CreateUserAdmin", "Enter Primary Email");
                office_UserHelper.TypeText("PrimaryEmail", email);

                executionLog.Log("CreateUserAdmin", "Click on Admin Avatar");
                office_UserHelper.ClickElement("AvtarAdminUser");

                executionLog.Log("CreateUserAdmin", "ClicK on Save  ");
                office_UserHelper.ClickElement("Save");
                office_UserHelper.WaitForWorkAround(3000);

                executionLog.Log("CreateUserAdmin", "Wait for Confirmation");
                office_UserHelper.WaitForText("The user is successfully added", 5);

                executionLog.Log("CreateUserAdmin", "Search the agent name");
                office_UserHelper.TypeText("EnterEmail", email);
                office_UserHelper.WaitForWorkAround(3000);

                executionLog.Log("CreateUserAdmin", "Select All");
                office_UserHelper.SelectByText("Status", "All");
                office_UserHelper.WaitForWorkAround(3000);

                executionLog.Log("CreateUserAdmin", "Click on agent name");
                office_UserHelper.ClickElement("FirstAgentName");
                office_UserHelper.WaitForWorkAround(5000);

                executionLog.Log("CreateUserAdmin", "Click Delete user button");
                office_UserHelper.ClickElement("ClickOnDeleteUser");
                office_UserHelper.AcceptAlert();

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CreateUserAdmin");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Create User Admin ");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Create User Admin ", "Bug", "Medium", "Admin Office page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Create User Admin ");
                        TakeScreenshot("CreateUserAdmin");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateUserAdmin.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CreateUserAdmin");
                        string id = loginHelper.getIssueID("Create User Admin ");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateUserAdmin.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Create User Admin "), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Create User Admin ");
             //   executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("CreateUserAdmin");
                executionLog.WriteInExcel("Create User Admin ", Status, JIRA, "Office");
            }
        }
    }
}