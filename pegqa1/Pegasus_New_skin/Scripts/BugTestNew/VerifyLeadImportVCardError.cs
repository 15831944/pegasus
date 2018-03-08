using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class VerifyLeadImportVCardError : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin")]
        [TestCategory("TS2")]
        [TestCategory("BugTestNew")]
        public void verifyLeadImportVCardError()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var office_LeadsHelper = new Office_LeadsHelper(GetWebDriver());
            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Variable
            var FName = "Test" + RandomNumber(99, 99999);
            var LName = "Test" + RandomNumber(99, 99999);
            var CDBA = "New" + RandomNumber(99, 99999);
            var doc = "Docname" + RandomNumber(99,9999);
            var file = GetPathToFile() + "Winny.vcf";
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("VerifyLeadImportVCardError", "Login with valid credentials");
                Login(username[0], password[0]);

                executionLog.Log("VerifyLeadImportVCardError", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyLeadImportVCardError", "Redirect at Create Lead");
                VisitOffice("leads/importVcard");
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyLeadImportVCardError", "Upload vCard file");
                office_LeadsHelper.Upload("BrowsevCardfile", file);
                office_LeadsHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyLeadImportVCardError", "Click Import button");
                office_LeadsHelper.ClickForce("ImportvCardBtn");
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyLeadImportVCardError", "Verify 500 Error not occurred");
                Assert.IsFalse(GetWebDriver().PageSource.Contains("Internal Server Error"));

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyLeadImportVCardError");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Lead Import VCard Error");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Lead Import VCard Error", "Bug", "Medium", "Create Office page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Lead Import VCard Error");
                        TakeScreenshot("VerifyLeadImportVCardError");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyLeadImportVCardError.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyLeadImportVCardError");
                        string id = loginHelper.getIssueID("Verify Lead Import VCard Error");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyLeadImportVCardError.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Lead Import VCard Error"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Lead Import VCard Error");
                //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyLeadImportVCardError");
                executionLog.WriteInExcel("Verify Lead Import VCard Error", Status, JIRA, "Corp Office");
            }
        }
    }
}