using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class VerifyIconAndLabelForLeads : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin1")]
        [TestCategory("TS7")]
        [TestCategory("Pegasus_Admin1")]
        public void verifyIconAndLabelForLeads()
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
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("VerifyIconAndLabelForLeads", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyIconAndLabelForLeads", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("VerifyIconAndLabelForLeads", "Visit Leads page.");
                VisitOffice("leads");

                executionLog.Log("VerifyIconAndLabelForLeads", "Verify page title.");
                VerifyTitle("Leads");

                executionLog.Log("VerifyIconAndLabelForLeads", "Verify page title.");
                VerifyTitle("Leads");
                office_LeadsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyIconAndLabelForLeads", "Verify delete icon available.");
                office_LeadsHelper.IsElementPresent("DeleteIcon");
                office_LeadsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyIconAndLabelForLeads", "Verify delete label available.");
                office_LeadsHelper.IsElementPresent("DeleteLabel");
                office_LeadsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyIconAndLabelForLeads", "Click on delete button.");
                office_LeadsHelper.ClickElement("DeleteLabel");
                office_LeadsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyIconAndLabelForLeads", "Decline alert message.");
                office_LeadsHelper.DeclineAlert();
                office_LeadsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyIconAndLabelForLeads", "Verify bulk update icon.");
                office_LeadsHelper.IsElementPresent("BUlkUpdateIcon");
                office_LeadsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyIconAndLabelForLeads", "Verify bulk update label.");
                office_LeadsHelper.IsElementPresent("BulkUpdateLabel");
                office_LeadsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyIconAndLabelForLeads", "Click on bulk update button.");
                office_LeadsHelper.ClickForce("BulkUpdateLabel");
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyIconAndLabelForLeads", "Click on sales manager.");
                office_LeadsHelper.ClickElement("ChangeSaleManager");
                office_LeadsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyIconAndLabelForLeads", "Verify alert text on the page.");
                office_LeadsHelper.VerifyAlertText("Please select atleast one record to proceed.");

                executionLog.Log("VerifyIconAndLabelForLeads", "Accept alert message.");
                office_LeadsHelper.AcceptAlert();
                office_LeadsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyIconAndLabelForLeads", "Verify advance filter icon.");
                office_LeadsHelper.IsElementPresent("AdvanceFilIcon");
                office_LeadsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyIconAndLabelForLeads", "Verify advanced filter label.");
                office_LeadsHelper.IsElementPresent("AdvancefiltLabel");
                office_LeadsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyIconAndLabelForLeads", "Verify page text.");
                office_LeadsHelper.VerifyPageText("Tracking Fields");
                office_LeadsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyIconAndLabelForLeads", "Verify create leads icon.");
                office_LeadsHelper.IsElementPresent("CreateIcon");
                office_LeadsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyIconAndLabelForLeads", "Verify create leads label.");
                office_LeadsHelper.IsElementPresent("CreateLabel");
                office_LeadsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyIconAndLabelForLeads", "Click on create button.");
                office_LeadsHelper.ClickElement("CreateLabel");
                office_LeadsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyIconAndLabelForLeads", "Verify page title as create leads.");
                VerifyTitle("Create a Lead");

                executionLog.Log("VerifyIconAndLabelForLeads", "Visit opportunitities page.");
                VisitOffice("leads");

                executionLog.Log("VerifyIconAndLabelForLeads", "Verify page title");
                VerifyTitle("Leads");

                executionLog.Log("VerifyIconAndLabelForLeads", "Verify import icon present.");
                office_LeadsHelper.IsElementPresent("ImportIcon");
                office_LeadsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyIconAndLabelForLeads", "Verify import label present.");
                office_LeadsHelper.IsElementPresent("ImportLabel");
                office_LeadsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyIconAndLabelForLeads", "Click on import button.");
                office_LeadsHelper.ClickElement("ImportLabel");
                office_LeadsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyIconAndLabelForLeads", "Verify browse button present on page.");
                office_LeadsHelper.IsElementPresent("BrowseFile");
                office_LeadsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyIconAndLabelForLeads", "Visit opportunitities page.");
                VisitOffice("leads");

                executionLog.Log("VerifyIconAndLabelForLeads", "Verify page title");
                VerifyTitle("Leads");

                executionLog.Log("VerifyIconAndLabelForLeads", "Verify import icon present.");
                office_LeadsHelper.IsElementPresent("VcardIcon");
                office_LeadsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyIconAndLabelForLeads", "Verify import label present.");
                office_LeadsHelper.IsElementPresent("VcardLabel");
                office_LeadsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyIconAndLabelForLeads", "Click on import button.");
                office_LeadsHelper.ClickForce("VcardLabel");
                office_LeadsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyIconAndLabelForLeads", "Verify browse button present on page.");
                office_LeadsHelper.IsElementPresent("BrowseFile");
                office_LeadsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyIconAndLabelForLeads", "Visit Leads page.");
                VisitOffice("leads");

                executionLog.Log("VerifyIconAndLabelForLeads", "Verify page title..");
                VerifyTitle("Leads");

                executionLog.Log("VerifyIconAndLabelForLeads", "Verify merge icon present on page.");
                office_LeadsHelper.IsElementPresent("MergeIcon");
                office_LeadsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyIconAndLabelForLeads", "Verify merge label present on page.");
                office_LeadsHelper.IsElementPresent("MergeLabel");
                office_LeadsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyIconAndLabelForLeads", "Click on merge button.");
                office_LeadsHelper.ClickElement("MergeLabel");
                office_LeadsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyIconAndLabelForLeads", "Verify alert text on the page.");
                office_LeadsHelper.VerifyAlertText("Please select 2 or more leads you wish to merge");

                executionLog.Log("VerifyIconAndLabelForLeads", "Accept alert message.");
                office_LeadsHelper.AcceptAlert();
                office_LeadsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyIconAndLabelForLeads", "Verify export icon present on page.");
                office_LeadsHelper.IsElementPresent("ExportIcon");
                office_LeadsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyIconAndLabelForLeads", "Click on Export Icon");
                office_LeadsHelper.ClickJS("ExportIcon");
                office_LeadsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyIconAndLabelForLeads", "Verify the export label present");
                office_LeadsHelper.IsElementPresent("ExportLabel");
                office_LeadsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyIconAndLabelForLeads", "Verify export as csv present on page.");
                office_LeadsHelper.IsElementPresent("ExportCsv");
                office_LeadsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyIconAndLabelForLeads", "Verify the ");
                office_LeadsHelper.IsElementPresent("ExportExcel");
                office_LeadsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyIconAndLabelForLeads", "Click on export label present");
                office_LeadsHelper.ClickElement("ExportLabel");

                executionLog.Log("VerifyIconAndLabelForLeads", "Verify recycle bin icon on the page.");
                office_LeadsHelper.IsElementPresent("RecycleBinIcon");
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyIconAndLabelForLeads", "Click on recycle bin Icon");
                office_LeadsHelper.ClickJS("RecycleBinIcon");
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyIconAndLabelForLeads", "Verify page title as recycled Leads.");
                office_LeadsHelper.VerifyPageText("Recycled Leads");
                office_LeadsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyIconAndLabelForLeads", "Visit Leads page.");
                VisitOffice("leads");
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyIconAndLabelForLeads", "Verify page title..");
                VerifyTitle("Leads");

                executionLog.Log("VerifyIconAndLabelForLeads", "Verify all Leads button present on page.");
                office_LeadsHelper.IsElementPresent("AllLeads");
                office_LeadsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyIconAndLabelForLeads", "Verify my Leads button present on page.");
                office_LeadsHelper.IsElementPresent("MyLeads");
                office_LeadsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyIconAndLabelForLeads", "Verify my team's Leads button present on page.");
                office_LeadsHelper.IsElementPresent("MyTeamLeads");
                office_LeadsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyIconAndLabelForLeads", "Verify my saved filters button present on page.");
                office_LeadsHelper.IsElementPresent("MySavedFilters");
                office_LeadsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyIconAndLabelForLeads", "Logout from the application.");
                VisitOffice("logout");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyIconAndLabelForLeads");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Icon And Label For Leads");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Icon And Label For Leads", "Bug", "Medium", "Leads page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Icon And Label For Leads");
                        TakeScreenshot("VerifyIconAndLabelForLeads");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyIconAndLabelForLeads.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyIconAndLabelForLeads");
                        string id = loginHelper.getIssueID("Verify Icon And Label For Leads");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyIconAndLabelForLeads.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Icon And Label For Leads"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Icon And Label For Leads");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyIconAndLabelForLeads");
                executionLog.WriteInExcel("Verify Icon And Label For Leads", Status, JIRA, "Leads Management");
            }
        }
    }
}