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
    public class DeleteEntryFomParentList : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS9")]
        [TestCategory("ListManagement")]
        public void deleteEntryFomParentList()
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
                executionLog.Log("DeleteEntryFomParentList", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("DeleteEntryFomParentList", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("DeleteEntryFomParentList", "Redirect To List Management page");
                listManagementHelper.ClickElement("Marketing");
                listManagementHelper.WaitForWorkAround(4000);

                executionLog.Log("DeleteEntryFomParentList", "Redirect To List Management page");
                GetWebDriver().Navigate().GoToUrl("https://www.pegasus-test.com/en/listmanagements/leads");
                listManagementHelper.WaitForWorkAround(2000);

                executionLog.Log("DeleteEntryFomParentList", "Click on List Manager button");
                listManagementHelper.ClickForce("ListManager");
                listManagementHelper.WaitForWorkAround(2000);

                executionLog.Log("DeleteEntryFomParentList", "Open A List");
                listManagementHelper.ClickViaJavaScript("//*[@id='listManagerGroup']/li[1]/p");
                listManagementHelper.WaitForWorkAround(1000);

                executionLog.Log("DeleteEntryFomParentList", "Click on parent Entry");
                listManagementHelper.ClickViaJavaScript("//*[@id='leadsParentList']/tbody/tr[1]/td[2]");
                listManagementHelper.WaitForWorkAround(1000);

                executionLog.Log("DeleteEntryFomParentList" , "Right click on Parent list");
                listManagementHelper.RightClick("//*[@id='leadsParentList']/tbody/tr[1]/td[2]");
               
                executionLog.Log("DeleteEntryFomParentList", "Click on Remove item");
                listManagementHelper.ClickElement("RemoveList");
    
            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("DeleteEntryFomParentList");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Delete Entry Fom Parent List");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Delete Entry Fom Parent List", "Bug", "Medium", "Equipment page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Delete Entry Fom Parent List");
                        TakeScreenshot("DeleteEntryFomParentList");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\DeleteEntryFomParentList.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("DeleteEntryFomParentList");
                        string id = loginHelper.getIssueID("Delete Entry Fom Parent List");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\DeleteEntryFomParentList.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Delete Entry Fom Parent List"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Delete Entry Fom Parent List");
                //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("DeleteEntryFomParentList");
                executionLog.WriteInExcel("Delete Entry Fom Parent List", Status, JIRA, "List Management");
            }
        }
    }
}