using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Admin
{
    [TestClass]
    public class CreatePriorityTicket : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin")]
        [TestCategory("TS6")]
        [TestCategory("Pegasus_Admin")]
        public void createPriorityTicket()
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
                executionLog.Log("CreatePriorityTicket", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("CreatePriorityTicket", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("CreatePriorityTicket", "Click On  Admin");
                VisitOffice("admin");

                executionLog.Log("CreatePriorityTicket", "Redirect To Ticket");
                VisitOffice("tickets/masterdata/priority");

                executionLog.Log("CreatePriorityTicket", "Verify title");
                VerifyTitle("Master Data");

                executionLog.Log("CreatePriorityTicket", " Click On Create");
                tickets_MasterDataHelper.ClickElement("Create");

                executionLog.Log("CreatePriorityTicket", "Verify title");
                VerifyTitle("Create");

                executionLog.Log("CreatePriorityTicket", "Enter Name");
                tickets_MasterDataHelper.TypeText("PriorityName", name);

                executionLog.Log("CreatePriorityTicket", "cLICK on Save  ");
                tickets_MasterDataHelper.ClickElement("Save");

                executionLog.Log("CreatePriorityTicket", "Wait for Confirmation");
                tickets_MasterDataHelper.WaitForText("Masterdata created successfully", 30);

                executionLog.Log("CreatePriorityTicket", "Click on delete item.");
                tickets_MasterDataHelper.ClickElement("DeleteItem");
                tickets_MasterDataHelper.WaitForWorkAround(3000);

                executionLog.Log("CreatePriorityTicket", "Click on category to be deleted");
                tickets_MasterDataHelper.DeleteCategory(name);
                tickets_MasterDataHelper.WaitForWorkAround(3000);

                executionLog.Log("CreatePriorityTicket", "Select value to be replace with");
                tickets_MasterDataHelper.SelectByText("ReplaceWith", "Low");

                executionLog.Log("CreatePriorityTicket", "Confirm delete by clicking save.");
                tickets_MasterDataHelper.ClickElement("SaveDelete");
                tickets_MasterDataHelper.AcceptAlert();

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CreatePriorityTicket");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Create Priority Ticket");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Create Priority Ticket", "Bug", "Medium", "Ticket page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Create Priority Ticket");
                        TakeScreenshot("CreatePriorityTicket");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreatePriorityTicket.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CreatePriorityTicket");
                        string id = loginHelper.getIssueID("Create Priority Ticket");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreatePriorityTicket.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Create Priority Ticket"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Create Priority Ticket");
            //    executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("CreatePriorityTicket");
                executionLog.WriteInExcel("Create Priority Ticket", Status, JIRA, "Admin Ticket");
            }
        }
    }
}

