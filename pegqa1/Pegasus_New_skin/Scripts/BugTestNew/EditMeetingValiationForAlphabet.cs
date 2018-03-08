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
    public class EditMeetingValiationForAlphabet : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS2")]
        [TestCategory("BugTestNew")]
        public void editMeetingValiationForAlphabet()
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
            var officeActivities_MeetingHelper = new OfficeActivities_MeetingHelper(GetWebDriver());

            // Variable
            var name = "Subject" + RandomNumber(1, 9999);
            var email = "Test" + GetRandomNumber() + "@gmail.com";
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("EditMeetingValiationForAlphabet", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("EditMeetingValiationForAlphabet", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("EditMeetingValiationForAlphabet", " Go to create meeting page.");
                VisitOffice("meetings/create");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("EditMeetingValiationForAlphabet", "Verify title");
                VerifyTitle("Create a Meeting");

                executionLog.Log("EditMeetingValiationForAlphabet", "Enter Subject");
                officeActivities_MeetingHelper.TypeText("Subject", name);

                executionLog.Log("EditMeetingValiationForAlphabet", "Enter Meeting location");
                officeActivities_MeetingHelper.TypeText("Location", "Test Meeting");

                executionLog.Log("EditMeetingValiationForAlphabet", "Select Priority");
                officeActivities_MeetingHelper.Select("Priority", "Low");

                executionLog.Log("EditMeetingValiationForAlphabet", "select Module");
                officeActivities_MeetingHelper.SelectByText("RelatedTo", "Client");

                executionLog.Log("EditMeetingValiationForAlphabet", "Click on find list icon");
                officeActivities_MeetingHelper.ClickElement("FindListIcon");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("EditMeetingValiationForAlphabet", "Select a client");
                officeActivities_MeetingHelper.ClickElement("SelectedClient");
                officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("EditMeetingValiationForAlphabet", "Enter start date");
                officeActivities_MeetingHelper.TypeText("StartDate", "08/08/2018");
                //officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("EditMeetingValiationForAlphabet", "Enter an alphabet in end date");
                officeActivities_MeetingHelper.TypeText("EndDate", "07/07/2018");
                //officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("EditMeetingValiationForAlphabet", "Click on Save  ");
                officeActivities_MeetingHelper.ClickElement("Save");
                officeActivities_MeetingHelper.WaitForWorkAround(1000);

                executionLog.Log("EditMeetingValiationForAlphabet", "Verify alert message for valid date.");
                //GetWebDriver().SwitchTo().Alert();
               string poptxt = GetWebDriver().SwitchTo().Alert().Text;
               Assert.AreEqual("Start Date & Time should lesser than or equal to Due Date & Time.",poptxt);

                //executionLog.Log("EditMeetingValiationForAlphabet", "Verify the field validation");
                //officeActivities_MeetingHelper.VerifyPageText("Enter a valid end date");

                executionLog.Log("EditMeetingValiationForAlphabet", "Verify 500 error not occured.");
                officeActivities_MeetingHelper.VerifyTextNotPresent("500 Internal server error.");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("EditMeetingValiationForAlphabet");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Edit Meeting Valiation For Alphabet");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Edit Meeting Valiation For Alphabet", "Bug", "Medium", "Meeting page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Edit Meeting Valiation For Alphabet");
                        TakeScreenshot("EditMeetingValiationForAlphabet");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditMeetingValiationForAlphabet.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("EditMeetingValiationForAlphabet");
                        string id = loginHelper.getIssueID("Edit Meeting Valiation For Alphabet");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditMeetingValiationForAlphabet.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Edit Meeting Valiation For Alphabet"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Edit Meeting Valiation For Alphabet");
                //   executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("EditMeetingValiationForAlphabet");
                executionLog.WriteInExcel("Edit Meeting Valiation For Alphabet", Status, JIRA, "Office Activities");
            }
        }
    }
}