using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class VerifyDeletedTicketDisappearUnderMerchant : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin")]
        [TestCategory("Fail")]
        [TestCategory("BugTestNew")]
        public void verifyDeletedTicketDisappearUnderMerchant()
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
            var office_MerchantHelper = new Office_ClientsHelper(GetWebDriver());
            var officeticketsHelper = new OfficeTickets_CreateTicketsHelper(GetWebDriver());


            var DBA = "ClientDBA" + RandomNumber(1, 5000);
            var ticketname = "Testticket" + RandomNumber(1,500);

            // Variable random
            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("VerifyDeletedTicketDisappearUnderMerchant", "Login with valid username and password");
                Login(username[0], password[0]);

                executionLog.Log("VerifyDeletedTicketDisappearUnderMerchant", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("VerifyDeletedTicketDisappearUnderMerchant", "Redirect to Create merchant page");
                VisitOffice("clients/create");
                office_MerchantHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyDeletedTicketDisappearUnderMerchant", "Enter DBA name");
                office_MerchantHelper.TypeText("ClientDBAName", DBA);

                executionLog.Log("VerifyDeletedTicketDisappearUnderMerchant", "Select the client status");
                office_MerchantHelper.SelectByText("Status", "New");

                executionLog.Log("VerifyDeletedTicketDisappearUnderMerchant", "select the responsibity");
                office_MerchantHelper.SelectByText("Responsibility", "Howard Tang");

                executionLog.Log("VerifyDeletedTicketDisappearUnderMerchant", "Click on save btn");
                office_MerchantHelper.ClickElement("Save");
                office_MerchantHelper.WaitForWorkAround(4000);

                executionLog.Log("VerifyDeletedTicketDisappearUnderMerchant", "Click Create Ticket button");
                office_MerchantHelper.ClickForce("CreateTicket");
                office_MerchantHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyDeletedTicketDisappearUnderMerchant", "Enter Subject name");
                officeticketsHelper.TypeText("TicketName", ticketname);

                executionLog.Log("VerifyDeletedTicketDisappearUnderMerchant", "Select Status");
                officeticketsHelper.SelectByText("TickStatus", "New");

                executionLog.Log("VerifyDeletedTicketDisappearUnderMerchant", "Select Assigned To");
                officeticketsHelper.SelectByText("Assignedto", "Howard Tang");

                executionLog.Log("VerifyDeletedTicketDisappearUnderMerchant", "Click on save btn");
                officeticketsHelper.ClickElement("Save1");
                officeticketsHelper.WaitForWorkAround(4000);

                executionLog.Log("VerifyDeletedTicketDisappearUnderMerchant", "Redirect to All merchants page");
                VisitOffice("clients");
                office_MerchantHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyDeletedTicketDisappearUnderMerchant", "Search merchant by name");
                office_MerchantHelper.TypeText("SearchClient", DBA);
                office_MerchantHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyDeletedTicketDisappearUnderMerchant", "Click on client");
                office_MerchantHelper.ClickElement("Client1");
                office_MerchantHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyDeletedTicketDisappearUnderMerchant", "Click on created ticket");
                office_MerchantHelper.ClickForce("Ticket1");
                office_MerchantHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyDeletedTicketDisappearUnderMerchant", "Click on Delete button");
                officeticketsHelper.ClickElement("ClickDeleteBtn");
                officeticketsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyDeletedTicketDisappearUnderMerchant", "Redirect to All merchants page");
                VisitOffice("clients");
                office_MerchantHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyDeletedTicketDisappearUnderMerchant", "Search merchant by name");
                office_MerchantHelper.TypeText("SearchClient", DBA);
                office_MerchantHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyDeletedTicketDisappearUnderMerchant", "Click on client");
                office_MerchantHelper.ClickElement("Client1");
                office_MerchantHelper.WaitForWorkAround(5000);

                executionLog.Log("VerifyDeletedTicketDisappearUnderMerchant", "Verify ticket disappeared");
                office_MerchantHelper.VerifyText("NoTickets", "No Data Available.");
                Console.WriteLine("Deleted ticket disappeared");
                office_MerchantHelper.WaitForWorkAround(1000);

                VisitOffice("clients");
                office_MerchantHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyDeletedTicketDisappearUnderMerchant", "Search the company Name");
                office_MerchantHelper.TypeText("SearchClient", DBA);
                office_MerchantHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyDeletedTicketDisappearUnderMerchant", "Click on check box");
                office_MerchantHelper.ClickElement("ClickOnCheckBox");

                executionLog.Log("VerifyDeletedTicketDisappearUnderMerchant", "Delete the client");
                office_MerchantHelper.ClickElement("DeleteClient");
                office_MerchantHelper.AcceptAlert();
                office_MerchantHelper.WaitForWorkAround(4000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyDeletedTicketDisappearUnderMerchant");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Deleted Ticket Disappear Under Merchant");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Deleted Ticket Disappear Under Merchant", "Bug", "Medium", "Office Merchant page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Deleted Ticket Disappear Under Merchant");
                        TakeScreenshot("VerifyDeletedTicketDisappearUnderMerchant");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyDeletedTicketDisappearUnderMerchant.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyDeletedTicketDisappearUnderMerchant");
                        string id = loginHelper.getIssueID("Verify Deleted Ticket Disappear Under Merchant");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyDeletedTicketDisappearUnderMerchant.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Deleted Ticket Disappear Under Merchant"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Deleted Ticket Disappear Under Merchant");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyDeletedTicketDisappearUnderMerchant");
                executionLog.WriteInExcel("Verify Deleted Ticket Disappear Under Merchant", Status, JIRA, "Office Merchant");
            }
        }
    }
}
