using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class VerifyTicketsBulkUpdates : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin1")]
        [TestCategory("Fail")]
        [TestCategory("TS7")]
        [TestCategory("Pegasus_Admin1")]
        public void verifyTicketsBulkUpdates()
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
            var officeTickets_CreateTicketsHelper = new OfficeTickets_CreateTicketsHelper(GetWebDriver());

            // Random Variables.
            var TickName = "Ticket" + RandomNumber(666, 9999);
            var Ticket2 = "Ticket2" + RandomNumber(333, 9999);
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("VerifyTicketsBulkUpdates", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyTicketsBulkUpdates", "Verify Page title");
                VerifyTitle("Dashboard");
                //officeTickets_CreateTicketsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyTicketsBulkUpdates", "Redirect To Tickets");
                VisitOffice("tickets/create");
                officeTickets_CreateTicketsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTicketsBulkUpdates", "Verify page title.");
                VerifyTitle("Create a Ticket");
                //officeTickets_CreateTicketsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTicketsBulkUpdates", "Click on Save");
                officeTickets_CreateTicketsHelper.ClickElement("ClickOnSaveTicket");
                //officeTickets_CreateTicketsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyTicketsBulkUpdates", "Verify validation text for name");
                officeTickets_CreateTicketsHelper.VerifyText("TickNameErr", "This field is required.");

                executionLog.Log("VerifyTicketsBulkUpdates", "Verify validation text for assignee.");
                officeTickets_CreateTicketsHelper.VerifyText("TickParentErr", "This field is required.");

                executionLog.Log("VerifyTicketsBulkUpdates", "Enter Ticket Name");
                officeTickets_CreateTicketsHelper.TypeText("TicketName", TickName);

                executionLog.Log("VerifyTicketsBulkUpdates", "Click On Client Display Icon");
                officeTickets_CreateTicketsHelper.ClickElement("ClientDisplayIcon");
                officeTickets_CreateTicketsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyTicketsBulkUpdates", "ClickOnClient");
                officeTickets_CreateTicketsHelper.ClickElement("ClickOnClient");
                officeTickets_CreateTicketsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyTicketsBulkUpdates", "Select client status.");
                officeTickets_CreateTicketsHelper.SelectByText("TickStatus", "Resolved");
                //officeTickets_CreateTicketsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyTicketsBulkUpdates", "Click on Save");
                officeTickets_CreateTicketsHelper.ClickElement("ClickOnSaveTicket");

                executionLog.Log("VerifyTicketsBulkUpdates", "Wait text Ticket Created Successfully.");
                officeTickets_CreateTicketsHelper.WaitForText("Ticket Created Successfully.", 05);

                executionLog.Log("VerifyTicketsBulkUpdates", "Redirect To Tickets");
                VisitOffice("tickets/create");
                officeTickets_CreateTicketsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTicketsBulkUpdates", "Verify page title.");
                VerifyTitle("Create a Ticket");
                //officeTickets_CreateTicketsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTicketsBulkUpdates", "Click on Save");
                officeTickets_CreateTicketsHelper.ClickElement("ClickOnSaveTicket");
                //officeTickets_CreateTicketsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyTicketsBulkUpdates", "Verify validation text for name");
                officeTickets_CreateTicketsHelper.VerifyText("TickNameErr", "This field is required.");
                //officeTickets_CreateTicketsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyTicketsBulkUpdates", "Verify validation text for assignee.");
                officeTickets_CreateTicketsHelper.VerifyText("TickParentErr", "This field is required.");
                //officeTickets_CreateTicketsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTicketsBulkUpdates", "Enter Ticket Name");
                officeTickets_CreateTicketsHelper.TypeText("TicketName", Ticket2);
                //officeTickets_CreateTicketsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTicketsBulkUpdates", "Select client status.");
                officeTickets_CreateTicketsHelper.SelectByText("TickStatus", "Resolved");
                //officeTickets_CreateTicketsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTicketsBulkUpdates", "Click On Client Display Icon");
                officeTickets_CreateTicketsHelper.ClickElement("ClientDisplayIcon");
                officeTickets_CreateTicketsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyTicketsBulkUpdates", "ClickOnClient");
                officeTickets_CreateTicketsHelper.ClickElement("ClickOnClient");
                officeTickets_CreateTicketsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyTicketsBulkUpdates", "Click on Save");
                officeTickets_CreateTicketsHelper.ClickElement("ClickOnSaveTicket");
                officeTickets_CreateTicketsHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyTicketsBulkUpdates", "Wait text Ticket Created Successfully.");
                officeTickets_CreateTicketsHelper.WaitForText("Ticket Created Successfully.", 05);

                executionLog.Log("VerifyTicketsBulkUpdates", "Redirect To Tickets");
                VisitOffice("tickets");
                officeTickets_CreateTicketsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTicketsBulkUpdates", "Verify page title.");
                VerifyTitle("Tickets");
                //officeTickets_CreateTicketsHelper.WaitForWorkAround(3000);

                //executionLog.Log("VerifyTicketsBulkUpdates", "Wait for locator to present.");
                //officeTickets_CreateTicketsHelper.WaitForElementPresent("BulkUpdate", 30);

                executionLog.Log("VerifyTicketsBulkUpdates", "Click on bulk update.");
                officeTickets_CreateTicketsHelper.ClickElement("BulkUpdate");
                officeTickets_CreateTicketsHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyTicketsBulkUpdates", "Click to update status");
                officeTickets_CreateTicketsHelper.ClickElement("ChangeStatus");
                officeTickets_CreateTicketsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyTicketsBulkUpdates", "Verify Alert message for selecting record.");
                officeTickets_CreateTicketsHelper.VerifyAlertText("Please select atleast one record to proceed.");
                officeTickets_CreateTicketsHelper.AcceptAlert();
                //officeTickets_CreateTicketsHelper.WaitForWorkAround(3000);

                //executionLog.Log("VerifyTicketsBulkUpdates", "Wait for locator to present.");
                //officeTickets_CreateTicketsHelper.WaitForElementPresent("Checkbox1", 20);

                executionLog.Log("VerifyTicketsBulkUpdates", "Click on first checkbox.");
                officeTickets_CreateTicketsHelper.ClickElement("Checkbox1");
                officeTickets_CreateTicketsHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyTicketsBulkUpdates", "Click on seconf checkbox.");
                officeTickets_CreateTicketsHelper.ClickElement("Checkbox2");
                officeTickets_CreateTicketsHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyTicketsBulkUpdates", "Click on bulk update.");
                officeTickets_CreateTicketsHelper.ClickElement("BulkUpdate");
                officeTickets_CreateTicketsHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyTicketsBulkUpdates", "Click on change status.");
                officeTickets_CreateTicketsHelper.ClickElement("ChangeStatus");
                officeTickets_CreateTicketsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyTicketsBulkUpdates", "Verify page title.");
                officeTickets_CreateTicketsHelper.SelectByText("UpdatedStatus", "New");
                officeTickets_CreateTicketsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyTicketsBulkUpdates", "Click on update button.");
                officeTickets_CreateTicketsHelper.ClickElement("UPButton");
                officeTickets_CreateTicketsHelper.AcceptAlert();
                //officeTickets_CreateTicketsHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyTicketsBulkUpdates", "Wait updation success message.");
                officeTickets_CreateTicketsHelper.WaitForText("2 Record(s) updated successfully", 10);
                officeTickets_CreateTicketsHelper.WaitForWorkAround(2000);

                //executionLog.Log("VerifyTicketsBulkUpdates", "Wait for locator to present.");
                //officeTickets_CreateTicketsHelper.WaitForElementPresent("Checkbox1", 20);

                executionLog.Log("VerifyTicketsBulkUpdates", "Click on first checkbox.");
                officeTickets_CreateTicketsHelper.ClickElement("Checkbox1");
                //officeTickets_CreateTicketsHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyTicketsBulkUpdates", "Click on seconf checkbox.");
                officeTickets_CreateTicketsHelper.ClickElement("Checkbox2");
                //officeTickets_CreateTicketsHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyTicketsBulkUpdates", "Click on bulk update..");
                officeTickets_CreateTicketsHelper.ClickElement("BulkUpdate");
                officeTickets_CreateTicketsHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyTicketsBulkUpdates", "Verify page title.");
                officeTickets_CreateTicketsHelper.ClickElement("ChangeCategory");
                officeTickets_CreateTicketsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyTicketsBulkUpdates", "Verify page title.");
                officeTickets_CreateTicketsHelper.SelectByText("SelectCategoryy", "Billing");
                //officeTickets_CreateTicketsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTicketsBulkUpdates", "Click on update button.");
                officeTickets_CreateTicketsHelper.ClickElement("UpdateButton");
                officeTickets_CreateTicketsHelper.AcceptAlert();
                //officeTickets_CreateTicketsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTicketsBulkUpdates", "Wait updation success message.");
                officeTickets_CreateTicketsHelper.WaitForText("2 Record(s) updated successfully", 10);

                //executionLog.Log("VerifyTicketsBulkUpdates", "Wait for locator to present.");
                //officeTickets_CreateTicketsHelper.WaitForElementPresent("Checkbox1", 30);

                executionLog.Log("VerifyTicketsBulkUpdates", "Click on first checkbox.");
                officeTickets_CreateTicketsHelper.ClickElement("Checkbox1");
                //officeTickets_CreateTicketsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyTicketsBulkUpdates", "Click on seconf checkbox.");
                officeTickets_CreateTicketsHelper.ClickElement("Checkbox2");
                //officeTickets_CreateTicketsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyTicketsBulkUpdates", "Click on bulk update..");
                officeTickets_CreateTicketsHelper.ClickElement("BulkUpdate");
                officeTickets_CreateTicketsHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyTicketsBulkUpdates", "Verify page title.");
                officeTickets_CreateTicketsHelper.ClickElement("ChangeTopic");
                officeTickets_CreateTicketsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyTicketsBulkUpdates", "Verify page title.");
                officeTickets_CreateTicketsHelper.SelectByText("SelectTopic", "Merge");
                //officeTickets_CreateTicketsHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyTicketsBulkUpdates", "Click on update button.");
                officeTickets_CreateTicketsHelper.ClickElement("UpdateTopic");
                officeTickets_CreateTicketsHelper.AcceptAlert();
                //officeTickets_CreateTicketsHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyTicketsBulkUpdates", "Wait updation success message.");
                officeTickets_CreateTicketsHelper.WaitForText("2 Record(s) updated successfully", 10);
                //officeTickets_CreateTicketsHelper.WaitForWorkAround(1000);

                //executionLog.Log("VerifyTicketsBulkUpdates", "Wait for locator to present.");
                //officeTickets_CreateTicketsHelper.WaitForElementPresent("Checkbox1", 20);

                executionLog.Log("VerifyTicketsBulkUpdates", "Click on first checkbox.");
                officeTickets_CreateTicketsHelper.ClickElement("Checkbox1");
                //officeTickets_CreateTicketsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyTicketsBulkUpdates", "Click on seconf checkbox.");
                officeTickets_CreateTicketsHelper.ClickElement("Checkbox2");
                //officeTickets_CreateTicketsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyTicketsBulkUpdates", "Click on bulk update..");
                officeTickets_CreateTicketsHelper.ClickJs("BulkUpdate");
                officeTickets_CreateTicketsHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyTicketsBulkUpdates", "Verify page title.");
                officeTickets_CreateTicketsHelper.ClickJs("ChangePriority");
                officeTickets_CreateTicketsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyTicketsBulkUpdates", "Verify page title.");
                officeTickets_CreateTicketsHelper.SelectByText("SelectPriority", "Medium");
                //officeTickets_CreateTicketsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyTicketsBulkUpdates", "Click on update button.");
                officeTickets_CreateTicketsHelper.ClickElement("UpdatePrirority");
                officeTickets_CreateTicketsHelper.AcceptAlert();
                //officeTickets_CreateTicketsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyTicketsBulkUpdates", "Wait updation success message.");
                officeTickets_CreateTicketsHelper.WaitForText("2 Record(s) updated successfully", 10);
                //officeTickets_CreateTicketsHelper.WaitForWorkAround(3000);

                //executionLog.Log("VerifyTicketsBulkUpdates", "Wait for locator to present.");
                //officeTickets_CreateTicketsHelper.WaitForElementPresent("Checkbox1", 20);

                executionLog.Log("VerifyTicketsBulkUpdates", "Click on first checkbox.");
                officeTickets_CreateTicketsHelper.ClickElement("Checkbox1");
                //officeTickets_CreateTicketsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyTicketsBulkUpdates", "Click on seconf checkbox.");
                officeTickets_CreateTicketsHelper.ClickElement("Checkbox2");
                //officeTickets_CreateTicketsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyTicketsBulkUpdates", "Click on bulk update..");
                officeTickets_CreateTicketsHelper.ClickJs("BulkUpdate");
                officeTickets_CreateTicketsHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyTicketsBulkUpdates", "Verify page title.");
                officeTickets_CreateTicketsHelper.ClickJs("ChangeOwner");
                officeTickets_CreateTicketsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyTicketsBulkUpdates", "Verify page title.");
                officeTickets_CreateTicketsHelper.SelectByText("OwnerSelect", "Howard Tang");
                //officeTickets_CreateTicketsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyTicketsBulkUpdates", "Click on update button.");
                officeTickets_CreateTicketsHelper.ClickJs("UpdateOwner");
                officeTickets_CreateTicketsHelper.AcceptAlert();
                //officeTickets_CreateTicketsHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyTicketsBulkUpdates", "Wait updation success message.");
                officeTickets_CreateTicketsHelper.WaitForText("2 Record(s) updated successfully", 10);

                //executionLog.Log("VerifyTicketsBulkUpdates", "Wait for locator to present.");
                //officeTickets_CreateTicketsHelper.WaitForElementPresent("Checkbox1", 20);

                executionLog.Log("VerifyTicketsBulkUpdates", "Click on first checkbox.");
                officeTickets_CreateTicketsHelper.ClickElement("Checkbox1");
                //officeTickets_CreateTicketsHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyTicketsBulkUpdates", "Click on seconf checkbox.");
                officeTickets_CreateTicketsHelper.ClickElement("Checkbox2");
                //officeTickets_CreateTicketsHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyTicketsBulkUpdates", "Click on bulk update..");
                officeTickets_CreateTicketsHelper.ClickJs("BulkUpdate");
                officeTickets_CreateTicketsHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyTicketsBulkUpdates", "Verify page title.");
                officeTickets_CreateTicketsHelper.ClickJs("ChangeManager");
                officeTickets_CreateTicketsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyTicketsBulkUpdates", "Verify page title.");
                officeTickets_CreateTicketsHelper.SelectByText("SelectManager", "Howard Tang");
                //officeTickets_CreateTicketsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyTicketsBulkUpdates", "Click on update button.");
                officeTickets_CreateTicketsHelper.ClickJs("UpdateManager");
                officeTickets_CreateTicketsHelper.AcceptAlert();
                //officeTickets_CreateTicketsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyTicketsBulkUpdates", "Wait updation success message.");
                officeTickets_CreateTicketsHelper.WaitForText("2 Record(s) updated successfully", 10);
                //officeTickets_CreateTicketsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTicketsBulkUpdates", "Redirect To Tickets");
                VisitOffice("tickets");
                officeTickets_CreateTicketsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTicketsBulkUpdates", "SearchTicket by Name");
                officeTickets_CreateTicketsHelper.TypeText("SearchTicket", TickName);
                officeTickets_CreateTicketsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyTicketsBulkUpdates", "Select 'All' in Assigned field");
                officeTickets_CreateTicketsHelper.SelectByText("AssignedField", "All");
                officeTickets_CreateTicketsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyTicketsBulkUpdates", "Select first ticket");
                officeTickets_CreateTicketsHelper.ClickElement("Checkbox1");

                executionLog.Log("VerifyTicketsBulkUpdates", "Click on delete link");
                officeTickets_CreateTicketsHelper.ClickElement("DeleteBulk");
                officeTickets_CreateTicketsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyTicketsBulkUpdates", "Accept alert message.");
                officeTickets_CreateTicketsHelper.AcceptAlert();
                //officeTickets_CreateTicketsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyTicketsBulkUpdates", "Redirect To Tickets");
                VisitOffice("tickets");
                officeTickets_CreateTicketsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTicketsBulkUpdates", "SearchTicket by Name");
                officeTickets_CreateTicketsHelper.TypeText("SearchTicket", Ticket2);
                officeTickets_CreateTicketsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyTicketsBulkUpdates", "Select 'All' in Assigned field");
                officeTickets_CreateTicketsHelper.SelectByText("AssignedField", "All");
                officeTickets_CreateTicketsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyTicketsBulkUpdates", "Select first ticket");
                officeTickets_CreateTicketsHelper.ClickElement("Checkbox1");

                executionLog.Log("VerifyTicketsBulkUpdates", "Click on delete link");
                officeTickets_CreateTicketsHelper.ClickElement("DeleteBulk");
                //officeTickets_CreateTicketsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyTicketsBulkUpdates", "Accept alert message.");
                officeTickets_CreateTicketsHelper.AcceptAlert();
                officeTickets_CreateTicketsHelper.WaitForWorkAround(4000);
            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyTicketsBulkUpdates");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Tickets Bulk Updates");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Tickets Bulk Updates", "Bug", "Medium", "Meeting page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Tickets Bulk Updates");
                        TakeScreenshot("VerifyTicketsBulkUpdates");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyTicketsBulkUpdates.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyTicketsBulkUpdates");
                        string id = loginHelper.getIssueID("Verify Tickets Bulk Updates");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyTicketsBulkUpdates.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Tickets Bulk Updates"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Tickets Bulk Updates");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyTicketsBulkUpdates");
                executionLog.WriteInExcel("Verify Tickets Bulk Updates", Status, JIRA, "Office Activities");
            }
        }
    }
}