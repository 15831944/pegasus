using System;
using System.IO;
using LinqToExcel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class CreateUserAdditionalFields : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS4")]
        [TestCategory("NewSkinTaskJira")]
        public void createUserAdditionalFields()
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

            // Variables
            var name = "User" + RandomNumber(1, 9999);
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("CreateUserAdditionalFields", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("CreateUserAdditionalFields", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("CreateUserAdditionalFields", "Go to Create user page");
                VisitOffice("users/create");

                executionLog.Log("CreateUserAdditionalFields", "Verify title");
                VerifyTitle("Create User");

                executionLog.Log("CreateUserAdditionalFields", "Select parnter association as type");
                office_UserHelper.SelectByText("Usertype", "Partner Association");

                executionLog.Log("CreateUserAdditionalFields", "Click on Create new");
                office_UserHelper.ClickElement("CreateNew");

                executionLog.Log("CreateUserAdditionalFields", "Expand the Additional Contact Info link");
                office_UserHelper.ClickElement("AddiInfo");

                executionLog.Log("CreateUserAdditionalFields", "Verify fields are available");
                office_UserHelper.verifyElementAvailable("AddInfoEmail");

                executionLog.Log("CreateUserAdditionalFields", "Verify fields are available");
                office_UserHelper.verifyElementAvailable("AddInfoPhone");

                executionLog.Log("CreateUserAdditionalFields", "Verify fields are available");
                office_UserHelper.verifyElementAvailable("AddInfoAdd");

                executionLog.Log("CreateUserAdditionalFields", "Log out from the application");
                VisitOffice("logout");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CreateUserAdditionalFields");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Create User Additional Fields");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Create User Additional Fields", "Bug", "Medium", "User page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Create User Additional Fields");
                        TakeScreenshot("CreateUserAdditionalFields");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateUserAdditionalFields.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CreateUserAdditionalFields");
                        string id = loginHelper.getIssueID("Create User Additional Fields");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateUserAdditionalFields.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Create User Additional Fields"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Create User Additional Fields");
           //     executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("CreateUserAdditionalFields");
                executionLog.WriteInExcel("Create User Additional Fields", Status, JIRA, "Office Admin");
            }
        }
    }
}
