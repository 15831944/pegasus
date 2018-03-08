using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class LeadCreateAndViewIssue : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin")]
        [TestCategory("TS2")]
        [TestCategory("BugTestNew")]
        public void leadCreateAndViewIssue()
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
                executionLog.Log("LeadCreateAndViewIssue", "Login with valid credential  Username");
                Login(username[0], password[0]);

                executionLog.Log("LeadCreateAndViewIssue", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("LeadCreateAndViewIssue", "Redirect at Create Lead");
                VisitOffice("leads/create");

                executionLog.Log("LeadCreateAndViewIssue", "Click on Save");
                office_LeadsHelper.ClickElement("SaveLeadNewSkin");
                office_LeadsHelper.WaitForWorkAround(2000);

                executionLog.Log("LeadCreateAndViewIssue", "Enter First Name");
                office_LeadsHelper.TypeText("FirstNameLead", FName);

                executionLog.Log("LeadCreateAndViewIssue", "Enter Last Name");
                office_LeadsHelper.TypeText("LeadLastName", LName);

                executionLog.Log("LeadCreateAndViewIssue", "Enter Lead Company DBA Name");
                office_LeadsHelper.TypeText("CompanyName", CDBA);

                executionLog.Log("LeadCreateAndViewIssue", "Wait for element to be visible.");
                office_LeadsHelper.WaitForElementPresent("LeadStatus", 10);

                executionLog.Log("LeadCreateAndViewIssue", "Select Lead Status");
                office_LeadsHelper.SelectByText("LeadStatus", "New");

                executionLog.Log("LeadCreateAndViewIssue", "Select Responsibilities");
                office_LeadsHelper.SelectByText("Responsibility", "Howard Tang");

                executionLog.Log("LeadCreateAndViewIssue", "Click on Save");
                office_LeadsHelper.ClickElement("SaveLeadNewSkin");
                office_LeadsHelper.WaitForWorkAround(7000);

                var loc = "//h3[text()='Existing Leads']";
                if (office_LeadsHelper.IsElementPresent(loc))
                {
                    Console.WriteLine("We are in first If cond as lead is duplicate !!");
                    executionLog.Log("LeadCreateAndViewIssue", "Click on Duplicate");
                    office_LeadsHelper.ClickOnDisplayed("CraeteLeadDub");
                    office_LeadsHelper.WaitForText("Lead saved successfully.", 10);

                    executionLog.Log("LeadCreateAndViewIssue", "Redirect at leads page.");
                    VisitOffice("leads");

                    executionLog.Log("LeadCreateAndViewIssue", "Verify page title as leads.");
                    VerifyTitle("Leads");

                    executionLog.Log("LeadCreateAndViewIssue", "Dearch lead by company name.");
                    office_LeadsHelper.TypeText("CompanySearch", CDBA);
                    office_LeadsHelper.WaitForWorkAround(4000);

                    executionLog.Log("LeadCreateAndViewIssue", "Click to view searched lead.");
                    office_LeadsHelper.ClickElement("Lead1");

                    executionLog.Log("LeadCreateAndViewIssue", "Verify heading information on view lead page.");
                    office_LeadsHelper.IsElementPresent("Information");


                }
                else
                {
                    Console.WriteLine("We are in first else cond as lead is not duplicate !!");
                    executionLog.Log("LeadCreateAndViewIssue", "Wait for Confirmation");
                    office_LeadsHelper.WaitForText("Lead saved successfully.", 10);

                    executionLog.Log("LeadCreateAndViewIssue", "Redirect at leads page.");
                    VisitOffice("leads");

                    executionLog.Log("LeadCreateAndViewIssue", "Verify page title as leads.");
                    VerifyTitle("Leads");

                    executionLog.Log("LeadCreateAndViewIssue", "Dearch lead by company name.");
                    office_LeadsHelper.TypeText("CompanySearch", CDBA);
                    office_LeadsHelper.WaitForWorkAround(4000);

                    executionLog.Log("LeadCreateAndViewIssue", "Click to view searched lead.");
                    office_LeadsHelper.ClickElement("Lead1");

                    executionLog.Log("LeadCreateAndViewIssue", "Verify heading information on view lead page.");
                    office_LeadsHelper.IsElementPresent("Information");

                }
            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("LeadCreateAndViewIssue");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("LeadCreateAndViewIssue");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("LeadCreateAndViewIssue", "Bug", "Medium", "Leads page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("LeadCreateAndViewIssue");
                        TakeScreenshot("LeadCreateAndViewIssue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\Lead Create And View Issue.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("LeadCreateAndViewIssue");
                        string id = loginHelper.getIssueID("LeadCreateAndViewIssue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\Lead Create And View Issue.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("LeadCreateAndViewIssue"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("LeadCreateAndViewIssue");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("LeadCreateAndViewIssue");
                executionLog.WriteInExcel("LeadCreateAndViewIssue", Status, JIRA, "Leads Management");
            }
        }
    }
}