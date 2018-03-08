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
    public class MergeOpportunitiesConfirmation : DriverTestCase
    {
        [TestMethod]
        [TestCategory("Bug")]
        [TestCategory("All")]
        [TestCategory("Temp")]
        [TestCategory("TS2")]
        [TestCategory("BugTestNew")]
        public void mergeOpportunitiesConfirmation()
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

            // Random Variables
            var File = GetPathToFile() + "opportunitysamples.csv";
            String JIRA = "";
            String Status = "Pass";


            try
            {
                executionLog.Log("MergeOpportunitiesConfirmation", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("MergeOpportunitiesConfirmation", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("MergeOpportunitiesConfirmation", "Redirect at import opportunities page.");
                VisitOffice("opportunities/import");

                executionLog.Log("MergeOpportunitiesConfirmation", "Upload a csv file");
                office_OpportunitiesHelper.Upload("BrowseFile", File);

                executionLog.Log("MergeOpportunitiesConfirmation", "Click on Import Opportunities");
                office_OpportunitiesHelper.ClickElement("ImportOpp.");
                office_OpportunitiesHelper.WaitForWorkAround(1000);

                executionLog.Log("MergeOpportunitiesConfirmation", "Scroll to merge button.");
                office_OpportunitiesHelper.ScrollDown("//div[@class='row']/div/a[@title='Merge']");

                executionLog.Log("MergeOpportunitiesConfirmation", "Click on merge button.");
                office_OpportunitiesHelper.ClickElement("MergeOpp.");

                executionLog.Log("MergeOpportunitiesConfirmation", "Click ok to accept alert message");
                office_OpportunitiesHelper.AcceptAlert();

                executionLog.Log("MergeOpportunitiesConfirmation", "Click on Import Opportunities");
                office_OpportunitiesHelper.WaitForText("No records are merged", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("MergeOpportunitiesConfirmation");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Merge Opportunities Confirmation");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Merge Opportunities Confirmation", "Bug", "Medium", "Opportunities page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Merge Opportunities Confirmation");
                        TakeScreenshot("MergeOpportunitiesConfirmation");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\MergeOpportunitiesConfirmation.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("MergeOpportunitiesConfirmation");
                        string id = loginHelper.getIssueID("Merge Opportunities Confirmation");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\MergeOpportunitiesConfirmation.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Merge Opportunities Confirmation"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Merge Opportunities Confirmation");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("MergeOpportunitiesConfirmation");
                executionLog.WriteInExcel("Merge Opportunities Confirmation", Status, JIRA, "Opportunities Management");
            }
        }
    }
}