using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class VerifyingIssuesOnPartnerAssoPage : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS3")]
        [TestCategory("BugTestNew")]
        public void verifyingIssuesOnPartnerAssoPage()
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
            executionLog.Log("VerifyingIssuesOnPartnerAssoPage", "Login with valid credentials");
            Login("aslamassociate", "123456");
            Console.WriteLine("Logged in as: aslamassociate / 123456");

            executionLog.Log("VerifyingIssuesOnPartnerAssoPage", "Redirect to All leads");
            office_LeadsHelper.ClickElement("LeadTab");
            office_LeadsHelper.WaitForWorkAround(3000);

            executionLog.Log("VerifyingIssuesOnPartnerAssoPage", "Click on create button.");
            office_LeadsHelper.ClickElement("CreateIcon");
            office_LeadsHelper.WaitForWorkAround(3000);

            executionLog.Log("VerifyingIssuesOnPartnerAssoPage", "Verify page title.");
            VerifyTitle("Create a Lead");

            executionLog.Log("VerifyingIssuesOnPartnerAssoPage", "Enter First Name");
            office_LeadsHelper.TypeText("FirstNameLead", FName);

            executionLog.Log("VerifyingIssuesOnPartnerAssoPage", "Enter Last Name");
            office_LeadsHelper.TypeText("LastName", LName);

            executionLog.Log("VerifyingIssuesOnPartnerAssoPage", "Enter Company DBA");
            office_LeadsHelper.TypeText("CompanyName", CDBA);

            executionLog.Log("VerifyingIssuesOnPartnerAssoPage", "Click on Assignments");
            office_LeadsHelper.ClickElement("Assignments");

            executionLog.Log("VerifyingIssuesOnPartnerAssoPage", "Wait for element to be visible.");
            office_LeadsHelper.WaitForElementPresent("LeadStatus", 10);

            executionLog.Log("VerifyingIssuesOnPartnerAssoPage", "Select Status");
            office_LeadsHelper.SelectByText("LeadStatus", "New");

            executionLog.Log("VerifyingIssuesOnPartnerAssoPage", "Select Responsibilities");
            office_LeadsHelper.SelectByText("Responsibility", "Howard Tang");

            executionLog.Log("VerifyingIssuesOnPartnerAssoPage", "Select source.");
            office_LeadsHelper.SelectByText("SelectSource", "Email");
            office_LeadsHelper.WaitForWorkAround(3000);

            executionLog.Log("VerifyingIssuesOnPartnerAssoPage", "Enter Zip code");
            office_LeadsHelper.TypeText("LeadZip", "60601");
            office_LeadsHelper.WaitForWorkAround(3000);

            executionLog.Log("VerifyingIssuesOnPartnerAssoPage", "Enter address line1");
            office_LeadsHelper.TypeText("AddressLine1", "test line 1");
            //office_LeadsHelper.WaitForWorkAround(3000);

            executionLog.Log("VerifyingIssuesOnPartnerAssoPage", "Enter address line2");
            office_LeadsHelper.TypeText("AddressLine2", "line 2");
            //office_LeadsHelper.WaitForWorkAround(3000);

            //executionLog.Log("VerifyingIssuesOnPartnerAssoPage", "Wait for locator to be present.");
            //office_LeadsHelper.IsElementVisible("//*[@id='LeadDetailSameAsLocation']");

            executionLog.Log("VerifyingIssuesOnPartnerAssoPage", "Click on same as location checkbox.");
            office_LeadsHelper.Click("//*[@id='LeadDetailSameAsLocation']");

            executionLog.Log("VerifyingIssuesOnPartnerAssoPage", "Click on Save");
            office_LeadsHelper.ClickElement("SaveLeadNewSkin");

            executionLog.Log("VerifyingIssuesOnPartnerAssoPage", "Wait for creation success text.");
            office_LeadsHelper.WaitForText("Lead saved successfully.", 10);
            office_LeadsHelper.WaitForWorkAround(2000);

            executionLog.Log("VerifyingIssuesOnPartnerAssoPage", "Search lead usnig company name.");
            office_LeadsHelper.TypeText("CompanySearch", CDBA);
            office_LeadsHelper.WaitForWorkAround(3000);

            executionLog.Log("VerifyingIssuesOnPartnerAssoPage", "Click on edit icon to edit lead.");
            office_LeadsHelper.ClickElement("EditLeadPartner");
            office_LeadsHelper.WaitForWorkAround(3000);

            executionLog.Log("VerifyingIssuesOnPartnerAssoPage", "Verify page title as edit a lead");
            VerifyTitle("Edit a Lead");

            executionLog.Log("VerifyingIssuesOnPartnerAssoPage", "Verify mailing address line 1 address copied.");
            office_LeadsHelper.verifyAddress1();
            //office_LeadsHelper.WaitForWorkAround(3000);

            executionLog.Log("VerifyingIssuesOnPartnerAssoPage", "Verify mailing line 2 copied address.");
            office_LeadsHelper.verifyAddress2();
            //office_LeadsHelper.WaitForWorkAround(3000);

            executionLog.Log("VerifyingIssuesOnPartnerAssoPage", "Click on Save");
            office_LeadsHelper.ClickElement("SaveLeadNewSkin");
            office_LeadsHelper.WaitForWorkAround(3000);

            executionLog.Log("VerifyingIssuesOnPartnerAssoPage", "Wait for updation success text.");
            office_LeadsHelper.WaitForText("Lead updated successfully.", 10);

            executionLog.Log("VerifyingIssuesOnPartnerAssoPage", "Verify Lead created by credits.");
            office_LeadsHelper.VerifyText("CreatedBy", "Aslam Associate");

            executionLog.Log("VerifyingIssuesOnPartnerAssoPage", "Verify Lead modified by credits");
            office_LeadsHelper.VerifyText("ModifiedBy", "Aslam Associate");

            executionLog.Log("VerifyingIssuesOnPartnerAssoPage", "Logout from the application");
            VisitOffice("logout");

            executionLog.Log("VerifyingIssuesOnPartnerAssoPage", "Login with valid credentials");
            Login(username[0], password[0]);
            Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

            executionLog.Log("VerifyingIssuesOnPartnerAssoPage", "Verify page Tilte");
            VerifyTitle("Dashboard");

            executionLog.Log("VerifyingIssuesOnPartnerAssoPage", "Redirect at leads page.");
            VisitOffice("leads");
            office_LeadsHelper.WaitForWorkAround(3000);

            executionLog.Log("VerifyingIssuesOnPartnerAssoPage", "Verify page titles.");
            VerifyTitle("Leads");

            //executionLog.Log("VerifyingIssuesOnPartnerAssoPage", "Login with valid credential  Username");
            //office_LeadsHelper.WaitForElementPresent("CheckDocToDel", 10);

            executionLog.Log("VerifyingIssuesOnPartnerAssoPage", "Search lead");
            office_LeadsHelper.TypeText("SearchCompany", CDBA);
            office_LeadsHelper.WaitForWorkAround(3000);

            executionLog.Log("VerifyingIssuesOnPartnerAssoPage", "Select first lead");
            office_LeadsHelper.ClickElement("CheckDocToDel");

            executionLog.Log("VerifyingIssuesOnPartnerAssoPage", "Click on delete button.");
            office_LeadsHelper.ClickElement("DeleteLead");
            office_LeadsHelper.AcceptAlert();

            executionLog.Log("VerifyingIssuesOnPartnerAssoPage", "Wait for confirmation message.");
            office_LeadsHelper.WaitForText("1 records deleted successfully", 10);

            executionLog.Log("VerifyingIssuesOnPartnerAssoPage", "Redirect at leads recycle bin page.");
            VisitOffice("leads/recyclebin");

            executionLog.Log("VerifyingIssuesOnPartnerAssoPage", "Verify page title.");
            VerifyTitle("Recycled Leads");

            executionLog.Log("VerifyingIssuesOnPartnerAssoPage", "Click on delete icon");
            office_LeadsHelper.ClickElement("DeleteLeadPer");
            office_LeadsHelper.AcceptAlert();

            executionLog.Log("VerifyingIssuesOnPartnerAssoPage", "Wait for confirmation.");
            office_LeadsHelper.WaitForText("Lead Permanently Deleted.", 10);

            VisitOffice("logout");
            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyingIssuesOnPartnerAssoPage");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("VerifyingIssuesOnPartnerAssoPage");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("VerifyingIssuesOnPartnerAssoPage", "Bug", "Medium", "Leads page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("VerifyingIssuesOnPartnerAssoPage");
                        TakeScreenshot("VerifyingIssuesOnPartnerAssoPage");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyingIssuesOnPartnerAssoPage.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyingIssuesOnPartnerAssoPage");
                        string id = loginHelper.getIssueID("VerifyingIssuesOnPartnerAssoPage");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyingIssuesOnPartnerAssoPage.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("VerifyingIssuesOnPartnerAssoPage"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("VerifyingIssuesOnPartnerAssoPage");
                // executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyingIssuesOnPartnerAssoPage");
                executionLog.WriteInExcel("VerifyingIssuesOnPartnerAssoPage", Status, JIRA, "Lead Management");
            }
        }
    }
} 