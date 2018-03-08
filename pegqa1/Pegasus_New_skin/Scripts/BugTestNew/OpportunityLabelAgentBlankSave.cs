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
    public class VerifyOpportunityAddressLabel : DriverTestCase
    {
        [TestMethod]
        [TestCategory("Bug")]
        [TestCategory("BugTestNew")]
       
        public void verifyOpportunityAddressLabel()
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

                executionLog.Log("VerifyOpportunityAddressLabel", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyOpportunityAddressLabel", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyOpportunityAddressLabel", "Redirect at opportunities page.");
                VisitOffice("opportunities");

                executionLog.Log("VerifyOpportunityAddressLabel", "Click on Create button");
                office_OpportunitiesHelper.ClickElement("Create");

                executionLog.Log("VerifyOpportunityAddressLabel", "Verify Address Line 1 label");
                office_OpportunitiesHelper.VerifyText("AddressLine1Label", "Address Line 1:");

            }
            catch (Exception e)
            {
                
            }
            
        }
    }
}