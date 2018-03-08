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
    public class VerifyTicketResolution : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("BugTestNew")]
        public void verifyTicketResolution()
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
            var ticket_ViewATicketHelper = new OfficeTickets_ViewTicketsHelper(GetWebDriver());

            // Variable
            
            
            var TickName = "Test Ticket" + GetRandomNumber();

            String JIRA = "";
            String Status = "Pass";

            //try
            //{
                executionLog.Log("VerifyTicketResolution", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyTicketResolution", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("VerifyTicketResolution", "Redirect To Create Tickets");
                VisitOffice("tickets/create");

                executionLog.Log("VerifyTicketResolution", "Verify Page title");
                VerifyTitle("Create a Ticket");

                executionLog.Log("VerifyTicketResolution", "Enter Ticket Name");
                ticket_CreateATicketHelper.TypeText("TicketName", TickName);
                ticket_CreateATicketHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyTicketResolution", "Select the status");
                ticket_CreateATicketHelper.SelectByText("TickStatus", "Open");
                ticket_CreateATicketHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyTicketResolution", "Click On Select Client Button");
                ticket_CreateATicketHelper.ClickElement("ClientDisplayIcon");
                ticket_CreateATicketHelper.WaitForWorkAround(4000);

                executionLog.Log("VerifyTicketResolution", "Search DBA Client by Company Name");
                ticket_CreateATicketHelper.TypeText("CompanyName", "Thompson");

                executionLog.Log("VerifyTicketResolution", "Search Button");
                ticket_CreateATicketHelper.ClickElement("SearchBtn");
                ticket_CreateATicketHelper.WaitForWorkAround(4000);

                
                executionLog.Log("VerifyTicketResolution", "Click On Client");
                ticket_CreateATicketHelper.ClickElement("ClickOnClient");
                ticket_CreateATicketHelper.WaitForWorkAround(4000);

                executionLog.Log("VerifyTicketResolution", "Assigned To ");
                ticket_CreateATicketHelper.SelectByText("Assignedto", "Howard Tang");

                executionLog.Log("VerifyTicketResolution", "Assigned Manager");
                ticket_CreateATicketHelper.SelectByText("AssignedManager", "Howard Tang");

                executionLog.Log("VerifyTicketResolution", "Click on Save");
                ticket_CreateATicketHelper.ClickElement("ClickOnSaveTicket");
                ticket_CreateATicketHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTicketResolution", "Verify Ticket Created Successfully.");
                ticket_CreateATicketHelper.WaitForText("Ticket Created Successfully.", 10);

                executionLog.Log("VerifyTicketResolution", "Click on Resolve Button");
                ticket_ViewATicketHelper.ClickElement("ResolveBtn");
                ticket_ViewATicketHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyTicketResolution", "Click on Resolution Drop down");
                ticket_ViewATicketHelper.ClickElement("Resolution");
                ticket_ViewATicketHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTicketResolution", "Click on Drop down options present");
                ticket_ViewATicketHelper.IsElementVisible("//option[@value='Resolved']");
    
            //}
            //catch (Exception e)
            //{
            //}
            
        }
    }
}
