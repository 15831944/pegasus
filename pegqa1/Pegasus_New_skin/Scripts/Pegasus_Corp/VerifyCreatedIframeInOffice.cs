using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class VerifyCreatedIframeInOffice : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Corp")]
        [TestCategory("TS8")]
        [TestCategory("Pegasus_Corp")]
        public void verifyCreatedIframeInOffice()
        {
            string[] username = null;
            string[] username1 = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_corp");
            username1 = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var corpIntegration_IframeAppsHelper = new CorpIntegration_IframeAppsHelper(GetWebDriver());

            // Variable
            var Tab = "Tab" + RandomNumber(99, 999);
            var UserName = "Test" + RandomNumber(99, 999);

            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("VerifyCreatedIframeInOffice", "Login with valid credentials");
                Login(username[0], password[0]);

                executionLog.Log("VerifyCreatedIframeInOffice", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyCreatedIframeInOffice", "Redirect at Iframe apps page.");
                VisitCorp("iframes");
                corpIntegration_IframeAppsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCreatedIframeInOffice", "Verify Page title.");
                VerifyTitle("Iframe Apps");

                executionLog.Log("VerifyCreatedIframeInOffice", "Click on create button.");
                corpIntegration_IframeAppsHelper.ClickJava("Create");
                corpIntegration_IframeAppsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCreatedIframeInOffice", "Verify page title.");
                VerifyTitle("Create Iframe");

                executionLog.Log("VerifyCreatedIframeInOffice", "Click on save button.");
                corpIntegration_IframeAppsHelper.ClickJava("Save");
                //corpIntegration_IframeAppsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCreatedIframeInOffice", "Verify required text for tab name");
                corpIntegration_IframeAppsHelper.VerifyText("TabNameError", "This field is required.");

                executionLog.Log("VerifyCreatedIframeInOffice", "Verify required text for user name.");
                corpIntegration_IframeAppsHelper.VerifyText("UserNameerror", "This field is required.");

                executionLog.Log("VerifyCreatedIframeInOffice", "Verify required text for password.");
                corpIntegration_IframeAppsHelper.VerifyText("PassWordError", "This field is required.");

                executionLog.Log("VerifyCreatedIframeInOffice", "Verify required text for URL.");
                corpIntegration_IframeAppsHelper.VerifyText("URLError", "This field is required.");

                executionLog.Log("VerifyCreatedIframeInOffice", "Click on Cancel button.");
                corpIntegration_IframeAppsHelper.ClickJava("Cancel");
                corpIntegration_IframeAppsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCreatedIframeInOffice", "Verify Page title");
                VerifyTitle("Users");

                executionLog.Log("VerifyCreatedIframeInOffice", "Redirect at Iframe apps page.");
                VisitCorp("iframes");
                corpIntegration_IframeAppsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCreatedIframeInOffice", "Verify Page title");
                VerifyTitle("Iframe Apps");

                executionLog.Log("VerifyCreatedIframeInOffice", "Click on create button.");
                corpIntegration_IframeAppsHelper.ClickJava("Create");
                corpIntegration_IframeAppsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCreatedIframeInOffice", "Verify Page title.");
                VerifyTitle("Create Iframe");

                executionLog.Log("VerifyCreatedIframeInOffice", "Enter tab name.");
                corpIntegration_IframeAppsHelper.TypeText("TabName", Tab);

                executionLog.Log("VerifyCreatedIframeInOffice", "Enter user name field name.");
                corpIntegration_IframeAppsHelper.TypeText("UserName", "User");

                executionLog.Log("VerifyCreatedIframeInOffice", "Enter Password field name");
                corpIntegration_IframeAppsHelper.TypeText("Paasword", "PIN");

                executionLog.Log("VerifyCreatedIframeInOffice", "Enter invalid URL.");
                corpIntegration_IframeAppsHelper.TypeText("LoginUrl", "Abcd@gmail");

                executionLog.Log("VerifyCreatedIframeInOffice", "Verify validation for invalid url.");
                corpIntegration_IframeAppsHelper.VerifyText("URLError2", "Invalid URL");

                executionLog.Log("VerifyCreatedIframeInOffice", "Enter a valid URL");
                corpIntegration_IframeAppsHelper.TypeText("LoginUrl", "https://www.google.co.in");

                executionLog.Log("VerifyCreatedIframeInOffice", "Click on tab appear on chk box.");
                corpIntegration_IframeAppsHelper.ClickJava("TabAppearOnOffice");

                executionLog.Log("VerifyCreatedIframeInOffice", "Click on tab appear on chk box.");
                corpIntegration_IframeAppsHelper.ClickJava("TabAppearClient");

                executionLog.Log("VerifyCreatedIframeInOffice", "Click on tab appear on chk box.");
                corpIntegration_IframeAppsHelper.ClickJava("TabAppearPartner");

                executionLog.Log("VerifyCreatedIframeInOffice", "Enter User Name for Iframe.");
                corpIntegration_IframeAppsHelper.TypeText("UsrNAme", UserName);

                executionLog.Log("VerifyCreatedIframeInOffice", "Enter Password for Iframe.");
                corpIntegration_IframeAppsHelper.TypeText("Passwrd", "Pegasus");

                executionLog.Log("VerifyCreatedIframeInOffice", "Select which office to iframe displayed.");
                corpIntegration_IframeAppsHelper.ClickJava("AllOffices");

                executionLog.Log("VerifyCreatedIframeInOffice", "Click on save button.");
                corpIntegration_IframeAppsHelper.ClickJava("Save");

                executionLog.Log("VerifyCreatedIframeInOffice", "Wait for iframe creation success text.");
                corpIntegration_IframeAppsHelper.WaitForText("Iframe created successfully.", 10);

                executionLog.Log("VerifyCreatedIframeInOffice", "Logout from corp module.");
                VisitCorp("logout");
                //corpIntegration_IframeAppsHelper.WaitForWorkAround(7000);

                executionLog.Log("VerifyCreatedIframeInOffice", "Login using office credentials.");
                Login(username1[0], password[0]);
                corpIntegration_IframeAppsHelper.WaitForWorkAround(3000);

                if (GetWebDriver().Title == "Login")

                {
                    Login(username1[0], password[0]);
                }

                executionLog.Log("VerifyCreatedIframeInOffice", "Verify page title.");
                VerifyTitle("Dashboard");
                //corpIntegration_IframeAppsHelper.WaitForWorkAround(5000);

                executionLog.Log("VerifyCreatedIframeInOffice", "Verify created iframe present in office.");
                corpIntegration_IframeAppsHelper.IsElementPresent("//span[text()='" + Tab + "']");
                //corpIntegration_IframeAppsHelper.WaitForWorkAround(5000);

                executionLog.Log("VerifyCreatedIframeInOffice", "Click on the created iframe.");
                corpIntegration_IframeAppsHelper.ClickViaJavaScript("//span[text()='" + Tab + "']");

                executionLog.Log("VerifyCreatedIframeInOffice", "Verify user iframe id present.");
                corpIntegration_IframeAppsHelper.WaitForElementPresent("UserID", 05);

                executionLog.Log("VerifyCreatedIframeInOffice", "Switch to the iframe..");
                corpIntegration_IframeAppsHelper.GetWebDriver().SwitchTo().Frame(0);
                corpIntegration_IframeAppsHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyCreatedIframeInOffice", "Logout from the application.");
                VisitOffice("logout");

                executionLog.Log("VerifyCreatedIframeInOffice", "Login using corp credentials.");
                Login(username[0], password[0]);

                executionLog.Log("VerifyCreatedIframeInOffice", "Verify page title.");
                VerifyTitle("Dashboard");

                executionLog.Log("VerifyCreatedIframeInOffice", "Redirect at Iframe apps page.");
                VisitCorp("iframes");
                corpIntegration_IframeAppsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCreatedIframeInOffice", "Verify Page title.");
                VerifyTitle("Iframe Apps");

                //executionLog.Log("VerifyCreatedIframeInOffice", "Wait for locator to be present.");
                //corpIntegration_IframeAppsHelper.WaitForElementPresent("SearchTabName", 10);

                executionLog.Log("VerifyCreatedIframeInOffice", "Enter tab name to be searched.");
                corpIntegration_IframeAppsHelper.TypeText("SearchTabName", Tab);
                corpIntegration_IframeAppsHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyCreatedIframeInOffice", "Click on delete icon.");
                corpIntegration_IframeAppsHelper.ClickJava("Delete");
                corpIntegration_IframeAppsHelper.AcceptAlert();

                executionLog.Log("VerifyCreatedIframeInOffice", "Wait for deletion success.");
                corpIntegration_IframeAppsHelper.WaitForText("Iframe deleted successfully.", 10);

                executionLog.Log("VerifyCreatedIframeInOffice", "Logout from corp module.");
                VisitCorp("logout");
                //corpIntegration_IframeAppsHelper.WaitForWorkAround(7000);

                executionLog.Log("VerifyCreatedIframeInOffice", "Login using office credentials.");
                Login(username1[0], password[0]);
                corpIntegration_IframeAppsHelper.WaitForWorkAround(3000);

                if (GetWebDriver().Title == "Login")

                {
                    Login(username1[0], password[0]);
                }

                executionLog.Log("VerifyCreatedIframeInOffice", "Verify page title.");
                VerifyTitle("Dashboard");
                //corpIntegration_IframeAppsHelper.WaitForWorkAround(5000);

                executionLog.Log("VerifyCreatedIframeInOffice", "Verify deleted iframe not present in office.");
                corpIntegration_IframeAppsHelper.ElementNotAvailable("//span[text()='" + Tab + "']");
                //corpIntegration_IframeAppsHelper.WaitForWorkAround(5000);

                executionLog.Log("VerifyCreatedIframeInOffice", "Logout from the application.");
                VisitOffice("logout");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyCreatedIframeInOffice");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("VerifyCreatedIframeInOffice");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("VerifyCreatedIframeInOffice", "Bug", "Medium", "IFrame page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("VerifyCreatedIframeInOffice");
                        TakeScreenshot("VerifyCreatedIframeInOffice");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyCreatedIframeInOffice.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyCreatedIframeInOffice");
                        string id = loginHelper.getIssueID("VerifyCreatedIframeInOffice");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyCreatedIframeInOffice.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("VerifyCreatedIframeInOffice"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("VerifyCreatedIframeInOffice");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyCreatedIframeInOffice");
                executionLog.WriteInExcel("VerifyCreatedIframeInOffice", Status, JIRA, "Iframe Management");
            }
        }
    }
}