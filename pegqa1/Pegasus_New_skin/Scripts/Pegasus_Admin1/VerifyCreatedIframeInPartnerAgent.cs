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
    public class VerifyCreatedIframeInPartnerAgent : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin1")]
        [TestCategory("TS7")]
        [TestCategory("Pegasus_Admin1")]
        public void verifyCreatedIframeInPartnerAgent()
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
            var integration_IframeAppsHelper = new Integration_IframeAppsHelper(GetWebDriver());

            // Variable
            var tab = "tab" + RandomNumber(111, 999);

            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("VerifyCreatedIframeInPartnerAgent", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyCreatedIframeInPartnerAgent", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("VerifyCreatedIframeInPartnerAgent", "Redirect at admin page");
                VisitOffice("admin");

                executionLog.Log("VerifyCreatedIframeInPartnerAgent", "Redirect at iframe apps page.");
                VisitOffice("iframes");
                integration_IframeAppsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCreatedIframeInPartnerAgent", "Verify page title.");
                VerifyTitle("Iframe Apps");

                executionLog.Log("VerifyCreatedIframeInPartnerAgent", " Click On Create button.");
                integration_IframeAppsHelper.ClickElement("Create");

                executionLog.Log("VerifyCreatedIframeInPartnerAgent", "Verify page title.");
                VerifyTitle("Create Iframe");

                executionLog.Log("VerifyCreatedIframeInPartnerAgent", "Click on Save button.");
                integration_IframeAppsHelper.ClickElement("Save");

                executionLog.Log("VerifyCreatedIframeInPartnerAgent", "Verify validation for mandatoryness.");
                integration_IframeAppsHelper.VerifyText("TabNameErr", "This field is required.");

                executionLog.Log("VerifyCreatedIframeInPartnerAgent", "Verify validation for mandatoryness.");
                integration_IframeAppsHelper.VerifyText("UserNameErr", "This field is required.");

                executionLog.Log("VerifyCreatedIframeInPartnerAgent", "Verify validation for mandatoryness.");
                integration_IframeAppsHelper.VerifyText("PasswrdErr", "This field is required.");

                executionLog.Log("VerifyCreatedIframeInPartnerAgent", "Verify validation for mandatoryness.");
                integration_IframeAppsHelper.VerifyText("URLErr", "This field is required.");

                executionLog.Log("VerifyCreatedIframeInPartnerAgent", "Click on cancel button.");
                integration_IframeAppsHelper.ClickElement("Cancel");

                executionLog.Log("VerifyCreatedIframeInPartnerAgent", "Verify Page title.");
                VerifyTitle("Iframe Apps");

                executionLog.Log("VerifyCreatedIframeInPartnerAgent", " Click On Create button.");
                integration_IframeAppsHelper.ClickElement("Create");

                executionLog.Log("VerifyCreatedIframeInPartnerAgent", "Verify page title.");
                VerifyTitle("Create Iframe");

                executionLog.Log("VerifyCreatedIframeInPartnerAgent", "Enter Tab Name");
                integration_IframeAppsHelper.TypeText("TabName", tab);

                executionLog.Log("VerifyCreatedIframeInPartnerAgent", "Enter user Name");
                integration_IframeAppsHelper.TypeText("UserNameInputFieldName", "User");
                integration_IframeAppsHelper.WaitForWorkAround(4000);

                executionLog.Log("VerifyCreatedIframeInPartnerAgent", "Enter Password");
                integration_IframeAppsHelper.TypeText("PasswordInputFieldNmae", "1qaz!QAZ");

                executionLog.Log("VerifyCreatedIframeInPartnerAgent", "Enter an invalid alphabetical url.");
                integration_IframeAppsHelper.TypeText("LoginURL", "sadada");

                executionLog.Log("VerifyCreatedIframeInPartnerAgent", "Verify validation for invalid url.");
                integration_IframeAppsHelper.VerifyText("URLErr2", "Invalid URL");

                executionLog.Log("VerifyCreatedIframeInPartnerAgent", "Enter an invalid numerical url.");
                integration_IframeAppsHelper.TypeText("LoginURL", "12222");

                executionLog.Log("VerifyCreatedIframeInPartnerAgent", "Verify validation for invalid url.");
                integration_IframeAppsHelper.VerifyText("URLErr2", "Invalid URL");

                executionLog.Log("VerifyCreatedIframeInPartnerAgent", "Enter an invalid web address.");
                integration_IframeAppsHelper.TypeText("LoginURL", "www.google.com");

                executionLog.Log("VerifyCreatedIframeInPartnerAgent", "Verify validation for invalid url.");
                integration_IframeAppsHelper.VerifyText("URLErr2", "Invalid URL");

                executionLog.Log("VerifyCreatedIframeInPartnerAgent", "Enter a valid Login Url");
                integration_IframeAppsHelper.TypeText("LoginURL", _office + "login");

                executionLog.Log("VerifyCreatedIframeInPartnerAgent", "Click on partner portal check box.");
                integration_IframeAppsHelper.ClickElement("PartnerPortal");

                executionLog.Log("VerifyCreatedIframeInPartnerAgent", "Click on Save button");
                integration_IframeAppsHelper.ClickElement("Save");

                executionLog.Log("VerifyCreatedIframeInPartnerAgent", "Wait for creation success text.");
                integration_IframeAppsHelper.WaitForText("Iframe created successfully.", 10);

                executionLog.Log("VerifyCreatedIframeInPartnerAgent", "Logout from the application.");
                VisitOffice("logout");

                executionLog.Log("VerifyCreatedIframeInPartnerAgent", "Login with valid username and password");
                Login("MyPartnerAgent", "1qaz!QAZ");
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyCreatedIframeInPartnerAgent", "Verify Page title.");
                VerifyTitle("QApartner - Details");

                var loc = "//span[text()='" + tab + "']";
                executionLog.Log("VerifyCreatedIframeInPartnerAgent", "Wait for locator to be present.");
                integration_IframeAppsHelper.WaitForElementPresent(loc, 10);

                executionLog.Log("VerifyCreatedIframeInPartnerAgent", "Verify created iframe present in partner agent portal.");
                integration_IframeAppsHelper.IsElementPresent(loc);

                executionLog.Log("VerifyCreatedIframeInPartnerAgent", "Logout fron the partner portal.");
                VisitOffice("logout");

                executionLog.Log("VerifyCreatedIframeInPartnerAgent", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyCreatedIframeInPartnerAgent", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("VerifyCreatedIframeInPartnerAgent", "Redirect at office admin.");
                VisitOffice("admin");

                executionLog.Log("VerifyCreatedIframeInPartnerAgent", "Redirect at iframe apps page.");
                VisitOffice("iframes");

                executionLog.Log("VerifyCreatedIframeInPartnerAgent", "Enter iframe name to be searched.");
                integration_IframeAppsHelper.TypeText("SearchTabName", tab);
                integration_IframeAppsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCreatedIframeInPartnerAgent", "Click on edit icon.");
                integration_IframeAppsHelper.ClickElement("Edit");

                executionLog.Log("VerifyCreatedIframeInPartnerAgent", "Verify Page title.");
                VerifyTitle("Edit Iframe");

                executionLog.Log("VerifyCreatedIframeInPartnerAgent", "Uncheck the partner portal check box.");
                integration_IframeAppsHelper.ClickElement("PartnerPortal");

                executionLog.Log("VerifyCreatedIframeInPartnerAgent", "Click on Save button.");
                integration_IframeAppsHelper.ClickElement("Save");

                executionLog.Log("VerifyCreatedIframeInPartnerAgent", "Wait for iframe updation success text.");
                integration_IframeAppsHelper.WaitForText("Iframe updated Successfully.", 10);

                executionLog.Log("VerifyCreatedIframeInPartnerAgent", "Logout from the application.");
                VisitOffice("logout");

                executionLog.Log("VerifyCreatedIframeInPartnerAgent", "Login with valid username and password");
                Login("MyPartnerAgent", "1qaz!QAZ");
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyCreatedIframeInPartnerAgent", "Verify Page title.");
                VerifyTitle("QApartner - Details");

                executionLog.Log("VerifyCreatedIframeInPartnerAgent", "Wait for locator to be present.");
                integration_IframeAppsHelper.WaitForElementPresent(loc, 10);

                executionLog.Log("VerifyCreatedIframeInPartnerAgent", "Verify created iframe not present in partner agent portal.");
                integration_IframeAppsHelper.ElementNotAvailable(loc);

                executionLog.Log("VerifyCreatedIframeInPartnerAgent", "Logout from partner portal..");
                VisitOffice("logout");

                executionLog.Log("VerifyCreatedIframeInPartnerAgent", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyCreatedIframeInPartnerAgent", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("VerifyCreatedIframeInPartnerAgent", "Redirect at iframe apps page.");
                VisitOffice("iframes");

                executionLog.Log("VerifyCreatedIframeInPartnerAgent", "Verify page title.");
                VerifyTitle("Iframe Apps");

                executionLog.Log("VerifyCreatedIframeInPartnerAgent", "Enter iframe name to  be searched.");
                integration_IframeAppsHelper.TypeText("SearchTabName", tab);
                integration_IframeAppsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCreatedIframeInPartnerAgent", "Click on delete button.");
                integration_IframeAppsHelper.ClickElement("ClickOnDelete");

                executionLog.Log("VerifyCreatedIframeInPartnerAgent", "Click ok to accept alert message.");
                integration_IframeAppsHelper.AcceptAlert();

                executionLog.Log("VerifyCreatedIframeInPartnerAgent", "Wait for deletion success message.");
                integration_IframeAppsHelper.WaitForText("Iframe deleted successfully.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyCreatedIframeInPartnerAgent");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Created Iframe In Partner Agent");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Created Iframe In Partner Agent", "Bug", "Medium", "Iframe Intergration page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Created Iframe In Partner Agent");
                        TakeScreenshot("VerifyCreatedIframeInPartnerAgent");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyCreatedIframeInPartnerAgent.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyCreatedIframeInPartnerAgent");
                        string id = loginHelper.getIssueID("Verify Created Iframe In Partner Agent");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyCreatedIframeInPartnerAgent.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Created Iframe In Partner Agent"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Created Iframe In Partner Agent");
            //    executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("VerifyCreatedIframeInPartnerAgent");
                executionLog.WriteInExcel("Verify Created Iframe In Partner Agent", Status, JIRA, "IFrame");
            }
        }
    }
}