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
    public class VerifyEquipmentCreatedAndByModifiedByCredits : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin1")]
        [TestCategory("TS7")]
        [TestCategory("Pegasus_Admin1")]
        public void verifyEquipmentCreatedAndByModifiedByCredits()
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
                executionLog.Log("VerifyEquipmentCreatedAndByModifiedByCredits", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyEquipmentCreatedAndByModifiedByCredits", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("VerifyEquipmentCreatedAndByModifiedByCredits", "Redirect To URL");
                VisitOffice("equipment/create");
                eqiupment_EquipmentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyEquipmentCreatedAndByModifiedByCredits", "Verify title");
                VerifyTitle("Equipment Create");

                executionLog.Log("VerifyEquipmentCreatedAndByModifiedByCredits", "Enter Equipment Name");
                eqiupment_EquipmentHelper.TypeText("Name", name);

                executionLog.Log("VerifyEquipmentCreatedAndByModifiedByCredits", "Select DownloadsIDName");
                eqiupment_EquipmentHelper.Select("Type", "Check Reader");

                executionLog.Log("VerifyEquipmentCreatedAndByModifiedByCredits", "Enter Equipment Id");
                eqiupment_EquipmentHelper.TypeText("EquipmentId", Id);

                executionLog.Log("VerifyEquipmentCreatedAndByModifiedByCredits", "Enter Version");
                eqiupment_EquipmentHelper.TypeText("Version", "Testing");

                executionLog.Log("VerifyEquipmentCreatedAndByModifiedByCredits", "Enter Description");
                eqiupment_EquipmentHelper.TypeText("Description", "This is Testing Description");

                executionLog.Log("VerifyEquipmentCreatedAndByModifiedByCredits", "Click On First CheckBox");
                eqiupment_EquipmentHelper.ClickElement("ApplicableProcessors1");

                executionLog.Log("VerifyEquipmentCreatedAndByModifiedByCredits", "Click On First CheckBox");
                eqiupment_EquipmentHelper.ClickElement("ApplicableProcessors2");

                executionLog.Log("VerifyEquipmentCreatedAndByModifiedByCredits", " Click on Save button ");
                eqiupment_EquipmentHelper.ClickElement("Save");
                eqiupment_EquipmentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyEquipmentCreatedAndByModifiedByCredits", " Redirect at equipments.");
                VisitOffice("equipment");
                eqiupment_EquipmentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyEquipmentCreatedAndByModifiedByCredits", " Verify page title as equipments.");
                VerifyTitle("Equipment");

                executionLog.Log("VerifyEquipmentCreatedAndByModifiedByCredits", "Enter Name in seacrh field");
                eqiupment_EquipmentHelper.TypeText("EnterName", name);
                eqiupment_EquipmentHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyEquipmentCreatedAndByModifiedByCredits", "Click on any equipment");
                eqiupment_EquipmentHelper.ClickElement("FirstEquip");
                eqiupment_EquipmentHelper.WaitForWorkAround(4000);

                executionLog.Log("VerifyEquipmentCreatedAndByModifiedByCredits", "Verify equipment created by credits.");
                eqiupment_EquipmentHelper.VerifyText("CreatedBy", "Howard Tang");

                executionLog.Log("VerifyEquipmentCreatedAndByModifiedByCredits", "Verify equipment created by credits.");
                eqiupment_EquipmentHelper.VerifyText("ModifiedBy", "Howard Tang");

                executionLog.Log("VerifyEquipmentCreatedAndByModifiedByCredits", "Redirect at eqipments page.");
                VisitOffice("equipment");
                eqiupment_EquipmentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyEquipmentCreatedAndByModifiedByCredits", "Verify page title as equipments.");
                VerifyTitle("Equipment");

                executionLog.Log("VerifyEquipmentCreatedAndByModifiedByCredits", "Enter Name in seacrh field");
                eqiupment_EquipmentHelper.TypeText("EnterName", name);
                eqiupment_EquipmentHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyEquipmentCreatedAndByModifiedByCredits", "Click on edit button.");
                eqiupment_EquipmentHelper.ClickElement("EditEquipment");
                eqiupment_EquipmentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyEquipmentCreatedAndByModifiedByCredits", "Verify title");
                VerifyTitle("Equipment Edit:");

                executionLog.Log("VerifyEquipmentCreatedAndByModifiedByCredits", "Enter Equipment Name");
                eqiupment_EquipmentHelper.TypeText("Name", name);

                executionLog.Log("VerifyEquipmentCreatedAndByModifiedByCredits", "Enter DownloadsIDName");
                eqiupment_EquipmentHelper.Select("Type", "Check Reader");

                executionLog.Log("VerifyEquipmentCreatedAndByModifiedByCredits", "Enter Equipment IdNum");
                eqiupment_EquipmentHelper.TypeText("EquipmentId", Id);

                executionLog.Log("VerifyEquipmentCreatedAndByModifiedByCredits", "Enter Version");
                eqiupment_EquipmentHelper.TypeText("Version", "Testing");

                executionLog.Log("VerifyEquipmentCreatedAndByModifiedByCredits", "Enter Description");
                eqiupment_EquipmentHelper.TypeText("Description", "This is Testing Description");

                executionLog.Log("VerifyEquipmentCreatedAndByModifiedByCredits", " Click on Save button");
                eqiupment_EquipmentHelper.ClickElement("Save");

                executionLog.Log("VerifyEquipmentCreatedAndByModifiedByCredits", " Wait for edit message.");
                eqiupment_EquipmentHelper.WaitForText("The Equipment is edited successfully", 10);

                executionLog.Log("VerifyEquipmentCreatedAndByModifiedByCredits", " Redirect at equipments.");
                VisitOffice("equipment");
                eqiupment_EquipmentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyEquipmentCreatedAndByModifiedByCredits", " Verify page title as equipments.");
                VerifyTitle("Equipment");

                executionLog.Log("VerifyEquipmentCreatedAndByModifiedByCredits", "Enter Name in seacrh field");
                eqiupment_EquipmentHelper.TypeText("EnterName", name);
                eqiupment_EquipmentHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyEquipmentCreatedAndByModifiedByCredits", "Click on any equipment");
                eqiupment_EquipmentHelper.ClickElement("FirstEquip");
                eqiupment_EquipmentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyEquipmentCreatedAndByModifiedByCredits", "Verify equipment created by credits.");
                eqiupment_EquipmentHelper.VerifyText("CreatedBy", "Howard Tang");

                executionLog.Log("VerifyEquipmentCreatedAndByModifiedByCredits", "Verify equipment created by credits.");
                eqiupment_EquipmentHelper.VerifyText("ModifiedBy", "Howard Tang");

                executionLog.Log("VerifyEquipmentCreatedAndByModifiedByCredits", "Redirect To URL");
                VisitOffice("equipment");
                eqiupment_EquipmentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyEquipmentCreatedAndByModifiedByCredits", "Verify title");
                VerifyTitle("Equipment");

                executionLog.Log("VerifyEquipmentCreatedAndByModifiedByCredits", "Enter Name to search");
                eqiupment_EquipmentHelper.TypeText("SearchId", Id);
                eqiupment_EquipmentHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyEquipmentCreatedAndByModifiedByCredits", "cLICK Delete btn  ");
                eqiupment_EquipmentHelper.ClickElement("DeleteEuipment");

                executionLog.Log("VerifyEquipmentCreatedAndByModifiedByCredits", "Accept alert message. ");
                eqiupment_EquipmentHelper.AcceptAlert();

                executionLog.Log("VerifyEquipmentCreatedAndByModifiedByCredits", "Wait for delete message. ");
                eqiupment_EquipmentHelper.WaitForText("Equipment deleted successfully", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyEquipmentCreatedAndByModifiedByCredits");
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
                        TakeScreenshot("VerifyEquipmentCreatedAndByModifiedByCredits");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyEquipmentCreatedAndByModifiedByCredits.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyEquipmentCreatedAndByModifiedByCredits");
                        string id = loginHelper.getIssueID("Create Equipments");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyEquipmentCreatedAndByModifiedByCredits.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Create Equipments"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Create Equipments");
                //   executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("VerifyEquipmentCreatedAndByModifiedByCredits");
                executionLog.WriteInExcel("Create Equipments", Status, JIRA, "Equipment Management");
            }
        }
    }
}