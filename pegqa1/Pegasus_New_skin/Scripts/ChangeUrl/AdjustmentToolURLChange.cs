using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class AdjustmentToolURLChange : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Url")]
        [TestCategory("TS3")]
        [TestCategory("ChangeUrl")]
        public void adjustmentToolURLChange()
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
                executionLog.Log("AdjustmentToolURLChange", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("AdjustmentToolURLChange", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("AdjustmentToolURLChange", "Goto User Residual Income >> Master Data >>  Adjustment Tool");
                VisitOffice("rir/adjustments_tool");
                residualIncome_MasterData_AdjustmentToolHelper.WaitForWorkAround(1000);

                executionLog.Log("AdjustmentToolURLChange", "Click On Adjustment Tool");
                residualIncome_MasterData_AdjustmentToolHelper.ClickElement("ClickOnAdjustmentFirst");
                residualIncome_MasterData_AdjustmentToolHelper.WaitForWorkAround(1000);

                executionLog.Log("AdjustmentToolURLChange", "Change the url with the url number of another office");
                VisitOffice("rir/adjustments_tool/edit/418");
                residualIncome_MasterData_AdjustmentToolHelper.WaitForWorkAround(2000);

                executionLog.Log("AdjustmentToolURLChange", "Verify Validation");
                residualIncome_MasterData_AdjustmentToolHelper.WaitForText("oops something went wrong", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("AdjustmentToolURLChange");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Adjustment Tool URL Change");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Adjustment Tool URL Change", "Bug", "Medium", "Adjustment Tool page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Adjustment Tool URL Change");
                        TakeScreenshot("AdjustmentToolURLChange");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AdjustmentToolURLChange.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("AdjustmentToolURLChange");
                        string id = loginHelper.getIssueID("Adjustment Tool URL Change");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AdjustmentToolURLChange.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Adjustment Tool URL Change"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Adjustment Tool URL Change");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("AdjustmentToolURLChange");
                executionLog.WriteInExcel("Adjustment Tool URL Change", Status, JIRA, "Residual Income");
            }
        }
    }
}
