using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class VerifyOpportunityStatusOfNoteUnderOpportunityHistory : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin")]
        [TestCategory("BugTestNew")]
        public void verifyOpportunityStatusOfNoteUnderOpportunityHistory()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            //Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var office_OpportunitiesHelper = new Office_OpportunitiesHelper(GetWebDriver());
            var officeActivities_NotesHelper = new OfficeActivities_NotesHelper(GetWebDriver());

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Variable
            var Subject = "Testnote" + RandomNumber(99, 99999);
            
            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("VerifyOpportunityStatusOfNoteUnderOpportunityHistory", "Login with valid credentials");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyOpportunityStatusOfNoteUnderOpportunityHistory", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyOpportunityStatusOfNoteUnderOpportunityHistory", "Go to All tickets page");
                VisitOffice("opportunities");
                office_OpportunitiesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyOpportunityStatusOfNoteUnderOpportunityHistory", "Verify page title.");
                VerifyTitle("Opportunities");

                executionLog.Log("VerifyOpportunityStatusOfNoteUnderOpportunityHistory", "Click on an opportunity");
                office_OpportunitiesHelper.ClickElement("Opportunities1");
                office_OpportunitiesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyOpportunityStatusOfNoteUnderOpportunityHistory", "Observe Status of opportunity");
                var status = office_OpportunitiesHelper.GetText("//div[@id='status']");

                executionLog.Log("VerifyOpportunityStatusOfNoteUnderOpportunityHistory", "Click on Add Note button");
                office_OpportunitiesHelper.ClickElement("AddNote");
                office_OpportunitiesHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyOpportunityStatusOfNoteUnderOpportunityHistory", "Enter Subject of note");
                officeActivities_NotesHelper.TypeText("Subject", Subject);

                executionLog.Log("VerifyOpportunityStatusOfNoteUnderOpportunityHistory", "Click on Save button");
                officeActivities_NotesHelper.ClickForce("OpporSaveBtn");
                office_OpportunitiesHelper.WaitForWorkAround(3000);
                office_OpportunitiesHelper.WaitForText("Note successfully Created.", 05);

                executionLog.Log("VerifyOpportunityStatusOfNoteUnderOpportunityHistory", "Select Activity Type >> Notes");
                office_OpportunitiesHelper.SelectByText("ActvtyType", "Notes");
                office_OpportunitiesHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyOpportunityStatusOfNoteUnderOpportunityHistory", "Search Note by name");
                office_OpportunitiesHelper.TypeText("ActivitySubject", Subject);
                office_OpportunitiesHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyOpportunityStatusOfNoteUnderOpportunityHistory", "Verify Opportunity Status is appearing");
                office_OpportunitiesHelper.VerifyText("ActvtyStatus1", status);
                Console.WriteLine("Opportunity Status is appearing in front of note under Opportunity History ");


            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyOpportunityStatusOfNoteUnderOpportunityHistory");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Opportunity Status Of Note Under Opportunity History");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Opportunity Status Of Note Under Opportunity History", "Bug", "Medium", "Opportunity page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Opportunity Status Of Note Under Opportunity History");
                        TakeScreenshot("VerifyOpportunityStatusOfNoteUnderOpportunityHistory");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyOpportunityStatusOfNoteUnderOpportunityHistory.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyOpportunityStatusOfNoteUnderOpportunityHistory");
                        string id = loginHelper.getIssueID("Verify Opportunity Status Of Note Under Opportunity History");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyOpportunityStatusOfNoteUnderOpportunityHistory.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Opportunity Status Of Note Under Opportunity History"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Opportunity Status Of Note Under Opportunity History");
                //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyOpportunityStatusOfNoteUnderOpportunityHistory");
                executionLog.WriteInExcel("Verify Opportunity Status Of Note Under Opportunity History", Status, JIRA, "Office Opportunity");
            }
        }
    }
} 