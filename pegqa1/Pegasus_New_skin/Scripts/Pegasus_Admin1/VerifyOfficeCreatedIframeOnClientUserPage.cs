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
    public class VerifyOfficeCreatedIframeOnClientUserPage : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin1")]
        [TestCategory("TS7")]
        [TestCategory("Pegasus_Admin1")]
        public void verifyOfficeCreatedIframeOnClientUserPage()
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

                executionLog.Log("VerifyOfficeCreatedIframeOnClientUserPage", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyOfficeCreatedIframeOnClientUserPage", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("VerifyOfficeCreatedIframeOnClientUserPage", "Redirect at iframe apps page.");
                VisitOffice("iframes");
                integration_IframeAppsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyOfficeCreatedIframeOnClientUserPage", "Verify page title.");
                VerifyTitle("Iframe Apps");

                executionLog.Log("VerifyOfficeCreatedIframeOnClientUserPage", " Click On Create button.");
                integration_IframeAppsHelper.ClickElement("Create");
                integration_IframeAppsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyOfficeCreatedIframeOnClientUserPage", "Verify page title.");
                VerifyTitle("Create Iframe");

                executionLog.Log("VerifyOfficeCreatedIframeOnClientUserPage", "Click on Save button.");
                integration_IframeAppsHelper.ClickElement("Save");

                executionLog.Log("VerifyOfficeCreatedIframeOnClientUserPage", "Verify validation for mandatoryness.");
                integration_IframeAppsHelper.VerifyText("TabNameErr", "This field is required.");

                executionLog.Log("VerifyOfficeCreatedIframeOnClientUserPage", "Verify validation for mandatoryness.");
                integration_IframeAppsHelper.VerifyText("UserNameErr", "This field is required.");

                executionLog.Log("VerifyOfficeCreatedIframeOnClientUserPage", "Verify validation for mandatoryness.");
                integration_IframeAppsHelper.VerifyText("PasswrdErr", "This field is required.");

                executionLog.Log("VerifyOfficeCreatedIframeOnClientUserPage", "Verify validation for mandatoryness.");
                integration_IframeAppsHelper.VerifyText("URLErr", "This field is required.");

                executionLog.Log("VerifyOfficeCreatedIframeOnClientUserPage", "Click on cancel button.");
                integration_IframeAppsHelper.ClickElement("Cancel");

                executionLog.Log("VerifyOfficeCreatedIframeOnClientUserPage", "Verify Page title.");
                VerifyTitle("Iframe Apps");

                executionLog.Log("VerifyOfficeCreatedIframeOnClientUserPage", " Click On Create button.");
                integration_IframeAppsHelper.ClickElement("Create");
                integration_IframeAppsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyOfficeCreatedIframeOnClientUserPage", "Verify page title.");
                VerifyTitle("Create Iframe");

                executionLog.Log("VerifyOfficeCreatedIframeOnClientUserPage", "Enter Tab Name");
                integration_IframeAppsHelper.TypeText("TabName", tab);

                executionLog.Log("VerifyOfficeCreatedIframeOnClientUserPage", "Enter user Name");
                integration_IframeAppsHelper.TypeText("UserNameInputFieldName", "User");
                //integration_IframeAppsHelper.WaitForWorkAround(4000);

                executionLog.Log("VerifyOfficeCreatedIframeOnClientUserPage", "Enter Password");
                integration_IframeAppsHelper.TypeText("PasswordInputFieldNmae", "1qaz!QAZ");

                executionLog.Log("VerifyOfficeCreatedIframeOnClientUserPage", "Enter an invalid alphabetical url.");
                integration_IframeAppsHelper.TypeText("LoginURL", "sadada");

                executionLog.Log("VerifyOfficeCreatedIframeOnClientUserPage", "Verify validation for invalid url.");
                integration_IframeAppsHelper.VerifyText("URLErr2", "Invalid URL");

                executionLog.Log("VerifyOfficeCreatedIframeOnClientUserPage", "Enter an invalid numerical url.");
                integration_IframeAppsHelper.TypeText("LoginURL", "12222");

                executionLog.Log("VerifyOfficeCreatedIframeOnClientUserPage", "Verify validation for invalid url.");
                integration_IframeAppsHelper.VerifyText("URLErr2", "Invalid URL");

                executionLog.Log("VerifyOfficeCreatedIframeOnClientUserPage", "Enter an invalid web address.");
                integration_IframeAppsHelper.TypeText("LoginURL", "www.google.com");

                executionLog.Log("VerifyOfficeCreatedIframeOnClientUserPage", "Verify validation for invalid url.");
                integration_IframeAppsHelper.VerifyText("URLErr2", "Invalid URL");

                executionLog.Log("VerifyOfficeCreatedIframeOnClientUserPage", "Enter a valid Login Url");
                integration_IframeAppsHelper.TypeText("LoginURL", _office + "login");

                executionLog.Log("VerifyOfficeCreatedIframeOnClientUserPage", "Click on client portal check box.");
                integration_IframeAppsHelper.ClickElement("ClientPortal");

                executionLog.Log("VerifyOfficeCreatedIframeOnClientUserPage", "Click on Save button");
                integration_IframeAppsHelper.ClickElement("Save");

                executionLog.Log("VerifyOfficeCreatedIframeOnClientUserPage", "Wait for creation success text.");
                integration_IframeAppsHelper.WaitForText("Iframe created successfully.", 10);

                executionLog.Log("VerifyOfficeCreatedIframeOnClientUserPage", "Logout from the application.");
                VisitOffice("logout");

                executionLog.Log("VerifyOfficeCreatedIframeOnClientUserPage", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);
                integration_IframeAppsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCreatedIframeForClientUser", "Verify page title.");
                VerifyTitle("Dashboard");
                integration_IframeAppsHelper.WaitForWorkAround(5000);

                var loc = "//span[text()='" + tab + "']";
                executionLog.Log("VerifyOfficeCreatedIframeOnClientUserPage", "Wait for locator to be present.");
                integration_IframeAppsHelper.WaitForElementPresent(loc, 10);

                executionLog.Log("VerifyOfficeCreatedIframeOnClientUserPage", "Verify created iframe present in client user portal.");
                integration_IframeAppsHelper.IsElementPresent(loc);

                executionLog.Log("VerifyOfficeCreatedIframeOnClientUserPage", "Logout fron the partner portal.");
                VisitOffice("logout");

                executionLog.Log("VerifyOfficeCreatedIframeOnClientUserPage", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyOfficeCreatedIframeOnClientUserPage", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("VerifyOfficeCreatedIframeOnClientUserPage", "Redirect at iframe apps page.");
                VisitOffice("iframes");
                integration_IframeAppsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyOfficeCreatedIframeOnClientUserPage", "Enter iframe name to be searched.");
                integration_IframeAppsHelper.TypeText("SearchTabName", tab);
                integration_IframeAppsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyOfficeCreatedIframeOnClientUserPage", "Click on edit icon.");
                integration_IframeAppsHelper.ClickElement("Edit");

                executionLog.Log("VerifyOfficeCreatedIframeOnClientUserPage", "Verify Page title.");
                VerifyTitle("Edit Iframe");

                executionLog.Log("VerifyOfficeCreatedIframeOnClientUserPage", "Uncheck the client user check box.");
                integration_IframeAppsHelper.ClickElement("ClientPortal");

                executionLog.Log("VerifyOfficeCreatedIframeOnClientUserPage", "Click on Save button.");
                integration_IframeAppsHelper.ClickElement("Save");

                executionLog.Log("VerifyOfficeCreatedIframeOnClientUserPage", "Wait for iframe updation success text.");
                integration_IframeAppsHelper.WaitForText("Iframe updated Successfully.", 10);

                executionLog.Log("VerifyOfficeCreatedIframeOnClientUserPage", "Logout from the application.");
                VisitOffice("logout");

                executionLog.Log("VerifyOfficeCreatedIframeOnClientUserPage", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyCreatedIframeForClientUser", "Verify page title.");
                VerifyTitle("Dashboard");
                integration_IframeAppsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyOfficeCreatedIframeOnClientUserPage", "Wait for locator to be present.");
                integration_IframeAppsHelper.WaitForElementPresent(loc, 10);

                executionLog.Log("VerifyOfficeCreatedIframeOnClientUserPage", "Verify created iframe not present in client user portal.");
                integration_IframeAppsHelper.ElementNotAvailable(loc);

                executionLog.Log("VerifyOfficeCreatedIframeOnClientUserPage", "Logout from client user portal.");
                VisitOffice("logout");

                executionLog.Log("VerifyOfficeCreatedIframeOnClientUserPage", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);
                integration_IframeAppsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyOfficeCreatedIframeOnClientUserPage", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("VerifyOfficeCreatedIframeOnClientUserPage", "Redirect at iframe apps page.");
                VisitOffice("iframes");
                integration_IframeAppsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyOfficeCreatedIframeOnClientUserPage", "Verify page title.");
                VerifyTitle("Iframe Apps");

                executionLog.Log("VerifyOfficeCreatedIframeOnClientUserPage", "Enter iframe name to  be searched.");
                integration_IframeAppsHelper.TypeText("SearchTabName", tab);
                integration_IframeAppsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyOfficeCreatedIframeOnClientUserPage", "Click on delete button.");
                integration_IframeAppsHelper.ClickElement("ClickOnDelete");

                executionLog.Log("VerifyOfficeCreatedIframeOnClientUserPage", "Click ok to accept alert message.");
                integration_IframeAppsHelper.AcceptAlert();

                executionLog.Log("VerifyOfficeCreatedIframeOnClientUserPage", "Wait for deletion success message.");
                integration_IframeAppsHelper.WaitForText("Iframe deleted successfully.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyOfficeCreatedIframeOnClientUserPage");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Office Created Iframe On Client User Page");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Office Created Iframe On Client User Page", "Bug", "Medium", "Iframe Intergration page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Office Created Iframe On Client User Page");
                        TakeScreenshot("VerifyOfficeCreatedIframeOnClientUserPage");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyOfficeCreatedIframeOnClientUserPage.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyOfficeCreatedIframeOnClientUserPage");
                        string id = loginHelper.getIssueID("Verify Office Created Iframe On Client User Page");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyOfficeCreatedIframeOnClientUserPage.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Office Created Iframe On Client User Page"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Office Created Iframe On Client User Page");
            //    executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("VerifyOfficeCreatedIframeOnClientUserPage");
                executionLog.WriteInExcel("Verify Office Created Iframe On Client User Page", Status, JIRA, "IFrame");
            }
        }
    }
}