using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class VerifyResidualReportPublished : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin")]
        [TestCategory("Fail")]
        [TestCategory("BugTestNew")]
        public void verifyResidualReportPublished()
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

            //try
            //{

                executionLog.Log("VerifyResidualReportPublished", "Login with valid username and password");
                Login(username[0], password[0]);

                executionLog.Log("VerifyResidualReportPublished", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("VerifyResidualReportPublished", "Go to Residual Income Reports");
                VisitOffice("rir/reports");
                residual_income_officePayoutHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyResidualReportPublished", "Select Reporting Period");
                residual_income_officePayoutHelper.Select("ReportingPeriod", "2017-02-01");

                executionLog.Log("VerifyResidualReportPublished", "Select File Date");
                residual_income_officePayoutHelper.Select("FileDate", "2017-02-27");

                executionLog.Log("VerifyResidualReportPublished", "Select Processor");
                residual_income_officePayoutHelper.Select("Processor", "First Data North");
                residual_income_officePayoutHelper.WaitForWorkAround(4000);

                executionLog.Log("VerifyResidualReportPublished", "Select File Format");
                residual_income_officePayoutHelper.Select("FileFormat", "Pegasus");
                
                executionLog.Log("VerifyResidualReportPublished", "Search Payouts");
                residual_income_officePayoutHelper.ClickElement("SearchBtn");
                residual_income_officePayoutHelper.WaitForWorkAround(6000);

                executionLog.Log("VerifyResidualReportPublished", "Verify Reports Generated");
                residual_income_officePayoutHelper.VerifyPageText("NewThemeCorp Residual Reports");
               

            //}
            //catch (Exception e)
            //{
                
                
                    
            //}
        }
    }
}