using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class TicketsAdvanceFilterResultsPP : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin1")]
        [TestCategory("TS7")]
        [TestCategory("Pegasus_Admin1")]
        public void ticketsAdvanceFilterResultsPP()
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

            // Variable
            var DocName = "Test Exe" + GetRandomNumber();
            var fileUpl = GetPathToFile() + "chrome.exe";
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("TicketsAdvanceFilterResultsPP", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("TicketsAdvanceFilterResultsPP", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("TicketsAdvanceFilterResultsPP", "Redirect To URL");
                VisitOffice("tickets");
                officeTickets_AllTicketsHelper.WaitForWorkAround(5000);

                executionLog.Log("TicketsAdvanceFilterResultsPP", "Verify page title.");
                VerifyTitle("Tickets");

                executionLog.Log("TicketsAdvanceFilterResultsPP", "Click on advance filter.");
                officeTickets_AllTicketsHelper.ClickElement("Advance");
                officeTickets_AllTicketsHelper.WaitForWorkAround(3000);

                executionLog.Log("TicketsAdvanceFilterResultsPP", "Select number of records to 10.");
                officeTickets_AllTicketsHelper.SelectByText("ResultsPerp", "10");
                officeTickets_AllTicketsHelper.WaitForWorkAround(3000);

                executionLog.Log("TicketsAdvanceFilterResultsPP", "Click on apply button.");
                officeTickets_AllTicketsHelper.ClickElement("Apply");
                officeTickets_AllTicketsHelper.WaitForWorkAround(3000);
                officeTickets_AllTicketsHelper.WaitForElementPresent("BootomResults", 10);

                executionLog.Log("TicketsAdvanceFilterResultsPP", "Verify number of records displayed.");
                officeTickets_AllTicketsHelper.ShowResult(10);
                // officeTickets_AllTicketsHelper.VerifyText("BootomResults", "Showing 1 - 10 of");
                officeTickets_AllTicketsHelper.WaitForWorkAround(3000);

                executionLog.Log("TicketsAdvanceFilterResultsPP", "Click on advance filter.");
                officeTickets_AllTicketsHelper.ClickElement("Advance");
                officeTickets_AllTicketsHelper.WaitForWorkAround(3000);

                executionLog.Log("TicketsAdvanceFilterResultsPP", "Select number of records to 20.");
                officeTickets_AllTicketsHelper.SelectByText("ResultsPerp", "20");
                officeTickets_AllTicketsHelper.WaitForWorkAround(3000);

                executionLog.Log("TicketsAdvanceFilterResultsPP", "Click on apply button.");
                officeTickets_AllTicketsHelper.ClickElement("Apply");
                officeTickets_AllTicketsHelper.WaitForWorkAround(3000);
                officeTickets_AllTicketsHelper.WaitForElementPresent("BootomResults", 10);

                executionLog.Log("TicketsAdvanceFilterResultsPP", "Verify number of records displayed.");
                officeTickets_AllTicketsHelper.ShowResult(20);
                //officeTickets_AllTicketsHelper.VerifyText("BootomResults", "Showing 1 - 20 of");
                officeTickets_AllTicketsHelper.WaitForWorkAround(3000);

                executionLog.Log("TicketsAdvanceFilterResultsPP", "Click on advance filter.");
                officeTickets_AllTicketsHelper.ClickElement("Advance");
                officeTickets_AllTicketsHelper.WaitForWorkAround(3000);

                executionLog.Log("TicketsAdvanceFilterResultsPP", "Select number of records to 50.");
                officeTickets_AllTicketsHelper.SelectByText("ResultsPerp", "50");
                officeTickets_AllTicketsHelper.WaitForWorkAround(3000);

                executionLog.Log("TicketsAdvanceFilterResultsPP", "Click on apply button.");
                officeTickets_AllTicketsHelper.ClickElement("Apply");
                officeTickets_AllTicketsHelper.WaitForWorkAround(3000);
                officeTickets_AllTicketsHelper.WaitForElementPresent("BootomResults", 10);

                executionLog.Log("TicketsAdvanceFilterResultsPP", "Verify number of records displayed.");
                officeTickets_AllTicketsHelper.ShowResult(50);
                //officeTickets_AllTicketsHelper.VerifyText("BootomResults", "Showing 1 - 50 of");
                officeTickets_AllTicketsHelper.WaitForWorkAround(3000);

                executionLog.Log("TicketsAdvanceFilterResultsPP", "Click on advance filter.");
                officeTickets_AllTicketsHelper.ClickElement("Advance");
                officeTickets_AllTicketsHelper.WaitForWorkAround(3000);

                executionLog.Log("TicketsAdvanceFilterResultsPP", "Select number of records to 100.");
                officeTickets_AllTicketsHelper.SelectByText("ResultsPerp", "100");
                officeTickets_AllTicketsHelper.WaitForWorkAround(3000);

                executionLog.Log("TicketsAdvanceFilterResultsPP", "Click on apply button.");
                officeTickets_AllTicketsHelper.ClickElement("Apply");
                officeTickets_AllTicketsHelper.WaitForWorkAround(3000);
                officeTickets_AllTicketsHelper.WaitForElementPresent("BootomResults", 10);

                executionLog.Log("TicketsAdvanceFilterResultsPP", "Verify number of records displayed.");
                officeTickets_AllTicketsHelper.ShowResult(100);
                // officeTickets_AllTicketsHelper.VerifyText("BootomResults", "Showing 1 - 100 of");
                officeTickets_AllTicketsHelper.WaitForWorkAround(3000);

                executionLog.Log("TicketsAdvanceFilterResultsPP", "Logout from the application.");
                VisitOffice("logout");

            }

            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("TicketsAdvanceFilterResultsPP");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Tickets Advance Filter ResultsPP");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Tickets Advance Filter ResultsPP", "Bug", "Medium", "Opportunities page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Tickets Advance Filter ResultsPP");
                        TakeScreenshot("TicketsAdvanceFilterResultsPP");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\TicketsAdvanceFilterResultsPP.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("TicketsAdvanceFilterResultsPP");
                        string id = loginHelper.getIssueID("Tickets Advance Filter ResultsPP");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\TicketsAdvanceFilterResultsPP.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Tickets Advance Filter ResultsPP"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Tickets Advance Filter ResultsPP");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("TicketsAdvanceFilterResultsPP");
                executionLog.WriteInExcel("Tickets Advance Filter ResultsPP", Status, JIRA, "Opportunities Management");
            }
        }
    }
}