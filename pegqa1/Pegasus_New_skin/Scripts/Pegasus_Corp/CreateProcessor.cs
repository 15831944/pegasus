using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class CreateProcessor : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Corp")]
        [TestCategory("TS7")]
        [TestCategory("Pegasus_Corp")]
        public void createProcessor()
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

                executionLog.Log("CreateProcessor", "Login with valid credential  Username");
                Login(username[0], password[0]);

                executionLog.Log("CreateProcessor", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("CreateProcessor", "Go to Processor type page");
                VisitCorp("masterdata/processor_types");

                executionLog.Log("CreateProcessor", "Verify Page title");
                VerifyTitle("Master Processors");

                executionLog.Log("CreateProcessor", "Click On Create");
                corpMasterdata_ProcessorHelper.ClickElement("Create");

                executionLog.Log("CreateProcessor", "Verify Page title");
                VerifyTitle("Manage Processor");

                executionLog.Log("CreateProcessor", "Enter Processor name");
                corpMasterdata_ProcessorHelper.TypeText("ProcessorName", name);

                executionLog.Log("CreateProcessor", "Enter ProcessorCode");
                corpMasterdata_ProcessorHelper.TypeText("ProcessorCode", Test);

                executionLog.Log("CreateProcessor", "Click On Save Btn");
                corpMasterdata_ProcessorHelper.ClickElement("Save");

                executionLog.Log("CreateProcessor", "Verify text present");
                corpMasterdata_ProcessorHelper.WaitForText("Processor is successfully created!!", 10);

                executionLog.Log("CreateProcessor", "Go to Processor type page");
                VisitCorp("masterdata/processor_types");

                executionLog.Log("CreateProcessor", "Verify Page title");
                VerifyTitle("Master Processors");

                executionLog.Log("CreateProcessor", "Enter Name to search");
                corpMasterdata_ProcessorHelper.TypeText("SearchProcessor", name);
                corpMasterdata_ProcessorHelper.WaitForWorkAround(2000);

                executionLog.Log("CreateProcessor", "Click Delete btn  ");
                corpMasterdata_ProcessorHelper.ClickElement("DeleteIcon");

                executionLog.Log("CreateProcessor", "Accept alert message. ");
                corpMasterdata_ProcessorHelper.AcceptAlert();

                executionLog.Log("CreateProcessor", "Wait for delete message. ");
                corpMasterdata_ProcessorHelper.WaitForText("The processor is successfully deleted!!", 10);

                VisitCorp("logout");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CreateProcessor");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Create Processor");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Create Processor", "Bug", "Medium", "Processer page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Create Processor");
                        TakeScreenshot("CreateProcessor");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateProcessor.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CreateProcessor");
                        string id = loginHelper.getIssueID("Create Processor");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateProcessor.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Create Processor"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Create Processor");
           //     executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("CreateProcessor");
                executionLog.WriteInExcel("Create Processor", Status, JIRA, "Corp Master Data");
            }
        }
    }
}
