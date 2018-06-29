using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class VerifyResidualReportMailing : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin")]
        [TestCategory("Fail")]
        [TestCategory("BugTestNew")]
        public void verifyResidualReportMailing()
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
            var residual_income_officePayoutHelper = new ResidualIncome_OfficePayoutHelper(GetWebDriver());


            // Variable random
            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("VerifyResidualReportMailing", "Login with valid username and password");
                Login(username[0], password[0]);

                executionLog.Log("VerifyResidualReportMailing", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("VerifyResidualReportMailing", "Go to Residual Income Reports");
                VisitOffice("rir/reports");
                residual_income_officePayoutHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyResidualReportMailing", "Select Reporting Period");
                residual_income_officePayoutHelper.Select("ReportingPeriod", "2017-02-01");

                executionLog.Log("VerifyResidualReportMailing", "Select File Date");
                residual_income_officePayoutHelper.Select("FileDate", "2017-02-27");

                executionLog.Log("VerifyResidualReportMailing", "Select Processor");
                residual_income_officePayoutHelper.Select("Processor", "First Data North");
                residual_income_officePayoutHelper.WaitForWorkAround(4000);

                executionLog.Log("VerifyResidualReportMailing", "Select File Format");
                residual_income_officePayoutHelper.Select("FileFormat", "Pegasus");

                executionLog.Log("VerifyResidualReportMailing", "Search Payouts");
                residual_income_officePayoutHelper.ClickElement("SearchBtn");
                residual_income_officePayoutHelper.WaitForWorkAround(6000);

                executionLog.Log("VerifyResidualReportMailing", "Verify Reports Generated");
                residual_income_officePayoutHelper.VerifyPageText("NewThemeCorp Residual Reports");

                executionLog.Log("VerifyResidualReportMailing", "Select Partner Agent");
                residual_income_officePayoutHelper.ClickElement("DolphoWeerCheckBox");

                executionLog.Log("VerifyResidualReportMailing", "Email PDF Residual Report to selected Partner Agent");
                residual_income_officePayoutHelper.ClickElement("SendBtn");
                residual_income_officePayoutHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyResidualReportMailing", "Verify Report is Emailed successfully");
                residual_income_officePayoutHelper.VerifyPageText("Report Mail Sent Successfully.");



            }
            catch (Exception e)
            {



            }
        }
    }
}