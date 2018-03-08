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
    public class ClickEditEquipmentAfterConfirmDelete : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS4")]
        [TestCategory("NewSkinTaskJira")]
        public void clickEditEquipmentAfterConfirmDelete()
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
            var Id = GetRandomNumber().ToString();
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("ClickEditEquipmentAfterConfirmDelete", "Login with valid username and password");
                Login(username[0], password[0]);

                executionLog.Log("ClickEditEquipmentAfterConfirmDelete", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("ClickEditEquipmentAfterConfirmDelete", "Go to Admin page");
                VisitOffice("admin");

                executionLog.Log("ClickEditEquipmentAfterConfirmDelete", "Go to equipments page.");
                VisitOffice("equipment");

                executionLog.Log("ClickEditEquipmentAfterConfirmDelete", "Click On Create");
                eqiupment_EquipmentHelper.ClickElement("Create");

                executionLog.Log("ClickEditEquipmentAfterConfirmDelete", "Verify Page title");
                VerifyTitle("Equipment Create");

                executionLog.Log("ClickEditEquipmentAfterConfirmDelete", "Enter Equipment Name");
                eqiupment_EquipmentHelper.TypeText("Name", name);

                executionLog.Log("ClickEditEquipmentAfterConfirmDelete", "Select type");
                eqiupment_EquipmentHelper.Select("Type", "Check Reader");

                executionLog.Log("ClickEditEquipmentAfterConfirmDelete", "Enter Equipment Id");
                eqiupment_EquipmentHelper.TypeText("EquipmentId", Id);

                executionLog.Log("ClickEditEquipmentAfterConfirmDelete", "Enter Version");
                eqiupment_EquipmentHelper.TypeText("Version", "Testing");

                executionLog.Log("ClickEditEquipmentAfterConfirmDelete", "Enter Description");
                eqiupment_EquipmentHelper.TypeText("Description", "This is Testing Description");

                executionLog.Log("ClickEditEquipmentAfterConfirmDelete", " Click on Save button");
                eqiupment_EquipmentHelper.ClickElement("Save");

                executionLog.Log("ClickEditEquipmentAfterConfirmDelete", " Wait for success message.");
                eqiupment_EquipmentHelper.WaitForText("Equipment saved successfully", 10);

                executionLog.Log("ClickEditEquipmentAfterConfirmDelete", "SearchEquipmenmt");
                eqiupment_EquipmentHelper.TypeText("SearchEquipment", name);
                eqiupment_EquipmentHelper.WaitForWorkAround(4000);

                executionLog.Log("ClickEditEquipmentAfterConfirmDelete", "Click on delete icon");
                eqiupment_EquipmentHelper.ClickElement("DeleteEuipment");

                executionLog.Log("ClickEditEquipmentAfterConfirmDelete", "Accept Alert");
                eqiupment_EquipmentHelper.AcceptAlert();

                executionLog.Log("ClickEditEquipmentAfterConfirmDelete", "Click edit equipment.");
                eqiupment_EquipmentHelper.ClickElement("DeleteEuipment");
                eqiupment_EquipmentHelper.WaitForWorkAround(3000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("ClickEditEquipmentAfterConfirmDelete");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Click Edit Equipment After Confirm Delete");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Click Edit Equipment After Confirm Delete", "Bug", "Medium", "Eqipment page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Click Edit Equipment After Confirm Delete");
                        TakeScreenshot("ClickEditEquipmentAfterConfirmDelete");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ClickEditEquipmentAfterConfirmDelete.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("ClickEditEquipmentAfterConfirmDelete");
                        string id = loginHelper.getIssueID("Click Edit Equipment After Confirm Delete");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ClickEditEquipmentAfterConfirmDelete.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Click Edit Equipment After Confirm Delete"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Click Edit Equipment After Confirm Delete");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("ClickEditEquipmentAfterConfirmDelete");
                executionLog.WriteInExcel("Click Edit Equipment After Confirm Delete", Status, JIRA, "Equipment Management");
            }
        }
    }
}
