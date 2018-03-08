using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class EditProcessor : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Corp")]
        [TestCategory("TS7")]
        [TestCategory("Pegasus_Corp")]
        public void editProcessor()
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

            // Variable
            var name = "Test" + GetRandomNumber();
            var EditName = "EditedTest" + GetRandomNumber();
            var Test = "New" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("EditProcessor", "Login with valid credential  Username");
                Login(username[0], password[0]);

                executionLog.Log("EditProcessor", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("EditProcessor", "Click to Import");
                VisitCorp("masterdata/processor_types");

                executionLog.Log("EditProcessor", "Verify Page title");
                VerifyTitle("Master Processors");

                executionLog.Log("EditProcessor", "Click On Create");
                corpMasterData_ProcessorsHelper.ClickElement("Create");

                executionLog.Log("EditProcessor", "Enter Processor name");
                corpMasterData_ProcessorsHelper.TypeText("ProcessorName", name);

                executionLog.Log("EditProcessor", "Enter ProcessorCode");
                corpMasterData_ProcessorsHelper.TypeText("ProcessorCode", Test);

                executionLog.Log("EditProcessor", "Click On Save Btn");
                corpMasterData_ProcessorsHelper.ClickElement("Save");
                corpMasterData_ProcessorsHelper.WaitForWorkAround(3000);

                executionLog.Log("EditProcessor", "Search Processor");
                corpMasterData_ProcessorsHelper.TypeText("SearchProcessor", name);
                corpMasterData_ProcessorsHelper.WaitForWorkAround(2000);

                executionLog.Log("EditProcessor", "Click On Edit Icon");
                corpMasterData_ProcessorsHelper.ClickElement("ClickEditIcon");
                corpMasterData_ProcessorsHelper.WaitForWorkAround(3000);

                executionLog.Log("EditProcessor", "Enter Processor name");
                corpMasterData_ProcessorsHelper.TypeText("ProcessorName", EditName);

                executionLog.Log("EditProcessor", "Enter ProcessorCode");
                corpMasterData_ProcessorsHelper.TypeText("ProcessorCode", Test);

                executionLog.Log("EditProcessor", "Click on Save  button");
                corpMasterData_ProcessorsHelper.ClickElement("Save");
                corpMasterData_ProcessorsHelper.WaitForWorkAround(3000);

                executionLog.Log("EditProcessor", "Verify page text");
                corpMasterData_ProcessorsHelper.WaitForText("Processor is successfully updated!!", 30);
                corpMasterData_ProcessorsHelper.WaitForWorkAround(3000);

                executionLog.Log("EditProcessor", "Search Processor");
                corpMasterData_ProcessorsHelper.TypeText("SearchProcessor", EditName);
                corpMasterData_ProcessorsHelper.WaitForWorkAround(3000);

                executionLog.Log("EditProcessor", "Click On Edit Icon");
                corpMasterData_ProcessorsHelper.ClickElement("ClickEditIcon");

                executionLog.Log("EditProcessor", "Verify text on page.");
                corpMasterData_ProcessorsHelper.VerifyPageText(EditName);

                executionLog.Log("EditProcessor", "Go to Processor type page");
                VisitCorp("masterdata/processor_types");

                executionLog.Log("EditProcessor", "Verify Page title");
                VerifyTitle("Master Processors");

                executionLog.Log("EditProcessor", "Enter Name to search");
                corpMasterData_ProcessorsHelper.TypeText("SearchProcessor", name);
                corpMasterData_ProcessorsHelper.WaitForWorkAround(2000);

                executionLog.Log("EditProcessor", "Click Delete btn  ");
                corpMasterData_ProcessorsHelper.ClickElement("DeleteIcon");

                executionLog.Log("EditProcessor", "Accept alert message. ");
                corpMasterData_ProcessorsHelper.AcceptAlert();

                executionLog.Log("EditProcessor", "Wait for delete message. ");
                corpMasterData_ProcessorsHelper.WaitForText("The processor is successfully deleted!!", 10);

                VisitCorp("logout");

            }

            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("EditProcessor");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Edit Processor");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Edit Processor", "Bug", "Medium", "Processer page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Edit Processor");
                        TakeScreenshot("EditProcessor");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditProcessor.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("EditProcessor");
                        string id = loginHelper.getIssueID("Edit Processor");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditProcessor.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Edit Processor"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Edit Processor");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("EditProcessor");
                executionLog.WriteInExcel("Edit Processor", Status, JIRA, "Corp Master Data");
            }
        }
    }
}