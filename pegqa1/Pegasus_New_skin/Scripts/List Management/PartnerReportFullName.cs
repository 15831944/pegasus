using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class PartnerReportFullName : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS1")]
        [TestCategory("ListManagement")]
        public void partnerReportFullName()
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
            var residual_income_officePayoutHelper = new ResidualIncome_OfficePayoutHelper(GetWebDriver());

            // Variable 
            var Id = "12345" + GetRandomNumber();
            String Jira = "";
            String Status = "Pass";

            //try
            //{
                executionLog.Log("PartnerReportFullName", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("PartnerReportFullName", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("PartnerReportFullName", "Redirect To Report Columns page");
                GetWebDriver().Navigate().GoToUrl("https://www.pegasus-test.com/newthemecorp/pegasustestoffice/rir/reports");
                residual_income_officePayoutHelper.WaitForWorkAround(2000);

                executionLog.Log("PartnerReportFullName", "Select Reporting Period");
                residual_income_officePayoutHelper.Select("ReportingPeriod", "2018-04-01");

                executionLog.Log("PartnerReportFullName", "Select File Date");
                residual_income_officePayoutHelper.Select("FileDate", "2018-03-01");

                executionLog.Log("PartnerReportFullName", "Select Processor");
                residual_income_officePayoutHelper.Select("Processor", "FIRST BANK OF DELAWARE");
                residual_income_officePayoutHelper.WaitForWorkAround(4000);

                executionLog.Log("PartnerReportFullName", "Select File Format");
                residual_income_officePayoutHelper.Select("FileFormat", "Pegasus");

                executionLog.Log("PartnerReportFullName", "Search Payouts");
                residual_income_officePayoutHelper.ClickElement("SearchBtn");
                residual_income_officePayoutHelper.WaitForWorkAround(6000);

                executionLog.Log("PartnerReportFullName", "Select Partner Agent");
                residual_income_officePayoutHelper.ClickElement("AslamPartnerCheckBox");

                executionLog.Log("PartnerReportFullName", "Verify Reports Generated");
                residual_income_officePayoutHelper.VerifyPageText("NewThemeCorp Residual Reports");

                executionLog.Log("PartnerReportFullName", "Verify Partner Agent Name");
                String pname1 = residual_income_officePayoutHelper.GetText("//*[@id='right']/div[3]/div[1]/table/tbody/tr[3]/td[2]/span");

                executionLog.Log("PartnerReportFullName ", "Logout from the application.");
                VisitOffice("logout");

                executionLog.Log("PartnerReportFullName ", "Login with valid username and password");
                Login("aslampartneragent", "123456");
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);
                residual_income_officePayoutHelper.WaitForWorkAround(3000);

                executionLog.Log("PartnerReportFullName ", "Redirect To Report Columns page");
                GetWebDriver().Navigate().GoToUrl("https://www.pegasus-test.com/newthemecorp/pegasustestoffice/partners/reports");
                residual_income_officePayoutHelper.WaitForWorkAround(2000);

                executionLog.Log("PartnerReportFullName ", "Select Reporting Period");
                residual_income_officePayoutHelper.Select("ReportingPeriod", "2018-04-01");

                executionLog.Log("PartnerReportFullName ", "Select File Date");
                residual_income_officePayoutHelper.Select("FileDate", "2018-03-01");

                executionLog.Log("PartnerReportFullName ", "Select Processor");
                residual_income_officePayoutHelper.Select("Processor", "FIRST BANK OF DELAWARE");
                residual_income_officePayoutHelper.WaitForWorkAround(4000);

                executionLog.Log("PartnerReportFullName ", "Select File Format");
                residual_income_officePayoutHelper.Select("FileFormat", "Pegasus");

                executionLog.Log("PartnerReportFullName ", "Search Payouts");
                residual_income_officePayoutHelper.ClickElement("Searchreport");
                residual_income_officePayoutHelper.WaitForWorkAround(6000);

                executionLog.Log("PartnerReportFullName ", "Verify Reports Generated");
                residual_income_officePayoutHelper.VerifyPageText("NewThemeCorp Residual Reports");

                executionLog.Log("PartnerReportFullName", "Verify Partner Agent Name");
                String pname2 = residual_income_officePayoutHelper.GetText("//*[@id='right']/div[2]/table/tbody/tr[3]/td[2]");

                executionLog.Log("PartnerReportFullName", "Verify Partner Agent Full Name");
                Assert.AreEqual(pname1, pname2);
                Console.WriteLine("Partner Agent Full Name is Appearing");

                }
            //}
            //catch (Exception e)
            //{
            //    executionLog.Log("Error", e.StackTrace);
            //    Status = "Fail";

            //    String counter = executionLog.readLastLine("counter");
            //    String Description = executionLog.GetAllTextFile("PartnerReportFullName");
            //    String Error = executionLog.GetAllTextFile("Error");
            //    if (counter == "")
            //    {
            //        counter = "0";
            //    }
            //    bool result = loginHelper.CheckExstingIssue("Partner Report Full Name");
            //    if (!result)
            //    {
            //        if (Int16.Parse(counter) < 9)
            //        {
            //            executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
            //            loginHelper.CreateIssue("Partner Report Full Name", "Bug", "Medium", "Equipment page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
            //            string id = loginHelper.getIssueID("Rename ListManagement Item");
            //            TakeScreenshot("PartnerReportFullName");
            //            string directoryName = loginHelper.GetnewDirectoryName(GetPath());
            //            var location = directoryName + "\\PartnerReportFullName.png";
            //            loginHelper.AddAttachment(location, id);
            //        }
            //    }
            //    else
            //    {
            //        if (Int16.Parse(counter) < 9)
            //        {
            //            executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
            //            TakeScreenshot("PartnerReportFullName");
            //            string id = loginHelper.getIssueID("Partner Report Full Name");
            //            string directoryName = loginHelper.GetnewDirectoryName(GetPath());
            //            var location = directoryName + "\\PartnerReportFullName.png";
            //            loginHelper.AddAttachment(location, id);
            //            loginHelper.AddComment(loginHelper.getIssueID("Partner Report Full Name"), "This issue is still occurring");
            //        }
            //    }
            //    JIRA = loginHelper.getIssueID("Partner Report Full Name");
            //    //  executionLog.DeleteFile("Error");
            //    throw;

            //}
            //finally
            //{
            //    executionLog.DeleteFile("PartnerReportFullName");
            //    executionLog.WriteInExcel("Partner Report Full Name", Status, JIRA, "List Management");
            //}
        }
    }