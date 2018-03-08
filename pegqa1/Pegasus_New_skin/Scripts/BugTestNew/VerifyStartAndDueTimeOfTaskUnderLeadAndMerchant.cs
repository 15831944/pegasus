using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class VerifyStartAndDueTimeOfTaskUnderLeadAndMerchant : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin")]
        [TestCategory("BugTestNew")]
        public void verifyStartAndDueTimeOfTaskUnderLeadAndMerchant()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            //Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var office_ClientsHelper = new Office_ClientsHelper(GetWebDriver());
            var office_LeadsHelper = new Office_LeadsHelper(GetWebDriver());
            var officeActivities_TasksHelper = new OfficeActivities_TasksHelper(GetWebDriver());
            var officeActivities_NotesHelper = new OfficeActivities_NotesHelper(GetWebDriver());
            

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Variable
            var Subject = "Testtask" + RandomNumber(99, 99999);
            
            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("VerifyStartAndDueTimeOfTaskUnderLeadAndMerchant", "Login with valid credentials");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyStartAndDueTimeOfTaskUnderLeadAndMerchant", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyStartAndDueTimeOfTaskUnderLeadAndMerchant", "Go to All leads page");
                VisitOffice("leads");
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyStartAndDueTimeOfTaskUnderLeadAndMerchant", "Verify page title.");
                VerifyTitle("Leads");

                executionLog.Log("VerifyStartAndDueTimeOfTaskUnderLeadAndMerchant", "Click on a lead");
                office_LeadsHelper.ClickElement("FirstLead");
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyStartAndDueTimeOfTaskUnderLeadAndMerchant", "Click on Add Note button");
                office_LeadsHelper.ClickElement("AddNote");
                office_LeadsHelper.WaitForWorkAround(3000);
                office_LeadsHelper.SwitchToWindow();

                executionLog.Log("VerifyStartAndDueTimeOfTaskUnderLeadAndMerchant", "Enter Subject of note");
                officeActivities_NotesHelper.TypeText("Subject", Subject);

                executionLog.Log("VerifyStartAndDueTimeOfTaskUnderLeadAndMerchant", "Select Add Task check box");
                officeActivities_NotesHelper.ClickElement("AddTaskChkBox");
                officeActivities_NotesHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyStartAndDueTimeOfTaskUnderLeadAndMerchant", "Enter Start Date of task");
                officeActivities_TasksHelper.TypeText("StartDate", "2017-10-10");

                executionLog.Log("VerifyStartAndDueTimeOfTaskUnderLeadAndMerchant", "Enter End Date of task");
                officeActivities_TasksHelper.TypeText("DueDate", "2017-10-10");

                executionLog.Log("VerifyStartAndDueTimeOfTaskUnderLeadAndMerchant", "Click on Save button");
                officeActivities_NotesHelper.ClickForce("Save");
                //officeActivities_TasksHelper.SwitchToWindow();
                officeActivities_TasksHelper.WaitForWorkAround(3000);
                officeActivities_TasksHelper.SwitchToWindow();
                officeActivities_TasksHelper.WaitForWorkAround(3000);
                office_LeadsHelper.WaitForText("Note successfully Created.", 05);

                executionLog.Log("VerifyStartAndDueTimeOfTaskUnderLeadAndMerchant", "Select Activity Type >> Tasks");
                office_LeadsHelper.WaitForWorkAround(3000);
                office_LeadsHelper.SelectByText("SelectActivityType", "Tasks");
                office_LeadsHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyStartAndDueTimeOfTaskUnderLeadAndMerchant", "Search Task by name");
                office_LeadsHelper.TypeText("ActivitySubject", Subject);
                office_LeadsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyStartAndDueTimeOfTaskUnderLeadAndMerchant", "Open task");
                office_LeadsHelper.ClickForce("ClickNotes1");
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyStartAndDueTimeOfTaskUnderLeadAndMerchant", "Click on Edit button");
                officeActivities_TasksHelper.ClickElement("EditBtn");
                officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyStartAndDueTimeOfTaskUnderLeadAndMerchant", "Verify Start Time");
                officeActivities_TasksHelper.VerifySelectdOptn("StartHour", "01");
                officeActivities_TasksHelper.VerifySelectdOptn("StartMin", "00");
                officeActivities_TasksHelper.VerifySelectdOptn("StartAMPM", "AM");

                executionLog.Log("VerifyStartAndDueTimeOfTaskUnderLeadAndMerchant", "Verify Due Time");
                officeActivities_TasksHelper.VerifySelectdOptn("DueHour", "01");
                officeActivities_TasksHelper.VerifySelectdOptn("DueMin", "15");
                officeActivities_TasksHelper.VerifySelectdOptn("DueAMPM", "AM");

                executionLog.Log("VerifyStartAndDueTimeOfTaskUnderLeadAndMerchant", "Go to All merchants page");
                VisitOffice("clients");
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyStartAndDueTimeOfTaskUnderLeadAndMerchant", "Verify page title.");
                VerifyTitle("Clients");

                executionLog.Log("VerifyStartAndDueTimeOfTaskUnderLeadAndMerchant", "Click on a merchant");
                office_ClientsHelper.ClickElement("Client1");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyStartAndDueTimeOfTaskUnderLeadAndMerchant", "Click on Add Note button");
                office_ClientsHelper.ClickElement("AddNotes");
                office_ClientsHelper.WaitForWorkAround(3000);
                office_ClientsHelper.SwitchToWindow();

                executionLog.Log("VerifyStartAndDueTimeOfTaskUnderLeadAndMerchant", "Enter Subject of note");
                officeActivities_NotesHelper.TypeText("Subject", Subject);

                executionLog.Log("VerifyStartAndDueTimeOfTaskUnderLeadAndMerchant", "Select Add Task check box");
                officeActivities_NotesHelper.ClickElement("AddTaskChkBox");
                officeActivities_NotesHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyStartAndDueTimeOfTaskUnderLeadAndMerchant", "Enter Start Date of task");
                officeActivities_TasksHelper.TypeText("StartDate", "2017-10-10");

                executionLog.Log("VerifyStartAndDueTimeOfTaskUnderLeadAndMerchant", "Enter End Date of task");
                officeActivities_TasksHelper.TypeText("DueDate", "2017-10-10");

                executionLog.Log("VerifyStartAndDueTimeOfTaskUnderLeadAndMerchant", "Click on Save button");
                officeActivities_NotesHelper.ClickForce("Save");
                //officeActivities_TasksHelper.SwitchToWindow();
                officeActivities_TasksHelper.WaitForWorkAround(3000);
                officeActivities_TasksHelper.SwitchToWindow();
                officeActivities_TasksHelper.WaitForWorkAround(3000);
                office_ClientsHelper.WaitForText("Note successfully Created.", 05);

                executionLog.Log("VerifyStartAndDueTimeOfTaskUnderLeadAndMerchant", "Select Activity Type >> Tasks");
                office_ClientsHelper.WaitForWorkAround(3000);
                office_ClientsHelper.SelectByText("ActivityType", "Tasks");
                office_ClientsHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyStartAndDueTimeOfTaskUnderLeadAndMerchant", "Search Task by name");
                office_ClientsHelper.TypeText("SearchActivity", Subject);
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyStartAndDueTimeOfTaskUnderLeadAndMerchant", "Open task");
                office_ClientsHelper.ClickForce("Activity1");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyStartAndDueTimeOfTaskUnderLeadAndMerchant", "Click on Edit button");
                officeActivities_TasksHelper.ClickElement("EditBtn");
                officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyStartAndDueTimeOfTaskUnderLeadAndMerchant", "Verify Start Time");
                officeActivities_TasksHelper.VerifySelectdOptn("StartHour", "01");
                officeActivities_TasksHelper.VerifySelectdOptn("StartMin", "00");
                officeActivities_TasksHelper.VerifySelectdOptn("StartAMPM", "AM");

                executionLog.Log("VerifyStartAndDueTimeOfTaskUnderLeadAndMerchant", "Verify Due Time");
                officeActivities_TasksHelper.VerifySelectdOptn("DueHour", "01");
                officeActivities_TasksHelper.VerifySelectdOptn("DueMin", "15");
                officeActivities_TasksHelper.VerifySelectdOptn("DueAMPM", "AM");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyStartAndDueTimeOfTaskUnderLeadAndMerchant");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Start And Due Time Of Task Under Lead And Merchant");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Start And Due Time Of Task Under Lead And Merchant", "Bug", "Medium", "Leads and Merchants page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Start And Due Time Of Task Under Lead And Merchant");
                        TakeScreenshot("VerifyStartAndDueTimeOfTaskUnderLeadAndMerchant");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyStartAndDueTimeOfTaskUnderLeadAndMerchant.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyStartAndDueTimeOfTaskUnderLeadAndMerchant");
                        string id = loginHelper.getIssueID("Verify Start And Due Time Of Task Under Lead And Merchant");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyStartAndDueTimeOfTaskUnderLeadAndMerchant.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Start And Due Time Of Task Under Lead And Merchant"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Start And Due Time Of Task Under Lead And Merchant");
                //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyStartAndDueTimeOfTaskUnderLeadAndMerchant");
                executionLog.WriteInExcel("Verify Start And Due Time Of Task Under Lead And Merchant", Status, JIRA, "Office Leads And Merchants");
            }
        }
    }
} 