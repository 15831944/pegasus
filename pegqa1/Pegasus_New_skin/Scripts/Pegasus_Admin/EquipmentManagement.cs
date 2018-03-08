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
    public class EquipmentManagement : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin")]
        [TestCategory("Fail")]
        [TestCategory("TS6")]
        [TestCategory("Pegasus_Admin")]
        public void equipmentManagement()
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
            var name = "Test" + RandomNumber(1, 999);
            var Id = "123" + RandomNumber(1, 999);
            String JIRA = "";
            String Status = "Pass";

            //try
            //{

                executionLog.Log("EquipmentManagement", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("EquipmentManagement", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("EquipmentManagement", "Click On  Admin");
                VisitOffice("admin");

                executionLog.Log("EquipmentManagement", "Redirect To URL");
                VisitOffice("equipment");
                eqiupment_EquipmentHelper.WaitForWorkAround(3000);

                executionLog.Log("EquipmentManagement", "Verify title");
                VerifyTitle("Equipment");

                executionLog.Log("EquipmentManagement", " Click On Create");
                eqiupment_EquipmentHelper.Clickjs("Create");
                eqiupment_EquipmentHelper.WaitForWorkAround(3000);

                executionLog.Log("EquipmentManagement", "Verify title");
                VerifyTitle("Equipment Create");

                executionLog.Log("EquipmentManagement", " Click on Save button ");
                eqiupment_EquipmentHelper.Clickjs("Save");
                //eqiupment_EquipmentHelper.WaitForWorkAround(3000);

                executionLog.Log("EquipmentManagement", " Verify validation text on page.");
                eqiupment_EquipmentHelper.VerifyPageText("This field is required.");
                //eqiupment_EquipmentHelper.WaitForWorkAround(500);

                executionLog.Log("EquipmentManagement", "Enter Equipment Name");
                eqiupment_EquipmentHelper.TypeText("Name", name);
                //eqiupment_EquipmentHelper.WaitForWorkAround(500);

                executionLog.Log("EquipmentManagement", "Select Equipment type");
                eqiupment_EquipmentHelper.Select("Type", "Check Reader");
                //eqiupment_EquipmentHelper.WaitForWorkAround(500);

                executionLog.Log("EquipmentManagement", "Enter Equipment Id");
                eqiupment_EquipmentHelper.TypeText("EquipmentId", Id);
                //eqiupment_EquipmentHelper.WaitForWorkAround(500);

                executionLog.Log("EquipmentManagement", "Enter Version");
                eqiupment_EquipmentHelper.TypeText("Version", "6.0.1");
                //eqiupment_EquipmentHelper.WaitForWorkAround(500);

                executionLog.Log("EquipmentManagement", "Enter Description");
                eqiupment_EquipmentHelper.TypeText("Description", "This is Testing Description");
                //eqiupment_EquipmentHelper.WaitForWorkAround(500);

                executionLog.Log("EquipmentManagement", "Select first processor");
                eqiupment_EquipmentHelper.ClickElement("ApplicableProcessors1");

                executionLog.Log("EquipmentManagement", "Select second processor.");
                eqiupment_EquipmentHelper.ClickElement("ApplicableProcessors2");

                executionLog.Log("EquipmentManagement", " Click on Save button ");
                eqiupment_EquipmentHelper.ClickElement("Save");
                eqiupment_EquipmentHelper.WaitForWorkAround(3000);

                executionLog.Log("EquipmentManagement", " Wait for save equipment text ");
                eqiupment_EquipmentHelper.WaitForText("Equipment saved successfully", 10);

                executionLog.Log("EquipmentManagement", "Redirect To URL");
                VisitOffice("equipment");
                eqiupment_EquipmentHelper.WaitForWorkAround(4000);

                executionLog.Log("EquipmentManagement", "Verify title");
                VerifyTitle("Equipment");
                //eqiupment_EquipmentHelper.WaitForWorkAround(3000);

                executionLog.Log("EquipmentManagement", "Enter ID to search equipment.");
                eqiupment_EquipmentHelper.TypeText("SearchId", Id);
                eqiupment_EquipmentHelper.WaitForWorkAround(2000);

                executionLog.Log("EquipmentManagement", "Click on edit equipment.");
                eqiupment_EquipmentHelper.Clickjs("EditEquipment");
                eqiupment_EquipmentHelper.WaitForWorkAround(3000);

                executionLog.Log("EquipmentManagement", "Enter Version");
                eqiupment_EquipmentHelper.TypeText("Version", "");
                //eqiupment_EquipmentHelper.WaitForWorkAround(500);

                executionLog.Log("EquipmentManagement", " Click on Save button ");
                eqiupment_EquipmentHelper.ClickElement("Save");
                //eqiupment_EquipmentHelper.WaitForWorkAround(500);

                executionLog.Log("EquipmentManagement", " Verify version error text.");
                eqiupment_EquipmentHelper.VerifyText("VerifyVersionVal", "This field is required.");
                //eqiupment_EquipmentHelper.WaitForWorkAround(500);

                executionLog.Log("EquipmentManagement", "Enter Version");
                eqiupment_EquipmentHelper.TypeText("Version", "10.0.1");
                //eqiupment_EquipmentHelper.WaitForWorkAround(500);

                executionLog.Log("EquipmentManagement", "Click on Add version.");
                eqiupment_EquipmentHelper.ClickElement("ClickAddAnother");
                eqiupment_EquipmentHelper.WaitForWorkAround(1000);

                executionLog.Log("EquipmentManagement", "Enter Version");
                eqiupment_EquipmentHelper.TypeText("AddedVersion", "2");
                //eqiupment_EquipmentHelper.WaitForWorkAround(500);

                executionLog.Log("EquipmentManagement", " Click on Save button ");
                eqiupment_EquipmentHelper.ClickElement("Save");
                eqiupment_EquipmentHelper.WaitForWorkAround(4000);

                executionLog.Log("EquipmentManagement", "Enter ID to search equipment");
                eqiupment_EquipmentHelper.TypeText("SearchId", Id);
                eqiupment_EquipmentHelper.WaitForWorkAround(2000);

                executionLog.Log("EquipmentManagement", "Click On Equipment");
                eqiupment_EquipmentHelper.ClickElement("ClickOneQUIP");
                eqiupment_EquipmentHelper.WaitForWorkAround(3000);

                executionLog.Log("EquipmentManagement", "Click On Clone");
                eqiupment_EquipmentHelper.ClickElement("ClickOnClone");
                eqiupment_EquipmentHelper.WaitForWorkAround(2000);

                executionLog.Log("EquipmentManagement", "wait for success message.");
                eqiupment_EquipmentHelper.WaitForText("Equipment is cloned successfully", 20);

                executionLog.Log("EquipmentManagement", "Redirect To URL");
                VisitOffice("equipment");
                eqiupment_EquipmentHelper.WaitForWorkAround(3000);

                executionLog.Log("EquipmentManagement", "Verify title");
                VerifyTitle("Equipment");
                //eqiupment_EquipmentHelper.WaitForWorkAround(1000);

                executionLog.Log("EquipmentManagement", "Click On first chk box");
                eqiupment_EquipmentHelper.ClickElement("ClickOnFirstCheckBox");

                executionLog.Log("EquipmentManagement", "Click On Bulk Update");
                eqiupment_EquipmentHelper.Clickjs("BulkUpdate");
                eqiupment_EquipmentHelper.WaitForWorkAround(1000);

                executionLog.Log("EquipmentManagement", "Click to Change Status");
                eqiupment_EquipmentHelper.ClickElement("ChangeStatusBU");
                eqiupment_EquipmentHelper.WaitForWorkAround(2000);

                executionLog.Log("EquipmentManagement", "Select Status as active");
                eqiupment_EquipmentHelper.Select("SelectStatus", "1");
                //eqiupment_EquipmentHelper.WaitForWorkAround(2000);

                executionLog.Log("EquipmentManagement", "Click on Update button");
                eqiupment_EquipmentHelper.ClickElement("ClickOnSaveBulkPopUp");
                //eqiupment_EquipmentHelper.WaitForWorkAround(2000);

                executionLog.Log("EquipmentManagement", "Accept alert message.");
                eqiupment_EquipmentHelper.AcceptAlert();
                eqiupment_EquipmentHelper.WaitForWorkAround(2000);

                executionLog.Log("EquipmentManagement", "Wait for success message.");
                eqiupment_EquipmentHelper.WaitForText("1 Record(s) updated successfully", 10);

                executionLog.Log("EquipmentManagement", "Click on delete Icon");
                eqiupment_EquipmentHelper.ClickElement("ClickDelClone");
                eqiupment_EquipmentHelper.WaitForWorkAround(1000);

                executionLog.Log("EquipmentManagement", "Accept alert message.");
                eqiupment_EquipmentHelper.AcceptAlert();

                executionLog.Log("EquipmentManagement", "Wait for delete success message.");
                eqiupment_EquipmentHelper.WaitForText("Equipment deleted successfully.", 10);
                eqiupment_EquipmentHelper.WaitForWorkAround(2000);

                executionLog.Log("EquipmentManagement", "Enter ID to search equipment.");
                eqiupment_EquipmentHelper.TypeText("SearchId", Id);
                eqiupment_EquipmentHelper.WaitForWorkAround(2000);

                executionLog.Log("EquipmentManagement", "Click Delete Icon ");
                eqiupment_EquipmentHelper.Clickjs("DeleteEuipment");
                eqiupment_EquipmentHelper.WaitForWorkAround(1000);

                executionLog.Log("EquipmentManagement", "Accept alert message. ");
                eqiupment_EquipmentHelper.AcceptAlert();
                eqiupment_EquipmentHelper.WaitForWorkAround(2000);

                executionLog.Log("EquipmentManagement", "Wait for delete message. ");
                eqiupment_EquipmentHelper.WaitForText("Equipment deleted successfully", 10);

            //}
            //catch (Exception e)
            //{
            //    executionLog.Log("Error", e.StackTrace);
            //    Status = "Fail";

            //    String counter = executionLog.readLastLine("counter");
            //    String Description = executionLog.GetAllTextFile("EquipmentManagement");
            //    String Error = executionLog.GetAllTextFile("Error");
            //    Console.WriteLine(Error);
            //    if (counter == "")
            //    {
            //        counter = "0";
            //    }
            //    bool result = loginHelper.CheckExstingIssue("Equipment Management");
            //    if (!result)
            //    {
            //        if (Int16.Parse(counter) < 9)
            //        {
            //            executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
            //            loginHelper.CreateIssue("Equipment Management", "Bug", "Medium", "Create Eqiupment page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
            //            string id = loginHelper.getIssueID("Equipment Management");
            //            TakeScreenshot("EquipmentManagement");
            //            string directoryName = loginHelper.GetnewDirectoryName(GetPath());
            //            var location = directoryName + "\\EquipmentManagement.png";
            //            loginHelper.AddAttachment(location, id);
            //        }
            //    }
            //    else
            //    {
            //        if (Int16.Parse(counter) < 9)
            //        {
            //            executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
            //            TakeScreenshot("EquipmentManagement");
            //            string id = loginHelper.getIssueID("Equipment Management");
            //            string directoryName = loginHelper.GetnewDirectoryName(GetPath());
            //            var location = directoryName + "\\EquipmentManagement.png";
            //            loginHelper.AddAttachment(location, id);
            //            loginHelper.AddComment(loginHelper.getIssueID("Equipment Management"), "This issue is still occurring");
            //        }
            //    }
            //    JIRA = loginHelper.getIssueID("Equipment Management");
            // //   executionLog.DeleteFile("Error");
            //    throw;
            //}
            //finally
            //{
            //    executionLog.DeleteFile("EquipmentManagement");
            //    executionLog.WriteInExcel("Equipment Management", Status, JIRA, "Equipment Management");
            //}
        }
    }
}