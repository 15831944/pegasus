using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class VerifyDocumentDisplayedColumnIssue : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS3")]
        [TestCategory("BugTestNew")]
        public void verifyDocumentDisplayedColumnIssue()
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
            var officeActivities_DocumentHelper = new OfficeActivities_DocumentHelper(GetWebDriver());

            // Variable Random
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("VerifyDocumentDisplayedColumnIssue", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyDocumentDisplayedColumnIssue", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyDocumentDisplayedColumnIssue", "Redirect To URL");
                VisitOffice("documents");
                officeActivities_DocumentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyDocumentDisplayedColumnIssue", "Verify page title.");
                VerifyTitle("Documents");

                executionLog.Log("VerifyDocumentDisplayedColumnIssue", "Click on advance filter.");
                officeActivities_DocumentHelper.ClickElement("AdvanceFilter");
                officeActivities_DocumentHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyDocumentDisplayedColumnIssue", "Get text of all the displayed columns.");
                var text = officeActivities_DocumentHelper.GetTextByXpath("//*[@id='display_cols']");
                //officeActivities_DocumentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyDocumentDisplayedColumnIssue", "Print all column names on thr console.");
                Console.WriteLine(text);
                //officeActivities_DocumentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyDocumentDisplayedColumnIssue", "Verify owner column present in displayed column.");
                Assert.IsTrue(text.Contains("Owner"));
                //officeActivities_DocumentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyDocumentDisplayedColumnIssue", "Verify modified column present in displayed column.");
                Assert.IsTrue(text.Contains("Modified"));
                //officeActivities_DocumentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyDocumentDisplayedColumnIssue", "Redirect To URL");
                VisitOffice("documents");
                officeActivities_DocumentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyDocumentDisplayedColumnIssue", "Verify page title.");
                VerifyTitle("Documents");

                executionLog.Log("VerifyDocumentDisplayedColumnIssue", "Verify owner column is visible on the page.");
                officeActivities_DocumentHelper.IsElementPresent("HeadOwner");
                //officeActivities_DocumentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyDocumentDisplayedColumnIssue", "Verify Modified column is visible on the page.");
                officeActivities_DocumentHelper.IsElementPresent("HeadModified");
                //officeActivities_DocumentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyDocumentDisplayedColumnIssue", "Logout from the application.");
                VisitCorp("logout");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyDocumentDisplayedColumnIssue");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Document Displayed Column Issue");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Document Displayed Column Issue", "Bug", "Medium", "Activities page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Document Displayed Column Issue");
                        TakeScreenshot("VerifyDocumentDisplayedColumnIssue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyDocumentDisplayedColumnIssue.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyDocumentDisplayedColumnIssue");
                        string id = loginHelper.getIssueID("Verify Document Displayed Column Issue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyDocumentDisplayedColumnIssue.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Document Displayed Column Issue"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Document Displayed Column Issue");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyDocumentDisplayedColumnIssue");
                executionLog.WriteInExcel("Verify Document Displayed Column Issue", Status, JIRA, "Tasks Management");
            }
        }
    }
}