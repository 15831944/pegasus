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
    public class VerfiyTicketSettingAssignedToAndManagerSave : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("BugTestNew")]
        public void verfiyTicketSettingAssignedToAndManagerSave()
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
            var ticket_SettingsHelper = new Ticket_SettingsHelper(GetWebDriver());

            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("VerfiyTicketSettingAssignedToAndManagerSave", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerfiyTicketSettingAssignedToAndManagerSave", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerfiyTicketSettingAssignedToAndManagerSave", "Redirect at ticket topic page.");
                VisitOffice("tickets/settings");
                ticket_SettingsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerfiyTicketSettingAssignedToAndManagerSave", "Select set as Department");
                ticket_SettingsHelper.ClickElement("SetDept");
                ticket_SettingsHelper.Click("//*[@id='assign0Department']/option[1]");

                executionLog.Log("VerfiyTicketSettingAssignedToAndManagerSave", "Select set as Assigned To");
                ticket_SettingsHelper.SelectByText("SetAssignto", "Howard Tang");

                executionLog.Log("VerfiyTicketSettingAssignedToAndManagerSave", "Select set as Assigned Manager");
                ticket_SettingsHelper.SelectByText("SetAssignMgr", "Howard Tang");

                executionLog.Log("VerfiyTicketSettingAssignedToAndManagerSave", "Click on Save button.");
                ticket_SettingsHelper.ClickElement("SaveBtn");
                ticket_SettingsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerfiyTicketSettingAssignedToAndManagerSave", "Verify settings saved");
                ticket_SettingsHelper.VerifySlctdOptn("SetAssignto", "Howard Tang");
                ticket_SettingsHelper.VerifySlctdOptn("SetAssignMgr", "Howard Tang");
            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerfiyTicketSettingAssignedToAndManagerSave");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verfiy Ticket Setting Assigned To And Manager Save");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verfiy Ticket Setting Assigned To And Manager Save", "Bug", "Medium", "Ticket Admin page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verfiy Ticket Setting Assigned To And Manager Save");
                        TakeScreenshot("VerfiyTicketSettingAssignedToAndManagerSave");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerfiyTicketSettingAssignedToAndManagerSave.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerfiyTicketSettingAssignedToAndManagerSave");
                        string id = loginHelper.getIssueID("Verfiy Ticket Setting Assigned To And Manager Save");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerfiyTicketSettingAssignedToAndManagerSave.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verfiy Ticket Setting Assigned To And Manager Save"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verfiy Ticket Setting Assigned To And Manager Save");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerfiyTicketSettingAssignedToAndManagerSave");
                executionLog.WriteInExcel("Verfiy Ticket Setting Assigned To And Manager Save", Status, JIRA, "Admin Ticket settings");
            }
        }
    }
}