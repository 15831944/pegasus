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
    public class CreateAdminProcessor : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin")]
        [TestCategory("TS6")]
        [TestCategory("Pegasus_Admin")]
        public void createAdminProcessor()
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

            // Variable
            String name = "Test" + GetRandomNumber();
            String code = "12" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("CreateAdminProcessor", " Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("CreateAdminProcessor", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("CreateAdminProcessor", "Click On  Admin");
                VisitOffice("admin");

                executionLog.Log("CreateAdminProcessor", "Redirect To URL");
                VisitOffice("processor_types");

                executionLog.Log("CreateAdminProcessor", "Verify title");
                VerifyTitle("Master Processors");

                executionLog.Log("CreateAdminProcessor", " Click On Create");
                masterData_ProcessorHelper.ClickElement("Create");

                executionLog.Log("CreateAdminProcessor", "Verify title");
                VerifyTitle("Manage Processor");

                executionLog.Log("CreateAdminProcessor", "Enter Processor Name");
                masterData_ProcessorHelper.TypeText("ProcessorName", name);

                executionLog.Log("CreateAdminProcessor", " Enter On ProcessorCode");
                masterData_ProcessorHelper.TypeText("ProcessorCode", code);

                executionLog.Log("CreateAdminProcessor", "  Click on Save button");
                masterData_ProcessorHelper.ClickElement("Save");

                executionLog.Log("CreateAdminProcessor", "Wait for text");
                masterData_ProcessorHelper.WaitForText("The processor is successfully created!!", 30);

                executionLog.Log("CreateAdminProcessor", " Search processor name");
                masterData_ProcessorHelper.TypeText("SearchProcessorName", name);

                executionLog.Log("CreateAdminProcessor", "Search Processor Code");
                masterData_ProcessorHelper.TypeText("SearchProcessorCode", code);

                executionLog.Log("CreateAdminProcessor", "  Click on delete icon");
                masterData_ProcessorHelper.ClickElement("DeleteIcon");

                executionLog.Log("CreateAdminProcessor", "  Accept alert message.");
                masterData_ProcessorHelper.AcceptAlert();

                executionLog.Log("CreateAdminProcessor", "  Verify text");
                masterData_ProcessorHelper.WaitForText("The processor is successfully deleted!!", 10);


            }
            catch (Exception e)
            {
                Console.WriteLine("ERRROROOR");
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CreateAdminProcessor");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Create Admin Processor");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Create Admin Processor", "Bug", "Medium", "Processor page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Create Admin Processor");
                        TakeScreenshot("CreateAdminProcessor");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateAdminProcessor.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CreateAdminProcessor");
                        string id = loginHelper.getIssueID("Create Admin Processor");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateAdminProcessor.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Create Admin Processor"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Create Admin Processor");
              //  executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("CreateAdminProcessor");
                executionLog.WriteInExcel("Create Admin Processor", Status, JIRA, "Leads/Client Management");
            }
        }
    }
}
