using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class LeadSave : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS1")]
        [TestCategory("BugTest")]
        public void leadSave()
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
            var Office_LeadsHelper = new Office_LeadsHelper(GetWebDriver());

            // VARIABLE
            var Company = "My Company" + GetRandomNumber();
            var name = "TestEmployee" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("LeadSave", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("LeadSave", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("LeadSave", "Redirect To ");
                VisitOffice("leads/create");

                executionLog.Log("LeadSave", "Verify page title. ");
                VerifyTitle("Create a Lead");
                Office_LeadsHelper.WaitForWorkAround(2000);

                executionLog.Log("LeadSave", "Click on Assignments");
                Office_LeadsHelper.ClickElement("Assignments");

                executionLog.Log("LeadSave", "Wait for element to be visible.");
                Office_LeadsHelper.WaitForElementPresent("LeadStatus", 10);

                executionLog.Log("LeadSave", "Select Lead Status");
                Office_LeadsHelper.Select("LeadStatus", "New");

                executionLog.Log("LeadSave", "LeadResponsibility");
                Office_LeadsHelper.SelectByText("Responsibility", "Howard Tang");

                executionLog.Log("LeadSave", "Enter First Name ");
                Office_LeadsHelper.TypeText("FirstNameLead", "Test Lead");

                executionLog.Log("LeadSave", "Enter Last Name");
                Office_LeadsHelper.TypeText("LastName", "Tester");

                executionLog.Log("LeadSave", "Enter Company Name");
                Office_LeadsHelper.TypeText("CompanyName", Company);

                executionLog.Log("LeadSave", "Click on Save");
                Office_LeadsHelper.ClickElement("SaveLeadButton");
                Office_LeadsHelper.WaitForWorkAround(1000);

                var LocDub = "//button[text()='Create Duplicate']";
                if (Office_LeadsHelper.IsElementPresent(LocDub))
                {

                    Office_LeadsHelper.WaitForWorkAround(4000);

                    executionLog.Log("LeadSave", "Click on duplicate btn");
                    Office_LeadsHelper.Click(LocDub);
                    Office_LeadsHelper.WaitForWorkAround(3000);

                    executionLog.Log("LeadSave", "Verify text.");
                    Office_LeadsHelper.WaitForText("Lead saved successfully.", 10);
                    Office_LeadsHelper.WaitForWorkAround(1000);

                    executionLog.Log("LeadSave", "Redirect To create lead page. ");
                    VisitOffice("leads");

                    executionLog.Log("LeadSave", "Enter Company Name");
                    Office_LeadsHelper.TypeText("CompanySearch", Company);
                    Office_LeadsHelper.WaitForWorkAround(2000);

                    executionLog.Log("LeadSave", "Select lead by check box");
                    Office_LeadsHelper.ClickElement("CheckDocToDel");
                    Office_LeadsHelper.WaitForWorkAround(2000);

                    executionLog.Log("LeadSave", "Click on delete lead");
                    Office_LeadsHelper.ClickElement("DeleteLead");

                    executionLog.Log("LeadSave", "Accept alert message.");
                    Office_LeadsHelper.AcceptAlert();

                    executionLog.Log("LeadSave", "Wait for success message.");
                    Office_LeadsHelper.WaitForText("1 records deleted successfully", 10);

                }
            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("LeadSave");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Lead Save");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Lead Save", "Bug", "Medium", "Lead page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Lead Save");
                        TakeScreenshot("LeadSave");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\LeadSave.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("LeadSave");
                        string id = loginHelper.getIssueID("Lead Save");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\LeadSave.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Lead Save"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Lead Save");
          //      executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("LeadSave");
                executionLog.WriteInExcel("Lead Save", Status, JIRA, "Leads Management");
            }
        }
    }
}