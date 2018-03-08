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
    public class CreateIframeIntegration : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin")]
        [TestCategory("TS6")]
        [TestCategory("Pegasus_Admin")]
        public void createIframeIntegration()
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
            var name = "Test" + GetRandomNumber();
            var usrname = "Test.Tester" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("CreateIframeIntegration", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("CreateIframeIntegration", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("CreateIframeIntegration", "Click On  Admin");
                VisitOffice("admin");

                executionLog.Log("CreateIframeIntegration", "Redirect To URL");
                VisitOffice("iframes");

                executionLog.Log("CreateIframeIntegration", "Verify title");
                VerifyTitle("Iframe Apps");

                executionLog.Log("CreateIframeIntegration", " Click On Create");
                integration_IframeAppsHelper.ClickElement("Create");

                executionLog.Log("CreateIframeIntegration", "Verify title");
                VerifyTitle("Create Iframe");

                executionLog.Log("CreateIframeIntegration", "Enter Tab Name");
                integration_IframeAppsHelper.TypeText("TabName", name);

                executionLog.Log("CreateIframeIntegration", "Enter user Name");
                integration_IframeAppsHelper.TypeText("UserNameInputFieldName", usrname);
                integration_IframeAppsHelper.WaitForWorkAround(4000);

                executionLog.Log("CreateIframeIntegration", "Enter Password");
                integration_IframeAppsHelper.TypeText("PasswordInputFieldNmae", "1qaz!QAZ");

                executionLog.Log("CreateIframeIntegration", "Enter Login Url");
                integration_IframeAppsHelper.TypeText("LoginURL", _office + "login");

                executionLog.Log("CreateIframeIntegration", "Click on mainportal");
                integration_IframeAppsHelper.ClickElement("mainportalCheckbox");

                executionLog.Log("CreateIframeIntegration", "Click on Save");
                integration_IframeAppsHelper.ClickElement("Save");

                executionLog.Log("CreateIframeIntegration", "Wait for text");
                integration_IframeAppsHelper.WaitForText("Iframe created successfully.", 10);

                executionLog.Log("CreateIframeIntegration", "Redirect To URL");
                VisitOffice("iframes");

                executionLog.Log("CreateIframeIntegration", "Verify title");
                VerifyTitle("Iframe Apps");

                executionLog.Log("CreateIframeIntegration", "Enter Name to search");
                integration_IframeAppsHelper.TypeText("SearchTabName", name);
                integration_IframeAppsHelper.WaitForWorkAround(2000);

                executionLog.Log("CreateIframeIntegration", "cLICK Delete btn  ");
                integration_IframeAppsHelper.ClickElement("ClickOnDelete");

                executionLog.Log("CreateIframeIntegration", "Accept alert message. ");
                integration_IframeAppsHelper.AcceptAlert();

                executionLog.Log("CreateIframeIntegration", "Wait for delete message. ");
                integration_IframeAppsHelper.WaitForText("Iframe deleted successfully.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CreateIframeIntegration");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Create Iframe Integration");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Create Iframe Integration", "Bug", "Medium", "Iframe Intergration page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Create Iframe Integration");
                        TakeScreenshot("CreateIframeIntegration");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateIframeIntegration.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CreateIframeIntegration");
                        string id = loginHelper.getIssueID("Create Iframe Integration");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateIframeIntegration.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Create Iframe Integration"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Create Iframe Integration");
             //   executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("CreateIframeIntegration");
                executionLog.WriteInExcel("Create Iframe Integration", Status, JIRA, "IFrame");
            }
        }
    }
}