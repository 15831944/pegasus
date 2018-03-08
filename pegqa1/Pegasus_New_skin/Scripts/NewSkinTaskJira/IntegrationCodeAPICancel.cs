using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class IntegrationCodeAPICancel : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS5")]
        [TestCategory("NewSkinTaskJira")]
        public void integrationCodeAPICancel()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var integration_APIHelper = new Integration_APIHelper(GetWebDriver());


            // Variable random
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("IntegrationCodeAPICancel", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("IntegrationCodeAPICancel", "Verify Page title");
                VerifyTitle("Dashboard");
               
                executionLog.Log("IntegrationCodeAPICancel", "Redirect To Admin");
                VisitOffice("admin");
                
                executionLog.Log("IntegrationCodeAPICancel", "Redirect To API Codes page");
                VisitOffice("api_codes");
               
                executionLog.Log("IntegrationCodeAPICancel", "Click on cancel button");
                integration_APIHelper.ClickElement("ClickCanelApi");
                
                executionLog.Log("IntegrationCodeAPICancel", "Verify API Keys");
                integration_APIHelper.WaitForText("API Keys" ,10);
                
            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("IntegrationCodeAPICancel");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Integration Code API Cancel");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Integration Code API Cancel", "Bug", "Medium", "Integration API page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Integration Code API Cancel");
                        TakeScreenshot("IntegrationCodeAPICancel");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\IntegrationCodeAPICancel.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("IntegrationCodeAPICancel");
                        string id = loginHelper.getIssueID("Integration Code API Cancel");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\IntegrationCodeAPICancel.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Integration Code API Cancel"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Integration Code API Cancel");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("IntegrationCodeAPICancel");
                executionLog.WriteInExcel("Integration Code API Cancel", Status, JIRA, "Integration API");
            }
        }
    }
}