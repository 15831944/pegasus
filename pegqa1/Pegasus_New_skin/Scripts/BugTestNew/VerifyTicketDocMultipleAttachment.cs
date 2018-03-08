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
    public class VerifyTicketDocMultipleAttachment : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        public void verifyTicketDocMultipleAttachment()
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
            var officeTickets_AllTicketsHelper = new OfficeTickets_AllTicketsHelper(GetWebDriver());

            // Variable
            var name = "Ticket" + RandomNumber(1, 999);
            var File1 = GetPathToFile() + "2.pdf";
            var File2 = GetPathToFile() + "1.pdf";
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

            executionLog.Log("VerifyTicketCommentRemoved", "Click on any ticket.");
            officeTickets_AllTicketsHelper.ClickElement("ClickTicket1");

            executionLog.Log("VerifyTicketCommentRemoved", "Wait for locator to be present.");
            officeTickets_AllTicketsHelper.WaitForElementPresent("AddDoc", 10);

            executionLog.Log("VerifyTicketCommentRemoved", "Click on add document.");
            officeTickets_AllTicketsHelper.ClickElement("AddDoc");

            executionLog.Log("VerifyTicketCommentRemoved", "Wait for locator to be present.");
            officeTickets_AllTicketsHelper.WaitForElementPresent("DocName", 10);

            executionLog.Log("VerifyTicketCommentRemoved", "Enter document name.");
            officeTickets_AllTicketsHelper.TypeText("DocName", name);

            executionLog.Log("VerifyTicketCommentRemoved", "Browse first file.");
            officeTickets_AllTicketsHelper.UploadFile("//*[@id='DocumentFiles']", File1);
            officeTickets_AllTicketsHelper.WaitForWorkAround(2000);

            executionLog.Log("VerifyTicketCommentRemoved", "Browse second file.");
            officeTickets_AllTicketsHelper.UploadFile("//*[@id='DocumentFiles']", File2);
            officeTickets_AllTicketsHelper.WaitForWorkAround(2000);

            executionLog.Log("VerifyTicketCommentRemoved", "Click on save button.");
            officeTickets_AllTicketsHelper.ClickElement("SaveDoc");

            executionLog.Log("VerifyTicketCommentRemoved", "Redirect at documents page.");
            VisitOffice("documents");

            executionLog.Log("VerifyTicketCommentRemoved", "Verify page title as documents.");
            VerifyTitle("Documents");

            executionLog.Log("VerifyTicketCommentRemoved", "Wait for locator to be present.");
            //         officeTickets_AllTicketsHelper.WaitForElementPresent("File1", 10);

            officeTickets_AllTicketsHelper.TypeText("SearchDoc", name);
            officeTickets_AllTicketsHelper.WaitForWorkAround(3000);

            executionLog.Log("VerifyTicketCommentRemoved", "Verify first attachment.");
            officeTickets_AllTicketsHelper.VerifyText("File1", "1.pdf");

            executionLog.Log("VerifyTicketCommentRemoved", "Verify second attachment.");
            officeTickets_AllTicketsHelper.VerifyText("File2", "2.pdf");

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
