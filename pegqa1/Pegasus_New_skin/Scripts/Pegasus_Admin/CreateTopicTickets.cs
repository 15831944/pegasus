using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Admin
{
    [TestClass]
    public class CreateTopicTickets : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin")]
        [TestCategory("TS6")]
        [TestCategory("Pegasus_Admin")]
        public void createTopicTickets()
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
            var name = "Topic" + RandomNumber(1, 99);
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("CreateTopicTickets", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("CreateTopicTickets", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("CreateTopicTickets", "Redirect To Ticket");
                VisitOffice("tickets/masterdata/topic");
                tickets_MasterDataHelper.WaitForWorkAround(3000);

                executionLog.Log("CreateTopicTickets", "Verify title");
                VerifyTitle("Master Data");

                executionLog.Log("CreateTopicTickets", " Click On Create");
                tickets_MasterDataHelper.ClickElement("Create");
                tickets_MasterDataHelper.WaitForWorkAround(3000);

                executionLog.Log("CreateTopicTickets", "Verify title");
                VerifyTitle("Create");

                executionLog.Log("CreateTopicTickets", "Enter Name");
                tickets_MasterDataHelper.TypeText("Name", name);

                executionLog.Log("CreateTopicTickets", "cLICK on Save  ");
                tickets_MasterDataHelper.ClickElement("Save");
                tickets_MasterDataHelper.WaitForWorkAround(2000);

                executionLog.Log("CreateTopicTickets", "Wait for Confirmation");
                tickets_MasterDataHelper.WaitForText("Masterdata created successfully", 10);

                executionLog.Log("CreateTopicTickets", "Click on delete item.");
                tickets_MasterDataHelper.ClickElement("DeleteItem");
                tickets_MasterDataHelper.WaitForWorkAround(2000);

                executionLog.Log("CreateTopicTickets", "Click on category to be deleted");
                tickets_MasterDataHelper.DeleteCategory(name);
                //tickets_MasterDataHelper.WaitForWorkAround(3000);

                executionLog.Log("CreateTopicTickets", "Select value to be replace with");
                tickets_MasterDataHelper.SelectByText("ReplaceWith", "Other");

                executionLog.Log("CreateTopicTickets", "Confirm delete by clicking save.");
                tickets_MasterDataHelper.ClickElement("SaveDelete");
                tickets_MasterDataHelper.AcceptAlert();
                tickets_MasterDataHelper.WaitForWorkAround(3000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CreateTopicTickets");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Create Topic Tickets");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Create Topic Tickets", "Bug", "Medium", "Ticket page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Create Topic Tickets");
                        TakeScreenshot("CreateTopicTickets");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateTopicTickets.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CreateTopicTickets");
                        string id = loginHelper.getIssueID("Create Topic Tickets");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateTopicTickets.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Create Topic Tickets"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Create Topic Tickets");
            //    executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("CreateTopicTickets");
                executionLog.WriteInExcel("Create Topic Tickets", Status, JIRA, "Ticket Admin");
            }
        }
    }
}
