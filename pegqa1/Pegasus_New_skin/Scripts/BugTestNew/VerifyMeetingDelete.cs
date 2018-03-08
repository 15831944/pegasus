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
    public class VerifyMeetingDelete : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS2")]
        [TestCategory("BugTestNew")]
        public void verifyMeetingDelete()
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
           // var email = "Test" + GetRandomNumber() + "@gmail.com";
            String JIRA = "";
            String Status = "Pass";

            //try
            //{
                executionLog.Log("VerifyMeetingDelete", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyMeetingDelete", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("VerifyMeetingDelete", " Go to create meeting page.");
                VisitOffice("meetings/create");
                officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyMeetingDelete", "Verify title");
                VerifyTitle("Create a Meeting");

                executionLog.Log("VerifyMeetingDelete", "Enter Subject");
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
                officeActivities_MeetingHelper.ClickElement("SelectClient");
                officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("EditMeetingValiationForAlphabet", "Enter start date");
                officeActivities_MeetingHelper.TypeText("StartDate", "08/08/2018");
                //officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("EditMeetingValiationForAlphabet", "Enter an alphabet in end date");
                officeActivities_MeetingHelper.TypeText("EndDate", "09/09/2018");
                //officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("EditMeetingValiationForAlphabet", "Click on Save");
                officeActivities_MeetingHelper.ClickElement("Save");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("EditMeetingValiationForAlphabet", "Search created meeting by Subject");
                officeActivities_MeetingHelper.TypeText("SearchSubject", name);
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("EditMeetingValiationForAlphabet", "Select check box of created meeting");
                officeActivities_MeetingHelper.ClickElement("SelectCheckbox");

                executionLog.Log("EditMeetingValiationForAlphabet", "Click on Delete button");
                officeActivities_MeetingHelper.ClickElement("DeleteBtn");
                officeActivities_MeetingHelper.AcceptAlert();

                executionLog.Log("EditMeetingValiationForAlphabet", "Wait for successfully meeting delete");
                officeActivities_MeetingHelper.WaitForText("Meeting deleted successfully.", 5);
                Console.WriteLine("Meeting deleted successfully.");


            //}
            //catch (Exception e)
            //{
                
            //}
           
        }
    }
}