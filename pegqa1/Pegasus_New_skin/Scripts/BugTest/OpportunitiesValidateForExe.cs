using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class OpportunitiesValidateForExe : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS1")]
        [TestCategory("BugTest")]
        public void opportunitiesValidateForExe()
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
            var office_OpportunitiesHelper = new Office_OpportunitiesHelper(GetWebDriver());

            // Variable
            var DocName = "Test Exe" + GetRandomNumber();
            var fileUpl = GetPathToFile() + "chrome.exe";
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("OpportunitiesValidateForExe", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("OpportunitiesValidateForExe", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("OpportunitiesValidateForExe", "Redirect To URL");
                VisitOffice("opportunities");
                office_OpportunitiesHelper.WaitForWorkAround(5000);

                executionLog.Log("OpportunitiesValidateForExe", "Click On Task Edit");
                office_OpportunitiesHelper.ClickElement("ClickOnTaskEdit");
                office_OpportunitiesHelper.WaitForWorkAround(1000);

                executionLog.Log("OpportunitiesValidateForExe", "Click On Add Document");
                office_OpportunitiesHelper.ClickElement("ClickOnAddDocument");
                office_OpportunitiesHelper.WaitForWorkAround(1000);

                executionLog.Log("OpportunitiesValidateForExe", "Enter Document Name");
                office_OpportunitiesHelper.TypeText("EnterDocumentName", DocName);

                executionLog.Log("OpportunitiesValidateForExe", "Browse Doc");
                office_OpportunitiesHelper.Upload("BrowseDoc", fileUpl);
                office_OpportunitiesHelper.WaitForWorkAround(3000);

                executionLog.Log("OpportunitiesValidateForExe", "Click Save Of Doc PopUp");
                office_OpportunitiesHelper.ClickDisplayed("//a[text()='Save']");
                office_OpportunitiesHelper.WaitForWorkAround(1000);

                executionLog.Log("OpportunitiesValidateForExe", "Wait for validation mesage.");
                office_OpportunitiesHelper.WaitForText("This field is required.", 10);
                office_OpportunitiesHelper.WaitForWorkAround(3000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("OpportunitiesValidateForExe");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Opportunities Validate For Exe");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Opportunities Validate For Exe", "Bug", "Medium", "Opportunities page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Opportunities Validate For Exe");
                        TakeScreenshot("OpportunitiesValidateForExe");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\OpportunitiesValidateForExe.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("OpportunitiesValidateForExe");
                        string id = loginHelper.getIssueID("Opportunities Validate For Exe");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\OpportunitiesValidateForExe.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Opportunities Validate For Exe"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Opportunities Validate For Exe");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("OpportunitiesValidateForExe");
                executionLog.WriteInExcel("Opportunities Validate For Exe", Status, JIRA, "Opportunities Management");
            }
        }
    }
}