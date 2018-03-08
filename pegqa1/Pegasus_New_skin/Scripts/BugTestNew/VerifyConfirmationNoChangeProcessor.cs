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
    public class VerifyConfirmationNoChangeProcessor : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS3")]
        [TestCategory("BugTestNew")]
        public void verifyConfirmationNoChangeProcessor()
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
                executionLog.Log("VerifyConfirmationNoChangeProcessor", " Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyConfirmationNoChangeProcessor", "Verify Page title.");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyConfirmationNoChangeProcessor", "Redirect at admin page.");
                VisitOffice("admin");

                executionLog.Log("VerifyConfirmationNoChangeProcessor", "Redirect To Processor types page.");
                VisitOffice("processor_types");

                executionLog.Log("VerifyConfirmationNoChangeProcessor", "Verify title");
                VerifyTitle("Master Processors");

                executionLog.Log("VerifyConfirmationNoChangeProcessor", " Click On Create");
                masterData_ProcessorHelper.ClickElement("Create");

                executionLog.Log("VerifyConfirmationNoChangeProcessor", "Verify title");
                VerifyTitle("Manage Processor");

                executionLog.Log("VerifyConfirmationNoChangeProcessor", "Enter Processor Name");
                masterData_ProcessorHelper.TypeText("ProcessorName", name);

                executionLog.Log("VerifyConfirmationNoChangeProcessor", " Enter ProcessorCode");
                masterData_ProcessorHelper.TypeText("ProcessorCode", code);

                executionLog.Log("VerifyConfirmationNoChangeProcessor", "Click on Save button");
                masterData_ProcessorHelper.ClickElement("Save");

                executionLog.Log("VerifyConfirmationNoChangeProcessor", "Wait for text");
                masterData_ProcessorHelper.WaitForText("The processor is successfully created!!", 10);

                executionLog.Log("VerifyConfirmationNoChangeProcessor", " Search processor name");
                masterData_ProcessorHelper.TypeText("SearchProcessorName", name);
                masterData_ProcessorHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyConfirmationNoChangeProcessor", "  Click on delete icon");
                masterData_ProcessorHelper.ClickElement("EditIcon");

                executionLog.Log("VerifyConfirmationNoChangeProcessor", "  Click on Save button");
                masterData_ProcessorHelper.ClickElement("Save");
                masterData_ProcessorHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyConfirmationNoChangeProcessor", "  Verify text");
                masterData_ProcessorHelper.WaitForText("The processor is successfully updated!!", 10);

                executionLog.Log("VerifyConfirmationNoChangeProcessor", "Redirect To URL");
                VisitOffice("processor_types");

                executionLog.Log("VerifyConfirmationNoChangeProcessor", "Verify title");
                VerifyTitle("Master Processors");
                masterData_ProcessorHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyConfirmationNoChangeProcessor", " Search processor name");
                masterData_ProcessorHelper.TypeText("SearchProcessorName", name);
                masterData_ProcessorHelper.WaitForWorkAround(4000);

                executionLog.Log("VerifyConfirmationNoChangeProcessor", "  Click on delete icon");
                masterData_ProcessorHelper.ClickElement("DeleteIcon");
                masterData_ProcessorHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyConfirmationNoChangeProcessor", "  Accept alert message.");
                masterData_ProcessorHelper.AcceptAlert();

                executionLog.Log("VerifyConfirmationNoChangeProcessor", "  Verify text");
                masterData_ProcessorHelper.WaitForText("The processor is successfully deleted!!", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyConfirmationNoChangeProcessor");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("VerifyConfirmationNoChangeProcessor");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("VerifyConfirmationNoChangeProcessor", "Bug", "Medium", "Processor page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("VerifyConfirmationNoChangeProcessor");
                        TakeScreenshot("VerifyConfirmationNoChangeProcessor");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyConfirmationNoChangeProcessor.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyConfirmationNoChangeProcessor");
                        string id = loginHelper.getIssueID("Verify Confirmation NoChange Processor");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyConfirmationNoChangeProcessor.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Confirmation NoChange Processor"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Confirmation NoChange Processor");
           //     executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("VerifyConfirmationNoChangeProcessor");
                executionLog.WriteInExcel("Verify Confirmation NoChange Processor", Status, JIRA, "Office Master Data");
            }
        }
    }
}
