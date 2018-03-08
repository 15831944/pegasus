using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class VerifyAddEmailButtonForOffice : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        public void verifyAddEmailButtonForOffice()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");
            username = oXMLData.getData("settings/Credentials", "username_corp");
            password = oXMLData.getData("settings/Credentials", "password2");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var corpOffice_OfficeHelper = new CorpOffice_OfficeHelper(GetWebDriver());

            // Variable random
            var usernme = "Sysprins" + RandomNumber(44, 777);
            var name = "Test" + RandomNumber(99, 999);
            String JIRA = "";
            String Status = "Pass";

          try
            {

            executionLog.Log("VerifyAddEmailButtonForOffice", "Login with valid username and password");
            Login(username[0], password[0]);

            executionLog.Log("VerifyAddEmailButtonForOffice", "Verify Page title");
            VerifyTitle("Dashboard");

            executionLog.Log("VerifyAddEmailButtonForOffice", "Redirect to office page.");
            VisitCorp("offices");

            executionLog.Log("VerifyAddEmailButtonForOffice", "Click on create button.");
            corpOffice_OfficeHelper.ClickElement("Create");

            executionLog.Log("VerifyAddEmailButtonForOffice", "Verify add email butto present on page.");
            corpOffice_OfficeHelper.IsElementPresent("AddEmail");

        }
        catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyAddEmailButtonForOffice");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Add Email Button For Office");
                if (!result)
                {
                    if (Int16.Parse(counter) < 5)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Add Email Button For Office", "Bug", "Medium", "Corp Office page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Add Email Button For Office");
                        TakeScreenshot("VerifyAddEmailButtonForOffice");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyAddEmailButtonForOffice.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 5)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyAddEmailButtonForOffice");
                        string id = loginHelper.getIssueID("Verify Add Email Button For Office");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyAddEmailButtonForOffice.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Add Email Button For Office"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Add Email Button For Office");
                executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyAddEmailButtonForOffice");
                executionLog.WriteInExcel("Verify Add Email Button For Office", Status, JIRA, "Corp Office");
            }
        }
    }
}