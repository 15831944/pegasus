using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class AdminMerchantTypeBackIssue : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS1")]
        [TestCategory("BugTest")]
        public void adminMerchantTypeBackIssue()
        {
            string[] username = null;
            string[] password = null;

            XMLParse oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var masterData_MerchantTypeHelper = new MasterData_MerchantTypeHelper(GetWebDriver());
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("AdminMerchantTypeBackIssue", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("AdminMerchantTypeBackIssue", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("AdminMerchantTypeBackIssue", "Goto Merchant Type");
                VisitOffice("merchant_types");
                masterData_MerchantTypeHelper.WaitForWorkAround(3000);

                executionLog.Log("AdminMerchantTypeBackIssue", "Click On Merchant");
                masterData_MerchantTypeHelper.ClickElement("ClickOnMerchantAdmin");
                masterData_MerchantTypeHelper.WaitForWorkAround(3000);

                executionLog.Log("AdminMerchantTypeBackIssue", "Click Cancel");
                masterData_MerchantTypeHelper.ClickElement("Cancel");

                executionLog.Log("AdminMerchantTypeBackIssue", "Wait for text");
                masterData_MerchantTypeHelper.WaitForText("Master Merchant Types", 05);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("AdminMerchantTypeBackIssue");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Admin Merchant Type Back Issue");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Admin Merchant Type Back Issue", "Bug", "Medium", "Office Admin", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Admin Merchant Type Back Issue");
                        TakeScreenshot("AdminMerchantTypeBackIssue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AdminMerchantTypeBackIssue.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("AdminMerchantTypeBackIssue");
                        string id = loginHelper.getIssueID("Admin Merchant Type Back Issue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AdminMerchantTypeBackIssue.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Admin Merchant Type Back Issue"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Admin Merchant Type Back Issue");
                //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("AdminMerchantTypeBackIssue");
                executionLog.WriteInExcel("Admin Merchant Type Back Issue", Status, JIRA, "Office Master Data");
            }
        }
    }
}