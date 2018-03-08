using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class CreateLanguageCorp : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Corp")]
        [TestCategory("TS7")]
        [TestCategory("Pegasus_Corp")]
        public void createLanguageCorp()
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
            var lang = "Eng" + GetRandomNumber();
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

                executionLog.Log("CreateLanguageCorp", "Verify title");
                VerifyTitle("Languages");

                executionLog.Log("CreateLanguageCorp", "Click On Create Btn");
                corpMasterdata_LanguageHelper.ClickElement("Create");
                corpMasterdata_LanguageHelper.WaitForWorkAround(2000);

                executionLog.Log("CreateLanguageCorp", "Enter Language Name");
                corpMasterdata_LanguageHelper.TypeText("Language", lang);

                executionLog.Log("CreateLanguageCorp", "Click o0n savebtn");
                corpMasterdata_LanguageHelper.ClickElement("Save");

                executionLog.Log("CreateLanguageCorp", "Wait for  Text");
                corpMasterdata_LanguageHelper.WaitForText("Language Created Successfully", 10);

                executionLog.Log("CreateDepartmentOffice", "Enter Name to search");
                corpMasterdata_LanguageHelper.TypeText("SearchLang", lang);
                corpMasterdata_LanguageHelper.WaitForWorkAround(2000);

                executionLog.Log("CreateLanguageCorp", "Click Delete btn ");
                corpMasterdata_LanguageHelper.ClickElement("Delete");

                executionLog.Log("CreateLanguageCorp", "Click Delete btn  ");
                corpMasterdata_LanguageHelper.ClickElement("DeletePopup");
                corpMasterdata_LanguageHelper.WaitForWorkAround(3000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CreateLanguageCorp");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Create Language Corp");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Create Language Corp", "Bug", "Medium", "Change Language", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Create Language Corp");
                        TakeScreenshot("CreateLanguageCorp");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateLanguageCorp.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CreateLanguageCorp");
                        string id = loginHelper.getIssueID("Create Language Corp");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateLanguageCorp.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Create Language Corp"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Create Language Corp");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("CreateLanguageCorp");
                executionLog.WriteInExcel("Create Language Corp", Status, JIRA, "Corp Master Data");
            }
        }
    }
}