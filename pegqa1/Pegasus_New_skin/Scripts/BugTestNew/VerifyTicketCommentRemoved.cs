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
    public class VerifyTicketCommentRemoved : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        public void verifyTicketCommentRemoved()
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
            var ticket_CreateATicketHelper = new OfficeTickets_CreateTicketsHelper(GetWebDriver());

            // Variable
            var File = GetPathToFile() + "test.msg";
            String JIRA = "";
            String Status = "Pass";

            //        try
            //      {
            executionLog.Log("VerifyTicketCommentRemoved", "Login with valid username and password");
            Login(username[0], password[0]);
            Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

            executionLog.Log("VerifyTicketCommentRemoved", "Verify Page title");
            VerifyTitle("Dashboard");

            executionLog.Log("VerifyTicketCommentRemoved", "Redirect To Tickets");
            VisitOffice("tickets");

            executionLog.Log("VerifyTicketCommentRemoved", "Verify Page title");
            VerifyTitle("Tickets");

            executionLog.Log("VerifyTicketCommentRemoved", "Verify Page title");
            ticket_CreateATicketHelper.ClickElement("ClickOnTicket");

            executionLog.Log("VerifyTicketCommentRemoved", "Verify Page title");
            ticket_CreateATicketHelper.WaitForElementPresent("AddComment", 10);

            executionLog.Log("VerifyTicketCommentRemoved", "Verify Page title");
            ticket_CreateATicketHelper.ClickElement("AddComment");

            executionLog.Log("VerifyTicketCommentRemoved", "Verify Page title");
            ticket_CreateATicketHelper.WaitForElementPresent("CommentDescryption", 10);

            executionLog.Log("VerifyTicketCommentRemoved", "Verify Page title");
            ticket_CreateATicketHelper.TypeText("CommentDescryption", "This is a test ticket");
            ticket_CreateATicketHelper.WaitForWorkAround(4000);

            executionLog.Log("VerifyTicketCommentRemoved", "Verify Page title");
            ticket_CreateATicketHelper.UploadFile("//*[@id='DocumentFile']", File);
            ticket_CreateATicketHelper.WaitForWorkAround(2000);

            executionLog.Log("VerifyTicketCommentRemoved", "Verify Page title");
            ticket_CreateATicketHelper.VerifyPageText("test.msg");

            executionLog.Log("VerifyTicketCommentRemoved", "Verify Page title");
            ticket_CreateATicketHelper.ClickElement("DeleteAttachment");
            ticket_CreateATicketHelper.WaitForWorkAround(2000);

            executionLog.Log("VerifyTicketCommentRemoved", "Verify Page title");
            ticket_CreateATicketHelper.VerifyPageText("No File Selected.");

        }
    }
}
/*       catch (Exception e)
       {
           executionLog.Log("Error", e.StackTrace);
           Status = "Fail";

           String counter = executionLog.readLastLine("counter");
           String Description = executionLog.GetAllTextFile("VerifyTicketCommentRemoved");
           String Error = executionLog.GetAllTextFile("Error");
           if (counter == "")
           {
               counter = "0";
           }
           bool result = loginHelper.CheckExstingIssue("Edit Ticket Verify Responsibility");
           if (!result)
           {
               if (Int16.Parse(counter) < 5)
               {
                   executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                   loginHelper.CreateIssue("Edit Ticket Verify Responsibility", "Bug", "Medium", "Ticket page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                   string id = loginHelper.getIssueID("Edit Ticket Verify Responsibility");
                   TakeScreenshot("VerifyTicketCommentRemoved");
                   string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                   var location = directoryName + "\\VerifyTicketCommentRemoved.png";
                   loginHelper.AddAttachment(location, id);
               }
           }
           else
           {
               if (Int16.Parse(counter) < 5)
               {
                   executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                   TakeScreenshot("VerifyTicketCommentRemoved");
                   string id = loginHelper.getIssueID("Edit Ticket Verify Responsibility");
                   string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                   var location = directoryName + "\\VerifyTicketCommentRemoved.png";
                   loginHelper.AddAttachment(location, id);
                   loginHelper.AddComment(loginHelper.getIssueID("Edit Ticket Verify Responsibility"), "This issue is still occurring");
               }
           }
           JIRA = loginHelper.getIssueID("Edit Ticket Verify Responsibility");
           executionLog.DeleteFile("Error");
           throw;

       }
       finally
       {
           executionLog.DeleteFile("VerifyTicketCommentRemoved");
           executionLog.WriteInExcel("Edit Ticket Verify Responsibility", Status, JIRA, "Office tickets");
       }
   }
}
}*/
