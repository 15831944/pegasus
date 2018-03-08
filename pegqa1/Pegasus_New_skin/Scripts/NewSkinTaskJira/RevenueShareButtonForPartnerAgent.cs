using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class RevenueShareButtonForPartnerAgent : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS5")]
        [TestCategory("NewSkinTaskJira")]
        public void revenueShareButtonForPartnerAgent()
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
            var name = "TestAgent" + GetRandomNumber();

            try
            {
                executionLog.Log("RevenueShareButtonForPartnerAgent", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("RevenueShareButtonForPartnerAgent", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("RevenueShareButtonForPartnerAgent", "Redirect at partner agent page.");
                VisitOffice("partners/agents");

                executionLog.Log("RevenueShareButtonForPartnerAgent", "Click On Revenue Share");
                agents_PartnerAgentsHelper.ClickElement("RevenueSahrnepartneragent");
                agents_PartnerAgentsHelper.WaitForWorkAround(2000);

                executionLog.Log("RevenueShareButtonForPartnerAgent", "Verify partner agent available");
                agents_PartnerAgentsHelper.VerifyPageText("Partner Agents");
                agents_PartnerAgentsHelper.WaitForWorkAround(3000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("RevenueShareButtonForPartnerAgent");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Revenue Share Button For Partner Agent");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Revenue Share Button For Partner Agent", "Bug", "Medium", "Partner Agent page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Revenue Share Button For Partner Agent");
                        TakeScreenshot("RevenueShareButtonForPartnerAgent");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\RevenueShareButtonForPartnerAgent.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("RevenueShareButtonForPartnerAgent");
                        string id = loginHelper.getIssueID("Revenue Share Button For Partner Agent");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\RevenueShareButtonForPartnerAgent.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Revenue Share Button For Partner Agent"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Revenue Share Button For Partner Agent");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("RevenueShareButtonForPartnerAgent");
                executionLog.WriteInExcel("Revenue Share Button For Partner Agent", Status, JIRA, "Agent Portal");
            }
        }
    }
}