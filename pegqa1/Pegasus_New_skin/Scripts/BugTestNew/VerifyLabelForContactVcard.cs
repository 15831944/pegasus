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
    public class VerifyLabelForContactVcard : DriverTestCase
    {
        [TestCategory("Bug")]
        [TestCategory("TS3")]
        [TestCategory("BugTestNew")]
        [TestMethod]
        public void verifyLabelForContactVcard()
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
                executionLog.Log("VerifyLabelForContactVcard", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyLabelForContactVcard", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyLabelForContactVcard", "Redirect at contacts page.");
                VisitOffice("contacts");

                executionLog.Log("VerifyLabelForContactVcard", "Verify page title as contacts.");
                VerifyTitle("Contacts");

                executionLog.Log("VerifyLabelForContactVcard", "Click on any contact.");
                office_ContactsHelper.ClickElement("Contact1");

                executionLog.Log("VerifyLabelForContactVcard", "Verify Download as vcard label present on the page..");
                office_ContactsHelper.IsElementPresent("VcardDownload");

                executionLog.Log("VerifyLabelForContactVcard", "Logout from the application.");
                VisitOffice("logout");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyLabelForContactVcard");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Label For Contact Vcard");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Label For Contact Vcard", "Bug", "Medium", "Contacts page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Label For Contact Vcard");
                        TakeScreenshot("VerifyLabelForContactVcard");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyLabelForContactVcard.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyLabelForContactVcard");
                        string id = loginHelper.getIssueID("Verify Label For Contact Vcard");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyLabelForContactVcard.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Label For Contact Vcard"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Label For Contact Vcard");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyLabelForContactVcard");
                executionLog.WriteInExcel("Verify Label For Contact Vcard", Status, JIRA, "Contacts Management");
            }
        }
    }
}