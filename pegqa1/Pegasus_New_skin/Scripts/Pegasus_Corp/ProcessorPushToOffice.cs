using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class ProcessorPushToOffice : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Corp")]
        [TestCategory("TS8")]
        [TestCategory("Pegasus_Corp")]
        public void processorPushToOffice()
        {
            string[] username = null;
            string[] username1 = null;
            string[] password = null;


            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_corp");
            username1 = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var corpMasterData_ProcessorsHelper = new CorpMasterData_ProcessorsHelper(GetWebDriver());

            // Variable
            var name = "Test" + GetRandomNumber();
            var Test = "New" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("ProcessorPushToOffice", "Login with valid credential  Username");
                Login(username[0], password[0]);

                executionLog.Log("ProcessorPushToOffice", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("ProcessorPushToOffice", "Go to Processor type page");
                VisitCorp("masterdata/processor_types");
                corpMasterData_ProcessorsHelper.WaitForWorkAround(3000);

                executionLog.Log("ProcessorPushToOffice", "Verify Page title");
                VerifyTitle("Master Processors");

                executionLog.Log("ProcessorPushToOffice", "Click On Create");
                corpMasterData_ProcessorsHelper.ClickElement("Create");
                corpMasterData_ProcessorsHelper.WaitForWorkAround(3000);

                executionLog.Log("ProcessorPushToOffice", "Verify Page title");
                VerifyTitle("Manage Processor");

                executionLog.Log("ProcessorPushToOffice", "Enter Processor name");
                corpMasterData_ProcessorsHelper.TypeText("ProcessorName", name);

                executionLog.Log("ProcessorPushToOffice", "Enter ProcessorCode");
                corpMasterData_ProcessorsHelper.TypeText("ProcessorCode", Test);

                executionLog.Log("ProcessorPushToOffice", "Click On Save Btn");
                corpMasterData_ProcessorsHelper.ClickElement("Save");
                corpMasterData_ProcessorsHelper.WaitForWorkAround(1000);

                executionLog.Log("ProcessorPushToOffice", "Verify text present");
                corpMasterData_ProcessorsHelper.WaitForText("Processor is successfully created!!", 30);
                corpMasterData_ProcessorsHelper.WaitForWorkAround(2000);

                executionLog.Log("ProcessorPushToOffice", "Click On Push Office");
                corpMasterData_ProcessorsHelper.ClickElement("PushtoOffice");
                corpMasterData_ProcessorsHelper.AcceptAlert();
                corpMasterData_ProcessorsHelper.WaitForWorkAround(7000);

                executionLog.Log("ProcessorPushToOffice", "Logout from the application");
                VisitCorp("logout");

                executionLog.Log("ProcessorPushToOffice", "Login using office credentials");
                Login(username1[0], password[0]);
                //corpMasterData_ProcessorsHelper.WaitForWorkAround(2000);

                executionLog.Log("ProcessorPushToOffice", "Verify dashboard title.");
                VerifyTitle("Dashboard");

                executionLog.Log("ProcessorPushToOffice", "Redirect to Procesosr");
                VisitOffice("processor_types");
                corpMasterData_ProcessorsHelper.WaitForWorkAround(3000);

                executionLog.Log("ProcessorPushToOffice", "Logout button");
                VisitOffice("logout");

                executionLog.Log("ProcessorPushToOffice", "Login with valid credential");
                Login(username[0], password[0]);

                executionLog.Log("ProcessorPushToOffice", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("ProcessorPushToOffice", "Redirect to Procesosr");
                VisitCorp("masterdata/processor_types");
                corpMasterData_ProcessorsHelper.WaitForWorkAround(3000);

                //executionLog.Log("ProcessorPushToOffice", "Wait for element to be visible.");
                //corpMasterData_ProcessorsHelper.WaitForElementPresent("SearchProcessor", 10);

                executionLog.Log("ProcessorPushToOffice", "Enter Name to search");
                corpMasterData_ProcessorsHelper.TypeText("SearchProcessor", name);
                corpMasterData_ProcessorsHelper.WaitForWorkAround(2000);

                executionLog.Log("ProcessorPushToOffice", "Click Delete btn  ");
                corpMasterData_ProcessorsHelper.ClickElement("DeleteIcon");

                executionLog.Log("ProcessorPushToOffice", "Accept alert message. ");
                corpMasterData_ProcessorsHelper.AcceptAlert();

                executionLog.Log("ProcessorPushToOffice", "Wait for delete message. ");
                corpMasterData_ProcessorsHelper.WaitForText("The processor is successfully deleted!!", 10);

            }

            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("ProcessorPushToOffice");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Processor Push To Office");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Processor Push To Office", "Bug", "Medium", "prcessor page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Processor Push To Office");
                        TakeScreenshot("ProcessorPushToOffice");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ProcessorPushToOffice.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("ProcessorPushToOffice");
                        string id = loginHelper.getIssueID("Processor Push To Office");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ProcessorPushToOffice.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Processor Push To Office"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Processor Push To Office");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("ProcessorPushToOffice");
                executionLog.WriteInExcel("Processor Push To Office", Status, JIRA, "Corp Master Data");
            }
        }
    }
}