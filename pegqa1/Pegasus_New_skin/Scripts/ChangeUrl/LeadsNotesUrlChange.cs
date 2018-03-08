using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class LeadsNotesUrlChange : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Url")]
        [TestCategory("TS4")]
        [TestCategory("ChangeUrl")]
        public void leadsNotesUrlChange()
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
            var office_LeadsHelper = new Office_LeadsHelper(GetWebDriver());
            var officeActivities_NotesHelper = new OfficeActivities_NotesHelper(GetWebDriver());

            // Variable
            var Subject = "Subject" + RandomNumber(1, 999);
            var FirstName = "LeadQA" + RandomNumber(1, 50);

            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("LeadsNotesUrlChange", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("LeadsNotesUrlChange", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("LeadsNotesUrlChange", "Go to All Leads");
                VisitOffice("leads");
                office_LeadsHelper.WaitForWorkAround(3000);

                //office_LeadsHelper.TypeText("SearchLName", "Aslam Lead");
                //office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("LeadsNotesUrlChange", "Click On Advanced Filter");
                office_LeadsHelper.ClickElement("AdvanceFilter");
                office_LeadsHelper.WaitForWorkAround(2000);

                executionLog.Log("LeadsNotesUrlChange", "Click On Show Activities");
                office_LeadsHelper.ClickElement("ShowActivities");
                office_LeadsHelper.WaitForWorkAround(2000);

                executionLog.Log("LeadsNotesUrlChange", "Click On Client with notes");
                office_LeadsHelper.ClickElement("LeadWithNote");
                //office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("LeadsNotesUrlChange", "Click On Apply Filter");
                office_LeadsHelper.ClickElement("Apply");
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("LeadsNotesUrlChange", "Click On Any Lead");
                office_LeadsHelper.ClickElement("ClickAnyLead");
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("LeadsNotesUrlChange", "Select Activity >> Notes");
                office_LeadsHelper.Select("SelectActivityType", "Notes");
                officeActivities_NotesHelper.WaitForWorkAround(2000);

                executionLog.Log("LeadsNotesUrlChange", "select all in created by field");
                officeActivities_NotesHelper.SelectByText("CreatedbyField", "All");
                officeActivities_NotesHelper.WaitForWorkAround(2000);

                executionLog.Log("LeadsNotesUrlChange", "Click On Notes");
                office_LeadsHelper.PressEnter("ClickNotes1");
                officeActivities_NotesHelper.WaitForWorkAround(2000);

                executionLog.Log("LeadsNotesUrlChange", "Change the url with the url number of another office");
                VisitOffice("notes/view/765589");

                executionLog.Log("LeadsNotesUrlChange", "Verify Validation");
                officeActivities_NotesHelper.WaitForText("You don't have privileges to view this note.", 10);
                //officeActivities_NotesHelper.WaitForWorkAround(2000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("LeadsNotesUrlChange");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Leads Notes Url Change");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Leads Notes Url Change", "Bug", "Medium", "Leads page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Leads Notes Url Change");
                        TakeScreenshot("LeadsNotesUrlChange");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\LeadsNotesUrlChange.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("LeadsNotesUrlChange");
                        string id = loginHelper.getIssueID("Leads Notes Url Change");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\LeadsNotesUrlChange.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Leads Notes Url Change"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Leads Notes Url Change");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("LeadsNotesUrlChange");
                executionLog.WriteInExcel("Leads Notes Url Change", Status, JIRA, "Leads Management");
            }
        }
    }
}
