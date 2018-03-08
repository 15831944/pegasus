using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class ProcessorEditWithalreadyExistingNameIssue : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin")]
        [TestCategory("Fail")]
        [TestCategory("TS8")]
        [TestCategory("Pegasus_Corp")]
        public void processorEditWithalreadyExistingNameIssue()
        {
            string[] username = null;
            string[] password = null;

            XMLParse oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var masterData_ProcessorHelper = new MasterData_ProcessorsHelper(GetWebDriver());

            // Variable Random
            var name = "Test" + GetRandomNumber();
            var code = "12" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";


            try
            {
                executionLog.Log("ProcessorEditWithalreadyExistingNameIssue", " Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("ProcessorEditWithalreadyExistingNameIssue", "Verify Page title.");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("ProcessorEditWithalreadyExistingNameIssue", "Redirect at admin page.");
                VisitOffice("admin");
                masterData_ProcessorHelper.WaitForWorkAround(5000);

                executionLog.Log("ProcessorEditWithalreadyExistingNameIssue", "Redirect To Processor types page.");
                VisitOffice("processor_types");
                masterData_ProcessorHelper.WaitForWorkAround(5000);

                executionLog.Log("ProcessorEditWithalreadyExistingNameIssue", "Verify title");
                VerifyTitle("Master Processors");

                executionLog.Log("ProcessorEditWithalreadyExistingNameIssue", " Click On Create");
                masterData_ProcessorHelper.ClickElement("Create");
                masterData_ProcessorHelper.WaitForWorkAround(5000);

                executionLog.Log("ProcessorEditWithalreadyExistingNameIssue", "Verify title");
                VerifyTitle("Manage Processor");

                executionLog.Log("ProcessorEditWithalreadyExistingNameIssue", "Enter Processor Name");
                masterData_ProcessorHelper.TypeText("ProcessorName", name);

                executionLog.Log("ProcessorEditWithalreadyExistingNameIssue", " Enter ProcessorCode");
                masterData_ProcessorHelper.TypeText("ProcessorCode", code);

                executionLog.Log("ProcessorEditWithalreadyExistingNameIssue", "Click on Save button");
                masterData_ProcessorHelper.ClickElement("Save");

                executionLog.Log("ProcessorEditWithalreadyExistingNameIssue", "Wait for text");
                masterData_ProcessorHelper.WaitForText("The processor is successfully created!!", 10);
            masterData_ProcessorHelper.WaitForWorkAround(2000);

                executionLog.Log("ProcessorEditWithalreadyExistingNameIssue", " Search processor name");
                masterData_ProcessorHelper.TypeText("SearchProcessorName", name);
                masterData_ProcessorHelper.WaitForWorkAround(5000);

                executionLog.Log("ProcessorEditWithalreadyExistingNameIssue", "  Click on delete icon");
                masterData_ProcessorHelper.ClickElement("EditIcon");
                masterData_ProcessorHelper.WaitForWorkAround(3000);

                executionLog.Log("ProcessorEditWithalreadyExistingNameIssue", " Search processor name");
                masterData_ProcessorHelper.TypeText("ProcessorName", "Transfirst");

                executionLog.Log("ProcessorEditWithalreadyExistingNameIssue", "  Click on Save button");
                masterData_ProcessorHelper.ClickElement("Save");
                masterData_ProcessorHelper.WaitForWorkAround(3000);

                executionLog.Log("ProcessorEditWithalreadyExistingNameIssue", "  Verify text");
                masterData_ProcessorHelper.WaitForText("The processor already exists.", 20);

                executionLog.Log("ProcessorEditWithalreadyExistingNameIssue", "Redirect To URL");
                VisitOffice("processor_types");
                masterData_ProcessorHelper.WaitForWorkAround(4000);

                executionLog.Log("ProcessorEditWithalreadyExistingNameIssue", "Verify title");
                VerifyTitle("Master Processors");

                executionLog.Log("ProcessorEditWithalreadyExistingNameIssue", " Search processor name");
                masterData_ProcessorHelper.TypeText("SearchProcessorName", name);
                masterData_ProcessorHelper.WaitForWorkAround(5000);

                executionLog.Log("ProcessorEditWithalreadyExistingNameIssue", "  Click on delete icon");
                masterData_ProcessorHelper.ClickElement("DeleteIcon");
                masterData_ProcessorHelper.WaitForWorkAround(1000);

                executionLog.Log("ProcessorEditWithalreadyExistingNameIssue", "  Accept alert message.");
                masterData_ProcessorHelper.AcceptAlert();

                executionLog.Log("ProcessorEditWithalreadyExistingNameIssue", "  Verify text");
                masterData_ProcessorHelper.WaitForText("The processor is successfully deleted!!", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("ProcessorEditWithalreadyExistingNameIssue");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("ProcessorEditWithalreadyExistingNameIssue");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("ProcessorEditWithalreadyExistingNameIssue", "Bug", "Medium", "Processor page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("ProcessorEditWithalreadyExistingNameIssue");
                        TakeScreenshot("ProcessorEditWithalreadyExistingNameIssue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ProcessorEditWithalreadyExistingNameIssue.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("ProcessorEditWithalreadyExistingNameIssue");
                        string id = loginHelper.getIssueID("Processor Edit With already Existing Name Issue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ProcessorEditWithalreadyExistingNameIssue.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Processor Edit With already Existing Name Issue"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Processor Edit With already Existing Name Issue");
             //   executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("ProcessorEditWithalreadyExistingNameIssue");
                executionLog.WriteInExcel("Processor Edit With already Existing Name Issue", Status, JIRA, "Masterdata");
            }
        }
    }
}
