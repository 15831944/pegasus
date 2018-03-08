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
    public class VerifyImportOpportunityWithDescription : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("BugTestNew")]
        public void verifyImportOpportunityWithDescription()
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

            //Variables
            String JIRA = "";
            String Status = "Pass";
            var filepath = GetPathToFile() + "opportunitysamples - Original.csv";

            try
            {

                executionLog.Log("VerifyImportOpportunityWithDescription", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyImportOpportunityWithDescription", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyImportOpportunityWithDescription", "Redirect to All opportunities page.");
                VisitOffice("opportunities");

                executionLog.Log("VerifyImportOpportunityWithDescription", "Click on Import button");
                office_OpportunitiesHelper.ClickElement("Import");

                executionLog.Log("VerifyImportOpportunityWithDescription", "Select csv file to import");
                office_OpportunitiesHelper.Upload("SelectFile", filepath);
                Console.WriteLine("Selected File");

                executionLog.Log("VerifyImportOpportunityWithDescription", "Click on Import button");
                office_OpportunitiesHelper.ClickElement("ImportBtn");
                office_OpportunitiesHelper.WaitForWorkAround(3000);
                Console.WriteLine("Opportunity successfully imported");

                executionLog.Log("VerifyImportOpportunityWithDescription", "Redirect to All opportunities page.");
                VisitOffice("opportunities");
                office_OpportunitiesHelper.WaitForWorkAround(3000);
                Console.WriteLine("Redirected to All opportunities page");

                executionLog.Log("VerifyImportOpportunityWithDescription", "Enter opportunity to be searched");
                office_OpportunitiesHelper.TypeText("SearchOpportunity", "Steve");
                office_OpportunitiesHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyImportOpportunityWithDescription", "Click on Imported opportunity");
                office_OpportunitiesHelper.ClickElement("Opportunity1");
                office_OpportunitiesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyImportOpportunityWithDescription", "Click on Description");
                office_OpportunitiesHelper.ClickForce("DescriptionHead");
                office_OpportunitiesHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyImportOpportunityWithDescription", "Verify Description");
                office_OpportunitiesHelper.VerifyText("Description", "tester1");


            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyImportOpportunityWithDescription");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Import Opportunity With Description");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Import Opportunity With Description", "Bug", "Medium", "Partner Agents", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Import Opportunity With Description");
                        TakeScreenshot("VerifyImportOpportunityWithDescription");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyImportOpportunityWithDescription.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyImportOpportunityWithDescription");
                        string id = loginHelper.getIssueID("Verify Import Opportunity With Description");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyImportOpportunityWithDescription.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Import Opportunity With Description"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Import Opportunity With Description");
                //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyImportOpportunityWithDescription");
                executionLog.WriteInExcel("Verify Import Opportunity With Description", Status, JIRA, "Agents Portal");
            }
           
        }
    }
}