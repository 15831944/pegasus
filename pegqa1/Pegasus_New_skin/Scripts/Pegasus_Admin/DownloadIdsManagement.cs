using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Admin
{
    [TestClass]
    public class DownloadIdsManagement : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin")]
        [TestCategory("Fail")]
        [TestCategory("TS6")]
        [TestCategory("Pegasus_Admin")]
        public void downloadIdsManagement()
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
            var office_ClientsHelper = new Office_ClientsHelper(GetWebDriver());

            // Variable 
            var Name = "Download" + RandomNumber(1, 999);
            var name = "UpdateDown" + RandomNumber(33, 9999);
            var Id = "12" + RandomNumber(1, 999);
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("DownloadIdsManagement", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("DownloadIdsManagement", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("DownloadIdsManagement", "Redirect To Downlaod id page");
                VisitOffice("download_ids");
                equipment_DownloadIDHelper.WaitForWorkAround(3000);

                executionLog.Log("DownloadIdsManagement", "Verify title");
                VerifyTitle("Download IDs");

                executionLog.Log("DownloadIdsManagement", " Click On Create");
                equipment_DownloadIDHelper.ClickElement("Create");
                equipment_DownloadIDHelper.WaitForWorkAround(3000);

                executionLog.Log("DownloadIdsManagement", "Verify title");
                VerifyTitle("Manage Master Equipment Type Download Ids");

                executionLog.Log("DownloadIdsManagement", " Click on Save button");
                equipment_DownloadIDHelper.ClickElement("Save");
                //equipment_DownloadIDHelper.WaitForWorkAround(5000);

                executionLog.Log("DownloadIdsManagement", " Verify error for type");
                equipment_DownloadIDHelper.VerifyText("TypeError", "This field is required.");

                executionLog.Log("DownloadIdsManagement", " Verify validation for name");
                equipment_DownloadIDHelper.VerifyText("NameError", "This field is required");

                executionLog.Log("DownloadIdsManagement", "Verify validation for ID");
                equipment_DownloadIDHelper.VerifyText("IDError", "This field is required.");

                executionLog.Log("DownloadIdsManagement", "Select DownloadIdsType");
                equipment_DownloadIDHelper.Select("DownloadIdsType", "Terminal");

                executionLog.Log("DownloadIdsManagement", "Enter DownloadsIDName");
                equipment_DownloadIDHelper.TypeText("DownloadsIDName", Name);

                executionLog.Log("DownloadIdsManagement", "Enter DownloadsID");
                equipment_DownloadIDHelper.TypeText("DownloadID", Id);

                executionLog.Log("DownloadIdsManagement", "Select Status");
                equipment_DownloadIDHelper.Select("Status", "0");

                executionLog.Log("DownloadIdsManagement", " Click on Save button");
                equipment_DownloadIDHelper.ClickElement("Save");
                equipment_DownloadIDHelper.WaitForWorkAround(3000);

                executionLog.Log("DownloadIdsManagement", "Wait for text");
                equipment_DownloadIDHelper.WaitForText("The download id is successfully created!!", 10);
                equipment_DownloadIDHelper.WaitForWorkAround(2000);

                executionLog.Log("DownloadIdsManagement", "Enter Id to search");
                equipment_DownloadIDHelper.TypeText("SearchDownloadIds", Id);
                equipment_DownloadIDHelper.WaitForWorkAround(2000);

                executionLog.Log("DownloadIdsManagement", "Click on Edit button");
                equipment_DownloadIDHelper.ClickElement("Edit");
                equipment_DownloadIDHelper.WaitForWorkAround(3000);

                executionLog.Log("DownloadIdsManagement", "Verify title");
                VerifyTitle("Manage Master Equipment Type Download Ids");

                executionLog.Log("DownloadIdsManagement", "Select DownloadIdsType");
                equipment_DownloadIDHelper.Select("DownloadIdsType", "VAR");

                executionLog.Log("DownloadIdsManagement", "Enter DownloadsIDName");
                equipment_DownloadIDHelper.TypeText("DownloadsIDName", name);

                executionLog.Log("DownloadIdsManagement", "Enter DownloadsID");
                equipment_DownloadIDHelper.TypeText("DownloadID", Id + "22");

                executionLog.Log("DownloadIdsManagement", "Select Status");
                equipment_DownloadIDHelper.Select("Status", "0");

                executionLog.Log("DownloadIdsManagement", " Click on Save button ");
                equipment_DownloadIDHelper.ClickElement("Save");
                equipment_DownloadIDHelper.WaitForWorkAround(3000);

                executionLog.Log("DownloadIdsManagement", "Wait for text");
                equipment_DownloadIDHelper.WaitForText("The download id is successfully updated!!", 10);
                equipment_DownloadIDHelper.WaitForWorkAround(2000);

                executionLog.Log("DownloadIdsManagement", "Redirect at clients page");
                VisitOffice("clients");
                equipment_DownloadIDHelper.WaitForWorkAround(3000);

                executionLog.Log("DownloadIdsManagement", "Verify page title");
                VerifyTitle();

                executionLog.Log("DownloadIdsManagement", "Click on any client");
                office_ClientsHelper.ClickElement("Client1");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("DownloadIdsManagement", "Click on terminals and equipment tab");
                office_ClientsHelper.ClickElement("TerminalsandEquipments");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("DownloadIdsManagement", "Click on Add equipment");
                office_ClientsHelper.ClickElement("AddEquipment");
                office_ClientsHelper.WaitForWorkAround(4000);

                executionLog.Log("DownloadIdsManagement", "Click on any equipment");
                office_ClientsHelper.ClickForce("Equipment1");
                office_ClientsHelper.WaitForWorkAround(4000);

                executionLog.Log("DownloadIdsManagement", "Verify created Download ID present on page.");
                Assert.IsTrue(office_ClientsHelper.IsElementPresent("//select//option[text()= '" + name + "']"));
                //office_ClientsHelper.WaitForWorkAround(7000);

                executionLog.Log("DownloadIdsManagement", "Redirect at download id page");
                VisitOffice("download_ids");
                equipment_DownloadIDHelper.WaitForWorkAround(3000);

                executionLog.Log("DownloadIdsManagement", "Enter Id to search");
                equipment_DownloadIDHelper.TypeText("SearchDownloadIds", Id + "22");
                equipment_DownloadIDHelper.WaitForWorkAround(3000);

                executionLog.Log("DownloadIdsManagement", " Click to change status");
                equipment_DownloadIDHelper.ClickElement("StatusToggle");

                executionLog.Log("DownloadIdsManagement", " Accept alert message.");
                equipment_DownloadIDHelper.AcceptAlert();
                equipment_DownloadIDHelper.WaitForWorkAround(2000);

                executionLog.Log("DownloadIdsManagement", "Wait for status updation message");
                equipment_DownloadIDHelper.WaitForText("Equipment Download ID is successfully deactivated", 10);

                executionLog.Log("DownloadIdsManagement", "Redirect at clients page");
                VisitOffice("clients");
                equipment_DownloadIDHelper.WaitForWorkAround(3000);

                executionLog.Log("DownloadIdsManagement", "Verify page title");
                VerifyTitle();

                executionLog.Log("DownloadIdsManagement", "Click on any client");
                office_ClientsHelper.ClickElement("Client1");
                equipment_DownloadIDHelper.WaitForWorkAround(3000);

                executionLog.Log("DownloadIdsManagement", "Click on terminals and equipment tab");
                equipment_DownloadIDHelper.ClickElement("TerminalsandEquipments");
                equipment_DownloadIDHelper.WaitForWorkAround(3000);

                executionLog.Log("DownloadIdsManagement", "Click on Add equipment");
                equipment_DownloadIDHelper.ClickElement("AddEquipment");
                equipment_DownloadIDHelper.WaitForWorkAround(4000);

                executionLog.Log("DownloadIdsManagement", "Click on any equipment");
                equipment_DownloadIDHelper.ClickViaJavaScript("//*[@id='popupContent']/table/tbody/tr[1]/td[1]/a");
                equipment_DownloadIDHelper.WaitForWorkAround(4000);

                executionLog.Log("DownloadIdsManagement", "Verify download id not present on page.");
                Assert.IsFalse(equipment_DownloadIDHelper.IsElementPresent("//select//option[text()= '" + name + "']"));

                executionLog.Log("DownloadIdsManagement", "Redirect At download id page");
                VisitOffice("download_ids");
                equipment_DownloadIDHelper.WaitForWorkAround(3000);

                executionLog.Log("DownloadIdsManagement", "Enter Id to search");
                equipment_DownloadIDHelper.TypeText("SearchDownloadIds", Id + "22");
                equipment_DownloadIDHelper.WaitForWorkAround(2000);

                executionLog.Log("DownloadIdsManagement", " Click on Change status icon");
                equipment_DownloadIDHelper.ClickElement("StatusToggle");

                executionLog.Log("DownloadIdsManagement", " Accept Alert message.");
                equipment_DownloadIDHelper.AcceptAlert();
                equipment_DownloadIDHelper.WaitForWorkAround(3000);

                executionLog.Log("DownloadIdsManagement", "Wait for status updation message");
                equipment_DownloadIDHelper.WaitForText("Equipment Download ID is successfully activated", 5);

                VisitOffice("logout");
            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("DownloadIdsManagement");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("DownloadIds Management");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("DownloadIds Management", "Bug", "Medium", "Create DownloadsIDS page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("DownloadIds Management");
                        TakeScreenshot("DownloadIdsManagement");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\DownloadIdsManagement.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("DownloadIdsManagement");
                        string id = loginHelper.getIssueID("DownloadIds Management");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\DownloadIdsManagement.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("DownloadIds Management"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("DownloadIds Management");
                //    executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("DownloadIdsManagement");
                executionLog.WriteInExcel("DownloadIds Management", Status, JIRA, "Equipment Management");
            }
        }
    }
}