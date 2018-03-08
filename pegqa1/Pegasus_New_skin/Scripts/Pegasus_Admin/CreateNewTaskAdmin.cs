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
    public class CreateNewTaskAdmin : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin")]
        [TestCategory("Fail")]
        [TestCategory("TS6")]
        [TestCategory("Pegasus_Admin")]
        public void createNewTaskAdmin()
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
            var officeAdmin_CorporateHelper = new OfficeAdmin_CorporateHelper(GetWebDriver());

            // Variable
            var name = "Testing Subject" + GetRandomNumber();
            var email = "Test" + GetRandomNumber() + "@gmail.com";
            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("CreateNewTaskAdmin", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("CreateNewTaskAdmin", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("CreateNewTaskAdmin", "Click On  Admin");
                VisitOffice("admin");

                executionLog.Log("CreateNewTaskAdmin", "Redirect to mycorp dtails page.");
                VisitOffice("mycorp");
                officeAdmin_CorporateHelper.WaitForWorkAround(5000);

                executionLog.Log("CreateNewTaskAdmin", " Click On Create");
                officeAdmin_CorporateHelper.ClickElement("NewTask");
                officeAdmin_CorporateHelper.WaitForWorkAround(5000);

                executionLog.Log("CreateNewTaskAdmin", "Enter Subject");
                officeAdmin_CorporateHelper.TypeText("Subject", name);

                executionLog.Log("CreateNewTaskAdmin", "Select Priority");
                officeAdmin_CorporateHelper.Select("Priority", "Low");

                executionLog.Log("CreateNewTaskAdmin", "Select Department");
                officeAdmin_CorporateHelper.TypeText("Description", "This is testing description notes");

                executionLog.Log("CreateNewTaskAdmin", "Enter date");
                officeAdmin_CorporateHelper.TypeText("StartDate", "2017-01-10");

                executionLog.Log("CreateNewTaskAdmin", "Due date");
                officeAdmin_CorporateHelper.TypeText("DueDate", "2017-01-25");

                executionLog.Log("CreateNewTaskAdmin", "cLICK on Save");
                officeAdmin_CorporateHelper.ClickJS("Save");
                officeAdmin_CorporateHelper.WaitForWorkAround(3000);

                executionLog.Log("CreateNewTaskAdmin", "verify text present");
                officeAdmin_CorporateHelper.WaitForText("Task Created Successfully.", 10);

            }
            catch (Exception e)
            {

                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CreateNewTaskAdmin");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Create New Task Admin");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Create New Task Admin", "Bug", "Medium", "Crate New admin task page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Create New Task Admin");
                        TakeScreenshot("CreateNewTaskAdmin");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateNewTaskAdmin.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CreateNewTaskAdmin");
                        string id = loginHelper.getIssueID("Create New Task Admin");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateNewTaskAdmin.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Create New Task Admin"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Create New Task Admin");
              //  executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("CreateNewTaskAdmin");
                executionLog.WriteInExcel("Create New Task Admin", Status, JIRA, "Office Activities");
            }
        }
    }
}