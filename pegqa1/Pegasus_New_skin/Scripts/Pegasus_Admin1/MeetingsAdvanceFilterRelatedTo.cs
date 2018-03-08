using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class MeetingsAdvanceFilterRelatedTo : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin1")]
        [TestCategory("Fail")]
        [TestCategory("TS6")]
        [TestCategory("Pegasus_Admin1")]
        public void meetingsAdvanceFilterRelatedTo()
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
            var officeActivities_MeetingHelper = new OfficeActivities_MeetingHelper(GetWebDriver());

            // Variable
            var DocName = "Test Exe" + GetRandomNumber();
            var fileUpl = GetPathToFile() + "chrome.exe";
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("MeetingsAdvanceFilteeRelatedTo", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("MeetingsAdvanceFilteeRelatedTo", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");


                //Verify meeting with notes.

                executionLog.Log("MeetingsAdvanceFilteeRelatedTo", "Redirect To URL");
                VisitOffice("meetings");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("MeetingsAdvanceFilteeRelatedTo", "Verify page title.");
                VerifyTitle("Meetings");

                executionLog.Log("MeetingsAdvanceFilteeRelatedTo", "Click on advance filter.");
                officeActivities_MeetingHelper.ClickForce("AdvanceFilter");
                officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("MeetingsAdvanceFilteeRelatedTo", "Selct client with activity type.");
                officeActivities_MeetingHelper.ClickForce("MeetingWithNotes");

                executionLog.Log("MeetingsAdvanceFilteeRelatedTo", "Click on apply button.");
                officeActivities_MeetingHelper.ClickForce("Apply");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("MeetingsAdvanceFilteeRelatedTo", "Click on any meeting.");
                officeActivities_MeetingHelper.ClickForce("ClickOnAnyMeeting");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("MeetingsAdvanceFilteeRelatedTo", "Verify notes present for meeting.");
                officeActivities_MeetingHelper.IsElementPresent("//table[@class='table table-bordered']/tbody/tr[1]/td[6]/a");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                //Verify meeting with clients.

                executionLog.Log("MeetingsAdvanceFilteeRelatedTo", "Redirect To URL");
                VisitOffice("meetings");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("MeetingsAdvanceFilteeRelatedTo", "Verify page title.");
                VerifyTitle("Meetings");

                executionLog.Log("MeetingsAdvanceFilteeRelatedTo", "Click on advance filter.");
                officeActivities_MeetingHelper.ClickForce("AdvanceFilter");
                officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("MeetingsAdvanceFilteeRelatedTo", "Selct meeting related to clients");
                officeActivities_MeetingHelper.ClickForce("MeetingWithClients");
                //officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("MeetingsAdvanceFilteeRelatedTo", "Click on apply button.");
                officeActivities_MeetingHelper.ClickForce("Apply");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("MeetingsAdvanceFilteeRelatedTo", "Verify meeting present is related to clients.");
                officeActivities_MeetingHelper.VerifyText("MeetingClient", "Merchants");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                //Verify meeting with Leads.

                executionLog.Log("MeetingsAdvanceFilteeRelatedTo", "Redirect To URL");
                VisitOffice("meetings");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("MeetingsAdvanceFilteeRelatedTo", "Verify page title.");
                VerifyTitle("Meetings");

                executionLog.Log("MeetingsAdvanceFilteeRelatedTo", "Click on advance filter.");
                officeActivities_MeetingHelper.ClickForce("AdvanceFilter");
                officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("MeetingsAdvanceFilteeRelatedTo", "click on meeting with activity type.");
                officeActivities_MeetingHelper.ClickForce("MeetingWithLeads");
                //officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("MeetingsAdvanceFilteeRelatedTo", "Click on apply button.");
                officeActivities_MeetingHelper.ClickForce("Apply");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("MeetingsAdvanceFilteeRelatedTo", "Verify meeting present is related to leads.");
                officeActivities_MeetingHelper.VerifyText("MeetingClient", "Leads");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                // Verify meeting with Opportunities .

                executionLog.Log("MeetingsAdvanceFilteeRelatedTo", "Redirect To URL");
                VisitOffice("meetings");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("MeetingsAdvanceFilteeRelatedTo", "Verify page title.");
                VerifyTitle("Meetings");

                executionLog.Log("MeetingsAdvanceFilteeRelatedTo", "Click on advance filter.");
                officeActivities_MeetingHelper.ClickForce("AdvanceFilter");
                officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("MeetingsAdvanceFilteeRelatedTo", "click on meeting  with opportunities.");
                officeActivities_MeetingHelper.ClickForce("MeetingWithOpps");
                //officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("MeetingsAdvanceFilteeRelatedTo", "Click on apply button.");
                officeActivities_MeetingHelper.ClickForce("Apply");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("MeetingsAdvanceFilteeRelatedTo", "Verify meeting present is related to opportunities.");
                officeActivities_MeetingHelper.VerifyText("MeetingClient", "Opportunities");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                // Verify meeting with Attachments .

                executionLog.Log("MeetingsAdvanceFilteeRelatedTo", "Redirect To URL");
                VisitOffice("meetings");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("MeetingsAdvanceFilteeRelatedTo", "Verify page title.");
                VerifyTitle("Meetings");

                executionLog.Log("MeetingsAdvanceFilteeRelatedTo", "Click on advance filter.");
                officeActivities_MeetingHelper.ClickForce("AdvanceFilter");
                officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("MeetingsAdvanceFilteeRelatedTo", "Selct meeting with activity type.");
                officeActivities_MeetingHelper.ClickForce("MeetingWithAttach.");
                //officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("MeetingsAdvanceFilteeRelatedTo", "Click on apply button.");
                officeActivities_MeetingHelper.ClickForce("Apply");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("MeetingsAdvanceFilteeRelatedTo", "Click on any meeting.");
                officeActivities_MeetingHelper.ClickForce("ClickOnAnyMeeting");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("MeetingsAdvanceFilteeRelatedTo", "Verify meeting contains documents.");
                officeActivities_MeetingHelper.IsElementPresent("//table[@class='table']/tbody/tr[1]/td[1]");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);
            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("MeetingsAdvanceFilteeRelatedTo");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Meetings Advance Filter RelatedTo");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Meetings Advance Filter RelatedTo", "Bug", "Medium", "Opportunities page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Meetings Advance Filter RelatedTo");
                        TakeScreenshot("MeetingsAdvanceFilteeRelatedTo");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\MeetingsAdvanceFilteeRelatedTo.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("MeetingsAdvanceFilteeRelatedTo");
                        string id = loginHelper.getIssueID("Meetings Advance Filter RelatedTo");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\MeetingsAdvanceFilteeRelatedTo.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Meetings Advance Filter RelatedTo"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Meetings Advance Filter RelatedTo");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("MeetingsAdvanceFilteeRelatedTo");
                executionLog.WriteInExcel("Meetings Advance Filter RelatedTo", Status, JIRA, "Opportunities Management");
            }
        }
    }
}
