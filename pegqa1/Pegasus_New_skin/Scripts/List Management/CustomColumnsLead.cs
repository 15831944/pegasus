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
    public class CustomColumnsLead : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS1")]
        [TestCategory("BugTest")]
        public void customColumnsLead()
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

            //try
            //{
                executionLog.Log("CustomColumnsLead", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("CustomColumnsLead", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("CustomColumnsLead", "Redirect To List Management page");
                listManagementHelper.ClickElement("Marketing");
                listManagementHelper.WaitForWorkAround(4000);

                executionLog.Log("CustomColumnsLead", "Redirect To List Management page");
                GetWebDriver().Navigate().GoToUrl("https://www.pegasus-test.com/en/listmanagements/leads");
                listManagementHelper.WaitForWorkAround(2000);

                executionLog.Log("CustomColumnsLead", "Click on Settings icon");
                listManagementHelper.ClickForce("SettingIcon");
                listManagementHelper.WaitForWorkAround(2000);

                executionLog.Log("CustomColumnsLead", "Click on Search Box");
                listManagementHelper.TypeText("SearchBox", "Account Manager");
                listManagementHelper.WaitForWorkAround(1000);

                executionLog.Log("CustomColumnsLead", "Click on Plus icon");
                listManagementHelper.ClickElement("PlusIcon");
                listManagementHelper.WaitForWorkAround(1000);

                executionLog.Log("CustomColumnsLead", "Clear Search Field value");
                listManagementHelper.ClearTextBoxValue("//input[@id='searchHeaderColumns']");
                listManagementHelper.WaitForWorkAround(1000);

                //executionLog.Log("CustomColumnsLead", "Click on Cross icon");
                //listManagementHelper.ClickElement("CrossIcon");
                //listManagementHelper.WaitForWorkAround(1000);

                executionLog.Log("CustomColumnsLead", "Click on Apply Button");
                listManagementHelper.ClickForce("Apply");
                listManagementHelper.WaitForWorkAround(1000);

                //executionLog.Log("CustomColumnsLead", "Verify the Option in Display Column");
                //listManagementHelper.VerifyText("//*[@id='modalHeaderColumnsright']/div[10]/div/p", "Account Manager");
                //listManagementHelper.WaitForWorkAround(1000);


                }
            //}
            //catch (Exception e)
            //{
            //    executionLog.Log("Error", e.StackTrace);
            //    Status = "Fail";

            //    String counter = executionLog.readLastLine("counter");
            //    String Description = executionLog.GetAllTextFile("CustomColumnsLead");
            //    String Error = executionLog.GetAllTextFile("Error");
            //    if (counter == "")
            //    {
            //        counter = "0";
            //    }
            //    bool result = loginHelper.CheckExstingIssue("Rename ListManagement Item");
            //    if (!result)
            //    {
            //        if (Int16.Parse(counter) < 9)
            //        {
            //            executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
            //            loginHelper.CreateIssue("Rename ListManagement Item", "Bug", "Medium", "Equipment page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
            //            string id = loginHelper.getIssueID("Rename ListManagement Item");
            //            TakeScreenshot("CustomColumnsLead");
            //            string directoryName = loginHelper.GetnewDirectoryName(GetPath());
            //            var location = directoryName + "\\CustomColumnsLead.png";
            //            loginHelper.AddAttachment(location, id);
            //        }
            //    }
            //    else
            //    {
            //        if (Int16.Parse(counter) < 9)
            //        {
            //            executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
            //            TakeScreenshot("CustomColumnsLead");
            //            string id = loginHelper.getIssueID("Rename ListManagement Item");
            //            string directoryName = loginHelper.GetnewDirectoryName(GetPath());
            //            var location = directoryName + "\\CustomColumnsLead.png";
            //            loginHelper.AddAttachment(location, id);
            //            loginHelper.AddComment(loginHelper.getIssueID("Rename ListManagement Item"), "This issue is still occurring");
            //        }
            //    }
            //    JIRA = loginHelper.getIssueID("Rename ListManagement Item");
            //    //  executionLog.DeleteFile("Error");
            //    throw;

            //}
            //finally
            //{
            //    executionLog.DeleteFile("RenameListManagement Item");
            //    executionLog.WriteInExcel("Rename ListManagement Item", Status, JIRA, "List Management");
            //}
        }
    }