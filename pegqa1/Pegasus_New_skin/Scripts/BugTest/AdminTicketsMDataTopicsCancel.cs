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
    public class AdminTicketsMDataTopicsCancel : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS1")]
        [TestCategory("BugTest")]
        public void adminTicketsMDataTopicsCancel()
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
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("AdminTicketsMDataTopicsCancel", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("AdminTicketsMDataTopicsCancel", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("AdminTicketsMDataTopicsCancel", "Redirect To Admin");
                VisitOffice("admin");
                tickets_MasterDataHelper.WaitForWorkAround(3000);

                executionLog.Log("AdminTicketsMDataTopicsCancel", "Goto Resolution page");
                VisitOffice("tickets/masterdata/resolution");

                executionLog.Log("AdminTicketsMDataTopicsCancel", "Click On any resolution");
                tickets_MasterDataHelper.ClickElement("ClicKOnIssueResolved");
                tickets_MasterDataHelper.WaitForWorkAround(3000);

                executionLog.Log("AdminTicketsMDataTopicsCancel", "Click Cancel button");
                tickets_MasterDataHelper.ClickElement("Cancel");
                tickets_MasterDataHelper.WaitForWorkAround(3000);


            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("AdminTicketsMDataTopicsCancel");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Admin Tickets MData Topics Cancel");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Admin Tickets MData Topics Cancel", "Bug", "Medium", "Admin tickets", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Admin Tickets MData Topics Cancel");
                        TakeScreenshot("AdminTicketsMDataTopicsCancel");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AdminTicketsMDataTopicsCancel.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("AdminTicketsMDataTopicsCancel");
                        string id = loginHelper.getIssueID("Admin Tickets MData Topics Cancel");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AdminTicketsMDataTopicsCancel.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Admin Tickets MData Topics Cancel"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Admin Tickets MData Topics Cancel");
                // executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("AdminTicketsMDataTopicsCancel");
                executionLog.WriteInExcel("Admin Tickets MData Topics Cancel", Status, JIRA, "Admin tickets");
            }
        }
    }
}