using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class VerifyUserBirthdateValidation : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("BugTestNew")]
        public void verifyUserBirthdateValidation()
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
                executionLog.Log("VerifyUserBirthdateValidation", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyUserBirthdateValidation", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyUserBirthdateValidation", "Redirect at users page.");
                VisitOffice("users");
                office_UserHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyUserBirthdateValidation", "Click on Create button");
                office_UserHelper.ClickElement("CreateButton");
                office_UserHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyUserBirthdateValidation", "Select user type >> 1099 Sales Agent");
                office_UserHelper.SelectByText("UserType", "1099 Sales Agent");
                office_UserHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyUserBirthdateValidation", "Click on Create New radio button");
                office_UserHelper.ClickElement("CreateNew");
                office_UserHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyUserBirthdateValidation", "Enter username");
                office_UserHelper.TypeText("UserName", username1);

                executionLog.Log("VerifyUserBirthdateValidation", "Enter First Name");
                office_UserHelper.TypeText("FirstName", fname);

                executionLog.Log("VerifyUserBirthdateValidation", "Enter Last Name");
                office_UserHelper.TypeText("LastName", "Agent");

                executionLog.Log("VerifyUserBirthdateValidation", "Enter Birthdate");
                office_UserHelper.TypeText("Birthdate", "2017-04-15");

                executionLog.Log("VerifyUserBirthdateValidation", "Enter Primary Email");
                var email = fname + "@yopmail.com";
                office_UserHelper.TypeText("PrimaryEmail", email);

                executionLog.Log("VerifyUserBirthdateValidation", "Select avatar");
                office_UserHelper.ClickElement("SAAvatar");

                executionLog.Log("VerifyUserBirthdateValidation", "Click on Save button");
                office_UserHelper.ClickElement("Save");
                office_UserHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyUserBirthdateValidation", "Verify validation for invalid birthday");
                office_UserHelper.VerifyPageText("Age should be greater than 18.");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyUserBirthdateValidation");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify User Birthdate Validation");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify User Birthdate Validation", "Bug", "Medium", "Users page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify User Birthdate Validation");
                        TakeScreenshot("VerifyUserBirthdateValidation");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyUserBirthdateValidation.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyUserBirthdateValidation");
                        string id = loginHelper.getIssueID("Verify User Birthdate Validation");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyUserBirthdateValidation.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify User Birthdate Validation"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify User Birthdate Validation");
         //       executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyUserBirthdateValidation");
                executionLog.WriteInExcel("Verify User Birthdate Validation", Status, JIRA, "Users Management");
            }
        }
    }
}