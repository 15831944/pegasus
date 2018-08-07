using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class PartnerReportReflectsColumnsSpecified : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin")]
        [TestCategory("Fail")]
        [TestCategory("ListManagement")]
        public void partnerReportReflectsColumnsSpecified()
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

            executionLog.Log("PartnerReportReflectsColumnsSpecified", "Login with valid username and password");
            Login(username[0], password[0]);

            executionLog.Log("PartnerReportReflectsColumnsSpecified", "Verify Page title");
            VerifyTitle("Dashboard");

            executionLog.Log("PartnerReportReflectsColumnsSpecified", "Redirect To Report Columns page");
            GetWebDriver().Navigate().GoToUrl("https://www.pegasus-test.com/newthemecorp/pegasustestoffice/rir/report_columns");
            residual_income_officePayoutHelper.WaitForWorkAround(2000);

            executionLog.Log("PartnerReportReflectsColumnsSpecified", "Check Merchant ID checkbox");
            bool checkbox = residual_income_officePayoutHelper.isChecked("//*[@id='RirOrgReportColumnMerchantNumber']");
            if (checkbox == false)
                residual_income_officePayoutHelper.ClickViaJavaScript("//*[@id='RirOrgReportColumnMerchantNumber']");
            else
            { }

            executionLog.Log("PartnerReportReflectsColumnsSpecified", "Uncheck Sales Transaction checkbox");
            bool checkbox2 = residual_income_officePayoutHelper.isChecked("//*[@id='RirOrgReportColumnSalesTrans']");
            if (checkbox == true)
                residual_income_officePayoutHelper.ClickViaJavaScript("//*[@id='RirOrgReportColumnSalesTrans']");
            else { }

            executionLog.Log("PartnerReportReflectsColumnsSpecified", "Uncheck Average Ticket checkbox");
            bool checkbox3 = residual_income_officePayoutHelper.isChecked("//*[@id='RirOrgReportColumnAvgTicket']");
            if (checkbox3 == true)
                residual_income_officePayoutHelper.ClickViaJavaScript("//*[@id='RirOrgReportColumnAvgTicket']");
            else
            { }

            executionLog.Log("PartnerReportReflectsColumnsSpecified", "Uncheck Office Income checkbox");
            bool checkbox4 = residual_income_officePayoutHelper.isChecked("//*[@id='RirOrgReportColumnOfficeIncome']");
            if (checkbox4 == true)
                residual_income_officePayoutHelper.ClickViaJavaScript("//*[@id='RirOrgReportColumnOfficeIncome']");
            else
            { }

            executionLog.Log("PartnerReportReflectsColumnsSpecified", "Uncheck Office Adjustment checkbox");
            bool checkbox5 = residual_income_officePayoutHelper.isChecked("//*[@id='RirOrgReportColumnAdjustment']");
            if (checkbox5 == true)
                residual_income_officePayoutHelper.ClickViaJavaScript("//*[@id='RirOrgReportColumnAdjustment']");
            else
            { }

            executionLog.Log("PartnerReportReflectsColumnsSpecified", "Uncheck Agent Revenue Share checkbox");
            bool checkbox6 = residual_income_officePayoutHelper.isChecked("//*[@id='RirOrgReportColumnRevShare']");
            if (checkbox5 == true)
                residual_income_officePayoutHelper.ClickViaJavaScript("//*[@id='RirOrgReportColumnRevShare']");
            else
            { }

            executionLog.Log("PartnerReportReflectsColumnsSpecified", "Click on Save button");
            residual_income_officePayoutHelper.ClickViaJavaScript("//*[@id='RirOrgReportColumnReportColumnsForm']/div[4]/div/button");

            executionLog.Log("VerifyCreatedIframeOnPartnerAssociationPage", "Wait for creation success text.");
            residual_income_officePayoutHelper.WaitForText("Report display columns updated successfully.", 10);

            executionLog.Log("PartnerReportReflectsColumnsSpecified", "Logout from the application.");
            VisitOffice("logout");

            executionLog.Log("PartnerReportReflectsColumnsSpecified", "Login with valid username and password");
            Login("aslamassociate", "123456");
            Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);
            residual_income_officePayoutHelper.WaitForWorkAround(3000);

            executionLog.Log("PartnerReportReflectsColumnsSpecified", "Redirect To Report Columns page");
            GetWebDriver().Navigate().GoToUrl("https://www.pegasus-test.com/newthemecorp/pegasustestoffice/partners/reports");
            residual_income_officePayoutHelper.WaitForWorkAround(2000);


            executionLog.Log("PartnerReportReflectsColumnsSpecified", "Select Reporting Period");
            residual_income_officePayoutHelper.Select("ReportingPeriod", "2017-03-01");

            executionLog.Log("PartnerReportReflectsColumnsSpecified", "Select File Date");
            residual_income_officePayoutHelper.Select("FileDate", "2017-03-16");

            executionLog.Log("PartnerReportReflectsColumnsSpecified", "Select Processor");
            residual_income_officePayoutHelper.Select("Processor", "First Data North");
            residual_income_officePayoutHelper.WaitForWorkAround(4000);

            executionLog.Log("PartnerReportReflectsColumnsSpecified", "Select File Format");
            residual_income_officePayoutHelper.Select("FileFormat", "Pegasus");

            executionLog.Log("PartnerReportReflectsColumnsSpecified", "Search Payouts");
            residual_income_officePayoutHelper.ClickElement("Searchreport");
            residual_income_officePayoutHelper.WaitForWorkAround(6000);

            executionLog.Log("PartnerReportReflectsColumnsSpecified", "Verify Reports Generated");
            residual_income_officePayoutHelper.VerifyPageText("NewThemeCorp Residual Reports");

            executionLog.Log("PartnerReportReflectsColumnsSpecified", "Verify Merchant Id");
            residual_income_officePayoutHelper.VerifyText("MerchantID", "Merchant Id");

            executionLog.Log("PartnerReportReflectsColumnsSpecified", "Verify Merchant Name");
            residual_income_officePayoutHelper.VerifyText("Merchantname", "Merchant Name");

            executionLog.Log("PartnerReportReflectsColumnsSpecified", "Verify Merchant Income");
            residual_income_officePayoutHelper.VerifyText("Merchantincome", "Merchant Income");

            executionLog.Log("PartnerReportReflectsColumnsSpecified", "Verify Merchant Expense");
            residual_income_officePayoutHelper.VerifyText("Merchantexpense", "Merchant Expense");

            executionLog.Log("PartnerReportReflectsColumnsSpecified", "Verify Sales Volume");
            residual_income_officePayoutHelper.VerifyText("Salesvol", "Sales Volume");

            executionLog.Log("PartnerReportReflectsColumnsSpecified", "Verify Sales Transaction");
            var value = residual_income_officePayoutHelper.GetText("//div[@id='report_container']/table/thead/tr/th[6]");
            Assert.IsFalse(value.Contains("Sales Transactions"));

            executionLog.Log("PartnerReportReflectsColumnsSpecified", "Verify Payout");
            residual_income_officePayoutHelper.VerifyText("Payout", "Payout");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("PartnerReportReflectsColumnsSpecified");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("PartnerReportReflectsColumnsSpecified");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Partner Report Reflects Columns Specified", "Bug", "Medium", "Corp Employee page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Partner Report Reflects Columns Specified");
                        TakeScreenshot("PartnerReportReflectsColumnsSpecified");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\PartnerReportReflectsColumnsSpecified.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("PartnerReportReflectsColumnsSpecified");
                        string id = loginHelper.getIssueID("Partner Report Reflects Columns Specified");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\PartnerReportReflectsColumnsSpecified.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Partner Report Reflects Columns Specified"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Partner Report Reflects Columns Specified");
                //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("PartnerReportReflectsColumnsSpecified");
                executionLog.WriteInExcel("Partner Report Reflects Columns Specified", Status, JIRA, "Corp Employee");


            }
        }
    }
}
