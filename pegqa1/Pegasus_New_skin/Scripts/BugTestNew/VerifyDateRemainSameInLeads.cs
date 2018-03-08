using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class VerifyDateRemainSameInLeads : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin")]
        [TestCategory("TS2")]
        [TestCategory("BugTestNew")]
        public void verifyDateRemainSameInLeads()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var office_LeadsHelper = new Office_LeadsHelper(GetWebDriver());
            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Variable
            String JIRA = "";
            String Status = "Pass";

            //try
            //{
                executionLog.Log("VerifyDateRemainSameInLeads", "Login with valid credential  Username");
                Login(username[0], password[0]);

                executionLog.Log("VerifyDateRemainSameInLeads", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyDateRemainSameInLeads", "Redirect to All Leads page");
                VisitOffice("leads");

                executionLog.Log("VerifyDateRemainSameInLeads", "Click on first lead");
                office_LeadsHelper.ClickElement("FirstLead");
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyDateRemainSameInLeads", "Click on Company Details");
                office_LeadsHelper.ClickElement("CompanyDetails");
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyDateRemainSameInLeads", "Expand Other Information tab");
                office_LeadsHelper.ClickElement("OtherInfoExpand");

                executionLog.Log("VerifyDateRemainSameInLeads", "Clear initial value of Opening Date For Previous Processsor");
                office_LeadsHelper.ClearTextBoxValue("//input[@id='LeadDetailPrevProcessorOpenDate']");

                executionLog.Log("VerifyDateRemainSameInLeads", "Enter Opening Date For Previous Processsor");
                office_LeadsHelper.TypeText("OpeningDate", "02/04/2017");

                executionLog.Log("VerifyDateRemainSameInLeads", "Clear initial value of Date of Current Ownership");
                office_LeadsHelper.ClearTextBoxValue("//input[@id='LeadDetailCurrentOwnDate']");

                executionLog.Log("VerifyDateRemainSameInLeads", "Enter Date of Current Ownership");
                office_LeadsHelper.TypeText("DateOfCurrentOwnership", "02/04/2017");

                executionLog.Log("VerifyDateRemainSameInLeads", "Click on Save");
                office_LeadsHelper.ClickElement("SaveCD");
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyDateRemainSameInLeads", "Expand Other Information tab");
                office_LeadsHelper.ClickElement("OtherInfoExpand");
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyDateRemainSameInLeads", "Verify value of Opening Date For Previous Processsor");
                office_LeadsHelper.VerifyValue("OpeningDate", "02/04/2017");
                Console.WriteLine("Value of Opening Date For Previous Processsor is not changed");

                executionLog.Log("VerifyDateRemainSameInLeads", "Verify value of Date of Current Ownership");
                office_LeadsHelper.VerifyValue("DateOfCurrentOwnership", "02/04/2017");
                Console.WriteLine("Value of Date of Current Ownership is not changed");


            //}
            //catch (Exception e)
            //{

            //}
           
        }
    }
}