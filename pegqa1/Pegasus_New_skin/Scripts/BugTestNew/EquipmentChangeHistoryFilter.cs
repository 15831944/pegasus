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
    public class EquipmentChangeHistoryFilter : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS2")]
        [TestCategory("BugTestNew")]
        public void equipmentChangeHistoryFilter()
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
            String JIRA = "";
            String Status = "Pass";


            try
            {
                executionLog.Log("EquipmentChangeHistoryFilter", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("EquipmentChangeHistoryFilter", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("EquipmentChangeHistoryFilter", "Redirecte to admin");
                VisitOffice("admin");

                executionLog.Log("EquipmentChangeHistoryFilter", "Redirect at equipment page.");
                VisitOffice("equipment");

                executionLog.Log("EquipmentChangeHistoryFilter", "Click on any equipment.");
                eqiupment_EquipmentHelper.ClickElement("ClickOneQUIP");

                executionLog.Log("EquipmentChangeHistoryFilter", "Wait for change history text box to be present.");
                eqiupment_EquipmentHelper.WaitForElementPresent("ChangeHistory", 06);

                executionLog.Log("EquipmentChangeHistoryFilter", "Scroll to change history field.");
                eqiupment_EquipmentHelper.ScrollToElement("ChangeHistory");

                executionLog.Log("EquipmentChangeHistoryFilter", "Enter id in chngr history.");
                //eqiupment_EquipmentHelper.TypeText("ChangeHistory", "id");

                executionLog.Log("EquipmentChangeHistoryFilter", "Wait for locator to be present.");
                eqiupment_EquipmentHelper.WaitForElementPresent("VerifyID", 06);

                executionLog.Log("EquipmentChangeHistoryFilter", "Verify id on the page.");
                eqiupment_EquipmentHelper.VerifyText("VerifyID", "id");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("EquipmentChangeHistoryFilter");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Equipment Change History Filter");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Equipment Change History Filter", "Bug", "Medium", "Equipment page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Bulk Update Equipment Status");
                        TakeScreenshot("EquipmentChangeHistoryFilter");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EquipmentChangeHistoryFilter.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("EquipmentChangeHistoryFilter");
                        string id = loginHelper.getIssueID("Equipment Change History Filter");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EquipmentChangeHistoryFilter.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Equipment Change History Filter"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Equipment Change History Filter");
                //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("EquipmentChangeHistoryFilter");
                executionLog.WriteInExcel("Equipment Change History Filter", Status, JIRA, "Admin Equipments");
            }
        }
    }
}
