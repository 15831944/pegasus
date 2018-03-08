using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class PartnerAgentsAdvanceFilterResultsPP : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin1")]
        [TestCategory("TS7")]
        [TestCategory("Pegasus_Admin1")]
        public void partnerAgentsAdvanceFilterResultsPP()
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

            // Variable
            var DocName = "Test Exe" + GetRandomNumber();
            var fileUpl = GetPathToFile() + "chrome.exe";
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("PartnerAgentsAdvanceFilterResultsPP", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("PartnerAgentsAdvanceFilterResultsPP", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("PartnerAgentsAdvanceFilterResultsPP", "Redirect at employee page.");
                VisitOffice("partners/agents");
                agents_PartnerAgentsHelper.WaitForWorkAround(5000);

                executionLog.Log("PartnerAgentsAdvanceFilterResultsPP", "Verify page title.");
                VerifyTitle("Partner Agents");

                executionLog.Log("PartnerAgentsAdvanceFilterResultsPP", "Click on advance filter.");
                agents_PartnerAgentsHelper.ClickElement("AdvanceFilter");
                agents_PartnerAgentsHelper.WaitForWorkAround(3000);

                executionLog.Log("PartnerAgentsAdvanceFilterResultsPP", "Select number of records to 10.");
                agents_PartnerAgentsHelper.SelectByText("ResultsPerPage", "10");
                agents_PartnerAgentsHelper.WaitForWorkAround(3000);

                executionLog.Log("PartnerAgentsAdvanceFilterResultsPP", "Click on Apply button.");
                agents_PartnerAgentsHelper.ClickElement("ApplyButton");
                agents_PartnerAgentsHelper.WaitForWorkAround(3000);

                executionLog.Log("PartnerAgentsAdvanceFilterResultsPP", "Verify number of records displayed.");
                agents_PartnerAgentsHelper.ShowResult(10);
                //agents_PartnerAgentsHelper.VerifyText("BottomResults", "Showing 1 - 10 of ");
                agents_PartnerAgentsHelper.WaitForWorkAround(3000);

                executionLog.Log("PartnerAgentsAdvanceFilterResultsPP", "Click on advance filter.");
                agents_PartnerAgentsHelper.ClickElement("AdvanceFilter");
                agents_PartnerAgentsHelper.WaitForWorkAround(3000);

                executionLog.Log("PartnerAgentsAdvanceFilterResultsPP", "Select number of records to 20.");
                agents_PartnerAgentsHelper.SelectByText("ResultsPerPage", "20");
                agents_PartnerAgentsHelper.WaitForWorkAround(3000);

                executionLog.Log("PartnerAgentsAdvanceFilterResultsPP", "Click on Apply button.");
                agents_PartnerAgentsHelper.ClickElement("ApplyButton");
                agents_PartnerAgentsHelper.WaitForWorkAround(3000);

                executionLog.Log("PartnerAgentsAdvanceFilterResultsPP", "Verify number of records displayed.");
                agents_PartnerAgentsHelper.ShowResult(20);
                //agents_PartnerAgentsHelper.VerifyText("BottomResults", "Showing 1 - 20 of ");
                agents_PartnerAgentsHelper.WaitForWorkAround(3000);

                executionLog.Log("PartnerAgentsAdvanceFilterResultsPP", "Click on advance filter.");
                agents_PartnerAgentsHelper.ClickElement("AdvanceFilter");
                agents_PartnerAgentsHelper.WaitForWorkAround(3000);

                executionLog.Log("PartnerAgentsAdvanceFilterResultsPP", "Select number of records to 50.");
                agents_PartnerAgentsHelper.SelectByText("ResultsPerPage", "50");
                agents_PartnerAgentsHelper.WaitForWorkAround(3000);

                executionLog.Log("PartnerAgentsAdvanceFilterResultsPP", "Click on ApplyButton button.");
                agents_PartnerAgentsHelper.ClickElement("ApplyButton");
                agents_PartnerAgentsHelper.WaitForWorkAround(3000);

                executionLog.Log("PartnerAgentsAdvanceFilterResultsPP", "Verify number of records displayed.");
                agents_PartnerAgentsHelper.ShowResult(50);
                //agents_PartnerAgentsHelper.VerifyText("BottomResults", "Showing 1 - 50 of ");
                agents_PartnerAgentsHelper.WaitForWorkAround(3000);

                executionLog.Log("PartnerAgentsAdvanceFilterResultsPP", "Click on advance filter.");
                agents_PartnerAgentsHelper.ClickElement("AdvanceFilter");
                agents_PartnerAgentsHelper.WaitForWorkAround(3000);

                executionLog.Log("PartnerAgentsAdvanceFilterResultsPP", "Select number of records to 100.");
                agents_PartnerAgentsHelper.SelectByText("ResultsPerPage", "100");
                agents_PartnerAgentsHelper.WaitForWorkAround(3000);

                executionLog.Log("PartnerAgentsAdvanceFilterResultsPP", "Click on ApplyButton button.");
                agents_PartnerAgentsHelper.ClickElement("ApplyButton");
                agents_PartnerAgentsHelper.WaitForWorkAround(3000);

                executionLog.Log("PartnerAgentsAdvanceFilterResultsPP", "Verify number of records displayed.");
                agents_PartnerAgentsHelper.ShowResult(100);
                //agents_PartnerAgentsHelper.VerifyText("BottomResults", "Showing 1 - 100 of ");
                agents_PartnerAgentsHelper.WaitForWorkAround(3000);

                executionLog.Log("PartnerAgentsAdvanceFilterResultsPP", "Logout from the application.");
                VisitOffice("logout");

            }

            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("PartnerAgentsAdvanceFilterResultsPP");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Partner Agents Advance Filter ResultsPP");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Partner Agents Advance Filter ResultsPP", "Bug", "Medium", "Opportunities page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Partner Agents Advance Filter ResultsPP");
                        TakeScreenshot("PartnerAgentsAdvanceFilterResultsPP");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\PartnerAgentsAdvanceFilterResultsPP.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("PartnerAgentsAdvanceFilterResultsPP");
                        string id = loginHelper.getIssueID("Partner Agents Advance Filter ResultsPP");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\PartnerAgentsAdvanceFilterResultsPP.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Partner Agents Advance Filter ResultsPP"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Partner Agents Advance Filter ResultsPP");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("PartnerAgentsAdvanceFilterResultsPP");
                executionLog.WriteInExcel("Partner Agents Advance Filter ResultsPP", Status, JIRA, "Opportunities Management");
            }
        }
    }
}