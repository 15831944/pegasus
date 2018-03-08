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
    public class AdminMasterDataResolutionUrlChange : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS1")]
        [TestCategory("BugTest")]
        public void adminMasterDataResolutionUrlChange()
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

                executionLog.Log("AdminMasterDataResolutionUrlChange", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("AdminMasterDataResolutionUrlChange", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("AdminMasterDataResolutionUrlChange", "Redirect To Admin");
                VisitOffice("admin");
                tickets_MasterDataHelper.WaitForWorkAround(1000);

                executionLog.Log("AdminMasterDataResolutionUrlChange", "Goto master data Resolution");
                VisitOffice("tickets/masterdata/resolution");

                executionLog.Log("AdminMasterDataResolutionUrlChange", "Click On Issue");
                tickets_MasterDataHelper.ClickElement("ClicKOnIssueResolved");
                tickets_MasterDataHelper.WaitForWorkAround(3000);

                executionLog.Log("AdminMasterDataResolutionUrlChange", "Change the Url");
                VisitOffice("tickets/masterdata/edit/resolution/121");
                tickets_MasterDataHelper.WaitForWorkAround(3000);

                executionLog.Log("AdminMasterDataResolutionUrlChange", "Verify text You don't have privilege");
                tickets_MasterDataHelper.WaitForText("You don't have privilege.", 05);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("AdminMasterDataResolutionUrlChange");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Admin Master Data Resolution Url Change");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Admin Master Data Resolution Url Change", "Bug", "Medium", "Office Admin", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Admin Master Data Resolution Url Change");
                        TakeScreenshot("AdminMasterDataResolutionUrlChange");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AdminMasterDataResolutionUrlChange.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("AdminMasterDataResolutionUrlChange");
                        string id = loginHelper.getIssueID("Admin Master Data Resolution Url Change");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AdminMasterDataResolutionUrlChange.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Admin Master Data Resolution Url Change"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Admin Master Data Resolution Url Change");
                //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("AdminMasterDataResolutionUrlChange");
                executionLog.WriteInExcel("Admin Master Data Resolution Url Change", Status, JIRA, "Admin Tickets");
            }
        }
    }
}
