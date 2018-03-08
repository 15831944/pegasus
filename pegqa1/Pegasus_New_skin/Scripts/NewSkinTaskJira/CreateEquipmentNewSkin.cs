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
    public class CreateEquipmentNewSkin : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("Fail")]
        [TestCategory("TS4")]
        [TestCategory("NewSkinTaskJira")]
        public void createEquipmentNewSkin()
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
            var name = "Test" + RandomNumber(1, 99999);
            var Id = "12345" + RandomNumber(1, 99999);
            String JIRA = "";
            String Status = "Pass";

            try
            {

            executionLog.Log("CreateEquipmentNewSkin", "Login with valid username and password");
            Login(username[0], password[0]);

            executionLog.Log("CreateEquipmentNewSkin", "Verify Page title");
            VerifyTitle("Dashboard");

            executionLog.Log("CreateEquipmentNewSkin", "Click On  Admin");
            VisitOffice("admin");

            executionLog.Log("CreateEquipmentNewSkin", "Redirect To equipment");
            VisitOffice("equipment");
            eqiupment_EquipmentHelper.WaitForWorkAround(4000);

            executionLog.Log("CreateEquipmentNewSkin", "Click On Create");
            eqiupment_EquipmentHelper.ClickElement("Create");
            eqiupment_EquipmentHelper.WaitForWorkAround(2000);

            executionLog.Log("CreateEquipmentNewSkin", "Enter Equipment Name");
            eqiupment_EquipmentHelper.TypeText("Name", name);
            eqiupment_EquipmentHelper.WaitForWorkAround(1000);

            executionLog.Log("CreateEquipmentNewSkin", "Select Type");
            eqiupment_EquipmentHelper.Select("Type", "Check Reader");
            eqiupment_EquipmentHelper.WaitForWorkAround(1000);

            executionLog.Log("CreateEquipmentNewSkin", "Enter Equipment Id");
            eqiupment_EquipmentHelper.TypeText("EquipmentId", Id);
            eqiupment_EquipmentHelper.WaitForWorkAround(1000);

            executionLog.Log("CreateEquipmentNewSkin", "Enter Version");
            eqiupment_EquipmentHelper.TypeText("Version", "Testing");
            eqiupment_EquipmentHelper.WaitForWorkAround(1000);

            executionLog.Log("CreateEquipmentNewSkin", "Enter Description");
            eqiupment_EquipmentHelper.TypeText("Description", "This is Testing Description");
            eqiupment_EquipmentHelper.WaitForWorkAround(1000);

            executionLog.Log("CreateEquipmentNewSkin", "Click on Save button   ");
            eqiupment_EquipmentHelper.ClickElement("Save");
            eqiupment_EquipmentHelper.WaitForWorkAround(3000);

            executionLog.Log("CreateEquipmentNewSkin", " Wait for save equipment text ");
            eqiupment_EquipmentHelper.WaitForText("Equipment saved successfully", 10);
            eqiupment_EquipmentHelper.WaitForWorkAround(1000);

            executionLog.Log("CreateEquipmentNewSkin", "Redirect To URL");
            VisitOffice("equipment");
            eqiupment_EquipmentHelper.WaitForWorkAround(4000);

            executionLog.Log("CreateEquipmentNewSkin", "Verify title");
            VerifyTitle("Equipment");
            eqiupment_EquipmentHelper.WaitForWorkAround(1000);

            executionLog.Log("CreateEquipmentNewSkin", "Enter Name to search");
            eqiupment_EquipmentHelper.TypeText("SearchId", Id);
            eqiupment_EquipmentHelper.WaitForWorkAround(2000);

            executionLog.Log("CreateEquipmentNewSkin", "cLICK Delete btn  ");
            eqiupment_EquipmentHelper.ClickElement("DeleteEuipment");
            eqiupment_EquipmentHelper.WaitForWorkAround(1000);

            executionLog.Log("CreateEquipmentNewSkin", "Accept alert message. ");
            eqiupment_EquipmentHelper.AcceptAlert();
            eqiupment_EquipmentHelper.WaitForWorkAround(1000);

            executionLog.Log("CreateEquipmentNewSkin", "Wait for delete message. ");
            eqiupment_EquipmentHelper.WaitForText("Equipment deleted successfully", 10);

        }
    

        catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CreateEquipmentNewSkin");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Create Equipment New Skin");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Create Equipment New Skin", "Bug", "Medium", "Equipment page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Create Equipment New Skin");
                        TakeScreenshot("CreateEquipmentNewSkin");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateEquipmentNewSkin.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CreateEquipmentNewSkin");
                        string id = loginHelper.getIssueID("Create Equipment New Skin");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateEquipmentNewSkin.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Create Equipment New Skin"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Create Equipment New Skin");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("CreateEquipmentNewSkin");
                executionLog.WriteInExcel("Create Equipment New Skin", Status, JIRA, "Equipment Management");
            }
        }
    }
}  