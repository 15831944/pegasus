using System;
using System.IO;
using LinqToExcel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class ProcessorDeleteTypo : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS5")]
        [TestCategory("NewSkinTaskJira")]
        public void processorDeleteTypo()
        {
            string[] username = null;
            string[] password = null;
            string[] log = null;
            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_corp");
            password = oXMLData.getData("settings/Credentials", "password");
            log = oXMLData.getData("settings/URL", "logout");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var corpMasterData_ProcessorsHelper = new CorpMasterData_ProcessorsHelper(GetWebDriver());
            String JIRA = "";
            String Status = "Pass";

            var Name = "Processortest" + RandomNumber(1, 100);
            var code = "" + RandomNumber(100, 500);

            try
            {
                executionLog.Log("ProcessorDeleteTypo", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("ProcessorDeleteTypo", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("ProcessorDeleteTypo", "Go to processor page");
                VisitCorp("masterdata/processor_types");
                corpMasterData_ProcessorsHelper.WaitForWorkAround(4000);

                executionLog.Log("ProcessorDeleteTypo", "Verify title");
                VerifyTitle("Master Processors");

                executionLog.Log("ProcessorDeleteTypo", "Click on Create button");
                corpMasterData_ProcessorsHelper.ClickElement("Create");
                corpMasterData_ProcessorsHelper.WaitForWorkAround(3000);

                executionLog.Log("ProcessorDeleteTypo", "Enter the processor Name");
                corpMasterData_ProcessorsHelper.TypeText("ProcessorName", Name);

                executionLog.Log("ProcessorDeleteTypo", "Enter the processor code");
                corpMasterData_ProcessorsHelper.TypeText("ProcessorCode", code);

                executionLog.Log("ProcessorDeleteTypo", "Click on Save button");
                corpMasterData_ProcessorsHelper.ClickElement("Save");
                corpMasterData_ProcessorsHelper.WaitForWorkAround(4000);

                executionLog.Log("ProcessorDeleteTypo", "Search the processor");
                corpMasterData_ProcessorsHelper.TypeText("SearchProcessor", Name);
                corpMasterData_ProcessorsHelper.WaitForWorkAround(2000);

                executionLog.Log("ProcessorDeleteTypo", "Click on 'Delete' button");
                corpMasterData_ProcessorsHelper.ClickElement("DeleteIcon");

                executionLog.Log("ProcessorDeleteTypo", "Verify Alert text");
                corpMasterData_ProcessorsHelper.VerifyAlertText("Are you sure you want to delete this processor permanently?");

                executionLog.Log("ProcessorDeleteTypo", "Logout from the application");
                VisitCorp("logout");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("ProcessorDeleteTypo");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Processor Delete Typo");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Processor Delete Typo", "Bug", "Medium", "Processor page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Processor Delete Typo");
                        TakeScreenshot("ProcessorDeleteTypo");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ProcessorDeleteTypo.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("ProcessorDeleteTypo");
                        string id = loginHelper.getIssueID("Processor Delete Typo");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ProcessorDeleteTypo.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Processor Delete Typo"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Processor Delete Typo");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("ProcessorDeleteTypo");
                executionLog.WriteInExcel("Processor Delete Typo", Status, JIRA, "Master Data");
            }
        }
    }
}
