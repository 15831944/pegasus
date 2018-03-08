using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class VerifyCreatedIframeForPartnerAssociation : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Corp")]
        [TestCategory("TS8")]
        [TestCategory("Pegasus_Corp")]
        public void verifyCreatedIframeForPartnerAssociation()
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

                executionLog.Log("VerifyCreatedIframeForPartnerAssociation", "Login with valid credentials");
                Login(username[0], password[0]);

                executionLog.Log("VerifyCreatedIframeForPartnerAssociation", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyCreatedIframeForPartnerAssociation", "Redirect at Iframe apps page.");
                VisitCorp("iframes");
                corpIntegration_IframeAppsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCreatedIframeForPartnerAssociation", "Verify Page title.");
                VerifyTitle("Iframe Apps");

                executionLog.Log("VerifyCreatedIframeForPartnerAssociation", "Click on create button.");
                corpIntegration_IframeAppsHelper.ClickJava("Create");
                corpIntegration_IframeAppsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCreatedIframeForPartnerAssociation", "Verify page title.");
                VerifyTitle("Create Iframe");

                executionLog.Log("VerifyCreatedIframeForPartnerAssociation", "Click on save button.");
                corpIntegration_IframeAppsHelper.ClickJava("Save");
                //corpIntegration_IframeAppsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCreatedIframeForPartnerAssociation", "Verify required text for tab name");
                corpIntegration_IframeAppsHelper.VerifyText("TabNameError", "This field is required.");

                executionLog.Log("VerifyCreatedIframeForPartnerAssociation", "Verify required text for user name.");
                corpIntegration_IframeAppsHelper.VerifyText("UserNameerror", "This field is required.");

                executionLog.Log("VerifyCreatedIframeForPartnerAssociation", "Verify required text for password.");
                corpIntegration_IframeAppsHelper.VerifyText("PassWordError", "This field is required.");

                executionLog.Log("VerifyCreatedIframeForPartnerAssociation", "Verify required text for URL.");
                corpIntegration_IframeAppsHelper.VerifyText("URLError", "This field is required.");

                executionLog.Log("VerifyCreatedIframeForPartnerAssociation", "Click on Cancel button.");
                corpIntegration_IframeAppsHelper.ClickJava("Cancel");
                corpIntegration_IframeAppsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCreatedIframeForPartnerAssociation", "Verify Page title");
                VerifyTitle("Users");

                executionLog.Log("VerifyCreatedIframeForPartnerAssociation", "Redirect at Iframe apps page.");
                VisitCorp("iframes");
                corpIntegration_IframeAppsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCreatedIframeForPartnerAssociation", "Verify Page title");
                VerifyTitle("Iframe Apps");

                executionLog.Log("VerifyCreatedIframeForPartnerAssociation", "Click on create button.");
                corpIntegration_IframeAppsHelper.ClickJava("Create");
                corpIntegration_IframeAppsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCreatedIframeForPartnerAssociation", "Verify Page title.");
                VerifyTitle("Create Iframe");

                executionLog.Log("VerifyCreatedIframeForPartnerAssociation", "Enter tab name.");
                corpIntegration_IframeAppsHelper.TypeText("TabName", Tab);

                executionLog.Log("VerifyCreatedIframeForPartnerAssociation", "Enter user name field name.");
                corpIntegration_IframeAppsHelper.TypeText("UserName", "User");

                executionLog.Log("VerifyCreatedIframeForPartnerAssociation", "Enter Password field name");
                corpIntegration_IframeAppsHelper.TypeText("Paasword", "PIN");

                executionLog.Log("VerifyCreatedIframeForPartnerAssociation", "Enter invalid URL.");
                corpIntegration_IframeAppsHelper.TypeText("LoginUrl", "Abcd@gmail");

                executionLog.Log("VerifyCreatedIframeForPartnerAssociation", "Verify validation for invalid url.");
                corpIntegration_IframeAppsHelper.VerifyText("URLError2", "Invalid URL");

                executionLog.Log("VerifyCreatedIframeForPartnerAssociation", "Enter a valid URL");
                corpIntegration_IframeAppsHelper.TypeText("LoginUrl", "https://www.google.co.in");

                executionLog.Log("VerifyCreatedIframeForPartnerAssociation", "Click on tab appear on chk box.");
                corpIntegration_IframeAppsHelper.ClickJava("TabAppearOnOffice");

                executionLog.Log("VerifyCreatedIframeForPartnerAssociation", "Click on tab appear on chk box.");
                corpIntegration_IframeAppsHelper.ClickJava("TabAppearClient");

                executionLog.Log("VerifyCreatedIframeForPartnerAssociation", "Click on tab appear on chk box.");
                corpIntegration_IframeAppsHelper.ClickJava("TabAppearPartner");

                executionLog.Log("VerifyCreatedIframeForPartnerAssociation", "Enter User Name for Iframe.");
                corpIntegration_IframeAppsHelper.TypeText("UsrNAme", UserName);

                executionLog.Log("VerifyCreatedIframeForPartnerAssociation", "Enter Password for Iframe.");
                corpIntegration_IframeAppsHelper.TypeText("Passwrd", "Pegasus");

                executionLog.Log("VerifyCreatedIframeForPartnerAssociation", "Select which office to iframe displayed.");
                corpIntegration_IframeAppsHelper.ClickJava("AllOffices");

                executionLog.Log("VerifyCreatedIframeForPartnerAssociation", "Click on save button.");
                corpIntegration_IframeAppsHelper.ClickJava("Save");

                executionLog.Log("VerifyCreatedIframeForPartnerAssociation", "Wait for iframe creation success text.");
                corpIntegration_IframeAppsHelper.WaitForText("Iframe created successfully.", 10);

                executionLog.Log("VerifyCreatedIframeForPartnerAssociation", "Logout from corp module.");
                VisitCorp("logout");
                //corpIntegration_IframeAppsHelper.WaitForWorkAround(7000);

                executionLog.Log("VerifyCreatedIframeForPartnerAssociation", "Login using partner association credentials.");
                Login("DilliAgent", "1qaz!QAZ");
                corpIntegration_IframeAppsHelper.WaitForWorkAround(3000);

                if (GetWebDriver().Title == "Login")

                {
                    Login("DilliAgent", "1qaz!QAZ");
                }

                //executionLog.Log("VerifyCreatedIframeForPartnerAssociation", "Verify page title.");
                //VerifyTitle("Dashboard");
                //corpIntegration_IframeAppsHelper.WaitForWorkAround(5000);

                executionLog.Log("VerifyCreatedIframeForPartnerAssociation", "Verify created iframe present in partner association page.");
                corpIntegration_IframeAppsHelper.IsElementPresent("//span[text()='" + Tab + "']");
                //corpIntegration_IframeAppsHelper.WaitForWorkAround(5000);

                executionLog.Log("VerifyCreatedIframeForPartnerAssociation", "Click on the created iframe.");
                corpIntegration_IframeAppsHelper.ClickViaJavaScript("//span[text()='" + Tab + "']");

                executionLog.Log("VerifyCreatedIframeForPartnerAssociation", "Verify user iframe id present.");
                corpIntegration_IframeAppsHelper.WaitForElementPresent("UserID", 05);

                executionLog.Log("VerifyCreatedIframeForPartnerAssociation", "Switch to the iframe..");
                corpIntegration_IframeAppsHelper.GetWebDriver().SwitchTo().Frame(0);
                corpIntegration_IframeAppsHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyCreatedIframeForPartnerAssociation", "Logout from the application.");
                VisitOffice("logout");

                executionLog.Log("VerifyCreatedIframeForPartnerAssociation", "Login using corp credentials.");
                Login(username[0], password[0]);

                executionLog.Log("VerifyCreatedIframeForPartnerAssociation", "Verify page title.");
                VerifyTitle("Dashboard");

                executionLog.Log("VerifyCreatedIframeForPartnerAssociation", "Redirect at Iframe apps page.");
                VisitCorp("iframes");
                corpIntegration_IframeAppsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCreatedIframeForPartnerAssociation", "Verify Page title.");
                VerifyTitle("Iframe Apps");

                //executionLog.Log("VerifyCreatedIframeForPartnerAssociation", "Wait for locator to be present.");
                //corpIntegration_IframeAppsHelper.WaitForElementPresent("SearchTabName", 10);

                executionLog.Log("VerifyCreatedIframeForPartnerAssociation", "Enter tab name to be searched.");
                corpIntegration_IframeAppsHelper.TypeText("SearchTabName", Tab);
                corpIntegration_IframeAppsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyCreatedIframeForPartnerAssociation", "Click on delete icon.");
                corpIntegration_IframeAppsHelper.ClickJava("Delete");
                corpIntegration_IframeAppsHelper.AcceptAlert();

                executionLog.Log("VerifyCreatedIframeForPartnerAssociation", "Wait for deletion success.");
                corpIntegration_IframeAppsHelper.WaitForText("Iframe deleted successfully.", 10);

                executionLog.Log("VerifyCreatedIframeForPartnerAssociation", "Logout from corp module.");
                VisitCorp("logout");
                //corpIntegration_IframeAppsHelper.WaitForWorkAround(7000);

                executionLog.Log("VerifyCreatedIframeForPartnerAssociation", "Login using partner association credentials.");
                Login("DilliAgent", "1qaz!QAZ");
                corpIntegration_IframeAppsHelper.WaitForWorkAround(3000);

                if (GetWebDriver().Title == "Login")

                {
                    Login("DilliAgent", "1qaz!QAZ");
                }

                //executionLog.Log("VerifyCreatedIframeForPartnerAssociation", "Verify page title.");
                //VerifyTitle("Dashboard");
                //corpIntegration_IframeAppsHelper.WaitForWorkAround(5000);

                executionLog.Log("VerifyCreatedIframeForPartnerAssociation", "Verify deleted iframe not present in partner association page.");
                corpIntegration_IframeAppsHelper.ElementNotAvailable("//span[text()='" + Tab + "']");
                //corpIntegration_IframeAppsHelper.WaitForWorkAround(5000);

                executionLog.Log("VerifyCreatedIframeForPartnerAssociation", "Logout from the application.");
                VisitOffice("logout");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyCreatedIframeForPartnerAssociation");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("VerifyCreatedIframeForPartnerAssociation");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("VerifyCreatedIframeForPartnerAssociation", "Bug", "Medium", "IFrame page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("VerifyCreatedIframeForPartnerAssociation");
                        TakeScreenshot("VerifyCreatedIframeForPartnerAssociation");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyCreatedIframeForPartnerAssociation.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyCreatedIframeForPartnerAssociation");
                        string id = loginHelper.getIssueID("VerifyCreatedIframeForPartnerAssociation");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyCreatedIframeForPartnerAssociation.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("VerifyCreatedIframeForPartnerAssociation"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("VerifyCreatedIframeForPartnerAssociation");
           //     executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyCreatedIframeForPartnerAssociation");
                executionLog.WriteInExcel("VerifyCreatedIframeForPartnerAssociation", Status, JIRA, "Iframe Management");
            }
        }
    }
}