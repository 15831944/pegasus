using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Admin
{
    [TestClass]
    public class CreateDownloadIds : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin")]
        [TestCategory("TS6")]
        [TestCategory("Pegasus_Admin")]
        public void createDownloadIds()
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
            var equipment_DownloadIDHelper = new Equipment_DownloadIDHelper(GetWebDriver());

            // Variable 
            var name = "Test" + GetRandomNumber();
            var Id = "12345" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("CreateDownloadIds", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("CreateDownloadIds", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("CreateDownloadIds", "Click On  Admin");
                VisitOffice("admin");

                executionLog.Log("CreateDownloadIds", "Redirect To Downlaod id page");
                VisitOffice("download_ids");

                executionLog.Log("CreateDownloadIds", "Verify title");
                VerifyTitle("Download IDs");

                executionLog.Log("CreateDownloadIds", " Click On Create");
                equipment_DownloadIDHelper.ClickElement("Create");

                executionLog.Log("CreateDownloadIds", "Verify title");
                VerifyTitle("Manage Master Equipment Type Download Ids");

                executionLog.Log("CreateDownloadIds", "Select DownloadIdsType");
                equipment_DownloadIDHelper.Select("DownloadIdsType", "Terminal");

                executionLog.Log("CreateDownloadIds", "Enter DownloadsIDName");
                equipment_DownloadIDHelper.TypeText("DownloadsIDName", "Download" + GetRandomNumber());

                executionLog.Log("CreateDownloadIds", "Enter DownloadsIDName");
                equipment_DownloadIDHelper.TypeText("DownloadID", "Id" + GetRandomNumber());

                executionLog.Log("CreateDownloadIds", "Select Status");
                equipment_DownloadIDHelper.Select("Status", "0");

                executionLog.Log("CreateDownloadIds", " Click on Save button");
                equipment_DownloadIDHelper.ClickElement("Save");

                executionLog.Log("CreateDownloadIds", "Wait for text");
                equipment_DownloadIDHelper.WaitForText("The download id is successfully created!!", 30);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CreateDownloadIds");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Create DownloadIds");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Create DownloadIds", "Bug", "Medium", "Create DownloadsIDS page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Create DownloadIds");
                        TakeScreenshot("CreateDownloadIds");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateDownloadIds.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CreateDownloadIds");
                        string id = loginHelper.getIssueID("Create DownloadIds");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateDownloadIds.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Create DownloadIds"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Create DownloadIds");
             //   executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("CreateDownloadIds");
                executionLog.WriteInExcel("Create DownloadIds", Status, JIRA, "Equipment Management");
            }
        }
    }
}
