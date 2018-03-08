using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class CorpIframeAppsCreation : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Corp")]
        [TestCategory("TS7")]
        [TestCategory("Pegasus_Corp")]
        public void corpIframeAppsCreation()
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

                executionLog.Log("CorpIframeAppsCreation", "Login with valid credentials");
                Login(username[0], password[0]);

                executionLog.Log("CorpIframeAppsCreation", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("CorpIframeAppsCreation", "Redirect at Iframe apps page.");
                VisitCorp("iframes");

                executionLog.Log("CorpIframeAppsCreation", "Verify Page title.");
                VerifyTitle("Iframe Apps");

                executionLog.Log("CorpIframeAppsCreation", "Click on create button.");
                corpIntegration_IframeAppsHelper.ClickJava("Create");

                executionLog.Log("CorpIframeAppsCreation", "Verify page title.");
                VerifyTitle("Create Iframe");

                executionLog.Log("CorpIframeAppsCreation", "Click on save button.");
                corpIntegration_IframeAppsHelper.ClickJava("Save");
                corpIntegration_IframeAppsHelper.WaitForWorkAround(3000);

                executionLog.Log("CorpIframeAppsCreation", "Verify required text for tab name");
                corpIntegration_IframeAppsHelper.VerifyText("TabNameError", "This field is required.");

                executionLog.Log("CorpIframeAppsCreation", "Verify required text for user name.");
                corpIntegration_IframeAppsHelper.VerifyText("UserNameerror", "This field is required.");

                executionLog.Log("CorpIframeAppsCreation", "Verify required text for password.");
                corpIntegration_IframeAppsHelper.VerifyText("PassWordError", "This field is required.");

                executionLog.Log("CorpIframeAppsCreation", "Verify required text for URL.");
                corpIntegration_IframeAppsHelper.VerifyText("URLError", "This field is required.");

                executionLog.Log("CorpIframeAppsCreation", "Click on Cancel button.");
                corpIntegration_IframeAppsHelper.ClickJava("Cancel");
                corpIntegration_IframeAppsHelper.WaitForWorkAround(3000);

                executionLog.Log("CorpIframeAppsCreation", "Verify Page title");
                VerifyTitle("Users");

                executionLog.Log("CorpIframeAppsCreation", "Redirect at Iframe apps page.");
                VisitCorp("iframes");

                executionLog.Log("CorpIframeAppsCreation", "Verify Page title");
                VerifyTitle("Iframe Apps");

                executionLog.Log("CorpIframeAppsCreation", "Click on create button.");
                corpIntegration_IframeAppsHelper.ClickJava("Create");

                executionLog.Log("CorpIframeAppsCreation", "Verify Page title.");
                VerifyTitle("Create Iframe");

                executionLog.Log("CorpIframeAppsCreation", "Enter tab name.");
                corpIntegration_IframeAppsHelper.TypeText("TabName", Tab);

                executionLog.Log("CorpIframeAppsCreation", "Enter user name field name.");
                corpIntegration_IframeAppsHelper.TypeText("UserName", "User");

                executionLog.Log("CorpIframeAppsCreation", "Enter Password field name");
                corpIntegration_IframeAppsHelper.TypeText("Paasword", "PIN");

                executionLog.Log("CorpIframeAppsCreation", "Enter invalid URL.");
                corpIntegration_IframeAppsHelper.TypeText("LoginUrl", "Abcd@gmail");

                executionLog.Log("CorpIframeAppsCreation", "Verify validation for invalid url.");
                corpIntegration_IframeAppsHelper.VerifyText("URLError2", "Invalid URL");

                executionLog.Log("CorpIframeAppsCreation", "Enter a valid URL");
                corpIntegration_IframeAppsHelper.TypeText("LoginUrl", "https://www.google.co.in");

                executionLog.Log("CorpIframeAppsCreation", "Click on tab appear on chk box.");
                corpIntegration_IframeAppsHelper.ClickJava("TabAppearOnOffice");

                executionLog.Log("CorpIframeAppsCreation", "Click on tab appear on chk box.");
                corpIntegration_IframeAppsHelper.ClickJava("TabAppearClient");

                executionLog.Log("CorpIframeAppsCreation", "Click on tab appear on chk box.");
                corpIntegration_IframeAppsHelper.ClickJava("TabAppearPartner");

                executionLog.Log("CorpIframeAppsCreation", "Enter User Name for Iframe.");
                corpIntegration_IframeAppsHelper.TypeText("UsrNAme", UserName);

                executionLog.Log("CorpIframeAppsCreation", "Enter Password for Iframe.");
                corpIntegration_IframeAppsHelper.TypeText("Passwrd", "Pegasus");

                executionLog.Log("CorpIframeAppsCreation", "Select which office to iframe displayed.");
                corpIntegration_IframeAppsHelper.ClickJava("AllOffices");

                executionLog.Log("CorpIframeAppsCreation", "Click on save button.");
                corpIntegration_IframeAppsHelper.ClickJava("Save");

                executionLog.Log("CorpIframeAppsCreation", "Wait for iframe creation success text.");
                corpIntegration_IframeAppsHelper.WaitForText("Iframe created successfully.", 10);

                executionLog.Log("CorpIframeAppsCreation", "Wait for locator to be present.");
                corpIntegration_IframeAppsHelper.WaitForElementPresent("SearchTabName", 10);

                executionLog.Log("CorpIframeAppsCreation", "Enter tab name to be searched.");
                corpIntegration_IframeAppsHelper.TypeText("SearchTabName", Tab);
                corpIntegration_IframeAppsHelper.WaitForWorkAround(3000);

                executionLog.Log("CorpIframeAppsCreation", "Click on edit icon.");
                corpIntegration_IframeAppsHelper.ClickElement("Edit");

                executionLog.Log("CorpIframeAppsCreation", "Verify Page title");
                VerifyTitle("Edit Iframe");

                executionLog.Log("CorpIframeAppsCreation", "Remove partner from tab appear.");
                corpIntegration_IframeAppsHelper.ClickJava("TabAppearPartner");

                executionLog.Log("CorpIframeAppsCreation", "Type incorrect url");
                corpIntegration_IframeAppsHelper.TypeText("LoginUrl", "Abcd@gmail");

                executionLog.Log("CorpIframeAppsCreation", "Verify validation text for invalid url.");
                corpIntegration_IframeAppsHelper.VerifyText("URLError2", "Invalid URL");

                executionLog.Log("CorpIframeAppsCreation", "Enter a valid url.");
                corpIntegration_IframeAppsHelper.TypeText("LoginUrl", "https://www.google.co.in");

                executionLog.Log("CorpIframeAppsCreation", "Click on save button.");
                corpIntegration_IframeAppsHelper.ClickJava("Save");

                executionLog.Log("CorpIframeAppsCreation", "wait for updation success message.");
                corpIntegration_IframeAppsHelper.WaitForText("Iframe updated Successfully.", 10);

                executionLog.Log("CorpIframeAppsCreation", "Wait for locator to be present.");
                corpIntegration_IframeAppsHelper.WaitForElementPresent("SearchTabName", 10);

                executionLog.Log("CorpIframeAppsCreation", "Enter tab name to be searched.");
                corpIntegration_IframeAppsHelper.TypeText("SearchTabName", Tab);
                corpIntegration_IframeAppsHelper.WaitForWorkAround(3000);

                executionLog.Log("CorpIframeAppsCreation", "Click on delete icon.");
                corpIntegration_IframeAppsHelper.ClickJava("Delete");
                corpIntegration_IframeAppsHelper.AcceptAlert();

                executionLog.Log("CorpIframeAppsCreation", "Wait for deletion sucess.");
                corpIntegration_IframeAppsHelper.WaitForText("Iframe deleted successfully.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CorpIframeAppsCreation");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("CorpIframeAppsCreation");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("CorpIframeAppsCreation", "Bug", "Medium", "IFrame page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("CorpIframeAppsCreation");
                        TakeScreenshot("CorpIframeAppsCreation");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CorpIframeAppsCreation.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CorpIframeAppsCreation");
                        string id = loginHelper.getIssueID("CorpIframeAppsCreation");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CorpIframeAppsCreation.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("CorpIframeAppsCreation"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("CorpIframeAppsCreation");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("CorpIframeAppsCreation");
                executionLog.WriteInExcel("CorpIframeAppsCreation", Status, JIRA, "Iframe Management");
            }
        }
    }
}
