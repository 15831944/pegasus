using System;
using System.IO;
using LinqToExcel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class CreateSalesAgentIssue : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS4")]
        [TestCategory("NewSkinTaskJira")]
        public void createSalesAgentIssue()
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
            var office_UserHelper = new Office_UserHelper(GetWebDriver());

            // Variables.
            var name = "User" + RandomNumber(1, 9999);
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("CreateSalesAgentIssue", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("CreateSalesAgentIssue", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("CreateSalesAgentIssue", "Go to Create user page");
                VisitOffice("users/create");
                office_UserHelper.WaitForWorkAround(5000);

                executionLog.Log("CreateSalesAgentIssue", "Verify title");
                VerifyTitle("Create User");

                executionLog.Log("CreateSalesAgentIssue", "Select parnter association as type");
                office_UserHelper.SelectByText("Usertype", "1099 Sales Agent");

                executionLog.Log("CreateSalesAgentIssue", "Click Create new");
                office_UserHelper.ClickElement("CreateNew");
                office_UserHelper.WaitForWorkAround(3000);

                executionLog.Log("CreateSalesAgentIssue", "Click on Auto generte checkbox");
                office_UserHelper.ClickElement("UserAutoGenerate");
                office_UserHelper.WaitForWorkAround(2000);

                executionLog.Log("CreateSalesAgentIssue", "Enter Username");
                office_UserHelper.TypeText("UserName", name);

                executionLog.Log("CreateSalesAgentIssue", "Enter password");
                office_UserHelper.TypeText("UserPassword", "123456789");

                executionLog.Log("CreateSalesAgentIssue", "Enter First name");
                office_UserHelper.TypeText("FirstName", name + " First");

                executionLog.Log("CreateSalesAgentIssue", "Enter Last name");
                office_UserHelper.TypeText("LastName", name + " Last");

                executionLog.Log("CreateSalesAgentIssue", "Enter email");
                office_UserHelper.TypeText("PrimaryEmail", name + "@yopmail.com");

                executionLog.Log("CreateSalesAgentIssue", "Click on Avatar");
                office_UserHelper.ClickElement("AvtatSalesAgent");

                executionLog.Log("CreateSalesAgentIssue", "Click on Save button");
                office_UserHelper.ClickElement("Save1099");

                executionLog.Log("CreateSalesAgentIssue", "Verify Save button working");
                office_UserHelper.WaitForText("The user is successfully added", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CreateSalesAgentIssue");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Create Sales Agent Issue");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Create Sales Agent Issue", "Bug", "Medium", "User page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Create Sales Agent Issue");
                        TakeScreenshot("CreateSalesAgentIssue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateSalesAgentIssue.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CreateSalesAgentIssue");
                        string id = loginHelper.getIssueID("Create Sales Agent Issue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateSalesAgentIssue.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Create Sales Agent Issue"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Create Sales Agent Issue");
         //       executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("CreateSalesAgentIssue");
                executionLog.WriteInExcel("Create Sales Agent Issue", Status, JIRA, "Agent Portal");
            }
        }
    }
}