using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class AdminIntegrationIframe : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS1")]
        [TestCategory("BugTest")]
        public void adminIntegrationIframe()
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
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("AdminIntegrationIframe", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("AdminIntegrationIframe", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("AdminIntegrationIframe", "Redirect To Admin");
                VisitOffice("admin");
                integration_IframeAppsHelper.WaitForWorkAround(1000);

                executionLog.Log("AdminIntegrationIframe", "Goto Ingration >> IFrame");
                VisitOffice("iframes");

                executionLog.Log("AdminIntegrationIframe", "Click On Edit icon of Iframe");
                integration_IframeAppsHelper.ClickElement("Edit");
                integration_IframeAppsHelper.WaitForWorkAround(3000);

                executionLog.Log("AdminIntegrationIframe", "Change page url");
                VisitOffice("iframes/create/11");

                executionLog.Log("AdminIntegrationIframe", "Verify Message You don't have privilege.");
                integration_IframeAppsHelper.WaitForText("You don't have privilege.", 20);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("AdminIntegrationIframe");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Admin Integration Iframe");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Admin Integration Iframe", "Bug", "Medium", "Office Admin", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Admin Integration Iframe");
                        TakeScreenshot("AdminIntegrationIframe");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AdminIntegrationIframe.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("AdminIntegrationIframe");
                        string id = loginHelper.getIssueID("Admin Integration Iframe");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AdminIntegrationIframe.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Admin Integration Iframe"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Admin Integration Iframe");
                // executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("AdminIntegrationIframe");
                executionLog.WriteInExcel("Admin Integration Iframe", Status, JIRA, "Admin Integrations");
            }
        }
    }
}
