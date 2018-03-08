using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class VerifyQuickLookLabelsForOpportunity : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin1")]
        [TestCategory("TS7")]
        [TestCategory("Pegasus_Admin1")]
        public void verifyQuickLookLabelsForOpportunity()
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
                executionLog.Log("VerifyQuickLookLabelsForOpportunity", "Login with valid credential  Username");
                Login(username[0], password[0]);

                executionLog.Log("VerifyQuickLookLabelsForOpportunity", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyQuickLookLabelsForOpportunity", "Create Opportunities");
                VisitOffice("opportunities/create");
                office_OpportunitiesHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuickLookLabelsForOpportunity", "Click on Save");
                office_OpportunitiesHelper.ClickElement("SaveOpp");
                office_OpportunitiesHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuickLookLabelsForOpportunity", "Verify text on page.");
                office_OpportunitiesHelper.VerifyText("RequiredFieldsOpp", "This field is required.");

                executionLog.Log("VerifyQuickLookLabelsForOpportunity", "Enter Opportunity Name");
                office_OpportunitiesHelper.TypeText("Name", Oppname);

                executionLog.Log("VerifyQuickLookLabelsForOpportunity", "Enter Company DBA Name");
                office_OpportunitiesHelper.TypeText("CompanyName", CDBA);

                executionLog.Log("VerifyQuickLookLabelsForOpportunity", "Select Opp Status");
                office_OpportunitiesHelper.SelectByText("State", "New");

                executionLog.Log("VerifyQuickLookLabelsForOpportunity", "Select Opp Responsibility");
                office_OpportunitiesHelper.SelectByText("Responsibility", "Howard Tang");

                executionLog.Log("VerifyQuickLookLabelsForOpportunity", "Click on Save button.");
                office_OpportunitiesHelper.ClickElement("SaveOpp");
                office_OpportunitiesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyQuickLookLabelsForOpportunity", "Verify label for client type.");
                office_OpportunitiesHelper.VerifyText("ClientType", "Select");

                executionLog.Log("VerifyQuickLookLabelsForOpportunity", "Verify label for status.");
                office_OpportunitiesHelper.VerifyText("Status", "New");

                executionLog.Log("VerifyQuickLookLabelsForOpportunity", "Verify label for source.");
                office_OpportunitiesHelper.VerifyText("Source", "Select Source");

                executionLog.Log("VerifyQuickLookLabelsForOpportunity", "Verify label for category.");
                office_OpportunitiesHelper.VerifyText("Category", "Select Category");

                executionLog.Log("VerifyQuickLookLabelsForOpportunity", "Verify label for responsibility.");
                office_OpportunitiesHelper.VerifyText("Responsibilityl", "Howard Tang");

                executionLog.Log("VerifyQuickLookLabelsForOpportunity", "Verify label for account manager.");
                office_OpportunitiesHelper.VerifyText("AccountManager", "Select Account Manager");

                executionLog.Log("VerifyQuickLookLabelsForOpportunity", "Verify label for partner agent.");
                office_OpportunitiesHelper.VerifyText("PartnerAgentl", "Select Partner Agent");

                executionLog.Log("VerifyQuickLookLabelsForOpportunity", "Verify label for partner association.");
                office_OpportunitiesHelper.VerifyText("PartnerAssociationl", "Select Partner Association");

                executionLog.Log("VerifyQuickLookLabelsForOpportunity", "Verify label for sales manager.");
                office_OpportunitiesHelper.VerifyText("SalesManager", "Select Sales Manager");

                executionLog.Log("VerifyQuickLookLabelsForOpportunity", "Click on edit opportunity.");
                office_OpportunitiesHelper.ClickElement("EditLink");
                office_OpportunitiesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyQuickLookLabelsForOpportunity", "Select client type.");
                office_OpportunitiesHelper.Select("Clientt", "Processing");
                //office_OpportunitiesHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyQuickLookLabelsForOpportunity", "Select refferal source.");
                office_OpportunitiesHelper.Select("RSource", "Campaign");
                //office_OpportunitiesHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyQuickLookLabelsForOpportunity", "Select category.");
                office_OpportunitiesHelper.SelectByText("Categ", "Single");
                //office_OpportunitiesHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyQuickLookLabelsForOpportunity", "Select account manager.");
                office_OpportunitiesHelper.SelectByText("AccntManager", "Howard Tang");
                //office_OpportunitiesHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyQuickLookLabelsForOpportunity", "Select user group.");
                office_OpportunitiesHelper.SelectByText("UGroup", "Primary Group");
                //office_OpportunitiesHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyQuickLookLabelsForOpportunity", "Select sales manager.");
                office_OpportunitiesHelper.SelectByText("SaleMgr", "Howard Tang");
                //office_OpportunitiesHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyQuickLookLabelsForOpportunity", "Select responsibility.");
                office_OpportunitiesHelper.SelectByText("Responsibility", "Howard Tang");
                //office_OpportunitiesHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyQuickLookLabelsForOpportunity", "Select partner agent.");
                office_OpportunitiesHelper.SelectByText("PartnerAgent", "Mark Menu");
                //office_OpportunitiesHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyQuickLookLabelsForOpportunity", "Select partner association.");
                office_OpportunitiesHelper.SelectByText("PartAssociation", "Aslam Associate");
                //office_OpportunitiesHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyQuickLookLabelsForOpportunity", "Click on Save button.");
                office_OpportunitiesHelper.ClickElement("ClickSaveClient");
                office_OpportunitiesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyQuickLookLabelsForOpportunity", "Verify client type as processing.");
                office_OpportunitiesHelper.VerifyText("ClientType", "Processing");

                executionLog.Log("VerifyQuickLookLabelsForOpportunity", "Verify status as New");
                office_OpportunitiesHelper.VerifyText("Status", "New");

                executionLog.Log("VerifyQuickLookLabelsForOpportunity", "Verify source as campaign");
                office_OpportunitiesHelper.VerifyText("Source", "Campaign");

                executionLog.Log("VerifyQuickLookLabelsForOpportunity", "Verify category as Test1");
                office_OpportunitiesHelper.VerifyText("Category", "Single");

                executionLog.Log("VerifyQuickLookLabelsForOpportunity", "Verify responsibility as Howard.");
                office_OpportunitiesHelper.VerifyText("Responsibilityl", "Howard Tang");

                executionLog.Log("VerifyQuickLookLabelsForOpportunity", "Verify account manager as Howard.");
                office_OpportunitiesHelper.VerifyText("AccountManager", "Howard Tang");

                executionLog.Log("VerifyQuickLookLabelsForOpportunity", "Verify partner agent as mark matthews.");
                office_OpportunitiesHelper.VerifyText("PartnerAgentl", "Mark Menu");

                executionLog.Log("VerifyQuickLookLabelsForOpportunity", "Verify partner association. ");
                office_OpportunitiesHelper.VerifyText("PartnerAssociationl", "Aslam Associate");

                executionLog.Log("VerifyQuickLookLabelsForOpportunity", "Verify sales manager as howard.");
                office_OpportunitiesHelper.VerifyText("SalesManager", "Howard Tang");

                executionLog.Log("VerifyQuickLookLabelsForOpportunity", "Click on delete.");
                office_OpportunitiesHelper.ClickElement("DeleteLink");

                executionLog.Log("VerifyQuickLookLabelsForOpportunity", "Click on Save");
                office_OpportunitiesHelper.AcceptAlert();

                executionLog.Log("VerifyQuickLookLabelsForOpportunity", "Wait for success text.");
                office_OpportunitiesHelper.WaitForText("Opportunity deleted successfully.", 10);

                executionLog.Log("VerifyQuickLookLabelsForOpportunity", "Redirect at recyclebin page.");
                VisitOffice("opportunities/recyclebin");
                office_OpportunitiesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyQuickLookLabelsForOpportunity", "Verify page title as recycled opportunities.");
                VerifyTitle("Recycled Opportunities");

                executionLog.Log("VerifyQuickLookLabelsForOpportunity", "Click on delete icon.");
                office_OpportunitiesHelper.ClickElement("DeletePermanaently");

                executionLog.Log("VerifyQuickLookLabelsForOpportunity", "Accept alert message.");
                office_OpportunitiesHelper.AcceptAlert();

                executionLog.Log("VerifyQuickLookLabelsForOpportunity", "Wait for success text.");
                office_OpportunitiesHelper.WaitForText("Opportunity permanently deleted.", 10);

                VisitOffice("logout");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyQuickLookLabelsForOpportunity");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("VerifyQuickLookLabelsForOpportunity");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Opportunities", "Bug", "Medium", "Opportunity page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("VerifyQuickLookLabelsForOpportunity");
                        TakeScreenshot("VerifyQuickLookLabelsForOpportunity");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyQuickLookLabelsForOpportunity.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyQuickLookLabelsForOpportunity");
                        string id = loginHelper.getIssueID("VerifyQuickLookLabelsForOpportunity");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyQuickLookLabelsForOpportunity.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("VerifyQuickLookLabelsForOpportunity"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("VerifyQuickLookLabelsForOpportunity");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyQuickLookLabelsForOpportunity");
                executionLog.WriteInExcel("VerifyQuickLookLabelsForOpportunity", Status, JIRA, "Opportunity management");
            }
        }
    }
}