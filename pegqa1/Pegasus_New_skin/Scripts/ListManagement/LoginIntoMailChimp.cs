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
    public class LoginIntoMailChimp : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS1")]
        [TestCategory("ListManagement")]
        public void loginIntoMailChimp()
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
            var listManagementHelper = new ListManagementHelper(GetWebDriver());

            // Variable 
            var name = "Test" + GetRandomNumber();
            var name2 = "Testlist" + GetRandomNumber();
            var Id = "12345" + GetRandomNumber();
            String Jira = "";
            String Status = "Pass";

            //try
            //{
                executionLog.Log("LoginIntoMailChimp", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("LoginIntoMailChimp", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("LoginIntoMailChimp", "Redirect To List Management page");
                listManagementHelper.ClickElement("Marketing");
                listManagementHelper.WaitForWorkAround(4000);

                executionLog.Log("LoginIntoMailChimp", "Go To MailChimp Login Page");
                listManagementHelper.ClickElement("MailChimpLogin");
                listManagementHelper.WaitForWorkAround(4000);

                executionLog.Log("LoginIntoMailChimp", "Enter Username");
                listManagementHelper.TypeText("UserName", "pegasuscrmdevelopers@gmail.com");

                executionLog.Log("LoginIntoMailChimp", "Enter Password");
                listManagementHelper.TypeText("Password", "Comma,,,,pegasus1");

                executionLog.Log("LoginIntoMailChimp", "Enter Password");
                listManagementHelper.ClickElement("LoginButtn");
                listManagementHelper.WaitForWorkAround(4000);

                executionLog.Log("LoginIntoMailChimp", "Verify Mailchimp Page");
                listManagementHelper.VerifyPageText("Mailchimp");
                Console.WriteLine("MailChimp Page Verified");

            }
            //}
            //catch (Exception e)
            //{
            //    executionLog.Log("Error", e.StackTrace);
            //    Status = "Fail";

            //    String counter = executionLog.readLastLine("counter");
            //    String Description = executionLog.GetAllTextFile("LoginIntoMailChimp");
            //    String Error = executionLog.GetAllTextFile("Error");
            //    if (counter == "")
            //    {
            //        counter = "0";
            //    }
            //    bool result = loginHelper.CheckExstingIssue("Login Into Mail Chimp");
            //    if (!result)
            //    {
            //        if (Int16.Parse(counter) < 9)
            //        {
            //            executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
            //            loginHelper.CreateIssue("Login Into Mail Chimp", "Bug", "Medium", "Equipment page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
            //            string id = loginHelper.getIssueID("Rename ListManagement Item");
            //            TakeScreenshot("LoginIntoMailChimp");
            //            string directoryName = loginHelper.GetnewDirectoryName(GetPath());
            //            var location = directoryName + "\\LoginIntoMailChimp.png";
            //            loginHelper.AddAttachment(location, id);
            //        }
            //    }
            //    else
            //    {
            //        if (Int16.Parse(counter) < 9)
            //        {
            //            executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
            //            TakeScreenshot("LoginIntoMailChimp");
            //            string id = loginHelper.getIssueID("Login Into Mail Chimp");
            //            string directoryName = loginHelper.GetnewDirectoryName(GetPath());
            //            var location = directoryName + "\\LoginIntoMailChimp.png";
            //            loginHelper.AddAttachment(location, id);
            //            loginHelper.AddComment(loginHelper.getIssueID("Login Into Mail Chimp"), "This issue is still occurring");
            //        }
            //    }
            //    JIRA = loginHelper.getIssueID("Login Into Mail Chimp");
            //    //  executionLog.DeleteFile("Error");
            //    throw;

            //}
            //finally
            //{
            //    executionLog.DeleteFile("LoginIntoMailChimp");
            //    executionLog.WriteInExcel("Login Into Mail Chimp", Status, JIRA, "List Management");
            //}
        }
    }
