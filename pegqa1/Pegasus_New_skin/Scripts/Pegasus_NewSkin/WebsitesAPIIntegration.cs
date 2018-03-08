using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class WebsitesAPIIntegration : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin")]
        [TestCategory("TS8")]
        [TestCategory("Pegasus_NewSkin")]
        public void websitesAPIIntegration()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var integration_APIHelper = new Integration_APIHelper(GetWebDriver());

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Variable
            var FName = "Test" + RandomNumber(99, 99999);
            var LName = "Test" + RandomNumber(99, 99999);
            var CDBA = "New" + RandomNumber(99, 99999);
            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("WebsitesAPIIntegration", "Login with valid credential  Username");
                Login(username[0], password[0]);

                executionLog.Log("WebsitesAPIIntegration", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("WebsitesAPIIntegration", "Visit api codes");
                VisitOffice("api_codes");

                executionLog.Log("WebsitesAPIIntegration", "Click On Create API Codes");
                integration_APIHelper.ClickElement("Create");

                executionLog.Log("WebsitesAPIIntegration", "Wait for locator to be present.");
                integration_APIHelper.WaitForElementPresent("APIKey", 10);

                executionLog.Log("WebsitesAPIIntegration", "Genetrate Code Auto");
                integration_APIHelper.ClickElement("APIKey");

                executionLog.Log("WebsitesAPIIntegration", "Select Status");
                integration_APIHelper.SelectByText("Status", "New");

                executionLog.Log("WebsitesAPIIntegration", "Enter version number.");
                integration_APIHelper.TypeText("Version", "1");

                executionLog.Log("WebsitesAPIIntegration", "Select Responsibilities");
                integration_APIHelper.SelectByText("Responsibilities", "Howard Tang");

                executionLog.Log("WebsitesAPIIntegration", "Save");
                integration_APIHelper.ClickElement("Save");
                integration_APIHelper.WaitForWorkAround(5000);

                executionLog.Log("WebsitesAPIIntegration", "Delete the newly added API code");
                integration_APIHelper.SearchAndDelete();
                integration_APIHelper.WaitForWorkAround(4000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("WebsitesAPIIntegration");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("WebsitesAPIIntegration");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("WebsitesAPIIntegration", "Bug", "Medium", "API page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("WebsitesAPIIntegration");
                        TakeScreenshot("WebsitesAPIIntegration");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\WebsitesAPIIntegration.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("WebsitesAPIIntegration");
                        string id = loginHelper.getIssueID("WebsitesAPIIntegration");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\WebsitesAPIIntegration.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("WebsitesAPIIntegration"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("WebsitesAPIIntegration");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("WebsitesAPIIntegration");
                executionLog.WriteInExcel("WebsitesAPIIntegration", Status, JIRA, "API Integration");
            }
        }
    }
}
