using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class VerifyNotesPage : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("SELENIUM_TESTCASE")]
        [TestCategory("TS8")]
        public void verifyNotesPage()
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
            var officeActivities_NotesHelper = new OfficeActivities_NotesHelper(GetWebDriver());

            // Variable

            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("AddressTypeProfilePage", "Login with valid credential  Username");
                Login(username[0], password[0]);

                executionLog.Log("AddressTypeProfilePage", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");
                officeActivities_NotesHelper.WaitForWorkAround(6000);

                executionLog.Log("AddressTypeProfilePage", "Goto Notes.");
                VisitOffice("notes");
                officeActivities_NotesHelper.WaitForWorkAround(2000);

                executionLog.Log("AddressTypeProfilePage", "Verify titloe Notes.");
                VerifyTitle("Notes");
                officeActivities_NotesHelper.WaitForWorkAround(3000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("AmexRateCorp");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Amex Rate Corp");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Amex Rate Corp", "Bug", "Medium", "Amex page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Amex Rate Corp");
                        TakeScreenshot("AmexRateCorp");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AmexRateCorp.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("AmexRateCorp");
                        string id = loginHelper.getIssueID("Amex Rate Corp");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AmexRateCorp.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Amex Rate Corp"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Amex Rate Corp");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("AmexRateCorp");
                executionLog.WriteInExcel("Amex Rate Corp", Status, JIRA, "Corp Master Data");
            }
        }
    }
}
