using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class CallsAdvanceFilterResultsPP : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS6")]
        [TestCategory("Pegasus_Admin")]
        public void callsAdvanceFilterResultsPP()
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

            // Variable
            var DocName = "Test Exe" + GetRandomNumber();
            var fileUpl = GetPathToFile() + "chrome.exe";
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("CallsAdvanceFilterResultsPP", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("CallsAdvanceFilterResultsPP", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("CallsAdvanceFilterResultsPP", "Redirect at employee page.");
                VisitOffice("calls");
                officeActivities_CallsHelper.WaitForWorkAround(3000);

                executionLog.Log("CallsAdvanceFilterResultsPP", "Verify page title.");
                VerifyTitle("Calls");
                //officeActivities_CallsHelper.WaitForElementVisible("AdvanceFilter", 10);

                executionLog.Log("CallsAdvanceFilterResultsPP", "Click on advance filter.");
                officeActivities_CallsHelper.ClickElement("AdvanceFilter");
                officeActivities_CallsHelper.WaitForWorkAround(2000);

                executionLog.Log("CallsAdvanceFilterResultsPP", "Select number of records to 10.");
                officeActivities_CallsHelper.SelectByText("ResultsPerPage", "10");
                //officeActivities_CallsHelper.WaitForWorkAround(3000);

                executionLog.Log("CallsAdvanceFilterResultsPP", "Click on Apply button.");
                officeActivities_CallsHelper.ClickElement("Apply");
                officeActivities_CallsHelper.WaitForWorkAround(3000);

                executionLog.Log("CallsAdvanceFilterResultsPP", "Verify number of records displayed.");
                officeActivities_CallsHelper.VerifyText("No.ofRecords", "Showing 1 - 10 of");
                //officeActivities_CallsHelper.WaitForWorkAround(3000);

                executionLog.Log("CallsAdvanceFilterResultsPP", "Click on advance filter.");
                officeActivities_CallsHelper.ClickElement("AdvanceFilter");
                officeActivities_CallsHelper.WaitForWorkAround(2000);

                executionLog.Log("CallsAdvanceFilterResultsPP", "Select number of records to 20.");
                officeActivities_CallsHelper.SelectByText("ResultsPerPage", "20");
                //officeActivities_CallsHelper.WaitForWorkAround(3000);

                executionLog.Log("CallsAdvanceFilterResultsPP", "Click on Apply button.");
                officeActivities_CallsHelper.ClickElement("Apply");
                officeActivities_CallsHelper.WaitForWorkAround(3000);

                executionLog.Log("CallsAdvanceFilterResultsPP", "Verify number of records displayed.");
                officeActivities_CallsHelper.VerifyText("No.ofRecords", "Showing 1 - 20 of");
                //officeActivities_CallsHelper.WaitForWorkAround(3000);

                executionLog.Log("CallsAdvanceFilterResultsPP", "Click on advance filter.");
                officeActivities_CallsHelper.ClickElement("AdvanceFilter");
                officeActivities_CallsHelper.WaitForWorkAround(2000);

                executionLog.Log("CallsAdvanceFilterResultsPP", "Select number of records to 50.");
                officeActivities_CallsHelper.SelectByText("ResultsPerPage", "50");
                //officeActivities_CallsHelper.WaitForWorkAround(3000);

                executionLog.Log("CallsAdvanceFilterResultsPP", "Click on Apply button.");
                officeActivities_CallsHelper.ClickElement("Apply");
                officeActivities_CallsHelper.WaitForWorkAround(3000);

                executionLog.Log("CallsAdvanceFilterResultsPP", "Verify number of records displayed.");
                officeActivities_CallsHelper.VerifyText("No.ofRecords", "Showing 1 - 50 of");
                //officeActivities_CallsHelper.WaitForWorkAround(3000);

                executionLog.Log("CallsAdvanceFilterResultsPP", "Click on advance filter.");
                officeActivities_CallsHelper.ClickElement("AdvanceFilter");
                officeActivities_CallsHelper.WaitForWorkAround(2000);

                executionLog.Log("CallsAdvanceFilterResultsPP", "Select number of records to 100.");
                officeActivities_CallsHelper.SelectByText("ResultsPerPage", "100");
                //officeActivities_CallsHelper.WaitForWorkAround(3000);

                executionLog.Log("CallsAdvanceFilterResultsPP", "Click on Apply button.");
                officeActivities_CallsHelper.ClickElement("Apply");
                officeActivities_CallsHelper.WaitForWorkAround(3000);

                executionLog.Log("CallsAdvanceFilterResultsPP", "Verify number of records displayed.");
                officeActivities_CallsHelper.VerifyText("No.ofRecords", "Showing 1 - 100 of");
                //officeActivities_CallsHelper.WaitForWorkAround(3000);

                executionLog.Log("CallsAdvanceFilterResultsPP", "Logout from the application.");
                VisitOffice("logout");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CallsAdvanceFilterResultsPP");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Documents Advance Filter ResultsPP");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Documents Advance Filter ResultsPP", "Bug", "Medium", "Opportunities page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Documents Advance Filter ResultsPP");
                        TakeScreenshot("CallsAdvanceFilterResultsPP");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CallsAdvanceFilterResultsPP.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CallsAdvanceFilterResultsPP");
                        string id = loginHelper.getIssueID("Documents Advance Filter ResultsPP");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CallsAdvanceFilterResultsPP.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Documents Advance Filter ResultsPP"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Documents Advance Filter ResultsPP");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("CallsAdvanceFilterResultsPP");
                executionLog.WriteInExcel("Documents Advance Filter ResultsPP", Status, JIRA, "Opportunities Management");
            }
        }
    }
}