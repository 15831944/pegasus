using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class NotesAdvanceFilterRelatedTo : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin1")]
        [TestCategory("Fail")]
        [TestCategory("TS6")]
        [TestCategory("Pegasus_Admin1")]
        public void notesAdvanceFilterRelatedTo()
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
            var officeActivities_NotesHelper = new OfficeActivities_NotesHelper(GetWebDriver());

            // Variable
            var DocName = "Test Exe" + GetRandomNumber();
            var fileUpl = GetPathToFile() + "chrome.exe";
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("NotesAdvanceFilterRelatedTo", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("NotesAdvanceFilterRelatedTo", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");


                // Verify notes with clients.

                executionLog.Log("NotesAdvanceFilterRelatedTo", "Redirect To URL");
                VisitOffice("notes");
                officeActivities_NotesHelper.WaitForWorkAround(5000);

                executionLog.Log("NotesAdvanceFilterRelatedTo", "Verify page title.");
                VerifyTitle("Notes");

                executionLog.Log("NotesAdvanceFilterRelatedTo", "Click on advance filter.");
                officeActivities_NotesHelper.ClickForce("AdvanceFilter");
                officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("NotesAdvanceFilterRelatedTo", "click notes with activity type.");
                officeActivities_NotesHelper.ClickForce("NoteWithClient");
                officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("NotesAdvanceFilterRelatedTo", "Click on apply button.");
                officeActivities_NotesHelper.ClickForce("Apply");
                officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("NotesAdvanceFilterRelatedTo", "Verify note present is related to clients");
                officeActivities_NotesHelper.VerifyText("NoteRelatedTo", "Merchants");

                //Verify notes with contacts.

                executionLog.Log("NotesAdvanceFilterRelatedTo", "Redirect To URL");
                VisitOffice("notes");
                officeActivities_NotesHelper.WaitForWorkAround(5000);

                executionLog.Log("NotesAdvanceFilterRelatedTo", "Verify page title.");
                VerifyTitle("Notes");

                executionLog.Log("NotesAdvanceFilterRelatedTo", "Click on advance filter.");
                officeActivities_NotesHelper.ClickForce("AdvanceFilter");
                officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("NotesAdvanceFilterRelatedTo", "Selct notes related to contacts");
                officeActivities_NotesHelper.ClickForce("NoteWithCOntacts");
                officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("NotesAdvanceFilterRelatedTo", "Click on apply button.");
                officeActivities_NotesHelper.ClickForce("Apply");
                officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("NotesAdvanceFilterRelatedTo", "Verify note present is related to contacts.");
                officeActivities_NotesHelper.VerifyText("NoteRelatedTo", "Contacts");

                //Verify notes with Leads.

                executionLog.Log("NotesAdvanceFilterRelatedTo", "Redirect To URL");
                VisitOffice("notes");
                officeActivities_NotesHelper.WaitForWorkAround(5000);

                executionLog.Log("NotesAdvanceFilterRelatedTo", "Verify page title.");
                VerifyTitle("Notes");

                executionLog.Log("NotesAdvanceFilterRelatedTo", "Click on advance filter.");
                officeActivities_NotesHelper.ClickElement("AdvanceFilter");
                officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("NotesAdvanceFilterRelatedTo", "click on note with activity type.");
                officeActivities_NotesHelper.ClickForce("NoteWithLeads");
                officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("NotesAdvanceFilterRelatedTo", "Click on apply button.");
                officeActivities_NotesHelper.ClickForce("Apply");
                officeActivities_NotesHelper.WaitForWorkAround(5000);

                executionLog.Log("NotesAdvanceFilterRelatedTo", "Verify note present is related to leads.");
                officeActivities_NotesHelper.VerifyText("NoteRelatedTo", "Leads");

                // Verify notes with meetings .

                executionLog.Log("NotesAdvanceFilterRelatedTo", "Redirect To URL");
                VisitOffice("notes");
                officeActivities_NotesHelper.WaitForWorkAround(5000);

                executionLog.Log("NotesAdvanceFilterRelatedTo", "Verify page title.");
                VerifyTitle("Notes");

                executionLog.Log("NotesAdvanceFilterRelatedTo", "Click on advance filter.");
                officeActivities_NotesHelper.ClickForce("AdvanceFilter");
                officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("NotesAdvanceFilterRelatedTo", "click on note  with meetings.");
                officeActivities_NotesHelper.ClickForce("NoteWithMeetings");
                officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("NotesAdvanceFilterRelatedTo", "Click on apply button.");
                officeActivities_NotesHelper.ClickForce("Apply");
                officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("NotesAdvanceFilterRelatedTo", "Verify note present is related to meetings.");
                officeActivities_NotesHelper.VerifyText("NoteRelatedTo", "Meetings");

                // Verify notes with Opportunities .
                executionLog.Log("NotesAdvanceFilterRelatedTo", "Redirect To URL");
                VisitOffice("notes");
                officeActivities_NotesHelper.WaitForWorkAround(5000);

                executionLog.Log("NotesAdvanceFilterRelatedTo", "Verify page title.");
                VerifyTitle("Notes");

                executionLog.Log("NotesAdvanceFilterRelatedTo", "Click on advance filter.");
                officeActivities_NotesHelper.ClickForce("AdvanceFilter");
                officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("NotesAdvanceFilterRelatedTo", "click on notes  with opportunities.");
                officeActivities_NotesHelper.ClickForce("NoteWithOppo");
                officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("NotesAdvanceFilterRelatedTo", "Click on apply button.");
                officeActivities_NotesHelper.ClickForce("Apply");
                officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("NotesAdvanceFilterRelatedTo", "Verify note present is related to opportunities.");
                officeActivities_NotesHelper.VerifyText("NoteRelatedTo", "Opportunities");

                // Verify notes with tasks .
                executionLog.Log("NotesAdvanceFilterRelatedTo", "Redirect To URL");
                VisitOffice("notes");
                officeActivities_NotesHelper.WaitForWorkAround(5000);

                executionLog.Log("NotesAdvanceFilterRelatedTo", "Verify page title.");
                VerifyTitle("Notes");

                executionLog.Log("NotesAdvanceFilterRelatedTo", "Click on advance filter.");
                officeActivities_NotesHelper.ClickForce("AdvanceFilter");
                officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("NotesAdvanceFilterRelatedTo", "click on note  with tasks.");
                officeActivities_NotesHelper.ClickForce("NoteWithTask");
                officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("NotesAdvanceFilterRelatedTo", "Click on apply button.");
                officeActivities_NotesHelper.ClickForce("Apply");
                officeActivities_NotesHelper.WaitForWorkAround(5000);

                executionLog.Log("NotesAdvanceFilterRelatedTo", "Verify note present is related to tasks.");
                officeActivities_NotesHelper.VerifyText("NoteRelatedTo", "Tasks");


                // Verify notes with Attachments .

                executionLog.Log("NotesAdvanceFilterRelatedTo", "Redirect To URL");
                VisitOffice("notes");
                officeActivities_NotesHelper.WaitForWorkAround(5000);

                executionLog.Log("NotesAdvanceFilterRelatedTo", "Verify page title.");
                VerifyTitle("Notes");

                executionLog.Log("NotesAdvanceFilterRelatedTo", "Click on advance filter.");
                officeActivities_NotesHelper.ClickForce("AdvanceFilter");
                officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("NotesAdvanceFilterRelatedTo", "click note with attachment.");
                officeActivities_NotesHelper.ClickForce("NoteWithAttach");
                officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("NotesAdvanceFilterRelatedTo", "Click on apply button.");
                officeActivities_NotesHelper.ClickForce("Apply");
                officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("NotesAdvanceFilterRelatedTo", "Click on any note.");
                officeActivities_NotesHelper.ClickForce("ClickOnNote");
                officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("NotesAdvanceFilterRelatedTo", "Verify note contains documents.");
                officeActivities_NotesHelper.IsElementPresent("AttachmentNotes");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("NotesAdvanceFilterRelatedTo");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Notes Advance Filter RelatedTo");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Notes Advance Filter RelatedTo", "Bug", "Medium", "Opportunities page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Notes Advance Filter RelatedTo");
                        TakeScreenshot("NotesAdvanceFilterRelatedTo");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\NotesAdvanceFilterRelatedTo.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("NotesAdvanceFilterRelatedTo");
                        string id = loginHelper.getIssueID("Notes Advance Filter RelatedTo");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\NotesAdvanceFilterRelatedTo.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Notes Advance Filter RelatedTo"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Notes Advance Filter RelatedTo");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("NotesAdvanceFilterRelatedTo");
                executionLog.WriteInExcel("Notes Advance Filter RelatedTo", Status, JIRA, "Opportunities Management");
            }
        }
    }
}