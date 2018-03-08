using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Admin
{
    [TestClass]
    public class CreateStatus : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin")]
        [TestCategory("TS6")]
        [TestCategory("Pegasus_Admin")]
        public void createStatus()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var tickets_MasterDataHelper = new Tickets_MasterDataHelper(GetWebDriver());

            // Variable
            var name = "Test" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("CreateStatus", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("CreateStatus", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("CreateStatus", "Redirect To Status");
                VisitOffice("tickets/masterdata/status");
                tickets_MasterDataHelper.WaitForWorkAround(3000);

                executionLog.Log("CreateStatus", "Verify title");
                VerifyTitle("Master Data");

                executionLog.Log("CreateStatus", " Click On Create");
                tickets_MasterDataHelper.ClickElement("Create");
                tickets_MasterDataHelper.WaitForWorkAround(3000);

                executionLog.Log("CreateStatus", "Verify title");
                VerifyTitle("Create");

                executionLog.Log("CreateStatus", "Enter Name");
                tickets_MasterDataHelper.TypeText("Name", name);

                executionLog.Log("CreateStatus", "cLICK on Save");
                tickets_MasterDataHelper.ClickElement("Save");
                tickets_MasterDataHelper.WaitForWorkAround(2000);

                executionLog.Log("CreateStatus", "Wait for text");
                tickets_MasterDataHelper.WaitForText("Masterdata created successfully", 10);

                executionLog.Log("CreatePriorityTicket", "Click on delete item.");
                tickets_MasterDataHelper.ClickElement("DeleteItem");
                tickets_MasterDataHelper.WaitForWorkAround(2000);

                executionLog.Log("CreatePriorityTicket", "Click on category to be deleted");
                tickets_MasterDataHelper.DeleteCategory(name);
                //tickets_MasterDataHelper.WaitForWorkAround(1000);

                executionLog.Log("CreatePriorityTicket", "Select value to be replace with");
                tickets_MasterDataHelper.SelectByText("ReplaceWith", "Active");

                executionLog.Log("CreatePriorityTicket", "Confirm delete by clicking save.");
                tickets_MasterDataHelper.ClickElement("SaveDelete");
                tickets_MasterDataHelper.AcceptAlert();
                tickets_MasterDataHelper.WaitForWorkAround(3000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CreateStatus");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Create Status");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Create Status", "Bug", "Medium", "Status page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Create Status");
                        TakeScreenshot("CreateStatus");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateStatus.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CreateStatus");
                        string id = loginHelper.getIssueID("Create Status");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateStatus.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Create Status"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Create Status");
              //  executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("CreateStatus");
                executionLog.WriteInExcel("Create Status", Status, JIRA, "Ticket Admin");
            }
        }
    }
}