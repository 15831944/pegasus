using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class VerifyCreatedIframeForPartner : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Corp")]
        [TestCategory("TS8")]
        [TestCategory("Pegasus_Corp")]
        public void verifyCreatedIframeForPartner()
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

                executionLog.Log("VerifyCreatedIframeForPartner", "Login with valid credentials");
                Login(username[0], password[0]);

                executionLog.Log("VerifyCreatedIframeForPartner", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyCreatedIframeForPartner", "Redirect at Iframe apps page.");
                VisitCorp("iframes");
                corpIntegration_IframeAppsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCreatedIframeForPartner", "Verify Page title.");
                VerifyTitle("Iframe Apps");

                executionLog.Log("VerifyCreatedIframeForPartner", "Click on create button.");
                corpIntegration_IframeAppsHelper.ClickJava("Create");
                corpIntegration_IframeAppsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCreatedIframeForPartner", "Verify page title.");
                VerifyTitle("Create Iframe");

                executionLog.Log("VerifyCreatedIframeForPartner", "Click on save button.");
                corpIntegration_IframeAppsHelper.ClickJava("Save");
                //corpIntegration_IframeAppsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCreatedIframeForPartner", "Verify required text for tab name");
                corpIntegration_IframeAppsHelper.VerifyText("TabNameError", "This field is required.");

                executionLog.Log("VerifyCreatedIframeForPartner", "Verify required text for user name.");
                corpIntegration_IframeAppsHelper.VerifyText("UserNameerror", "This field is required.");

                executionLog.Log("VerifyCreatedIframeForPartner", "Verify required text for password.");
                corpIntegration_IframeAppsHelper.VerifyText("PassWordError", "This field is required.");

                executionLog.Log("VerifyCreatedIframeForPartner", "Verify required text for URL.");
                corpIntegration_IframeAppsHelper.VerifyText("URLError", "This field is required.");

                executionLog.Log("VerifyCreatedIframeForPartner", "Click on Cancel button.");
                corpIntegration_IframeAppsHelper.ClickJava("Cancel");
                corpIntegration_IframeAppsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCreatedIframeForPartner", "Verify Page title");
                VerifyTitle("Users");

                executionLog.Log("VerifyCreatedIframeForPartner", "Redirect at Iframe apps page.");
                VisitCorp("iframes");
                corpIntegration_IframeAppsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCreatedIframeForPartner", "Verify Page title");
                VerifyTitle("Iframe Apps");

                executionLog.Log("VerifyCreatedIframeForPartner", "Click on create button.");
                corpIntegration_IframeAppsHelper.ClickJava("Create");
                corpIntegration_IframeAppsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCreatedIframeForPartner", "Verify Page title.");
                VerifyTitle("Create Iframe");

                executionLog.Log("VerifyCreatedIframeForPartner", "Enter tab name.");
                corpIntegration_IframeAppsHelper.TypeText("TabName", Tab);

                executionLog.Log("VerifyCreatedIframeForPartner", "Enter user name field name.");
                corpIntegration_IframeAppsHelper.TypeText("UserName", "User");

                executionLog.Log("VerifyCreatedIframeForPartner", "Enter Password field name");
                corpIntegration_IframeAppsHelper.TypeText("Paasword", "PIN");

                executionLog.Log("VerifyCreatedIframeForPartner", "Enter invalid URL.");
                corpIntegration_IframeAppsHelper.TypeText("LoginUrl", "Abcd@gmail");

                executionLog.Log("VerifyCreatedIframeForPartner", "Verify validation for invalid url.");
                corpIntegration_IframeAppsHelper.VerifyText("URLError2", "Invalid URL");

                executionLog.Log("VerifyCreatedIframeForPartner", "Enter a valid URL");
                corpIntegration_IframeAppsHelper.TypeText("LoginUrl", "https://www.google.co.in");

                executionLog.Log("VerifyCreatedIframeForPartner", "Click on tab appear on chk box.");
                corpIntegration_IframeAppsHelper.ClickJava("TabAppearOnOffice");

                executionLog.Log("VerifyCreatedIframeForPartner", "Click on tab appear on chk box.");
                corpIntegration_IframeAppsHelper.ClickJava("TabAppearClient");

                executionLog.Log("VerifyCreatedIframeForPartner", "Click on tab appear on chk box.");
                corpIntegration_IframeAppsHelper.ClickJava("TabAppearPartner");

                executionLog.Log("VerifyCreatedIframeForPartner", "Enter User Name for Iframe.");
                corpIntegration_IframeAppsHelper.TypeText("UsrNAme", UserName);

                executionLog.Log("VerifyCreatedIframeForPartner", "Enter Password for Iframe.");
                corpIntegration_IframeAppsHelper.TypeText("Passwrd", "Pegasus");

                executionLog.Log("VerifyCreatedIframeForPartner", "Select which office to iframe displayed.");
                corpIntegration_IframeAppsHelper.ClickJava("AllOffices");

                executionLog.Log("VerifyCreatedIframeForPartner", "Click on save button.");
                corpIntegration_IframeAppsHelper.ClickJava("Save");

                executionLog.Log("VerifyCreatedIframeForPartner", "Wait for iframe creation success text.");
                corpIntegration_IframeAppsHelper.WaitForText("Iframe created successfully.", 10);

                executionLog.Log("VerifyCreatedIframeForPartner", "Logout from corp module.");
                VisitCorp("logout");
                //corpIntegration_IframeAppsHelper.WaitForWorkAround(7000);

                if (GetWebDriver().Title == "Login")

                {
                    Login("DilliAgent", "1qaz!QAZ");
                }

                //executionLog.Log("VerifyCreatedIframeForPartner", "Verify page title.");
                //VerifyTitle("Dashboard");
                //corpIntegration_IframeAppsHelper.WaitForWorkAround(5000);

                executionLog.Log("VerifyCreatedIframeForPartner", "Verify created iframe present in office.");
                corpIntegration_IframeAppsHelper.IsElementPresent("//span[text()='" + Tab + "']");
                //corpIntegration_IframeAppsHelper.WaitForWorkAround(5000);

                executionLog.Log("VerifyCreatedIframeForPartner", "Click on the created iframe.");
                corpIntegration_IframeAppsHelper.ClickViaJavaScript("//span[text()='" + Tab + "']");

                executionLog.Log("VerifyCreatedIframeForPartner", "Verify user iframe id present.");
                corpIntegration_IframeAppsHelper.WaitForElementPresent("UserID", 05);

                executionLog.Log("VerifyCreatedIframeForPartner", "Switch to the iframe..");
                corpIntegration_IframeAppsHelper.GetWebDriver().SwitchTo().Frame(0);
                corpIntegration_IframeAppsHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyCreatedIframeForPartner", "Logout from the application.");
                VisitOffice("logout");

                executionLog.Log("VerifyCreatedIframeForPartner", "Login using corp credentials.");
                Login(username[0], password[0]);

                executionLog.Log("VerifyCreatedIframeForPartner", "Verify page title.");
                VerifyTitle("Dashboard");

                executionLog.Log("VerifyCreatedIframeForPartner", "Redirect at Iframe apps page.");
                VisitCorp("iframes");
                corpIntegration_IframeAppsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCreatedIframeForPartner", "Verify Page title.");
                VerifyTitle("Iframe Apps");

                //executionLog.Log("VerifyCreatedIframeForPartner", "Wait for locator to be present.");
                //corpIntegration_IframeAppsHelper.WaitForElementPresent("SearchTabName", 10);

                executionLog.Log("VerifyCreatedIframeForPartner", "Enter tab name to be searched.");
                corpIntegration_IframeAppsHelper.TypeText("SearchTabName", Tab);
                corpIntegration_IframeAppsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyCreatedIframeForPartner", "Click on delete icon.");
                corpIntegration_IframeAppsHelper.ClickJava("Delete");
                corpIntegration_IframeAppsHelper.AcceptAlert();

                executionLog.Log("VerifyCreatedIframeForPartner", "Wait for deletion success.");
                corpIntegration_IframeAppsHelper.WaitForText("Iframe deleted successfully.", 10);

                executionLog.Log("VerifyCreatedIframeForPartner", "Logout from corp module.");
                VisitCorp("logout");
                //corpIntegration_IframeAppsHelper.WaitForWorkAround(7000);

                if (GetWebDriver().Title == "Login")

                {
                    Login("DilliAgent", "1qaz!QAZ");
                }

                //executionLog.Log("VerifyCreatedIframeForPartner", "Verify page title.");
                //VerifyTitle("Dashboard");
                //corpIntegration_IframeAppsHelper.WaitForWorkAround(5000);

                executionLog.Log("VerifyCreatedIframeForPartner", "Verify deleted iframe not present on partner agent.");
                corpIntegration_IframeAppsHelper.ElementNotAvailable("//span[text()='" + Tab + "']");
                //corpIntegration_IframeAppsHelper.WaitForWorkAround(5000);

                executionLog.Log("VerifyCreatedIframeForPartner", "Logout from the application.");
                VisitOffice("logout");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyCreatedIframeForPartner");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("VerifyCreatedIframeForPartner");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("VerifyCreatedIframeForPartner", "Bug", "Medium", "IFrame page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("VerifyCreatedIframeForPartner");
                        TakeScreenshot("VerifyCreatedIframeForPartner");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyCreatedIframeForPartner.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyCreatedIframeForPartner");
                        string id = loginHelper.getIssueID("VerifyCreatedIframeForPartner");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyCreatedIframeForPartner.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("VerifyCreatedIframeForPartner"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("VerifyCreatedIframeForPartner");
               // executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyCreatedIframeForPartner");
                executionLog.WriteInExcel("VerifyCreatedIframeForPartner", Status, JIRA, "Iframe Management");
            }
        }
    }
}