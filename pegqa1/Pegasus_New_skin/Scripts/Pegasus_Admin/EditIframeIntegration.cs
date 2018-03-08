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
    public class EditIframeIntegration : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin")]
        [TestCategory("TS6")]
        [TestCategory("Pegasus_Admin")]
        public void editIframeIntegration()
        {
            string[] username = null;
            string[] password = null;

            XMLParse oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            //Initializing the objects
            var loginHelper = new LoginHelper(GetWebDriver());
            var executionLog = new ExecutionLog();
            var integration_IframeAppsHelper = new Integration_IframeAppsHelper(GetWebDriver());

            // Variable
            var usrname = "Test.Tester" + GetRandomNumber();
            var tabname = "Iframe" + RandomNumber(1, 9999);
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("EditIframeIntegration", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("EditIframeIntegration", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("EditIframeIntegration", "Click On  Admin");
                VisitOffice("admin");

                executionLog.Log("EditIframeIntegration", "Redirect To URL");
                VisitOffice("iframes");

                executionLog.Log("EditIframeIntegration", "Verify title");
                VerifyTitle("Iframe Apps");

                executionLog.Log("EditIframeIntegration", "Enter Tab name search ");
                integration_IframeAppsHelper.TypeText("SearchTabName", "Edit Iframe");
                integration_IframeAppsHelper.WaitForWorkAround(5000);

                var LOC = "//table[@id='list1']/tbody/tr[2]/td[3]";
                if (integration_IframeAppsHelper.IsElementPresent(LOC))
                {
                    executionLog.Log("EditIframeIntegration", "Click On Edit Btn");
                    integration_IframeAppsHelper.ClickElement("Edit");

                    executionLog.Log("EditIframeIntegration", "Verify title");
                    VerifyTitle("Edit Iframe");

                    executionLog.Log("EditIframeIntegration", "Enter Tab Name");
                    integration_IframeAppsHelper.TypeText("TabName", tabname);

                    executionLog.Log("EditIframeIntegration", "Enter user name input field");
                    integration_IframeAppsHelper.TypeText("UserNameInputFieldName", usrname);

                    executionLog.Log("EditIframeIntegration", "Enter password input field name.");
                    integration_IframeAppsHelper.TypeText("PasswordInputFieldNmae", "1qaz!QAZ");

                    executionLog.Log("EditIframeIntegration", "Enter login url.");
                    integration_IframeAppsHelper.TypeText("LoginURL", _office + "login");

                    executionLog.Log("EditIframeIntegration", "Click on mainportal");
                    integration_IframeAppsHelper.ClickElement("mainportalCheckbox");

                    executionLog.Log("EditIframeIntegration", "Click on Save  ");
                    integration_IframeAppsHelper.ClickElement("Save");

                    executionLog.Log("EditIframeIntegration", "Wait for text");
                    integration_IframeAppsHelper.WaitForText("Iframe Created Successfully.", 10);

                }
                else
                {
                    executionLog.Log("EditIframeIntegration", " Click On Create");
                    integration_IframeAppsHelper.ClickElement("Create");

                    executionLog.Log("EditIframeIntegration", "Verify title");
                    VerifyTitle("Create Iframe");

                    executionLog.Log("EditIframeIntegration", "Enter Tab Name");
                    integration_IframeAppsHelper.TypeText("TabName", tabname);

                    executionLog.Log("EditIframeIntegration", "Enter user name input field");
                    integration_IframeAppsHelper.TypeText("UserNameInputFieldName", usrname);

                    executionLog.Log("EditIframeIntegration", "Enter password input field name.");
                    integration_IframeAppsHelper.TypeText("PasswordInputFieldNmae", "1qaz!QAZ");

                    executionLog.Log("EditIframeIntegration", "Enter login URL");
                    integration_IframeAppsHelper.TypeText("LoginURL", _office + "login");

                    executionLog.Log("EditIframeIntegration", "Click on mainportal");
                    integration_IframeAppsHelper.ClickElement("mainportalCheckbox");

                    executionLog.Log("EditIframeIntegration", "cLICK on Save  ");
                    integration_IframeAppsHelper.ClickElement("Save");

                    executionLog.Log("EditIframeIntegration", "Wait for successful create message");
                    integration_IframeAppsHelper.WaitForText("Iframe created successfully.", 10);

                    executionLog.Log("EditIframeIntegration", "Verify title");
                    VerifyTitle("Iframe Apps");

                    executionLog.Log("EditIframeIntegration", "Enter Tab name search ");
                    integration_IframeAppsHelper.TypeText("SearchTabName", tabname);

                    executionLog.Log("EditIframeIntegration", "Click On Edit Btn");
                    integration_IframeAppsHelper.ClickElement("Edit");

                    executionLog.Log("EditIframeIntegration", "Enter Tab Name");
                    integration_IframeAppsHelper.TypeText("TabName", tabname);

                    executionLog.Log("EditIframeIntegration", "Enter user name input field");
                    integration_IframeAppsHelper.TypeText("UserNameInputFieldName", usrname);

                    executionLog.Log("EditIframeIntegration", "Enter password input field name.");
                    integration_IframeAppsHelper.TypeText("PasswordInputFieldNmae", "1qaz!QAZ");

                    executionLog.Log("EditIframeIntegration", "Enter login URL");
                    integration_IframeAppsHelper.TypeText("LoginURL", _office + "login");

                    executionLog.Log("EditIframeIntegration", "Click on mainportal");
                    integration_IframeAppsHelper.ClickElement("mainportalCheckbox");

                    executionLog.Log("EditIframeIntegration", "cLICK on Save ");
                    integration_IframeAppsHelper.ClickElement("Save");

                    executionLog.Log("EditIframeIntegration", "Wait for text");
                    integration_IframeAppsHelper.WaitForText("Iframe updated Successfully.", 10);

                    executionLog.Log("EditIframeIntegration", "Enter user name to search");
                    integration_IframeAppsHelper.TypeText("SearchTabName", tabname);
                    integration_IframeAppsHelper.WaitForWorkAround(2000);

                    executionLog.Log("EditIframeIntegration", "Click on delete icon");
                    integration_IframeAppsHelper.ClickElement("ClickOnDelete");

                    executionLog.Log("EditIframeIntegration", "Accept alert message. ");
                    integration_IframeAppsHelper.AcceptAlert();

                    executionLog.Log("EditIframeIntegration", "Wait for text");
                    integration_IframeAppsHelper.WaitForText("Iframe deleted successfully.", 10);

                }
            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("EditIframeIntegration");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Edit Iframe Integration");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Edit Iframe Integration", "Bug", "Medium", "Iframe Integration page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Edit Iframe Integration");
                        TakeScreenshot("EditIframeIntegration");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditIframeIntegration.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("EditIframeIntegration");
                        string id = loginHelper.getIssueID("Edit Iframe Integration");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditIframeIntegration.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Edit Iframe Integration"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Edit Iframe Integration");
              //  executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("EditIframeIntegration");
                executionLog.WriteInExcel("Edit Iframe Integration", Status, JIRA, "IFrame");
            }
        }
    }
}