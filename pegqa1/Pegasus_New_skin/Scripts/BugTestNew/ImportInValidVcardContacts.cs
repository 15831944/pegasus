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
    public class ImportInValidVcardContacts : DriverTestCase
    {
        [TestCategory("Bug")]
        [TestCategory("TS2")]
        [TestCategory("BugTestNew")]
        [TestMethod]
        public void importInValidVcardContacts()
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
            var File = GetPathToFile() + "leadsamples.csv";
            String JIRA = "";
            String Status = "Pass";


            try
            {
                executionLog.Log("ImportInValidVcardContacts", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("ImportInValidVcardContacts", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("ImportInValidVcardContacts", "Redirect at contacts page.");
                VisitOffice("contacts");

                executionLog.Log("ImportInValidVcardContacts", "Click on Import Vcard.");
                office_ContactsHelper.ClickElement("ImportVCard");

                executionLog.Log("ImportInValidVcardContacts", "Browse a file");
                office_ContactsHelper.Upload("BrowseVcard", File);

                executionLog.Log("ImportInValidVcardContacts", "Click on import button");
                office_ContactsHelper.ClickElement("ImportBtn");
                office_ContactsHelper.WaitForWorkAround(1000);

                executionLog.Log("ImportInValidVcardContacts", "Wait for invalid file message.");
                office_ContactsHelper.WaitForText("vCard: invalid vCard", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("ImportInValidVcardContacts");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Import InValid Vcard Contacts");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Import InValid Vcard Contacts", "Bug", "Medium", "Contacts page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Import InValid Vcard Contacts");
                        TakeScreenshot("ImportInValidVcardContacts");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ImportInValidVcardContacts.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("ImportInValidVcardContacts");
                        string id = loginHelper.getIssueID("Import InValid Vcard Contacts");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ImportInValidVcardContacts.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Import InValid Vcard Contacts"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Import InValid Vcard Contacts");
               // executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("ImportInValidVcardContacts");
                executionLog.WriteInExcel("Import InValid Vcard Contacts", Status, JIRA, "Contacts Management");
            }
        }
    }
}