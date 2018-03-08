using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Admin
{
    [TestClass]
    public class CreateDepartmentOffice : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin")]
        [TestCategory("TS6")]
        [TestCategory("Pegasus_Admin")]
        public void createDepartmentOffice()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var office_DepartmentHelper = new Office_DepartmentHelper(GetWebDriver());

            // Variable
            var name = "Test" + GetRandomNumber();
            var email = "Test" + GetRandomNumber() + "@gmail.com.com";
            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("CreateDepartmentOffice", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("CreateDepartmentOffice", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("CreateDepartmentOffice", "Click On  Admin");
                VisitOffice("admin");

                executionLog.Log("CreateDepartmentOffice", "Redirect To Department");
                VisitOffice("departments");

                executionLog.Log("CreateDepartmentOffice", "Verify title");
                VerifyTitle("Departments");

                executionLog.Log("CreateDepartmentOffice", " Click On Create");
                office_DepartmentHelper.ClickElement("Create");

                executionLog.Log("CreateDepartmentOffice", "Verify title");
                VerifyTitle("Create a Department");

                executionLog.Log("CreateDepartmentOffice", "Enter Department Name");
                office_DepartmentHelper.TypeText("Name", name);

                executionLog.Log("CreateDepartmentOffice", "Enter Description");
                office_DepartmentHelper.TypeText("Descripton", "THIS IS TESTING DESCRIPTION");

                executionLog.Log("CreateDepartmentOffice", "Click on Save  ");
                office_DepartmentHelper.ClickElement("Save");

                executionLog.Log("CreateDepartmentOffice", "Wait for Success message.");
                office_DepartmentHelper.WaitForText("Department has been saved.", 10);

                executionLog.Log("CreateDepartmentOffice", "Redirect To Department");
                VisitOffice("departments");

                executionLog.Log("CreateDepartmentOffice", "Verify title");
                VerifyTitle("Departments");

                executionLog.Log("CreateDepartmentOffice", "Enter Name to search");
                office_DepartmentHelper.TypeText("SearchName", name);
                office_DepartmentHelper.WaitForWorkAround(2000);

                executionLog.Log("CreateDepartmentOffice", "Click Delete btn  ");
                office_DepartmentHelper.ClickElement("Delete");

                executionLog.Log("CreateDepartmentOffice", "Accept alert message. ");
                office_DepartmentHelper.AcceptAlert();

                executionLog.Log("CreateDepartmentOffice", "Wait for delete success message. ");
                office_DepartmentHelper.WaitForText("Department deleted successfully", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CreateDepartmentOffice");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Create Department Office");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Create Department Office", "Bug", "Medium", "Departments page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Create Department Office");
                        TakeScreenshot("CreateDepartmentOffice");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateDepartmentOffice.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CreateDepartmentOffice");
                        string id = loginHelper.getIssueID("Create Department Office");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateDepartmentOffice.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Create Department Office"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Create Department Office");
           //     executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("CreateDepartmentOffice");
                executionLog.WriteInExcel("Create Department Office", Status, JIRA,"Office Admin");
            }
        }
    }
}
    