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
    public class CustomizeRecordDetailsOpportunities : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS1")]
        [TestCategory("List Management")]
        public void customizeRecordDetailsOpportunities()
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
                executionLog.Log("CustomizeRecordDetailsOpportunities", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("CustomizeRecordDetailsOpportunities", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("CustomizeRecordDetailsOpportunities", "Redirect To List Management page");
                listManagementHelper.ClickElement("Marketing");
                listManagementHelper.WaitForWorkAround(4000);

                executionLog.Log("CustomizeRecordDetailsOpportunities", "Redirect To List Management page");
                GetWebDriver().Navigate().GoToUrl("https://www.pegasus-test.com/en/listmanagements/opportunities");
                listManagementHelper.WaitForWorkAround(2000);

                executionLog.Log("CustomizeRecordDetailsOpportunities", "Click on Expand Details Icon");
                listManagementHelper.ClickViaJavaScript("//*[@id='opportunities']/tbody/tr[1]/td[1]");
                listManagementHelper.WaitForWorkAround(2000);

                executionLog.Log("CustomizeRecordDetailsOpportunities", "Click on Customize Field Option");
                listManagementHelper.ClickViaJavaScript("//*[@id='opportunities']/tbody/tr[2]/td/div[2]");
                listManagementHelper.WaitForWorkAround(2000);

                executionLog.Log("CustomizeRecordDetailsOpportunities", "Click on Expand Section Icon");
                listManagementHelper.ClickViaJavaScript("//*[@id='modalDetailColumnsleft']/div[2]/p/i");
                listManagementHelper.WaitForWorkAround(2000);

                executionLog.Log("CustomizeRecordDetailsOpportunities", "Click on Expand Sub Section Option");
                listManagementHelper.ClickViaJavaScript("//*[@id='modalDetailColumnsleft']/div[2]/div/p/i");
                listManagementHelper.WaitForWorkAround(2000);

                executionLog.Log("CustomizeRecordDetailsOpportunities", "Click on Plus Icon");
                listManagementHelper.ClickViaJavaScript("//*[@id='modalDetailColumnsleft']/div[2]/div[1]/div[4]/div/i");
                listManagementHelper.WaitForWorkAround(2000);

                executionLog.Log("CustomizeRecordDetailsOpportunities", "Click on Apply Button");
                listManagementHelper.ClickForce("ApplyDetails");
                listManagementHelper.WaitForWorkAround(1000);

                executionLog.Log("CustomizeRecordDetailsOpportunities", "Click on Expand Details Icon");
                listManagementHelper.ClickViaJavaScript("//*[@id='opportunities']/tbody/tr[1]/td[1]");
                listManagementHelper.WaitForWorkAround(2000);

                executionLog.Log("CustomizeRecordDetailsOpportunities", "Click on Customize Field Option");
                listManagementHelper.ClickViaJavaScript("//*[@id='opportunities']/tbody/tr[2]/td/div[2]");
                listManagementHelper.WaitForWorkAround(2000);

                executionLog.Log("CustomizeRecordDetailsOpportunities", "Click on Expand Section Icon");
                listManagementHelper.ClickViaJavaScript("//*[@id='modalDetailColumnsleft']/div[2]/p/i");
                listManagementHelper.WaitForWorkAround(2000);

                executionLog.Log("CustomizeRecordDetailsOpportunities", "Click on Expand Sub Section Option");
                listManagementHelper.ClickViaJavaScript("//*[@id='modalDetailColumnsleft']/div[2]/div/p/i");
                listManagementHelper.WaitForWorkAround(2000);

                executionLog.Log("CustomizeRecordDetailsOpportunities", "Click on Plus Icon");
                listManagementHelper.ClickViaJavaScript("//*[@id='modalDetailColumnsleft']/div[2]/div[1]/div[4]/div/i");
                listManagementHelper.WaitForWorkAround(2000);

                executionLog.Log("CustomizeRecordDetailsOpportunities", "Click on Cancel button");
                listManagementHelper.ClickViaJavaScript("//*[@id='modalDetailColumns']/div/div/div/div[3]/button[1]");
                listManagementHelper.WaitForWorkAround(1000);

                }
            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CustomizeRecordDetailsOpportunities");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Customize Record Details Opportunities");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Customize Record Details Opportunities", "Bug", "Medium", "Equipment page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Customize Record Details Opportunities");
                        TakeScreenshot("CustomizeRecordDetailsOpportunities");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CustomizeRecordDetailsOpportunities.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CustomizeRecordDetailsOpportunities");
                        string id = loginHelper.getIssueID("Customize Record Details Opportunities");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CustomizeRecordDetailsOpportunities.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Customize Record Details Opportunities"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Customize Record Details Opportunities");
                //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
               executionLog.DeleteFile("CustomizeRecordDetailsOpportunities");
                executionLog.WriteInExcel("Customize Record Details Opportunities", Status, JIRA, "List Management");
            }
        }
    }