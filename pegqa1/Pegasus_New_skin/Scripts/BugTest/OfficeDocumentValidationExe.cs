using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class OfficeDocumentValidationExe : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS1")]
        [TestCategory("BugTest")]
        public void officeDocumentValidationExe()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_corp");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var corp_Office_OfficeHelper = new CorpOffice_OfficeHelper(GetWebDriver());

            // Variable
            var DocName = "Test Exe" + GetRandomNumber();
            var fileUpl = GetPathToFile() + "chrome.exe";
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("OfficeDocumentValidationExe", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("OfficeDocumentValidationExe", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("OfficeDocumentValidationExe", "Redirect To URL");
                VisitCorp("offices");
                corp_Office_OfficeHelper.WaitForWorkAround(1000);

                executionLog.Log("OfficeDocumentValidationExe", "Click On Any Corp");
                corp_Office_OfficeHelper.ClickElement("ClickOnAnyCorp");
                corp_Office_OfficeHelper.WaitForWorkAround(1000);

                executionLog.Log("OfficeDocumentValidationExe", "Click On Add Document");
                corp_Office_OfficeHelper.ClickElement("AddDocumentOff");
                corp_Office_OfficeHelper.WaitForWorkAround(1000);

                executionLog.Log("OfficeDocumentValidationExe", "Enter Document Name");
                corp_Office_OfficeHelper.TypeText("NameDocumentOff", DocName);
                corp_Office_OfficeHelper.WaitForWorkAround(1000);

                executionLog.Log("OfficeDocumentValidationExe", "Browse Doc");
                corp_Office_OfficeHelper.Upload("BrowseDoc", fileUpl);
                corp_Office_OfficeHelper.WaitForWorkAround(3000);

                executionLog.Log("OfficeDocumentValidationExe", "Click Save Of Doc PopUp");
                corp_Office_OfficeHelper.ClickDisplayed("//a[@title='Save']");
                corp_Office_OfficeHelper.WaitForWorkAround(1000);

                corp_Office_OfficeHelper.WaitForText("This field is required.", 10);
                corp_Office_OfficeHelper.WaitForWorkAround(3000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("OfficeDocumentValidationExe");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Office Document Validation Exe");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Office Document Validation Exe", "Bug", "Medium", "Office Activities", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Office Document Validation Exe");
                        TakeScreenshot("OfficeDocumentValidationExe");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\OfficeDocumentValidationExe.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("OfficeDocumentValidationExe");
                        string id = loginHelper.getIssueID("Office Document Validation Exe");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\OfficeDocumentValidationExe.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Office Document Validation Exe"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Office Document Validation Exe");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("OfficeDocumentValidationExe");
                executionLog.WriteInExcel("Office Document Validation Exe", Status, JIRA, "Corp Offices");
            }
        }
    }
}