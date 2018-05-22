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
    public class CreateNewListOnDroppingItems : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS9")]
        [TestCategory("List Management")]
        public void createNewListOnDroppingItems()
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
            executionLog.Log("CreateNewListOnDroppingItems", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("CreateNewListOnDroppingItems", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("CreateNewListOnDroppingItems", "Redirect To List Management page");
                listManagementHelper.ClickElement("Marketing");
                listManagementHelper.WaitForWorkAround(4000);

                executionLog.Log("CreateNewListOnDroppingItems", "Redirect To List Management page");
                GetWebDriver().Navigate().GoToUrl("https://www.pegasus-test.com/en/listmanagements/clients");
                listManagementHelper.WaitForWorkAround(2000);

                executionLog.Log("CreateNewListOnDroppingItems", "Select a entry check box");
                listManagementHelper.ClickForce("Entrycheckbox");
                listManagementHelper.WaitForWorkAround(1000);

                executionLog.Log("CreateNewListOnDroppingItems", "Drag and drop the entry to create button");
                listManagementHelper.dragndrop("//*[@id='clients']/tbody/tr[1]/td[3]/a", "//*[@id='listManagerDropzone']");
                listManagementHelper.WaitForWorkAround(4000);

                }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CreateNewListOnDroppingItems");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Create New List On Dropping Items");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Create New List On Dropping Items", "Bug", "Medium", "Equipment page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Create New List On Dropping Items");
                        TakeScreenshot("CreateNewListOnDroppingItems");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateNewListOnDroppingItems.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CreateNewListOnDroppingItems");
                        string id = loginHelper.getIssueID("Create New List On Dropping Items");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateNewListOnDroppingItems.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Create New List On Dropping Items"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Create New List On Dropping Items");
                //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("CreateNewListOnDroppingItems");
                executionLog.WriteInExcel("Create New List On Dropping Items", Status, JIRA, "List Management");
            }
        }
    }
}