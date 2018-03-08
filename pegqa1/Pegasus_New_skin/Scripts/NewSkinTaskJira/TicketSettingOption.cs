using System;
using System.IO;
using LinqToExcel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class TicketSettingOption : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS5")]
        [TestCategory("NewSkinTaskJira")]
        public void ticketSettingOption()
        {
            string[] username = null;
            string[] password = null;
            string[] log = null;
            String JIRA = "";
            String Status = "Pass";

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");
            log = oXMLData.getData("settings/URL", "logout");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var ticket_SettingsHelper = new Ticket_SettingsHelper(GetWebDriver());

            try
            {
                executionLog.Log("TicketSettingOption", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("TicketSettingOption", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("TicketSettingOption", "Go to Ticket setting page");
                VisitOffice("tickets/settings");

                executionLog.Log("TicketSettingOption", "Verify title");
                VerifyTitle("Settings");

                executionLog.Log("TicketSettingOption", "Verify options avaliable under set Assigned to as dropdown");
                ticket_SettingsHelper.verifyElementPresent("TSDropdown");

                executionLog.Log("TicketSettingOption", "Verify options avaliable under set Priority as dropdown");
                ticket_SettingsHelper.verifyElementPresent("TSDropdown1");

                executionLog.Log("TicketSettingOption", "Verify options avaliable under set Assigned Manager as dropdown");
                ticket_SettingsHelper.verifyElementPresent("TSDropdown2");

                executionLog.Log("TicketSettingOption", "Log out from the application");
                VisitOffice("logout");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";
                
                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("TicketSettingOption");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Ticket Setting Option");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Ticket Setting Option", "Bug", "Medium", "Ticket settings page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Ticket Setting Option");
                        TakeScreenshot("TicketSettingOption");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\TicketSettingOption.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("TicketSettingOption");
                        string id = loginHelper.getIssueID("Ticket Setting Option");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\TicketSettingOption.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Ticket Setting Option"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Ticket Setting Option");
           //     executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("TicketSettingOption");
                executionLog.WriteInExcel("Ticket Setting Option", Status, JIRA, "Ticketing System");
            }
        }
    }
}