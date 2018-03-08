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
    public class VerifyCallsOptionInActivityTypeOfOpportunity : DriverTestCase
    {
        [TestMethod]
        [TestCategory("Bug")]
        [TestCategory("BugTestNew")]
        public void verifyCallsOptionInActivityTypeOfOpportunity()
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

            // Random Variable.
            String JIRA = "";
            String Status = "Pass";
            try
            {

                executionLog.Log("VerifyCallsOptionInActivityTypeOfOpportunity", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyCallsOptionInActivityTypeOfOpportunity", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyCallsOptionInActivityTypeOfOpportunity", "Redirect at opportunities page.");
                VisitOffice("opportunities");
                office_OpportunitiesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCallsOptionInActivityTypeOfOpportunity", "Click on any Opportunity");
                office_OpportunitiesHelper.ClickElement("Opportunities1");
                office_OpportunitiesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCallsOptionInActivityTypeOfOpportunity", "Click on Opportunity History");
                office_OpportunitiesHelper.ClickElement("OpportunityHistory");
                office_OpportunitiesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCallsOptionInActivityTypeOfOpportunity", "Verify Calls option");
                office_OpportunitiesHelper.VerifyCallOption("ActvtyType");
                //office_OpportunitiesHelper.WaitForWorkAround(3000);

                

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyCallsOptionInActivityTypeOfOpportunity");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Calls Option In Activity Type Of Opportunity");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Calls Option In Activity Type Of Opportunity", "Bug", "Medium", "Opportunities page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Calls Option In Activity Type Of Opportunity");
                        TakeScreenshot("VerifyCallsOptionInActivityTypeOfOpportunity");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyCallsOptionInActivityTypeOfOpportunity.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyCallsOptionInActivityTypeOfOpportunity");
                        string id = loginHelper.getIssueID("Verify Calls Option In Activity Type Of Opportunity");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyCallsOptionInActivityTypeOfOpportunity.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Calls Option In Activity Type Of Opportunity"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Calls Option In Activity Type Of Opportunity");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyCallsOptionInActivityTypeOfOpportunity");
                executionLog.WriteInExcel("Verify Calls Option In Activity Type Of Opportunity", Status, JIRA, "Opportunities Activities");
            }
        }
    }
}