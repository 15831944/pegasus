using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class CorpOfficeSearchWithInvalidPhone : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS2")]
        [TestCategory("BugTestNew")]
        public void corpOfficeSearchWithInvalidPhone()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var corp_ProfileHelper = new Corp_ProfileHelper(GetWebDriver());

            username = oXMLData.getData("settings/Credentials", "username_corp");
            password = oXMLData.getData("settings/Credentials", "password");

            // Variable
            var Num = RandomNumber(1, 9999).ToString();
            var Nam = GetRandomNumber();
            var name = GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("CorpOfficeSearchWithInvalidPhone", "Login with valid credential  Username");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: "+username[0]+" / "+password[0]);

                executionLog.Log("CorpOfficeSearchWithInvalidPhone", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("CorpOfficeSearchWithInvalidPhone", "Enter Invalid phone Number");
                corp_ProfileHelper.TypeText("SearchPhone", "Invalid");

                executionLog.Log("CorpOfficeSearchWithInvalidPhone", "Wait for locator to be present.");
                corp_ProfileHelper.WaitForElementPresent("NoRecordFound", 10);

                executionLog.Log("CorpOfficeSearchWithInvalidPhone", "Verify Page text No records to view");
                corp_ProfileHelper.VerifyText("NoRecordFound", "No records to view");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CorpOfficeSearchWithInvalidPhone");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Corp Office Search With Invalid Phone");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Corp Office Search With Invalid Phone", "Bug", "Medium", "Corp Profile page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Corp Office Search With Invalid Phone");
                        TakeScreenshot("CorpOfficeSearchWithInvalidPhone");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CorpOfficeSearchWithInvalidPhone.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CorpOfficeSearchWithInvalidPhone");
                        string id = loginHelper.getIssueID("Corp Office Search With Invalid Phone");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CorpOfficeSearchWithInvalidPhone.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Corp Office Search With Invalid Phone"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Corp Office Search With Invalid Phone");
                //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("CorpOfficeSearchWithInvalidPhone");
                executionLog.WriteInExcel("Corp Office Search With Invalid Phone", Status, JIRA, "Corp Offices");
            }
        }
    }
}
