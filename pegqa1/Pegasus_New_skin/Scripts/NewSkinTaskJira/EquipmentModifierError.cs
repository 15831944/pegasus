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
    public class EquipmentModifierError : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS4")]
        [TestCategory("NewSkinTaskJira")]
        public void equipmentModifierError()
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
                executionLog.Log("EquipmentModifierError", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("EquipmentModifierError", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("EquipmentModifierError", "Redirect at Admin page.");
                VisitOffice("admin");

                executionLog.Log("EquipmentModifierError", "Redirect to equipment page.");
                VisitOffice("equipment");

                executionLog.Log("EquipmentModifierError", " Click On Create");
                eqiupment_EquipmentHelper.ClickElement("Create");
                eqiupment_EquipmentHelper.WaitForWorkAround(3000);

                executionLog.Log("EquipmentModifierError", "Click on Save button");
                eqiupment_EquipmentHelper.ClickElement("Save");

                executionLog.Log("EquipmentModifierError", "Verify modifier is not mandatory");
                Assert.IsFalse(eqiupment_EquipmentHelper.IsElementPresent("//*[@id='EquipmentModifier-error']"));
                eqiupment_EquipmentHelper.WaitForWorkAround(3000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";
                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("EquipmentModifierError");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Equipment Modifier Error");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Equipment Modifier Error", "Bug", "Medium", "Equipment page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Equipment Modifier Error");
                        TakeScreenshot("EquipmentModifierError");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EquipmentModifierError.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("EquipmentModifierError");
                        string id = loginHelper.getIssueID("Equipment Modifier Error");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EquipmentModifierError.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Equipment Modifier Error"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Equipment Modifier Error");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("EquipmentModifierError");
                executionLog.WriteInExcel("Equipment Modifier Error", Status, JIRA, "Equipment Management");
            }
        }
    }
}