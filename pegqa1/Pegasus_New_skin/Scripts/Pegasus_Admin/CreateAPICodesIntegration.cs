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
    public class CreateAPICodesIntegration : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin")]
        [TestCategory("Temp")]
        [TestCategory("TS6")]
        [TestCategory("Pegasus_Admin")]
        public void createAPICodesIntegration()
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
            var integration_APIHelper = new Integration_APIHelper(GetWebDriver());


            // Variable
            var name = "1" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("CreateAPICodesIntegration", " Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("CreateAPICodesIntegration", " Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("CreateAPICodesIntegration", " Click On  Admin");
                VisitOffice("admin");

                executionLog.Log("CreateAPICodesIntegration", " Redirect To URL");
                VisitOffice("api_codes");

                executionLog.Log("CreateAPICodesIntegration", " verify title");
                VerifyTitle("API Codes");

                executionLog.Log("CreateAPICodesIntegration", "  Click On Create");
                integration_APIHelper.ClickElement("Create");
                integration_APIHelper.WaitForWorkAround(2000);

                executionLog.Log("CreateAPICodesIntegration", " Click on Generate API Code");
                integration_APIHelper.ClickElement("APIKey");
                integration_APIHelper.WaitForWorkAround(2000);

                executionLog.Log("CreateAPICodesIntegration", " Enter Version");
                integration_APIHelper.TypeText("Version", name);

                executionLog.Log("CreateAPICodesIntegration", " Select  Status");
                integration_APIHelper.SelectByText("Status", "New");

                executionLog.Log("CreateAPICodesIntegration", " Select responsibilities");
                integration_APIHelper.SelectByText("Responsibilities", "Howard Tang");

                executionLog.Log("CreateAPICodesIntegration", " Click on Save  ");
                integration_APIHelper.ClickElement("Save");
                integration_APIHelper.WaitForWorkAround(7000);

                executionLog.Log("CreateAPICodesIntegration", " Wait for text");
                integration_APIHelper.WaitForText("API Code saved successfully.", 10);

                executionLog.Log("CreateAPICodesIntegration", "Delete the newly added API code");
                integration_APIHelper.SearchAndDelete();
                integration_APIHelper.WaitForWorkAround(4000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CreateAPICodesIntegration");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Create API Codes Integration");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Create API Codes Integration", "Bug", "Medium", "API page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Create API Codes Integration");
                        TakeScreenshot("CreateAPICodesIntegration");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateAPICodesIntegration.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CreateAPICodesIntegration");
                        string id = loginHelper.getIssueID("Create API Codes Integration");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateAPICodesIntegration.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Create API Codes Integration"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Create API Codes Integration");
             //   executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("CreateAPICodesIntegration");
                executionLog.WriteInExcel("Create API Codes Integration", Status, JIRA, "API Management");
            }
        }
    }
}