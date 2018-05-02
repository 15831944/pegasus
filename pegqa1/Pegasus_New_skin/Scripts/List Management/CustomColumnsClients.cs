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
    public class CustomColumnsClients : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS1")]
        [TestCategory("BugTest")]
        public void customColumnsClients()
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
            var listManagementHelper = new ListManagementHelper(GetWebDriver());

            // Variable 
            var name = "Test" + GetRandomNumber();
            var name2 = "Testlist" + GetRandomNumber();
            var Id = "12345" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("CustomColumnsClients", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("CustomColumnsClients", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("CustomColumnsClients", "Redirect To List Management page");
                listManagementHelper.ClickElement("Marketing");
                listManagementHelper.WaitForWorkAround(4000);

                executionLog.Log("CustomColumnsClients", "Redirect To List Management page");
                GetWebDriver().Navigate().GoToUrl("https://www.pegasus-test.com/en/listmanagements/clients");
                listManagementHelper.WaitForWorkAround(2000);

                executionLog.Log("CustomColumnsClients", "Click on Settings icon");
                listManagementHelper.ClickForce("SettingIcon");
                listManagementHelper.WaitForWorkAround(2000);

                executionLog.Log("CustomColumnsClients", "Click on Search Box");
                listManagementHelper.TypeText("SearchBox", "User Group");
                listManagementHelper.WaitForWorkAround(1000);

                executionLog.Log("CustomColumnsClients", "Click on Plus icon");
                listManagementHelper.ClickViaJavaScript("//*[@id='modalHeaderColumnsleft']/div[2]/div/div[7]/div/i");
                listManagementHelper.WaitForWorkAround(1000);

                executionLog.Log("CustomColumnsClients", "Clear Search Field value");
                listManagementHelper.ClearTextBoxValue("//input[@id='searchHeaderColumns']");
               
                executionLog.Log("CustomColumnsClients", "Click on Apply Button");
                listManagementHelper.ClickForce("Apply");
                listManagementHelper.WaitForWorkAround(1000);

                executionLog.Log("CustomColumnsClients", "Click on Settings icon");
                listManagementHelper.ClickForce("SettingIcon");
                listManagementHelper.WaitForWorkAround(2000);

                executionLog.Log("CustomColumnsClients", "Click on Search Box");
                listManagementHelper.TypeText("SearchBox", "Modified");
                listManagementHelper.WaitForWorkAround(1000);

                executionLog.Log("CustomColumnsClients", "Click on Plus icon");
                listManagementHelper.ClickViaJavaScript("//*[@id='modalHeaderColumnsleft']/div[1]/div/div[1]/div/i");
                listManagementHelper.WaitForWorkAround(1000);

                executionLog.Log("CustomColumnsClients", "Click on Cancel button");
                listManagementHelper.ClickElement("Cancel");
                listManagementHelper.WaitForWorkAround(1000);

                executionLog.Log("CustomColumnsClients", "Click on Settings icon");
                listManagementHelper.ClickForce("SettingIcon");
                listManagementHelper.WaitForWorkAround(2000);

                executionLog.Log("CustomColumnsClients", "Click on Search Box");
                listManagementHelper.TypeText("SearchBox", "Modified");
                listManagementHelper.WaitForWorkAround(1000);

                executionLog.Log("CustomColumnsClients", "Verify The Field");
                listManagementHelper.VerifyTextAvailable("Modified");
                listManagementHelper.WaitForWorkAround(1000);
                Console.WriteLine("Field Is Not Saved");

                }
            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CustomColumnsClients");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Custom Columns Clients");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Custom Columns Clients", "Bug", "Medium", "Equipment page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Custom Columns Clients");
                        TakeScreenshot("CustomColumnsClients");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CustomColumnsClients.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CustomColumnsClients");
                        string id = loginHelper.getIssueID("Custom Columns Clients");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CustomColumnsClients.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Custom Columns Clients"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Custom Columns Clients");
                //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
               executionLog.DeleteFile("CustomColumnsClients");
                executionLog.WriteInExcel("Custom Columns Clients", Status, JIRA, "List Management");
            }
        }
    }