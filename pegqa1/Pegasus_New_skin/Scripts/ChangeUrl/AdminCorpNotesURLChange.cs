using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class AdminCorpNotesURLChange : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Url")]
        [TestCategory("TS3")]
        [TestCategory("ChangeUrl")]
        public void adminCorpNotesURLChange()
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
            var FirstName = "Test" + GetRandomNumber();
            var LastName = "Tester" + GetRandomNumber();
            var Number = "12345678" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("AdminCorpNotesURLChange", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("AdminCorpNotesURLChange", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("AdminCorpNotesURLChange", "Goto User ");
                VisitOffice("mycorp");
                officeActivities_NotesHelper.WaitForWorkAround(1000);

                executionLog.Log("AdminCorpNotesURLChange", "Select Activity >> Notes");
                officeActivities_NotesHelper.Select("SelectActivityType", "Notes");
                officeActivities_NotesHelper.WaitForWorkAround(2000);

                executionLog.Log("AdminCorpNotesURLChange", "Click On Notes ");
                officeActivities_NotesHelper.ClickElement("OpenNotes");
                officeActivities_NotesHelper.WaitForWorkAround(2000);

                executionLog.Log("AdminCorpNotesURLChange", "Change the url with the url number of another office");
                VisitOffice("viewactivity/note/16");
                officeActivities_NotesHelper.WaitForWorkAround(1000);

                executionLog.Log("AdminCorpNotesURLChange", "Verify Validation");
                officeActivities_NotesHelper.WaitForText("You don't have privileges to view this office activity.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("AdminCorpNotesURLChange");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Admin Corp Notes URL Change");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Admin Corp Notes URL Change", "Bug", "Medium", "Corporate page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Admin Corp Notes URL Change");
                        TakeScreenshot("AdminCorpNotesURLChange");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AdminCorpNotesURLChange.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("AdminCorpNotesURLChange");
                        string id = loginHelper.getIssueID("Admin Corp Notes URL Change");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AdminCorpNotesURLChange.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Admin Corp Notes URL Change"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Admin Corp Notes URL Change");
               // executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("AdminCorpNotesURLChange");
                executionLog.WriteInExcel("Admin Corp Notes URL Change", Status, JIRA, "My Corp");
            }
        }
    }
}
