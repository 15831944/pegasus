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
    public class HorizontalAndVerticalScroll : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS1")]
        [TestCategory("ListManagement")]
        public void horizontalAndVerticalScroll()
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
                executionLog.Log("HorizontalAndVerticalScroll", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("HorizontalAndVerticalScroll", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("HorizontalAndVerticalScroll", "Redirect To List Management page");
                listManagementHelper.ClickElement("Marketing");
                listManagementHelper.WaitForWorkAround(4000);

                executionLog.Log("HorizontalAndVerticalScroll", "Redirect To List Management page");
                GetWebDriver().Navigate().GoToUrl("https://www.pegasus-test.com/en/listmanagements/clients");
                listManagementHelper.WaitForWorkAround(2000);

                executionLog.Log("CreateListWithBlankName", "Click on List Manager");
                listManagementHelper.ClickForce("ListManager");
                listManagementHelper.WaitForWorkAround(1000);

                executionLog.Log("HorizontalAndVerticalScroll", "Click on list");
                listManagementHelper.ClickElement("Firstlist");
                listManagementHelper.WaitForWorkAround(1000);

                executionLog.Log("HorizontalAndVerticalScroll", "Slide Vertically");
                listManagementHelper.ScrollDown("//*[@id='listmanagements']/tbody/tr[14]/td[3]");
                listManagementHelper.WaitForWorkAround(2000);

                executionLog.Log("HorizontalAndVerticalScroll", "Slide Vertically");
                listManagementHelper.ScrollDown("//*[@id='listmanagements']/tbody/tr[25]/td[3]");
                listManagementHelper.WaitForWorkAround(2000);

                executionLog.Log("HorizontalAndVerticalScroll", "Slide Horizontally");
                listManagementHelper.ScrollDown("//*[@id='listmanagements']/tbody/tr[5]/td[4]");
                listManagementHelper.WaitForWorkAround(2000);

                executionLog.Log("HorizontalAndVerticalScroll", "Slide Horizontally");
                listManagementHelper.ScrollDown("//*[@id='listmanagements']/tbody/tr[14]/td[6]");
                listManagementHelper.WaitForWorkAround(2000);

                }
            //}
            //catch (Exception e)
            //{
            //    executionLog.Log("Error", e.StackTrace);
            //    Status = "Fail";

            //    String counter = executionLog.readLastLine("counter");
            //    String Description = executionLog.GetAllTextFile("HorizontalAndVerticalScroll");
            //    String Error = executionLog.GetAllTextFile("Error");
            //    if (counter == "")
            //    {
            //        counter = "0";
            //    }
            //    bool result = loginHelper.CheckExstingIssue("Horizontal And Vertical Scroll");
            //    if (!result)
            //    {
            //        if (Int16.Parse(counter) < 9)
            //        {
            //            executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
            //            loginHelper.CreateIssue("Horizontal And Vertical Scroll", "Bug", "Medium", "Equipment page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
            //            string id = loginHelper.getIssueID("Horizontal And Vertical Scroll");
            //            TakeScreenshot("HorizontalAndVerticalScroll");
            //            string directoryName = loginHelper.GetnewDirectoryName(GetPath());
            //            var location = directoryName + "\\HorizontalAndVerticalScroll.png";
            //            loginHelper.AddAttachment(location, id);
            //        }
            //    }
            //    else
            //    {
            //        if (Int16.Parse(counter) < 9)
            //        {
            //            executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
            //            TakeScreenshot("HorizontalAndVerticalScroll");
            //            string id = loginHelper.getIssueID("Horizontal And Vertical Scroll");
            //            string directoryName = loginHelper.GetnewDirectoryName(GetPath());
            //            var location = directoryName + "\\HorizontalAndVerticalScroll.png";
            //            loginHelper.AddAttachment(location, id);
            //            loginHelper.AddComment(loginHelper.getIssueID("Horizontal And Vertical Scroll"), "This issue is still occurring");
            //        }
            //    }
            //    JIRA = loginHelper.getIssueID("Horizontal And Vertical Scroll");
            //    //  executionLog.DeleteFile("Error");
            //    throw;

            //}
            //finally
            //{
        //    executionLog.DeleteFile("HorizontalAndVerticalScroll");
            //    executionLog.WriteInExcel("Horizontal And Vertical Scroll", Status, JIRA, "List Management");
            //}
        }
    }