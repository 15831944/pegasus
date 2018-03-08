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
    public class DeleteIframeIntegration : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS1")]
        [TestCategory("BugTest")]
        public void deleteIframeIntegration()
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
            var name = "Delete" + GetRandomNumber();
            var usrname = "Test.Tester" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("DeleteIframeIntegration", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("DeleteIframeIntegration", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("DeleteIframeIntegration", "Redirect To Create ");
                VisitOffice("iframes/create");

                executionLog.Log("DeleteIframeIntegration", "Verify page title ");
                VerifyTitle("Create Iframe");
                integration_IframeAppsHelper.WaitForWorkAround(2000);

                executionLog.Log("DeleteIframeIntegration", "Enter Tab Name");
                integration_IframeAppsHelper.TypeText("TabName", name);

                executionLog.Log("DeleteIframeIntegration", "Enter user name");
                integration_IframeAppsHelper.TypeText("UserNameInputFieldName", usrname);

                executionLog.Log("DeleteIframeIntegration", "Enter password input field name");
                integration_IframeAppsHelper.TypeText("PasswordInputFieldNmae", "1qaz!QAZ");

                executionLog.Log("DeleteIframeIntegration", "Enter URL");
                integration_IframeAppsHelper.TypeText("LoginURL", "https://www.mypegasuscrm.com/newthemecorp/newthemeoffice");

                executionLog.Log("DeleteIframeIntegration", "Click on Save");
                integration_IframeAppsHelper.ClickElement("Save");
                integration_IframeAppsHelper.WaitForWorkAround(3000);

                executionLog.Log("DeleteIframeIntegration", "Wait for success message.");
                integration_IframeAppsHelper.WaitForText("Iframe created successfully.", 30);

                executionLog.Log("DeleteIframeIntegration", "Search Iframe by name");
                integration_IframeAppsHelper.TypeText("SearchTabName", name);
                integration_IframeAppsHelper.WaitForWorkAround(1000);

                executionLog.Log("DeleteIframeIntegration", "Clcik on delete");
                integration_IframeAppsHelper.ClickElement("ClickOnDelete");

                executionLog.Log("DeleteIframeIntegration", "Accept alert name.");
                integration_IframeAppsHelper.AcceptAlert();

                executionLog.Log("DeleteIframeIntegration", "Wait for sucess message.");
                integration_IframeAppsHelper.WaitForText("Iframe deleted successfully.", 20);

            }

            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("DeleteIframeIntegration");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Delete Iframe Integration");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Delete Iframe Integration", "Bug", "Medium", "Iframe page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Delete Iframe Integration");
                        TakeScreenshot("DeleteIframeIntegration");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\DeleteIframeIntegration.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("DeleteIframeIntegration");
                        string id = loginHelper.getIssueID("Delete Iframe Integration");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\DeleteIframeIntegration.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Delete Iframe Integration"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Delete Iframe Integration");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("DeleteIframeIntegration");
                executionLog.WriteInExcel("Delete Iframe Integration", Status, JIRA, "Admin Integrations");
            }
        }
    }
}