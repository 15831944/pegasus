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
    public class VerifyDurationOfTaskOnCorporateDetailsPage : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin")]
        [TestCategory("Fail")]
        [TestCategory("BugTestNew")]
        public void verifyDurationOfTaskOnCorporateDetailsPage()
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
            var officeAdmin_CorporateHelper = new OfficeAdmin_CorporateHelper(GetWebDriver());

            // Variable
            var name = "Task" + RandomNumber(111, 999999);
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("VerifyDurationOfTaskOnCorporateDetailsPage", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyDurationOfTaskOnCorporateDetailsPage", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("VerifyDurationOfTaskOnCorporateDetailsPage", "Redirect at My Corporate Details page");
                VisitOffice("mycorp");
                officeAdmin_CorporateHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyDurationOfTaskOnCorporateDetailsPage", "Click on New Task");
                officeAdmin_CorporateHelper.ClickElement("NewTask");
                officeAdmin_CorporateHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyDurationOfTaskOnCorporateDetailsPage", "Enter task Subject");
                officeAdmin_CorporateHelper.TypeText("TaskSub", name);

                executionLog.Log("VerifyDurationOfTaskOnCorporateDetailsPage", "Enter task start date");
                officeAdmin_CorporateHelper.TypeText("TaskStartDate", "2017-11-11");
                officeAdmin_CorporateHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyDurationOfTaskOnCorporateDetailsPage", "Enter task Due date");
                officeAdmin_CorporateHelper.TypeText("TaskDueDate", "2017-11-11");
                officeAdmin_CorporateHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyDurationOfTaskOnCorporateDetailsPage", "Click on Save button. ");
                officeAdmin_CorporateHelper.ClickElement("TaskSave");
                officeAdmin_CorporateHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyDurationOfTaskOnCorporateDetailsPage", "Select Activity Type >> Tasks");
                officeAdmin_CorporateHelper.SelectByText("ActivityType", "Tasks");
                officeAdmin_CorporateHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyDurationOfTaskOnCorporateDetailsPage", "Search task.");
                officeAdmin_CorporateHelper.TypeText("SearchActivity", name);
                officeAdmin_CorporateHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyDurationOfTaskOnCorporateDetailsPage", "Open first task");
                officeAdmin_CorporateHelper.ClickElement("Activity1");
                officeAdmin_CorporateHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyDurationOfTaskOnCorporateDetailsPage", "Verify Duration is displayed");
                officeAdmin_CorporateHelper.VerifyPageText("Duration");
            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyDurationOfTaskOnCorporateDetailsPage");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Duration Of Task On Corporate Details Page");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Duration Of Task On Corporate Details Page", "Bug", "Medium", "Corporate Details page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Duration Of Task On Corporate Details Page");
                        TakeScreenshot("VerifyDurationOfTaskOnCorporateDetailsPage");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyDurationOfTaskOnCorporateDetailsPage.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyDurationOfTaskOnCorporateDetailsPage");
                        string id = loginHelper.getIssueID("Verify Duration Of Task On Corporate Details Page");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyDurationOfTaskOnCorporateDetailsPage.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Duration Of Task On Corporate Details Page"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Duration Of Task On Corporate Details Page");
                //      executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("VerifyDurationOfTaskOnCorporateDetailsPage");
                executionLog.WriteInExcel("Verify Duration Of Task On Corporate Details Page", Status, JIRA, "Corporate Details");
            }
        }
    }
}