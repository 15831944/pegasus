using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class VerifyCorpCreatedIframeOnSaleAgentPage : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Corp")]
        [TestCategory("TS8")]
        [TestCategory("Pegasus_Corp")]
        public void verifyCorpCreatedIframeOnSaleAgentPage()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_corp");
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
                executionLog.Log("VerifyCorpCreatedIframeOnSaleAgentPage", "Login with valid credentials");
                Login(username[0], password[0]);

                executionLog.Log("VerifyCorpCreatedIframeOnSaleAgentPage", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyCorpCreatedIframeOnSaleAgentPage", "Redirect at Iframe apps page.");
                VisitCorp("iframes");
                corpIntegration_IframeAppsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCorpCreatedIframeOnSaleAgentPage", "Verify Page title.");
                VerifyTitle("Iframe Apps");

                executionLog.Log("VerifyCorpCreatedIframeOnSaleAgentPage", "Click on create button.");
                corpIntegration_IframeAppsHelper.ClickJava("Create");
                corpIntegration_IframeAppsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCorpCreatedIframeOnSaleAgentPage", "Verify page title.");
                VerifyTitle("Create Iframe");

                executionLog.Log("VerifyCorpCreatedIframeOnSaleAgentPage", "Click on save button.");
                corpIntegration_IframeAppsHelper.ClickJava("Save");
                //corpIntegration_IframeAppsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCorpCreatedIframeOnSaleAgentPage", "Verify required text for tab name");
                corpIntegration_IframeAppsHelper.VerifyText("TabNameError", "This field is required.");

                executionLog.Log("VerifyCorpCreatedIframeOnSaleAgentPage", "Verify required text for user name.");
                corpIntegration_IframeAppsHelper.VerifyText("UserNameerror", "This field is required.");

                executionLog.Log("VerifyCorpCreatedIframeOnSaleAgentPage", "Verify required text for password.");
                corpIntegration_IframeAppsHelper.VerifyText("PassWordError", "This field is required.");

                executionLog.Log("VerifyCorpCreatedIframeOnSaleAgentPage", "Verify required text for URL.");
                corpIntegration_IframeAppsHelper.VerifyText("URLError", "This field is required.");

                executionLog.Log("VerifyCorpCreatedIframeOnSaleAgentPage", "Click on Cancel button.");
                corpIntegration_IframeAppsHelper.ClickJava("Cancel");
                corpIntegration_IframeAppsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCorpCreatedIframeOnSaleAgentPage", "Verify Page title");
                VerifyTitle("Users");

                executionLog.Log("VerifyCorpCreatedIframeOnSaleAgentPage", "Redirect at Iframe apps page.");
                VisitCorp("iframes");
                corpIntegration_IframeAppsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCorpCreatedIframeOnSaleAgentPage", "Verify Page title");
                VerifyTitle("Iframe Apps");

                executionLog.Log("VerifyCorpCreatedIframeOnSaleAgentPage", "Click on create button.");
                corpIntegration_IframeAppsHelper.ClickJava("Create");
                corpIntegration_IframeAppsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCorpCreatedIframeOnSaleAgentPage", "Verify Page title.");
                VerifyTitle("Create Iframe");

                executionLog.Log("VerifyCorpCreatedIframeOnSaleAgentPage", "Enter tab name.");
                corpIntegration_IframeAppsHelper.TypeText("TabName", Tab);

                executionLog.Log("VerifyCorpCreatedIframeOnSaleAgentPage", "Enter user name field name.");
                corpIntegration_IframeAppsHelper.TypeText("UserName", "User");

                executionLog.Log("VerifyCorpCreatedIframeOnSaleAgentPage", "Enter Password field name");
                corpIntegration_IframeAppsHelper.TypeText("Paasword", "PIN");

                executionLog.Log("VerifyCorpCreatedIframeOnSaleAgentPage", "Enter invalid alphabetical URL.");
                corpIntegration_IframeAppsHelper.TypeText("LoginUrl", "Abcd@gmail");

                executionLog.Log("VerifyCorpCreatedIframeOnSaleAgentPage", "Verify validation for invalid url.");
                corpIntegration_IframeAppsHelper.VerifyText("URLError2", "Invalid URL");

                executionLog.Log("VerifyCorpCreatedIframeOnSaleAgentPage", "Enter invalid numerical URL.");
                corpIntegration_IframeAppsHelper.TypeText("LoginUrl", "12234");

                executionLog.Log("VerifyCorpCreatedIframeOnSaleAgentPage", "Verify validation for invalid url.");
                corpIntegration_IframeAppsHelper.VerifyText("URLError2", "Invalid URL");

                executionLog.Log("VerifyCorpCreatedIframeOnSaleAgentPage", "Enter invalid web address URL.");
                corpIntegration_IframeAppsHelper.TypeText("LoginUrl", "www.google.com");

                executionLog.Log("VerifyCorpCreatedIframeOnSaleAgentPage", "Verify validation for invalid url.");
                corpIntegration_IframeAppsHelper.VerifyText("URLError2", "Invalid URL");

                executionLog.Log("VerifyCorpCreatedIframeOnSaleAgentPage", "Enter a valid URL");
                corpIntegration_IframeAppsHelper.TypeText("LoginUrl", "https://www.google.co.in");

                executionLog.Log("VerifyCorpCreatedIframeOnSaleAgentPage", "Click on tab to be appear in office portal.");
                corpIntegration_IframeAppsHelper.ClickJava("TabAppearOnOffice");

                executionLog.Log("VerifyCorpCreatedIframeOnSaleAgentPage", "Click on tab to be appear in partner portal.");
                corpIntegration_IframeAppsHelper.ClickJava("TabAppearPartner");

                executionLog.Log("VerifyCorpCreatedIframeOnSaleAgentPage", "Enter User Name for Iframe.");
                corpIntegration_IframeAppsHelper.TypeText("UsrNAme", UserName);

                executionLog.Log("VerifyCorpCreatedIframeOnSaleAgentPage", "Enter Password for Iframe.");
                corpIntegration_IframeAppsHelper.TypeText("Passwrd", "Pegasus");

                executionLog.Log("VerifyCorpCreatedIframeOnSaleAgentPage", "Select which office to iframe displayed.");
                corpIntegration_IframeAppsHelper.ClickJava("AllOffices");

                executionLog.Log("VerifyCorpCreatedIframeOnSaleAgentPage", "Click on save button.");
                corpIntegration_IframeAppsHelper.ClickJava("Save");

                executionLog.Log("VerifyCorpCreatedIframeOnSaleAgentPage", "Wait for iframe creation success text.");
                corpIntegration_IframeAppsHelper.WaitForText("Iframe created successfully.", 10);

                executionLog.Log("VerifyCorpCreatedIframeOnSaleAgentPage", "Logout from corp module.");
                VisitCorp("logout");
                //corpIntegration_IframeAppsHelper.WaitForWorkAround(7000);

                executionLog.Log("VerifyCorpCreatedIframeOnSaleAgentPage", "Login using sales agent credentials.");
                Login("DilliAgent", "1qaz!QAZ");
                corpIntegration_IframeAppsHelper.WaitForWorkAround(3000);

                if (GetWebDriver().Title == "Login")

                {
                    Login("DilliAgent", "1qaz!QAZ");
                }

                executionLog.Log("VerifyCorpCreatedIframeOnSaleAgentPage", "Verify page title.");
                VerifyTitle("Details");
                //corpIntegration_IframeAppsHelper.WaitForWorkAround(3000);

                var loc = "//span[text()='" + Tab + "']";
                executionLog.Log("VerifyCorpCreatedIframeOnSaleAgentPage", "Verify created iframe present on sale agent portal.");
                corpIntegration_IframeAppsHelper.IsElementPresent(loc);
                //corpIntegration_IframeAppsHelper.WaitForWorkAround(5000);

                executionLog.Log("VerifyCorpCreatedIframeOnSaleAgentPage", "Click on the created iframe.");
                corpIntegration_IframeAppsHelper.ClickViaJavaScript(loc);

                executionLog.Log("VerifyCorpCreatedIframeOnSaleAgentPage", "Verify user iframe id present.");
                corpIntegration_IframeAppsHelper.WaitForElementPresent("UserID", 10);

                executionLog.Log("VerifyCorpCreatedIframeOnSaleAgentPage", "Switch to the iframe..");
                corpIntegration_IframeAppsHelper.GetWebDriver().SwitchTo().Frame(0);
                corpIntegration_IframeAppsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCorpCreatedIframeOnSaleAgentPage", "Verify page title as iframe name..");
                VerifyTitle(Tab);

                executionLog.Log("VerifyCorpCreatedIframeOnSaleAgentPage", "Logout from the application.");
                VisitOffice("logout");

                executionLog.Log("VerifyCorpCreatedIframeOnSaleAgentPage", "Login using corp credentials.");
                Login(username[0], password[0]);

                executionLog.Log("VerifyCorpCreatedIframeOnSaleAgentPage", "Verify page title.");
                VerifyTitle("Dashboard");

                executionLog.Log("VerifyCorpCreatedIframeOnSaleAgentPage", "Redirect at Iframe apps page.");
                VisitCorp("iframes");
                corpIntegration_IframeAppsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCorpCreatedIframeOnSaleAgentPage", "Verify Page title.");
                VerifyTitle("Iframe Apps");

                //executionLog.Log("VerifyCorpCreatedIframeOnSaleAgentPage", "Wait for locator to be present.");
                //corpIntegration_IframeAppsHelper.WaitForElementPresent("SearchTabName", 10);

                executionLog.Log("VerifyCorpCreatedIframeOnSaleAgentPage", "Enter tab name to be searched.");
                corpIntegration_IframeAppsHelper.TypeText("SearchTabName", Tab);
                corpIntegration_IframeAppsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyCorpCreatedIframeOnSaleAgentPage", "Click on edit icon.");
                corpIntegration_IframeAppsHelper.ClickJava("Edit");
                corpIntegration_IframeAppsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyCorpCreatedIframeOnSaleAgentPage", "Verify page title as edit iframe.");
                VerifyTitle("Edit Iframe");

                executionLog.Log("VerifyCorpCreatedIframeOnSaleAgentPage", "Click to uncheck the chechbox.");
                corpIntegration_IframeAppsHelper.ClickJava("TabAppearOnOffice");

                executionLog.Log("VerifyCorpCreatedIframeOnSaleAgentPage", "Click to uncheck the chechbox.");
                corpIntegration_IframeAppsHelper.ClickJava("TabAppearPartner");

                executionLog.Log("VerifyCorpCreatedIframeOnSaleAgentPage", "Click on save button.");
                corpIntegration_IframeAppsHelper.ClickJava("Save");

                executionLog.Log("VerifyCorpCreatedIframeOnSaleAgentPage", "Wait for iframe creation success text.");
                corpIntegration_IframeAppsHelper.WaitForText("Iframe updated Successfully.", 10);

                executionLog.Log("VerifyCorpCreatedIframeOnSaleAgentPage", "logout from corp module.");
                VisitCorp("logout");

                executionLog.Log("VerifyCorpCreatedIframeOnSaleAgentPage", "Login using sale agent credentials.");
                Login("DilliAgent", "1qaz!QAZ");
                corpIntegration_IframeAppsHelper.WaitForWorkAround(3000);

                if (GetWebDriver().Title == "Login")

                {
                    Login("DilliAgent", "1qaz!QAZ");
                }

                executionLog.Log("VerifyCorpCreatedIframeOnSaleAgentPage", "Verify page title.");
                VerifyTitle("Details");
                //corpIntegration_IframeAppsHelper.WaitForWorkAround(5000);

                executionLog.Log("VerifyCorpCreatedIframeOnSaleAgentPage", "Verify deleted iframe not present in sale agent portal.");
                corpIntegration_IframeAppsHelper.ElementNotAvailable(loc);
                //corpIntegration_IframeAppsHelper.WaitForWorkAround(5000);

                executionLog.Log("VerifyCorpCreatedIframeOnSaleAgentPage", "Logout from the application.");
                VisitOffice("logout");

                executionLog.Log("VerifyCorpCreatedIframeOnSaleAgentPage", "Login using corp credentials.");
                Login(username[0], password[0]);

                executionLog.Log("VerifyCorpCreatedIframeOnSaleAgentPage", "Verify page title.");
                VerifyTitle("Dashboard");

                executionLog.Log("VerifyCorpCreatedIframeOnSaleAgentPage", "Redirect at Iframe apps page.");
                VisitCorp("iframes");
                corpIntegration_IframeAppsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCorpCreatedIframeOnSaleAgentPage", "Verify Page title.");
                VerifyTitle("Iframe Apps");

                //executionLog.Log("VerifyCorpCreatedIframeOnSaleAgentPage", "Wait for locator to be present.");
                //corpIntegration_IframeAppsHelper.WaitForElementPresent("SearchTabName", 10);

                executionLog.Log("VerifyCorpCreatedIframeOnSaleAgentPage", "Enter tab name to be searched.");
                corpIntegration_IframeAppsHelper.TypeText("SearchTabName", Tab);
                corpIntegration_IframeAppsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyCorpCreatedIframeOnSaleAgentPage", "Click on delete icon.");
                corpIntegration_IframeAppsHelper.ClickJava("Delete");
                corpIntegration_IframeAppsHelper.AcceptAlert();

                executionLog.Log("VerifyCorpCreatedIframeOnSaleAgentPage", "Wait for deletion success.");
                corpIntegration_IframeAppsHelper.WaitForText("Iframe deleted successfully.", 10);

                executionLog.Log("VerifyCorpCreatedIframeOnSaleAgentPage", "Logout from corp module.");
                VisitCorp("logout");
                //corpIntegration_IframeAppsHelper.WaitForWorkAround(7000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                Console.WriteLine("Counter value is    " + counter);
                String Description = executionLog.GetAllTextFile("VerifyCorpCreatedIframeOnSaleAgentPage");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("VerifyCorpCreatedIframeOnSaleAgentPage");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("VerifyCorpCreatedIframeOnSaleAgentPage", "Bug", "Medium", "IFrame page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("VerifyCorpCreatedIframeOnSaleAgentPage");
                        TakeScreenshot("VerifyCorpCreatedIframeOnSaleAgentPage");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyCorpCreatedIframeOnSaleAgentPage.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyCorpCreatedIframeOnSaleAgentPage");
                        string id = loginHelper.getIssueID("VerifyCorpCreatedIframeOnSaleAgentPage");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyCorpCreatedIframeOnSaleAgentPage.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("VerifyCorpCreatedIframeOnSaleAgentPage"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("VerifyCorpCreatedIframeOnSaleAgentPage");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyCorpCreatedIframeOnSaleAgentPage");
                executionLog.WriteInExcel("VerifyCorpCreatedIframeOnSaleAgentPage", Status, JIRA, "Iframe Management");
            }
        }
    }
}
