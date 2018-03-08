using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class ResetPasswordOfficeUser : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin1")]
        [TestCategory("Fail")]
        [TestCategory("TS7")]
        [TestCategory("Pegasus_Admin1")]
        public void resetPasswordOfficeUser()
        {
            string[] username = null;
            string[] password = null;

            XMLParse oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var office_UserHelper = new Office_UserHelper(GetWebDriver());

            // Variable
            String name = "TestTester" + GetRandomNumber();
            String email = "Test" + GetRandomNumber() + "@yopmail.com";
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("ResetPasswordOfficeUser", " Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("ResetPasswordOfficeUser", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("ResetPasswordOfficeUser", "Redirect To User page");
                VisitOffice("users");

                executionLog.Log("ResetPasswordOfficeUser", "Verify title");
                VerifyTitle("Users");

                executionLog.Log("ResetPasswordOfficeUser", " Click On Create");
                office_UserHelper.ClickElement("CreateButton");

                executionLog.Log("ResetPasswordOfficeUser", "Verify title");
                VerifyTitle("Create User");

                executionLog.Log("ResetPasswordOfficeUser", "Select Type");
                office_UserHelper.Select("UserType", "Employee");
                office_UserHelper.WaitForWorkAround(2000);

                executionLog.Log("ResetPasswordOfficeUser", "Click on Create New");
                office_UserHelper.ClickElement("CreateNew");
                office_UserHelper.WaitForWorkAround(2000);

                executionLog.Log("ResetPasswordOfficeUser", "Enter USER NAME");
                office_UserHelper.TypeText("UserName", name);
                //office_UserHelper.WaitForWorkAround(2000);

                executionLog.Log("ResetPasswordOfficeUser", "Select Salutation");
                office_UserHelper.Select("Salutation", "Mr");
                //office_UserHelper.WaitForWorkAround(2000);

                executionLog.Log("ResetPasswordOfficeUser", "Enter FIRST NAME");
                office_UserHelper.TypeText("FirstName", "Test");
                //office_UserHelper.WaitForWorkAround(2000);

                executionLog.Log("ResetPasswordOfficeUser", "Enter LastName");
                office_UserHelper.TypeText("LastName", "Tester");
                //office_UserHelper.WaitForWorkAround(2000);

                executionLog.Log("ResetPasswordOfficeUser", "Enter Primary Email");
                office_UserHelper.TypeText("PrimaryEmail", email);
                //office_UserHelper.WaitForWorkAround(2000);

                executionLog.Log("ResetPasswordOfficeUser", "CLick on Admin");
                office_UserHelper.ClickElement("AvtarAdminUser");
                //office_UserHelper.WaitForWorkAround(2000);

                executionLog.Log("ResetPasswordOfficeUser", "Enter Birthday");
                office_UserHelper.TypeText("Birthdate", "05/05/1990");

                executionLog.Log("ResetPasswordOfficeUser", "cLICK on Save  ");
                office_UserHelper.ClickElement("Save");
                office_UserHelper.WaitForWorkAround(2000);

                executionLog.Log("ResetPasswordOfficeUser", "Wait for text");
                office_UserHelper.WaitForText("The user is successfully added", 10);

                executionLog.Log("ResetPasswordOfficeUser", "Select Status");
                office_UserHelper.Select("Status", "");
                office_UserHelper.WaitForWorkAround(2000);

                executionLog.Log("ResetPasswordOfficeUser", "Enter Email Search ");
                office_UserHelper.TypeText("EmailFilter", email);
                office_UserHelper.WaitForWorkAround(7000);

                executionLog.Log("ResetPasswordOfficeUser", "Click on User");
                office_UserHelper.ClickElement("ClickUser");
                office_UserHelper.WaitForWorkAround(1000);

                executionLog.Log("ResetPasswordOfficeUser", " Click on Reset Password");
                office_UserHelper.ClickElement("ResetPassword");
                office_UserHelper.WaitForWorkAround(2000);

                executionLog.Log("ResetPasswordOfficeUser", "Accept Alert");
                office_UserHelper.AcceptAlert();

                executionLog.Log("ResetPasswordOfficeUser", "No link in the email is present to reset password");
                office_UserHelper.WaitForText("Reset password link sent to ", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("ResetPasswordOfficeUser");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Reset Password Office User");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Reset Password Office User", "Bug", "Medium", "Reset Password page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Reset Password Office User");
                        TakeScreenshot("ResetPasswordOfficeUser");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ResetPasswordOfficeUser.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("ResetPasswordOfficeUser");
                        string id = loginHelper.getIssueID("Reset Password Office User");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ResetPasswordOfficeUser.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Reset Password Office User"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Reset Password Office User");
                //   executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("ResetPasswordOfficeUser");
                executionLog.WriteInExcel("Reset Password Office User", Status, JIRA, "Office");
            }
        }
    }
}