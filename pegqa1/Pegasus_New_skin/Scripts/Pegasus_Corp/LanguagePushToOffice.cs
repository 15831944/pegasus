using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class LanguagePushToOffice : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Corp")]
        [TestCategory("TS7")]
        [TestCategory("Pegasus_Corp")]
        public void languagePushToOffice()
        {
            string[] username = null;
            string[] username1 = null;
            string[] password = null;
            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var corpMasterdata_LanguageHelper = new CorpMasterdata_LanguageHelper(GetWebDriver());
            username = oXMLData.getData("settings/Credentials", "username_corp");
            username1 = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Variable
            var mail = "Test" + RandomNumber(1, 99) + "@yopmail.com";
            var numb = "12345678" + RandomNumber(10, 99);
            var lang = "Test" + RandomNumber(99, 999);

            String JIRA = "";
            String Status = "Pass";

            try
            {
            executionLog.Log("CreateLanguageCorp", "Login with valid credential  Username");
            Login(username[0], password[0]);

            executionLog.Log("CreateLanguageCorp", "Verify Page title");
            VerifyTitle("Dashboard");

            executionLog.Log("CreateLanguageCorp", "Redirect To Language");
            VisitCorp("languages");
            corpMasterdata_LanguageHelper.WaitForWorkAround(3000);

            executionLog.Log("CreateLanguageCorp", "Verify Page title");
            VerifyTitle("Languages");

            executionLog.Log("CreateLanguageCorp", "Click On Create Btn");
            corpMasterdata_LanguageHelper.ClickElement("Create");
            corpMasterdata_LanguageHelper.WaitForWorkAround(2000);

            executionLog.Log("CreateLanguageCorp", "Enter Language Name");
            corpMasterdata_LanguageHelper.TypeText("Language", lang);

            executionLog.Log("CreateLanguageCorp", "Clcik on Master Data");
            corpMasterdata_LanguageHelper.ClickElement("Save");

            executionLog.Log("CreateLanguageCorp", "Verify Text");
            corpMasterdata_LanguageHelper.WaitForText("Language Created Successfully", 10);

            executionLog.Log("LanguagePushToOffice", "Click on Push To Office");
            corpMasterdata_LanguageHelper.ClickElement("PushToOfficeLang");

            executionLog.Log("LanguagePushToOffice", "Click ok To Confirm");
            corpMasterdata_LanguageHelper.AcceptAlert();

            executionLog.Log("LanguagePushToOffice", "Verify Confirmation Languges Successfully Pushed to Offices.");
            corpMasterdata_LanguageHelper.WaitForText("Languges Successfully Pushed to Offices.", 30);

            executionLog.Log("LanguagePushToOffice", " Logout button");
            VisitCorp("logout");
            //corpMasterdata_LanguageHelper.WaitForWorkAround(2000);

            Login(username1[0], password[0]);
            corpMasterdata_LanguageHelper.WaitForWorkAround(3000);

            if (GetWebDriver().Title == "Login")

            {
                Login(username1[0], password[0]);
            }

            VerifyTitle("Dashboard");

            executionLog.Log("LanguagePushToOffice", "Redirect to language");
            VisitOffice("languages");
            corpMasterdata_LanguageHelper.WaitForWorkAround(3000);
            VerifyTitle("Languages");

            executionLog.Log("LanguagePushToOffice", "Search pushed language");
            corpMasterdata_LanguageHelper.TypeText("SearchLanguageOffice", lang);
            corpMasterdata_LanguageHelper.WaitForWorkAround(2000);

            executionLog.Log("LanguagePushToOffice", "Verify");
            corpMasterdata_LanguageHelper.VerifyPageText(lang);

            executionLog.Log("AmexRatesPushToOffice", "Logout button");
            VisitOffice("logout");

            executionLog.Log("AmexRatesPushToOffice", "Login with valid credential");
            Login(username[0], password[0]);

            executionLog.Log("AmexRatesPushToOffice", "Verify Page title");
            VerifyTitle("Dashboard");

            executionLog.Log("CreateLanguageCorp", "Redirect To Language");
            VisitCorp("languages");
            corpMasterdata_LanguageHelper.WaitForWorkAround(3000);

            executionLog.Log("CreateDepartmentOffice", "Enter Name to search");
            corpMasterdata_LanguageHelper.TypeText("SearchLang", lang);
            corpMasterdata_LanguageHelper.WaitForWorkAround(2000);

            executionLog.Log("CreateLanguageCorp", "Click Delete btn ");
            corpMasterdata_LanguageHelper.ClickElement("Delete");

            executionLog.Log("CreateLanguageCorp", "Click Delete btn  ");
            corpMasterdata_LanguageHelper.ClickElement("DeletePopup");
            corpMasterdata_LanguageHelper.WaitForWorkAround(4000);

        }
        catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("LanguagePushToOffice");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Language Push To Office");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Language Push To Office", "Bug", "Medium", "language page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Language Push To Office");
                        TakeScreenshot("LanguagePushToOffice");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\LanguagePushToOffice.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("LanguagePushToOffice");
                        string id = loginHelper.getIssueID("Language Push To Office");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\LanguagePushToOffice.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Language Push To Office"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Language Push To Office");
           //     executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("LanguagePushToOffice");
                executionLog.WriteInExcel("Language Push To Office", Status, JIRA, "Master Data");
            }
        }
    }
}