using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class DocumentsAdvanceFilterResultsPP : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("Fail")]
        [TestCategory("TS6")]
        [TestCategory("Pegasus_Admin")]
        public void documentsAdvanceFilterResultsPP()
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
            var officeActivities_DocumentHelper = new OfficeActivities_DocumentHelper(GetWebDriver());

            // Variable
            var DocName = "Test Exe" + GetRandomNumber();
            var fileUpl = GetPathToFile() + "chrome.exe";
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("DocumentsAdvanceFilterResultsPP", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("DocumentsAdvanceFilterResultsPP", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("DocumentsAdvanceFilterResultsPP", "Redirect at employee page.");
                VisitOffice("documents");
                officeActivities_DocumentHelper.WaitForWorkAround(3000);

                executionLog.Log("DocumentsAdvanceFilterResultsPP", "Verify page title.");
                VerifyTitle("Documents");
                //officeActivities_DocumentHelper.WaitForElementVisible("AdvanceFilter", 20);

                executionLog.Log("DocumentsAdvanceFilterResultsPP", "Click on advance filter.");
                officeActivities_DocumentHelper.ClickElement("AdvanceFilter");
                officeActivities_DocumentHelper.WaitForWorkAround(2000);

                executionLog.Log("DocumentsAdvanceFilterResultsPP", "Select number of records to 10.");
                officeActivities_DocumentHelper.SelectByText("ResultsPerPage", "10");
                //officeActivities_DocumentHelper.WaitForWorkAround(3000);

                executionLog.Log("DocumentsAdvanceFilterResultsPP", "Click on Apply button.");
                officeActivities_DocumentHelper.ClickElement("Apply");
                officeActivities_DocumentHelper.WaitForWorkAround(3000);

                executionLog.Log("DocumentsAdvanceFilterResultsPP", "Verify number of records displayed.");
                officeActivities_DocumentHelper.ShowResult(30);
                //officeActivities_DocumentHelper.WaitForWorkAround(3000);

                executionLog.Log("DocumentsAdvanceFilterResultsPP", "Click on advance filter.");
                officeActivities_DocumentHelper.ClickElement("AdvanceFilter");
                officeActivities_DocumentHelper.WaitForWorkAround(2000);

                executionLog.Log("DocumentsAdvanceFilterResultsPP", "Select number of records to 20.");
                officeActivities_DocumentHelper.SelectByText("ResultsPerPage", "20");
                //officeActivities_DocumentHelper.WaitForWorkAround(4000);

                executionLog.Log("DocumentsAdvanceFilterResultsPP", "Click on Apply button.");
                officeActivities_DocumentHelper.ClickElement("Apply");
                officeActivities_DocumentHelper.WaitForWorkAround(3000);

                executionLog.Log("DocumentsAdvanceFilterResultsPP", "Verify number of records displayed.");
                officeActivities_DocumentHelper.ShowResult(50);
                //officeActivities_DocumentHelper.WaitForWorkAround(4000);

                executionLog.Log("DocumentsAdvanceFilterResultsPP", "Click on advance filter.");
                officeActivities_DocumentHelper.ClickElement("AdvanceFilter");
                officeActivities_DocumentHelper.WaitForWorkAround(2000);

                executionLog.Log("DocumentsAdvanceFilterResultsPP", "Select number of records to 50.");
                officeActivities_DocumentHelper.SelectByText("ResultsPerPage", "50");
                //officeActivities_DocumentHelper.WaitForWorkAround(4000);

                executionLog.Log("DocumentsAdvanceFilterResultsPP", "Click on Apply button.");
                officeActivities_DocumentHelper.ClickElement("Apply");
                officeActivities_DocumentHelper.WaitForWorkAround(3000);

                executionLog.Log("DocumentsAdvanceFilterResultsPP", "Verify number of records displayed.");
                officeActivities_DocumentHelper.ShowResult(40);
                //officeActivities_DocumentHelper.WaitForWorkAround(4000);

                executionLog.Log("DocumentsAdvanceFilterResultsPP", "Click on advance filter.");
                officeActivities_DocumentHelper.ClickElement("AdvanceFilter");
                officeActivities_DocumentHelper.WaitForWorkAround(2000);

                executionLog.Log("DocumentsAdvanceFilterResultsPP", "Select number of records to 100.");
                officeActivities_DocumentHelper.SelectByText("ResultsPerPage", "100");
                //officeActivities_DocumentHelper.WaitForWorkAround(4000);

                executionLog.Log("DocumentsAdvanceFilterResultsPP", "Click on Apply button.");
                officeActivities_DocumentHelper.ClickElement("Apply");
                officeActivities_DocumentHelper.WaitForWorkAround(3000);

                executionLog.Log("DocumentsAdvanceFilterResultsPP", "Verify number of records displayed.");
                officeActivities_DocumentHelper.ShowResult(100);
                //officeActivities_DocumentHelper.WaitForWorkAround(4000);

                executionLog.Log("DocumentsAdvanceFilterResultsPP", "Logout from the application.");
                VisitOffice("logout");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("DocumentsAdvanceFilterResultsPP");
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
                        TakeScreenshot("DocumentsAdvanceFilterResultsPP");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\DocumentsAdvanceFilterResultsPP.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("DocumentsAdvanceFilterResultsPP");
                        string id = loginHelper.getIssueID("Documents Advance Filter ResultsPP");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\DocumentsAdvanceFilterResultsPP.png";
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
                executionLog.DeleteFile("DocumentsAdvanceFilterResultsPP");
                executionLog.WriteInExcel("Documents Advance Filter ResultsPP", Status, JIRA, "Opportunities Management");
            }
        }
    }
}