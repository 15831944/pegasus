using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class Opportunities : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin")]
        [TestCategory("TS8")]
        [TestCategory("Pegasus_NewSkin")]
        public void opportunities()
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

                executionLog.Log("Opportunities", "Login with valid credential  Username");
                Login(username[0], password[0]);

                executionLog.Log("Opportunities", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("Opportunities", "Create Opportunities");
                VisitOffice("opportunities/create");

                executionLog.Log("Opportunities", "Click on Save");
                office_OpportunitiesHelper.ClickElement("SaveOpp");
                office_OpportunitiesHelper.WaitForWorkAround(2000);

                executionLog.Log("Opportunities", "Verify text on page.");
                office_OpportunitiesHelper.VerifyText("RequiredFieldsOpp", "This field is required.");

                executionLog.Log("Opportunities", "Enter Opportunity Name");
                office_OpportunitiesHelper.TypeText("Name", Oppname);

                executionLog.Log("Opportunities", "Enter Company DBA Name");
                office_OpportunitiesHelper.TypeText("CompanyName", CDBA);

                executionLog.Log("Opportunities", "Select Opp Status");
                office_OpportunitiesHelper.SelectByText("State", "New");

                executionLog.Log("Opportunities", "Select Opp Responsibility");
                office_OpportunitiesHelper.SelectByText("Responsibility", "Howard Tang");

                executionLog.Log("Opportunities", "Click on Save");
                office_OpportunitiesHelper.ClickElement("SaveOpp");
                office_OpportunitiesHelper.WaitForWorkAround(3000);

                var loc = "//h3[text()='Existing Opportunities']";

                if (office_OpportunitiesHelper.IsElementPresent(loc))
                {

                    executionLog.Log("Opportunities", "Click Dublicate Button");
                    office_OpportunitiesHelper.ClickOnDisplayed("ClickOnDubBtn");

                    executionLog.Log("Opportunities", "Wait for success message");
                    office_OpportunitiesHelper.WaitForText("Opportunity saved successfully.", 10);
                }

                else
                {
                    executionLog.Log("Opportunities", "Wait for success message");
                    office_OpportunitiesHelper.WaitForText("Opportunity saved successfully.", 10);

                    executionLog.Log("Opportunities", "Redirect at Opportunities Create");
                    VisitOffice("opportunities/create");

                    executionLog.Log("Opportunities", "Click on Cancel");
                    office_OpportunitiesHelper.ClickElement("CancelOpp");
                    office_OpportunitiesHelper.WaitForWorkAround(3000);

                    executionLog.Log("Opportunities", "Verify Opportunities");
                    office_OpportunitiesHelper.VerifyText("VerifyTitle", "Opportunities");
                    office_OpportunitiesHelper.WaitForWorkAround(3000);

                    executionLog.Log("Opportunities", "Redirect at Opportunities Create page.");
                    VisitOffice("opportunities/create");

                    executionLog.Log("Opportunities", "Click Save button");
                    office_OpportunitiesHelper.ClickElement("SaveOpp");
                    office_OpportunitiesHelper.WaitForWorkAround(2000);

                    executionLog.Log("Opportunities", "Verify Validation text on page.");
                    office_OpportunitiesHelper.VerifyText("RequiredFieldsOpp", "This field is required.");

                    executionLog.Log("Opportunities", "Enter Oppoutrunities Name");
                    office_OpportunitiesHelper.TypeText("Name", Oppname);

                    executionLog.Log("Opportunities", "Enter Company DBA Name");
                    office_OpportunitiesHelper.TypeText("CompanyName", CDBA);

                    executionLog.Log("Opportunities", "Opp Status");
                    office_OpportunitiesHelper.SelectByText("State", "New");

                    executionLog.Log("Opportunities", "Opp Responsibity");
                    office_OpportunitiesHelper.SelectByText("Responsibility", "Howard Tang");

                    executionLog.Log("Opportunities", "Click Save");
                    office_OpportunitiesHelper.ClickElement("SaveOpp");
                    office_OpportunitiesHelper.WaitForWorkAround(3000);

                    if (office_OpportunitiesHelper.IsElementPresent(loc))
                    {
                        executionLog.Log("Opportunities", "Click Dublicate Button");
                        office_OpportunitiesHelper.ClickOnDisplayed("ClickOnDubBtn");

                        executionLog.Log("Opportunities", "Wait for success message");
                        office_OpportunitiesHelper.WaitForText("Opportunity saved successfully.", 10);

                        executionLog.Log("Opportunities", "Goto Opportunities");
                        VisitOffice("opportunities");
                        office_OpportunitiesHelper.WaitForWorkAround(5000);

                        executionLog.Log("Opportunities", "Check First Oppo");
                        office_OpportunitiesHelper.ClickElement("ClickOn1stOpp");
                        office_OpportunitiesHelper.WaitForWorkAround(1000);

                        executionLog.Log("Opportunities", "Check 2nd Opp");
                        office_OpportunitiesHelper.ClickElement("ClickOn2ndOpp");
                        office_OpportunitiesHelper.WaitForWorkAround(1000);

                        executionLog.Log("Opportunities", "Check On Merge Records");
                        office_OpportunitiesHelper.ClickElement("ClickOnMergeRecords");
                        office_OpportunitiesHelper.WaitForWorkAround(3000);

                        executionLog.Log("Opportunities", "Select primary opportunity.");
                        office_OpportunitiesHelper.ClickElement("ClickOnCname");
                        office_OpportunitiesHelper.WaitForWorkAround(1000);

                        executionLog.Log("Opportunities", "Click Merge Button");
                        office_OpportunitiesHelper.ClickElement("ClickOnMergeBtn");
                        office_OpportunitiesHelper.AcceptAlert();

                        executionLog.Log("Opportunities", "Verify Confirmation");
                        office_OpportunitiesHelper.WaitForText("Merging Opportunity(s) Completed Successfully.", 10);

                    }
                    else
                    {

                        executionLog.Log("Opportunities", "Wait for Confirmation");
                        office_OpportunitiesHelper.WaitForText("Opportunity saved successfully.", 10);

                        executionLog.Log("Opportunities", "Redirect at Opportunities");
                        VisitOffice("opportunities");

                        executionLog.Log("Opportunities", "Click on  1st Opportunities");
                        office_OpportunitiesHelper.ClickElement("ClickOn1stOpp");

                        executionLog.Log("Opportunities", "Select 2nd Opportunity");
                        office_OpportunitiesHelper.ClickElement("ClickOn2ndOpp");

                        executionLog.Log("Opportunities", "Click On Merge Records");
                        office_OpportunitiesHelper.ClickElement("ClickOnMergeRecords");

                        executionLog.Log("Opportunities", "Select perimary oportunity");
                        office_OpportunitiesHelper.ClickElement("ClickOnCname");

                        executionLog.Log("Opportunities", "Click On Merge Button");
                        office_OpportunitiesHelper.ClickElement("ClickOnMergeBtn");
                        office_OpportunitiesHelper.AcceptAlert();

                        executionLog.Log("Opportunities", "Wait For Confirmation");
                        office_OpportunitiesHelper.WaitForText("Merging Opportunity(s) Completed Successfully.", 10);


                    }
                }
            }

            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("Opportunities");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Opportunities");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Opportunities", "Bug", "Medium", "Opportunity page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Opportunities");
                        TakeScreenshot("Opportunities");
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
                        TakeScreenshot("Opportunities");
                        string id = loginHelper.getIssueID("Opportunities");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\Opportunities.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Opportunities"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Opportunities");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("Opportunities");
                executionLog.WriteInExcel("Opportunities", Status, JIRA, "Opportunity management");
            }
        }
    }
}
