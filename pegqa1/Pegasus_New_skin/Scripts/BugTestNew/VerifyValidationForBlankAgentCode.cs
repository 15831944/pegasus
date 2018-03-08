using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class VerifyValidationForBlankAgentCode : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("BugTestNew")]
        public void verifyValidationForBlankAgentCode()
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
            var agents_PartnerAgentHelper = new Agents_PartnerAgentsHelper(GetWebDriver());

            // Variable
            var Sales = "" + RandomNumber(1, 999);
            var Revenue = "" + RandomNumber(1, 99);
            
            //try
            //{
                executionLog.Log("VerifyValidationForBlankAgentCode", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyValidationForBlankAgentCode", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("VerifyValidationForBlankAgentCode", "Redirect to the URL");
                VisitOffice("partners/agents");
                agents_PartnerAgentHelper.WaitForWorkAround(3000);
                Console.WriteLine("Redirected to Partner Agents");

                executionLog.Log("VerifyValidationForBlankAgentCode", "Click on first agent");
                agents_PartnerAgentHelper.ClickElement("OpenAgent");
                agents_PartnerAgentHelper.WaitForWorkAround(6000);
                Console.WriteLine("Waited for given time span");

                executionLog.Log("VerifyValidationForBlankAgentCode", "Click Add a new Agent Code button");
                agents_PartnerAgentHelper.ClickElement("AddNewAgentCode");
                agents_PartnerAgentHelper.WaitForWorkAround(3000);
                Console.WriteLine("Create Agent Code Popup opened");

                executionLog.Log("VerifyValidationForBlankAgentCode", "Enter Sales Code");
                agents_PartnerAgentHelper.TypeText("SalesCode", Sales);

                executionLog.Log("VerifyValidationForBlankAgentCode", "Enter Revenue Share");
                agents_PartnerAgentHelper.TypeText("RevenueShare", Revenue);

                executionLog.Log("VerifyValidationForBlankAgentCode", "Click Save button");
                agents_PartnerAgentHelper.ClickElement("SaveBtn");
                agents_PartnerAgentHelper.WaitForWorkAround(4000);
                Console.WriteLine("Agent Code Created");

                executionLog.Log("VerifyValidationForBlankAgentCode", "Click Edit button");
                agents_PartnerAgentHelper.ClickElement("EditAgentCode1");

                executionLog.Log("VerifyValidationForBlankAgentCode", "Clear Agent Code");
                agents_PartnerAgentHelper.ClearText("AgentCode");
                Console.WriteLine("Cleared Agent Code");

                executionLog.Log("VerifyValidationForBlankAgentCode", "Clear Revenue Share");
                agents_PartnerAgentHelper.ClearText("RevenueShareEdit");
                Console.WriteLine("Cleared Revenue Share");

                executionLog.Log("VerifyValidationForBlankAgentCode", "Click Save button");
                agents_PartnerAgentHelper.ClickElement("SaveAgentCode");

                executionLog.Log("VerifyValidationForBlankAgentCode", "Click Save button");
                agents_PartnerAgentHelper.VerifyTextAvailable("Agent Code: Field is required");
                Console.WriteLine("Validation is appearing for blank Agent Code and Revenue Share");
                

            //}
            //catch (Exception e)
            //{
                
            //}
            
        }
    }
}
