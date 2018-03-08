using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class OpportunitiesnNotesUrlChange : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Url")]
        [TestCategory("Fail")]
        [TestCategory("TS4")]
        [TestCategory("ChangeUrl")]
        public void opportunitiesnNotesUrlChange()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var office_OpportunitiesHelper = new Office_OpportunitiesHelper(GetWebDriver());
            var officeActivities_NotesHelper = new OfficeActivities_NotesHelper(GetWebDriver());

            // Variable
            var Subject = "Subject" + RandomNumber(1, 999);
            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("OpportunitiesnNotesUrlChange", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("OpportunitiesnNotesUrlChange", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("OpportunitiesnNotesUrlChange", "Goto  Opportunities");
                VisitOffice("opportunities");

                executionLog.Log("OpportunitiesnNotesUrlChange", "Click On Any Opportunities tab");
                office_OpportunitiesHelper.ClickElement("OpenOpportunity");
                office_OpportunitiesHelper.WaitForWorkAround(2000);

                executionLog.Log("OpportunitiesnNotesUrlChange", "Click Add Note button");
                office_OpportunitiesHelper.ClickElement("AddNote");
                office_OpportunitiesHelper.WaitForWorkAround(10000);

                executionLog.Log("OpportunitiesnNotesUrlChange", "Enter Note Subject");
                officeActivities_NotesHelper.TypeText("Subject", Subject);
                office_OpportunitiesHelper.WaitForWorkAround(10000);

                executionLog.Log("OpportunitiesnNotesUrlChange", "Click Save");
                officeActivities_NotesHelper.ClickElement("SaveNoteoppo");
                officeActivities_NotesHelper.WaitForWorkAround(2000);

                executionLog.Log("OpportunitiesnNotesUrlChange", "Select Activity >> Notes");
                officeActivities_NotesHelper.Select("SelectActivityType", "Notes");
                officeActivities_NotesHelper.WaitForWorkAround(2000);

                executionLog.Log("OpportunitiesnNotesUrlChange", "Select All in created by field");
                officeActivities_NotesHelper.SelectByText("CreatedbyField", "All");
                officeActivities_NotesHelper.WaitForWorkAround(2000);

                executionLog.Log("OpportunitiesnNotesUrlChange", "Click On Notes ");
                officeActivities_NotesHelper.ClickForce("ClickNotes1");
                officeActivities_NotesHelper.WaitForWorkAround(4000);

                executionLog.Log("OpportunitiesnNotesUrlChange", "Change the url with the url number of another office");
                VisitOffice("notes/view/16");

                executionLog.Log("OpportunitiesnNotesUrlChange", "Verify Validation");
                officeActivities_NotesHelper.WaitForText("You don't have privileges to view this note.", 10);
                officeActivities_NotesHelper.WaitForWorkAround(2000);

                executionLog.Log("OpportunitiesnNotesUrlChange", "Redirect at notes page.");
                VisitOffice("notes");

                executionLog.Log("OpportunitiesnNotesUrlChange", "Search subject by note");
                officeActivities_NotesHelper.TypeText("EnterSubject", Subject);
                officeActivities_NotesHelper.WaitForWorkAround(2000);

                executionLog.Log("OpportunitiesnNotesUrlChange", "Select All in created by field");
                officeActivities_NotesHelper.SelectByText("CreatedbyField", "All");
                officeActivities_NotesHelper.WaitForWorkAround(2000);

                executionLog.Log("OpportunitiesnNotesUrlChange", "Search subject by note");
                officeActivities_NotesHelper.ClickElement("SelectNote1");

                executionLog.Log("OpportunitiesnNotesUrlChange", "Click on delete note.");
                officeActivities_NotesHelper.ClickElement("DeleteNote");

                executionLog.Log("OpportunitiesnNotesUrlChange", "Accept alert message.");
                officeActivities_NotesHelper.AcceptAlert();

                executionLog.Log("OpportunitiesnNotesUrlChange", "Wait for delete text");
                officeActivities_NotesHelper.WaitForText("Note deleted successfully.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("OpportunitiesnNotesUrlChange");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Opportunities Notes Url Change");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Opportunities Notes Url Change", "Bug", "Medium", "Opportunities page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Opportunities Notes Url Change");
                        TakeScreenshot("OpportunitiesnNotesUrlChange");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\OpportunitiesnNotesUrlChange.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("OpportunitiesnNotesUrlChange");
                        string id = loginHelper.getIssueID("Opportunities Notes Url Change");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\OpportunitiesnNotesUrlChange.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Opportunities Notes Url Change"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Opportunities Notes Url Change");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("OpportunitiesnNotesUrlChange");
                executionLog.WriteInExcel("Opportunities Notes Url Change", Status, JIRA, "Office opportunities");
            }
        }
    }
}
