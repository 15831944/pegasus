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
    public class VerifyMyProfileEmailIconWorking : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin")]
        public void verifyMyProfileEmailIconWorking()
        {
            string[] username = null;
            string[] password = null;

            XMLParse oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var office_MyProfileHelper = new Office_MyProfileHelper(GetWebDriver());

            // Variable
            var name = "Testing Subject" + GetRandomNumber();
            var email = "Test" + GetRandomNumber() + "@gmail.com";
            String JIRA = "";
            String Status = "Pass";

          try
            {
            executionLog.Log("VerifyMyProfileEmailIconWorking", " Login with valid username and password");
            Login(username[0], password[0]);
            Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

            executionLog.Log("VerifyMyProfileEmailIconWorking", "Verify Page title");
            VerifyTitle("Dashboard");

            executionLog.Log("VerifyMyProfileEmailIconWorking", "Redirect at admin page.");
            VisitOffice("admin");

            executionLog.Log("VerifyMyProfileEmailIconWorking", "Redirect to My Profile page.");
            VisitOffice("myprofile");

            executionLog.Log("VerifyMyProfileEmailIconWorking", "Verify page title as My profile.");
            VerifyTitle("My Profile");

            executionLog.Log("VerifyMyProfileEmailIconWorking", "Click on email icon");
            office_MyProfileHelper.ClickElement("ClicktoEmailIcon");

            office_MyProfileHelper.WaitForWorkAround(5000);
            office_MyProfileHelper.SwitchToWindow();

            executionLog.Log("VerifyMyProfileEmailIconWorking", "Verify title as compose.");
            VerifyTitle("Compose");

        }
        catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyMyProfileEmailIconWorking");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify My Profile Email Icon Working");
                if (!result)
                {
                    if (Int16.Parse(counter) < 5)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify My Profile Email Icon Working", "Bug", "Medium", "Document page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify My Profile Email Icon Working");
                        TakeScreenshot("VerifyMyProfileEmailIconWorking");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyMyProfileEmailIconWorking.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 5)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyMyProfileEmailIconWorking");
                        string id = loginHelper.getIssueID("Verify My Profile Email Icon Working");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyMyProfileEmailIconWorking.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify My Profile Email Icon Working"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify My Profile Email Icon Working");
                executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("VerifyMyProfileEmailIconWorking");
                executionLog.WriteInExcel("Verify My Profile Email Icon Working", Status, JIRA, "Admin Corp");
            }
        }
    }
}