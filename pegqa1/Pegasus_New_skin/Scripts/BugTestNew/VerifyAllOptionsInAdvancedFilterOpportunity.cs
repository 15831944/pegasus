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
    public class VerifyAllOptionsInAdvancedFilterOpportunity : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("BugTestNew")]
        public void verifyAllOptionsInAdvancedFilterOpportunity()
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

                executionLog.Log("VerifyAllOptionsInAdvancedFilterOpportunity", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyAllOptionsInAdvancedFilterOpportunity", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyAllOptionsInAdvancedFilterOpportunity", "Redirect at opportunities page.");
                VisitOffice("opportunities");

                executionLog.Log("VerifyAllOptionsInAdvancedFilterOpportunity", "Click on Advanced Filter");
                office_OpportunitiesHelper.ClickElement("AdvancefiltLabel");
                office_OpportunitiesHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyAllOptionsInAdvancedFilterOpportunity", "Click on Add Row");
                office_OpportunitiesHelper.ClickElement("AddRowBtn");
                office_OpportunitiesHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyAllOptionsInAdvancedFilterOpportunity", "Verify options present on second row");
                office_OpportunitiesHelper.IsElementPresent("//select[@id='OpportunityCustomColNames1']/optgroup[@label='Opportunities - Details']");
                office_OpportunitiesHelper.IsElementPresent("//select[@id='OpportunityCustomColNames1']/optgroup[@label='Opportunities - Description']");
                office_OpportunitiesHelper.IsElementPresent("//select[@id='OpportunityCustomColNames1']/optgroup[@label='Opportunities - Phones']");
                office_OpportunitiesHelper.IsElementPresent("//select[@id='OpportunityCustomColNames1']/optgroup[@label='Opportunities - Electronic Addresses']");
                office_OpportunitiesHelper.IsElementPresent("//select[@id='OpportunityCustomColNames1']/optgroup[@label='Opportunities - Addresses - Location Address']");
                office_OpportunitiesHelper.IsElementPresent("//select[@id='OpportunityCustomColNames1']/optgroup[@label='Opportunities - Addresses - Mailing Address']");
                office_OpportunitiesHelper.WaitForWorkAround(1000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyAllOptionsInAdvancedFilterOpportunity");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify All Options In Advanced Filter Opportunity");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify All Options In Advanced Filter Opportunity", "Bug", "Medium", "Opportunities page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify All Options In Advanced Filter Opportunity");
                        TakeScreenshot("VerifyAllOptionsInAdvancedFilterOpportunity");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyAllOptionsInAdvancedFilterOpportunity.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyAllOptionsInAdvancedFilterOpportunity");
                        string id = loginHelper.getIssueID("Verify All Options In Advanced Filter Opportunity");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyAllOptionsInAdvancedFilterOpportunity.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify All Options In Advanced Filter Opportunity"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify All Options In Advanced Filter Opportunity");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyAllOptionsInAdvancedFilterOpportunity");
                executionLog.WriteInExcel("Verify All Options In Advanced Filter Opportunity", Status, JIRA, "Opportunities Management");
            }
        }
    }
}