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
    public class VerifyCreatedIframeOnPartnerAssociationPage : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin1")]
        [TestCategory("TS7")]
        [TestCategory("Pegasus_Admin1")]
        public void verifyCreatedIframeOnPartnerAssociationPage()
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

                executionLog.Log("VerifyCreatedIframeOnPartnerAssociationPage", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyCreatedIframeOnPartnerAssociationPage", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("VerifyCreatedIframeOnPartnerAssociationPage", "Redirect at admin page");
                VisitOffice("admin");

                executionLog.Log("VerifyCreatedIframeOnPartnerAssociationPage", "Redirect at iframe apps page.");
                VisitOffice("iframes");

                executionLog.Log("VerifyCreatedIframeOnPartnerAssociationPage", "Verify page title.");
                VerifyTitle("Iframe Apps");

                executionLog.Log("VerifyCreatedIframeOnPartnerAssociationPage", " Click On Create button.");
                integration_IframeAppsHelper.ClickElement("Create");

                executionLog.Log("VerifyCreatedIframeOnPartnerAssociationPage", "Verify page title.");
                VerifyTitle("Create Iframe");

                executionLog.Log("VerifyCreatedIframeOnPartnerAssociationPage", "Click on Save button.");
                integration_IframeAppsHelper.ClickElement("Save");

                executionLog.Log("VerifyCreatedIframeOnPartnerAssociationPage", "Verify validation for mandatoryness.");
                integration_IframeAppsHelper.VerifyText("TabNameErr", "This field is required.");

                executionLog.Log("VerifyCreatedIframeOnPartnerAssociationPage", "Verify validation for mandatoryness.");
                integration_IframeAppsHelper.VerifyText("UserNameErr", "This field is required.");

                executionLog.Log("VerifyCreatedIframeOnPartnerAssociationPage", "Verify validation for mandatoryness.");
                integration_IframeAppsHelper.VerifyText("PasswrdErr", "This field is required.");

                executionLog.Log("VerifyCreatedIframeOnPartnerAssociationPage", "Verify validation for mandatoryness.");
                integration_IframeAppsHelper.VerifyText("URLErr", "This field is required.");

                executionLog.Log("VerifyCreatedIframeOnPartnerAssociationPage", "Click on cancel button.");
                integration_IframeAppsHelper.ClickElement("Cancel");

                executionLog.Log("VerifyCreatedIframeOnPartnerAssociationPage", "Verify Page title.");
                VerifyTitle("Iframe Apps");

                executionLog.Log("VerifyCreatedIframeOnPartnerAssociationPage", " Click On Create button.");
                integration_IframeAppsHelper.ClickElement("Create");

                executionLog.Log("VerifyCreatedIframeOnPartnerAssociationPage", "Verify page title.");
                VerifyTitle("Create Iframe");

                executionLog.Log("VerifyCreatedIframeOnPartnerAssociationPage", "Enter Tab Name");
                integration_IframeAppsHelper.TypeText("TabName", tab);

                executionLog.Log("VerifyCreatedIframeOnPartnerAssociationPage", "Enter user Name");
                integration_IframeAppsHelper.TypeText("UserNameInputFieldName", "User");
                integration_IframeAppsHelper.WaitForWorkAround(4000);

                executionLog.Log("VerifyCreatedIframeOnPartnerAssociationPage", "Enter Password");
                integration_IframeAppsHelper.TypeText("PasswordInputFieldNmae", "1qaz!QAZ");

                executionLog.Log("VerifyCreatedIframeOnPartnerAssociationPage", "Enter an invalid alphabetical url.");
                integration_IframeAppsHelper.TypeText("LoginURL", "sadada");

                executionLog.Log("VerifyCreatedIframeOnPartnerAssociationPage", "Verify validation for invalid url.");
                integration_IframeAppsHelper.VerifyText("URLErr2", "Invalid URL");

                executionLog.Log("VerifyCreatedIframeOnPartnerAssociationPage", "Enter an invalid numerical url.");
                integration_IframeAppsHelper.TypeText("LoginURL", "12222");

                executionLog.Log("VerifyCreatedIframeOnPartnerAssociationPage", "Verify validation for invalid url.");
                integration_IframeAppsHelper.VerifyText("URLErr2", "Invalid URL");

                executionLog.Log("VerifyCreatedIframeOnPartnerAssociationPage", "Enter an invalid web address.");
                integration_IframeAppsHelper.TypeText("LoginURL", "www.google.com");

                executionLog.Log("VerifyCreatedIframeOnPartnerAssociationPage", "Verify validation for invalid url.");
                integration_IframeAppsHelper.VerifyText("URLErr2", "Invalid URL");

                executionLog.Log("VerifyCreatedIframeOnPartnerAssociationPage", "Enter a valid Login Url");
                integration_IframeAppsHelper.TypeText("LoginURL", _office + "login");

                executionLog.Log("VerifyCreatedIframeOnPartnerAssociationPage", "Click on partner portal check box.");
                integration_IframeAppsHelper.ClickElement("PartnerPortal");

                executionLog.Log("VerifyCreatedIframeOnPartnerAssociationPage", "Click on Save button");
                integration_IframeAppsHelper.ClickElement("Save");

                executionLog.Log("VerifyCreatedIframeOnPartnerAssociationPage", "Wait for creation success text.");
                integration_IframeAppsHelper.WaitForText("Iframe created successfully.", 10);

                executionLog.Log("VerifyCreatedIframeOnPartnerAssociationPage", "Logout from the application.");
                VisitOffice("logout");

                executionLog.Log("VerifyCreatedIframeOnPartnerAssociationPage", "Login with valid username and password");
                Login("qa.association", "1qaz!QAZ");
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);
                integration_IframeAppsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCreatedIframeOnPartnerAssociationPage", "Verify Page title.");
                VerifyTitle("QA Association - Details");

                var loc = "//span[text()='" + tab + "']";
                executionLog.Log("VerifyCreatedIframeOnPartnerAssociationPage", "Wait for locator to be present.");
                integration_IframeAppsHelper.WaitForElementPresent(loc, 10);

                executionLog.Log("VerifyCreatedIframeOnPartnerAssociationPage", "Verify created iframe present in partner association portal.");
                integration_IframeAppsHelper.IsElementPresent(loc);

                executionLog.Log("VerifyCreatedIframeOnPartnerAssociationPage", "Logout fron the partner portal.");
                VisitOffice("logout");

                executionLog.Log("VerifyCreatedIframeOnPartnerAssociationPage", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyCreatedIframeOnPartnerAssociationPage", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("VerifyCreatedIframeOnPartnerAssociationPage", "Redirect at office admin.");
                VisitOffice("admin");

                executionLog.Log("VerifyCreatedIframeOnPartnerAssociationPage", "Redirect at iframe apps page.");
                VisitOffice("iframes");

                executionLog.Log("VerifyCreatedIframeOnPartnerAssociationPage", "Enter iframe name to be searched.");
                integration_IframeAppsHelper.TypeText("SearchTabName", tab);
                integration_IframeAppsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCreatedIframeOnPartnerAssociationPage", "Click on edit icon.");
                integration_IframeAppsHelper.ClickElement("Edit");

                executionLog.Log("VerifyCreatedIframeOnPartnerAssociationPage", "Verify Page title.");
                VerifyTitle("Edit Iframe");

                executionLog.Log("VerifyCreatedIframeOnPartnerAssociationPage", "Uncheck the partner portal check box.");
                integration_IframeAppsHelper.ClickElement("PartnerPortal");

                executionLog.Log("VerifyCreatedIframeOnPartnerAssociationPage", "Click on Save button.");
                integration_IframeAppsHelper.ClickElement("Save");

                executionLog.Log("VerifyCreatedIframeOnPartnerAssociationPage", "Wait for iframe updation success text.");
                integration_IframeAppsHelper.WaitForText("Iframe updated Successfully.", 10);

                executionLog.Log("VerifyCreatedIframeOnPartnerAssociationPage", "Logout from the application.");
                VisitOffice("logout");

                executionLog.Log("VerifyCreatedIframeOnPartnerAssociationPage", "Login with valid username and password");
                Login("qa.association", "1qaz!QAZ");
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);
                integration_IframeAppsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCreatedIframeOnPartnerAssociationPage", "Verify Page title.");
                VerifyTitle("QA Association - Details");

                executionLog.Log("VerifyCreatedIframeOnPartnerAssociationPage", "Wait for locator to be present.");
                integration_IframeAppsHelper.WaitForElementPresent(loc, 10);

                executionLog.Log("VerifyCreatedIframeOnPartnerAssociationPage", "Verify created iframe not present in partner association portal.");
                integration_IframeAppsHelper.ElementNotAvailable(loc);

                executionLog.Log("VerifyCreatedIframeOnPartnerAssociationPage", "Logout from partner association portal.");
                VisitOffice("logout");

                executionLog.Log("VerifyCreatedIframeOnPartnerAssociationPage", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyCreatedIframeOnPartnerAssociationPage", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("VerifyCreatedIframeOnPartnerAssociationPage", "Redirect at iframe apps page.");
                VisitOffice("iframes");

                executionLog.Log("VerifyCreatedIframeOnPartnerAssociationPage", "Verify page title.");
                VerifyTitle("Iframe Apps");

                executionLog.Log("VerifyCreatedIframeOnPartnerAssociationPage", "Enter iframe name to  be searched.");
                integration_IframeAppsHelper.TypeText("SearchTabName", tab);
                integration_IframeAppsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCreatedIframeOnPartnerAssociationPage", "Click on delete button.");
                integration_IframeAppsHelper.ClickElement("ClickOnDelete");

                executionLog.Log("VerifyCreatedIframeOnPartnerAssociationPage", "Click ok to accept alert message.");
                integration_IframeAppsHelper.AcceptAlert();

                executionLog.Log("VerifyCreatedIframeOnPartnerAssociationPage", "Wait for deletion success message.");
                integration_IframeAppsHelper.WaitForText("Iframe deleted successfully.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyCreatedIframeOnPartnerAssociationPage");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Created Iframe On Partner Association Page");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Created Iframe On Partner Association Page", "Bug", "Medium", "Iframe Intergration page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Created Iframe On Partner Association Page");
                        TakeScreenshot("VerifyCreatedIframeOnPartnerAssociationPage");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyCreatedIframeOnPartnerAssociationPage.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyCreatedIframeOnPartnerAssociationPage");
                        string id = loginHelper.getIssueID("Verify Created Iframe On Partner Association Page");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyCreatedIframeOnPartnerAssociationPage.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Created Iframe On Partner Association Page"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Created Iframe On Partner Association Page");
           //     executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("VerifyCreatedIframeOnPartnerAssociationPage");
                executionLog.WriteInExcel("Verify Created Iframe On Partner Association Page", Status, JIRA, "IFrame");
            }
        }
    }
}


