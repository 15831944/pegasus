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
    public class ImportOpportunitiesCancel : DriverTestCase
    {
        [TestMethod]
        [TestCategory("Bug")]
        [TestCategory("All")]
        [TestCategory("Temp")]
        [TestCategory("TS2")]
        [TestCategory("BugTestNew")]
        public void importOpportunitiesCancel()
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
                executionLog.Log("ImportOpportunitiesCancel", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("ImportOpportunitiesCancel", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("ImportOpportunitiesCancel", "Click on Import Opportunities");
                VisitOffice("opportunities/import");

                executionLog.Log("ImportOpportunitiesCancel", "Upload a file with duplicate records");
                office_OpportunitiesHelper.UploadFile("//*[@id='vcard_file']", File);

                executionLog.Log("ImportOpportunitiesCancel", "Click on Import button.");
                office_OpportunitiesHelper.ClickElement("ImportOpp.");

                executionLog.Log("ImportOpportunitiesCancel", "Scroll to merge button.");
                office_OpportunitiesHelper.ScrollDown("//div[@class='row']/div/a[@title='Merge']");

                executionLog.Log("ImportOpportunitiesCancel", "Click on merge button.");
                office_OpportunitiesHelper.ClickElement("MergeOpp.");

                executionLog.Log("ImportOpportunitiesCancel", "Decline the alert message.");
                office_OpportunitiesHelper.DeclineAlert();
                office_OpportunitiesHelper.WaitForWorkAround(1000);

                executionLog.Log("ImportOpportunitiesCancel", "Click on merge button.");
                office_OpportunitiesHelper.ClickElement("MergeOpp.");

            }

            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("ImportOpportunitiesCancel");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Import Opportunities Cancel");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Import Opportunities Cancel", "Bug", "Medium", "Client page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Import Opportunities Cancel");
                        TakeScreenshot("ImportOpportunitiesCancel");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ImportOpportunitiesCancel.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("ImportOpportunitiesCancel");
                        string id = loginHelper.getIssueID("Import Opportunities Cancel");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ImportOpportunitiesCancel.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Import Opportunities Cancel"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Import Opportunities Cancel");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("ImportOpportunitiesCancel");
                executionLog.WriteInExcel("Import Opportunities Cancel", Status, JIRA, "Opportunities Management");
            }
        }
    }
}