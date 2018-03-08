using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class PartAgentLeadsCreditsIssue : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS2")]
        [TestCategory("BugTestNew")]
        public void partAgentLeadsCreditsIssue()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var office_LeadsHelper = new Office_LeadsHelper(GetWebDriver());
            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Variable
            var FName = "Test" + RandomNumber(99, 99999);
            var LName = "Test" + RandomNumber(99, 99999);
            var CDBA = "New" + RandomNumber(99, 99999);
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("PartAgentLeadsCreditsIssue", "Login with valid credential  Username");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: "+username[0]+" / "+password[0]);
                //office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("PartAgentLeadsCreditsIssue", "Redirect to Create Lead page.");
                VisitOffice("leads/create");
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("PartAgentLeadsCreditsIssue", "Verify page title.");
                VerifyTitle("Create a Lead");

                executionLog.Log("PartAgentLeadsCreditsIssue", "Enter First Name");
                office_LeadsHelper.TypeText("FirstNameLead", FName);

                executionLog.Log("PartAgentLeadsCreditsIssue", "Enter Last Name");
                office_LeadsHelper.TypeText("LastName", LName);

                executionLog.Log("PartAgentLeadsCreditsIssue", "Enter Company DBA");
                office_LeadsHelper.TypeText("CompanyName", CDBA);

                executionLog.Log("PartAgentLeadsCreditsIssue", "Select Status");
                office_LeadsHelper.SelectByText("LeadStatus", "New");

                executionLog.Log("PartAgentLeadsCreditsIssue", "Select lead Responsibility");
                office_LeadsHelper.SelectByText("Responsibility", "Howard Tang");

                executionLog.Log("PartAgentLeadsCreditsIssue", "Click on Save");
                office_LeadsHelper.ClickElement("SaveLeadNewSkin");

                executionLog.Log("PartAgentLeadsCreditsIssue", "Wait for creation success text.");
                office_LeadsHelper.WaitForText("Lead saved successfully.", 10);

                executionLog.Log("PartAgentLeadsCreditsIssue", "Verify Lead created by credits.");
                office_LeadsHelper.VerifyText("CreatedBy", "Howard Tang");

                executionLog.Log("PartAgentLeadsCreditsIssue", "Verify Lead modified by credits");
                office_LeadsHelper.VerifyText("ModifiedBy", "Howard Tang");

                VisitOffice("leads");
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("PartAgentLeadsCreditsIssue", "Select first lead");
                office_LeadsHelper.ClickElement("CheckDocToDel");

                executionLog.Log("PartAgentLeadsCreditsIssue", "Click on delete button.");
                office_LeadsHelper.ClickElement("DeleteLead");
                office_LeadsHelper.AcceptAlert();

                executionLog.Log("PartAgentLeadsCreditsIssue", "Wait for confirmation message.");
                office_LeadsHelper.WaitForText("1 records deleted successfully", 10);

                executionLog.Log("PartAgentLeadsCreditsIssue", "Redirect at leads recycle bin page.");
                VisitOffice("leads/recyclebin");

                executionLog.Log("PartAgentLeadsCreditsIssue", "Verify page title.");
                VerifyTitle("Recycled Leads");

                executionLog.Log("PartAgentLeadsCreditsIssue", "Click on delete icon");
                office_LeadsHelper.ClickElement("DeleteLeadPer");
                office_LeadsHelper.AcceptAlert();

                executionLog.Log("PartAgentLeadsCreditsIssue", "Wait for confirmation.");
                office_LeadsHelper.WaitForText("Lead Permanently Deleted.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("PartAgentLeadsCreditsIssue");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("PartAgentLeadsCreditsIssue");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("PartAgentLeadsCreditsIssue", "Bug", "Medium", "Leads page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("PartAgentLeadsCreditsIssue");
                        TakeScreenshot("PartAgentLeadsCreditsIssue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\PartAgentLeadsCreditsIssue.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("PartAgentLeadsCreditsIssue");
                        string id = loginHelper.getIssueID("PartAgentLeadsCreditsIssue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\PartAgentLeadsCreditsIssue.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("PartAgentLeadsCreditsIssue"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("PartAgentLeadsCreditsIssue");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("PartAgentLeadsCreditsIssue");
                executionLog.WriteInExcel("PartAgentLeadsCreditsIssue", Status, JIRA, "Lead Management");
            }
        }
    }
}