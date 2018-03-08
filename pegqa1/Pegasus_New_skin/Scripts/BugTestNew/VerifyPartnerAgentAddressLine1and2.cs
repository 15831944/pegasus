using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class VerifyPartnerAgentAddressLine1and2 : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("BugTestNew")]
        public void verifyPartnerAgentAddressLine1and2()
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
            var agent_PartnerAgentHelper = new Agents_PartnerAgentsHelper(GetWebDriver());

            // Variable
            var fname = "Partner" + RandomNumber(111,99999);
            var lname = "Agent" + RandomNumber(111,99999);
            var username1 = "testagent" + RandomNumber(100,999);
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("VerifyPartnerAgentAddressLine1and2", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyPartnerAgentAddressLine1and2", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("VerifyPartnerAgentAddressLine1and2", "Redirect to the URL");
                VisitOffice("partners/agents");
                agent_PartnerAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyPartnerAgentAddressLine1and2", "Click on Create button");
                agent_PartnerAgentHelper.ClickElement("Create");
                agent_PartnerAgentHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyPartnerAgentAddressLine1and2", "Enter First Name");
                agent_PartnerAgentHelper.TypeText("FirstName", fname);

                executionLog.Log("VerifyPartnerAgentAddressLine1and2", "Enter Last Name");
                agent_PartnerAgentHelper.TypeText("LastName", lname);

                executionLog.Log("VerifyPartnerAgentAddressLine1and2", "Select eAddress type");
                agent_PartnerAgentHelper.Select("eAddressType", "E-Mail");

                executionLog.Log("VerifyPartnerAgentAddressLine1and2", "Enter eAddress");
                agent_PartnerAgentHelper.TypeText("eAddress", "Agent@yopmail.com");

                executionLog.Log("VerifyPartnerAgentAddressLine1and2", "Select Address type");
                agent_PartnerAgentHelper.Select("AddressType", "Office");

                executionLog.Log("VerifyPartnerAgentAddressLine1and2", "Enter Address Line1");
                agent_PartnerAgentHelper.TypeText("AddressLine1", "Add1");

                executionLog.Log("VerifyPartnerAgentAddressLine1and2", "Enter Address Line2");
                agent_PartnerAgentHelper.TypeText("AddressLine2", "Add2");

                executionLog.Log("VerifyPartnerAgentAddressLine1and2", "Enter Postal Code");
                agent_PartnerAgentHelper.TypeText("PostalCode", "20001");
                agent_PartnerAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyPartnerAgentAddressLine1and2", "Enter Username");
                agent_PartnerAgentHelper.TypeText("UserName", username1);

                executionLog.Log("VerifyPartnerAgentAddressLine1and2", "Click Partner User Avatar check box");
                agent_PartnerAgentHelper.ClickElement("ParnterUserAvatar");

                executionLog.Log("VerifyPartnerAgentAddressLine1and2", "Click Save button");
                agent_PartnerAgentHelper.ClickElement("ClickSave");
                agent_PartnerAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyPartnerAgentAddressLine1and2", "Redirect to All Partner Agents page");
                VisitOffice("partners/agents");
                agent_PartnerAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyPartnerAgentAddressLine1and2", "Click Advanced Filter button");
                agent_PartnerAgentHelper.ClickElement("AdvanceFilter");
                agent_PartnerAgentHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyPartnerAgentAddressLine1and2", "Select Address Line1 from Available Columns");
                agent_PartnerAgentHelper.Select("AvailableCols", "address_line_1");

                executionLog.Log("VerifyPartnerAgentAddressLine1and2", "Move Address Line1 to Display Columns");
                agent_PartnerAgentHelper.ClickElement("AddCols");

                executionLog.Log("VerifyPartnerAgentAddressLine1and2", "Select Address Line2 from Available Columns");
                agent_PartnerAgentHelper.Select("AvailableCols", "address_line_2");

                executionLog.Log("VerifyPartnerAgentAddressLine1and2", "Move Address Line2 to Display Columns");
                agent_PartnerAgentHelper.ClickElement("AddCols");

                executionLog.Log("VerifyPartnerAgentAddressLine1and2", "Move Address Line2 to Display Columns");
                agent_PartnerAgentHelper.ClickElement("ApplyButton");
                agent_PartnerAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyPartnerAgentAddressLine1and2", "Search Agent");
                agent_PartnerAgentHelper.TypeText("SearchAgent", fname+" "+lname);

                executionLog.Log("VerifyPartnerAgentAddressLine1and2","Verify Address Line 1");
                agent_PartnerAgentHelper.VerifyText("SixthRowHead", "Address1");
                Console.WriteLine("Address Line1 is present");

                executionLog.Log("VerifyPartnerAgentAddressLine1and2", "Verify Address Line 2");
                agent_PartnerAgentHelper.VerifyText("SeventhRowHead", "Address2");
                Console.WriteLine("Address Line2 is present");


            }
            catch (Exception e)
            {
               
            }
           
        }
    }
}
