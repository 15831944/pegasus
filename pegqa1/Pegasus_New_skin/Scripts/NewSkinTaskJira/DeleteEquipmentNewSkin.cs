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
    public class DeleteEquipmentNewSkin : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS4")]
        [TestCategory("NewSkinTaskJira")]
        public void deleteEquipmentNewSkin()
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
            var name = "Test" + RandomNumber(1, 99);
            var Id = "12345" + RandomNumber(1, 99);
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("DeleteEquipmentNewSkin", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("DeleteEquipmentNewSkin", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("DeleteEquipmentNewSkin", "Click On  Admin");
                VisitOffice("admin");

                executionLog.Log("DeleteEquipmentNewSkin", "Redirect To equipment");
                VisitOffice("equipment");
                eqiupment_EquipmentHelper.WaitForWorkAround(4000);

                executionLog.Log("DeleteEquipmentNewSkin", " Click On Create");
                eqiupment_EquipmentHelper.ClickElement("Create");
                eqiupment_EquipmentHelper.WaitForWorkAround(2000);

                executionLog.Log("DeleteEquipmentNewSkin", "Enter Equipment Name");
                eqiupment_EquipmentHelper.TypeText("Name", "Delete Equip");

                executionLog.Log("DeleteEquipmentNewSkin", "Select Type");
                eqiupment_EquipmentHelper.Select("Type", "Check Reader");

                executionLog.Log("DeleteEquipmentNewSkin", "Enter Equipment Id");
                eqiupment_EquipmentHelper.TypeText("EquipmentId", Id);

                executionLog.Log("DeleteEquipmentNewSkin", "Enter Version");
                eqiupment_EquipmentHelper.TypeText("Version", "Testing");

                executionLog.Log("DeleteEquipmentNewSkin", "Enter Description");
                eqiupment_EquipmentHelper.TypeText("Description", "This is Testing Description");

                executionLog.Log("DeleteEquipmentNewSkin", " Click on Save button.");
                eqiupment_EquipmentHelper.ClickElement("Save");
                eqiupment_EquipmentHelper.WaitForWorkAround(2000);

                executionLog.Log("DeleteEquipmentNewSkin", "Enter Name in seacrh field");
                eqiupment_EquipmentHelper.TypeText("SearchEquipment", "Delete Equip");
                eqiupment_EquipmentHelper.WaitForWorkAround(2000);

                executionLog.Log("DeleteEquipmentNewSkin", "Click on delete icon");
                eqiupment_EquipmentHelper.ClickElement("DeleteEuipment");
                eqiupment_EquipmentHelper.WaitForWorkAround(2000);

                executionLog.Log("DeleteEquipmentNewSkin", "Accept Alert Message.");
                eqiupment_EquipmentHelper.AcceptAlert();
                eqiupment_EquipmentHelper.WaitForWorkAround(1000);

                executionLog.Log("DeleteEquipmentNewSkin", "Verify text Equipment deleted.");
                eqiupment_EquipmentHelper.WaitForText("Equipment deleted successfully.", 20);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("DeleteEquipmentNewSkin");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Delete Equipment New Skin");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Delete Equipment New Skin", "Bug", "Medium", "Equipment page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Delete Equipment New Skin");
                        TakeScreenshot("DeleteEquipmentNewSkin");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\DeleteEquipmentNewSkin.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("DeleteEquipmentNewSkin");
                        string id = loginHelper.getIssueID("Delete Equipment New Skin");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\DeleteEquipmentNewSkin.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Delete Equipment New Skin"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Delete Equipment New Skin");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("DeleteEquipmentNewSkin");
                executionLog.WriteInExcel("Delete Equipment New Skin", Status, JIRA, "Equipment Management");
            }
        }
    }
}