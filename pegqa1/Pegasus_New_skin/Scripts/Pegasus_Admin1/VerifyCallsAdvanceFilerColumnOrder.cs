using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class VerifyCallsAdvanceFilerColumnOrder : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin1")]
        [TestCategory("TS7")]
        [TestCategory("Pegasus_Admin1")]
        public void verifyCallsAdvanceFilerColumnOrder()
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
            var officeActivities_CallsHelper = new OfficeActivities_CallsHelper(GetWebDriver());

            // Variable Random
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("VerifyCallsAdvanceFilerColumnOrder", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyCallsAdvanceFilerColumnOrder", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyCallsAdvanceFilerColumnOrder", "Redirect To URL");
                VisitOffice("calls");
                officeActivities_CallsHelper.WaitForWorkAround(5000);

                executionLog.Log("VerifyCallsAdvanceFilerColumnOrder", "Verify page title.");
                VerifyTitle("Calls");

                executionLog.Log("VerifyCallsAdvanceFilerColumnOrder", "Verify related to column is visible on the page.");
                officeActivities_CallsHelper.IsElementPresent("HeadRelatedTo");
                officeActivities_CallsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCallsAdvanceFilerColumnOrder", "Verify call date column is visible on the page.");
                officeActivities_CallsHelper.IsElementPresent("HeadCallDate");
                officeActivities_CallsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCallsAdvanceFilerColumnOrder", "Verify employee column is visible on the page.");
                officeActivities_CallsHelper.IsElementPresent("HeadEmployee");
                officeActivities_CallsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCallsAdvanceFilerColumnOrder", "Verify Modified column is visible on the page.");
                officeActivities_CallsHelper.IsElementPresent("HeadModified");
                officeActivities_CallsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCallsAdvanceFilerColumnOrder", "Click on advance filter.");
                officeActivities_CallsHelper.ClickElement("AdvanceFilter");
                officeActivities_CallsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCallsAdvanceFilerColumnOrder", "Select related to in displayed columns.");
                officeActivities_CallsHelper.SelectByText("DisplayedCols", "Related To");
                officeActivities_CallsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCallsAdvanceFilerColumnOrder", "Click arrow to move column to avail cols.");
                officeActivities_CallsHelper.ClickElement("RemoveCols");
                officeActivities_CallsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCallsAdvanceFilerColumnOrder", "Select call date in displayed columns.");
                officeActivities_CallsHelper.SelectByText("DisplayedCols", "Call Date");
                officeActivities_CallsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCallsAdvanceFilerColumnOrder", "Click arrow to move column to avail cols");
                officeActivities_CallsHelper.ClickElement("RemoveCols");
                officeActivities_CallsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCallsAdvanceFilerColumnOrder", "Select employee in displayed columns.");
                officeActivities_CallsHelper.SelectByText("DisplayedCols", "Employee");
                officeActivities_CallsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCallsAdvanceFilerColumnOrder", "Click arrow to move column to avail cols");
                officeActivities_CallsHelper.ClickElement("RemoveCols");
                officeActivities_CallsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCallsAdvanceFilerColumnOrder", "Select Modified in displayed columns.");
                officeActivities_CallsHelper.SelectByText("DisplayedCols", "Modified");
                officeActivities_CallsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCallsAdvanceFilerColumnOrder", "Click arrow to move column to avail cols");
                officeActivities_CallsHelper.ClickElement("RemoveCols");
                officeActivities_CallsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCallsAdvanceFilerColumnOrder", "Click on apply button.");
                officeActivities_CallsHelper.ClickElement("Apply");
                officeActivities_CallsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCallsAdvanceFilerColumnOrder", "Verify ralated to not present on page.");
                officeActivities_CallsHelper.IsElementNotPresent("HeadRelatedTo");
                officeActivities_CallsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCallsAdvanceFilerColumnOrder", "Verify call date not present on page.");
                officeActivities_CallsHelper.IsElementNotPresent("HeadCallDate");
                officeActivities_CallsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCallsAdvanceFilerColumnOrder", "Verify employee not present on page.");
                officeActivities_CallsHelper.IsElementNotPresent("HeadEmployee");
                officeActivities_CallsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCallsAdvanceFilerColumnOrder", "Verify Modified not present on page.");
                officeActivities_CallsHelper.IsElementNotPresent("HeadModified");
                officeActivities_CallsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCallsAdvanceFilerColumnOrder", "Redirect at leads page.");
                VisitOffice("leads");

                executionLog.Log("VerifyCallsAdvanceFilerColumnOrder", "Verify page title as leads.");
                VerifyTitle("Leads");

                executionLog.Log("VerifyCallsAdvanceFilerColumnOrder", "Redirect at calls page.");
                VisitOffice("calls");
                officeActivities_CallsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCallsAdvanceFilerColumnOrder", "Verify page title as calls");
                VerifyTitle("Calls");
                officeActivities_CallsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCallsAdvanceFilerColumnOrder", "Verify default position of related to column.");
                officeActivities_CallsHelper.IsElementPresent("HeadRelatedTo6");
                officeActivities_CallsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCallsAdvanceFilerColumnOrder", "Verify default position of employee column.");
                officeActivities_CallsHelper.IsElementPresent("HeadEmployee7");
                officeActivities_CallsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCallsAdvanceFilerColumnOrder", "Redirect at calls page.");
                VisitOffice("calls");
                officeActivities_CallsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCallsAdvanceFilerColumnOrder", "Click on advance filter.");
                officeActivities_CallsHelper.ClickElement("AdvanceFilter");
                officeActivities_CallsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCallsAdvanceFilerColumnOrder", "Select related to in displayed column.");
                officeActivities_CallsHelper.SelectByText("DisplayedCols", "Related To");
                officeActivities_CallsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCallsAdvanceFilerColumnOrder", "Move related to 1 step up.");
                officeActivities_CallsHelper.ClickElement("MoveUp");
                officeActivities_CallsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCallsAdvanceFilerColumnOrder", "Move related to 1 step up.");
                officeActivities_CallsHelper.ClickElement("MoveUp");
                officeActivities_CallsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCallsAdvanceFilerColumnOrder", "Move related to 1 step up.");
                officeActivities_CallsHelper.ClickElement("MoveUp");
                officeActivities_CallsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCallsAdvanceFilerColumnOrder", "Select employee in displayed column.");
                officeActivities_CallsHelper.SelectByText("DisplayedCols", "Employee");
                officeActivities_CallsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCallsAdvanceFilerColumnOrder", "Move employee 1 step down.");
                officeActivities_CallsHelper.ClickElement("MoveDown");
                officeActivities_CallsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCallsAdvanceFilerColumnOrder", "Click on apply button.");
                officeActivities_CallsHelper.ClickElement("Apply");
                officeActivities_CallsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCallsAdvanceFilerColumnOrder", "Verify changed position of related to column.");
                officeActivities_CallsHelper.IsElementPresent("HeadRelatedTo4");
                officeActivities_CallsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCallsAdvanceFilerColumnOrder", "Verify changed position of employee column.");
                officeActivities_CallsHelper.IsElementPresent("HeadEmployee8");
                officeActivities_CallsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCallsAdvanceFilerColumnOrder", "Logout from the application.");
                VisitOffice("logout");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyCallsAdvanceFilerColumnOrder");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Calls Advance Filer Column Order");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Calls Advance Filer Column Order", "Bug", "Medium", "Activities page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Calls Advance Filer Column Order");
                        TakeScreenshot("VerifyCallsAdvanceFilerColumnOrder");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyCallsAdvanceFilerColumnOrder.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyCallsAdvanceFilerColumnOrder");
                        string id = loginHelper.getIssueID("Verify Calls Advance Filer Column Order");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyCallsAdvanceFilerColumnOrder.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Calls Advance Filer Column Order"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Calls Advance Filer Column Order");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyCallsAdvanceFilerColumnOrder");
                executionLog.WriteInExcel("Verify Calls Advance Filer Column Order", Status, JIRA, "Calls Management");
            }
        }
    }
}