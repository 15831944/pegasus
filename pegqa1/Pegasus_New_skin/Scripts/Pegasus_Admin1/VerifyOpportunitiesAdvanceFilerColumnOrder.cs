using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class VerifyOpportunitiesAdvanceFilerColumnOrder : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin1")]
        [TestCategory("TS7")]
        [TestCategory("Pegasus_Admin1")]
        public void verifyOpportunitiesAdvanceFilerColumnOrder()
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

            // Variable
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("VerifyOpportunitiesAdvanceFilerColumnOrder", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyOpportunitiesAdvanceFilerColumnOrder", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyOpportunitiesAdvanceFilerColumnOrder", "Redirect To URL");
                VisitOffice("opportunities");
                office_OpportunitiesHelper.WaitForWorkAround(5000);

                executionLog.Log("VerifyOpportunitiesAdvanceFilerColumnOrder", "Verify page title.");
                VerifyTitle("Opportunities");

                executionLog.Log("VerifyOpportunitiesAdvanceFilerColumnOrder", "Verify default position of company name column.");
                office_OpportunitiesHelper.IsElementPresent("HeadCompany");
                office_OpportunitiesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyOpportunitiesAdvanceFilerColumnOrder", "Verify default position of contact column.");
                office_OpportunitiesHelper.IsElementPresent("HeadAmount");
                office_OpportunitiesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyOpportunitiesAdvanceFilerColumnOrder", "Verify default position of phone column.");
                office_OpportunitiesHelper.IsElementPresent("HeadIndustry");
                office_OpportunitiesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyOpportunitiesAdvanceFilerColumnOrder", "Verify default position of email column.");
                office_OpportunitiesHelper.IsElementPresent("HeadSource");
                office_OpportunitiesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyOpportunitiesAdvanceFilerColumnOrder", "Click on advance filter.");
                office_OpportunitiesHelper.ClickElement("AdvanceFilter");
                office_OpportunitiesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyOpportunitiesAdvanceFilerColumnOrder", "Select company in displayed columns.");
                office_OpportunitiesHelper.SelectByText("DisplayedCols", "Company");
                office_OpportunitiesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyOpportunitiesAdvanceFilerColumnOrder", "Click arrow to move column to avail cols.");
                office_OpportunitiesHelper.ClickElement("RemoveCols");
                office_OpportunitiesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyOpportunitiesAdvanceFilerColumnOrder", "Select contact in displayed columns.");
                office_OpportunitiesHelper.SelectByText("DisplayedCols", "Name");
                office_OpportunitiesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyOpportunitiesAdvanceFilerColumnOrder", "Click arrow to move column to avail cols");
                office_OpportunitiesHelper.ClickElement("RemoveCols");
                office_OpportunitiesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyOpportunitiesAdvanceFilerColumnOrder", "Select phone in displayed columns.");
                office_OpportunitiesHelper.SelectByText("DisplayedCols", "Phone");
                office_OpportunitiesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyOpportunitiesAdvanceFilerColumnOrder", "Click arrow to move column to avail cols");
                office_OpportunitiesHelper.ClickElement("RemoveCols");
                office_OpportunitiesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyOpportunitiesAdvanceFilerColumnOrder", "Select email in displayed columns.");
                office_OpportunitiesHelper.SelectByText("DisplayedCols", "E-Mail");
                office_OpportunitiesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyOpportunitiesAdvanceFilerColumnOrder", "Click arrow to move column to avail cols");
                office_OpportunitiesHelper.ClickElement("RemoveCols");
                office_OpportunitiesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyOpportunitiesAdvanceFilerColumnOrder", "Click on apply button.");
                office_OpportunitiesHelper.ClickElement("Apply");
                office_OpportunitiesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyOpportunitiesAdvanceFilerColumnOrder", "Verify company name not present on page.");
                office_OpportunitiesHelper.IsElementNotPresent("HeadCompany");
                office_OpportunitiesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyOpportunitiesAdvanceFilerColumnOrder", "Verify contact name not present on page.");
                office_OpportunitiesHelper.IsElementNotPresent("HeadContact");
                office_OpportunitiesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyOpportunitiesAdvanceFilerColumnOrder", "Verify phone not present on page.");
                office_OpportunitiesHelper.IsElementNotPresent("HeadPhone");
                office_OpportunitiesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyOpportunitiesAdvanceFilerColumnOrder", "Verify email not present on page.");
                office_OpportunitiesHelper.IsElementNotPresent("HeadEmail");
                office_OpportunitiesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyOpportunitiesAdvanceFilerColumnOrder", "Logout from the application.");
                VisitOffice("logout");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyOpportunitiesAdvanceFilerColumnOrder");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Opportunities Advance Filter ResultsPP");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Opportunities Advance Filter ResultsPP", "Bug", "Medium", "Opportunities page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Opportunities Advance Filter ResultsPP");
                        TakeScreenshot("VerifyOpportunitiesAdvanceFilerColumnOrder");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyOpportunitiesAdvanceFilerColumnOrder.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyOpportunitiesAdvanceFilerColumnOrder");
                        string id = loginHelper.getIssueID("Opportunities Advance Filter ResultsPP");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyOpportunitiesAdvanceFilerColumnOrder.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Opportunities Advance Filter ResultsPP"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Opportunities Advance Filter ResultsPP");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyOpportunitiesAdvanceFilerColumnOrder");
                executionLog.WriteInExcel("Opportunities Advance Filter ResultsPP", Status, JIRA, "Opportunities Management");
            }
        }
    }
}