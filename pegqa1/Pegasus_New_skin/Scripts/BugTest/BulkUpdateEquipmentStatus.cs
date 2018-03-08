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
    public class BulkUpdateEquipmentStatus : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS1")]
        [TestCategory("BugTest")]
        public void bulkUpdateEquipmentStatus()
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
            var eqiupment_EquipmentHelper = new Eqiupment_EquipmentHelper(GetWebDriver());

            // Variable 
            var name = "Test" + GetRandomNumber();
            var Id = "12345" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("BulkUpdateEquipmentStatus", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("BulkUpdateEquipmentStatus", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("BulkUpdateEquipmentStatus", "Go to office  Admin");
                VisitOffice("admin");

                executionLog.Log("BulkUpdateEquipmentStatus", "Redirect To equipment page");
                VisitOffice("equipment");
                eqiupment_EquipmentHelper.WaitForWorkAround(1000);

                executionLog.Log("BulkUpdateEquipmentStatus", "Verify title");
                VerifyTitle("Equipment");

                var Loc = "//table[@id='list1']/tbody/tr[2]";
                if (eqiupment_EquipmentHelper.IsElementPresent(Loc))
                {
                    executionLog.Log("BulkUpdateEquipmentStatus", "Click On Equipment");
                    eqiupment_EquipmentHelper.ClickElement("FirstEquip");
                    eqiupment_EquipmentHelper.WaitForWorkAround(1000);

                    executionLog.Log("BulkUpdateEquipmentStatus", "Click On Clone");
                    eqiupment_EquipmentHelper.ClickElement("Clone");
                    eqiupment_EquipmentHelper.WaitForWorkAround(1000);

                    executionLog.Log("BulkUpdateEquipmentStatus", "Verify text Equipment is cloned successfully");
                    eqiupment_EquipmentHelper.WaitForText("Equipment is cloned successfully", 10);

                    executionLog.Log("BulkUpdateEquipmentStatus", "Redirect to equipments page.");
                    VisitOffice("equipment");

                    executionLog.Log("BulkUpdateEquipmentStatus", "Click On first chk box");
                    eqiupment_EquipmentHelper.ClickElement("ClickOnFirstCheckBox");

                    executionLog.Log("BulkUpdateEquipmentStatus", "Click On Bulk Update");
                    eqiupment_EquipmentHelper.Clickjs("BulkUpdate");
                    eqiupment_EquipmentHelper.WaitForWorkAround(2000);

                    executionLog.Log("BulkUpdateEquipmentStatus", "Change Status");
                    eqiupment_EquipmentHelper.ClickElement("ChangeStatusBU");
                    eqiupment_EquipmentHelper.WaitForWorkAround(2000);

                    executionLog.Log("BulkUpdateEquipmentStatus", "Select Status as active");
                    eqiupment_EquipmentHelper.Select("SelectStatus", "1");
                    eqiupment_EquipmentHelper.WaitForWorkAround(2000);

                    executionLog.Log("BulkUpdateEquipmentStatus", "Click on Update button");
                    eqiupment_EquipmentHelper.ClickElement("ClickOnSaveBulkPopUp");
                    eqiupment_EquipmentHelper.WaitForWorkAround(2000);

                    executionLog.Log("BulkUpdateEquipmentStatus", "Accept alert message.");
                    eqiupment_EquipmentHelper.AcceptAlert();

                }
                else
                {
                    executionLog.Log("BulkUpdateEquipmentStatus", " Click On Create");
                    eqiupment_EquipmentHelper.ClickElement("Create");
                    eqiupment_EquipmentHelper.WaitForWorkAround(3000);

                    var EquipName = "Bulk Equipment" + GetRandomNumber();
                    executionLog.Log("BulkUpdateEquipmentStatus", "Enter Equipment Name");
                    eqiupment_EquipmentHelper.TypeText("Name", EquipName);

                    executionLog.Log("BulkUpdateEquipmentStatus", "Select equipment type.");
                    eqiupment_EquipmentHelper.Select("Type", "Check Reader");

                    executionLog.Log("BulkUpdateEquipmentStatus", "Enter Equipment Id");
                    eqiupment_EquipmentHelper.TypeText("EquipmentId", Id);

                    executionLog.Log("BulkUpdateEquipmentStatus", "Enter equipment Version");
                    eqiupment_EquipmentHelper.TypeText("Version", "Testing");

                    executionLog.Log("BulkUpdateEquipmentStatus", "Enter Description");
                    eqiupment_EquipmentHelper.TypeText("Description", "This is Testing Description");

                    executionLog.Log("BulkUpdateEquipmentStatus", "Click On First CheckBox");
                    eqiupment_EquipmentHelper.ClickElement("ClickOnFirstCheckBox");

                    executionLog.Log("BulkUpdateEquipmentStatus", " Click on Save button ");
                    eqiupment_EquipmentHelper.ClickElement("Save");
                    eqiupment_EquipmentHelper.WaitForWorkAround(3000);

                    executionLog.Log("BulkUpdateEquipmentStatus", "Enter Name in seacrh field");
                    eqiupment_EquipmentHelper.TypeText("SearchEquipment", EquipName);

                    executionLog.Log("BulkUpdateEquipmentStatus", "ClickOn Equipment");
                    eqiupment_EquipmentHelper.ClickElement("ClickOnEquipChkBox");

                    executionLog.Log("BulkUpdateEquipmentStatus", "Click On Bulk Update");
                    eqiupment_EquipmentHelper.Clickjs("ClickBulkUpdateBtn");

                    executionLog.Log("BulkUpdateEquipmentStatus", "Change Status");
                    eqiupment_EquipmentHelper.ClickElement("ChangeStatusBU");
                    eqiupment_EquipmentHelper.WaitForWorkAround(1000);

                    executionLog.Log("BulkUpdateEquipmentStatus", "Select Status");
                    eqiupment_EquipmentHelper.Select("SelectStatus", "1");

                    executionLog.Log("BulkUpdateEquipmentStatus", "Click on Update button");
                    eqiupment_EquipmentHelper.ClickElement("ClickOnSaveBulkPopUp");
                    eqiupment_EquipmentHelper.WaitForWorkAround(2000);

                    executionLog.Log("BulkUpdateEquipmentStatus", "Accept Alert");
                    eqiupment_EquipmentHelper.AcceptAlert();

                    eqiupment_EquipmentHelper.WaitForText("Record(s) updated successfully", 20);
                    eqiupment_EquipmentHelper.WaitForWorkAround(2000);

                }
            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("BulkUpdateEquipmentStatus");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Bulk Update Equipment Status");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Bulk Update Equipment Status", "Bug", "Medium", "Equipment page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Bulk Update Equipment Status");
                        TakeScreenshot("BulkUpdateEquipmentStatus");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\BulkUpdateEquipmentStatus.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("BulkUpdateEquipmentStatus");
                        string id = loginHelper.getIssueID("Bulk Update Equipment Status");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\BulkUpdateEquipmentStatus.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Bulk Update Equipment Status"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Bulk Update Equipment Status");
                //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("BulkUpdateEquipmentStatus");
                executionLog.WriteInExcel("Bulk Update Equipment Status", Status, JIRA, "Office equipment");
            }
        }
    }
}