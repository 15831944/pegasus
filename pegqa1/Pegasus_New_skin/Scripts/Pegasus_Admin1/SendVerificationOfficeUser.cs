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
    public class SendVerificationOfficeUser : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin1")]
        [TestCategory("Fail")]
        [TestCategory("TS7")]
        [TestCategory("Pegasus_Admin1")]
        public void sendVerificationOfficeUser()
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
            var name = "TestTester" + GetRandomNumber();
            var email = "Test" + GetRandomNumber() + "@yopmail.com";
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("SendVerificationOfficeUser", " Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("SendVerificationOfficeUser", " Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("SendVerificationOfficeUser", " Redirect To User page");
                VisitOffice("users");
                office_UserHelper.WaitForWorkAround(3000);

                executionLog.Log("SendVerificationOfficeUser", " Verify title");
                VerifyTitle("Users");

                executionLog.Log("SendVerificationOfficeUser", "  Click On Create");
                office_UserHelper.ClickElement("CreateButton");

                executionLog.Log("SendVerificationOfficeUser", " Select Type");
                office_UserHelper.Select("UserType", "Employee");
                office_UserHelper.WaitForWorkAround(2000);

                executionLog.Log("SendVerificationOfficeUser", " Click on Create New");
                office_UserHelper.ClickElement("CreateNew");
                office_UserHelper.WaitForWorkAround(2000);

                executionLog.Log("SendVerificationOfficeUser", " Enter USER NAME");
                office_UserHelper.TypeText("UserName", name);
                //office_UserHelper.WaitForWorkAround(2000);

                executionLog.Log("SendVerificationOfficeUser", " Select Salutation");
                office_UserHelper.Select("Salutation", "Mr");
                //office_UserHelper.WaitForWorkAround(2000);

                executionLog.Log("SendVerificationOfficeUser", " Enter FIRST NAME");
                office_UserHelper.TypeText("FirstName", "Test");
                //office_UserHelper.WaitForWorkAround(2000);

                executionLog.Log("SendVerificationOfficeUser", " Enter LastName");
                office_UserHelper.TypeText("LastName", "  Tester");
                //office_UserHelper.WaitForWorkAround(2000);

                executionLog.Log("SendVerificationOfficeUser", " Enter Primary Email");
                office_UserHelper.TypeText("PrimaryEmail", email);
                //office_UserHelper.WaitForWorkAround(2000);

                executionLog.Log("SendVerificationOfficeUser", " Click on Admin Avatar");
                office_UserHelper.ClickElement("AvtarAdminUser");
                //office_UserHelper.WaitForWorkAround(2000);

                executionLog.Log("ResetPasswordOfficeUser", "Enter Birthday");
                office_UserHelper.TypeText("Birthdate", "05/05/1990");

                executionLog.Log("SendVerificationOfficeUser", " cLICK on Save  ");
                office_UserHelper.ClickElement("Save");
                office_UserHelper.WaitForWorkAround(3000);

                executionLog.Log("SendVerificationOfficeUser", "Wait for success message. ");
                office_UserHelper.WaitForText("The user is successfully added", 05);

                executionLog.Log("SendVerificationOfficeUser", " Select Status");
                office_UserHelper.Select("Status", "");
                office_UserHelper.WaitForWorkAround(2000);

                executionLog.Log("SendVerificationOfficeUser", " Enter Email Search ");
                office_UserHelper.TypeText("EmailFilter", email);
                office_UserHelper.WaitForWorkAround(2000);

                executionLog.Log("SendVerificationOfficeUser", " Click on User");
                office_UserHelper.ClickElement("ClickUser");
                office_UserHelper.WaitForWorkAround(2000);

                executionLog.Log("SendVerificationOfficeUser", "  Click on Reset Password");
                office_UserHelper.ClickElement("ResetPassword");
                office_UserHelper.WaitForWorkAround(2000);

                executionLog.Log("SendVerificationOfficeUser", " Accept Alert");
                office_UserHelper.AcceptAlert();
                office_UserHelper.WaitForWorkAround(2000);

                executionLog.Log("SendVerificationOfficeUser", "Wait for text");
                office_UserHelper.WaitForText("Reset password link sent to ", 05);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("SendVerificationOfficeUser");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Send Verification Office User");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Send Verification Office User", "Bug", "Medium", "Office User page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Send Verification Office User");
                        TakeScreenshot("SendVerificationOfficeUser");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\SendVerificationOfficeUser.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("SendVerificationOfficeUser");
                        string id = loginHelper.getIssueID("Send Verification Office User");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\SendVerificationOfficeUser.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Send Verification Office User"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Send Verification Office User");
                //    executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("SendVerificationOfficeUser");
                executionLog.WriteInExcel("Send Verification Office User", Status, JIRA, "Email Integration");
            }
        }
    }
}