using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Admin
{
    [TestClass]
    public class CreateOfficeTeams : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin")]
        [TestCategory("Fail")]
        [TestCategory("TS6")]
        [TestCategory("Pegasus_Admin")]
        public void createOfficeTeams()
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
            var office_TeamHelper = new Office_TeamHelper(GetWebDriver());

            // Variable
            var name = "Testing" + GetRandomNumber();
            var email = "Test" + GetRandomNumber() + "@gmail.com";
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("CreateOfficeTeams", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("CreateOfficeTeams", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("CreateOfficeTeams", "Redirect To Team");
                VisitOffice("teams");
                office_TeamHelper.WaitForWorkAround(3000);

                executionLog.Log("CreateOfficeTeams", " Click On Create");
                office_TeamHelper.ClickElement("Create");
                office_TeamHelper.WaitForWorkAround(3000);

                //executionLog.Log("CreateOfficeTeams", " Wait for element to be present.");
                //office_TeamHelper.WaitForElementPresent("Name", 10);

                executionLog.Log("CreateOfficeTeams", "Enter Name");
                office_TeamHelper.TypeText("Name", name);

                executionLog.Log("CreateOfficeTeams", "Select Department");
                office_TeamHelper.Selectbytext("Department", "Information Technology");

                executionLog.Log("CreateOfficeTeams", "Click on Save  ");
                office_TeamHelper.ClickElement("Save");
                office_TeamHelper.WaitForWorkAround(2000);

                executionLog.Log("CreateOfficeTeams", "Wait for Confimation");
                office_TeamHelper.VerifyPageText("Team has been saved.");
                office_TeamHelper.WaitForWorkAround(2000);

                executionLog.Log("CreateOfficeTeams", "Enter Name to search");
                office_TeamHelper.TypeText("SearchTeam", name);
                office_TeamHelper.WaitForWorkAround(2000);

                executionLog.Log("CreateOfficeTeams", "Click Delete btn  ");
                office_TeamHelper.ClickElement("DeleteTeam");

                executionLog.Log("CreateOfficeTeams", "Accept alert message. ");
                office_TeamHelper.AcceptAlert();

                executionLog.Log("CreateOfficeTeams", "Wait for delete message. ");
                office_TeamHelper.WaitForText("Team deleted successfully.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                Console.WriteLine("Counter value is    " + counter);
                String Description = executionLog.GetAllTextFile("CreateOfficeTeams");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Create Office Teams");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Create Office Teams", "Bug", "Medium", "Office Team page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Create Office Teams");
                        TakeScreenshot("CreateOfficeTeams");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateOfficeTeams.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CreateOfficeTeams");
                        string id = loginHelper.getIssueID("Create Office Teams");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateOfficeTeams.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Create Office Teams"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Create Office Teams");
            //    executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("CreateOfficeTeams");
                executionLog.WriteInExcel("Create Office Teams", Status, JIRA, "Office Admin");
            }
        }
    }
}