using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class VerifyValidEmailOnCreateUsersPage : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("BugTestNew")]
        public void verifyValidEmailOnCreateUsersPage()
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
            var username1 = "testinguser" + GetRandomNumber();
            var fname = "Sales" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("VerifyValidEmailOnCreateUsersPage", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyValidEmailOnCreateUsersPage", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyValidEmailOnCreateUsersPage", "Redirect at users page.");
                VisitOffice("users/create");
                office_UserHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyValidEmailOnCreateUsersPage", "Select user type >> 1099 Sales Agent");
                office_UserHelper.SelectByText("UserType", "1099 Sales Agent");
                office_UserHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyValidEmailOnCreateUsersPage", "Click on Create New radio button");
                office_UserHelper.ClickElement("CreateNew");
                office_UserHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyValidEmailOnCreateUsersPage", "Enter username");
                office_UserHelper.TypeText("UserName", username1);

                executionLog.Log("VerifyValidEmailOnCreateUsersPage", "Enter First Name");
                office_UserHelper.TypeText("FirstName", fname);

                executionLog.Log("VerifyValidEmailOnCreateUsersPage", "Enter Last Name");
                office_UserHelper.TypeText("LastName", "Agent");

                executionLog.Log("VerifyValidEmailOnCreateUsersPage", "Enter Birthdate");
                office_UserHelper.TypeText("Birthdate", "1992-04-15");

                executionLog.Log("VerifyValidEmailOnCreateUsersPage", "Enter Primary Email");
                var email = fname + "@yopmail.com";
                office_UserHelper.TypeText("PrimaryEmail", email);

                executionLog.Log("VerifyValidEmailOnCreateUsersPage", "Select avatar");
                office_UserHelper.ClickJS("SAAvatar");

                executionLog.Log("VerifyValidEmailOnCreateUsersPage", "Click on Additional Contact Info");
                office_UserHelper.ClickViaJavaScript("//h5[contains(text(),'Additional Contact Info')]");
                office_UserHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyValidEmailOnCreateUsersPage", "Enter eAddress");
                office_UserHelper.TypeText("eAddress", "tester23");

                executionLog.Log("VerifyValidEmailOnCreateUsersPage", "Click on Save button");
                office_UserHelper.ClickJS("Save");
                office_UserHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyValidEmailOnCreateUsersPage", "Verify validation for invalid email");
                office_UserHelper.VerifyPageText("Please enter a valid email address.");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyValidEmailOnCreateUsersPage");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Valid Email On Create Users Page");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Valid Email On Create Users Page", "Bug", "Medium", "Users page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Valid Email On Create Users Page");
                        TakeScreenshot("VerifyValidEmailOnCreateUsersPage");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyValidEmailOnCreateUsersPage.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyValidEmailOnCreateUsersPage");
                        string id = loginHelper.getIssueID("Verify Valid Email On Create Users Page");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyValidEmailOnCreateUsersPage.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Valid Email On Create Users Page"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Valid Email On Create Users Page");
                //       executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyValidEmailOnCreateUsersPage");
                executionLog.WriteInExcel("Verify Valid Email On Create Users Page", Status, JIRA, "Users Management");
            }
        }
    }
}