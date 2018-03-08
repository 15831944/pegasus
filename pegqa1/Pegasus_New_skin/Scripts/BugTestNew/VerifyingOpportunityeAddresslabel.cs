using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class VerifyingOpportunityeAddresslabel : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS3")]
        [TestCategory("BugTestNew")]
        public void verifyingOpportunityeAddresslabel()
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
            var Name = "Opportunity" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("VerifyingOpportunityeAddresslabel", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyingOpportunityeAddresslabel", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyingOpportunityeAddresslabel", "Redirect To URL");
                VisitOffice("opportunities/create");

                executionLog.Log("VerifyingOpportunityeAddresslabel", "Enter Opportunity Name");
                office_OpportunitiesHelper.TypeText("Name", Name);

                executionLog.Log("VerifyingOpportunityeAddresslabel", "Enter Company Name");
                office_OpportunitiesHelper.TypeText("CompanyName", Name);

                executionLog.Log("VerifyingOpportunityeAddresslabel", "Select Opportunity Status");
                office_OpportunitiesHelper.Select("State", "New");

                executionLog.Log("VerifyingOpportunityeAddresslabel", "Select Opportunity responsibility");
                office_OpportunitiesHelper.SelectByText("Responsibility", "Howard Tang");

                executionLog.Log("VerifyingOpportunityeAddresslabel", "Select eAddress Type");
                office_OpportunitiesHelper.Select("EaddressType", "E-Mail");
                office_OpportunitiesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyingOpportunityeAddresslabel", "Verify eAddress Label");
                office_OpportunitiesHelper.VerifyText("EaddressLabel", "Home");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyingOpportunityeAddresslabel");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verifying Opportunity eAddress label");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verifying Opportunity eAddress label", "Bug", "Medium", "Opportunities page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verifying Opportunity eAddress label");
                        TakeScreenshot("VerifyingOpportunityeAddresslabel");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyingOpportunityeAddresslabel.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyingOpportunityeAddresslabel");
                        string id = loginHelper.getIssueID("Verifying Opportunity eAddress label");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyingOpportunityeAddresslabel.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verifying Opportunity eAddress label"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verifying Opportunity eAddress label");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyingOpportunityeAddresslabel");
                executionLog.WriteInExcel("Verifying Opportunity eAddress label", Status, JIRA, "Opportunities Management");
            }
        }
    }
}