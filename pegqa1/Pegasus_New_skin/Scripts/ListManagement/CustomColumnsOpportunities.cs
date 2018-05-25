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
    public class CustomColumnsOpportunities : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS9")]
        [TestCategory("ListManagement")]
        public void customColumnsOpportunities()
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
                executionLog.Log("CustomColumnsOpportunities", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("CustomColumnsOpportunities", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("CustomColumnsOpportunities", "Redirect To List Management page");
                listManagementHelper.ClickElement("Marketing");
                listManagementHelper.WaitForWorkAround(4000);

                executionLog.Log("CustomColumnsOpportunities", "Redirect To List Management page");
                GetWebDriver().Navigate().GoToUrl("https://www.pegasus-test.com/en/listmanagements/opportunities");
                listManagementHelper.WaitForWorkAround(2000);

                executionLog.Log("CustomColumnsOpportunities", "Click on Settings icon");
                listManagementHelper.ClickForce("SettingIcon");
                listManagementHelper.WaitForWorkAround(2000);

                executionLog.Log("CustomColumnsOpportunities", "Click on Search Box");
                listManagementHelper.TypeText("SearchBox", "Account Manager");
                listManagementHelper.WaitForWorkAround(1000);

                executionLog.Log("CustomColumnsOpportunities", "Click on Plus icon");
                listManagementHelper.ClickViaJavaScript("//*[@id='modalHeaderColumnsleft']/div[2]/div[2]/div[6]/div/i");
                listManagementHelper.WaitForWorkAround(1000);

                executionLog.Log("CustomColumnsOpportunities", "Clear Search Field value");
                listManagementHelper.ClearTextBoxValue("//input[@id='searchHeaderColumns']");

                executionLog.Log("CustomColumnsOpportunities", "Click on Apply Button");
                listManagementHelper.ClickForce("Apply");
                listManagementHelper.WaitForWorkAround(1000);

                executionLog.Log("CustomColumnsOpportunities", "Click on Settings icon");
                listManagementHelper.ClickForce("SettingIcon");
                listManagementHelper.WaitForWorkAround(2000);

                executionLog.Log("CustomColumnsOpportunities", "Click on Search Box");
                listManagementHelper.TypeText("SearchBox", "Category");
                listManagementHelper.WaitForWorkAround(1000);

                executionLog.Log("CustomColumnsOpportunities", "Click on Plus icon");
                listManagementHelper.ClickViaJavaScript("//*[@id='modalHeaderColumnsleft']/div[2]/div[2]/div[4]/div/i");
                listManagementHelper.WaitForWorkAround(1000);

                executionLog.Log("CustomColumnsOpportunities", "Click on Cancel button");
                listManagementHelper.ClickElement("Cancel");
                listManagementHelper.WaitForWorkAround(1000);

                executionLog.Log("CustomColumnsOpportunities", "Click on Settings icon");
                listManagementHelper.ClickForce("SettingIcon");
                listManagementHelper.WaitForWorkAround(2000);

                executionLog.Log("CustomColumnsOpportunities", "Click on Search Box");
                listManagementHelper.TypeText("SearchBox", "Category");
                listManagementHelper.WaitForWorkAround(1000);

                executionLog.Log("CustomColumnsOpportunities", "Verify the Field");
                listManagementHelper.VerifyTextAvailable("Category");
                listManagementHelper.WaitForWorkAround(1000);
                Console.WriteLine("Field Is Not Saved");


            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CustomColumnsOpportunities");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Custom Columns Opportunities");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Custom Columns Opportunities", "Bug", "Medium", "Equipment page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Custom Columns Opportunities");
                        TakeScreenshot("CustomColumnsOpportunities");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CustomColumnsOpportunities.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CustomColumnsOpportunities");
                        string id = loginHelper.getIssueID("Custom Columns Opportunities");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CustomColumnsOpportunities.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Custom Columns Opportunities"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Custom Columns Opportunities");
                //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("CustomColumnsOpportunities");
                executionLog.WriteInExcel("Custom Columns Opportunities", Status, JIRA, "List Management");
            }
        }
    }
}