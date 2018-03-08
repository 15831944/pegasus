using System;
using System.IO;
using LinqToExcel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class FDNRequirement : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS5")]
        [TestCategory("NewSkinTaskJira")]
        public void fDNRequirement()
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
            var corpOffice_OfficeHelper = new CorpOffice_OfficeHelper(GetWebDriver());


            String Status = "Pass";
            String JIRA = "";

            try
            {
                executionLog.Log("FDNRequirement", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("FDNRequirement", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("FDNRequirement", "Go to Create office page");
                VisitCorp("offices/create");

                executionLog.Log("FDNRequirement", "Verify title");
                VerifyTitle("Create an Office");

                executionLog.Log("FDNRequirement", "Click on 'Save' button");
                corpOffice_OfficeHelper.ClickElement("Save");

                executionLog.Log("FDNRequirement", "Verify FDN field is not mandatory");
                corpOffice_OfficeHelper.verifyElementNotPresent("OfficeFDN");

                executionLog.Log("FDNRequirement", "Logout from the application");
                VisitCorp("logout");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";
                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("FDNRequirement");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("FDN Requirement");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("FDN Requirement", "Bug", "Medium", "Office page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("FDN Requirement");
                        TakeScreenshot("FDNRequirement");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\FDNRequirement.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("FDNRequirement");
                        string id = loginHelper.getIssueID("FDN Requirement");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\FDNRequirement.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("FDN Requirement"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("FDN Requirement");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("FDNRequirement");
                executionLog.WriteInExcel("FDN Requirement", Status, JIRA,"Corp Office");
            }
        }
    }
}