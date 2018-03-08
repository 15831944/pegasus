using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class VerifyPartnerAgentAdvanceFilterColumnOrder : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin1")]
        [TestCategory("TS7")]
        [TestCategory("Pegasus_Admin1")]
        public void verifyPartnerAgentAdvanceFilterColumnOrder()
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
            var agents_PartnerAgentsHelper = new Agents_PartnerAgentsHelper(GetWebDriver());

            // Variable Random
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("VerifyPartnerAgentAdvanceFilterColumnOrder", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyPartnerAgentAdvanceFilterColumnOrder", "Verify Page title as dash board");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyPartnerAgentAdvanceFilterColumnOrder", "Redirect To partner agents page.");
                VisitOffice("partners/agents");
                agents_PartnerAgentsHelper.WaitForWorkAround(5000);

                executionLog.Log("VerifyPartnerAgentAdvanceFilterColumnOrder", "Verify page title as partner agents");
                VerifyTitle("Partner Agents");

                executionLog.Log("VerifyPartnerAgentAdvanceFilterColumnOrder", "Verify status column is visible on the page.");
                agents_PartnerAgentsHelper.IsElementPresent("HeadStatus");
                agents_PartnerAgentsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyPartnerAgentAdvanceFilterColumnOrder", "Verify E-Mail column is visible on the page.");
                agents_PartnerAgentsHelper.IsElementPresent("HeadEmail");
                agents_PartnerAgentsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyPartnerAgentAdvanceFilterColumnOrder", "Verify Phone column is visible on the page.");
                agents_PartnerAgentsHelper.IsElementPresent("HeadPhone");
                agents_PartnerAgentsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyPartnerAgentAdvanceFilterColumnOrder", "Verify Modified column is visible on the page.");
                agents_PartnerAgentsHelper.IsElementPresent("HeadModified");
                agents_PartnerAgentsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyPartnerAgentAdvanceFilterColumnOrder", "Click on advance filter button.");
                agents_PartnerAgentsHelper.ClickElement("AdvanceFilter");
                agents_PartnerAgentsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyPartnerAgentAdvanceFilterColumnOrder", "Select status in displayed columns.");
                agents_PartnerAgentsHelper.SelectByText("DisplayedCols", "Status");
                agents_PartnerAgentsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyPartnerAgentAdvanceFilterColumnOrder", "Click arrow to move column to avail cols.");
                agents_PartnerAgentsHelper.ClickElement("RemoveCols");
                agents_PartnerAgentsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyPartnerAgentAdvanceFilterColumnOrder", "Select E-Mail in displayed columns.");
                agents_PartnerAgentsHelper.SelectByText("DisplayedCols", "E-Mail");
                agents_PartnerAgentsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyPartnerAgentAdvanceFilterColumnOrder", "Click arrow to move column to avail cols");
                agents_PartnerAgentsHelper.ClickElement("RemoveCols");
                agents_PartnerAgentsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyPartnerAgentAdvanceFilterColumnOrder", "Select Phone in displayed columns.");
                agents_PartnerAgentsHelper.SelectByText("DisplayedCols", "Phone");
                agents_PartnerAgentsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyPartnerAgentAdvanceFilterColumnOrder", "Click arrow to move column to avail cols");
                agents_PartnerAgentsHelper.ClickElement("RemoveCols");
                agents_PartnerAgentsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyPartnerAgentAdvanceFilterColumnOrder", "Select Modified in displayed columns.");
                agents_PartnerAgentsHelper.SelectByText("DisplayedCols", "Modified");
                agents_PartnerAgentsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyPartnerAgentAdvanceFilterColumnOrder", "Click arrow to move column to avail cols");
                agents_PartnerAgentsHelper.ClickElement("RemoveCols");
                agents_PartnerAgentsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyPartnerAgentAdvanceFilterColumnOrder", "Click on Apply button.");
                agents_PartnerAgentsHelper.ClickElement("ApplyButton");
                agents_PartnerAgentsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyPartnerAgentAdvanceFilterColumnOrder", "Verify status column not present on page.");
                agents_PartnerAgentsHelper.IsElementNotPresent("HeadStatus");
                agents_PartnerAgentsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyPartnerAgentAdvanceFilterColumnOrder", "Verify E-Mail column not present on page.");
                agents_PartnerAgentsHelper.IsElementNotPresent("HeadEmail");
                agents_PartnerAgentsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyPartnerAgentAdvanceFilterColumnOrder", "Verify Phone column not present on page.");
                agents_PartnerAgentsHelper.IsElementNotPresent("HeadPhone");
                agents_PartnerAgentsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyPartnerAgentAdvanceFilterColumnOrder", "Verify Modified column not present on page.");
                agents_PartnerAgentsHelper.IsElementNotPresent("HeadModified");
                agents_PartnerAgentsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyPartnerAgentAdvanceFilterColumnOrder", "Redirect at partner agents page.");
                VisitOffice("partners/agents");
                agents_PartnerAgentsHelper.WaitForWorkAround(5000);

                executionLog.Log("VerifyPartnerAgentAdvanceFilterColumnOrder", "Verify page title as partner agents.");
                VerifyTitle("Partner Agents");

                executionLog.Log("VerifyPartnerAgentAdvanceFilterColumnOrder", "Verify default position of E-Mail column.");
                agents_PartnerAgentsHelper.IsElementPresent("HeadEmail3");
                agents_PartnerAgentsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyPartnerAgentAdvanceFilterColumnOrder", "Verify default position of Phone column.");
                agents_PartnerAgentsHelper.IsElementPresent("HeadPhone4");
                agents_PartnerAgentsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyPartnerAgentAdvanceFilterColumnOrder", "Redirect at partner agents page.");
                VisitOffice("partners/agents");
                agents_PartnerAgentsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyPartnerAgentAdvanceFilterColumnOrder", "Click on advance filter button.");
                agents_PartnerAgentsHelper.ClickElement("AdvanceFilter");
                agents_PartnerAgentsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyPartnerAgentAdvanceFilterColumnOrder", "Select E-Mail in displayed column.");
                agents_PartnerAgentsHelper.SelectByText("DisplayedCols", "E-Mail");
                agents_PartnerAgentsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyPartnerAgentAdvanceFilterColumnOrder", "Move email 1 step up.");
                agents_PartnerAgentsHelper.ClickElement("MoveUp");
                agents_PartnerAgentsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyPartnerAgentAdvanceFilterColumnOrder", "Move email 1 step up.");
                agents_PartnerAgentsHelper.ClickElement("MoveUp");
                agents_PartnerAgentsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyPartnerAgentAdvanceFilterColumnOrder", "Select Phone in displayed column.");
                agents_PartnerAgentsHelper.SelectByText("DisplayedCols", "Phone");
                agents_PartnerAgentsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyPartnerAgentAdvanceFilterColumnOrder", "Move phone 1 step down.");
                agents_PartnerAgentsHelper.ClickElement("MoveDown");
                agents_PartnerAgentsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyPartnerAgentAdvanceFilterColumnOrder", "Click on Apply button.");
                agents_PartnerAgentsHelper.ClickElement("ApplyButton");
                agents_PartnerAgentsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyPartnerAgentAdvanceFilterColumnOrder", "Verify changed position of E-Mail column.");
                agents_PartnerAgentsHelper.IsElementPresent("HeadEmail2");
                agents_PartnerAgentsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyPartnerAgentAdvanceFilterColumnOrder", "Verify changed position of Phone column.");
                agents_PartnerAgentsHelper.IsElementPresent("HeadPhone5");
                agents_PartnerAgentsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyPartnerAgentAdvanceFilterColumnOrder", "Logout from the application.");
                VisitOffice("logout");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyPartnerAgentAdvanceFilterColumnOrder");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Partner Agent Advance Filter Column Order");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Partner Agent Advance Filter Column Order", "Bug", "Medium", "Activities page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Partner Agent Advance Filter Column Order");
                        TakeScreenshot("VerifyPartnerAgentAdvanceFilterColumnOrder");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyPartnerAgentAdvanceFilterColumnOrder.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyPartnerAgentAdvanceFilterColumnOrder");
                        string id = loginHelper.getIssueID("Verify Partner Agent Advance Filter Column Order");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyPartnerAgentAdvanceFilterColumnOrder.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Partner Agent Advance Filter Column Order"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Partner Agent Advance Filter Column Order");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyPartnerAgentAdvanceFilterColumnOrder");
                executionLog.WriteInExcel("Verify Partner Agent Advance Filter Column Order", Status, JIRA, "Meetings Management");
            }
        }
    }
}