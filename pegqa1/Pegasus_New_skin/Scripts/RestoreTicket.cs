using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;
namespace Pegasus_New_skin.Scripts
{
    [TestClass]
    public class RestoreTicket : DriverTestCase
    {
        [TestMethod]
        public void TicketRestoreTester()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var restoreTicketHelper = new RestoreTicketHelper(GetWebDriver());

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Variable
            var FName = "Test" + RandomNumber(99, 99999);
            var LName = "Test" + RandomNumber(99, 99999);
            var CDBA = "New" + RandomNumber(99, 99999);
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("RestoreTicket", "Login with valid credential  Username");
                Login(username[0], password[0]);

                executionLog.Log("RestoreTicket", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("RestoreTicket", "Create a ticket");
                VisitOffice("tickets/create");
                restoreTicketHelper.WaitForWorkAround(2000);
                executionLog.Log("RestoreTicket", "Input Subject Name");
                restoreTicketHelper.TypeText("Subject", "TestRestore");

                executionLog.Log("RestoreTicket", "Select Client");
                restoreTicketHelper.ClickElement("SelectClient");
                restoreTicketHelper.ClickElement("FirstClient");

                executionLog.Log("RestoreTicket", "Select Client");
                restoreTicketHelper.SelectByText("Status", "Open");

                executionLog.Log("RestoreTicket", "Save");
                restoreTicketHelper.ClickElement("Save");
                restoreTicketHelper.WaitForWorkAround(3000);

                executionLog.Log("RestoreTicket", "get ID");
                string Tid = restoreTicketHelper.getId();

                VisitOffice("tickets/create");



            }
            catch (Exception e)
            {
                //    Console.WriteLine("ERRROROOR");
                //    executionLog.Log("Error", e.StackTrace);
                //    Status = "Fail";

                //    String counter = executionLog.readLastLine("counter");
                //    String Description = executionLog.GetAllTextFile("ticketDraft");
                //    String Error = executionLog.GetAllTextFile("Error");
                //    if (counter == "")
                //    {
                //        counter = "0";
                //    }
                //    bool result = loginHelper.CheckExstingIssue("ticketDraft");
                //    if (!result)
                //    {
                //        if (Int16.Parse(counter) < 5)
                //        {
                //            executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                //            loginHelper.CreateIssue("ticketDraft", "Bug", "Medium", "Office page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                //            string id = loginHelper.getIssueID("ticketDraft");
                //            TakeScreenshot("ticketDraft");
                //            string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                //            var location = directoryName + "\\ticketDraft";
                //            loginHelper.AddAttachment(location, id);
                //        }
                //    }
                //    else
                //    {
                //        if (Int16.Parse(counter) < 5)
                //        {
                //            executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                //            TakeScreenshot("ticketDraft");
                //            string id = loginHelper.getIssueID("ticketDraft");
                //            string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                //            var location = directoryName + "\\ticketDraft";
                //            loginHelper.AddAttachment(location, id);
                //            loginHelper.AddComment(loginHelper.getIssueID("ticketDraft"), "This issue is still occurring");
                //        }
                //    }
                //    JIRA = loginHelper.getIssueID("ticketDraft");
                //    executionLog.DeleteFile("Error");
                //    throw;

                //}
                //finally
                //{
                //   // executionLog.DeleteFile("ticketDraft");
                //    executionLog.WriteInExcel("ticketDraft", Status, JIRA, "Office");
                //}

            }
        }
    }
}
