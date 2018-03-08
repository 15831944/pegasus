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
    public class VerifyInvalidContactFile : DriverTestCase
    {
        [TestCategory("Bug")]
        [TestCategory("TS3")]
        [TestCategory("BugTestNew")]
        [TestMethod]
        public void verifyInvalidContactFile()
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
            var office_ContactsHelper = new Office_ContactsHelper(GetWebDriver());

            // Random variables
            var File = GetPathToFile() + "contactsamplesInvalid.csv";
            String JIRA = "";
            String Status = "Pass";


            try
            {
                executionLog.Log("VerifyInvalidContactFile", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyInvalidContactFile", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyInvalidContactFile", "Redirect at contacts page.");
                VisitOffice("contacts");

                executionLog.Log("VerifyInvalidContactFile", "Verify page title.");
                VerifyTitle("Contacts");

                executionLog.Log("VerifyInvalidContactFile", "Click on Import Vcard.");
                office_ContactsHelper.ClickElement("Import");

                executionLog.Log("VerifyInvalidContactFile", "Browse a file");
                office_ContactsHelper.Upload("BrowseVcard", File);

                executionLog.Log("VerifyInvalidContactFile", "Click on import button");
                office_ContactsHelper.ClickElement("ImportBtn");
                office_ContactsHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyInvalidContactFile", "Wait for invalid file message.");
                office_ContactsHelper.WaitForText("0 Records Imported Successfully.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyInvalidContactFile");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Invalid Contact File");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Invalid Contact File", "Bug", "Medium", "Contacts page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Invalid Contact File");
                        TakeScreenshot("VerifyInvalidContactFile");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyInvalidContactFile.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyInvalidContactFile");
                        string id = loginHelper.getIssueID("Verify Invalid Contact File");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyInvalidContactFile.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Invalid Contact File"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Invalid Contact File");
               // executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyInvalidContactFile");
                executionLog.WriteInExcel("Verify Invalid Contact File", Status, JIRA, "Contacts Management");
            }
        }
    }
}