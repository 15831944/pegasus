using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class VerifyProcessorInCustomFields : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin")]
        [TestCategory("BugTestNew")]
        public void verifyProcessorInCustomFields()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var office_LeadsHelper = new Office_LeadsHelper(GetWebDriver());
            var office_ClientsHelper = new Office_ClientsHelper(GetWebDriver());
            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Variable
            var FName = "Test" + RandomNumber(99, 99999);
            var LName = "Test" + RandomNumber(99, 99999);
            var DBA = "New" + RandomNumber(99, 99999);
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("VerifyProcessorInCustomFields", "Login with valid credentials");
                Login(username[0], password[0]);

                executionLog.Log("VerifyProcessorInCustomFields", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyProcessorInCustomFields", "Redirect at All Clients page");
                VisitOffice("clients");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyProcessorInCustomFields", "Open a merchant");
                office_ClientsHelper.ClickElement("Client1");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyProcessorInCustomFields", "Go to Company Details tab");
                office_ClientsHelper.ClickElement("CompanyDetailsTab");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyProcessorInCustomFields", "Click on Pencil icon of Processor for custom field.");
                office_ClientsHelper.ClickElement("ProcessorPencilIcon");
                office_ClientsHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyProcessorInCustomFields", "Click on Add button");
                office_ClientsHelper.ClickElement("AddCustomBtn");
                office_ClientsHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyProcessorInCustomFields", "Verify Processor section present");
                office_ClientsHelper.verifyElementPresent("ProcSection");
                office_ClientsHelper.WaitForWorkAround(1000);
            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyProcessorInCustomFields");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Processor In Custom Fields");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Processor In Custom Fields", "Bug", "Medium", "Create Vendor page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Processor In Custom Fields");
                        TakeScreenshot("VerifyProcessorInCustomFields");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyProcessorInCustomFields.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyProcessorInCustomFields");
                        string id = loginHelper.getIssueID("Verify Processor In Custom Fields");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyProcessorInCustomFields.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Processor In Custom Fields"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Processor In Custom Fields");
                //  executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("VerifyProcessorInCustomFields");
                executionLog.WriteInExcel("Verify Processor In Custom Fields", Status, JIRA, "Vendor page");
            }
        }
    }
}