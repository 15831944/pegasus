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
    public class MergeContactCancel : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS2")]
        [TestCategory("BugTestNew")]
        public void mergeContactCancel()
        {
            string[] username = null;
            string[] password = null;

            XMLParse oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            //Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var office_ContactsHelper = new Office_ContactsHelper(GetWebDriver());

            // Random Variable
            var File = GetPathToFile() + "contactsamples.csv";
            String JIRA = "";
            String Status = "Pass";


            try
            {
                executionLog.Log("MergeContactCancel", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("MergeContactCancel", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("MergeContactCancel", "Redirect at import contacts page.");
                VisitOffice("contacts/import");

                executionLog.Log("MergeContactCancel", "Upload a csv file");
                office_ContactsHelper.Upload("UploadFile", File);
                office_ContactsHelper.WaitForWorkAround(4000);

                executionLog.Log("MergeContactCancel", "Click on import button");
                office_ContactsHelper.ClickElement("ImportBtn");
                office_ContactsHelper.WaitForWorkAround(3000);

                executionLog.Log("MergeContactCancel", "Scroll to merge button");
                office_ContactsHelper.ScrollDown("//a[@title='Merge']");

                executionLog.Log("MergeContactCancel", "Click on merge button");
                office_ContactsHelper.ClickElement("ClickOnMergeBtn");
                office_ContactsHelper.WaitForWorkAround(3000);

                executionLog.Log("MergeContactCancel", "Decline the alert message.");
                office_ContactsHelper.DeclineAlert();

                executionLog.Log("MergeContactCancel", "Click on merge button.");
                office_ContactsHelper.ClickElement("ClickOnMergeBtn");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("MergeContactCancel");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Merge Contact Cancel");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Merge Contact Cancel", "Bug", "Medium", "Contacts page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Merge Contact Cancel");
                        TakeScreenshot("MergeContactCancel");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\MergeContactCancel.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("MergeContactCancel");
                        string id = loginHelper.getIssueID("Merge Contact Cancel");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\MergeContactCancel.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Merge Contact Cancel"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Merge Contact Cancel");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("MergeContactCancel");
                executionLog.WriteInExcel("Merge Contact Cancel", Status, JIRA, "Contact Management");
            }
        }
    }
}