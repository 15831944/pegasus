using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class CreateProcessorWithoutError : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin")]
        [TestCategory("Temp")]
        [TestCategory("TS6")]
        [TestCategory("Pegasus_Admin")]
        public void createProcessorWithoutError()
        {
            string[] username = null;
            string[] password = null;

            XMLParse oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_corp");
            password = oXMLData.getData("settings/Credentials", "password");

            var ProcessCode = "Code" + RandomNumber(1, 999);
            var ProcessName = "Process" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var corpMasterData_ProcessorsHelper = new CorpMasterData_ProcessorsHelper(GetWebDriver());

            try
            {
                executionLog.Log("CreateProcessorWithoutError", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("CreateProcessorWithoutError", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("CreateProcessorWithoutError", "Go to Processor page");
                VisitCorp("masterdata/processor_types");
                corpMasterData_ProcessorsHelper.WaitForWorkAround(3000);

                executionLog.Log("CreateProcessorWithoutError", "Verify title");
                VerifyTitle("Master Processors");

                executionLog.Log("CreateProcessorWithoutError", "Verify created Processor is available");
                bool available = corpMasterData_ProcessorsHelper.verifyAvatarAvailable(ProcessName);

                if (available)
                {
                    executionLog.Log("CreateProcessorWithoutError", "Delete the processor");
                    corpMasterData_ProcessorsHelper.deleteProcessor(ProcessName);
                }

                executionLog.Log("CreateProcessorWithoutError", "Go to create Processor page");
                VisitCorp("masterdata/manage_processors");
                corpMasterData_ProcessorsHelper.WaitForWorkAround(3000);

                executionLog.Log("CreateProcessorWithoutError", "Verify Title");
                VerifyTitle("Manage Processor");

                executionLog.Log("CreateProcessorWithoutError", "Enter Process name");
                corpMasterData_ProcessorsHelper.TypeText("ProcessorName", ProcessName);

                executionLog.Log("CreateProcessorWithoutError", "Enter Process Code");
                corpMasterData_ProcessorsHelper.TypeText("ProcessorCode", ProcessCode);

                executionLog.Log("CreateProcessorWithoutError", "Click on Save button");
                corpMasterData_ProcessorsHelper.ClickElement("Save");
                corpMasterData_ProcessorsHelper.WaitForWorkAround(3000);

                executionLog.Log("CreateProcessorWithoutError", "Verify title");
                VerifyTitle("Master Processors");

                executionLog.Log("CreateProcessorWithoutError", "Verify process added sussfully");
                Assert.IsTrue(corpMasterData_ProcessorsHelper.verifyAvatarAvailable(ProcessName));

                executionLog.Log("CreateProcessorWithoutError", "Delete created processor");
                corpMasterData_ProcessorsHelper.deleteProcessor(ProcessName);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CreateProcessorWithoutError");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Create Processor Without Error");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Create Processor Without Error", "Bug", "Medium", "Processor Page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Create Processor Without Error");
                        TakeScreenshot("CreateProcessorWithoutError");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateProcessorWithoutError.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CreateProcessorWithoutError");
                        string id = loginHelper.getIssueID("Create Processor Without Error");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateProcessorWithoutError.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Create Processor Without Error"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Create Processor Without Error");
            //    executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("CreateProcessorWithoutError");
                executionLog.WriteInExcel("Create Processor Without Error", Status, JIRA, "Office Master Data");
            }
        }
    }
}

