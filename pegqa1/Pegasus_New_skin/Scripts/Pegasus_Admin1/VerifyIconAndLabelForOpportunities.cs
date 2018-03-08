using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class VerifyIconAndLabelForOpportunities : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin1")]
        [TestCategory("TS7")]
        [TestCategory("Pegasus_Admin1")]
        public void verifyIconAndLabelForOpportunities()
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

                executionLog.Log("VerifyIconAndLabelForOpportunities", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyIconAndLabelForOpportunities", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("VerifyIconAndLabelForOpportunities", "Visit opportunities page.");
                VisitOffice("opportunities");
                office_OpportunitiesHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyIconAndLabelForOpportunities", "Verify page title.");
                VerifyTitle("Opportunities");
                //office_OpportunitiesHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyIconAndLabelForOpportunities", "Verify delete icon available.");
                office_OpportunitiesHelper.IsElementPresent("DeleteIcon");
                //office_OpportunitiesHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyIconAndLabelForOpportunities", "Verify delete label available.");
                office_OpportunitiesHelper.IsElementPresent("DeleteLabel");
                //office_OpportunitiesHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyIconAndLabelForOpportunities", "Click on delete button.");
                office_OpportunitiesHelper.ClickElement("DeleteLabel");
                office_OpportunitiesHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyIconAndLabelForOpportunities", "Decline alert message.");
                office_OpportunitiesHelper.DeclineAlert();
                //office_OpportunitiesHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyIconAndLabelForOpportunities", "Verify bulk update icon.");
                office_OpportunitiesHelper.IsElementPresent("BUlkUpdateIcon");
                //office_OpportunitiesHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyIconAndLabelForOpportunities", "Verify bulk update label.");
                office_OpportunitiesHelper.IsElementPresent("BulkUpdateLabel");
                //office_OpportunitiesHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyIconAndLabelForOpportunities", "Click on bulk update button.");
                office_OpportunitiesHelper.clickJS("BUlkUpdateIcon");
                office_OpportunitiesHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyIconAndLabelForOpportunities", "Click on sales manager.");
                office_OpportunitiesHelper.ClickElement("ChangeSaleManager");
                office_OpportunitiesHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyIconAndLabelForOpportunities", "Verify alert text on the page.");
                office_OpportunitiesHelper.VerifyAlertText("Please select atleast one record to proceed.");

                executionLog.Log("VerifyIconAndLabelForOpportunities", "Accept alert message.");
                office_OpportunitiesHelper.AcceptAlert();
                //office_OpportunitiesHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyIconAndLabelForOpportunities", "Verify advance filter icon.");
                office_OpportunitiesHelper.IsElementPresent("AdvanceFilIcon");
                //office_OpportunitiesHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyIconAndLabelForOpportunities", "Verify advanced filter label.");
                office_OpportunitiesHelper.IsElementPresent("AdvancefiltLabel");
                //office_OpportunitiesHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyIconAndLabelForOpportunities", "Verify page text.");
                office_OpportunitiesHelper.VerifyPageText("Tracking Fields");
                //office_OpportunitiesHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyIconAndLabelForOpportunities", "Verify create opportunity icon.");
                office_OpportunitiesHelper.IsElementPresent("CreateIcon");
                //office_OpportunitiesHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyIconAndLabelForOpportunities", "Verify create opportunity label.");
                office_OpportunitiesHelper.IsElementPresent("CreateLabel");
                //office_OpportunitiesHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyIconAndLabelForOpportunities", "Click on create button.");
                office_OpportunitiesHelper.ClickElement("CreateLabel");
                office_OpportunitiesHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyIconAndLabelForOpportunities", "Verify page title as create opportunity.");
                VerifyTitle("Create an Opportunity");

                executionLog.Log("VerifyIconAndLabelForOpportunities", "Visit opportunitities page.");
                VisitOffice("opportunities");
                office_OpportunitiesHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyIconAndLabelForOpportunities", "Verify page title");
                VerifyTitle("Opportunities");

                executionLog.Log("VerifyIconAndLabelForOpportunities", "Verify import icon present.");
                office_OpportunitiesHelper.IsElementPresent("ImportIcon");
                //office_OpportunitiesHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyIconAndLabelForOpportunities", "Verify import label present.");
                office_OpportunitiesHelper.IsElementPresent("ImportLabel");
                //office_OpportunitiesHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyIconAndLabelForOpportunities", "Click on import button.");
                office_OpportunitiesHelper.ClickElement("ImportLabel");
                office_OpportunitiesHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyIconAndLabelForOpportunities", "Verify browse button present on page.");
                office_OpportunitiesHelper.IsElementPresent("BrowseFile");
                //office_OpportunitiesHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyIconAndLabelForOpportunities", "Visit opportunities page.");
                VisitOffice("opportunities");
                office_OpportunitiesHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyIconAndLabelForOpportunities", "Verify page title..");
                VerifyTitle("Opportunities");

                executionLog.Log("VerifyIconAndLabelForOpportunities", "Verify merge icon present on page.");
                office_OpportunitiesHelper.IsElementPresent("MergeIcon");
                //office_OpportunitiesHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyIconAndLabelForOpportunities", "Verify merge label present on page.");
                office_OpportunitiesHelper.IsElementPresent("MergeLabel");
                //office_OpportunitiesHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyIconAndLabelForOpportunities", "Click on merge button.");
                office_OpportunitiesHelper.ClickElement("MergeLabel");
                office_OpportunitiesHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyIconAndLabelForOpportunities", "Verify alert text on the page..");
                office_OpportunitiesHelper.VerifyAlertText("Please select 2 or more opportunities you wish to merge");

                executionLog.Log("VerifyIconAndLabelForOpportunities", "Accept alert message.");
                office_OpportunitiesHelper.AcceptAlert();
                //office_OpportunitiesHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyIconAndLabelForOpportunities", "Verify export icon present on page.");
                office_OpportunitiesHelper.IsElementPresent("ExportIcon");
                //office_OpportunitiesHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyIconAndLabelForOpportunities", "Click on export button.");
                office_OpportunitiesHelper.clickJS("ExportIcon");
                office_OpportunitiesHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyIconAndLabelForOpportunities", "Verify export as csv present on page.");
                office_OpportunitiesHelper.IsElementPresent("ExportCsv");
                //office_OpportunitiesHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyIconAndLabelForOpportunities", "Verify recycle bin icon on the page.");
                office_OpportunitiesHelper.IsElementPresent("RecycleIcon");
                //office_OpportunitiesHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyIconAndLabelForOpportunities", "Click on recycle bin button.");
                office_OpportunitiesHelper.clickJS("RecycleIcon");
                office_OpportunitiesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyIconAndLabelForOpportunities", "Verify page title as recycled opportunities.");
                VerifyTitle("Opportunities");
                //office_OpportunitiesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyIconAndLabelForOpportunities", "Visit opportunities page.");
                VisitOffice("opportunities");
                office_OpportunitiesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyIconAndLabelForOpportunities", "Verify page title..");
                VerifyTitle("Opportunities");

                executionLog.Log("VerifyIconAndLabelForOpportunities", "Verify all opportunities button present on page.");
                office_OpportunitiesHelper.IsElementPresent("AllOpportunities");
                //office_OpportunitiesHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyIconAndLabelForOpportunities", "Verify my opportunities button present on page.");
                office_OpportunitiesHelper.IsElementPresent("MyOpportunities");
                //office_OpportunitiesHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyIconAndLabelForOpportunities", "Verify my team's opportunities button present on page.");
                office_OpportunitiesHelper.IsElementPresent("MyTeamOppor");
                //office_OpportunitiesHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyIconAndLabelForOpportunities", "Verify my saved filters button present on page.");
                office_OpportunitiesHelper.IsElementPresent("MySavedFilters");
                //office_OpportunitiesHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyIconAndLabelForOpportunities", "Logout from the application.");
                VisitOffice("logout");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyIconAndLabelForOpportunities");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Icon And Label For Opportunities");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Icon And Label For Opportunities", "Bug", "Medium", "Opportunities page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Icon And Label For Opportunities");
                        TakeScreenshot("VerifyIconAndLabelForOpportunities");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyIconAndLabelForOpportunities.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyIconAndLabelForOpportunities");
                        string id = loginHelper.getIssueID("Verify Icon And Label For Opportunities");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyIconAndLabelForOpportunities.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Icon And Label For Opportunities"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Icon And Label For Opportunities");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyIconAndLabelForOpportunities");
                executionLog.WriteInExcel("Verify Icon And Label For Opportunities", Status, JIRA, "Opportunities Management");
            }
        }
    }
}