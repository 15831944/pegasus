using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class LeadsAdvanceFilterResultsPP : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin1")]
        [TestCategory("TS6")]
        [TestCategory("Pegasus_Admin1")]
        public void leadsAdvanceFilterResultsPP()
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
            var office_LeadsHelper = new Office_LeadsHelper(GetWebDriver());

            // Variable
            var DocName = "Test Exe" + GetRandomNumber();
            var fileUpl = GetPathToFile() + "chrome.exe";
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("LeadsAdvanceFilterResultsPP", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("LeadsAdvanceFilterResultsPP", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("LeadsAdvanceFilterResultsPP", "Redirect To URL");
                VisitOffice("leads");
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("LeadsAdvanceFilterResultsPP", "Verify page title.");
                VerifyTitle("Leads");

                executionLog.Log("LeadsAdvanceFilterResultsPP", "Click on advance filter.");
                office_LeadsHelper.ClickElement("AdvanceFilter");
                office_LeadsHelper.WaitForWorkAround(2000);

                executionLog.Log("LeadsAdvanceFilterResultsPP", "Select number of records to 10.");
                office_LeadsHelper.SelectByText("ResultsPerPage", "10");
                //office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("LeadsAdvanceFilterResultsPP", "Click on apply button.");
                office_LeadsHelper.ClickElement("Apply");
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("LeadsAdvanceFilterResultsPP", "Verify number of records displayed.");
                office_LeadsHelper.VerifyText("No.ofResults", "Showing 1 - 10 of");
                //office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("LeadsAdvanceFilterResultsPP", "Click on advance filter.");
                office_LeadsHelper.ClickElement("AdvanceFilter");
                office_LeadsHelper.WaitForWorkAround(2000);

                executionLog.Log("LeadsAdvanceFilterResultsPP", "Select number of records to 20.");
                office_LeadsHelper.SelectByText("ResultsPerPage", "20");
                //office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("LeadsAdvanceFilterResultsPP", "Click on apply button.");
                office_LeadsHelper.ClickElement("Apply");
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("LeadsAdvanceFilterResultsPP", "Verify number of records displayed.");
                office_LeadsHelper.VerifyText("No.ofResults", "Showing 1 - 20 of");
                //office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("LeadsAdvanceFilterResultsPP", "Click on advance filter.");
                office_LeadsHelper.ClickElement("AdvanceFilter");
                office_LeadsHelper.WaitForWorkAround(2000);

                executionLog.Log("LeadsAdvanceFilterResultsPP", "Select number of records to 50.");
                office_LeadsHelper.SelectByText("ResultsPerPage", "50");
                //office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("LeadsAdvanceFilterResultsPP", "Click on apply button.");
                office_LeadsHelper.ClickElement("Apply");
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("LeadsAdvanceFilterResultsPP", "Verify number of records displayed.");
                office_LeadsHelper.VerifyText("No.ofResults", "Showing 1 - 50 of");
                //office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("LeadsAdvanceFilterResultsPP", "Click on advance filter.");
                office_LeadsHelper.ClickElement("AdvanceFilter");
                office_LeadsHelper.WaitForWorkAround(2000);

                executionLog.Log("LeadsAdvanceFilterResultsPP", "Select number of records to 100.");
                office_LeadsHelper.SelectByText("ResultsPerPage", "100");
                //office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("LeadsAdvanceFilterResultsPP", "Click on apply button.");
                office_LeadsHelper.ClickElement("Apply");
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("LeadsAdvanceFilterResultsPP", "Verify number of records displayed.");
                office_LeadsHelper.VerifyText("No.ofResults", "Showing 1 - 100 of");
                //office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("LeadsAdvanceFilterResultsPP", "Logout from the application.");
                VisitOffice("logout");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("LeadsAdvanceFilterResultsPP");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Clients Advance Filter ResultsPP");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Clients Advance Filter ResultsPP", "Bug", "Medium", "Opportunities page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Clients Advance Filter ResultsPP");
                        TakeScreenshot("LeadsAdvanceFilterResultsPP");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\LeadsAdvanceFilterResultsPP.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("LeadsAdvanceFilterResultsPP");
                        string id = loginHelper.getIssueID("Clients Advance Filter ResultsPP");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\LeadsAdvanceFilterResultsPP.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Clients Advance Filter ResultsPP"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Clients Advance Filter ResultsPP");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("LeadsAdvanceFilterResultsPP");
                executionLog.WriteInExcel("Clients Advance Filter ResultsPP", Status, JIRA, "Opportunities Management");
            }
        }
    }
}