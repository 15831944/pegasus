using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class EditOppStateIssue : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS4")]
        [TestCategory("NewSkinTaskJira")]
        public void editOppStateIssue()
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

            // VARIABLE
            var Name = "Test" + RandomNumber(1, 999);
            var Company = "Company" + RandomNumber(1, 9999);
            String JIRA = "";
            String Status = "Pass";


            try
            {
                executionLog.Log("EditOppStateIssue", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("EditOppStateIssue", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("EditOppStateIssue", "Redirect at create opportunities page.");
                VisitOffice("opportunities/create");

                executionLog.Log("EditOppStateIssue", "Verify Page title");
                VerifyTitle("Create an Opportunity");

                executionLog.Log("EditOppStateIssue", "Enter Name");
                office_OpportunitiesHelper.TypeText("Name", Name);

                executionLog.Log("EditOppStateIssue", "Enter Company Name");
                office_OpportunitiesHelper.TypeText("CompanyName", Company);

                executionLog.Log("EditOppStateIssue", "Select Status");
                office_OpportunitiesHelper.SelectByText("State", "Closed Dead");

                executionLog.Log("EditOppStateIssue", "Select Responsibility");
                office_OpportunitiesHelper.SelectByText("Responsibility", "Howard Tang");

                executionLog.Log("EditOppStateIssue", "Select Country");
                office_OpportunitiesHelper.SelectByText("Country", "United States");

                executionLog.Log("EditOppStateIssue", "Enter zipcode");
                office_OpportunitiesHelper.TypeText("ZipCode", "60601");
                office_OpportunitiesHelper.WaitForWorkAround(3000);

                executionLog.Log("EditOppStateIssue", "Click on 'Copy address' button");
                office_OpportunitiesHelper.ClickForce("SameAsLocation");

                executionLog.Log("EditOppStateIssue", "Click on 'Save' button");
                office_OpportunitiesHelper.ClickElement("SaveOpp");

                executionLog.Log("EditOppStateIssue", "Verify opp saved successfully");
                office_OpportunitiesHelper.WaitForText("Opportunity saved successfully.", 10);

                executionLog.Log("EditOppStateIssue", "Verify Page title");
                VerifyTitle("Details");

                executionLog.Log("EditOppStateIssue", "Click on 'Edit' button");
                office_OpportunitiesHelper.ClickElement("OppEdit");

                executionLog.Log("EditOppStateIssue", "Verify Page title");
                VerifyTitle("Edit an Opportunity");

                executionLog.Log("EditOppStateIssue", "Redirect at create opportunities page.");
                VisitOffice("opportunities");

                executionLog.Log("EditOppStateIssue", "Verify Page title");
                VerifyTitle("Opportunities");
                office_OpportunitiesHelper.WaitForWorkAround(2000);

                executionLog.Log("EditOppStateIssue", "Search company name");
                office_OpportunitiesHelper.TypeText("SearchCompanyopp", Company);
                office_OpportunitiesHelper.WaitForWorkAround(2000);

                executionLog.Log("EditOppStateIssue", "Select All in responsibity tab");
                office_OpportunitiesHelper.SelectByText("Responsibiltytab", "All");
                office_OpportunitiesHelper.WaitForWorkAround(3000);

                executionLog.Log("EditOppStateIssue", "Select first opportunity");
                office_OpportunitiesHelper.ClickElement("ClickOn1stOpp");

                executionLog.Log("EditOppStateIssue", "Click on delete button.");
                office_OpportunitiesHelper.ClickElement("DeleteOpp");

                executionLog.Log("EditOppStateIssue", "Accept alert message");
                office_OpportunitiesHelper.AcceptAlert();

                executionLog.Log("EditOppStateIssue", "Wait for deleted success message.");
                office_OpportunitiesHelper.WaitForText("1 records deleted successfully", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("EditOppStateIssue");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Edit Opp State Issue");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Edit Opp State Issue", "Bug", "Medium", "Opportunity page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Edit Opp State Issue");
                        TakeScreenshot("EditOppStateIssue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditOppStateIssue.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("EditOppStateIssue");
                        string id = loginHelper.getIssueID("Edit Opp State Issue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditOppStateIssue.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Edit Opp State Issue"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Edit Opp State Issue");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("EditOppStateIssue");
                executionLog.WriteInExcel("Edit Opp State Issue", Status, JIRA, "Opportunities Management");
            }
        }
    }
}