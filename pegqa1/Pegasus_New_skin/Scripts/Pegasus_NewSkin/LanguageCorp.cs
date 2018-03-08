using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class LanguageCorp : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin")]
        [TestCategory("TS8")]
        [TestCategory("Pegasus_NewSkin")]
        public void languageCorp()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var corpMasterdata_LanguageHelper = new CorpMasterdata_LanguageHelper(GetWebDriver());
            username = oXMLData.getData("settings/Credentials", "username_corp");
            password = oXMLData.getData("settings/Credentials", "password");

            // Variable
            var Elang = "Corp Lang" + GetRandomNumber();
            var lang = "Test" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";


            try
            {

                executionLog.Log("LanguageCorp", "Login with valid credential  Username");
                Login(username[0], password[0]);
                corpMasterdata_LanguageHelper.WaitForWorkAround(3000);

                executionLog.Log("LanguageCorp", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("LanguageCorp", "Redirect To Language");
                VisitCorp("languages");

                executionLog.Log("LanguageCorp", "Verify Page title");
                VerifyTitle("Languages");

                executionLog.Log("LanguageCorp", "Click On Create Btn");
                corpMasterdata_LanguageHelper.ClickElement("Create");
                corpMasterdata_LanguageHelper.WaitForWorkAround(3000);

                executionLog.Log("LanguageCorp", "Enter Language Name");
                corpMasterdata_LanguageHelper.TypeText("Language", lang);

                executionLog.Log("LanguageCorp", "Clcik on Save button");
                corpMasterdata_LanguageHelper.ClickElement("Save");

                executionLog.Log("LanguageCorp", "Wait for confirmation.");
                corpMasterdata_LanguageHelper.WaitForText("Language Created Successfully", 10);
                corpMasterdata_LanguageHelper.WaitForWorkAround(3000);

                executionLog.Log("LanguageCorp", "Search Language");
                corpMasterdata_LanguageHelper.TypeText("SearchLang", lang);
                corpMasterdata_LanguageHelper.WaitForWorkAround(3000);

                executionLog.Log("LanguageCorp", "Click on Edit language");
                corpMasterdata_LanguageHelper.ClickElement("Edit");

                executionLog.Log("LanguageCorp", "Enter Language Name");
                corpMasterdata_LanguageHelper.TypeText("EditLanguage", Elang);

                executionLog.Log("LanguageCorp", "Click On Save Button");
                corpMasterdata_LanguageHelper.ClickElement("SaveEditLanguage");
                corpMasterdata_LanguageHelper.WaitForWorkAround(3000);

                executionLog.Log("LanguageCorp", "Click On Del Lang");
                corpMasterdata_LanguageHelper.ClickElement("Delete");

                executionLog.Log("LanguageCorp", "Click On Delete popup.");
                corpMasterdata_LanguageHelper.ClickElement("DeletePopup");
                corpMasterdata_LanguageHelper.WaitForWorkAround(3000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("LanguageCorp");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Language Corp");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Language Corp", "Bug", "Medium", "Language page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Language Corp");
                        TakeScreenshot("LanguageCorp");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\LanguageCorp.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("LanguageCorp");
                        string id = loginHelper.getIssueID("Language Corp");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\LanguageCorp.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Language Corp"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Language Corp");
           //     executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("LanguageCorp");
                executionLog.WriteInExcel("Language Corp", Status, JIRA, "Master Data");
            }
        }
    }
}