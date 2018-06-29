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
    public class Pagination : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS1")]
        [TestCategory("ListManagement")]
        public void pagination()
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
                executionLog.Log("Pagination", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("Pagination", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("Pagination", "Redirect To List Management page");
                listManagementHelper.ClickElement("Marketing");
                listManagementHelper.WaitForWorkAround(4000);

                executionLog.Log("Pagination", "Redirect To List Management page");
                GetWebDriver().Navigate().GoToUrl("https://www.pegasus-test.com/en/listmanagements/clients");
                listManagementHelper.WaitForWorkAround(2000);

                //executionLog.Log("CreateListWithBlankName", "Click on List Manager");
                //listManagementHelper.ClickForce("ListManager");
                //listManagementHelper.WaitForWorkAround(1000);

                executionLog.Log("Pagination", "Click on Any Number in pagination");
                listManagementHelper.ClickViaJavaScript("//*[@id='clients_paginate']/ul/li[4]/a");
                listManagementHelper.WaitForWorkAround(1000);

                executionLog.Log("Pagination", "Click on Previous Number in pagination");
                listManagementHelper.ClickViaJavaScript("//*[@id='clients_paginate']/ul/li[2]/a");
                listManagementHelper.WaitForWorkAround(1000);

                executionLog.Log("Pagination", "Select Results Per Page From Drop Down");
                listManagementHelper.SelectDropDown("//*[@id='clients_length']/label/select", "50");
                listManagementHelper.WaitForWorkAround(2000);

                executionLog.Log("Pagination", "Redirect To List Management page");
                GetWebDriver().Navigate().GoToUrl("https://www.pegasus-test.com/");
                listManagementHelper.WaitForWorkAround(2000);

                executionLog.Log("Pagination", "Redirect To List Management page");
                GetWebDriver().Navigate().GoToUrl("https://www.pegasus-test.com/en/listmanagements/clients");
                listManagementHelper.WaitForWorkAround(2000);

                executionLog.Log("Pagination", "Page Is Set To Default");
                listManagementHelper.VerifyTextAvailable("Showing 1 - 50 of");
                listManagementHelper.WaitForWorkAround(1000);

                }
            //}
            //catch (Exception e)
            //{
            //    executionLog.Log("Error", e.StackTrace);
            //    Status = "Fail";

            //    String counter = executionLog.readLastLine("counter");
            //    String Description = executionLog.GetAllTextFile("Pagination");
            //    String Error = executionLog.GetAllTextFile("Error");
            //    if (counter == "")
            //    {
            //        counter = "0";
            //    }
            //    bool result = loginHelper.CheckExstingIssue("Pagination");
            //    if (!result)
            //    {
            //        if (Int16.Parse(counter) < 9)
            //        {
            //            executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
            //            loginHelper.CreateIssue("Pagination", "Bug", "Medium", "Equipment page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
            //            string id = loginHelper.getIssueID("Pagination");
            //            TakeScreenshot("Pagination");
            //            string directoryName = loginHelper.GetnewDirectoryName(GetPath());
            //            var location = directoryName + "\\Pagination.png";
            //            loginHelper.AddAttachment(location, id);
            //        }
            //    }
            //    else
            //    {
            //        if (Int16.Parse(counter) < 9)
            //        {
            //            executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
            //            TakeScreenshot("Pagination");
            //            string id = loginHelper.getIssueID("Pagination");
            //            string directoryName = loginHelper.GetnewDirectoryName(GetPath());
            //            var location = directoryName + "\\Pagination.png";
            //            loginHelper.AddAttachment(location, id);
            //            loginHelper.AddComment(loginHelper.getIssueID("Pagination"), "This issue is still occurring");
            //        }
            //    }
            //    JIRA = loginHelper.getIssueID("Pagination");
            //    //  executionLog.DeleteFile("Error");
            //    throw;

            //}
            //finally
            //{
        //    executionLog.DeleteFile("Pagination");
            //    executionLog.WriteInExcel("Pagination", Status, JIRA, "List Management");
            //}
        }
    }