using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class VerifyPartnerAssociationAdvanceFilterColumnOrder : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin1")]
        [TestCategory("TS7")]
        [TestCategory("Pegasus_Admin1")]
        public void verifyPartnerAssociationAdvanceFilterColumnOrder()
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
            var agents_PartnerAssociationHelper = new Agents_PartnerAssociationHelper(GetWebDriver());

            // Variable Random
            String JIRA = "";
            String Status = "Pass";
            try
            {
                executionLog.Log("VerifyPartnerAssociationAdvanceFilterColumnOrder", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyPartnerAssociationAdvanceFilterColumnOrder", "Verify Page title as dash board");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyPartnerAssociationAdvanceFilterColumnOrder", "Redirect To partner association page.");
                VisitOffice("partners/associations");
                agents_PartnerAssociationHelper.WaitForWorkAround(5000);

                executionLog.Log("VerifyPartnerAssociationAdvanceFilterColumnOrder", "Verify page title as partner association");
                VerifyTitle("Partner Associations");

                executionLog.Log("VerifyPartnerAssociationAdvanceFilterColumnOrder", "Verify status column is visible on the page.");
                agents_PartnerAssociationHelper.IsElementPresent("HeadStatus");
                agents_PartnerAssociationHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyPartnerAssociationAdvanceFilterColumnOrder", "Verify E-Mail column is visible on the page.");
                agents_PartnerAssociationHelper.IsElementPresent("HeadEmail");
                agents_PartnerAssociationHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyPartnerAssociationAdvanceFilterColumnOrder", "Verify Phone column is visible on the page.");
                agents_PartnerAssociationHelper.IsElementPresent("HeadPhone");
                agents_PartnerAssociationHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyPartnerAssociationAdvanceFilterColumnOrder", "Verify Modified column is visible on the page.");
                agents_PartnerAssociationHelper.IsElementPresent("HeadModified");
                agents_PartnerAssociationHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyPartnerAssociationAdvanceFilterColumnOrder", "Click on advance filter button.");
                agents_PartnerAssociationHelper.ClickElement("AdvanceFilter");
                agents_PartnerAssociationHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyPartnerAssociationAdvanceFilterColumnOrder", "Select status in displayed columns.");
                agents_PartnerAssociationHelper.SelectByText("DisplayedCols", "Status");
                agents_PartnerAssociationHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyPartnerAssociationAdvanceFilterColumnOrder", "Click arrow to move column to avail cols.");
                agents_PartnerAssociationHelper.ClickElement("RemoveCols");
                agents_PartnerAssociationHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyPartnerAssociationAdvanceFilterColumnOrder", "Select E-Mail in displayed columns.");
                agents_PartnerAssociationHelper.SelectByText("DisplayedCols", "E-Mail");
                agents_PartnerAssociationHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyPartnerAssociationAdvanceFilterColumnOrder", "Click arrow to move column to avail cols");
                agents_PartnerAssociationHelper.ClickElement("RemoveCols");
                agents_PartnerAssociationHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyPartnerAssociationAdvanceFilterColumnOrder", "Select Phone in displayed columns.");
                agents_PartnerAssociationHelper.SelectByText("DisplayedCols", "Phone");
                agents_PartnerAssociationHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyPartnerAssociationAdvanceFilterColumnOrder", "Click arrow to move column to avail cols");
                agents_PartnerAssociationHelper.ClickElement("RemoveCols");
                agents_PartnerAssociationHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyPartnerAssociationAdvanceFilterColumnOrder", "Select Modified in displayed columns.");
                agents_PartnerAssociationHelper.SelectByText("DisplayedCols", "Modified");
                agents_PartnerAssociationHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyPartnerAssociationAdvanceFilterColumnOrder", "Click arrow to move column to avail cols");
                agents_PartnerAssociationHelper.ClickElement("RemoveCols");
                agents_PartnerAssociationHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyPartnerAssociationAdvanceFilterColumnOrder", "Click on Apply button.");
                agents_PartnerAssociationHelper.ClickElement("ApplyButton");
                agents_PartnerAssociationHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyPartnerAssociationAdvanceFilterColumnOrder", "Verify status column not present on page.");
                agents_PartnerAssociationHelper.IsElementNotPresent("HeadStatus");
                agents_PartnerAssociationHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyPartnerAssociationAdvanceFilterColumnOrder", "Verify E-Mail column not present on page.");
                agents_PartnerAssociationHelper.IsElementNotPresent("HeadEmail");
                agents_PartnerAssociationHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyPartnerAssociationAdvanceFilterColumnOrder", "Verify Phone column not present on page.");
                agents_PartnerAssociationHelper.IsElementNotPresent("HeadPhone");
                agents_PartnerAssociationHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyPartnerAssociationAdvanceFilterColumnOrder", "Verify Modified column not present on page.");
                agents_PartnerAssociationHelper.IsElementNotPresent("HeadModified");
                agents_PartnerAssociationHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyPartnerAssociationAdvanceFilterColumnOrder", "Redirect at leads page.");
                VisitOffice("leads");

                executionLog.Log("VerifyPartnerAssociationAdvanceFilterColumnOrder", "Verify page title as leads.");
                VerifyTitle("Leads");

                executionLog.Log("VerifyPartnerAssociationAdvanceFilterColumnOrder", "Redirect at partner associations page.");
                VisitOffice("partners/associations");
                agents_PartnerAssociationHelper.WaitForWorkAround(5000);

                executionLog.Log("VerifyPartnerAssociationAdvanceFilterColumnOrder", "Verify page title as partner association.");
                VerifyTitle("Partner Associations");

                executionLog.Log("VerifyPartnerAssociationAdvanceFilterColumnOrder", "Verify default position of E-Mail column.");
                agents_PartnerAssociationHelper.IsElementPresent("HeadEmail5");
                agents_PartnerAssociationHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyPartnerAssociationAdvanceFilterColumnOrder", "Verify default position of Phone column.");
                agents_PartnerAssociationHelper.IsElementPresent("HeadPhone6");
                agents_PartnerAssociationHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyPartnerAssociationAdvanceFilterColumnOrder", "Redirect at partner associations page.");
                VisitOffice("partners/associations");
                agents_PartnerAssociationHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyPartnerAssociationAdvanceFilterColumnOrder", "Click on advance filter button.");
                agents_PartnerAssociationHelper.ClickElement("AdvanceFilter");
                agents_PartnerAssociationHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyPartnerAssociationAdvanceFilterColumnOrder", "Select E-Mail in displayed column.");
                agents_PartnerAssociationHelper.SelectByText("DisplayedCols", "E-Mail");
                agents_PartnerAssociationHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyPartnerAssociationAdvanceFilterColumnOrder", "Move email 1 step up.");
                agents_PartnerAssociationHelper.ClickElement("MoveUp");
                agents_PartnerAssociationHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyPartnerAssociationAdvanceFilterColumnOrder", "Move email 1 step up.");
                agents_PartnerAssociationHelper.ClickElement("MoveUp");
                agents_PartnerAssociationHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyPartnerAssociationAdvanceFilterColumnOrder", "Move email 1 step up.");
                agents_PartnerAssociationHelper.ClickElement("MoveUp");
                agents_PartnerAssociationHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyPartnerAssociationAdvanceFilterColumnOrder", "Select Phone in displayed column.");
                agents_PartnerAssociationHelper.SelectByText("DisplayedCols", "Phone");
                agents_PartnerAssociationHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyPartnerAssociationAdvanceFilterColumnOrder", "Move phone 1 step down.");
                agents_PartnerAssociationHelper.ClickElement("MoveDown");
                agents_PartnerAssociationHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyPartnerAssociationAdvanceFilterColumnOrder", "Click on Apply button.");
                agents_PartnerAssociationHelper.ClickElement("ApplyButton");
                agents_PartnerAssociationHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyPartnerAssociationAdvanceFilterColumnOrder", "Verify changed position of E-Mail column.");
                agents_PartnerAssociationHelper.IsElementPresent("HeadEmail3");
                agents_PartnerAssociationHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyPartnerAssociationAdvanceFilterColumnOrder", "Verify changed position of Phone column.");
                agents_PartnerAssociationHelper.IsElementPresent("HeadPhone7");
                agents_PartnerAssociationHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyPartnerAssociationAdvanceFilterColumnOrder", "Logout from the application.");
                VisitOffice("logout");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyPartnerAssociationAdvanceFilterColumnOrder");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Partner Association Advance Filter Column Order");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Partner Association Advance Filter Column Order", "Bug", "Medium", "Activities page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Partner Association Advance Filter Column Order");
                        TakeScreenshot("VerifyPartnerAssociationAdvanceFilterColumnOrder");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyPartnerAssociationAdvanceFilterColumnOrder.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyPartnerAssociationAdvanceFilterColumnOrder");
                        string id = loginHelper.getIssueID("Verify Partner Association Advance Filter Column Order");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyPartnerAssociationAdvanceFilterColumnOrder.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Partner Association Advance Filter Column Order"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Partner Association Advance Filter Column Order");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyPartnerAssociationAdvanceFilterColumnOrder");
                executionLog.WriteInExcel("Verify Partner Association Advance Filter Column Order", Status, JIRA, "Meetings Management");
            }
        }
    }
}