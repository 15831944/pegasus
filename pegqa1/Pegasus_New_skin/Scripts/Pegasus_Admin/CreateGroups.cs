using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Admin
{
    [TestClass]
    public class CreateGroups : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin")]
        [TestCategory("TS6")]
        [TestCategory("Pegasus_Admin")]
        public void createGroups()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            //Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver()); ;
            var office_GroupHelper = new Office_GroupHelper(GetWebDriver());

            // Variable
            var name = "Test" + GetRandomNumber();
            var email = "Test" + GetRandomNumber() + "@gmail.com";
            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("CreateGroups", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("CreateGroups", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("CreateGroups", "Click On  Admin");
                VisitOffice("admin");

                executionLog.Log("CreateGroups", "Redirect To Group page");
                VisitOffice("groups");

                executionLog.Log("CreateGroups", "Verify title");
                VerifyTitle("Groups");

                executionLog.Log("CreateGroups", " Click On Create");
                office_GroupHelper.ClickElement("Create");

                executionLog.Log("CreateGroups", "Verify title");
                VerifyTitle("Create a Group");

                executionLog.Log("CreateGroups", "Enter Group Name");
                office_GroupHelper.TypeText("Name", name);

                executionLog.Log("CreateGroups", "Enter Description");
                office_GroupHelper.TypeText("Description", "THIS IS TEST DESCRIPTION");

                executionLog.Log("CreateGroups", "cLICK on Save ");
                office_GroupHelper.ClickElement("Save");

                executionLog.Log("CreateGroups", "Wait for text");
                office_GroupHelper.WaitForText("Group has been saved.", 10);

                executionLog.Log("CreateGroups", "Redirect To Group page");
                VisitOffice("groups");

                executionLog.Log("CreateGroups", "Verify title");
                VerifyTitle("Groups");

                executionLog.Log("CreateGroups", "Enter Name to search");
                office_GroupHelper.TypeText("SearchName", name);
                office_GroupHelper.WaitForWorkAround(2000);

                executionLog.Log("CreateGroups", "Click Delete btn  ");
                office_GroupHelper.ClickElement("DeleteIcon");

                executionLog.Log("CreateGroups", "Accept alert message. ");
                office_GroupHelper.AcceptAlert();

                executionLog.Log("CreateGroups", "Wait for delete message.");
                office_GroupHelper.WaitForText("Group Deleted Successfully.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CreateGroups");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Create Groups");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Create Groups", "Bug", "Medium", "Create Group page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Create Groups");
                        TakeScreenshot("CreateGroups");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateGroups.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CreateGroups");
                        string id = loginHelper.getIssueID("Create Groups");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateGroups.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Create Groups"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Create Groups");
              //  executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("CreateGroups");
                executionLog.WriteInExcel("Create Groups", Status, JIRA, "Office Admin");
            }
        }
    }
}
