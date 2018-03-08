using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class CreateLanguage : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin")]
        [TestCategory("Temp")]
        [TestCategory("TS6")]
        [TestCategory("Pegasus_Admin")]
        public void createLanguage()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            //Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var masterData_LanguageHelper = new MasterData_LanguageHelper(GetWebDriver());

            //Variable
            var lang = "AB_Lang" + RandomNumber(99, 999999);
            var Elang = "AAB" + RandomNumber(666, 99999);
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("CreateLanguage", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("CreateLanguage", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("CreateLanguage", "Redirect To Language");
                VisitOffice("languages");

                executionLog.Log("CreateLanguage", "Verify title");
                VerifyTitle("Languages");

                executionLog.Log("CreateLanguage", "Click On Create Btn");
                masterData_LanguageHelper.ClickElement("Create");
                masterData_LanguageHelper.WaitForWorkAround(2000);

                executionLog.Log("CreateLanguage", "Enter Language Name");
                masterData_LanguageHelper.TypeText("Name", lang);
                masterData_LanguageHelper.WaitForWorkAround(2000);

                executionLog.Log("CreateLanguage", "Clcik on Save");
                masterData_LanguageHelper.ClickElement("Save");
                masterData_LanguageHelper.WaitForWorkAround(2000);

                executionLog.Log("CreateLanguage", "Clcik on Edit language");
                masterData_LanguageHelper.ClickElement("Edit");
                masterData_LanguageHelper.WaitForWorkAround(2000);

                executionLog.Log("CreateLanguage", "Enter Language Name");
                masterData_LanguageHelper.TypeText("Language", Elang);
                masterData_LanguageHelper.WaitForWorkAround(2000);

                executionLog.Log("CreateLanguage", "ClickOn Edit Save Button");
                masterData_LanguageHelper.ClickElement("SaveEdit");
                masterData_LanguageHelper.WaitForWorkAround(2000);

                executionLog.Log("CreateLanguage", "Wait for locator to bepresent.");
                masterData_LanguageHelper.WaitForElementPresent("Delete", 10);

                executionLog.Log("CreateLanguage", "Click On Del Lang");
                masterData_LanguageHelper.ClickForce("Delete");
                masterData_LanguageHelper.WaitForWorkAround(2000);

                executionLog.Log("CreateLanguage", "Click On Del Lang prompt message");
                masterData_LanguageHelper.ClickForce("DeletePrompt");
                masterData_LanguageHelper.WaitForWorkAround(2000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CreateLanguage");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Create Language");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Create Language", "Bug", "Medium", "Create language page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Create Language");
                        TakeScreenshot("CreateLanguage");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateLanguage.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CreateLanguage");
                        string id = loginHelper.getIssueID("Create Language");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateLanguage.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Create Language"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Create Language");
              //  executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("CreateLanguage");
                executionLog.WriteInExcel("Create Language", Status, JIRA, "Language Management");
            }
        }
    }
}