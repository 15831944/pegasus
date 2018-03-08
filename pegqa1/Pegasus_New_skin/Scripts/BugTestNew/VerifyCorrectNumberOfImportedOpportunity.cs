using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class VerifyCorrectNumberOfImportedOpportunity : DriverTestCase
    {
        [TestMethod]
        [TestCategory("Bug")]
        [TestCategory("TS2")]
        [TestCategory("BugTestNew")]
        public void verifyCorrectNumberOfImportedOpportunity()
        {
            string[] username = null;
            string[] password = null;

            XMLParse oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var office_OpportunitiesHelper = new Office_OpportunitiesHelper(GetWebDriver());
            var path = GetPathToFile() + "opportunitysamples - Original.csv";

            // Random Variable.
            String JIRA = "";
            String Status = "Pass";
            try
            {

                executionLog.Log("VerifyCorrectNumberOfImportedOpportunity", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyCorrectNumberOfImportedOpportunity", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyCorrectNumberOfImportedOpportunity", "Redirect at Import opportunities page.");
                VisitOffice("opportunities/import");
                office_OpportunitiesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCorrectNumberOfImportedOpportunity", "Upload csv file");
                office_OpportunitiesHelper.Upload("BrowseFile", path);
                office_OpportunitiesHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyCorrectNumberOfImportedOpportunity", "Click on Import button");
                office_OpportunitiesHelper.ClickElement("ImportOpp.");
                office_OpportunitiesHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyCorrectNumberOfImportedOpportunity", "Verify message of import");
                office_OpportunitiesHelper.WaitForText("4 Records Imported Successfully.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyCorrectNumberOfImportedOpportunity");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Correct Number Of Imported Opportunity");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Correct Number Of Imported Opportunity", "Bug", "Medium", "Opportunities page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Correct Number Of Imported Opportunity");
                        TakeScreenshot("VerifyCorrectNumberOfImportedOpportunity");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyCorrectNumberOfImportedOpportunity.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyCorrectNumberOfImportedOpportunity");
                        string id = loginHelper.getIssueID("Verify Correct Number Of Imported Opportunity");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyCorrectNumberOfImportedOpportunity.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Correct Number Of Imported Opportunity"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Correct Number Of Imported Opportunity");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyCorrectNumberOfImportedOpportunity");
                executionLog.WriteInExcel("Verify Correct Number Of Imported Opportunity", Status, JIRA, "Opportunities Management");
            }
        }
    }
}