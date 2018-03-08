using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class EditAndDeleteLanguageCorp : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Corp")]
        [TestCategory("Temp")]
        [TestCategory("TS7")]
        [TestCategory("Pegasus_Corp")]
        public void editAndDeleteLanguageCorp()
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
            var mail = "Test" + RandomNumber(1, 99) + "@yopmail.com";
            var numb = "12345678" + RandomNumber(10, 99);
            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("EditAndDeleteLanguageCorp", "Login with valid credential  Username");
                Login(username[0], password[0]);

                executionLog.Log("EditAndDeleteLanguageCorp", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("EditAndDeleteLanguageCorp", "Redirect To Language");
                VisitCorp("languages");
                corpMasterdata_LanguageHelper.WaitForWorkAround(3000);

                executionLog.Log("EditAndDeleteLanguageCorp", "Verify Page title");
                VerifyTitle("Languages");

                executionLog.Log("EditAndDeleteLanguageCorp", "Click On Create Btn");
                corpMasterdata_LanguageHelper.ClickElement("Create");
                corpMasterdata_LanguageHelper.WaitForWorkAround(1000);

                executionLog.Log("EditAndDeleteLanguageCorp", "Enter Language Name");
                var lang = "Test" + RandomNumber(99, 999);
                corpMasterdata_LanguageHelper.TypeText("Language", lang);

                executionLog.Log("EditAndDeleteLanguageCorp", "Clcik on Save button");
                corpMasterdata_LanguageHelper.ClickElement("Save");

                executionLog.Log("EditAndDeleteLanguageCorp", "Verify Text");
                corpMasterdata_LanguageHelper.WaitForText("Language Created Successfully", 05);
                corpMasterdata_LanguageHelper.WaitForWorkAround(3000);

                executionLog.Log("EditAndDeleteLanguageCorp", "Search Language");
                corpMasterdata_LanguageHelper.TypeText("SearchLang", lang);
                corpMasterdata_LanguageHelper.WaitForWorkAround(2000);

                executionLog.Log("EditAndDeleteLanguageCorp", "Click on Edit language");
                corpMasterdata_LanguageHelper.ClickElement("Edit");

                executionLog.Log("EditAndDeleteLanguageCorp", "Enter Language Name");
                var Elang = "Corp Lang" + RandomNumber(99, 999);
                corpMasterdata_LanguageHelper.TypeText("EditLanguage", Elang);

                executionLog.Log("EditAndDeleteLanguageCorp", "ClickOn Edit Save Button");
                corpMasterdata_LanguageHelper.ClickElement("SaveEditLanguage");
                corpMasterdata_LanguageHelper.WaitForWorkAround(4000);

                executionLog.Log("EditAndDeleteLanguageCorp", "Click on delete button");
                corpMasterdata_LanguageHelper.ClickElement("Delete");

                executionLog.Log("EditAndDeleteLanguageCorp", "Accept delete popup.");
                corpMasterdata_LanguageHelper.ClickElement("DeletePopup");
                corpMasterdata_LanguageHelper.WaitForWorkAround(3000);

                VisitCorp("logout");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("EditAndDeleteLanguageCorp");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Edit And Delete Language Corp");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Edit And Delete Language Corp", "Bug", "Medium", "Language page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Edit And Delete Language Corp");
                        TakeScreenshot("EditAndDeleteLanguageCorp");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditAndDeleteLanguageCorp.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("EditAndDeleteLanguageCorp");
                        string id = loginHelper.getIssueID("Edit And Delete Language Corp");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditAndDeleteLanguageCorp.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Edit And Delete Language Corp"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Edit And Delete Language Corp");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("EditAndDeleteLanguageCorp");
                executionLog.WriteInExcel("Edit And Delete Language Corp", Status, JIRA, "Corp Master Data");
            }
        }
    }
}
