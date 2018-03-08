using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class VerifyModifiedByCredentialsforLead : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS3")]
        [TestCategory("BugTestNew")]
        public void verifyModifiedByCredentialsforLead()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var office_LeadsHelper = new Office_LeadsHelper(GetWebDriver());

            // Variable
            var FirstName = "Test" + RandomNumber(1, 99);
            var LastName = "Tester" + RandomNumber(1, 99);
            var Number = "12345678" + RandomNumber(10, 99);
            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("VerifyModifiedByCredentialsforLead", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyModifiedByCredentialsforLead", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("VerifyModifiedByCredentialsforLead", "Click on Leads in Topmenu");
                VisitOffice("leads");

                executionLog.Log("VerifyModifiedByCredentialsforLead", "Verify Page title");
                VerifyTitle("Leads");

                executionLog.Log("VerifyModifiedByCredentialsforLead", "Click On Any Lead");
                office_LeadsHelper.ClickElement("ClickAnyLead");
                office_LeadsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyModifiedByCredentialsforLead", "Click On Convert lead button");
                office_LeadsHelper.ClickElement("ClickConvert");
                office_LeadsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyModifiedByCredentialsforLead", "Click on save button");
                office_LeadsHelper.ClickElement("SaveConvertLead");
                office_LeadsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyModifiedByCredentialsforLead", "Verify Success text for lead conversion.");
                office_LeadsHelper.VerifyPageText("Lead is converted and moved to recyclebin.");

                executionLog.Log("VerifyModifiedByCredentialsforLead", "");
                office_LeadsHelper.VerifyPageText("Clients");

                executionLog.Log("VerifyModifiedByCredentialsforLead", "Verify Modified by name.");
                office_LeadsHelper.VerifyText("Editedby", "By Howard Tang");
                office_LeadsHelper.WaitForWorkAround(2000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyModifiedByCredentialsforLead");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Modified By Credentials for Lead");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Modified By Credentials for Lead", "Bug", "Medium", "Lead  page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Modified By Credentials for Lead");
                        TakeScreenshot("VerifyModifiedByCredentialsforLead");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyModifiedByCredentialsforLead.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("Verify Modified By Credentials for Lead");
                        string id = loginHelper.getIssueID("Verify Modified By Credentials for Lead");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyModifiedByCredentialsforLead.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Modified By Credentials for Lead"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Modified By Credentials for Lead");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyModifiedByCredentialsforLead");
                executionLog.WriteInExcel("Verify Modified By Credentials for Lead", Status, JIRA, "Leads Management");
            }
        }
    }
}