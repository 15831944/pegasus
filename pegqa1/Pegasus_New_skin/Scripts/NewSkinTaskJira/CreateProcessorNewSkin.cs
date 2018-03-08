using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class CreateProcessorNewSkin : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS4")]
        [TestCategory("NewSkinTaskJira")]
        public void createProcessorNewSkin()
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
            var corpMasterData_ProcessorsHelper = new CorpMasterData_ProcessorsHelper(GetWebDriver());

            // VARIABLE
            var ProcessorName = "TestPro" + RandomNumber(99, 99999);
            var ProcessorCode = "Code" + RandomNumber(9, 999);

            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("CreateProcessorNewSkin", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("CreateProcessorNewSkin", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("CreateProcessorNewSkin", "Redirect at processors page.");
                VisitCorp("masterdata/processor_types");

                executionLog.Log("CreateProcessorNewSkin", "Click on Create");
                corpMasterData_ProcessorsHelper.ClickElement("Create");
                corpMasterData_ProcessorsHelper.WaitForWorkAround(3000);

                executionLog.Log("CreateProcessorNewSkin", "Click save");
                corpMasterData_ProcessorsHelper.ClickElement("Save");

                executionLog.Log("CreateProcessorNewSkin", "EnterProcessorName");
                corpMasterData_ProcessorsHelper.TypeText("ProcessorName", ProcessorName);

                executionLog.Log("CreateProcessorNewSkin", "EnterProcessorName");
                corpMasterData_ProcessorsHelper.TypeText("ProcessorCode", ProcessorCode);

                executionLog.Log("CreateProcessorNewSkin", "Click save");
                corpMasterData_ProcessorsHelper.ClickElement("Save");

                executionLog.Log("CreateProcessorNewSkin", "Verify Message");
                corpMasterData_ProcessorsHelper.WaitForText("Processor is successfully created!!", 10);

                executionLog.Log("CreateProcessorNewSkin", "Go to Processor type page");
                VisitCorp("masterdata/processor_types");

                executionLog.Log("CreateProcessorNewSkin", "Verify Page title");
                VerifyTitle("Master Processors");

                executionLog.Log("CreateProcessorNewSkin", "Enter Name to search");
                corpMasterData_ProcessorsHelper.TypeText("SearchProcessor", ProcessorName);
                corpMasterData_ProcessorsHelper.WaitForWorkAround(2000);

                executionLog.Log("CreateProcessorNewSkin", "Click Delete btn  ");
                corpMasterData_ProcessorsHelper.ClickElement("DeleteIcon");

                executionLog.Log("CreateProcessorNewSkin", "Accept alert message. ");
                corpMasterData_ProcessorsHelper.AcceptAlert();

                executionLog.Log("CreateProcessorNewSkin", "Wait for delete message. ");
                corpMasterData_ProcessorsHelper.WaitForText("The processor is successfully deleted!!", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CreateProcessorNewSkin");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Create Processor New Skin");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Create Processor New Skin", "Bug", "Medium", "Processor page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Create Processor New Skin");
                        TakeScreenshot("CreateProcessorNewSkin");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateProcessorNewSkin.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CreateProcessorNewSkin");
                        string id = loginHelper.getIssueID("Create Processor New Skin");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateProcessorNewSkin.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Create Processor New Skin"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Create Processor New Skin");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("CreateProcessorNewSkin");
                executionLog.WriteInExcel("Create Processor New Skin", Status, JIRA, "Corp Master Data");
            }
        }
    }
}