using System;
using System.IO;
using LinqToExcel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class FieldOrderIssue : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS5")]
        [TestCategory("NewSkinTaskJira")]
        public void fieldOrderIssue()
        {
            string[] username = null;
            string[] password = null;
            string[] log = null;
            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");
            log = oXMLData.getData("settings/URL", "logout");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var office_FieldDictionary_FieldsHelper = new Office_FieldDictionary_FieldsHelper(GetWebDriver());


            String Status = "Pass";
            String JIRA = "";

            try
            {
                executionLog.Log("FieldOrderIssue", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("FieldOrderIssue", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("FieldOrderIssue", "Go to Field Order page");
                VisitOffice("field_order_management");

                executionLog.Log("FieldOrderIssue", "Verify Title");
                VerifyTitle("Field Order Management");

                executionLog.Log("FieldOrderIssue", "Select Module");
                office_FieldDictionary_FieldsHelper.SelectByText("FSModule", "Clients");
                office_FieldDictionary_FieldsHelper.WaitForWorkAround(2000);

                executionLog.Log("FieldOrderIssue", "Select First Data Omaha Processor");
                office_FieldDictionary_FieldsHelper.SelectByText("SearchProcessor", "First Data Omaha");
                office_FieldDictionary_FieldsHelper.WaitForWorkAround(2000);

                executionLog.Log("FieldOrderIssue", "Select Tab");
                office_FieldDictionary_FieldsHelper.SelectByText("FSTab", "Company Details");
                office_FieldDictionary_FieldsHelper.WaitForWorkAround(3000);

                executionLog.Log("FieldOrderIssue", "Select Section");
                office_FieldDictionary_FieldsHelper.SelectByText("FSSection", "Company Address");
                office_FieldDictionary_FieldsHelper.WaitForWorkAround(3000);

                executionLog.Log("FieldOrderIssue", "Click on Search Button");
                office_FieldDictionary_FieldsHelper.ClickElement("FSSearch");

                executionLog.Log("FieldOrderIssue", "Verify Postal Code Last is not available");
                office_FieldDictionary_FieldsHelper.VerifyTextNotPresent("Postal Code Last");

                executionLog.Log("FieldOrderIssue", "Log out from the application");
                VisitOffice("logout");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";
                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("FieldOrderIssue");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Field Order Issue");
                if (!result)
                {

                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Field Order Issue", "Bug", "Medium", "Field Order page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Field Order Issue");
                        TakeScreenshot("FieldOrderIssue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\FieldOrderIssue.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("FieldOrderIssue");
                        string id = loginHelper.getIssueID("Field Order Issue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\FieldOrderIssue.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Field Order Issue"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Field Order Issue");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("FieldOrderIssue");
                executionLog.WriteInExcel("Field Order Issue", Status, JIRA, "Field Management");
            }
        }
    }
}