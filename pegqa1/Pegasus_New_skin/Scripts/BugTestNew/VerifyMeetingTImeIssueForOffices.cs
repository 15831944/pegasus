using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class VerifyMeetingTImeIssueForOffices : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin")]
        public void verifyMeetingTImeIssueForOffices()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username2");
            password = oXMLData.getData("settings/Credentials", "password2");


            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var corpOffice_OfficeHelper = new CorpOffice_OfficeHelper(GetWebDriver());

            //Variable random
            var usernme = "TESTUSER" + RandomNumber(676, 99999);
            var name = "Meeting" + RandomNumber(99, 999);
            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("VerifyMeetingTImeIssueForOffices", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyMeetingTImeIssueForOffices", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyMeetingTImeIssueForOffices", "Redirect at office page.");
                VisitCorp("offices");

                executionLog.Log("VerifyMeetingTImeIssueForOffices", "Verify Page title.");
                VerifyTitle("Offices");

                executionLog.Log("VerifyMeetingTImeIssueForOffices", "Click on any office..");
                corpOffice_OfficeHelper.ClickElement("ClickOnAnyCorp");

                executionLog.Log("VerifyMeetingTImeIssueForOffices", "Click on add meeting.");
                corpOffice_OfficeHelper.ClickElement("NewMeeting");

                executionLog.Log("VerifyMeetingTImeIssueForOffices", "Enter meeting subject.");
                corpOffice_OfficeHelper.TypeText("MeetingSubject", name);

                executionLog.Log("VerifyMeetingTImeIssueForOffices", "Enter start date.");
                corpOffice_OfficeHelper.TypeText("StartDate", "2015-03-25");
                corpOffice_OfficeHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyMeetingTImeIssueForOffices", "Enter same due date as start date..");
                corpOffice_OfficeHelper.TypeText("DueDate", "2015-03-25");
                corpOffice_OfficeHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyMeetingTImeIssueForOffices", "Select start time");
                corpOffice_OfficeHelper.Select("StartHour", "02");
                corpOffice_OfficeHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyMeetingTImeIssueForOffices", "Select start minute");
                corpOffice_OfficeHelper.Select("StartMinute", "00");
                corpOffice_OfficeHelper.WaitForWorkAround(3000);


                executionLog.Log("VerifyMeetingTImeIssueForOffices", "Select due hour");
                corpOffice_OfficeHelper.Select("DueHour", "01");

                executionLog.Log("VerifyMeetingTImeIssueForOffices", "Click on save meeting.");
                corpOffice_OfficeHelper.ClickElement("SaveMeeting");
                corpOffice_OfficeHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyMeetingTImeIssueForOffices", "Verify alert text for lesser start date and time.");
                corpOffice_OfficeHelper.VerifyAlertText("Start Date & Time should lesser than or equal to End Date & Time.");

                executionLog.Log("VerifyMeetingTImeIssueForOffices", "Click ok to accept alert.");
                corpOffice_OfficeHelper.AcceptAlert();

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyMeetingTImeIssueForOffices");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Meeting TIme Issue For Offices");
                if (!result)
                {
                    if (Int16.Parse(counter) < 5)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Meeting TIme Issue For Offices", "Bug", "Medium", "Corp office page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Meeting TIme Issue For Offices");
                        TakeScreenshot("VerifyMeetingTImeIssueForOffices");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyMeetingTImeIssueForOffices.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 5)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyMeetingTImeIssueForOffices");
                        string id = loginHelper.getIssueID("Verify Meeting TIme Issue For Offices");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyMeetingTImeIssueForOffices.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Meeting TIme Issue For Offices"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Meeting TIme Issue For Offices");
                executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyMeetingTImeIssueForOffices");
                executionLog.WriteInExcel("Verify Meeting TIme Issue For Offices", Status, JIRA, "Corp Office");
            }
        }
    }
}