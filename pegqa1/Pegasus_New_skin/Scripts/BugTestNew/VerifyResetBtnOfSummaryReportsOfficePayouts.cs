using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class VerifyResetBtnOfSummaryReportsOfficePayouts : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin")]
        [TestCategory("BugTestNew")]
        public void verifyResetBtnOfSummaryReportsOfficePayouts()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            //Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var residualIncome_OfficePayoutHelper = new ResidualIncome_OfficePayoutHelper(GetWebDriver());

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");
            
            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("VerifyResetBtnOfSummaryReportsOfficePayouts", "Login with valid credentials");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyResetBtnOfSummaryReportsOfficePayouts", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyResetBtnOfSummaryReportsOfficePayouts", "Go to Create Call page");
                VisitOffice("rir/summary_reports");
                residualIncome_OfficePayoutHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyResetBtnOfSummaryReportsOfficePayouts", "Observe Total records");
                string txt = residualIncome_OfficePayoutHelper.GetText("//td[@id='list1_pager_left']/div");
                residualIncome_OfficePayoutHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyResetBtnOfSummaryReportsOfficePayouts", "Click on Filters");
                residualIncome_OfficePayoutHelper.ClickElement("FiltersBtn");
                residualIncome_OfficePayoutHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyResetBtnOfSummaryReportsOfficePayouts", "Click on Processor drop down");
                residualIncome_OfficePayoutHelper.ClickElement("Processordrpdwn");
                residualIncome_OfficePayoutHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyResetBtnOfSummaryReportsOfficePayouts", "Select First Data North");
                residualIncome_OfficePayoutHelper.ClickElement("Proc3Optn");
                residualIncome_OfficePayoutHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyResetBtnOfSummaryReportsOfficePayouts", "Click on Apply Button");
                residualIncome_OfficePayoutHelper.ClickElement("ApplyBtn");
                residualIncome_OfficePayoutHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyResetBtnOfSummaryReportsOfficePayouts", "Click on Filters");
                residualIncome_OfficePayoutHelper.ClickElement("FiltersBtn");
                residualIncome_OfficePayoutHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyResetBtnOfSummaryReportsOfficePayouts", "Click on Reset Button");
                residualIncome_OfficePayoutHelper.ClickElement("ResetBtn");
                residualIncome_OfficePayoutHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyResetBtnOfSummaryReportsOfficePayouts", "Verify Reset button works");
                residualIncome_OfficePayoutHelper.VerifyText("NoOfRecrdsTxt", txt);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyResetBtnOfSummaryReportsOfficePayouts");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Reset Btn Of Summary Reports Office Payouts");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Reset Btn Of Summary Reports Office Payouts", "Bug", "Medium", "Summary Reports page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Reset Btn Of Summary Reports Office Payouts");
                        TakeScreenshot("VerifyResetBtnOfSummaryReportsOfficePayouts");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyResetBtnOfSummaryReportsOfficePayouts.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyResetBtnOfSummaryReportsOfficePayouts");
                        string id = loginHelper.getIssueID("Verify Reset Btn Of Summary Reports Office Payouts");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyResetBtnOfSummaryReportsOfficePayouts.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Reset Btn Of Summary Reports Office Payouts"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Reset Btn Of Summary Reports Office Payouts");
                //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyResetBtnOfSummaryReportsOfficePayouts");
                executionLog.WriteInExcel("Verify Reset Btn Of Summary Reports Office Payouts", Status, JIRA, "Residual Income Office Payouts");
            }
        }
    }
} 