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
    public class VerifyAdminButtonWorking : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin1")]
        [TestCategory("TS7")]
        [TestCategory("Pegasus_Admin1")]
        public void verifyAdminButtonWorking()
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
            var office_MyProfileHelper = new Office_MyProfileHelper(GetWebDriver());

            // Variable
            var name = "Testing Subject" + GetRandomNumber();
            var email = "Test" + GetRandomNumber() + "@gmail.com";
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("VerifyAdminButtonWorking", " Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyAdminButtonWorking", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("VerifyAdminButtonWorking", "Redirect to create document page");
                office_MyProfileHelper.ClickViaJavaScript("//a[text()='Admin']");
                office_MyProfileHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyAdminButtonWorking", "Get URL of the page.");
                var currentUrl = office_MyProfileHelper.getCurrentURL();

                executionLog.Log("VerifyAdminButtonWorking", "Verify Admin is present in the page.");
                Console.WriteLine("url is " + currentUrl);
                Assert.IsTrue(currentUrl.Contains("admin"));

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyAdminButtonWorking");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Admin Button Working");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Admin Button Working", "Bug", "Medium", "Admin page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Admin Button Working");
                        TakeScreenshot("VerifyAdminButtonWorking");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyAdminButtonWorking.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyAdminButtonWorking");
                        string id = loginHelper.getIssueID("Verify Admin Button Working");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyAdminButtonWorking.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Admin Button Working"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Admin Button Working");
            //    executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("VerifyAdminButtonWorking");
                executionLog.WriteInExcel("Verify Admin Button Working", Status, JIRA, "My Profile Page.");
            }
        }
    }
}