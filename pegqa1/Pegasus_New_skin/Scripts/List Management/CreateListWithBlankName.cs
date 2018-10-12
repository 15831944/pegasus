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
    public class CreateListWithBlankName : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS1")]
        [TestCategory("ListManagement")]
        public void createListWithBlankName()
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
            String Jira = "";
            String Status = "Pass";

            //try
            //{
                executionLog.Log("CreateListWithBlankName", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("CreateListWithBlankName", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("CreateListWithBlankName", "Redirect To List Management page");
                listManagementHelper.ClickElement("Marketing");
                listManagementHelper.WaitForWorkAround(4000);

                executionLog.Log("CreateListWithBlankName", "Redirect To List Management page");
                GetWebDriver().Navigate().GoToUrl("https://www.pegasus-test.com/en/listmanagements/clients");
                listManagementHelper.WaitForWorkAround(2000);

                executionLog.Log("CreateListWithBlankName", "Hover to List Manger");
                listManagementHelper.MouseOver("//*[@id='toggleListManager']/span/i[2]");
                listManagementHelper.WaitForWorkAround(3000);

                executionLog.Log("CreateListWithBlankName", "Click on List Manager");
                listManagementHelper.ClickForce("ListManager");
                listManagementHelper.WaitForWorkAround(1000);

                executionLog.Log("CreateListWithBlankName", "Click on Create list");
                listManagementHelper.ClickForce("Create");
                listManagementHelper.WaitForWorkAround(1000);

                executionLog.Log("CreateListWithBlankName", "Clear the list name");
                listManagementHelper.ClearTextBoxValue("//input[@id='listNameInput']");

                executionLog.Log("CreateListWithBlankName", "Click on Create button");
                listManagementHelper.ClickElement("Createbuttn");
                listManagementHelper.WaitForWorkAround(1000);

                executionLog.Log("CreateListWithBlankName", "Search the list name");
                listManagementHelper.VerifyText("ValidationMessage", "List name cannot be empty!");
                Console.WriteLine("List name cannot be empty!");



            }
            //}
            //catch (Exception e)
            //{
            //    executionLog.Log("Error", e.StackTrace);
            //    Status = "Fail";

            //    String counter = executionLog.readLastLine("counter");
            //    String Description = executionLog.GetAllTextFile("CreateListWithBlankName");
            //    String Error = executionLog.GetAllTextFile("Error");
            //    if (counter == "")
            //    {
            //        counter = "0";
            //    }
            //    bool result = loginHelper.CheckExstingIssue("Create List With Blank Name");
            //    if (!result)
            //    {
            //        if (Int16.Parse(counter) < 9)
            //        {
            //            executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
            //            loginHelper.CreateIssue("Create List With Blank Name", "Bug", "Medium", "Equipment page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
            //            string id = loginHelper.getIssueID("Rename ListManagement Item");
            //            TakeScreenshot("CreateListWithBlankName");
            //            string directoryName = loginHelper.GetnewDirectoryName(GetPath());
            //            var location = directoryName + "\\CreateListWithBlankName.png";
            //            loginHelper.AddAttachment(location, id);
            //        }
            //    }
            //    else
            //    {
            //        if (Int16.Parse(counter) < 9)
            //        {
            //            executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
            //            TakeScreenshot("CreateListWithBlankName");
            //            string id = loginHelper.getIssueID("Create List With Blank Name");
            //            string directoryName = loginHelper.GetnewDirectoryName(GetPath());
            //            var location = directoryName + "\\CreateListWithBlankName.png";
            //            loginHelper.AddAttachment(location, id);
            //            loginHelper.AddComment(loginHelper.getIssueID("Create List With Blank Name"), "This issue is still occurring");
            //        }
            //    }
            //    JIRA = loginHelper.getIssueID("Create List With Blank Name");
            //    //  executionLog.DeleteFile("Error");
            //    throw;

            //}
            //finally
            //{
            //    executionLog.DeleteFile("CreateListWithBlankName");
            //    executionLog.WriteInExcel("Create List With Blank Name", Status, JIRA, "List Management");
            //}
        }
    }
