using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class OpportunitiesAdvanceFilterResultsPP : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin1")]
        [TestCategory("TS6")]
        [TestCategory("Pegasus_Admin1")]
        public void opportunitiesAdvanceFilterResultsPP()
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
            var DocName = "Test Exe" + GetRandomNumber();
            var fileUpl = GetPathToFile() + "chrome.exe";
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("OpportunitiesAdvanceFilterResultsPP", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("OpportunitiesAdvanceFilterResultsPP", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("OpportunitiesAdvanceFilterResultsPP", "Redirect To URL");
                VisitOffice("opportunities");
                office_OpportunitiesHelper.WaitForWorkAround(3000);

                executionLog.Log("OpportunitiesAdvanceFilterResultsPP", "Verify page title.");
                VerifyTitle("Opportunities");

                executionLog.Log("OpportunitiesAdvanceFilterResultsPP", "Click on advance filter.");
                office_OpportunitiesHelper.ClickElement("AdvanceFilter");
                office_OpportunitiesHelper.WaitForWorkAround(2000);

                executionLog.Log("OpportunitiesAdvanceFilterResultsPP", "Select number of records to 10.");
                office_OpportunitiesHelper.SelectByText("ResultsPerPage", "10");
                //office_OpportunitiesHelper.WaitForWorkAround(3000);

                executionLog.Log("OpportunitiesAdvanceFilterResultsPP", "Click on apply button.");
                office_OpportunitiesHelper.ClickElement("Apply");
                office_OpportunitiesHelper.WaitForWorkAround(3000);
                //office_OpportunitiesHelper.WaitForElementPresent("No.ofResults", 10);

                executionLog.Log("OpportunitiesAdvanceFilterResultsPP", "Verify number of records displayed.");
                office_OpportunitiesHelper.ShowResult(10);
                // office_OpportunitiesHelper.VerifyText("No.ofResults", "Showing 1 - 10 of");
                //office_OpportunitiesHelper.WaitForWorkAround(3000);

                executionLog.Log("OpportunitiesAdvanceFilterResultsPP", "Click on advance filter.");
                office_OpportunitiesHelper.ClickElement("AdvanceFilter");
                office_OpportunitiesHelper.WaitForWorkAround(2000);

                executionLog.Log("OpportunitiesAdvanceFilterResultsPP", "Select number of records to 20.");
                office_OpportunitiesHelper.SelectByText("ResultsPerPage", "20");
                //office_OpportunitiesHelper.WaitForWorkAround(3000);

                executionLog.Log("OpportunitiesAdvanceFilterResultsPP", "Click on apply button.");
                office_OpportunitiesHelper.ClickElement("Apply");
                office_OpportunitiesHelper.WaitForWorkAround(3000);
                //office_OpportunitiesHelper.WaitForElementPresent("No.ofResults", 10);

                executionLog.Log("OpportunitiesAdvanceFilterResultsPP", "Verify number of records displayed.");
                office_OpportunitiesHelper.ShowResult(20);
                // office_OpportunitiesHelper.VerifyText("No.ofResults", "Showing 1 - 20 of");
                //office_OpportunitiesHelper.WaitForWorkAround(3000);

                executionLog.Log("OpportunitiesAdvanceFilterResultsPP", "Click on advance filter.");
                office_OpportunitiesHelper.ClickElement("AdvanceFilter");
                office_OpportunitiesHelper.WaitForWorkAround(2000);

                executionLog.Log("OpportunitiesAdvanceFilterResultsPP", "Select number of records to 50.");
                office_OpportunitiesHelper.SelectByText("ResultsPerPage", "50");
                //office_OpportunitiesHelper.WaitForWorkAround(3000);

                executionLog.Log("OpportunitiesAdvanceFilterResultsPP", "Click on apply button.");
                office_OpportunitiesHelper.ClickElement("Apply");
                office_OpportunitiesHelper.WaitForWorkAround(3000);
                //office_OpportunitiesHelper.WaitForElementPresent("No.ofResults", 10);

                executionLog.Log("OpportunitiesAdvanceFilterResultsPP", "Verify number of records displayed.");
                office_OpportunitiesHelper.ShowResult(50);
                //office_OpportunitiesHelper.VerifyText("No.ofResults", "Showing 1 - 50 of");
                //office_OpportunitiesHelper.WaitForWorkAround(3000);

                executionLog.Log("OpportunitiesAdvanceFilterResultsPP", "Click on advance filter.");
                office_OpportunitiesHelper.ClickElement("AdvanceFilter");
                office_OpportunitiesHelper.WaitForWorkAround(2000);

                executionLog.Log("OpportunitiesAdvanceFilterResultsPP", "Select number of records to 100.");
                office_OpportunitiesHelper.SelectByText("ResultsPerPage", "100");
                //office_OpportunitiesHelper.WaitForWorkAround(3000);

                executionLog.Log("OpportunitiesAdvanceFilterResultsPP", "Click on apply button.");
                office_OpportunitiesHelper.ClickElement("Apply");
                office_OpportunitiesHelper.WaitForWorkAround(3000);
                //office_OpportunitiesHelper.WaitForElementPresent("No.ofResults", 10);

                executionLog.Log("OpportunitiesAdvanceFilterResultsPP", "Verify number of records displayed.");
                office_OpportunitiesHelper.ShowResult(100);
                // office_OpportunitiesHelper.VerifyText("No.ofResults", "Showing 1 - 100 of");
                //office_OpportunitiesHelper.WaitForWorkAround(3000);

                executionLog.Log("OpportunitiesAdvanceFilterResultsPP", "Logout from the application.");
                VisitOffice("logout");

            }

            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("OpportunitiesAdvanceFilterResultsPP");
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
                        TakeScreenshot("OpportunitiesAdvanceFilterResultsPP");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\OpportunitiesAdvanceFilterResultsPP.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("OpportunitiesAdvanceFilterResultsPP");
                        string id = loginHelper.getIssueID("Opportunities Advance Filter ResultsPP");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\OpportunitiesAdvanceFilterResultsPP.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Opportunities Advance Filter ResultsPP"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Opportunities Advance Filter ResultsPP");
         //       executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("OpportunitiesAdvanceFilterResultsPP");
                executionLog.WriteInExcel("Opportunities Advance Filter ResultsPP", Status, JIRA, "Opportunities Management");
            }
        }
    }
}