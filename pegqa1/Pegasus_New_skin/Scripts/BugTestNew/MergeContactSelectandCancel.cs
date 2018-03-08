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
    public class MergeContactSelectandCancel : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS2")]
        [TestCategory("BugTestNew")]
        public void mergeContactSelectandCancel()
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


            // Random Variables
            var File = GetPathToFile() + "contactsamples.csv";
            String JIRA = "";
            String Status = "Pass";
            try
            {

                executionLog.Log("MergeContactSelectandCancel", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("MergeContactSelectandCancel", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("MergeContactSelectandCancel", "Redirect at import contacts page.");
                VisitOffice("contacts/import");

                executionLog.Log("MergeContactSelectandCancel", "Upload a csv file");
                office_ContactsHelper.Upload("UploadFile", File);

                executionLog.Log("MergeContactSelectandCancel", "Click on import button. ");
                office_ContactsHelper.ClickElement("ImportBtn");
                office_ContactsHelper.WaitForWorkAround(1000);

                executionLog.Log("MergeContactSelectandCancel", "Select a primary contact");
                office_ContactsHelper.ClickElement("MergeRadio");

                executionLog.Log("MergeContactSelectandCancel", "Scroll to merge button");
                office_ContactsHelper.ScrollDown("//a[@title='Merge']");

                executionLog.Log("MergeContactSelectandCancel", "Click on merge button.");
                office_ContactsHelper.ClickElement("ClickOnMergeBtn");
                office_ContactsHelper.WaitForWorkAround(3000);

                executionLog.Log("MergeContactSelectandCancel", "Click on cancel to decline alert");
                office_ContactsHelper.DeclineAlert();

                executionLog.Log("MergeContactSelectandCancel", "Click on merge button.");
                office_ContactsHelper.ClickElement("ClickOnMergeBtn");

            }

            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("MergeContactSelectandCancel");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Merge Contact Select and Cancel");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Merge Contact Select and Cancel", "Bug", "Medium", "Contacts page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Merge Contact Select and Cancel");
                        TakeScreenshot("MergeContactSelectandCancel");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\MergeContactSelectandCancel.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("MergeContactSelectandCancel");
                        string id = loginHelper.getIssueID("Merge Contact Select and Cancel");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\MergeContactSelectandCancel.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Merge Contact Select and Cancel"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Merge Contact Select and Cancel");
               // executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("MergeContactSelectandCancel");
                executionLog.WriteInExcel("Merge Contact Select and Cancel", Status, JIRA, "Contact Management");
            }
        }
    }
}