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
    public class EditDepartmentOffice : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin")]
        [TestCategory("TS6")]
        [TestCategory("Pegasus_Admin")]
        public void editDepartmentOffice()
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
            var office_DepartmentHelper = new Office_DepartmentHelper(GetWebDriver());

            // Variable
            String name = "Test" + GetRandomNumber();
            String email = "Test" + GetRandomNumber() + "@gmail.com";

            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("EditDepartmentOffice", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("EditDepartmentOffice", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("EditDepartmentOffice", "Click On  Admin");
                VisitOffice("admin");

                executionLog.Log("EditDepartmentOffice", "Redirect To URL");
                VisitOffice("departments");

                executionLog.Log("EditDepartmentOffice", "Verify title");
                VerifyTitle("Departments");

                executionLog.Log("EditDepartmentOffice", " Click On Create");
                office_DepartmentHelper.ClickElement("Create");

                executionLog.Log("EditDepartmentOffice", "Wait for text");
                VerifyTitle("Create a Department");

                executionLog.Log("EditDepartmentOffice", "Enter DepartmentName");
                office_DepartmentHelper.TypeText("Name", name);

                executionLog.Log("EditDepartmentOffice", "Enter Description");
                office_DepartmentHelper.TypeText("Descripton", "THIS IS TESTING DESCRIPTION");

                executionLog.Log("EditDepartmentOffice", "cLICK on Save  ");
                office_DepartmentHelper.ClickElement("Save");

                executionLog.Log("EditDepartmentOffice", "Wait for Save message.");
                office_DepartmentHelper.WaitForText("Department has been saved.", 30);

                executionLog.Log("EditDepartmentOffice", "Redirect To Office");
                office_DepartmentHelper.RedirectToPage();
                office_DepartmentHelper.WaitForWorkAround(5000);

                executionLog.Log("EditDepartmentOffice", "Verify title");
                VerifyTitle("Departments");

                executionLog.Log("EditDepartmentOffice", " Search");
                office_DepartmentHelper.TypeText("SearchName", name);
                office_DepartmentHelper.WaitForWorkAround(3000);

                executionLog.Log("EditDepartmentOffice", "Click on  Edit");
                office_DepartmentHelper.ClickElement("Edit");

                executionLog.Log("EditDepartmentOffice", "Verify title");
                VerifyTitle("Edit a Department");

                executionLog.Log("EditDepartmentOffice", "Enter DepartmentName");
                office_DepartmentHelper.TypeText("Name", "Edit Department");

                executionLog.Log("EditDepartmentOffice", "Enter Description");
                office_DepartmentHelper.TypeText("Descripton", "THIS IS TESTING DESCRIPTION");

                executionLog.Log("EditDepartmentOffice", "cLICK on Save ");
                office_DepartmentHelper.ClickElement("Save");

                executionLog.Log("EditDepartmentOffice", "Redirect To Department");
                VisitOffice("departments");

                executionLog.Log("EditDepartmentOffice", "Verify title");
                VerifyTitle("Departments");

                executionLog.Log("EditDepartmentOffice", "Enter Name to search");
                office_DepartmentHelper.TypeText("SearchName", name);
                office_DepartmentHelper.WaitForWorkAround(2000);

                executionLog.Log("EditDepartmentOffice", "cLICK Delete btn  ");
                office_DepartmentHelper.ClickElement("Delete");

                executionLog.Log("EditDepartmentOffice", "Accept alert message. ");
                office_DepartmentHelper.AcceptAlert();

                executionLog.Log("EditDepartmentOffice", "Wait for delete message. ");
                office_DepartmentHelper.WaitForText("Department deleted successfully", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("EditDepartmentOffice");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Edit Department Office");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Edit Department Office", "Bug", "Medium", "Admin Office page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Edit Department Office");
                        TakeScreenshot("EditDepartmentOffice");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditDepartmentOffice.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("EditDepartmentOffice");
                        string id = loginHelper.getIssueID("Edit Department Office");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditDepartmentOffice.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Edit Department Office"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Edit Department Office");
           //     executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("EditDepartmentOffice");
                executionLog.WriteInExcel("Edit Department Office", Status, JIRA, "Office");
            }
        }
    }
}
