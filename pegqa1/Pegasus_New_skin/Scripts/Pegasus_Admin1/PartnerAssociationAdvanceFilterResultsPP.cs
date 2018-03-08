using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class PartnerAssociationAdvanceFilterResultsPP : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin1")]
        [TestCategory("TS7")]
        [TestCategory("Pegasus_Admin1")]
        public void partnerAssociationAdvanceFilterResultsPP()
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

            // Variable
            var DocName = "Test Exe" + GetRandomNumber();
            var fileUpl = GetPathToFile() + "chrome.exe";
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("PartnerAssociationAdvanceFilterResultsPP", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("PartnerAssociationAdvanceFilterResultsPP", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("PartnerAssociationAdvanceFilterResultsPP", "Redirect at employee page.");
                VisitOffice("partners/associations");
                agents_PartnerAssociationHelper.WaitForWorkAround(5000);

                executionLog.Log("PartnerAssociationAdvanceFilterResultsPP", "Verify page title.");
                VerifyTitle("Partner Associations");

                executionLog.Log("PartnerAssociationAdvanceFilterResultsPP", "Click on advance filter.");
                agents_PartnerAssociationHelper.ClickElement("AdvanceFilter");
                agents_PartnerAssociationHelper.WaitForWorkAround(3000);

                executionLog.Log("PartnerAssociationAdvanceFilterResultsPP", "Select number of records to 10.");
                agents_PartnerAssociationHelper.SelectByText("ResultsPerPage", "10");
                agents_PartnerAssociationHelper.WaitForWorkAround(3000);

                executionLog.Log("PartnerAssociationAdvanceFilterResultsPP", "Click on Apply button.");
                agents_PartnerAssociationHelper.ClickElement("ApplyButton");
                agents_PartnerAssociationHelper.WaitForWorkAround(3000);

                executionLog.Log("PartnerAssociationAdvanceFilterResultsPP", "Verify number of records displayed.");
                agents_PartnerAssociationHelper.ShowResult(10);
                //agents_PartnerAssociationHelper.VerifyText("BottomResults", "Showing 1 - 10 of");
                agents_PartnerAssociationHelper.WaitForWorkAround(3000);

                executionLog.Log("PartnerAssociationAdvanceFilterResultsPP", "Click on advance filter.");
                agents_PartnerAssociationHelper.ClickElement("AdvanceFilter");
                agents_PartnerAssociationHelper.WaitForWorkAround(3000);

                executionLog.Log("PartnerAssociationAdvanceFilterResultsPP", "Select number of records to 20.");
                agents_PartnerAssociationHelper.SelectByText("ResultsPerPage", "20");
                agents_PartnerAssociationHelper.WaitForWorkAround(3000);

                executionLog.Log("PartnerAssociationAdvanceFilterResultsPP", "Click on Apply button.");
                agents_PartnerAssociationHelper.ClickElement("ApplyButton");
                agents_PartnerAssociationHelper.WaitForWorkAround(3000);

                executionLog.Log("PartnerAssociationAdvanceFilterResultsPP", "Verify number of records displayed.");
                agents_PartnerAssociationHelper.ShowResult(20);
                //agents_PartnerAssociationHelper.VerifyText("BottomResults", "Showing 1 - 20 of");
                agents_PartnerAssociationHelper.WaitForWorkAround(3000);

                executionLog.Log("PartnerAssociationAdvanceFilterResultsPP", "Click on advance filter.");
                agents_PartnerAssociationHelper.ClickElement("AdvanceFilter");
                agents_PartnerAssociationHelper.WaitForWorkAround(3000);

                executionLog.Log("PartnerAssociationAdvanceFilterResultsPP", "Select number of records to 50.");
                agents_PartnerAssociationHelper.SelectByText("ResultsPerPage", "50");
                agents_PartnerAssociationHelper.WaitForWorkAround(3000);

                executionLog.Log("PartnerAssociationAdvanceFilterResultsPP", "Click on ApplyButton button.");
                agents_PartnerAssociationHelper.ClickElement("ApplyButton");
                agents_PartnerAssociationHelper.WaitForWorkAround(3000);

                executionLog.Log("PartnerAssociationAdvanceFilterResultsPP", "Verify number of records displayed.");
                agents_PartnerAssociationHelper.ShowResult(50);
                //agents_PartnerAssociationHelper.VerifyText("BottomResults", "Showing 1 - 50 of");
                agents_PartnerAssociationHelper.WaitForWorkAround(3000);

                executionLog.Log("PartnerAssociationAdvanceFilterResultsPP", "Click on advance filter.");
                agents_PartnerAssociationHelper.ClickElement("AdvanceFilter");
                agents_PartnerAssociationHelper.WaitForWorkAround(3000);

                executionLog.Log("PartnerAssociationAdvanceFilterResultsPP", "Select number of records to 100.");
                agents_PartnerAssociationHelper.SelectByText("ResultsPerPage", "100");
                agents_PartnerAssociationHelper.WaitForWorkAround(3000);

                executionLog.Log("PartnerAssociationAdvanceFilterResultsPP", "Click on ApplyButton button.");
                agents_PartnerAssociationHelper.ClickElement("ApplyButton");
                agents_PartnerAssociationHelper.WaitForWorkAround(3000);

                executionLog.Log("PartnerAssociationAdvanceFilterResultsPP", "Verify number of records displayed.");
                agents_PartnerAssociationHelper.ShowResult(100);
                //agents_PartnerAssociationHelper.VerifyText("BottomResults", "Showing 1 - 100 of");
                agents_PartnerAssociationHelper.WaitForWorkAround(3000);

                executionLog.Log("PartnerAssociationAdvanceFilterResultsPP", "Logout from the application.");
                VisitOffice("logout");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("PartnerAssociationAdvanceFilterResultsPP");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Partner Association Advance Filter ResultsPP");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Partner Association Advance Filter ResultsPP", "Bug", "Medium", "Opportunities page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Partner Association Advance Filter ResultsPP");
                        TakeScreenshot("PartnerAssociationAdvanceFilterResultsPP");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\PartnerAssociationAdvanceFilterResultsPP.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("PartnerAssociationAdvanceFilterResultsPP");
                        string id = loginHelper.getIssueID("Partner Association Advance Filter ResultsPP");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\PartnerAssociationAdvanceFilterResultsPP.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Partner Association Advance Filter ResultsPP"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Partner Association Advance Filter ResultsPP");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("PartnerAssociationAdvanceFilterResultsPP");
                executionLog.WriteInExcel("Partner Association Advance Filter ResultsPP", Status, JIRA, "Opportunities Management");
            }
        }
    }
}