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
    public class VerifyAddAnotherVersionDisplay : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS5")]
        [TestCategory("NewSkinTaskJira")]
        public void verifyAddAnotherVersionDisplay()
        {
            string[] username = null;
            string[] password = null;

            XMLParse oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");
            String JIRA = "";
            String Status = "Pass";

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var eqiupment_EquipmentHelper = new Eqiupment_EquipmentHelper(GetWebDriver());

            // Variable 
            String name = "Test" + RandomNumber(1, 99);
            String Id = "12345" + RandomNumber(1, 99);


            try
            {
                executionLog.Log("VerifyAddAnotherVersionDisplay", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyAddAnotherVersionDisplay", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");


                executionLog.Log("VerifyAddAnotherVersionDisplay", "Click on Profile Icon");
                eqiupment_EquipmentHelper.Clickjs("ProfileIcon");

                executionLog.Log("VerifyAddAnotherVersionDisplay", "Click on Admin Button");
                eqiupment_EquipmentHelper.Clickjs("AdminBtn");
                eqiupment_EquipmentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyAddAnotherVersionDisplay", "Click on Terminal And Equipment Tab");
                eqiupment_EquipmentHelper.MouseOverAndWait("ClickOnEquipmentTab", 3);

                executionLog.Log("VerifyAddAnotherVersionDisplay", "Click on equipmet button");
                eqiupment_EquipmentHelper.Clickjs("EquipmentBtn");
                eqiupment_EquipmentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyAddAnotherVersionDisplay", " Click On Create");
                eqiupment_EquipmentHelper.ClickElement("Create");
                eqiupment_EquipmentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyAddAnotherVersionDisplay", "Click on Add Another");
                eqiupment_EquipmentHelper.ClickElement("ClickAddAnother");

                executionLog.Log("VerifyAddAnotherVersionDisplay", "ClickAddAnother");
                eqiupment_EquipmentHelper.ClickElement("ClickAddAnother");
                eqiupment_EquipmentHelper.WaitForWorkAround(3000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyAddAnotherVersionDisplay");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Add Another Version Display");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Add Another Version Display", "Bug", "Medium", "Equipment page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Add Another Version Display");
                        TakeScreenshot("VerifyAddAnotherVersionDisplay");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyAddAnotherVersionDisplay.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyAddAnotherVersionDisplay");
                        string id = loginHelper.getIssueID("Verify Add Another Version Display");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyAddAnotherVersionDisplay.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Add Another Version Display"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Add Another Version Display");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyAddAnotherVersionDisplay");
                executionLog.WriteInExcel("Verify Add Another Version Display", Status, JIRA, "Equipment Management");
            }
        }
    }
}