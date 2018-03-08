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
    public class EquipentblankValidation : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS4")]
        [TestCategory("NewSkinTaskJira")]
        public void equipentblankValidation()
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
            String Status = "Pass";
            String JIRA = "";

            try
            {
                executionLog.Log("EquipentblankValidation", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("EquipentblankValidation", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("EquipentblankValidation", "Click On  Admin");
                VisitOffice("admin");

                executionLog.Log("EquipentblankValidation", "Click  On menu icon");
                // eqiupment_EquipmentHelper.ClickElement("MenuIcon");

                executionLog.Log("EquipentblankValidation", "Click on Terminal And Equipment Tab");
                eqiupment_EquipmentHelper.MouseOverAndWait("ClickOnEquipmentTab", 2);

                executionLog.Log("EquipentblankValidation", "Click on equipment button");
                eqiupment_EquipmentHelper.Clickjs("EquipmentBtn");
                eqiupment_EquipmentHelper.WaitForWorkAround(3000);

                executionLog.Log("EquipentblankValidation", " Click On Create");
                eqiupment_EquipmentHelper.ClickElement("Create");
                eqiupment_EquipmentHelper.WaitForWorkAround(3000);

                executionLog.Log("EquipentblankValidation", " Click on Save button   ");
                eqiupment_EquipmentHelper.ClickElement("Save");
                eqiupment_EquipmentHelper.WaitForWorkAround(3000);

                executionLog.Log("EquipentblankValidation", "Verify validation");
                eqiupment_EquipmentHelper.VerifyText("VerifyVersionVal", "This field is required.");

                executionLog.Log("EquipentblankValidation", "Enter Equipment Name");
                eqiupment_EquipmentHelper.TypeText("Name", name);

                executionLog.Log("EquipentblankValidation", "Enter DownloadsIDName");
                eqiupment_EquipmentHelper.Select("Type", "Check Reader");

                executionLog.Log("EquipentblankValidation", "Enter Equipment version");
                eqiupment_EquipmentHelper.TypeText("Version", "1");

                executionLog.Log("EquipentblankValidation", " Click on Save button   ");
                eqiupment_EquipmentHelper.ClickElement("Save");
                eqiupment_EquipmentHelper.WaitForWorkAround(3000);

                executionLog.Log("EquipentblankValidation", "Wait for success text.");
                eqiupment_EquipmentHelper.WaitForText("Equipment saved successfully", 10);

                executionLog.Log("EquipentblankValidation", "Enter Name in seacrh field");
                eqiupment_EquipmentHelper.TypeText("SearchEquipment", name);
                eqiupment_EquipmentHelper.WaitForWorkAround(3000);

                executionLog.Log("EquipentblankValidation", "Click on delete icon");
                eqiupment_EquipmentHelper.ClickElement("DeleteEuipment");

                executionLog.Log("EquipentblankValidation", "Accept Alert Message.");
                eqiupment_EquipmentHelper.AcceptAlert();

                executionLog.Log("EquipentblankValidation", "Verify text Equipment deleted.");
                eqiupment_EquipmentHelper.WaitForText("Equipment deleted successfully.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";
                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("EquipentblankValidation");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Equipent blank Validation");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Equipent blank Validation", "Bug", "Medium", "Equipment page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Equipent blank Validation");
                        TakeScreenshot("EquipentblankValidation");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EquipentblankValidation.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("EquipentblankValidation");
                        string id = loginHelper.getIssueID("Equipent blank Validation");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EquipentblankValidation.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Equipent blank Validation"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Equipent blank Validation");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("EquipentblankValidation");
                executionLog.WriteInExcel("Equipent blank Validation", Status, JIRA, "Equipment Management");
            }
        }
    }
}