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
    public class RenameListManagementItem : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS1")]
        [TestCategory("List Management")]
        public void renameListManagementItem()
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
                executionLog.Log("RenameListManagementItem", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("RenameListManagementItem", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("RenameListManagementItem", "Redirect To List Management page");
                listManagementHelper.ClickElement("Marketing");
                listManagementHelper.WaitForWorkAround(4000);

                //executionLog.Log("RenameListManagementItem", "Redirect To List Management page");
                //GetWebDriver().Navigate().GoToUrl("https://www.pegasus-test.com/en/listmanagements/clients");
                //listManagementHelper.WaitForWorkAround(2000);

                executionLog.Log("RenameListManagementItem", "Click on Create list");
                listManagementHelper.ClickForce("Create");
                listManagementHelper.WaitForWorkAround(1000);

                executionLog.Log("RenameListManagementItem", "Name the list");
                listManagementHelper.TypeText("ListName", name);

                executionLog.Log("RenameListManagementItem", "Click on Create button");
                listManagementHelper.ClickElement("Createbuttn");
                listManagementHelper.WaitForWorkAround(1000);

                executionLog.Log("RenameListManagementItem", "Search the list name");
                listManagementHelper.TypeText("SearchList", name);

                executionLog.Log("RenameListManagementItem", "Click on list");
                listManagementHelper.ClickElement("Firstlist");
                listManagementHelper.WaitForWorkAround(1000);

                executionLog.Log("RenameListManagementItem", "Rename the list");
                listManagementHelper.TypeText("Name", name2);

                executionLog.Log("RenameListManagementItem", "Click on Back button");
                listManagementHelper.ClickElement("Backbutton");
                listManagementHelper.WaitForWorkAround(1000);

                executionLog.Log("RenameListManagementItem", "Search the list name");
                listManagementHelper.TypeText("SearchList", name2);

                executionLog.Log("RenameListManagementItem", "verify the list name");
                String listname = listManagementHelper.GetText("//ul[@id='listManagerGroup']/li/p");

                executionLog.Log("RenameListManagementItem", "verify the list name");
                Assert.AreEqual(listname, name2);
                Console.WriteLine("List Rename successfull");

                executionLog.Log("RenameListManagementItem", "Delete List");
                listManagementHelper.MouseOver("//ul[@id='listManagerGroup']/li/p");
                listManagementHelper.WaitForWorkAround(2000);

                //executionLog.Log("RenameListManagementItem", "Delete List");
                //listManagementHelper.ClickElement("DeleteList");

                //executionLog.Log("RenameListManagementItem", "Click on Delete button");
                //listManagementHelper.ClickElement("Deletebuttn");
                

                }
            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("RenameListManagementItem");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Rename ListManagement Item");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Rename ListManagement Item", "Bug", "Medium", "Equipment page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Rename ListManagement Item");
                        TakeScreenshot("RenameListManagementItem");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\RenameListManagementItem.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("RenameListManagementItem");
                        string id = loginHelper.getIssueID("Rename ListManagement Item");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\RenameListManagementItem.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Rename ListManagement Item"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Rename ListManagement Item");
                //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("RenameListManagement Item");
                executionLog.WriteInExcel("Rename ListManagement Item", Status, JIRA, "List Management");
            }
        }
    }