using System;
using System.IO;
using LinqToExcel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class ResidualAdjustmentTool1 : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS5")]
        [TestCategory("NewSkinTaskJira")]
        public void residualAdjustmentTool1()
        {
            string[] username = null;
            string[] password = null;
            string[] log = null;
            String JIRA = "";
            String Status = "Pass";

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");
            log = oXMLData.getData("settings/URL", "logout");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var residualIncome_OfficePayout_DetailedPayoutHelper = new ResidualIncome_OfficePayout_DetailedPayoutHelper(GetWebDriver());

            try
            {
                executionLog.Log("ResidualAdjustmentTool1", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("ResidualAdjustmentTool1", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("ResidualAdjustmentTool1", "Go to Residual Adjustment Tool page");
                VisitOffice("rir/adjustments_tool");
                residualIncome_OfficePayout_DetailedPayoutHelper.WaitForWorkAround(5000);

                executionLog.Log("ResidualAdjustmentTool1", "Click on Reporting Period");
                residualIncome_OfficePayout_DetailedPayoutHelper.ClickElement("RPMPeriod");
                residualIncome_OfficePayout_DetailedPayoutHelper.WaitForWorkAround(3000);

                executionLog.Log("ResidualAdjustmentTool1", "Verify Calendar available");
                residualIncome_OfficePayout_DetailedPayoutHelper.verifyElementPresent("RPMCalender");

                executionLog.Log("ResidualAdjustmentTool1", "Log out from the application");
                VisitOffice("logout");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("ResidualAdjustmentTool1");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Residual Adjustment Tool 1");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Residual Adjustment Tool 1", "Bug", "Medium", "Residual page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Residual Adjustment Tool 1");
                        TakeScreenshot("ResidualAdjustmentTool1");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ResidualAdjustmentTool1.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("ResidualAdjustmentTool1");
                        string id = loginHelper.getIssueID("Residual Adjustment Tool 1");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ResidualAdjustmentTool1.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Residual Adjustment Tool 1"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Residual Adjustment Tool 1");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("ResidualAdjustmentTool1");
                executionLog.WriteInExcel("Residual Adjustment Tool 1", Status, JIRA, "Residual Adjustment");
            }
        }
    }
}