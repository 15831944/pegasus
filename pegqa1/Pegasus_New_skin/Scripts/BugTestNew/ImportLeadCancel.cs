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
    public class ImportLeadCancel : DriverTestCase
    {
        [TestMethod]
        [TestCategory("Bug")]
        [TestCategory("TS2")]
        [TestCategory("BugTestNew")]
        public void importLeadCancel()
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
            var office_LeadsHelper = new Office_LeadsHelper(GetWebDriver());


            // Random Variables
            var File = GetPathToFile() + "leadsamples.csv";
            String JIRA = "";
            String Status = "Pass";


            try
            {
                executionLog.Log("ImportLeadCancel", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("ImportLeadCancel", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("ImportLeadCancel", "Redirect at import leads page.");
                VisitOffice("leads/import");

                executionLog.Log("ImportLeadCancel", "Upload a file with duplicate records");
                office_LeadsHelper.UploadFile("//*[@id='vcard_file']", File);
                office_LeadsHelper.WaitForWorkAround(2000);

                executionLog.Log("ImportLeadCancel", "Click on import button");
                office_LeadsHelper.ClickElement("LeadImport");
                office_LeadsHelper.WaitForWorkAround(1000);

                executionLog.Log("ImportLeadCancel", "Scroll to merge button");
                office_LeadsHelper.ScrollDown("//div[@class='row']/div/a[@title='Merge']");

                executionLog.Log("ImportLeadCancel", "Click on merge button");
                office_LeadsHelper.ClickElement("MergeDuplicate");
                office_LeadsHelper.WaitForWorkAround(1000);

                executionLog.Log("ImportLeadCancel", "Decline the alert message by click cancel.");
                office_LeadsHelper.DeclineAlert();

                executionLog.Log("ImportLeadCancel", "Again click on merge button.");
                office_LeadsHelper.ClickElement("MergeDuplicate");
                office_LeadsHelper.WaitForWorkAround(2000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("ImportLeadCancel");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Import Lead Cancel");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Import Lead Cancel", "Bug", "Medium", "Leads page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Import Lead Cancel");
                        TakeScreenshot("ImportLeadCancel");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ImportLeadCancel.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("ImportLeadCancel");
                        string id = loginHelper.getIssueID("Import Lead Cancel");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ImportLeadCancel.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Import Lead Cancel"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Import Lead Cancel");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("ImportLeadCancel");
                executionLog.WriteInExcel("Import Lead Cancel", Status, JIRA, "Leads Management");
            }
        }
    }
}