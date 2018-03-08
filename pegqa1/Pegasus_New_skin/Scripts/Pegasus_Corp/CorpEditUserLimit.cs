using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class CorpEditUserLimit : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Corp")]
        [TestCategory("TS7")]
        [TestCategory("Pegasus_Corp")]
        public void corpEditUserLimit()
        {
            string[] username = null;
            string[] password = null;


            XMLParse oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var corpMasterdata_UserLimitsHelper = new CorpMasterdata_UserLimitsHelper(GetWebDriver());
            username = oXMLData.getData("settings/Credentials", "username_corp");
            password = oXMLData.getData("settings/Credentials", "password");

            // Variable
            var name = "TEST COMPANY" + GetRandomNumber();
            var TemplateName = "Test" + GetRandomNumber();
            var LastName = "Tester" + GetRandomNumber();
            var Number = "12345678" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("EditUserLimitCorp", "Login with valid credential  Username");
                Login(username[0], password[0]);
                corpMasterdata_UserLimitsHelper.WaitForWorkAround(3000);

                executionLog.Log("EditUserLimitCorp", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("EditUserLimitCorp", "Redirect To URL");
                VisitCorp("masterdata/users_limit");

                executionLog.Log("EditUserLimitCorp", "Verify title");
                VerifyTitle("User Limits");

                executionLog.Log("EditUserLimitCorp", "Click On Edit User Limit");
                corpMasterdata_UserLimitsHelper.ClickElement("EditUserLimt");

                executionLog.Log("EditUserLimitCorp", "Enter EmployeeLimit");
                corpMasterdata_UserLimitsHelper.TypeText("EmployeeLimit", "15");

                executionLog.Log("EditUserLimitCorp", "Enter sale 1099 agent limit");
                corpMasterdata_UserLimitsHelper.TypeText("Sale1099Agent", "15");

                executionLog.Log("EditUserLimitCorp", "Enter client user limit");
                corpMasterdata_UserLimitsHelper.TypeText("ClientUser", "15");

                executionLog.Log("EditUserLimitCorp", "Enter partner user limit");
                corpMasterdata_UserLimitsHelper.TypeText("PartnerUser", "15");

                executionLog.Log("EditUserLimitCorp", "Click Save");
                corpMasterdata_UserLimitsHelper.ClickElement("Save");
                corpMasterdata_UserLimitsHelper.WaitForWorkAround(3000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("EditUserLimitCorp");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Edit User Limit Corp");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Edit User Limit Corp", "Bug", "Medium", "Employee page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Edit User Limit Corp");
                        TakeScreenshot("EditUserLimitCorp");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditUserLimitCorp.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("EditUserLimitCorp");
                        string id = loginHelper.getIssueID("Edit User Limit Corp");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditUserLimitCorp.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Edit User Limit Corp"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Edit User Limit Corp");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("EditUserLimitCorp");
                executionLog.WriteInExcel("Edit User Limit Corp", Status, JIRA, "Corp Master Data");
            }
        }
    }
}
