using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class VerifyOpportunitiesCreatedModifiedByCredits : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin1")]
        [TestCategory("TS7")]
        [TestCategory("Pegasus_Admin1")]
        public void verifyOpportunitiesCreatedModifiedByCredits()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var office_OpportunitiesHelper = new Office_OpportunitiesHelper(GetWebDriver());
            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Variable
            var Oppname = "Test" + RandomNumber(99, 99999);
            var CDBA = "New" + RandomNumber(99, 99999);
            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("VerifyOpportunitiesCreatedModifiedByCredits", "Login with valid credential  Username");
                Login(username[0], password[0]);

                executionLog.Log("VerifyOpportunitiesCreatedModifiedByCredits", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyOpportunitiesCreatedModifiedByCredits", "Create Opportunities");
                VisitOffice("opportunities/create");

                executionLog.Log("VerifyOpportunitiesCreatedModifiedByCredits", "Click on Save");
                office_OpportunitiesHelper.ClickElement("SaveOpp");
                office_OpportunitiesHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyOpportunitiesCreatedModifiedByCredits", "Verify text on page.");
                office_OpportunitiesHelper.VerifyText("RequiredFieldsOpp", "This field is required.");

                executionLog.Log("VerifyOpportunitiesCreatedModifiedByCredits", "Enter Opportunity Name");
                office_OpportunitiesHelper.TypeText("Name", Oppname);

                executionLog.Log("VerifyOpportunitiesCreatedModifiedByCredits", "Enter Company DBA Name");
                office_OpportunitiesHelper.TypeText("CompanyName", CDBA);

                executionLog.Log("VerifyOpportunitiesCreatedModifiedByCredits", "Select Opp Status");
                office_OpportunitiesHelper.SelectByText("State", "New");

                executionLog.Log("VerifyOpportunitiesCreatedModifiedByCredits", "Select Opp Responsibility");
                office_OpportunitiesHelper.SelectByText("Responsibility", "Howard Tang");

                executionLog.Log("VerifyOpportunitiesCreatedModifiedByCredits", "Click on Save");
                office_OpportunitiesHelper.ClickElement("SaveOpp");
                office_OpportunitiesHelper.WaitForWorkAround(3000);

                var loc = "//h3[text()='Existing Opportunities']";
                if (office_OpportunitiesHelper.IsElementPresent(loc))
                {

                    executionLog.Log("VerifyOpportunitiesCreatedModifiedByCredits", "Click Dublicate Button");
                    office_OpportunitiesHelper.ClickOnDisplayed("ClickOnDubBtn");

                    executionLog.Log("VerifyOpportunitiesCreatedModifiedByCredits", "Wait for success message");
                    office_OpportunitiesHelper.WaitForText("Opportunity saved successfully.", 10);

                    executionLog.Log("VerifyOpportunitiesCreatedModifiedByCredits", "Verify opportunity created by");
                    office_OpportunitiesHelper.VerifyText("CreatedBy", "Howard Tang");

                    executionLog.Log("VerifyOpportunitiesCreatedModifiedByCredits", "Verify opportunity modified by");
                    office_OpportunitiesHelper.VerifyText("ModifiedBy", "Howard Tang");

                    executionLog.Log("VerifyOpportunitiesCreatedModifiedByCredits", "Click on edit button.");
                    office_OpportunitiesHelper.ClickElement("EditLink");

                    executionLog.Log("VerifyOpportunitiesCreatedModifiedByCredits", "Click on save button.");
                    office_OpportunitiesHelper.ClickElement("SaveOpp");

                    executionLog.Log("VerifyOpportunitiesCreatedModifiedByCredits", "Verify opportunity created by");
                    office_OpportunitiesHelper.VerifyText("CreatedBy", "Howard Tang");

                    executionLog.Log("VerifyOpportunitiesCreatedModifiedByCredits", "Verify opportunity modified by");
                    office_OpportunitiesHelper.VerifyText("ModifiedBy", "Howard Tang");

                    executionLog.Log("VerifyOpportunitiesCreatedModifiedByCredits", "Redirect at Opportunities");
                    VisitOffice("VerifyOpportunitiesCreatedModifiedByCredits");

                    executionLog.Log("VerifyOpportunitiesCreatedModifiedByCredits", "Click on  1st Opportunities");
                    office_OpportunitiesHelper.ClickElement("ClickOn1stOpp");

                    executionLog.Log("VerifyOpportunitiesCreatedModifiedByCredits", "Click On Merge Records");
                    office_OpportunitiesHelper.ClickElement("DeleteLink");
                    office_OpportunitiesHelper.AcceptAlert();

                    executionLog.Log("VerifyOpportunitiesCreatedModifiedByCredits", "Wait for confirmation");
                    office_OpportunitiesHelper.WaitForText("1 records deleted successfully", 10);

                    executionLog.Log("VerifyOpportunitiesCreatedModifiedByCredits", "Redirect at recyclebin Page.");
                    VisitOffice("opportunities/recyclebin");

                    executionLog.Log("VerifyOpportunitiesCreatedModifiedByCredits", "Verify page title.");
                    VerifyTitle("Recycled Opportunities");

                    executionLog.Log("VerifyOpportunitiesCreatedModifiedByCredits", "Click on delete icon.");
                    office_OpportunitiesHelper.ClickElement("DeletePermanaently");
                    office_OpportunitiesHelper.AcceptAlert();

                    executionLog.Log("VerifyOpportunitiesCreatedModifiedByCredits", "Wait For Confirmation");
                    office_OpportunitiesHelper.WaitForText("Opportunity permanently deleted.", 10);

                }
                else
                {
                    executionLog.Log("VerifyOpportunitiesCreatedModifiedByCredits", "Wait for success message");
                    office_OpportunitiesHelper.WaitForText("Opportunity saved successfully.", 10);

                    executionLog.Log("VerifyOpportunitiesCreatedModifiedByCredits", "Verify opportunity created by");
                    office_OpportunitiesHelper.VerifyText("CreatedBy", "Howard Tang");

                    executionLog.Log("VerifyOpportunitiesCreatedModifiedByCredits", "Verify opportunity modified by");
                    office_OpportunitiesHelper.VerifyText("ModifiedBy", "Howard Tang");

                    executionLog.Log("VerifyOpportunitiesCreatedModifiedByCredits", "Click on edit button.");
                    office_OpportunitiesHelper.ClickElement("EditLink");

                    executionLog.Log("VerifyOpportunitiesCreatedModifiedByCredits", "Click on save button.");
                    office_OpportunitiesHelper.ClickElement("SaveOpp");

                    executionLog.Log("VerifyOpportunitiesCreatedModifiedByCredits", "Verify opportunity created by");
                    office_OpportunitiesHelper.VerifyText("CreatedBy", "Howard Tang");

                    executionLog.Log("VerifyOpportunitiesCreatedModifiedByCredits", "Verify opportunity modified by");
                    office_OpportunitiesHelper.VerifyText("ModifiedBy", "Howard Tang");

                    executionLog.Log("VerifyOpportunitiesCreatedModifiedByCredits", "Redirect at Opportunities");
                    VisitOffice("opportunities");

                    executionLog.Log("VerifyOpportunitiesCreatedModifiedByCredits", "Click on  1st Opportunities");
                    office_OpportunitiesHelper.ClickElement("ClickOn1stOpp");

                    executionLog.Log("VerifyOpportunitiesCreatedModifiedByCredits", "Click On Merge Records");
                    office_OpportunitiesHelper.ClickElement("DeleteLink");
                    office_OpportunitiesHelper.AcceptAlert();

                    executionLog.Log("VerifyOpportunitiesCreatedModifiedByCredits", "Wait for confirmation");
                    office_OpportunitiesHelper.WaitForText("1 records deleted successfully", 10);

                    executionLog.Log("VerifyOpportunitiesCreatedModifiedByCredits", "Redirect at recyclebin Page.");
                    VisitOffice("opportunities/recyclebin");

                    executionLog.Log("VerifyOpportunitiesCreatedModifiedByCredits", "Verify page title.");
                    VerifyTitle("Recycled Opportunities");

                    executionLog.Log("VerifyOpportunitiesCreatedModifiedByCredits", "Click on delete icon.");
                    office_OpportunitiesHelper.ClickElement("DeletePermanaently");
                    office_OpportunitiesHelper.AcceptAlert();

                    executionLog.Log("VerifyOpportunitiesCreatedModifiedByCredits", "Wait For Confirmation");
                    office_OpportunitiesHelper.WaitForText("Opportunity permanently deleted.", 10);

                }
            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyOpportunitiesCreatedModifiedByCredits");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("VerifyOpportunitiesCreatedModifiedByCredits");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("VerifyOpportunitiesCreatedModifiedByCredits", "Bug", "Medium", "Opportunity page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("VerifyOpportunitiesCreatedModifiedByCredits");
                        TakeScreenshot("VerifyOpportunitiesCreatedModifiedByCredits");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\Opportunities.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyOpportunitiesCreatedModifiedByCredits");
                        string id = loginHelper.getIssueID("VerifyOpportunitiesCreatedModifiedByCredits");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\Opportunities.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("VerifyOpportunitiesCreatedModifiedByCredits"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("VerifyOpportunitiesCreatedModifiedByCredits");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyOpportunitiesCreatedModifiedByCredits");
                executionLog.WriteInExcel("VerifyOpportunitiesCreatedModifiedByCredits", Status, JIRA, "Opportunity management");
            }
        }
    }
}