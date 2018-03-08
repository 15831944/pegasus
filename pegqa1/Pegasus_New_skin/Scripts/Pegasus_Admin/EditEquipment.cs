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
    public class EditEquipment : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin")]
        [TestCategory("TS6")]
        [TestCategory("Pegasus_Admin")]
        public void editEquipment()
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
            var IdNum = "5" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("EditEquipment", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("EditEquipment", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("EditEquipment", "Click On  Admin");
                VisitOffice("admin");

                executionLog.Log("EditEquipment", "Redirect To URL");
                VisitOffice("equipment");

                executionLog.Log("EditEquipment", "Verify title");
                VerifyTitle("Equipment");

                executionLog.Log("EditEquipment", " Click On Create");
                eqiupment_EquipmentHelper.ClickElement("Create");

                executionLog.Log("EditEquipment", "Verify title");
                VerifyTitle("Equipment Create");

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

                executionLog.Log("EditEquipment", "Verify title");
                VerifyTitle("Equipment");

                executionLog.Log("EditEquipment", "Enter Name in seacrh field");
                eqiupment_EquipmentHelper.TypeText("EnterName", name);
                eqiupment_EquipmentHelper.WaitForWorkAround(1000);

                executionLog.Log("EditEquipment", "Clik To EditEquipment");
                eqiupment_EquipmentHelper.ClickElement("EditEquipment");

                executionLog.Log("EditEquipment", "Verify title");
                VerifyTitle("Equipment Edit:");

                executionLog.Log("EditEquipment", "Enter Equipment Name");
                eqiupment_EquipmentHelper.TypeText("Name", name);

                executionLog.Log("EditEquipment", "Enter DownloadsIDName");
                eqiupment_EquipmentHelper.Select("Type", "Check Reader");

                executionLog.Log("EditEquipment", "Enter Equipment IdNum");
                eqiupment_EquipmentHelper.TypeText("EquipmentId", IdNum);

                executionLog.Log("EditEquipment", "Enter Version");
                eqiupment_EquipmentHelper.TypeText("Version", "Testing");

                executionLog.Log("EditEquipment", "Enter Description");
                eqiupment_EquipmentHelper.TypeText("Description", "This is Testing Description");

                executionLog.Log("EditEquipment", " Click on Save button");
                eqiupment_EquipmentHelper.ClickElement("Save");

                executionLog.Log("EditEquipment", " Wait for edit message.");
                eqiupment_EquipmentHelper.WaitForText("The Equipment is edited successfully", 10);

                executionLog.Log("EditEquipment", "Enter Name to search");
                eqiupment_EquipmentHelper.TypeText("EnterName", name);
                eqiupment_EquipmentHelper.WaitForWorkAround(2000);

                executionLog.Log("EditEquipment", "cLICK Delete btn  ");
                eqiupment_EquipmentHelper.ClickElement("DeleteEuipment");

                executionLog.Log("EditEquipment", "Accept alert message. ");
                eqiupment_EquipmentHelper.AcceptAlert();

                executionLog.Log("EditEquipment", "Wait for delete message. ");
                eqiupment_EquipmentHelper.WaitForText("Equipment deleted successfully", 10);


            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("EditEquipment");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Edit Equipment");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Edit Equipment", "Bug", "Medium", "Equipment page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Edit Equipment");
                        TakeScreenshot("EditEquipment");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditEquipment.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("EditEquipment");
                        string id = loginHelper.getIssueID("Edit Equipment");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditEquipment.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Edit Equipment"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Edit Equipment");
              //  executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("EditEquipment");
                executionLog.WriteInExcel("Edit Equipment", Status, JIRA, "Equipment Management");
            }
        }
    }
}
