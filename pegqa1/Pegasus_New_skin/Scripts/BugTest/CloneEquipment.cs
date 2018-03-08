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
    public class CloneEquipment : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS1")]
        [TestCategory("BugTest")]
        public void cloneEquipment()
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
            var Id = "12345" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("CloneEquipment", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("CloneEquipment", "Login with valid username and password");
                VerifyTitle("Dashboard");

                executionLog.Log("CloneEquipment", "Visit equipment page.");
                VisitOffice("equipment");

                executionLog.Log("CloneEquipment", "Verify page title.");
                VerifyTitle("Equipment");

                var Loc = "//table[@id='list1']/tbody/tr[2]";
                if (eqiupment_EquipmentHelper.IsElementPresent(Loc))
                {

                    executionLog.Log("CloneEquipment", "Click On Equipment");
                    eqiupment_EquipmentHelper.ClickElement("ClickOneQUIP");

                    executionLog.Log("CloneEquipment", "Click On Clone");
                    eqiupment_EquipmentHelper.ClickElement("ClickOnClone");
                    eqiupment_EquipmentHelper.WaitForWorkAround(2000);

                    executionLog.Log("CloneEquipment", "Verify Confirmation");
                    eqiupment_EquipmentHelper.VerifyPageText("Equipment is cloned successfully");

                    executionLog.Log("CloneEquipment", "Click on delete Clone");
                    eqiupment_EquipmentHelper.ClickElement("ClickDelClone");

                    executionLog.Log("CloneEquipment", "Accept alert message.");
                    eqiupment_EquipmentHelper.AcceptAlert();

                    executionLog.Log("CloneEquipment", "Wait for delete success message.");
                    eqiupment_EquipmentHelper.WaitForText("Equipment deleted successfully.", 30);

                }
                else
                {

                    executionLog.Log("CloneEquipment", " Click On Create");
                    VisitOffice("equipment/create");
                    eqiupment_EquipmentHelper.WaitForWorkAround(3000);

                    executionLog.Log("CloneEquipment", "Enter Equipment Name");
                    eqiupment_EquipmentHelper.TypeText("Name", "Clone Equipment");

                    executionLog.Log("CloneEquipment", "Select equipment type.");
                    eqiupment_EquipmentHelper.Select("Type", "Check Reader");

                    executionLog.Log("CloneEquipment", "Enter Equipment Id");
                    eqiupment_EquipmentHelper.TypeText("EquipmentId", Id);

                    executionLog.Log("CloneEquipment", "Enter Version");
                    eqiupment_EquipmentHelper.TypeText("Version", "Testing");

                    executionLog.Log("CloneEquipment", "Enter Description");
                    eqiupment_EquipmentHelper.TypeText("Description", "This is Testing Description");

                    executionLog.Log("CloneEquipment", " Click on Save button");
                    eqiupment_EquipmentHelper.ClickElement("Save");
                    eqiupment_EquipmentHelper.WaitForWorkAround(2000);

                    executionLog.Log("CloneEquipment", "Click On Equipment");
                    eqiupment_EquipmentHelper.ClickElement("FirstEquip");

                    executionLog.Log("CloneEquipment", "Click On Clone");
                    eqiupment_EquipmentHelper.ClickElement("ClickOnClone");

                    executionLog.Log("CloneEquipment", "Verify Equipment is cloned successfully");
                    eqiupment_EquipmentHelper.VerifyPageText("Equipment is cloned successfully");
                    eqiupment_EquipmentHelper.VerifyPageText("Clone of Equipment");

                    executionLog.Log("CloneEquipment", "Click on delete clone");
                    eqiupment_EquipmentHelper.ClickElement("ClickDelClone");

                    executionLog.Log("CloneEquipment", "Accept alert message.");
                    eqiupment_EquipmentHelper.AcceptAlert();

                    executionLog.Log("CloneEquipment", "Verify Equipment is deleted successfully");
                    eqiupment_EquipmentHelper.VerifyPageText("Equipment deleted successfully.");
                    eqiupment_EquipmentHelper.WaitForWorkAround(3000);

                }
            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CloneEquipment");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Clone Equipment");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Clone Equipment", "Bug", "Medium", "Equipment page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Clone Equipment");
                        TakeScreenshot("CloneEquipment");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CloneEquipment.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CloneEquipment");
                        string id = loginHelper.getIssueID("Clone Equipment");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CloneEquipment.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Clone Equipment"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Clone Equipment");
                //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("CloneEquipment");
                executionLog.WriteInExcel("Clone Equipment", Status, JIRA, "Admin Equipments");
            }
        }
    }
}