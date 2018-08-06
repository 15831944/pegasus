using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class PrintReportInPartnerAgent  : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin")]
        [TestCategory("Fail")]
        [TestCategory("ListManagement")]
        public void printReportInPartnerAgent ()
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

            executionLog.Log("PrintReportInPartnerAgent ", "Login with valid username and password");
            Login(username[0], password[0]);

            executionLog.Log("PrintReportInPartnerAgent ", "Verify Page title");
            VerifyTitle("Dashboard");

            executionLog.Log("PrintReportInPartnerAgent ", "Redirect To Report Columns page");
            GetWebDriver().Navigate().GoToUrl("https://www.pegasus-test.com/newthemecorp/pegasustestoffice/rir/report_columns");
            residual_income_officePayoutHelper.WaitForWorkAround(2000);

            executionLog.Log("PrintReportInPartnerAgent ", "Check Merchant ID checkbox");
            bool checkbox = residual_income_officePayoutHelper.isChecked("//*[@id='RirOrgReportColumnMerchantNumber']");
            if (checkbox == false)
                residual_income_officePayoutHelper.ClickViaJavaScript("//*[@id='RirOrgReportColumnMerchantNumber']");
            else
            { }

            executionLog.Log("PrintReportInPartnerAgent ", "Uncheck Sales Transaction checkbox");
            bool checkbox2 = residual_income_officePayoutHelper.isChecked("//*[@id='RirOrgReportColumnSalesTrans']");
            if (checkbox == true)
                residual_income_officePayoutHelper.ClickViaJavaScript("//*[@id='RirOrgReportColumnSalesTrans']");
            else { }

            executionLog.Log("PrintReportInPartnerAgent ", "Uncheck Average Ticket checkbox");
            bool checkbox3 = residual_income_officePayoutHelper.isChecked("//*[@id='RirOrgReportColumnAvgTicket']");
            if (checkbox3 == true)
                residual_income_officePayoutHelper.ClickViaJavaScript("//*[@id='RirOrgReportColumnAvgTicket']");
            else
            { }

            executionLog.Log("PrintReportInPartnerAgent ", "Uncheck Office Income checkbox");
            bool checkbox4 = residual_income_officePayoutHelper.isChecked("//*[@id='RirOrgReportColumnOfficeIncome']");
            if (checkbox4 == true)
                residual_income_officePayoutHelper.ClickViaJavaScript("//*[@id='RirOrgReportColumnOfficeIncome']");
            else
            { }

            executionLog.Log("PrintReportInPartnerAgent ", "Uncheck Office Adjustment checkbox");
            bool checkbox5 = residual_income_officePayoutHelper.isChecked("//*[@id='RirOrgReportColumnAdjustment']");
            if (checkbox5 == true)
                residual_income_officePayoutHelper.ClickViaJavaScript("//*[@id='RirOrgReportColumnAdjustment']");
            else
            { }

            executionLog.Log("PrintReportInPartnerAgent ", "Uncheck Agent Revenue Share checkbox");
            bool checkbox6 = residual_income_officePayoutHelper.isChecked("//*[@id='RirOrgReportColumnRevShare']");
            if (checkbox5 == true)
                residual_income_officePayoutHelper.ClickViaJavaScript("//*[@id='RirOrgReportColumnRevShare']");
            else
            { }

            executionLog.Log("PrintReportInPartnerAgent ", "Click on Save button");
            residual_income_officePayoutHelper.ClickViaJavaScript("//*[@id='RirOrgReportColumnReportColumnsForm']/div[4]/div/button");

            executionLog.Log("VerifyCreatedIframeOnPartnerAssociationPage", "Wait for creation success text.");
            residual_income_officePayoutHelper.WaitForText("Report display columns updated successfully.", 10);

            executionLog.Log("PrintReportInPartnerAgent ", "Logout from the application.");
            VisitOffice("logout");

            executionLog.Log("PrintReportInPartnerAgent ", "Login with valid username and password");
            Login("aslampartneragent", "123456");
            Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);
            residual_income_officePayoutHelper.WaitForWorkAround(3000);

            executionLog.Log("PrintReportInPartnerAgent ", "Redirect To Report Columns page");
            GetWebDriver().Navigate().GoToUrl("https://www.pegasus-test.com/newthemecorp/pegasustestoffice/partners/reports");
            residual_income_officePayoutHelper.WaitForWorkAround(2000);


            executionLog.Log("PrintReportInPartnerAgent ", "Select Reporting Period");
            residual_income_officePayoutHelper.Select("ReportingPeriod", "2018-04-01");

            executionLog.Log("PrintReportInPartnerAgent ", "Select File Date");
            residual_income_officePayoutHelper.Select("FileDate", "2018-03-01");

            executionLog.Log("PrintReportInPartnerAgent ", "Select Processor");
            residual_income_officePayoutHelper.Select("Processor", "FIRST BANK OF DELAWARE");
            residual_income_officePayoutHelper.WaitForWorkAround(4000);

            executionLog.Log("PrintReportInPartnerAgent ", "Select File Format");
            residual_income_officePayoutHelper.Select("FileFormat", "Pegasus");

            executionLog.Log("PrintReportInPartnerAgent ", "Search Payouts");
            residual_income_officePayoutHelper.ClickElement("Searchreport");
            residual_income_officePayoutHelper.WaitForWorkAround(6000);

            executionLog.Log("PrintReportInPartnerAgent ", "Verify Reports Generated");
            residual_income_officePayoutHelper.VerifyPageText("NewThemeCorp Residual Reports");

            executionLog.Log("PrintReportInPartnerAgent ", "Click on Print button");
            residual_income_officePayoutHelper.ClickElement("Print");
            residual_income_officePayoutHelper.WaitForWorkAround(3000);


            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("PrintReportInPartnerAgent ");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("PrintReportInPartnerAgent ");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Print Report In Partner Agent ", "Bug", "Medium", "Corp Employee page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Print Report In Partner Agent ");
                        TakeScreenshot("PrintReportInPartnerAgent ");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\PrintReportInPartnerAgent .png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("PrintReportInPartnerAgent ");
                        string id = loginHelper.getIssueID("Print Report In Partner Agent ");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\PrintReportInPartnerAgent .png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Print Report In Partner Agent "), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Print Report In Partner Agent ");
                //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("PrintReportInPartnerAgent ");
                executionLog.WriteInExcel("Print Report In Partner Agent ", Status, JIRA, "Corp Employee");


            }
        }
    }
}
