using System;
using System.IO;
using LinqToExcel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class LanguageNameValidation : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS5")]
        [TestCategory("NewSkinTaskJira")]
        public void languageNameValidation()
        {
            string[] username = null;
            string[] password = null;
            string[] log = null;
            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_corp");
            password = oXMLData.getData("settings/Credentials", "password");
            log = oXMLData.getData("settings/URL", "logout");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var corpMasterdata_LanguageHelper = new CorpMasterdata_LanguageHelper(GetWebDriver());

            String JIRA = "";
            String Status = "Pass";

            var Lang = "Eng" + GetRandomNumber();

            try
            {
                executionLog.Log("LanguageNameValidation", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("LanguageNameValidation", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("LanguageNameValidation", "Go to Language page");
                VisitCorp("languages");

                executionLog.Log("LanguageNameValidation", "Verify title");
                VerifyTitle("Languages");

                executionLog.Log("LanguageNameValidation", "Click on 'Create' button");
                corpMasterdata_LanguageHelper.ClickForce("Create");
                corpMasterdata_LanguageHelper.WaitForWorkAround(2000);

                executionLog.Log("LanguageNameValidation", "Wait for text");
                corpMasterdata_LanguageHelper.WaitForText("Add New Language", 10);
                corpMasterdata_LanguageHelper.WaitForWorkAround(4000);

                executionLog.Log("LanguageNameValidation", "Enter language name");
                corpMasterdata_LanguageHelper.TypeText("Language", Lang);
                corpMasterdata_LanguageHelper.WaitForWorkAround(4000);

                executionLog.Log("LanguageNameValidation", "Click on Save button");
                corpMasterdata_LanguageHelper.ClickForce("Save");

                executionLog.Log("LanguageNameValidation", "Wait for text");
                corpMasterdata_LanguageHelper.WaitForText("Language Created Successfully", 10);

                executionLog.Log("LanguageNameValidation", "Click on Edit button");
                corpMasterdata_LanguageHelper.ClickForce("Edit");

                executionLog.Log("LanguageNameValidation", "Remove text from the field");
                corpMasterdata_LanguageHelper.removeText("LanguageBlank");

                executionLog.Log("LanguageNameValidation", "Click on Check box");
                corpMasterdata_LanguageHelper.Click("//div[@class='ibox-title']");

                executionLog.Log("LanguageNameValidation", "Click on Save");
                corpMasterdata_LanguageHelper.ClickForce("LanguageSave1");

                executionLog.Log("LanguageNameValidation", "Verify error message");
                corpMasterdata_LanguageHelper.VerifyPageText("Name: Field is required");

                executionLog.Log("LanguageNameValidation", "Click on Close button");
                corpMasterdata_LanguageHelper.ClickForce("LanguageClose");

                executionLog.Log("LanguageNameValidation", "Click on Cancel button");
                corpMasterdata_LanguageHelper.ClickForce("LanguageCancel");

                executionLog.Log("LanguageNameValidation", "Click on Delete button");
                corpMasterdata_LanguageHelper.ClickForce("Delete");

                executionLog.Log("LanguageNameValidation", "Click on OK button");
                corpMasterdata_LanguageHelper.ClickForce("LanguageOK");

                executionLog.Log("LanguageNameValidation", "Logout from the application");
                VisitCorp("logout");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("LanguageNameValidation");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Language Name Validation");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Language Name Validation", "Bug", "Medium", "Corp Language page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Language Name Validation");
                        TakeScreenshot("LanguageNameValidation");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\LanguageNameValidation.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("LanguageNameValidation");
                        string id = loginHelper.getIssueID("Language Name Validation");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\LanguageNameValidation.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Language Name Validation"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Language Name Validation");
           //     executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("LanguageNameValidation");
                executionLog.WriteInExcel("Language Name Validation", Status, JIRA, "Corp Master Data");
            }
        }
    }
}