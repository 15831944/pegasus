using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class DeleteProcessor : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Corp")]
        [TestCategory("TS7")]
        [TestCategory("Pegasus_Corp")]
        public void deleteProcessor()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_corp");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var corpMasterdata_ProcessorHelper = new CorpMasterdata_ProcessorHelper(GetWebDriver());

            // Variable
            var name = "Test" + GetRandomNumber();
            var Test = "New" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {

            executionLog.Log("DeleteProcessor", "Login with valid credential  Username");
            Login(username[0], password[0]);

            executionLog.Log("DeleteProcessor", "Verify Page title");
            VerifyTitle("Dashboard");
            Console.WriteLine("Redirected at Dashboard screen.");

            executionLog.Log("DeleteProcessor", "Go to Processor type page");
            VisitCorp("masterdata/processor_types");

            executionLog.Log("DeleteProcessor", "Verify Page title");
            VerifyTitle("Master Processors");

            executionLog.Log("DeleteProcessor", "Click On Create");
            corpMasterdata_ProcessorHelper.ClickElement("Create");

            executionLog.Log("DeleteProcessor", "Enter Processor name");
            corpMasterdata_ProcessorHelper.TypeText("ProcessorName", name);

            executionLog.Log("DeleteProcessor", "Enter ProcessorCode");
            corpMasterdata_ProcessorHelper.TypeText("ProcessorCode", Test);

            executionLog.Log("DeleteProcessor", "Click On Save Btn");
            corpMasterdata_ProcessorHelper.ClickElement("Save");

            executionLog.Log("DeleteProcessor", "Verify text present");
            corpMasterdata_ProcessorHelper.WaitForText("Processor is successfully created!!", 10);

            executionLog.Log("DeleteProcessor", "Search Processor");
            corpMasterdata_ProcessorHelper.TypeText("SearchProcessor", name);
            corpMasterdata_ProcessorHelper.WaitForWorkAround(2000);

            executionLog.Log("DeleteProcessor", "Click On Delete Icon");
            corpMasterdata_ProcessorHelper.ClickElement("DeleteIcon");

            executionLog.Log("DeleteProcessor", "Confirm Action to delete");
            corpMasterdata_ProcessorHelper.AcceptAlert();

            executionLog.Log("DeleteProcessor", "Verify Confirmation text");
            corpMasterdata_ProcessorHelper.WaitForText("The processor is successfully deleted!!", 10);

        }

            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("DeleteProcessor");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Delete Processor");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Delete Processor", "Bug", "Medium", "Processor page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Delete Processor");
                        TakeScreenshot("DeleteProcessor");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\DeleteProcessor.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("DeleteProcessor");
                        string id = loginHelper.getIssueID("Delete Processor");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\DeleteProcessor.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Delete Processor"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Delete Processor");
           //     executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("DeleteProcessor");
                executionLog.WriteInExcel("Delete Processor", Status, JIRA, "Corp Master Data");
            }
        }
    }
}  