using System;
using System.IO;
using LinqToExcel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class ExportError : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS5")]
        [TestCategory("NewSkinTaskJira")]
        public void exportError()
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
            var residualIncome_OfficePayout_DetailedPayoutHelper = new ResidualIncome_OfficePayout_DetailedPayoutHelper(GetWebDriver());

            String Status = "Pass";
            String JIRA = "";

            try
            {
                executionLog.Log("ExportError", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("ExportError", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("ExportError", "Navigate to the detailed payout page.");
                VisitOffice("rir/detailed_payouts");

                executionLog.Log("ExportError", "verify title");
                VerifyTitle("Residual Income - Payouts");

                executionLog.Log("ExportError", "Click on Advance filter");
                residualIncome_OfficePayout_DetailedPayoutHelper.ClickElement("Advance");

                executionLog.Log("ExportError", "Click on Apply buton");
                residualIncome_OfficePayout_DetailedPayoutHelper.ClickElement("Apply");

                executionLog.Log("ExportError", "Click on Export buton");
                residualIncome_OfficePayout_DetailedPayoutHelper.ClickElement("Exp");

                executionLog.Log("ExportError", "Click on Export as csv link");
                residualIncome_OfficePayout_DetailedPayoutHelper.ClickElement("ExpCSV");

                executionLog.Log("ExportError", "Click on Advance filter");
                residualIncome_OfficePayout_DetailedPayoutHelper.ClickElement("Advance");
                Console.WriteLine("Done");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";
                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("ExportError");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Export Error");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Export Error", "Bug", "Medium", "Residual Income page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Export Error");
                        TakeScreenshot("ExportError");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ExportError.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("ExportError");
                        string id = loginHelper.getIssueID("Export Error");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ExportError.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Export Error"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Export Error");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("ExportError");
                executionLog.WriteInExcel("Export Error", Status, JIRA, "Residual Adjustment");
            }
        }
    }
}