using System;
using System.IO;
using LinqToExcel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class ResidualAdjustmentTool : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS5")]
        [TestCategory("NewSkinTaskJira")]
        public void residualAdjustmentTool()
        {
            string[] username = null;
            string[] password = null;
            string[] log = null;
            String JIRA = "";
            String Status = "Pass";

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_corp");
            password = oXMLData.getData("settings/Credentials", "password");
            log = oXMLData.getData("settings/URL", "logout");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var corpResidualIncome_Masterdata_AdjustmentToolHelper = new CorpResidualIncome_Masterdata_AdjustmentToolHelper(GetWebDriver());

            try
            {
                executionLog.Log("ResidualAdjustmentTool", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("ResidualAdjustmentTool", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("ResidualAdjustmentTool", "Go to Create Residual Adjustment Tool page");
                VisitCorp("rir/create");
                corpResidualIncome_Masterdata_AdjustmentToolHelper.WaitForWorkAround(3000);

                executionLog.Log("ResidualAdjustmentTool", "Verify title");
                VerifyTitle("Residual Master Rules - Create Adjustments");

                executionLog.Log("ResidualAdjustmentTool", "Click on 'Save' button without filling any field");
                corpResidualIncome_Masterdata_AdjustmentToolHelper.ClickElement("Save");
                corpResidualIncome_Masterdata_AdjustmentToolHelper.WaitForWorkAround(2000);

                executionLog.Log("ResidualAdjustmentTool", "Verify error message");
                corpResidualIncome_Masterdata_AdjustmentToolHelper.verifyElementPresent("RMPProcessorError");
                corpResidualIncome_Masterdata_AdjustmentToolHelper.WaitForWorkAround(1000);

                executionLog.Log("ResidualAdjustmentTool", "Verify error message");
                corpResidualIncome_Masterdata_AdjustmentToolHelper.verifyElementPresent("RMPSetNameError");
                corpResidualIncome_Masterdata_AdjustmentToolHelper.WaitForWorkAround(1000);

                executionLog.Log("ResidualAdjustmentTool", "Select Processor");
                corpResidualIncome_Masterdata_AdjustmentToolHelper.SelectByText("RMPProcessor", "First Data Omaha");
                corpResidualIncome_Masterdata_AdjustmentToolHelper.WaitForWorkAround(1000);

                executionLog.Log("ResidualAdjustmentTool", "Click on 'Save' button after selecting Processor");
                corpResidualIncome_Masterdata_AdjustmentToolHelper.ClickElement("Save");
                corpResidualIncome_Masterdata_AdjustmentToolHelper.WaitForWorkAround(1000);

                executionLog.Log("ResidualAdjustmentTool", "Verify error ");
                corpResidualIncome_Masterdata_AdjustmentToolHelper.verifyElementPresent("RMPSetNameError");
                corpResidualIncome_Masterdata_AdjustmentToolHelper.WaitForWorkAround(1000);

                executionLog.Log("ResidualAdjustmentTool", "Select Rule");
                corpResidualIncome_Masterdata_AdjustmentToolHelper.SelectByText("RMPRule", "Amount");
                corpResidualIncome_Masterdata_AdjustmentToolHelper.WaitForWorkAround(1000);

                executionLog.Log("ResidualAdjustmentTool", "Enter amount in alphabets");
                corpResidualIncome_Masterdata_AdjustmentToolHelper.TypeText("RMPRuleField", "Alpha");
                corpResidualIncome_Masterdata_AdjustmentToolHelper.WaitForWorkAround(1000);

                executionLog.Log("ResidualAdjustmentTool", "Click on anywhere");
                corpResidualIncome_Masterdata_AdjustmentToolHelper.ClickElement("RMPRule");
                corpResidualIncome_Masterdata_AdjustmentToolHelper.WaitForWorkAround(1000);

                executionLog.Log("ResidualAdjustmentTool", "Verify error displayed");
                corpResidualIncome_Masterdata_AdjustmentToolHelper.verifyElementPresent("RMPRuleError");
                corpResidualIncome_Masterdata_AdjustmentToolHelper.WaitForWorkAround(1000);

                executionLog.Log("ResidualAdjustmentTool", "Enter amount in numeric");
                corpResidualIncome_Masterdata_AdjustmentToolHelper.TypeText("RMPRuleField", "123");
                corpResidualIncome_Masterdata_AdjustmentToolHelper.WaitForWorkAround(1000);

                executionLog.Log("ResidualAdjustmentTool", "Click on anywhere");
                corpResidualIncome_Masterdata_AdjustmentToolHelper.ClickElement("RMPRule");
                corpResidualIncome_Masterdata_AdjustmentToolHelper.WaitForWorkAround(1000);

                executionLog.Log("ResidualAdjustmentTool", "Select Percantage");
                corpResidualIncome_Masterdata_AdjustmentToolHelper.SelectByText("RMPRule", "Percentage");
                corpResidualIncome_Masterdata_AdjustmentToolHelper.WaitForWorkAround(1000);

                executionLog.Log("ResidualAdjustmentTool", "Enter Percantage in alphabets");
                corpResidualIncome_Masterdata_AdjustmentToolHelper.TypeText("RMPRuleField", "Alpha");
                corpResidualIncome_Masterdata_AdjustmentToolHelper.WaitForWorkAround(1000);

                executionLog.Log("ResidualAdjustmentTool", "Click on anywher");
                corpResidualIncome_Masterdata_AdjustmentToolHelper.ClickElement("RMPRule");
                corpResidualIncome_Masterdata_AdjustmentToolHelper.WaitForWorkAround(1000);

                executionLog.Log("ResidualAdjustmentTool", "Verify error displayed");
                corpResidualIncome_Masterdata_AdjustmentToolHelper.verifyElementPresent("RMPRuleError");
                corpResidualIncome_Masterdata_AdjustmentToolHelper.WaitForWorkAround(1000);

                executionLog.Log("ResidualAdjustmentTool", "Enter Percantage in numeric");
                corpResidualIncome_Masterdata_AdjustmentToolHelper.TypeText("RMPRuleField", "13");
                corpResidualIncome_Masterdata_AdjustmentToolHelper.WaitForWorkAround(1000);

                executionLog.Log("ResidualAdjustmentTool", "Click on anywehe");
                corpResidualIncome_Masterdata_AdjustmentToolHelper.ClickElement("RMPRule");
                corpResidualIncome_Masterdata_AdjustmentToolHelper.WaitForWorkAround(1000);

                executionLog.Log("ResidualAdjustmentTool", "Select Ammount");
                corpResidualIncome_Masterdata_AdjustmentToolHelper.SelectByText("RMPRule", "Amount");
                corpResidualIncome_Masterdata_AdjustmentToolHelper.WaitForWorkAround(1000);

                executionLog.Log("ResidualAdjustmentTool", "Enter Ammount upto 3 decimal");
                corpResidualIncome_Masterdata_AdjustmentToolHelper.TypeText("RMPRuleField", "123.234");
                corpResidualIncome_Masterdata_AdjustmentToolHelper.WaitForWorkAround(1000);

                executionLog.Log("ResidualAdjustmentTool", "Click anywhere in the application.");
                corpResidualIncome_Masterdata_AdjustmentToolHelper.ClickElement("RMPRule");
                corpResidualIncome_Masterdata_AdjustmentToolHelper.WaitForWorkAround(1000);

                executionLog.Log("ResidualAdjustmentTool", "Verify error displayed");
                corpResidualIncome_Masterdata_AdjustmentToolHelper.verifyElementPresent("RMPRuleError");
                corpResidualIncome_Masterdata_AdjustmentToolHelper.WaitForWorkAround(1000);

                executionLog.Log("ResidualAdjustmentTool", "Select Percantage");
                corpResidualIncome_Masterdata_AdjustmentToolHelper.SelectByText("RMPRule", "Percentage");
                corpResidualIncome_Masterdata_AdjustmentToolHelper.WaitForWorkAround(1000);

                executionLog.Log("ResidualAdjustmentTool", "Enter Percantage more than 100");
                corpResidualIncome_Masterdata_AdjustmentToolHelper.TypeText("RMPRuleField", "1234");
                corpResidualIncome_Masterdata_AdjustmentToolHelper.WaitForWorkAround(1000);

                executionLog.Log("ResidualAdjustmentTool", "Click on any");
                corpResidualIncome_Masterdata_AdjustmentToolHelper.ClickElement("RMPRule");
                corpResidualIncome_Masterdata_AdjustmentToolHelper.WaitForWorkAround(2000);

                executionLog.Log("ResidualAdjustmentTool", "Verify error displayed");
                corpResidualIncome_Masterdata_AdjustmentToolHelper.verifyElementPresent("RMPRuleError");

                executionLog.Log("ResidualAdjustmentTool", "Click on Add another button.");
                corpResidualIncome_Masterdata_AdjustmentToolHelper.ClickElement("RMPAddRule");
                corpResidualIncome_Masterdata_AdjustmentToolHelper.WaitForWorkAround(2000);

                executionLog.Log("ResidualAdjustmentTool", "Verify Field added");
                corpResidualIncome_Masterdata_AdjustmentToolHelper.verifyElementPresent("RMPRuleError");

                executionLog.Log("ResidualAdjustmentTool", "Log out from the application");
                VisitCorp("logout");

            }

            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("ResidualAdjustmentTool");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Residual Adjustment Tool");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Residual Adjustment Tool", "Bug", "Medium", "Residual page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Residual Adjustment Tool");
                        TakeScreenshot("ResidualAdjustmentTool");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ResidualAdjustmentTool.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("ResidualAdjustmentTool");
                        string id = loginHelper.getIssueID("Residual Adjustment Tool");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ResidualAdjustmentTool.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Residual Adjustment Tool"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Residual Adjustment Tool");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("ResidualAdjustmentTool");
                executionLog.WriteInExcel("Residual Adjustment Tool", Status, JIRA, "Residual Adjustment");
            }
        }
    }
}
