using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class AdjustmentChangeUrl : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS1")]
        [TestCategory("BugTest")]
        public void adjustmentChangeUrl()
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
            var residualIncome_MasterData_AdjustmentToolHelper = new ResidualIncome_MasterDataHelper(GetWebDriver());

            // Variable

            var FirstName = "Test" + GetRandomNumber();
            var LastName = "Tester" + GetRandomNumber();
            var Number = "12345678" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("AdjustmentChangeUrl", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("AdjustmentChangeUrl", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("AdjustmentChangeUrl", "Go to URL");
                VisitOffice("rir/adjustments_tool");
                residualIncome_MasterData_AdjustmentToolHelper.WaitForWorkAround(1000);

                executionLog.Log("AdjustmentChangeUrl", "Click On Adjustment First");
                residualIncome_MasterData_AdjustmentToolHelper.ClickElement("ClickOnAdjustmentFirst");
                residualIncome_MasterData_AdjustmentToolHelper.WaitForWorkAround(1000);

                executionLog.Log("AdjustmentChangeUrl", "Chnage URL of the page");
                VisitOffice("rir/adjustments_tool/edit/20");
                residualIncome_MasterData_AdjustmentToolHelper.WaitForWorkAround(1000);

                executionLog.Log("AdjustmentChangeUrl", "Verify text.");
                residualIncome_MasterData_AdjustmentToolHelper.WaitForText("oops something went wrong", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("AdjustmentChangeUrl");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Adjustment Change Url");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Adjustment Change Url", "Bug", "Medium", "Residual Income", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Adjustment Change Url");
                        TakeScreenshot("AdjustmentChangeUrl");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AdjustmentChangeUrl.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("AdjustmentChangeUrl");
                        string id = loginHelper.getIssueID("Adjustment Change Url");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AdjustmentChangeUrl.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Adjustment Change Url"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Adjustment Change Url");
                //executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("AdjustmentChangeUrl");
                executionLog.WriteInExcel("Adjustment Change Url", Status, JIRA, "Office Residual Income");
            }
        }
    }
}
