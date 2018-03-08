using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class VerifyAdminTicketMasterDeleteConfirmation : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("Fail")]
        [TestCategory("TS3")]
        [TestCategory("BugTestNew")]
        public void verifyAdminTicketMasterDeleteConfirmation()
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
            var tickets_MasterDataHelper = new Tickets_MasterDataHelper(GetWebDriver());

            // Random variables
            var Ticket = "Ticket" + RandomNumber(111, 9999);
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("VerifyAdminTicketMasterDeleteConfirmation", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyAdminTicketMasterDeleteConfirmation", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyAdminTicketMasterDeleteConfirmation", "Redirect at ticket topic page.");
                VisitOffice("tickets/masterdata/category");
                tickets_MasterDataHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyAdminTicketMasterDeleteConfirmation", "Click on create button");
                tickets_MasterDataHelper.ClickElement("Create");
                tickets_MasterDataHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyAdminTicketMasterDeleteConfirmation", "Enter topic name");
                tickets_MasterDataHelper.TypeText("Name", Ticket);

                executionLog.Log("VerifyAdminTicketMasterDeleteConfirmation", "Click on Save button.");
                tickets_MasterDataHelper.ClickElement("Save");

                executionLog.Log("VerifyAdminTicketMasterDeleteConfirmation", "Wait for creation success text.");
                tickets_MasterDataHelper.WaitForText("Masterdata created successfully", 10);

                executionLog.Log("VerifyAdminTicketMasterDeleteConfirmation", "Click on delete item button.");
                tickets_MasterDataHelper.ClickElement("DeleteItem");
                tickets_MasterDataHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyAdminTicketMasterDeleteConfirmation", "Select topic to be deleted.");
                tickets_MasterDataHelper.SelectByText("Select", Ticket);
                //tickets_MasterDataHelper.WaitForWorkAround(4000);

                executionLog.Log("VerifyAdminTicketMasterDeleteConfirmation", "select value to be replaced.");
                tickets_MasterDataHelper.SelectByText("ReplaceWith", "Billing");
                //tickets_MasterDataHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyAdminTicketMasterDeleteConfirmation", "Click on save button and accept alert.");
                tickets_MasterDataHelper.ClickElement("SaveDelete");
                tickets_MasterDataHelper.AcceptAlert();
                tickets_MasterDataHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyAdminTicketMasterDeleteConfirmation", "Wait for item deletion text.");
                tickets_MasterDataHelper.VerifyPageText("Category deleted successfully.");

                executionLog.Log("VerifyAdminTicketMasterDeleteConfirmation", "Redirect at ticket topic page.");
                VisitOffice("tickets/masterdata/topic");
                tickets_MasterDataHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyAdminTicketMasterDeleteConfirmation", "Click on create button");
                tickets_MasterDataHelper.ClickElement("Create");
                tickets_MasterDataHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyAdminTicketMasterDeleteConfirmation", "Enter topic name");
                tickets_MasterDataHelper.TypeText("Name", Ticket);

                executionLog.Log("VerifyAdminTicketMasterDeleteConfirmation", "Click on Save button.");
                tickets_MasterDataHelper.ClickElement("Save");

                executionLog.Log("VerifyAdminTicketMasterDeleteConfirmation", "Wait for creation success text.");
                tickets_MasterDataHelper.WaitForText("Masterdata created successfully", 10);

                executionLog.Log("VerifyAdminTicketMasterDeleteConfirmation", "Click on delete item button.");
                tickets_MasterDataHelper.ClickElement("DeleteItem");
                tickets_MasterDataHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyAdminTicketMasterDeleteConfirmation", "Select topic to be deleted.");
                tickets_MasterDataHelper.SelectByText("Select", Ticket);
                //tickets_MasterDataHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyAdminTicketMasterDeleteConfirmation", "select value to be replaced.");
                tickets_MasterDataHelper.SelectByText("ReplaceWith", "Test");

                executionLog.Log("VerifyAdminTicketMasterDeleteConfirmation", "Click on save button and accept alert.");
                tickets_MasterDataHelper.ClickElement("SaveDelete");
                tickets_MasterDataHelper.AcceptAlert();
                tickets_MasterDataHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyAdminTicketMasterDeleteConfirmation", "Wait for itm deletion text.");
                tickets_MasterDataHelper.WaitForText("Topic deleted successfully.", 10);

                executionLog.Log("VerifyAdminTicketMasterDeleteConfirmation", "Redirect at ticket topic page.");
                VisitOffice("tickets/masterdata/status");
                tickets_MasterDataHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyAdminTicketMasterDeleteConfirmation", "Click on create button");
                tickets_MasterDataHelper.ClickElement("Create");
                tickets_MasterDataHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyAdminTicketMasterDeleteConfirmation", "Enter topic name");
                tickets_MasterDataHelper.TypeText("Name", Ticket);

                executionLog.Log("VerifyAdminTicketMasterDeleteConfirmation", "Click on Save button.");
                tickets_MasterDataHelper.ClickElement("Save");

                executionLog.Log("VerifyAdminTicketMasterDeleteConfirmation", "Wait for creation success text.");
                tickets_MasterDataHelper.WaitForText("Masterdata created successfully", 10);

                executionLog.Log("VerifyAdminTicketMasterDeleteConfirmation", "Click on delete item button.");
                tickets_MasterDataHelper.ClickElement("DeleteItem");
                tickets_MasterDataHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyAdminTicketMasterDeleteConfirmation", "Select topic to be deleted.");
                tickets_MasterDataHelper.SelectByText("Select", Ticket);
                //tickets_MasterDataHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyAdminTicketMasterDeleteConfirmation", "select value to be replaced.");
                tickets_MasterDataHelper.SelectByText("ReplaceWith", "New");
                //tickets_MasterDataHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyAdminTicketMasterDeleteConfirmation", "Click on save button and accept alert.");
                tickets_MasterDataHelper.ClickElement("SaveDelete");
                tickets_MasterDataHelper.AcceptAlert();
                tickets_MasterDataHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyAdminTicketMasterDeleteConfirmation", "Wait for itm deletion text.");
                tickets_MasterDataHelper.WaitForText("Status deleted successfully.", 10);

                executionLog.Log("VerifyAdminTicketMasterDeleteConfirmation", "Redirect at ticket topic page.");
                VisitOffice("tickets/masterdata/priority");
                tickets_MasterDataHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyAdminTicketMasterDeleteConfirmation", "Click on create button");
                tickets_MasterDataHelper.ClickElement("Create");
                tickets_MasterDataHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyAdminTicketMasterDeleteConfirmation", "Enter topic name");
                tickets_MasterDataHelper.TypeText("Name", Ticket);

                executionLog.Log("VerifyAdminTicketMasterDeleteConfirmation", "Click on Save button.");
                tickets_MasterDataHelper.ClickElement("Save");

                executionLog.Log("VerifyAdminTicketMasterDeleteConfirmation", "Wait for creation success text.");
                tickets_MasterDataHelper.WaitForText("Masterdata created successfully", 10);

                executionLog.Log("VerifyAdminTicketMasterDeleteConfirmation", "Click on delete item button.");
                tickets_MasterDataHelper.ClickElement("DeleteItem");
                tickets_MasterDataHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyAdminTicketMasterDeleteConfirmation", "Select topic to be deleted.");
                tickets_MasterDataHelper.SelectByText("Select", Ticket);
                //tickets_MasterDataHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyAdminTicketMasterDeleteConfirmation", "select value to be replaced.");
                tickets_MasterDataHelper.SelectByText("ReplaceWith", "Medium");
                //tickets_MasterDataHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyAdminTicketMasterDeleteConfirmation", "Click on save button and accept alert.");
                tickets_MasterDataHelper.ClickElement("SaveDelete");
                tickets_MasterDataHelper.AcceptAlert();
                tickets_MasterDataHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyAdminTicketMasterDeleteConfirmation", "Wait for itm deletion text.");
                tickets_MasterDataHelper.WaitForText("Priority deleted successfully.", 10);

                executionLog.Log("VerifyAdminTicketMasterDeleteConfirmation", "Redirect at ticket topic page.");
                VisitOffice("tickets/masterdata/resolution");
                tickets_MasterDataHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyAdminTicketMasterDeleteConfirmation", "Click on create button");
                tickets_MasterDataHelper.ClickElement("Create");
                tickets_MasterDataHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyAdminTicketMasterDeleteConfirmation", "Enter topic name");
                tickets_MasterDataHelper.TypeText("Name", Ticket);

                executionLog.Log("VerifyAdminTicketMasterDeleteConfirmation", "Click on Save button.");
                tickets_MasterDataHelper.ClickElement("Save");

                executionLog.Log("VerifyAdminTicketMasterDeleteConfirmation", "Wait for creation success text.");
                tickets_MasterDataHelper.WaitForText("Masterdata created successfully", 10);

                executionLog.Log("VerifyAdminTicketMasterDeleteConfirmation", "Click on delete item button.");
                tickets_MasterDataHelper.ClickElement("DeleteItem");
                tickets_MasterDataHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyAdminTicketMasterDeleteConfirmation", "Select topic to be deleted.");
                tickets_MasterDataHelper.SelectByText("Select", Ticket);
                //tickets_MasterDataHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyAdminTicketMasterDeleteConfirmation", "select value to be replaced.");
                tickets_MasterDataHelper.SelectByText("ReplaceWith", "Resolved");
                //tickets_MasterDataHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyAdminTicketMasterDeleteConfirmation", "Click on save button and accept alert.");
                tickets_MasterDataHelper.ClickElement("SaveDelete");
                tickets_MasterDataHelper.AcceptAlert();
                tickets_MasterDataHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyAdminTicketMasterDeleteConfirmation", "Wait for itm deletion text.");
                tickets_MasterDataHelper.WaitForText("Resolution deleted successfully.", 10);

                executionLog.Log("VerifyAdminTicketMasterDeleteConfirmation", "Click on Cancel button.");
                VisitOffice("logout");

            }

            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyAdminTicketMasterDeleteConfirmation");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Admin Ticket Master Delete Confirmation");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Admin Ticket Master Delete Confirmation", "Bug", "Medium", "Ticket Admin page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Admin Ticket Master Delete Confirmation");
                        TakeScreenshot("VerifyAdminTicketMasterDeleteConfirmation");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyAdminTicketMasterDeleteConfirmation.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyAdminTicketMasterDeleteConfirmation");
                        string id = loginHelper.getIssueID("Verify Admin Ticket Master Delete Confirmation");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyAdminTicketMasterDeleteConfirmation.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Admin Ticket Master Delete Confirmation"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Admin Ticket Master Delete Confirmation");
               // executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyAdminTicketMasterDeleteConfirmation");
                executionLog.WriteInExcel("Verify Admin Ticket Master Delete Confirmation", Status, JIRA, "Admin Tickets");
            }
        }
    }
}
