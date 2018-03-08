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
    public class EquepmentDeleteError : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS4")]
        [TestCategory("NewSkinTaskJira")]
        public void equepmentDeleteError()
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
            String Status = "Pass";
            String JIRA = "";
            var eqname = "testequip" + RandomNumber(11,99999);
            try
            {
                executionLog.Log("EquepmentDeleteError", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("EquepmentDeleteError", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("EquepmentDeleteError", "Redirect Create Equipment page");
                VisitOffice("equipment/create");
                eqiupment_EquipmentHelper.WaitForWorkAround(3000);

                executionLog.Log("EquepmentDeleteError", "Enter Name");
                eqiupment_EquipmentHelper.TypeText("Name", eqname);

                executionLog.Log("EquepmentDeleteError", "Select Type");
                eqiupment_EquipmentHelper.SelectByText("Type", "Terminal");

                executionLog.Log("EquepmentDeleteError", "Enter Version");
                eqiupment_EquipmentHelper.TypeText("Version", "2");

                executionLog.Log("EquepmentDeleteError", "Select Applicable Processor");
                eqiupment_EquipmentHelper.ClickElement("ApplicableProcessors1");

                executionLog.Log("EquepmentDeleteError", "Click on Save button");
                eqiupment_EquipmentHelper.ClickElement("Save");
                eqiupment_EquipmentHelper.WaitForText("Equipment saved successfully", 10);
                eqiupment_EquipmentHelper.WaitForWorkAround(2000);

                executionLog.Log("EquepmentDeleteError", "Search Equipment by Name");
                eqiupment_EquipmentHelper.TypeText("SearchEquipment", eqname);
                eqiupment_EquipmentHelper.WaitForWorkAround(2000);

                executionLog.Log("EquepmentDeleteError", "Click on checkbox.");
                eqiupment_EquipmentHelper.ClickElement("SectEquip");

                executionLog.Log("EquepmentDeleteError", "Delete second equipment");
                eqiupment_EquipmentHelper.ClickElement("Deleteeqp");
                eqiupment_EquipmentHelper.WaitForWorkAround(2000);

                executionLog.Log("EquepmentDeleteError", "Accept alert");
                eqiupment_EquipmentHelper.AcceptAlert();

                executionLog.Log("EquepmentDeleteError", "navigate to other tab");
                VisitCorp("newthemeoffice");

                executionLog.Log("EquepmentDeleteError", "Redirect To equipment");
                VisitOffice("equipment");

            }
        
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";
                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("EquepmentDeleteError");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Equepment Delete Error");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Equepment Delete Error", "Bug", "Medium", "Equipment page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Equepment Delete Error");
                        TakeScreenshot("EquepmentDeleteError");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EquepmentDeleteError.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("EquepmentDeleteError");
                        string id = loginHelper.getIssueID("Equepment Delete Error");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EquepmentDeleteError.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Equepment Delete Error"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Equepment Delete Error");
           //     executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("EquepmentDeleteError");
                executionLog.WriteInExcel("Equepment Delete Error", Status, JIRA, "Equipment Management");
            }
        }
    }
}