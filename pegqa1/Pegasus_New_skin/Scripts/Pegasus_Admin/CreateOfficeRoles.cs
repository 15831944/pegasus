using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Admin
{
    [TestClass]
    public class CreateOfficeRoles : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin")]
        [TestCategory("TS6")]
        [TestCategory("Pegasus_Admin")]
        public void createOfficeRoles()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            //Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var office_RolesHelper = new Office_RolesHelper(GetWebDriver());

            // Variable
            var name = "Test" + GetRandomNumber();
            var email = "Test" + GetRandomNumber() + "@gmail.com";
            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("CreateOfficeRoles", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("CreateOfficeRoles", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("CreateOfficeRoles", "Redirect To Roles page");
                VisitOffice("roles");
                office_RolesHelper.WaitForWorkAround(3000);

                executionLog.Log("CreateOfficeRoles", "Verify title");
                VerifyTitle("Roles");

                executionLog.Log("CreateOfficeRoles", "Click On Create");
                office_RolesHelper.ClickElement("Create");
                office_RolesHelper.WaitForWorkAround(3000);

                executionLog.Log("CreateOfficeRoles", "Enter DepartmentName");
                office_RolesHelper.TypeText("Name", name);

                executionLog.Log("CreateOfficeRoles", "Enter Description");
                office_RolesHelper.Selectbytext("Department", "Information Technology");

                executionLog.Log("CreateOfficeRoles", "Click on checkbox Manager");
                office_RolesHelper.ClickElement("Manager");

                executionLog.Log("CreateOfficeRoles", "click on Save  ");
                office_RolesHelper.ClickElement("Save");
                office_RolesHelper.WaitForWorkAround(2000);

                executionLog.Log("CreateOfficeRoles", "Wait for Confirmation");
                office_RolesHelper.WaitForText("Role has been saved.", 10);

                executionLog.Log("CreateOfficeRoles", "Redirect To Roles page");
                VisitOffice("roles");
                office_RolesHelper.WaitForWorkAround(3000);

                executionLog.Log("CreateOfficeRoles", "Verify title");
                VerifyTitle("Roles");

                executionLog.Log("CreateOfficeRoles", "Enter Name to search");
                office_RolesHelper.TypeText("SearchName", name);
                office_RolesHelper.WaitForWorkAround(2000);

                executionLog.Log("CreateOfficeRoles", "Click Delete btn  ");
                office_RolesHelper.ClickElement("DeleteIcon");

                executionLog.Log("CreateOfficeRoles", "Accept alert message. ");
                office_RolesHelper.AcceptAlert();

                executionLog.Log("CreateOfficeRoles", "Wait for delete message. ");
                office_RolesHelper.WaitForText("Role Deleted Successfully.", 10);

            }

            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CreateOfficeRoles");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Create Office Roles");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Create Office Roles", "Bug", "Medium", "Admin Office Role page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Create Office Roles");
                        TakeScreenshot("CreateOfficeRoles");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateOfficeRoles.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CreateOfficeRoles");
                        string id = loginHelper.getIssueID("Create Office Roles");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateOfficeRoles.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Create Office Roles"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Create Office Roles");
            //    executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("CreateOfficeRoles");
                executionLog.WriteInExcel("Create Office Roles", Status, JIRA, "Office Admin");
            }
        }
    }
}
