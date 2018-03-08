using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class VerifyTicketsAdvanceFilerColumnOrder : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin1")]
        [TestCategory("Fail")]
        [TestCategory("TS7")]
        [TestCategory("Pegasus_Admin1")]
        public void verifyTicketsAdvanceFilerColumnOrder()
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
            var officeTickets_AllTicketsHelper = new OfficeTickets_AllTicketsHelper(GetWebDriver());

            // Variable Random
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("VerifyTicketsAdvanceFilerColumnOrder", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyTicketsAdvanceFilerColumnOrder", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyTicketsAdvanceFilerColumnOrder", "Redirect To URL");
                VisitOffice("tickets");
                officeTickets_AllTicketsHelper.WaitForWorkAround(5000);

                executionLog.Log("VerifyTicketsAdvanceFilerColumnOrder", "Verify page title.");
                VerifyTitle("Tickets");
                officeTickets_AllTicketsHelper.WaitForWorkAround(5000);

                executionLog.Log("VerifyTicketsAdvanceFilerColumnOrder", "Verify status column is visible on the page..");
                officeTickets_AllTicketsHelper.IsElementPresent("HeadStatus");
                officeTickets_AllTicketsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTicketsAdvanceFilerColumnOrder", "Verify priority column is visible on the page.");
                officeTickets_AllTicketsHelper.IsElementPresent("HeadPriority");
                officeTickets_AllTicketsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTicketsAdvanceFilerColumnOrder", "Verify age column is visible on the page.");
                officeTickets_AllTicketsHelper.IsElementPresent("HeadAge");
                officeTickets_AllTicketsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTicketsAdvanceFilerColumnOrder", "Verify category column is visible on the page.");
                officeTickets_AllTicketsHelper.IsElementPresent("HeadCategory");
                officeTickets_AllTicketsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTicketsAdvanceFilerColumnOrder", "Click on advance filter.");
                officeTickets_AllTicketsHelper.ClickElement("AdvanceFilter");
                officeTickets_AllTicketsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTicketsAdvanceFilerColumnOrder", "Select status in displayed columns.");
                officeTickets_AllTicketsHelper.SelectByText("DisplayedCols", "Status");
                officeTickets_AllTicketsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTicketsAdvanceFilerColumnOrder", "Click arrow to move column to avail cols.");
                officeTickets_AllTicketsHelper.ClickElement("RemoveCols");
                officeTickets_AllTicketsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTicketsAdvanceFilerColumnOrder", "Select priority in displayed columns.");
                officeTickets_AllTicketsHelper.SelectByText("DisplayedCols", "Priority");
                officeTickets_AllTicketsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTicketsAdvanceFilerColumnOrder", "Click arrow to move column to avail cols");
                officeTickets_AllTicketsHelper.ClickElement("RemoveCols");
                officeTickets_AllTicketsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTicketsAdvanceFilerColumnOrder", "Select age in displayed columns.");
                officeTickets_AllTicketsHelper.SelectByText("DisplayedCols", "Age");
                officeTickets_AllTicketsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTicketsAdvanceFilerColumnOrder", "Click arrow to move column to avail cols");
                officeTickets_AllTicketsHelper.ClickElement("RemoveCols");
                officeTickets_AllTicketsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTicketsAdvanceFilerColumnOrder", "Select category in displayed columns.");
                officeTickets_AllTicketsHelper.SelectByText("DisplayedCols", "Category");
                officeTickets_AllTicketsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTicketsAdvanceFilerColumnOrder", "Click arrow to move column to avail cols");
                officeTickets_AllTicketsHelper.ClickElement("RemoveCols");
                officeTickets_AllTicketsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTicketsAdvanceFilerColumnOrder", "Click on apply button.");
                officeTickets_AllTicketsHelper.ClickElement("Apply");
                officeTickets_AllTicketsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTicketsAdvanceFilerColumnOrder", "Verify status not present on page.");
                officeTickets_AllTicketsHelper.IsElementNotPresent("HeadStatus");
                officeTickets_AllTicketsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTicketsAdvanceFilerColumnOrder", "Verify priority not present on page.");
                officeTickets_AllTicketsHelper.IsElementNotPresent("HeadPriority");
                officeTickets_AllTicketsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTicketsAdvanceFilerColumnOrder", "Verify age not present on page.");
                officeTickets_AllTicketsHelper.IsElementNotPresent("HeadAge");
                officeTickets_AllTicketsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTicketsAdvanceFilerColumnOrder", "Verify category not present on page.");
                officeTickets_AllTicketsHelper.IsElementNotPresent("HeadCategory");
                officeTickets_AllTicketsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTicketsAdvanceFilerColumnOrder", "Redirect at tickets page.");
                VisitOffice("tickets");
                officeTickets_AllTicketsHelper.WaitForWorkAround(5000);

                executionLog.Log("VerifyTicketsAdvanceFilerColumnOrder", "Verify page title as tickets");
                VerifyTitle("Tickets");
                officeTickets_AllTicketsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTicketsAdvanceFilerColumnOrder", "Verify default status of status column.");
                officeTickets_AllTicketsHelper.IsElementPresent("HeadStatus4");
                officeTickets_AllTicketsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTicketsAdvanceFilerColumnOrder", "Verify default position of prority column.");
                officeTickets_AllTicketsHelper.IsElementPresent("HeadPriority5");
                officeTickets_AllTicketsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTicketsAdvanceFilerColumnOrder", "Redirect at tickets page.");
                VisitOffice("tickets");
                officeTickets_AllTicketsHelper.WaitForWorkAround(5000);

                executionLog.Log("VerifyTicketsAdvanceFilerColumnOrder", "Click on advance filter.");
                officeTickets_AllTicketsHelper.ClickElement("AdvanceFilter");
                officeTickets_AllTicketsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTicketsAdvanceFilerColumnOrder", "Select status in displayed column.");
                officeTickets_AllTicketsHelper.SelectByText("DisplayedCols", "Status");
                officeTickets_AllTicketsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTicketsAdvanceFilerColumnOrder", "Move status 1 step up.");
                officeTickets_AllTicketsHelper.ClickElement("MoveUp");
                officeTickets_AllTicketsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTicketsAdvanceFilerColumnOrder", "Move status 1 step up.");
                officeTickets_AllTicketsHelper.ClickElement("MoveUp");
                officeTickets_AllTicketsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTicketsAdvanceFilerColumnOrder", "Move status 1 step up.");
                officeTickets_AllTicketsHelper.ClickElement("MoveUp");
                officeTickets_AllTicketsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTicketsAdvanceFilerColumnOrder", "Select priority in displayed column.");
                officeTickets_AllTicketsHelper.SelectByText("DisplayedCols", "Priority");
                officeTickets_AllTicketsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTicketsAdvanceFilerColumnOrder", "Move priority 1 step down.");
                officeTickets_AllTicketsHelper.ClickElement("MoveDown");
                officeTickets_AllTicketsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTicketsAdvanceFilerColumnOrder", "Click on apply button.");
                officeTickets_AllTicketsHelper.ClickElement("Apply");
                officeTickets_AllTicketsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTicketsAdvanceFilerColumnOrder", "Verify changed position of status column.");
                officeTickets_AllTicketsHelper.IsElementPresent("HeadStatus2");
                officeTickets_AllTicketsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTicketsAdvanceFilerColumnOrder", "Verify changed position of priority column.");
                officeTickets_AllTicketsHelper.IsElementPresent("HeadPriority6");
                officeTickets_AllTicketsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTicketsAdvanceFilerColumnOrder", "Logout from the application.");
                VisitCorp("logout");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyTicketsAdvanceFilerColumnOrder");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Tickets Advance Filer Column Order");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Tickets Advance Filer Column Order", "Bug", "Medium", "Activities page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Tickets Advance Filer Column Order");
                        TakeScreenshot("VerifyTicketsAdvanceFilerColumnOrder");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyTicketsAdvanceFilerColumnOrder.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyTicketsAdvanceFilerColumnOrder");
                        string id = loginHelper.getIssueID("Verify Tickets Advance Filer Column Order");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyTicketsAdvanceFilerColumnOrder.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Tickets Advance Filer Column Order"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Tickets Advance Filer Column Order");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyTicketsAdvanceFilerColumnOrder");
                executionLog.WriteInExcel("Verify Tickets Advance Filer Column Order", Status, JIRA, "Meetings Management");
            }
        }
    }
}