using System;
using System.IO;
using LinqToExcel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class VerifySuccessMessage : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS5")]
        [TestCategory("NewSkinTaskJira")]
        public void verifySuccessMessage()
        {
            string[] username = null;
            string[] password = null;
            string[] log = null;
            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");
            log = oXMLData.getData("settings/URL", "logout");
            String JIRA = "";
            String Status = "Pass";

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var office_LeadsHelper = new Office_LeadsHelper(GetWebDriver());

            try
            {
                executionLog.Log("VerifySuccessMessage", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifySuccessMessage", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifySuccessMessage", "Go to Import Leads page");
                VisitOffice("leads/import");

                executionLog.Log("VerifySuccessMessage", "Verify title");
                VerifyTitle("Leads");

                string file = GetPathToFile() + "leadslist.csv";
                executionLog.Log("VerifySuccessMessage", "Uplaod file");
                office_LeadsHelper.UploadFile("//*[@id='vcard_file']", file);

                executionLog.Log("VerifySuccessMessage", "Click on Import button");
                office_LeadsHelper.ClickElement("LeadImport");

                executionLog.Log("VerifySuccessMessage", "Verify success message");
                office_LeadsHelper.WaitForText("Records Imported Successfully.", 10);

                executionLog.Log("VerifySuccessMessage", "Log out from the application");
                VisitOffice("logout");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifySuccessMessage");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Success Message");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Success Message", "Bug", "Medium", "Lead page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Success Message");
                        TakeScreenshot("VerifySuccessMessage");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifySuccessMessage.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifySuccessMessage");
                        string id = loginHelper.getIssueID("Verify Success Message");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifySuccessMessage.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Success Message"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Success Message");
           //     executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifySuccessMessage");
                executionLog.WriteInExcel("Verify Success Message", Status, JIRA, "Leads Management");
            }
        }
    }
}
