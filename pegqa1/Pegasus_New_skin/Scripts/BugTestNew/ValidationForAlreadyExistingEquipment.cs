using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class ValidationForAlreadyExistingEquipment : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("Fail")]
        [TestCategory("TS3")]
        [TestCategory("BugTestNew")]
        public void validationForAlreadyExistingEquipment()
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

            // Random Variables
            var IdNum = "5" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";
            try
            {
                executionLog.Log("ValidationForAlreadyExistingEquipment", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("ValidationForAlreadyExistingEquipment", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("ValidationForAlreadyExistingEquipment", "Redirect at equipment page.");
                VisitOffice("equipment");
                eqiupment_EquipmentHelper.WaitForWorkAround(5000);

                var loc = "//table[@id='list1']/tbody/tr[2]/td[4]/a[text()='Test246']";
                if (eqiupment_EquipmentHelper.IsElementPresent(loc))
                {
                    executionLog.Log("ValidationForAlreadyExistingEquipment", "Click on Edit equipment");
                    eqiupment_EquipmentHelper.ClickElement("EditEquipment3");

                    executionLog.Log("ValidationForAlreadyExistingEquipment", "Enter equipment name.");
                    eqiupment_EquipmentHelper.TypeText("Name", "Test246");

                    executionLog.Log("ValidationForAlreadyExistingEquipment", "Click on Save button.");
                    eqiupment_EquipmentHelper.ClickElement("Save");

                    executionLog.Log("ValidationForAlreadyExistingEquipment", "Wait for exisiting equipment alert.");
                    eqiupment_EquipmentHelper.WaitForText("The Equipment name is already exist.", 10);
                }
                else
                {
                    executionLog.Log("EditEquipment", " Click On Create");
                    eqiupment_EquipmentHelper.ClickElement("Create");

                    executionLog.Log("EditEquipment", "Verify title");
                    VerifyTitle("Equipment Create");

                    var name = "Equipment" + RandomNumber(11, 999);
                    executionLog.Log("EditEquipment", "Enter Equipment Name");
                    eqiupment_EquipmentHelper.TypeText("Name", name);

                    executionLog.Log("EditEquipment", "Enter DownloadsIDName");
                    eqiupment_EquipmentHelper.Select("Type", "Check Reader");

                    executionLog.Log("EditEquipment", "Enter Equipment Id");
                    eqiupment_EquipmentHelper.TypeText("EquipmentId", IdNum);

                    executionLog.Log("EditEquipment", "Enter Version");
                    eqiupment_EquipmentHelper.TypeText("Version", "Testing");

                    executionLog.Log("EditEquipment", "Enter Description");
                    eqiupment_EquipmentHelper.TypeText("Description", "This is Testing Description");

                    executionLog.Log("EditEquipment", "Click On First CheckBox");
                    eqiupment_EquipmentHelper.ClickElement("ApplicableProcessors1");

                    executionLog.Log("EditEquipment", "Click On First CheckBox");
                    eqiupment_EquipmentHelper.ClickElement("ApplicableProcessors2");

                    executionLog.Log("EditEquipment", " Click on Save button ");
                    eqiupment_EquipmentHelper.ClickElement("Save");

                    executionLog.Log("EditEquipment", "Wait for save message.");
                    eqiupment_EquipmentHelper.WaitForText("Equipment saved successfully", 30);

                    executionLog.Log("EditEquipment", "Enter Name in seacrh field");
                    eqiupment_EquipmentHelper.TypeText("EnterName", name);
                    eqiupment_EquipmentHelper.WaitForWorkAround(4000);

                    executionLog.Log("ValidationForAlreadyExistingEquipment", "Click on Edit equipment");
                    eqiupment_EquipmentHelper.ClickElement("EditEquipment");

                    executionLog.Log("ValidationForAlreadyExistingEquipment", "Enter equipment name.");
                    eqiupment_EquipmentHelper.TypeText("Name", "Test246");

                    executionLog.Log("ValidationForAlreadyExistingEquipment", "Click on Save button.");
                    eqiupment_EquipmentHelper.ClickElement("Save");

                    executionLog.Log("ValidationForAlreadyExistingEquipment", "Wait for exisiting equipment alert.");
                    eqiupment_EquipmentHelper.WaitForText("The Equipment name is already exist.", 10);

                }

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("ValidationForAlreadyExistingEquipment");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Validation For Already Existing Equipment");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Validation For Already Existing Equipment", "Bug", "Medium", "Equipment page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Validation For Already Existing Equipment");
                        TakeScreenshot("ValidationForAlreadyExistingEquipment");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ValidationForAlreadyExistingEquipment.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("ValidationForAlreadyExistingEquipment");
                        string id = loginHelper.getIssueID("Validation For Already Existing Equipment");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ValidationForAlreadyExistingEquipment.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Validation For Already Existing Equipment"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Validation For Already Existing Equipment");
            //    executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("ValidationForAlreadyExistingEquipment");
                executionLog.WriteInExcel("Validation For Already Existing Equipment", Status, JIRA, "Admin Equipments");
            }
        }
    }
}