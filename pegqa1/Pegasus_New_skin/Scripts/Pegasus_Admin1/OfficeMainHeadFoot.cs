using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;
using System.Diagnostics;
namespace Pegasus_New_skin.Scripts
{
  //  [TestClass]
    public class OfficeMainHeadFoot : DriverTestCase
    {
        ExecutionLog executionLog = new ExecutionLog();
        string[] username = null;
        string[] password = null;
        //define a array which includes the path for every pages.
        string[] OMArray =
        {   "HomeTab",
            "OpportunitiesTab","OpportunitiesTab+CreateanOpportunity","OpportunitiesTab+ImportOpportunities","OpportunitiesTab+ExportOpportunities","OpportunitiesTab+OpportunitiesRecycleBin",
            "LeadsTab","LeadsTab+CreateaLead","LeadsTab+ImportLeads","LeadsTab+ExportLeads","LeadsTab+LeadsRecycleBin",
            "ClientsTab","ClientsTab+CreateaClient","ClientsTab+ImportClients","ClientsTab+ExportClients","ClientsTab+ClientsRecycleBin",
            "ContactsTab","ContactsTab+CreateaContact","ContactsTab+ImportContacts","ContactsTab+ExportContacts","ContactsTab+ContactsRecycleBin",
            "ActivitiesTab+Meetings","ActivitiesTab+Meetings+CreateaMeeting","ActivitiesTab+Meetings+MyUpcomingMeetings","ActivitiesTab+Meetings+MyRecentMeetings","ActivitiesTab+Meetings+MyCalendar","ActivitiesTab+Meetings+MyTeamUpcomingMeetings","ActivitiesTab+Meetings+MyTeamRecentMeetings","ActivitiesTab+Meetings+MyTeamCalendar",
            "ActivitiesTab+Tasks","ActivitiesTab+Tasks+CreateaTask","ActivitiesTab+Tasks+MyUpcomingTasks","ActivitiesTab+Tasks+MyRecentTasks","ActivitiesTab+Tasks+MyPastDueTasks","ActivitiesTab+Tasks+MyTeamUpcomingTasks","ActivitiesTab+Tasks+MyTeamRecentTasks","ActivitiesTab+Tasks+MyTeamPastDue",
            "ActivitiesTab+E-Mails","ActivitiesTab+E-Mails+ComposeE-Mail","ActivitiesTab+E-Mails+MyDrafts","ActivitiesTab+E-Mails+MySentE-Mails","ActivitiesTab+E-Mails+MyArchivedE-Mails","ActivitiesTab+E-Mails+MyTeamSentE-Mails","ActivitiesTab+E-Mails+MyTeamArchivedE-Mails","ActivitiesTab+E-Mails+E-MailAccounts",
            "ActivitiesTab+Notes","ActivitiesTab+Notes+CreateaNote","ActivitiesTab+Notes+NotesRecentlyAdded","ActivitiesTab+Notes+NotesRecentlyE-Mailed","ActivitiesTab+Notes+MyNotesRecentlyAdded","ActivitiesTab+Notes+MyNotesRecentlyE-Mailed","ActivitiesTab+Notes+MyTeamNotesRecentlyAdded","ActivitiesTab+Notes+MyTeamNotesRecentlyE-Mailed",
            "ActivitiesTab+Documents","ActivitiesTab+Documents+CreateaDocument","ActivitiesTab+Documents+DocumentsRecentlyAdded","ActivitiesTab+Documents+DocumentsRecentlyE-Mailed","ActivitiesTab+Documents+MyDocumentsRecentlyAdded","ActivitiesTab+Documents+MyDocumentsRecentlyE-Mailed","ActivitiesTab+Documents+MyTeamDocumentsRecentlyAdded","ActivitiesTab+Documents+MyTeamDocumentsRecentlyE-Mailed",
            "ActivitiesTab+ActiveCalls","ActivitiesTab+ActiveCalls+LogaCall","ActivitiesTab+ActiveCalls+MyActiveCalls",
            "TicketsTab","TicketsTab+CreateaTicket","TicketsTab+TicketDrafts","TicketsTab+TicketRecycleBin","TicketsTab+ViewAllTickets","TicketsTab+ViewAllTickets+NewTickets","TicketsTab+ViewAllTickets+OpenTickets","TicketsTab+ViewAllTickets+ResolvedTickets","TicketsTab+ViewAllTickets+ClosedTickets",
            "AgentsTab+AllAgents","AgentsTab+Employees","AgentsTab+SalesAgents","AgentsTab+PartnerAgents","AgentsTab+PartnerAssociations",
            "ResidualIncomeTab+OfficePayouts+OfficePayoutsSummary","ResidualIncomeTab+OfficePayouts+DetailedPayouts","ResidualIncomeTab+OfficePayouts+ViewReports","ResidualIncomeTab+OfficePayouts+SummaryReports","ResidualIncomeTab+OfficePayouts+AgentPayoutsSummary",
            "ResidualIncomeTab+UserPayouts+UserPayoutsSummary","ResidualIncomeTab+UserPayouts+UserPayoutsReports",
            "ResidualIncomeTab+MasterData+AdjustmentsTool","ResidualIncomeTab+MasterData+PayoutLookupSettings","ResidualIncomeTab+MasterData+CalculationHierarchySettings","ResidualIncomeTab+MasterData+ReportDisplayColumns",
            "ReportsTab","ReportsTab+Reports+AllReports","ReportsTab+Reports+CreateaReport","ReportsTab+Reports+SnapshotReport",
            "ReportsTab+Dashboards+AllDashboards","ReportsTab+Dashboards+CreateaDashboard","ReportsTab+Dashboards+LeadsPerformance","ReportsTab+Dashboards+ClientsPerformance","ReportsTab+Dashboards+CallsPerformance",
            "ReportsTab+ReportCalls+AllCalls","ReportsTab+ReportCalls+LeadCalls","ReportsTab+ReportCalls+ClientCalls","ReportsTab+ReportCalls+UnlinkedCalls",



        };
        //common layout of head
        string[] OMHead = { "HeadImage", "Header", "HeadDropDown", "HeadText","HeadInput" };
        //common layout of menu
        string[] OMMenu = { "HomeTab", "OpportunitiesTab", "LeadsTab", "ClientsTab", "ContactsTab", "ActivitiesTab", "TicketsTab", "AgentsTab", "ResidualIncomeTab", "ReportsTab" };
        OfficeMainHelper OMHelper = null;
        
        
        [TestInitialize]
        public void Initialize()
        {
            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");
            OMHelper = new OfficeMainHelper(GetWebDriver());


        }
        //parse the input path array and then go to each page,separate the strings by '+', 
        //mouseover the elements in each string, and click the last element
        public void iterHelper(string[] inArray)
        {


            int len = inArray.Length;
            int curlen;
            string[] current = null;
            for (int i = 0; i < len; i++)
            {
                executionLog.Log("OfficeMainHeadFoot", inArray[i]);
                current = inArray[i].Split('+');
                curlen = current.Length;
                for (int j = 0; j < curlen - 1; j++)
                {

                    OMHelper.MouseHover(current[j]);
                    OMHelper.WaitForWorkAround(2500);
                }


                OMHelper.ClickElement(current[curlen - 1]);
                OMHelper.WaitForWorkAround(3000);
                //check the common layout for each page
                foreach (string element in OMHead)
                {
                   Assert.IsTrue(OMHelper.ElementVisible(element));
                }

            }

        }
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin1")]
        [TestCategory("TS6")]
        [TestCategory("Pegasus_Admin1")]
        public void TestOM()
        {
            var loginHelper = new LoginHelper(GetWebDriver());
            var usernme = "Sysprins" + RandomNumber(44, 799999977);
            var name = "Test" + GetRandomNumber();
            string JIRA = "";
            string Status = "Pass";

            //try
            //{
                Login(username[0], password[0]);
                OMHelper.WaitForWorkAround(3000);
                iterHelper(OMArray);
        //    }
        //    catch(Exception e)
        //    {
        //        Console.WriteLine("ERRROROOR");
        //        executionLog.Log("Error", e.StackTrace);
        //        Status = "Fail";

        //        String counter = executionLog.readLastLine("counter");
        //        String Description = executionLog.GetAllTextFile("OfficeMainHeadFoot");
        //        String Error = executionLog.GetAllTextFile("Error");
        //        if (counter == "")
        //        {
        //            counter = "0";
        //        }
        //        bool result = loginHelper.CheckExstingIssue("OfficeMainHeadFoot");
        //        if (!result)
        //        {
        //            if (Int16.Parse(counter) < 9)
        //            {
        //                executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
        //                loginHelper.CreateIssue("OfficeMainHeadFoot", "Bug", "Medium", "Office page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
        //                string id = loginHelper.getIssueID("OfficeMainHeadFoot");
        //                TakeScreenshot("OfficeMainHeadFoot");
        //                string directoryName = loginHelper.GetnewDirectoryName(GetPath());
        //                var location = directoryName + "\\OfficeMainHeadFoot";
        //                loginHelper.AddAttachment(location, id);
        //            }
        //        }
        //        else
        //        {
        //            if (Int16.Parse(counter) < 9)
        //            {
        //                executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
        //                TakeScreenshot("OfficeMainHeadFoot");
        //                string id = loginHelper.getIssueID("OfficeMainHeadFoot");
        //                string directoryName = loginHelper.GetnewDirectoryName(GetPath());
        //                var location = directoryName + "\\OfficeMainHeadFoot";
        //                loginHelper.AddAttachment(location, id);
        //                loginHelper.AddComment(loginHelper.getIssueID("OfficeMainHeadFoot"), "This issue is still occurring");
        //            }
        //        }
        //        JIRA = loginHelper.getIssueID("OfficeMainHeadFoot");
        //        executionLog.DeleteFile("Error");
        //        throw;

        //    }
        //    finally
        //    {
        //        executionLog.DeleteFile("OfficeMainHeadFoot");
        //        executionLog.WriteInExcel("OfficeMainHeadFoot", Status, JIRA, "Office");
        //    }
        }

        [TestMethod]
        [TestCategory("All")]
        //check the common layout of office main menu
        public void TestMenuLayout()
        {
            var loginHelper = new LoginHelper(GetWebDriver());
            var usernme = "Sysprins" + RandomNumber(44, 799999977);
            var name = "Test" + GetRandomNumber();
            string JIRA = "";
            string Status = "Pass";
            //try
            //{
                Login(username[0], password[0]);
                OMHelper.WaitForWorkAround(3000);
                foreach (string element in OMMenu)
                {
                    executionLog.Log("OfficeMainMenuLayout", element);
                    Assert.IsTrue(OMHelper.ElementVisible(element));
                }
        //    }


        //       catch (Exception e)
        //    {
        //        Console.WriteLine("ERRROROOR");
        //        executionLog.Log("Error", e.StackTrace);
        //        Status = "Fail";

        //        String counter = executionLog.readLastLine("counter");
        //        String Description = executionLog.GetAllTextFile("OfficeMainMenuLayout");
        //        String Error = executionLog.GetAllTextFile("Error");
        //        if (counter == "")
        //        {
        //            counter = "0";
        //        }
        //        bool result = loginHelper.CheckExstingIssue("OfficeMainMenuLayout");
        //        if (!result)
        //        {
        //            if (Int16.Parse(counter) < 9)
        //            {
        //                executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
        //                loginHelper.CreateIssue("OfficeMainMenuLayout", "Bug", "Medium", "Office page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
        //                string id = loginHelper.getIssueID("OfficeMainMenuLayout");
        //                TakeScreenshot("OfficeMainMenuLayout");
        //                string directoryName = loginHelper.GetnewDirectoryName(GetPath());
        //                var location = directoryName + "\\OfficeMainMenuLayout";
        //                loginHelper.AddAttachment(location, id);
        //            }
        //        }
        //        else
        //        {
        //            if (Int16.Parse(counter) < 9)
        //            {
        //                executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
        //                TakeScreenshot("OfficeMainMenuLayout");
        //                string id = loginHelper.getIssueID("OfficeMainMenuLayout");
        //                string directoryName = loginHelper.GetnewDirectoryName(GetPath());
        //                var location = directoryName + "\\OfficeMainMenuLayout";
        //                loginHelper.AddAttachment(location, id);
        //                loginHelper.AddComment(loginHelper.getIssueID("OfficeMainMenuLayout"), "This issue is still occurring");
        //            }
        //        }
        //        JIRA = loginHelper.getIssueID("OfficeMainMenuLayout");
        //        executionLog.DeleteFile("Error");
        //        throw;

        //    }
        //    finally
        //    {
        //        executionLog.DeleteFile("OfficeMainMenuLayout");
        //        executionLog.WriteInExcel("OfficeMainMenuLayout", Status, JIRA, "Office");
        //    }
        }
    }
}
