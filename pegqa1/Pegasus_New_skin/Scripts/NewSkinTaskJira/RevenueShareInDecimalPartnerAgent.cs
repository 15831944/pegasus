using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class RevenueShareInDecimalPartnerAgent : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS5")]
        [TestCategory("NewSkinTaskJira")]
        public void revenueShareInDecimalPartnerAgent()
        {
            string[] username = null;
            string[] password = null;
            String JIRA = "";
            String Status = "Pass";

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var agents_PartnerAgentsHelper = new Agents_PartnerAgentsHelper(GetWebDriver());

            // Variable
            var code = "1" + RandomNumber(99, 999);
            var name = "TestAgent" + GetRandomNumber();
            var RevenueShare = "22." + RandomNumber(1, 99);


            try
            {
                executionLog.Log("RevenueShareInDecimalPartnerAgent", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("RevenueShareInDecimalPartnerAgent", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("RevenueShareInDecimalPartnerAgent", "Redirect at partner agents page");
                VisitOffice("partners/agents");

                executionLog.Log("RevenueShareInDecimalPartnerAgent", "verify title");
                VerifyTitle("Partner Agents");

                executionLog.Log("RevenueShareInDecimalPartnerAgent", "ClickOnRevenueShare");
                agents_PartnerAgentsHelper.ClickElement("RevenueSahrnepartneragent");

                executionLog.Log("RevenueShareInDecimalPartnerAgent", "Verify title");
                VerifyTitle("Partner Agent Codes and Revenue Shares");

                executionLog.Log("RevenueShareInDecimalPartnerAgent", "Click on add Revenue Share Partner Agnet");
                agents_PartnerAgentsHelper.ClickElement("AddANewAgentRevenueSahre");
                agents_PartnerAgentsHelper.WaitForWorkAround(2000);

                executionLog.Log("RevenueShareInDecimalPartnerAgent", "SelectPartnerAgnetRS");
                agents_PartnerAgentsHelper.SelectByText("SelectPartnerAgnetRS", "First Data Omaha");

                executionLog.Log("RevenueShareInDecimalPartnerAgent", "EnterPartnerCode");
                agents_PartnerAgentsHelper.TypeText("EnterPartnerCode", code);

                executionLog.Log("RevenueShareInDecimalPartnerAgent", "EnterPartnerCode");
                agents_PartnerAgentsHelper.TypeText("RevenueShareDec", RevenueShare);

                executionLog.Log("RevenueShareInDecimalPartnerAgent", "ClickOnSaveRS");
                agents_PartnerAgentsHelper.ClickElement("ClickOnSaveRS");

                executionLog.Log("RevenueShareInDecimalPartnerAgent", "verify message Partner agent code & revenue share saved successfully.");
                agents_PartnerAgentsHelper.WaitForText("revenue share saved successfully.", 10);

                executionLog.Log("RevenueShareInDecimalPartnerAgent", "Verify title");
                VerifyTitle("Partner Agent Codes and Revenue Shares");

                executionLog.Log("RevenueShareInDecimalPartnerAgent", "Enter Deciaml value");
                agents_PartnerAgentsHelper.TypeText("SearchDeciaml", RevenueShare);
                agents_PartnerAgentsHelper.WaitForWorkAround(3000);

                executionLog.Log("RevenueShareInDecimalPartnerAgent", "Verify value oin decimal");
                agents_PartnerAgentsHelper.VerifyPageText(RevenueShare);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("RevenueShareInDecimalPartnerAgent");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Revenue Share In Decimal Partner Agent");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Revenue Share In Decimal Partner Agent", "Bug", "Medium", "Partner page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Revenue Share In Decimal Partner Agent");
                        TakeScreenshot("RevenueShareInDecimalPartnerAgent");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\RevenueShareInDecimalPartnerAgent.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("RevenueShareInDecimalPartnerAgent");
                        string id = loginHelper.getIssueID("Revenue Share In Decimal Partner Agent");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\RevenueShareInDecimalPartnerAgent.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Revenue Share In Decimal Partner Agent"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Revenue Share In Decimal Partner Agent");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("RevenueShareInDecimalPartnerAgent");
                executionLog.WriteInExcel("Revenue Share In Decimal Partner Agent", Status, JIRA, "Agent Portal");
            }
        }
    }
}