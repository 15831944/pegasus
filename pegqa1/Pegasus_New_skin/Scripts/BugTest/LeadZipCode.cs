using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class LeadZipCode : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("Fail")]
        [TestCategory("TS1")]
        [TestCategory("BugTest")]
        public void leadZipCode()
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

            // Variable

            var CompanyNameLead = "Lead" + GetRandomNumber();
            var FirstName = "Lead" + GetRandomNumber();
            var Number = "12345678" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("LeadZipCode", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("LeadZipCode", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("LeadZipCode", "Redirect To Lead");
                VisitOffice("leads/create");
                Office_LeadsHelper.WaitForWorkAround(5000);

                executionLog.Log("LeadSave", "Verify page title. ");
                VerifyTitle("Create a Lead");

                executionLog.Log("LeadZipCode", "Click on Comapy Detail");
                Office_LeadsHelper.TypeText("CompanyName", CompanyNameLead);

                executionLog.Log("LeadZipCode", "Select Salutation");
                Office_LeadsHelper.Select("Salutaion", "Mr");

                executionLog.Log("LeadZipCode", "Enter First Name");
                Office_LeadsHelper.TypeText("FirstNameLead", FirstName);

                executionLog.Log("LeadZipCode", "Enter Last Name");
                Office_LeadsHelper.TypeText("LastName", "Tester");

                executionLog.Log("LeadZipCode", "Select the Status");
                Office_LeadsHelper.SelectByText("LeadStatus", "Marketing");

                executionLog.Log("LeadZipCode", "Select the Responsbility");
                Office_LeadsHelper.SelectByText("Responsibility", "Howard Tang");

                executionLog.Log("LeadZipCode", "Click on Save Button button");
                Office_LeadsHelper.ClickElement("SaveLeadButton");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("LeadZipCode");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Lead Zip Code");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Lead Zip Code", "Bug", "Medium", "Lead page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Lead Zip Code");
                        TakeScreenshot("LeadZipCode");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\LeadZipCode.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("LeadZipCode");
                        string id = loginHelper.getIssueID("Lead Zip Code");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\LeadZipCode.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Lead Zip Code"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Lead Zip Code");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("LeadZipCode");
                executionLog.WriteInExcel("Lead Zip Code", Status, JIRA, "Leads Management");
            }
        }
    }
}