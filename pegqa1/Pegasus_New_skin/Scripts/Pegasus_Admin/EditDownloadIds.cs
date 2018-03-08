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
    public class EditDownloadIds : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin")]
        [TestCategory("TS6")]
        [TestCategory("Pegasus_Admin")]
        public void editDownloadIds()
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
            var equipment_DownloadIDHelper = new Equipment_DownloadIDHelper(GetWebDriver());

            // Variable 
            var name = "Test" + GetRandomNumber();
            var idName = "4" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";


            try
            {
                executionLog.Log("EditDownloadIds", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("EditDownloadIds", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("EditDownloadIds", "Click On  Admin");
                VisitOffice("admin");

                executionLog.Log("EditDownloadIds", "Redirect To URL");
                VisitOffice("download_ids");

                executionLog.Log("EditDownloadIds", "Verify title");
                VerifyTitle("Download IDs");

                executionLog.Log("EditDownloadIds", " Click On Create");
                equipment_DownloadIDHelper.ClickElement("Create");

                executionLog.Log("EditDownloadIds", "Verify title");
                VerifyTitle("Manage Master Equipment Type Download Ids");

                executionLog.Log("EditDownloadIds", "Enter DownloadIdsType");
                equipment_DownloadIDHelper.Select("DownloadIdsType", "Terminal");

                executionLog.Log("EditDownloadIds", "Enter DownloadsIDName");
                equipment_DownloadIDHelper.TypeText("DownloadsIDName", name);

                executionLog.Log("EditDownloadIds", "Enter DownloadsIDName");
                equipment_DownloadIDHelper.TypeText("DownloadID", idName);

                executionLog.Log("EditDownloadIds", "Enter Category");
                equipment_DownloadIDHelper.Select("Status", "0");

                executionLog.Log("EditDownloadIds", " Click on Save button   ");
                equipment_DownloadIDHelper.ClickElement("Save");

                executionLog.Log("EditDownloadIds", "Wait for save text");
                equipment_DownloadIDHelper.WaitForText("The download id is successfully created!!", 10);

                executionLog.Log("EditDownloadIds", "Verify title");
                VerifyTitle("Download IDs");

                executionLog.Log("EditDownloadIds", "Enter Id to search");
                equipment_DownloadIDHelper.TypeText("SearchDownloadIds", idName);

                executionLog.Log("EditDownloadIds", "Click on Edit button");
                equipment_DownloadIDHelper.ClickElement("Edit");

                executionLog.Log("EditDownloadIds", "Verify title");
                VerifyTitle("Manage Master Equipment Type Download Ids");

                executionLog.Log("EditDownloadIds", "Select DownloadIdsType");
                equipment_DownloadIDHelper.Select("DownloadIdsType", "Terminal");

                executionLog.Log("EditDownloadIds", "Enter DownloadsIDName");
                equipment_DownloadIDHelper.TypeText("DownloadsIDName", name + "1");

                executionLog.Log("EditDownloadIds", "Enter DownloadsIDName");
                equipment_DownloadIDHelper.TypeText("DownloadID", idName);

                executionLog.Log("EditDownloadIds", "Select Status");
                equipment_DownloadIDHelper.Select("Status", "0");

                executionLog.Log("EditDownloadIds", " Click on Save button ");
                equipment_DownloadIDHelper.ClickElement("Save");

                executionLog.Log("EditDownloadIds", "Wait for text");
                equipment_DownloadIDHelper.WaitForText("The download id is successfully updated!!", 30);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("EditDownloadIds");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Edit DownloadIds");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Edit DownloadIds", "Bug", "Medium", "Downloads page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Edit DownloadIds");
                        TakeScreenshot("EditDownloadIds");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditDownloadIds.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("EditDownloadIds");
                        string id = loginHelper.getIssueID("Edit DownloadIds");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditDownloadIds.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Edit DownloadIds"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Edit DownloadIds");
            //    executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("EditDownloadIds");
                executionLog.WriteInExcel("Edit DownloadIds", Status, JIRA, "Equipment Management");
            }
        }
    }
}
