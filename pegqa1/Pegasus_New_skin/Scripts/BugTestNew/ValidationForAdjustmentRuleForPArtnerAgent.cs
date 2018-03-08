using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class ValidationForAdjustmentRuleForPartnerAgent : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS3")]
        [TestCategory("BugTestNew")]
        public void validationForAdjustmentRuleForPartnerAgent()
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
            var agent_PartnerAgentHelper = new Agents_PartnerAgentsHelper(GetWebDriver());
            var residualIncome_MasterDataHelper = new ResidualIncome_MasterDataHelper(GetWebDriver());

            // Variable
            var name = "TestAgent" + GetRandomNumber();
            var Adjustment = "Test" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("ValidationForAdjustmentRuleForPartnerAgent", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("ValidationForAdjustmentRuleForPartnerAgent", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("ValidationForAdjustmentRuleForPartnerAgent", "Redirect to the URL");
                VisitOffice("partners/agents");
                agent_PartnerAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("ValidationForAdjustmentRuleForPartnerAgent", "Click on any agent.");
                agent_PartnerAgentHelper.ClickElement("OpenAgent");
                agent_PartnerAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("ValidationForAdjustmentRuleForPartnerAgent", "Click create adjustment button");
                agent_PartnerAgentHelper.ClickElement("CreateRevenueAdjstmnt");
                agent_PartnerAgentHelper.WaitForWorkAround(2000);

                executionLog.Log("ValidationForAdjustmentRuleForPartnerAgent", "Enter Adjustment name.");
                agent_PartnerAgentHelper.TypeText("EnterAdjustmentName", Adjustment);

                executionLog.Log("ValidationForAdjustmentRuleForPartnerAgent", "Select Adjustment for");
                agent_PartnerAgentHelper.Select("SelectAdjustmentFor", "Agent");

                executionLog.Log("ValidationForAdjustmentRuleForPartnerAgent", "Select Adjustment type");
                agent_PartnerAgentHelper.Select("AdjustmentType", "Transaction");

                executionLog.Log("ValidationForAdjustmentRuleForPartnerAgent", "Select reporting period.");
                agent_PartnerAgentHelper.Select("SelectReportingPeriod", "00");

                executionLog.Log("ValidationForAdjustmentRuleForPartnerAgent", "Select processor");
                agent_PartnerAgentHelper.Select("SelectProcessor", "Any");

                executionLog.Log("ValidationForAdjustmentRuleForPartnerAgent", "Select rule type.");
                agent_PartnerAgentHelper.Select("SelectRuleType", "1");

                executionLog.Log("ValidationForAdjustmentRuleForPartnerAgent", "Enter Amount");
                agent_PartnerAgentHelper.TypeText("EnterAmount", "50");

                executionLog.Log("ValidationForAdjustmentRuleForPartnerAgent", "Select add or remove ");
                agent_PartnerAgentHelper.Select("AddRemove", "Add");

                executionLog.Log("ValidationForAdjustmentRuleForPartnerAgent", "Save Adjustment ");
                agent_PartnerAgentHelper.ClickElement("SaveAdjustment");
                agent_PartnerAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("ValidationForAdjustmentRuleForPartnerAgent", "Verify success message for MAster adjustment. ");
                agent_PartnerAgentHelper.VerifyPageText("Master Adjustment Rules Created Successfully.");

                executionLog.Log("ValidationForAdjustmentRuleForPartnerAgent", "Redirect at adjustment tools page.");
                VisitOffice("rir/adjustments_tool");
                agent_PartnerAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("ValidationForAdjustmentRuleForPartnerAgent", "Verify page title.");
                VerifyTitle("Adjustments Tool");

                executionLog.Log("ValidationForAdjustmentRuleForPartnerAgent", "Enter adjustment to be deleted.");
                residualIncome_MasterDataHelper.TypeText("EnterAdjustmentNameSrch", Adjustment);
                residualIncome_MasterDataHelper.WaitForWorkAround(3000);

                executionLog.Log("ValidationForAdjustmentRuleForPartnerAgent", "Click on delete icon.");
                residualIncome_MasterDataHelper.ClickElement("DeleteAdjtmnt");

                executionLog.Log("ValidationForAdjustmentRuleForPartnerAgent", "Accept alert message.");
                residualIncome_MasterDataHelper.AcceptAlert();

                executionLog.Log("ValidationForAdjustmentRuleForPartnerAgent", "Wait for delete success.");
                residualIncome_MasterDataHelper.WaitForText("Ruleset deleted successfully.", 10);


            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("ValidationForAdjustmentRuleForPartnerAgent");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Validation For Adjustment Rule For Partner Agent");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Validation For Adjustment Rule For Partner Agent", "Bug", "Medium", "Partner page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Validation For Adjustment Rule For Partner Agent");
                        TakeScreenshot("ValidationForAdjustmentRuleForPartnerAgent");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ValidationForAdjustmentRuleForPartnerAgent.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("ValidationForAdjustmentRuleForPartnerAgent");
                        string id = loginHelper.getIssueID("Validation For Adjustment Rule For Partner Agent");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ValidationForAdjustmentRuleForPartnerAgent.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Validation For Adjustment Rule For Partner Agent"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Validation For Adjustment Rule For Partner Agent");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("ValidationForAdjustmentRuleForPartnerAgent");
                executionLog.WriteInExcel("Validation For Adjustment Rule For Partner Agent", Status, JIRA, "Agents Portal");
            }
        }
    }
}
