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
    public class MultipleIframeDeleteError : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin")]
        [TestCategory("TS2")]
        [TestCategory("BugTestNew")]
        public void multipleIframeDeleteError()
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
            var corpIntegration_IframeAppsHelper = new CorpIntegration_IframeAppsHelper(GetWebDriver());

            // Variable
            var name = "Test" + GetRandomNumber();
            var name2 = "Iframe" + GetRandomNumber();
            var usrname = "Test.Tester" + GetRandomNumber();
            var Tab = "Tab" + RandomNumber(99, 999);
            var Tab2 = "Iframe" + RandomNumber(99, 999);
            var UserName = "Test" + RandomNumber(99, 999);



            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("MultipleIframeDeleteError", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("MultipleIframeDeleteError", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("MultipleIframeDeleteError", "Click On  Admin");
                VisitOffice("admin");

                executionLog.Log("MultipleIframeDeleteError", "Redirect To URL");
                VisitOffice("iframes");

                executionLog.Log("MultipleIframeDeleteError", "Verify title");
                VerifyTitle("Iframe Apps");

                executionLog.Log("MultipleIframeDeleteError", " Click On Create");
                integration_IframeAppsHelper.ClickElement("Create");

                executionLog.Log("MultipleIframeDeleteError", "Verify title");
                VerifyTitle("Create Iframe");

                executionLog.Log("MultipleIframeDeleteError", "Enter Tab Name");
                integration_IframeAppsHelper.TypeText("TabName", name);

                executionLog.Log("MultipleIframeDeleteError", "Enter user Name");
                integration_IframeAppsHelper.TypeText("UserNameInputFieldName", usrname);
                integration_IframeAppsHelper.WaitForWorkAround(4000);

                executionLog.Log("MultipleIframeDeleteError", "Enter Password");
                integration_IframeAppsHelper.TypeText("PasswordInputFieldNmae", "1qaz!QAZ");

                executionLog.Log("MultipleIframeDeleteError", "Enter Login Url");
                integration_IframeAppsHelper.TypeText("LoginURL", _office + "login");

                executionLog.Log("MultipleIframeDeleteError", "Click on mainportal");
                integration_IframeAppsHelper.ClickElement("mainportalCheckbox");

                executionLog.Log("MultipleIframeDeleteError", "Click on Save");
                integration_IframeAppsHelper.ClickElement("Save");

                executionLog.Log("MultipleIframeDeleteError", "Wait for text");
                integration_IframeAppsHelper.WaitForText("Iframe created successfully.", 10);

                executionLog.Log("MultipleIframeDeleteError", "Redirect To URL");
                VisitOffice("iframes");

                executionLog.Log("MultipleIframeDeleteError", "Verify title");
                VerifyTitle("Iframe Apps");

                executionLog.Log("MultipleIframeDeleteError", " Click On Create");
                integration_IframeAppsHelper.ClickElement("Create");

                executionLog.Log("MultipleIframeDeleteError", "Verify title");
                VerifyTitle("Create Iframe");

                executionLog.Log("MultipleIframeDeleteError", "Enter Tab Name");
                integration_IframeAppsHelper.TypeText("TabName", name2);

                executionLog.Log("MultipleIframeDeleteError", "Enter user Name");
                integration_IframeAppsHelper.TypeText("UserNameInputFieldName", usrname);
                integration_IframeAppsHelper.WaitForWorkAround(4000);

                executionLog.Log("MultipleIframeDeleteError", "Enter Password");
                integration_IframeAppsHelper.TypeText("PasswordInputFieldNmae", "1qaz!QAZ");

                executionLog.Log("MultipleIframeDeleteError", "Enter Login Url");
                integration_IframeAppsHelper.TypeText("LoginURL", _office + "login");

                executionLog.Log("MultipleIframeDeleteError", "Click on mainportal");
                integration_IframeAppsHelper.ClickElement("mainportalCheckbox");

                executionLog.Log("MultipleIframeDeleteError", "Click on Save");
                integration_IframeAppsHelper.ClickElement("Save");

                executionLog.Log("MultipleIframeDeleteError", "Wait for text");
                integration_IframeAppsHelper.WaitForText("Iframe created successfully.", 10);

                executionLog.Log("MultipleIframeDeleteError", "Redirect To URL");
                VisitOffice("iframes");

                executionLog.Log("MultipleIframeDeleteError", "Verify title");
                VerifyTitle("Iframe Apps");

                executionLog.Log("MultipleIframeDeleteError", "Click on first chk box.");
                integration_IframeAppsHelper.ClickElement("Chkbox1");

                executionLog.Log("MultipleIframeDeleteError", " Click on second chk box.  ");
                integration_IframeAppsHelper.ClickElement("ChkBox2");

                executionLog.Log("MultipleIframeDeleteError", "Clcik on delete button ");
                integration_IframeAppsHelper.ClickElement("DeleteMultiple");

                executionLog.Log("MultipleIframeDeleteError", "Accept alert message. ");
                integration_IframeAppsHelper.AcceptAlert();

                executionLog.Log("MultipleIframeDeleteError", "Wait for deletion success message. ");
                integration_IframeAppsHelper.WaitForText("2 Iframe(s) deleted successfully.", 10);

                executionLog.Log("MultipleIframeDeleteError", "Logout from the application.");
                VisitOffice("logout");

                executionLog.Log("MultipleIframeDeleteError", "Login with valid username and password");
                Login("newthemecorp", "mynewpegasus");

                executionLog.Log("MultipleIframeDeleteError", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("MultipleIframeDeleteError", "Redirect at Iframe apps page.");
                VisitCorp("iframes");

                executionLog.Log("MultipleIframeDeleteError", "Verify Page title.");
                VerifyTitle("Iframe Apps");

                executionLog.Log("MultipleIframeDeleteError", "Click on create button.");
                corpIntegration_IframeAppsHelper.ClickJava("Create");

                executionLog.Log("MultipleIframeDeleteError", "Verify page title.");
                VerifyTitle("Create Iframe");

                executionLog.Log("MultipleIframeDeleteError", "Click on save button.");
                corpIntegration_IframeAppsHelper.ClickJava("Save");
                corpIntegration_IframeAppsHelper.WaitForWorkAround(3000);

                executionLog.Log("MultipleIframeDeleteError", "Verify required text for tab name");
                corpIntegration_IframeAppsHelper.VerifyText("TabNameError", "This field is required.");

                executionLog.Log("MultipleIframeDeleteError", "Verify required text for user name.");
                corpIntegration_IframeAppsHelper.VerifyText("UserNameerror", "This field is required.");

                executionLog.Log("MultipleIframeDeleteError", "Verify required text for password.");
                corpIntegration_IframeAppsHelper.VerifyText("PassWordError", "This field is required.");

                executionLog.Log("MultipleIframeDeleteError", "Verify required text for URL.");
                corpIntegration_IframeAppsHelper.VerifyText("URLError", "This field is required.");

                executionLog.Log("MultipleIframeDeleteError", "Click on Cancel button.");
                corpIntegration_IframeAppsHelper.ClickJava("Cancel");
                corpIntegration_IframeAppsHelper.WaitForWorkAround(3000);

                executionLog.Log("MultipleIframeDeleteError", "Verify Page title");
                VerifyTitle("Users");

                executionLog.Log("MultipleIframeDeleteError", "Redirect at Iframe apps page.");
                VisitCorp("iframes");

                executionLog.Log("MultipleIframeDeleteError", "Verify Page title");
                VerifyTitle("Iframe Apps");

                executionLog.Log("MultipleIframeDeleteError", "Click on create button.");
                corpIntegration_IframeAppsHelper.ClickJava("Create");

                executionLog.Log("MultipleIframeDeleteError", "Verify Page title.");
                VerifyTitle("Create Iframe");

                executionLog.Log("MultipleIframeDeleteError", "Enter tab name.");
                corpIntegration_IframeAppsHelper.TypeText("TabName", Tab);

                executionLog.Log("MultipleIframeDeleteError", "Enter user name field name.");
                corpIntegration_IframeAppsHelper.TypeText("UserName", "User");

                executionLog.Log("MultipleIframeDeleteError", "Enter Password field name");
                corpIntegration_IframeAppsHelper.TypeText("Paasword", "PIN");

                executionLog.Log("MultipleIframeDeleteError", "Enter invalid URL.");
                corpIntegration_IframeAppsHelper.TypeText("LoginUrl", "Abcd@gmail");

                executionLog.Log("MultipleIframeDeleteError", "Verify validation for invalid url.");
                corpIntegration_IframeAppsHelper.VerifyText("URLError2", "Invalid URL");

                executionLog.Log("MultipleIframeDeleteError", "Enter a valid URL");
                corpIntegration_IframeAppsHelper.TypeText("LoginUrl", "https://www.google.co.in");

                executionLog.Log("MultipleIframeDeleteError", "Click on tab appear on chk box.");
                corpIntegration_IframeAppsHelper.ClickJava("TabAppearOnOffice");

                executionLog.Log("MultipleIframeDeleteError", "Click on tab appear on chk box.");
                corpIntegration_IframeAppsHelper.ClickJava("TabAppearClient");

                executionLog.Log("MultipleIframeDeleteError", "Click on tab appear on chk box.");
                corpIntegration_IframeAppsHelper.ClickJava("TabAppearPartner");

                executionLog.Log("MultipleIframeDeleteError", "Enter User Name for Iframe.");
                corpIntegration_IframeAppsHelper.TypeText("UsrNAme", UserName);

                executionLog.Log("MultipleIframeDeleteError", "Enter Password for Iframe.");
                corpIntegration_IframeAppsHelper.TypeText("Passwrd", "Pegasus");

                executionLog.Log("MultipleIframeDeleteError", "Select which office to iframe displayed.");
                corpIntegration_IframeAppsHelper.ClickJava("AllOffices");

                executionLog.Log("MultipleIframeDeleteError", "Click on save button.");
                corpIntegration_IframeAppsHelper.ClickJava("Save");

                executionLog.Log("MultipleIframeDeleteError", "Wait for iframe creation success text.");
                corpIntegration_IframeAppsHelper.WaitForText("Iframe created successfully.", 10);

                executionLog.Log("MultipleIframeDeleteError", "Redirect at Iframe apps page.");
                VisitCorp("iframes");

                executionLog.Log("MultipleIframeDeleteError", "Verify Page title");
                VerifyTitle("Iframe Apps");

                executionLog.Log("MultipleIframeDeleteError", "Click on create button.");
                corpIntegration_IframeAppsHelper.ClickJava("Create");

                executionLog.Log("MultipleIframeDeleteError", "Verify Page title.");
                VerifyTitle("Create Iframe");

                executionLog.Log("MultipleIframeDeleteError", "Enter tab name.");
                corpIntegration_IframeAppsHelper.TypeText("TabName", Tab2);

                executionLog.Log("MultipleIframeDeleteError", "Enter user name field name.");
                corpIntegration_IframeAppsHelper.TypeText("UserName", "User");

                executionLog.Log("MultipleIframeDeleteError", "Enter Password field name");
                corpIntegration_IframeAppsHelper.TypeText("Paasword", "PIN");

                executionLog.Log("MultipleIframeDeleteError", "Enter invalid URL.");
                corpIntegration_IframeAppsHelper.TypeText("LoginUrl", "Abcd@gmail");

                executionLog.Log("MultipleIframeDeleteError", "Verify validation for invalid url.");
                corpIntegration_IframeAppsHelper.VerifyText("URLError2", "Invalid URL");

                executionLog.Log("MultipleIframeDeleteError", "Enter a valid URL");
                corpIntegration_IframeAppsHelper.TypeText("LoginUrl", "https://www.google.co.in");

                executionLog.Log("MultipleIframeDeleteError", "Click on tab appear on chk box.");
                corpIntegration_IframeAppsHelper.ClickJava("TabAppearOnOffice");

                executionLog.Log("MultipleIframeDeleteError", "Click on tab appear on chk box.");
                corpIntegration_IframeAppsHelper.ClickJava("TabAppearClient");

                executionLog.Log("MultipleIframeDeleteError", "Click on tab appear on chk box.");
                corpIntegration_IframeAppsHelper.ClickJava("TabAppearPartner");

                executionLog.Log("MultipleIframeDeleteError", "Enter User Name for Iframe.");
                corpIntegration_IframeAppsHelper.TypeText("UsrNAme", UserName);

                executionLog.Log("MultipleIframeDeleteError", "Enter Password for Iframe.");
                corpIntegration_IframeAppsHelper.TypeText("Passwrd", "Pegasus");

                executionLog.Log("MultipleIframeDeleteError", "Select which office to iframe displayed.");
                corpIntegration_IframeAppsHelper.ClickJava("AllOffices");

                executionLog.Log("MultipleIframeDeleteError", "Click on save button.");
                corpIntegration_IframeAppsHelper.ClickJava("Save");

                executionLog.Log("MultipleIframeDeleteError", "Wait for iframe creation success text.");
                corpIntegration_IframeAppsHelper.WaitForText("Iframe created successfully.", 10);

                executionLog.Log("MultipleIframeDeleteError", "Click on first check box.");
                corpIntegration_IframeAppsHelper.ClickElement("Chkbox1");

                executionLog.Log("MultipleIframeDeleteError", "Click on second check box.");
                corpIntegration_IframeAppsHelper.ClickElement("ChkBox2");

                executionLog.Log("MultipleIframeDeleteError", "Click on delete icon.");
                corpIntegration_IframeAppsHelper.ClickJava("DeleteMultiple");
                corpIntegration_IframeAppsHelper.AcceptAlert();

                executionLog.Log("MultipleIframeDeleteError", "Wait for deletion sucess.");
                corpIntegration_IframeAppsHelper.WaitForText("2 Iframe(s) deleted successfully.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("MultipleIframeDeleteError");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Multiple Iframe Delete Error");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Multiple Iframe Delete Error", "Bug", "Medium", "Iframe Intergration page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Multiple Iframe Delete Error");
                        TakeScreenshot("MultipleIframeDeleteError");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\MultipleIframeDeleteError.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("MultipleIframeDeleteError");
                        string id = loginHelper.getIssueID("Multiple Iframe Delete Error");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\MultipleIframeDeleteError.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Multiple Iframe Delete Error"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Multiple Iframe Delete Error");
             //   executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("MultipleIframeDeleteError");
                executionLog.WriteInExcel("Multiple Iframe Delete Error", Status, JIRA, "IFrame");
            }
        }
    }
}