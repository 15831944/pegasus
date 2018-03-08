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
    public class VerifyCreatedIframeInOfficePortal : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin1")]
        [TestCategory("TS7")]
        [TestCategory("Pegasus_Admin1")]
        public void verifyCreatedIframeInOfficePortal()
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

                executionLog.Log("VerifyCreatedIframeInOfficePortal", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyCreatedIframeInOfficePortal", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("VerifyCreatedIframeInOfficePortal", "Redirect at admin page");
                VisitOffice("admin");

                executionLog.Log("VerifyCreatedIframeInOfficePortal", "Redirect at iframe apps page.");
                VisitOffice("iframes");

                executionLog.Log("VerifyCreatedIframeInOfficePortal", "Verify page title.");
                VerifyTitle("Iframe Apps");

                executionLog.Log("VerifyCreatedIframeInOfficePortal", " Click On Create button.");
                integration_IframeAppsHelper.ClickElement("Create");

                executionLog.Log("VerifyCreatedIframeInOfficePortal", "Verify page title.");
                VerifyTitle("Create Iframe");

                executionLog.Log("VerifyCreatedIframeInOfficePortal", "Click on Save button.");
                integration_IframeAppsHelper.ClickElement("Save");

                executionLog.Log("VerifyCreatedIframeInOfficePortal", "Verify validation for mandatoryness.");
                integration_IframeAppsHelper.VerifyText("TabNameErr", "This field is required.");

                executionLog.Log("VerifyCreatedIframeInOfficePortal", "Verify validation for mandatoryness.");
                integration_IframeAppsHelper.VerifyText("UserNameErr", "This field is required.");

                executionLog.Log("VerifyCreatedIframeInOfficePortal", "Verify validation for mandatoryness.");
                integration_IframeAppsHelper.VerifyText("PasswrdErr", "This field is required.");

                executionLog.Log("VerifyCreatedIframeInOfficePortal", "Verify validation for mandatoryness.");
                integration_IframeAppsHelper.VerifyText("URLErr", "This field is required.");

                executionLog.Log("VerifyCreatedIframeInOfficePortal", "Click on cancel button.");
                integration_IframeAppsHelper.ClickElement("Cancel");

                executionLog.Log("VerifyCreatedIframeInOfficePortal", "Verify Page title.");
                VerifyTitle("Iframe Apps");

                executionLog.Log("VerifyCreatedIframeInOfficePortal", " Click On Create button.");
                integration_IframeAppsHelper.ClickElement("Create");

                executionLog.Log("VerifyCreatedIframeInOfficePortal", "Verify page title.");
                VerifyTitle("Create Iframe");

                executionLog.Log("VerifyCreatedIframeInOfficePortal", "Enter Tab Name");
                integration_IframeAppsHelper.TypeText("TabName", tab);

                executionLog.Log("VerifyCreatedIframeInOfficePortal", "Enter user Name");
                integration_IframeAppsHelper.TypeText("UserNameInputFieldName", "User");
                integration_IframeAppsHelper.WaitForWorkAround(4000);

                executionLog.Log("VerifyCreatedIframeInOfficePortal", "Enter Password");
                integration_IframeAppsHelper.TypeText("PasswordInputFieldNmae", "1qaz!QAZ");

                executionLog.Log("VerifyCreatedIframeInOfficePortal", "Enter an invalid alphabetical url.");
                integration_IframeAppsHelper.TypeText("LoginURL", "sadada");

                executionLog.Log("VerifyCreatedIframeInOfficePortal", "Verify validation for invalid url.");
                integration_IframeAppsHelper.VerifyText("URLErr2", "Invalid URL");

                executionLog.Log("VerifyCreatedIframeInOfficePortal", "Enter an invalid numerical url.");
                integration_IframeAppsHelper.TypeText("LoginURL", "12222");

                executionLog.Log("VerifyCreatedIframeInOfficePortal", "Verify validation for invalid url.");
                integration_IframeAppsHelper.VerifyText("URLErr2", "Invalid URL");

                executionLog.Log("VerifyCreatedIframeInOfficePortal", "Enter an invalid web address.");
                integration_IframeAppsHelper.TypeText("LoginURL", "www.google.com");

                executionLog.Log("VerifyCreatedIframeInOfficePortal", "Verify validation for invalid url.");
                integration_IframeAppsHelper.VerifyText("URLErr2", "Invalid URL");

                executionLog.Log("VerifyCreatedIframeInOfficePortal", "Enter a valid Login Url");
                integration_IframeAppsHelper.TypeText("LoginURL", _office + "login");

                executionLog.Log("VerifyCreatedIframeInOfficePortal", "Click on mainportal check box.");
                integration_IframeAppsHelper.ClickElement("mainportalCheckbox");

                executionLog.Log("VerifyCreatedIframeInOfficePortal", "Click on Save button");
                integration_IframeAppsHelper.ClickElement("Save");

                executionLog.Log("VerifyCreatedIframeInOfficePortal", "Wait for creation success text.");
                integration_IframeAppsHelper.WaitForText("Iframe created successfully.", 10);

                executionLog.Log("VerifyCreatedIframeInOfficePortal", "Logout from the application.");
                VisitOffice("logout");

                executionLog.Log("VerifyCreatedIframeInOfficePortal", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyCreatedIframeInOfficePortal", "Verify Page title.");
                VerifyTitle("Dashboard");

                var loc = "//span[text()='" + tab + "']";
                executionLog.Log("VerifyCreatedIframeInOfficePortal", "Wait for locator to be present.");
                integration_IframeAppsHelper.WaitForElementPresent(loc, 10);

                executionLog.Log("VerifyCreatedIframeInOfficePortal", "Verify created iframe present on office main portal.");
                integration_IframeAppsHelper.IsElementPresent(loc);

                executionLog.Log("VerifyCreatedIframeInOfficePortal", "Redirect at office admin.");
                VisitOffice("admin");

                executionLog.Log("VerifyCreatedIframeInOfficePortal", "Redirect at iframe apps page.");
                VisitOffice("iframes");

                executionLog.Log("VerifyCreatedIframeInOfficePortal", "Enter iframe name to be searched.");
                integration_IframeAppsHelper.TypeText("SearchTabName", tab);
                integration_IframeAppsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCreatedIframeInOfficePortal", "Click on edit icon.");
                integration_IframeAppsHelper.ClickElement("Edit");

                executionLog.Log("VerifyCreatedIframeInOfficePortal", "Verify Page title.");
                VerifyTitle("Edit Iframe");

                executionLog.Log("VerifyCreatedIframeInOfficePortal", "Uncheck the main portal check box.");
                integration_IframeAppsHelper.ClickElement("mainportalCheckbox");

                executionLog.Log("VerifyCreatedIframeInOfficePortal", "Click on Save button.");
                integration_IframeAppsHelper.ClickElement("Save");

                executionLog.Log("VerifyCreatedIframeInOfficePortal", "Wait for iframe updation success text.");
                integration_IframeAppsHelper.WaitForText("Iframe updated Successfully.", 10);

                executionLog.Log("VerifyCreatedIframeInOfficePortal", "Logout from the application.");
                VisitOffice("logout");

                executionLog.Log("VerifyCreatedIframeInOfficePortal", "Login with valid username and password.");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyCreatedIframeInOfficePortal", "Verify Page title.");
                VerifyTitle("Dashboard");

                executionLog.Log("VerifyCreatedIframeInOfficePortal", "Wait for locator to be present.");
                integration_IframeAppsHelper.WaitForElementPresent(loc, 10);

                executionLog.Log("VerifyCreatedIframeInOfficePortal", "Verify created iframe not present on office main portal.");
                integration_IframeAppsHelper.ElementNotAvailable(loc);

                executionLog.Log("VerifyCreatedIframeInOfficePortal", "Redirect at iframe apps page.");
                VisitOffice("iframes");
                integration_IframeAppsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCreatedIframeInOfficePortal", "Verify page title.");
                VerifyTitle("Iframe Apps");

                executionLog.Log("VerifyCreatedIframeInOfficePortal", "Enter iframe name to  be searched.");
                integration_IframeAppsHelper.TypeText("SearchTabName", tab);
                integration_IframeAppsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCreatedIframeInOfficePortal", "Click on delete button.");
                integration_IframeAppsHelper.ClickElement("ClickOnDelete");

                executionLog.Log("VerifyCreatedIframeInOfficePortal", "Click ok to accept alert message.");
                integration_IframeAppsHelper.AcceptAlert();

                executionLog.Log("VerifyCreatedIframeInOfficePortal", "Wait for deletion success message.");
                integration_IframeAppsHelper.WaitForText("Iframe deleted successfully.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyCreatedIframeInOfficePortal");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Created Iframe In Office Portal");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Created Iframe In Office Portal", "Bug", "Medium", "Iframe Intergration page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Created Iframe In Office Portal");
                        TakeScreenshot("VerifyCreatedIframeInOfficePortal");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyCreatedIframeInOfficePortal.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyCreatedIframeInOfficePortal");
                        string id = loginHelper.getIssueID("Verify Created Iframe In Office Portal");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyCreatedIframeInOfficePortal.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Created Iframe In Office Portal"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Created Iframe In Office Portal");
              //  executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("VerifyCreatedIframeInOfficePortal");
                executionLog.WriteInExcel("Verify Created Iframe In Office Portal", Status, JIRA, "IFrame");
            }
        }
    }
}