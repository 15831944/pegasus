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
    public class BulkUpdateEquipmentType : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("Fail")]
        [TestCategory("TS1")]
        [TestCategory("BugTest")]
        public void bulkUpdateEquipmentType()
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
            String name = "Test" + GetRandomNumber();
            String Id = "12345" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("BulkUpdateEquipmentType", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("BulkUpdateEquipmentType", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("BulkUpdateEquipmentType", "Redirecte to admin");
                VisitOffice("admin");
                eqiupment_EquipmentHelper.WaitForWorkAround(3000);

                executionLog.Log("BulkUpdateEquipmentType", "Redirect To equipment");
                VisitOffice("equipment");
                eqiupment_EquipmentHelper.WaitForWorkAround(5000);

                executionLog.Log("BulkUpdateEquipmentType", "Verify title");
                VerifyTitle("Equipment");

                var Loc = "//table[@id='list1']/tbody/tr[2]";
                if (eqiupment_EquipmentHelper.IsElementPresent(Loc))
                {

                    executionLog.Log("BulkUpdateEquipmentType", "Click On Equipment");
                    eqiupment_EquipmentHelper.ClickElement("ClickOnEquipChkBox");
                    eqiupment_EquipmentHelper.WaitForWorkAround(3000);

                    executionLog.Log("BulkUpdateEquipmentType", "Click On bulk update.");
                    eqiupment_EquipmentHelper.ClickElement("ClickBulkUpdateBtn");
                    eqiupment_EquipmentHelper.WaitForWorkAround(2000);

                    executionLog.Log("BulkUpdateEquipmentType", "Change Status");
                    eqiupment_EquipmentHelper.ClickElement("ClikEquipType");
                    eqiupment_EquipmentHelper.WaitForWorkAround(2000);

                    executionLog.Log("BulkUpdateEquipmentType", "Select Status");
                    eqiupment_EquipmentHelper.SelectByText("EquipTypeStatus", "Check Reader");

                    executionLog.Log("BulkUpdateEquipmentType", "Click on Update button");
                    eqiupment_EquipmentHelper.ClickElement("SaveBulkType");
                    eqiupment_EquipmentHelper.WaitForWorkAround(3000);

                    executionLog.Log("BulkUpdateEquipmentType", "Accept alert message.");
                    eqiupment_EquipmentHelper.AcceptAlert();
                    eqiupment_EquipmentHelper.WaitForWorkAround(5000);

                    executionLog.Log("BulkUpdateEquipmentType", "Verify text.");
                    eqiupment_EquipmentHelper.WaitForText("Record(s) updated successfully", 5);

                }
                else
                {

                    executionLog.Log("BulkUpdateEquipmentType", " Click On Create");
                    eqiupment_EquipmentHelper.ClickElement("Create");
                    eqiupment_EquipmentHelper.WaitForWorkAround(6000);

                    executionLog.Log("BulkUpdateEquipmentType", "Enter Equipment Name");
                    eqiupment_EquipmentHelper.TypeText("Name", "Bulk Equipment");

                    executionLog.Log("BulkUpdateEquipmentType", "Enter equipment type");
                    eqiupment_EquipmentHelper.Select("Type", "Check Reader");

                    executionLog.Log("BulkUpdateEquipmentType", "Enter Equipment Id");
                    eqiupment_EquipmentHelper.TypeText("EquipmentId", Id);

                    executionLog.Log("BulkUpdateEquipmentType", "Enter Category");
                    eqiupment_EquipmentHelper.SelectByText("Category", "EQP-1");

                    executionLog.Log("BulkUpdateEquipmentType", "Enter Version");
                    eqiupment_EquipmentHelper.TypeText("Version", "Testing");

                    executionLog.Log("BulkUpdateEquipmentType", "Enter Description");
                    eqiupment_EquipmentHelper.TypeText("Description", "This is Testing Description");

                    executionLog.Log("BulkUpdateEquipmentType", " Click on Save button ");
                    eqiupment_EquipmentHelper.ClickElement("Save");
                    eqiupment_EquipmentHelper.WaitForWorkAround(6000);

                    executionLog.Log("BulkUpdateEquipmentType", "Click On Equipment");
                    eqiupment_EquipmentHelper.ClickElement("ClickOnEquipChkBox");

                    executionLog.Log("BulkUpdateEquipmentType", "Click On Bulk update button.");
                    eqiupment_EquipmentHelper.ClickElement("ClickBulkUpdateBtn");

                    executionLog.Log("BulkUpdateEquipmentType", "Change Status");
                    eqiupment_EquipmentHelper.ClickElement("ClikEquipType");

                    executionLog.Log("BulkUpdateEquipmentType", "Select Status");
                    eqiupment_EquipmentHelper.Select("EquipTypeStatus", "Check Reader");

                    executionLog.Log("BulkUpdateEquipmentType", "Click on Update button");
                    eqiupment_EquipmentHelper.ClickElement("SaveBulk");
                    eqiupment_EquipmentHelper.WaitForWorkAround(5000);

                    executionLog.Log("BulkUpdateEquipmentType", "Accept Alert message.");
                    eqiupment_EquipmentHelper.AcceptAlert();
                    eqiupment_EquipmentHelper.WaitForWorkAround(5000);

                    eqiupment_EquipmentHelper.WaitForText("Record(s) updated successfully", 5);

                }
            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("BulkUpdateEquipmentType");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Bulk Update Equipment Type");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Bulk Update Equipment Type", "Bug", "Medium", "Equipment page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Bulk Update Equipment Status");
                        TakeScreenshot("BulkUpdateEquipmentType");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\BulkUpdateEquipmentType.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("BulkUpdateEquipmentType");
                        string id = loginHelper.getIssueID("Bulk Update Equipment Type");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\BulkUpdateEquipmentType.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Bulk Update Equipment Type"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Bulk Update Equipment Type");
                //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("BulkUpdateEquipmentType");
                executionLog.WriteInExcel("Bulk Update Equipment Type", Status, JIRA, "Admin Equipments");
            }
        }
    }
}
