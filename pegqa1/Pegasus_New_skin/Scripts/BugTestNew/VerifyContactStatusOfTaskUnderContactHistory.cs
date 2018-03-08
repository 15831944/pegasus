using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class VerifyContactStatusOfTaskUnderContactHistory : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin")]
        [TestCategory("BugTestNew")]
        public void verifyContactStatusOfTaskUnderContactHistory()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            //Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var office_ContactsHelper = new Office_ContactsHelper(GetWebDriver());
            var officeActivities_TasksHelper = new OfficeActivities_TasksHelper(GetWebDriver());

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Variable
            var Subject = "Testtask" + RandomNumber(99, 99999);
            
            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("VerifyContactStatusOfTaskUnderContactHistory", "Login with valid credentials");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyContactStatusOfTaskUnderContactHistory", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyContactStatusOfTaskUnderContactHistory", "Goto Contact");
                VisitOffice("contacts");
                office_ContactsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyContactStatusOfTaskUnderContactHistory", "Verify page title.");
                VerifyTitle("Contacts");

                executionLog.Log("VerifyContactStatusOfTaskUnderContactHistory", "Click on a contact");
                office_ContactsHelper.ClickElement("Contact1");
                office_ContactsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyContactStatusOfTaskUnderContactHistory", "Observe Contact Status");
                var status = office_ContactsHelper.GetText("//div[@id='status']");
                office_ContactsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyContactStatusOfTaskUnderContactHistory", "Click Create New(Task/Meeting) button");
                office_ContactsHelper.ClickElement("CreateNewTMBtn");
                office_ContactsHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyContactStatusOfTaskUnderContactHistory", "Click Task Option");
                office_ContactsHelper.ClickElement("TaskOption");
                office_ContactsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyContactStatusOfTaskUnderContactHistory", "Enter Subject of task");
                officeActivities_TasksHelper.TypeText("Subject", Subject);

                executionLog.Log("VerifyContactStatusOfTaskUnderContactHistory", "Enter Start Date of task");
                officeActivities_TasksHelper.TypeText("StartDate", "17/11/2017");

                executionLog.Log("VerifyContactStatusOfTaskUnderContactHistory", "Enter End Date of task");
                officeActivities_TasksHelper.TypeText("DueDate", "17/11/2017");

                executionLog.Log("VerifyContactStatusOfTaskUnderContactHistory", "Click on Save button");
                officeActivities_TasksHelper.ClickElement("Save");
                officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyContactStatusOfTaskUnderContactHistory", "Select Activity Type >> Tasks");
                office_ContactsHelper.SelectByText("SelectActivityType", "Tasks");
                office_ContactsHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyContactStatusOfTaskUnderContactHistory", "Search Task by name");
                office_ContactsHelper.TypeText("SearchActivity", Subject);
                office_ContactsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyContactStatusOfTaskUnderContactHistory", "Verify Contact Status is appearing");
                office_ContactsHelper.VerifyText("FirstActvtyCntctStatus", status);
                Console.WriteLine("Contact Status is appearing in front of task under Contact History "+status);


            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyContactStatusOfTaskUnderContactHistory");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Contact Status Of Task Under Contact History");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Contact Status Of Task Under Contact History", "Bug", "Medium", "Contacts page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Contact Status Of Task Under Contact History");
                        TakeScreenshot("VerifyContactStatusOfTaskUnderContactHistory");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyContactStatusOfTaskUnderContactHistory.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyContactStatusOfTaskUnderContactHistory");
                        string id = loginHelper.getIssueID("Verify Contact Status Of Task Under Contact History");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyContactStatusOfTaskUnderContactHistory.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Contact Status Of Task Under Contact History"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Contact Status Of Task Under Contact History");
                //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyContactStatusOfTaskUnderContactHistory");
                executionLog.WriteInExcel("Verify Contact Status Of Task Under Contact History", Status, JIRA, "Office Contacts");
            }
        }
    }
} 