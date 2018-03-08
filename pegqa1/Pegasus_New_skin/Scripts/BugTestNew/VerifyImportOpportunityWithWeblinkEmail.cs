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
    public class VerifyImportOpportunityWithWeblinkEmail : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("BugTestNew")]
        public void verifyImportOpportunityWithWeblinkEmail()
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

            //try
            //{

                executionLog.Log("VerifyImportOpportunityWithWeblinkEmail", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyImportOpportunityWithWeblinkEmail", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyImportOpportunityWithWeblinkEmail", "Redirect to All opportunities page.");
                VisitOffice("opportunities");

                executionLog.Log("VerifyImportOpportunityWithWeblinkEmail", "Click on Import button");
                office_OpportunitiesHelper.ClickElement("Import");

                executionLog.Log("VerifyImportOpportunityWithWeblinkEmail", "Select csv file to import");
                office_OpportunitiesHelper.Upload("SelectFile", "C:/Users/user/Downloads/opportunitysamples.csv");
                Console.WriteLine("Selected File");

                executionLog.Log("VerifyImportOpportunityWithWeblinkEmail", "Click on Import button");
                office_OpportunitiesHelper.ClickElement("ImportBtn");
                office_OpportunitiesHelper.WaitForWorkAround(3000);
                Console.WriteLine("Opportunity successfully imported");

                executionLog.Log("VerifyImportOpportunityWithWeblinkEmail", "Redirect to All opportunities page.");
                VisitOffice("opportunities");
                Console.WriteLine("Redirected to All opportunities page");

                executionLog.Log("VerifyImportOpportunityWithWeblinkEmail", "Click on Imported opportunity");
                office_OpportunitiesHelper.ClickElement("FirstRowSteve");
                office_OpportunitiesHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyImportOpportunityWithWeblinkEmail", "Click on Edit button");
                office_OpportunitiesHelper.ClickElement("EditBtn");
                office_OpportunitiesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyImportOpportunityWithWeblinkEmail", "Value of Email Type 2");
                office_OpportunitiesHelper.VerifySelectedValue("EmailType2", "Web Links");
                Console.WriteLine("Email Type 2 is Web Links");

                executionLog.Log("VerifyImportOpportunityWithWeblinkEmail", "Value of Email Label 2");
                office_OpportunitiesHelper.VerifySelectedValue("EmailLabel2","Web Link");
                Console.WriteLine("Email Label 2 is Web Link");

                Console.WriteLine("Opportunity is imported with Email - 2 as Web Link");
                

            //}
            //catch (Exception e)
            //{
               

            //}
           
        }
    }
}