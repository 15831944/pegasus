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
    public class CreateEquipments : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin")]
        [TestCategory("Fail")]
        [TestCategory("TS6")]
        [TestCategory("Pegasus_Admin")]
        public void createEquipments()
        {
            string[] username = null;
            string[] password = null;

            XMLParse oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            //Initializing the objects
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

                executionLog.Log("CreateEquipments", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("CreateEquipments", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("CreateEquipments", "Click On  Admin");
                VisitOffice("admin");
                eqiupment_EquipmentHelper.WaitForWorkAround(2000);

                executionLog.Log("CreateEquipments", "Redirect To URL");
                VisitOffice("equipment");
                eqiupment_EquipmentHelper.WaitForWorkAround(5000);

                executionLog.Log("CreateEquipments", "Verify title");
                VerifyTitle("Equipment");
                eqiupment_EquipmentHelper.WaitForWorkAround(4000);

                executionLog.Log("CreateEquipments", " Click On Create");
                eqiupment_EquipmentHelper.Clickjs("Create");
                eqiupment_EquipmentHelper.WaitForWorkAround(5000);

                executionLog.Log("CreateEquipments", "Verify title");
                VerifyTitle("Equipment Create");

                executionLog.Log("CreateEquipments", "Enter Equipment Name");
                eqiupment_EquipmentHelper.TypeText("Name", name);
                eqiupment_EquipmentHelper.WaitForWorkAround(500);

                executionLog.Log("CreateEquipments", "Select DownloadsIDName");
                eqiupment_EquipmentHelper.Select("Type", "Check Reader");
                eqiupment_EquipmentHelper.WaitForWorkAround(500);

                executionLog.Log("CreateEquipments", "Enter Equipment Id");
                eqiupment_EquipmentHelper.TypeText("EquipmentId", Id);
                eqiupment_EquipmentHelper.WaitForWorkAround(500);

                executionLog.Log("CreateEquipments", "Enter Version");
                eqiupment_EquipmentHelper.TypeText("Version", "5.1");
                eqiupment_EquipmentHelper.WaitForWorkAround(500);

                executionLog.Log("CreateEquipments", "Enter Description");
                eqiupment_EquipmentHelper.TypeText("Description", "This is Testing Description");
                eqiupment_EquipmentHelper.WaitForWorkAround(2000);

                executionLog.Log("CreateEquipments", "Click On First CheckBox");
                eqiupment_EquipmentHelper.ClickElement("ApplicableProcessors1");
                eqiupment_EquipmentHelper.WaitForWorkAround(2000);

                executionLog.Log("CreateEquipments", "Click On First CheckBox");
                eqiupment_EquipmentHelper.ClickElement("ApplicableProcessors2");
                eqiupment_EquipmentHelper.WaitForWorkAround(2000);

                executionLog.Log("CreateEquipments", " Click on Save button ");
                eqiupment_EquipmentHelper.Clickjs("Save");
                eqiupment_EquipmentHelper.WaitForWorkAround(5000);

                executionLog.Log("CreateEquipments", " Wait for save equipment text ");
                eqiupment_EquipmentHelper.WaitForText("Equipment saved successfully", 20);

                executionLog.Log("CreateEquipments", "Redirect To URL");
                VisitOffice("equipment");
                eqiupment_EquipmentHelper.WaitForWorkAround(6000);

                executionLog.Log("CreateEquipments", "Verify title");
                VerifyTitle("Equipment");
                eqiupment_EquipmentHelper.WaitForWorkAround(4000);

                executionLog.Log("CreateEquipments", "Enter Name to search");
                eqiupment_EquipmentHelper.TypeText("SearchId", Id);
                eqiupment_EquipmentHelper.WaitForWorkAround(5000);

                executionLog.Log("CreateEquipments", "cLICK Delete btn  ");
                eqiupment_EquipmentHelper.Clickjs("DeleteEuipment");
                eqiupment_EquipmentHelper.WaitForWorkAround(3000);

                executionLog.Log("CreateEquipments", "Accept alert message. ");
                eqiupment_EquipmentHelper.AcceptAlert();
                eqiupment_EquipmentHelper.WaitForWorkAround(3000);

                executionLog.Log("CreateEquipments", "Wait for delete message. ");
                eqiupment_EquipmentHelper.WaitForText("Equipment deleted successfully", 30);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CreateEquipments");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Create Equipments");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Create Equipments", "Bug", "Medium", "Create Eqiupment page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Create Equipments");
                        TakeScreenshot("CreateEquipments");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateEquipments.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CreateEquipments");
                        string id = loginHelper.getIssueID("Create Equipments");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateEquipments.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Create Equipments"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Create Equipments");
           //     executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("CreateEquipments");
                executionLog.WriteInExcel("Create Equipments", Status, JIRA, "Equipment Management");
            }
        }
    }
}