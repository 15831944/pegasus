using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class VerifyCreatorNameOfTaskAndNoteInMerchant : DriverTestCase
    {
        [TestMethod]
        [TestCategory("Bug")]
        [TestCategory("TS2")]
        [TestCategory("BugTestNew")]
        public void verifyCreatorNameOfTaskAndNoteInMerchant()
        {
            string[] username1 = null;
            string[] username = null;
            string[] password = null;

            XMLParse oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_corp");
            username1 = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var corp_MerchantHelper = new Corp_MerchantHelper(GetWebDriver());
            var office_clientsHelper = new Office_ClientsHelper(GetWebDriver());
            var myprofilepagehelper = new MyProfilePageHelper(GetWebDriver());
            var officeActivities_Taskshelper = new OfficeActivities_TasksHelper(GetWebDriver());
            var officeActivities_NotesHelper = new OfficeActivities_NotesHelper(GetWebDriver());

            // variables
            String JIRA = "";
            String Status = "Pass";
            var subject = "TestTask" + RandomNumber(1,999999);
            var subject1 = "TestNote" + RandomNumber(1, 999999);
            try
            {
                executionLog.Log("VerifyCreatorNameOfTaskAndNoteInMerchant", "Login to office portal with valid username and password");
                Login(username1[0], password[0]);
                Console.WriteLine("Logged in as: " + username1[0] + " / " + password[0]);

                executionLog.Log("VerifyCreatorNameOfTaskAndNoteInMerchant", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyCreatorNameOfTaskAndNoteInMerchant", "Redirect at My Profile page.");
                VisitOffice("myprofile");

                executionLog.Log("VerifyCreatorNameOfTaskAndNoteInMerchant", "Click on Edit profile Button.");
                myprofilepagehelper.ClickElement("EditProfileBtn");
                myprofilepagehelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyCreatorNameOfTaskAndNoteInMerchant", "Get First Name of User");
                var name = myprofilepagehelper.GetValue("//input[@id='EmployeeFirstName']");

                executionLog.Log("VerifyCreatorNameOfTaskAndNoteInMerchant", "Redirect at merchants page.");
                VisitOffice("clients");
                office_clientsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyCreatorNameOfTaskAndNoteInMerchant", "Click on a client");
                var clientname = office_clientsHelper.GetText("//table[@id='list1']/tbody/tr[2]/td[15]/a");
                office_clientsHelper.ClickElement("Client1");
                office_clientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCreatorNameOfTaskAndNoteInMerchant", "Click on New Task");
                office_clientsHelper.ClickElement("AddTask");

                executionLog.Log("VerifyCreatorNameOfTaskAndNoteInMerchant", "Enter Subject");
                officeActivities_Taskshelper.TypeText("Subject", subject);

                executionLog.Log("VerifyCreatorNameOfTaskAndNoteInMerchant", "Enter Start Date");
                officeActivities_Taskshelper.TypeText("StartDate", "27/11/2017");

                executionLog.Log("VerifyCreatorNameOfTaskAndNoteInMerchant", "Enter Due Date");
                officeActivities_Taskshelper.TypeText("DueDate", "28/11/2017");

                executionLog.Log("VerifyCreatorNameOfTaskAndNoteInMerchant", "Click on Save button");
                officeActivities_Taskshelper.ClickElement("Save");
                officeActivities_Taskshelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCreatorNameOfTaskAndNoteInMerchant", "Click on Add Note button");
                office_clientsHelper.ClickElement("AddNotes");
                office_clientsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyCreatorNameOfTaskAndNoteInMerchant", "Change to current window");
                office_clientsHelper.SwitchToWindow();

                executionLog.Log("VerifyCreatorNameOfTaskAndNoteInMerchant", "Enter Subject");
                officeActivities_NotesHelper.TypeText("Subject", subject1);

                executionLog.Log("VerifyCreatorNameOfTaskAndNoteInMerchant", "Click on Add Note button");
                officeActivities_NotesHelper.ClickElement("Save");
                officeActivities_NotesHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyCreatorNameOfTaskAndNoteInMerchant", "Change to current window");
                office_clientsHelper.SwitchToWindow();

                //executionLog.Log("VerifyCreatorNameOfTaskAndNoteInMerchant", "Logout");
                //Logout();

                executionLog.Log("VerifyCreatorNameOfTaskAndNoteInMerchant", "Click on Profile Icon");
                office_clientsHelper.ClickElement("ProfileIcon");

                executionLog.Log("VerifyCreatorNameOfTaskAndNoteInMerchant", "Click on Logout");
                office_clientsHelper.ClickElement("Logout");
                office_clientsHelper.WaitForWorkAround(3000);
                Console.WriteLine("Redirected to Login Screen");

                executionLog.Log("VerifyCreatorNameOfTaskAndNoteInMerchant", "Login to corporate portal with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyCreatorNameOfTaskAndNoteInMerchant", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyCreatorNameOfTaskAndNoteInMerchant", "Redirect at merchants page.");
                VisitCorp("merchants");
                corp_MerchantHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyCreatorNameOfTaskAndNoteInMerchant", "Enter client name in search box");
                corp_MerchantHelper.TypeText("EnterClinentToSearch", clientname);
                corp_MerchantHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyCreatorNameOfTaskAndNoteInMerchant", "Click on Merchant");
                corp_MerchantHelper.ClickElement("ClickOnMerchant");
                corp_MerchantHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyCreatorNameOfTaskAndNoteInMerchant", "Select Activity Type to Tasks");
                corp_MerchantHelper.Select("SelectActivityType", "Tasks");
                corp_MerchantHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyCreatorNameOfTaskAndNoteInMerchant", "Enter task subject name");
                corp_MerchantHelper.TypeText("SearchActivityName", subject);
                corp_MerchantHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyCreatorNameOfTaskAndNoteInMerchant", "Click on task");
                corp_MerchantHelper.ClickElement("ClickOnActivityAny");
                corp_MerchantHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCreatorNameOfTaskAndNoteInMerchant", "Verify Created By name is appearing");
                corp_MerchantHelper.VerifyText("CreatedOnText", name);
                Console.WriteLine("Creator Name of Task is "+name);

                executionLog.Log("VerifyCreatorNameOfTaskAndNoteInMerchant", "Redirect at merchants page.");
                VisitCorp("merchants");
                corp_MerchantHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyCreatorNameOfTaskAndNoteInMerchant", "Enter client name in search box");
                corp_MerchantHelper.TypeText("EnterClinentToSearch", clientname);
                corp_MerchantHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyCreatorNameOfTaskAndNoteInMerchant", "Click on Merchant");
                corp_MerchantHelper.ClickElement("ClickOnMerchant");
                corp_MerchantHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyCreatorNameOfTaskAndNoteInMerchant", "Select Activity Type to Notes");
                corp_MerchantHelper.Select("SelectActivityType", "Notes");
                corp_MerchantHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyCreatorNameOfTaskAndNoteInMerchant", "Enter note subject name");
                corp_MerchantHelper.TypeText("SearchActivityName", subject1);
                corp_MerchantHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyCreatorNameOfTaskAndNoteInMerchant", "Click on note");
                corp_MerchantHelper.ClickElement("ClickOnActivityAny");
                corp_MerchantHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCreatorNameOfTaskAndNoteInMerchant", "Verify Created By name is appearing");
                corp_MerchantHelper.VerifyText("CreatedOnText", name);
                Console.WriteLine("Creator Name of Task is " + name);


            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyCreatorNameOfTaskAndNoteInMerchant");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Creator Name Of Task And Note In Merchant");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Creator Name Of Task And Note In Merchant", "Bug", "Medium", "Merchant corp", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Creator Name Of Task And Note In Merchant");
                        TakeScreenshot("VerifyCreatorNameOfTaskAndNoteInMerchant");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyCreatorNameOfTaskAndNoteInMerchant.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyCreatorNameOfTaskAndNoteInMerchant");
                        string id = loginHelper.getIssueID("Verify Creator Name Of Task And Note In Merchant");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyCreatorNameOfTaskAndNoteInMerchant.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Creator Name Of Task And Note In Merchant"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Creator Name Of Task And Note In Merchant");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyCreatorNameOfTaskAndNoteInMerchant");
                executionLog.WriteInExcel("Verify Creator Name Of Task And Note In Merchant", Status, JIRA, "Corp Merchant");
            }
        }
    }
}