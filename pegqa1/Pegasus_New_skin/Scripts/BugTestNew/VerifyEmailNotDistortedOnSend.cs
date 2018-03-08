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
    public class VerifyEmailNotDistortedOnSend : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin")]
        public void verifyEmailNotDistortedOnSend()
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
                executionLog.Log("VerifyEmailNotDistortedOnSend", " Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyEmailNotDistortedOnSend", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("VerifyEmailNotDistortedOnSend", "Redirect at admin page.");
                VisitOffice("admin");

                executionLog.Log("VerifyEmailNotDistortedOnSend", "Redirect to My Profile page.");
                VisitOffice("myprofile");

                executionLog.Log("VerifyEmailNotDistortedOnSend", "Verify page title as My profile.");
                VerifyTitle("My Profile");

                executionLog.Log("VerifyEmailNotDistortedOnSend", "Click on email icon");
                office_MyProfileHelper.ClickElement("ClicktoEmailIcon");
                office_MyProfileHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyEmailNotDistortedOnSend", "Switch to compose mail page.");
                office_MyProfileHelper.SwitchToWindow();
                office_MyProfileHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyEmailNotDistortedOnSend", "Verify page title as compose.");
                VerifyTitle("Compose");

                executionLog.Log("VerifyEmailNotDistortedOnSend", "Verify .com present in the email");
                office_MyProfileHelper.VerifyCom();

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyEmailNotDistortedOnSend");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Email Not Distorted On Send");
                if (!result)
                {
                    if (Int16.Parse(counter) < 5)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Email Not Distorted On Send", "Bug", "Medium", "Document page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Email Not Distorted On Send");
                        TakeScreenshot("VerifyEmailNotDistortedOnSend");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyEmailNotDistortedOnSend.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 5)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyEmailNotDistortedOnSend");
                        string id = loginHelper.getIssueID("Verify Email Not Distorted On Send");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyEmailNotDistortedOnSend.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Email Not Distorted On Send"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Email Not Distorted On Send");
                executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("VerifyEmailNotDistortedOnSend");
                executionLog.WriteInExcel("Verify Email Not Distorted On Send", Status, JIRA, "Admin Corp");
            }

        }
    }
}
