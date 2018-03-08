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
    public class ActivitiesNotesManagement : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin")]
        [TestCategory("Fail")]
        [TestCategory("TS6")]
        [TestCategory("Pegasus_Admin")]
        public void activitiesNotesManagement()
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
            var officeActivities_NotesHelper = new OfficeActivities_NotesHelper(GetWebDriver());
            var office_ClientsHelper = new Office_ClientsHelper(GetWebDriver());
            var office_LeadsHelper = new Office_LeadsHelper(GetWebDriver());
            var office_OpportunitiesHelper = new Office_OpportunitiesHelper(GetWebDriver());
            var ticket_CreateATicketHelper = new OfficeTickets_CreateTicketsHelper(GetWebDriver());

            // Variable
            var name = "Note" + RandomNumber(1, 99);
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("ActivitiesNotesManagement", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("ActivitiesNotesManagement", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("ActivitiesNotesManagement", "Redirect at admin page.");
                VisitOffice("admin");

                executionLog.Log("ActivitiesNotesManagement", "Go to notes page");
                VisitOffice("notes");
                officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesNotesManagement", "Verify page title");
                VerifyTitle("Notes");

                executionLog.Log("ActivitiesNotesManagement", " Click On Create");
                officeActivities_NotesHelper.ClickElement("Create");
                officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesNotesManagement", "Verify page title");
                VerifyTitle("Create a New Note");

                executionLog.Log("ActivitiesNotesManagement", "Click on Save  ");
                officeActivities_NotesHelper.ClickElement("Save");

                executionLog.Log("ActivitiesNotesManagement", "Verify validation text for subject.");
                officeActivities_NotesHelper.VerifyText("SubjectError", "This field is required.");

                executionLog.Log("ActivitiesNotesManagement", "Enter note subject.");
                officeActivities_NotesHelper.TypeText("Subject", name);

                executionLog.Log("ActivitiesNotesManagement", "Click on save.");
                officeActivities_NotesHelper.ClickElement("Save");

                executionLog.Log("ActivitiesNotesManagement", "Wait for success text");
                officeActivities_NotesHelper.WaitForText("Note saved successfully. ", 10);
                officeActivities_NotesHelper.WaitForWorkAround(2000);

                executionLog.Log("ActivitiesNotesManagement", "Enter Subject in Search field");
                officeActivities_NotesHelper.TypeText("EnterSubject", name);
                officeActivities_NotesHelper.WaitForWorkAround(2000);

                executionLog.Log("ActivitiesNotesManagement", "Select All in created by field");
                officeActivities_NotesHelper.SelectByText("CreatedbyField", "All");
                officeActivities_NotesHelper.WaitForWorkAround(2000);

                executionLog.Log("ActivitiesNotesManagement", "Click on Edit");
                officeActivities_NotesHelper.ClickElement("Edit");
                VerifyTitle("Edit Note");

                executionLog.Log("ActivitiesNotesManagement", "Select note parent");
                officeActivities_NotesHelper.Select("NoteParent", "20");

                executionLog.Log("ActivitiesNotesManagement", "Click on find list icon.");
                officeActivities_NotesHelper.ClickElement("SelectClient");
                officeActivities_NotesHelper.WaitForWorkAround(2000);

                executionLog.Log("ActivitiesNotesManagement", " Click On any client.");
                officeActivities_NotesHelper.ClickElement("ClickONClientNS");
                officeActivities_NotesHelper.WaitForWorkAround(2000);

                executionLog.Log("ActivitiesNotesManagement", "Click on save button.");
                officeActivities_NotesHelper.ClickElement("Save");

                executionLog.Log("ActivitiesNotesManagement", "Verify note updated successfully");
                officeActivities_NotesHelper.WaitForText("Note Updated Success.", 10);

                executionLog.Log("ActivitiesNotesManagement", "Redirect at clients page.");
                VisitOffice("clients");
                officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesNotesManagement", "Click on any client.");
                office_ClientsHelper.ClickElement("Client1");
                officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesNotesManagement", "Select actitivity type as notes.");
                office_ClientsHelper.Select("SelectActivityType", "Notes");

                executionLog.Log("ActivitiesNotesManagement", "Enter note name to be search.");
                office_ClientsHelper.TypeText("ActivitySubject", name);
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("ActivitiesNotesManagement", "Verify created note present on client page.");
                office_ClientsHelper.IsElementPresent("OpenFirstActivity");

                executionLog.Log("ActivitiesNotesManagement", "Go to note page");
                VisitOffice("notes");
                officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesNotesManagement", "Verify page title");
                VerifyTitle("Notes");
                //officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesNotesManagement", "Enter Subject in Search field");
                officeActivities_NotesHelper.TypeText("EnterSubject", name);
                officeActivities_NotesHelper.WaitForWorkAround(2000);

                executionLog.Log("ActivitiesNotesManagement", "Select All in created by field");
                officeActivities_NotesHelper.SelectByText("CreatedbyField", "All");
                officeActivities_NotesHelper.WaitForWorkAround(2000);

                executionLog.Log("ActivitiesNotesManagement", "Click on Edit");
                officeActivities_NotesHelper.ClickElement("Edit");
                officeActivities_NotesHelper.WaitForWorkAround(3000);
                VerifyTitle("Edit Note");
                

                executionLog.Log("ActivitiesNotesManagement", "Select note parent as lead.");
                officeActivities_NotesHelper.Select("NoteParent", "14");

                executionLog.Log("ActivitiesNotesManagement", "Click on find list icon.");
                officeActivities_NotesHelper.ClickElement("SelectClient");
                officeActivities_NotesHelper.WaitForWorkAround(2000);

                executionLog.Log("ActivitiesNotesManagement", " Click On any opportunity");
                officeActivities_NotesHelper.ClickElement("ClickONClientNS");
                officeActivities_NotesHelper.WaitForWorkAround(2000);

                executionLog.Log("ActivitiesNotesManagement", "Click on save button.");
                officeActivities_NotesHelper.ClickElement("Save");

                executionLog.Log("ActivitiesNotesManagement", "Verify note updated successfully");
                officeActivities_NotesHelper.WaitForText("Note Updated Success.", 10);

                executionLog.Log("ActivitiesNotesManagement", "Redirect at leads page.");
                VisitOffice("leads");
                officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesNotesManagement", "Click On any lead.");
                office_LeadsHelper.ClickElement("ClickAnyLead");
                officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesNotesManagement", "Select actitivity type as notes.");
                office_LeadsHelper.Select("SelectActivityType", "Notes");

                executionLog.Log("ActivitiesNotesManagement", "Enter note name to be search.");
                office_LeadsHelper.TypeText("ActivitySubject", name);
                office_LeadsHelper.WaitForWorkAround(2000);

                executionLog.Log("ActivitiesNotesManagement", "Select All in created by field");
                office_LeadsHelper.SelectByText("CreatedByField", "All");
                office_LeadsHelper.WaitForWorkAround(2000);

                executionLog.Log("ActivitiesNotesManagement", "Verify created note present on leads page.");
                office_LeadsHelper.IsElementPresent("ClickNotes1");

                executionLog.Log("ActivitiesNotesManagement", "Go to note page");
                VisitOffice("notes");
                officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesNotesManagement", "Verify page title");
                VerifyTitle("Notes");

                executionLog.Log("ActivitiesNotesManagement", "Enter Subject in Search field");
                officeActivities_NotesHelper.TypeText("EnterSubject", name);
                officeActivities_NotesHelper.WaitForWorkAround(2000);

                executionLog.Log("ActivitiesNotesManagement", "Select All in created by field");
                officeActivities_NotesHelper.SelectByText("CreatedbyField", "All");
                officeActivities_NotesHelper.WaitForWorkAround(2000);

                executionLog.Log("ActivitiesNotesManagement", "Click on Edit");
                officeActivities_NotesHelper.ClickElement("Edit");
                VerifyTitle("Edit Note");

                executionLog.Log("ActivitiesNotesManagement", "Select note parent as opportunity.");
                officeActivities_NotesHelper.Select("NoteParent", "15");

                executionLog.Log("ActivitiesNotesManagement", "Click on findlist icon.");
                officeActivities_NotesHelper.ClickElement("SelectClient");
                officeActivities_NotesHelper.WaitForWorkAround(2000);

                executionLog.Log("ActivitiesNotesManagement", " Click On any opportunity");
                officeActivities_NotesHelper.ClickElement("ClickONClientNS");
                officeActivities_NotesHelper.WaitForWorkAround(2000);

                executionLog.Log("ActivitiesNotesManagement", "Edit on Save Btn");
                officeActivities_NotesHelper.ClickElement("Save");

                executionLog.Log("ActivitiesNotesManagement", "Verify note updated successfully");
                officeActivities_NotesHelper.WaitForText("Note Updated Success.", 10);

                executionLog.Log("ActivitiesNotesManagement", "Redirect at opportunities page.");
                VisitOffice("opportunities");
                officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesNotesManagement", "Click On any opportunity.");
                office_OpportunitiesHelper.ClickElement("Opportunities1");
                officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesNotesManagement", "Select actitivity type as notes");
                office_LeadsHelper.Select("SelectActivityType", "Notes");

                executionLog.Log("ActivitiesNotesManagement", "Enter notes name to be search.");
                office_OpportunitiesHelper.TypeText("ActivitySubject", name);
                office_LeadsHelper.WaitForWorkAround(2000);

                executionLog.Log("ActivitiesNotesManagement", "Select All in created by field");
                office_OpportunitiesHelper.SelectByText("CreateByField", "All");
                office_OpportunitiesHelper.WaitForWorkAround(2000);

                executionLog.Log("ActivitiesNotesManagement", "Verify created note present on opportunity page");
                office_OpportunitiesHelper.IsElementPresent("OpenOpportunity");

                executionLog.Log("ActivitiesNotesManagement", "Go to note page");
                VisitOffice("notes");
                officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesNotesManagement", "Verify title");
                VerifyTitle("Notes");

                executionLog.Log("ActivitiesNotesManagement", "Enter Subject in Search field");
                officeActivities_NotesHelper.TypeText("EnterSubject", name);
                officeActivities_NotesHelper.WaitForWorkAround(2000);

                executionLog.Log("ActivitiesNotesManagement", "Select All in created by field");
                officeActivities_NotesHelper.SelectByText("CreatedbyField", "All");
                officeActivities_NotesHelper.WaitForWorkAround(2000);

                executionLog.Log("ActivitiesNotesManagement", "Click on Edit");
                officeActivities_NotesHelper.ClickElement("Edit");
                VerifyTitle("Edit Note");

                executionLog.Log("ActivitiesNotesManagement", "Select note parent as tickets.");
                officeActivities_NotesHelper.Select("NoteParent", "36");

                executionLog.Log("ActivitiesNotesManagement", "Click on find list icon.");
                officeActivities_NotesHelper.ClickElement("SelectClient");
                officeActivities_NotesHelper.WaitForWorkAround(2000);

                executionLog.Log("ActivitiesNotesManagement", " Click On any ticket.");
                officeActivities_NotesHelper.ClickElement("ClickONClientNS");
                officeActivities_NotesHelper.WaitForWorkAround(2000);

                executionLog.Log("ActivitiesNotesManagement", "Edit on Save Btn");
                officeActivities_NotesHelper.ClickElement("Save");

                executionLog.Log("ActivitiesNotesManagement", "Verify note updated successfully");
                officeActivities_NotesHelper.WaitForText("Note Updated Success.", 10);

                executionLog.Log("ActivitiesNotesManagement", "Redirect at tickets page.");
                VisitOffice("tickets");
                officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesNotesManagement", "Click On any ticket.");
                ticket_CreateATicketHelper.ClickElement("Ticket1");
                officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesNotesManagement", "Select actitivity type as notes");
                office_LeadsHelper.Select("SelectActivityType", "Notes");

                executionLog.Log("ActivitiesNotesManagement", "Enter ticket name to be search.");
                office_OpportunitiesHelper.TypeText("ActivitySubject", name);
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesNotesManagement", "Select All in created by field");
                office_OpportunitiesHelper.SelectByText("CreateByField", "All");
                office_OpportunitiesHelper.WaitForWorkAround(2000);

                executionLog.Log("ActivitiesNotesManagement", "Verify created note present on ticket page.");
                ticket_CreateATicketHelper.IsElementPresent("OpenTicket");

                executionLog.Log("ActivitiesNotesManagement", "Go to note page");
                VisitOffice("notes");
                officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesNotesManagement", "Verify page title.");
                VerifyTitle("Notes");

                executionLog.Log("ActivitiesNotesManagement", "Enter Subject in Search field");
                officeActivities_NotesHelper.TypeText("EnterSubject", name);
                officeActivities_NotesHelper.WaitForWorkAround(2000);

                executionLog.Log("ActivitiesNotesManagement", "Select All in created by field");
                officeActivities_NotesHelper.SelectByText("CreatedbyField", "All");
                officeActivities_NotesHelper.WaitForWorkAround(2000);

                executionLog.Log("ActivitiesNotesManagement", "Select First Note");
                officeActivities_NotesHelper.ClickElement("SelectNote1");
                officeActivities_NotesHelper.WaitForWorkAround(2000);

                executionLog.Log("ActivitiesNotesManagement", "Click Delete btn  ");
                officeActivities_NotesHelper.ClickElement("DeleteNote");

                executionLog.Log("ActivitiesNotesManagement", "Accept alert message. ");
                officeActivities_NotesHelper.AcceptAlert();

                executionLog.Log("ActivitiesNotesManagement", "Wait for delete message. ");
                officeActivities_NotesHelper.WaitForText("Note deleted successfully", 10);

                executionLog.Log("ActivitiesNotesManagement", "Redirect to recycle bin");
                VisitOffice("notes/recyclebin");
                officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesNotesManagement", "Enter Subject in Search field");
                officeActivities_NotesHelper.TypeText("EnterSubject", name);
                officeActivities_NotesHelper.WaitForWorkAround(2000);

                executionLog.Log("ActivitiesNotesManagement", "Select All in created by field");
                officeActivities_NotesHelper.SelectByText("CreatedbyField", "All");
                officeActivities_NotesHelper.WaitForWorkAround(2000);

                executionLog.Log("ActivitiesNotesManagement", "Click on delete icon.");
                officeActivities_NotesHelper.ClickElement("DeleteNoteRBin");
                //officeActivities_NotesHelper.WaitForWorkAround(2000);

                executionLog.Log("ActivitiesNotesManagement", "Accept alert message.");
                officeActivities_NotesHelper.AcceptAlert();
                //officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesNotesManagement", "Wait for success message.");
                officeActivities_NotesHelper.WaitForText("Note Permanently Deleted.", 5);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("ActivitiesNotesManagement");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Activities Notes Management");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Activities Notes Management", "Bug", "Medium", "Notes page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Activities Notes Management");
                        TakeScreenshot("ActivitiesNotesManagement");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ActivitiesNotesManagement.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("ActivitiesNotesManagement");
                        string id = loginHelper.getIssueID("Activities Notes Management");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ActivitiesNotesManagement.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Activities Notes Management"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Activities Notes Management");
            //    executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("ActivitiesNotesManagement");
                executionLog.WriteInExcel("Activities Notes Management", Status, JIRA, "Office Activities");
            }
        }
    }
}
