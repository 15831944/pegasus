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
    public class VerifyAddBtnOfCustomFieldOnMerchantsTeminalTab : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("BugTestNew")]
        public void verifyAddBtnOfCustomFieldOnMerchantsTeminalTab()
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
            var office_ClientsHelper = new Office_ClientsHelper(GetWebDriver());

            String JIRA = "";
            String Status = "Pass";
            try
            {
                executionLog.Log("VerifyAddBtnOfCustomFieldOnMerchantsTeminalTab", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyAddBtnOfCustomFieldOnMerchantsTeminalTab", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyAddBtnOfCustomFieldOnMerchantsTeminalTab", "Redirect at merchants page.");
                VisitOffice("clients");

                executionLog.Log("VerifyAddBtnOfCustomFieldOnMerchantsTeminalTab", "Click on any Merchant");
                office_ClientsHelper.ClickElement("Client1");
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyAddBtnOfCustomFieldOnMerchantsTeminalTab", "Go to Terminals and Equipment tab");
                office_ClientsHelper.ClickElement("TerminalsandEquipments");
                office_ClientsHelper.WaitForWorkAround(2000);

                if (office_ClientsHelper.IsElementPresent("//div[@id='Equipment Details1infodiv']/div[1]/a/i") == true)
                {
                    executionLog.Log("VerifyAddBtnOfCustomFieldOnMerchantsTeminalTab", "Click on pencil icon");
                    office_ClientsHelper.Click("//div[@id='Equipment Details1infodiv']/div[1]/a/i");
                    office_ClientsHelper.WaitForWorkAround(1000);

                    executionLog.Log("VerifyAddBtnOfCustomFieldOnMerchantsTeminalTab", "Click on Add button");
                    office_ClientsHelper.ClickElement("AddCustomBtn");
                    office_ClientsHelper.WaitForWorkAround(1000);

                    executionLog.Log("VerifyAddBtnOfCustomFieldOnMerchantsTeminalTab", "Verify Create Custom field appeared");
                    office_ClientsHelper.IsElementPresent("//h3[text()='Create New Custom Field']");
                }
                else
                {
                    executionLog.Log("VerifyAddBtnOfCustomFieldOnMerchantsTeminalTab", "Click on Add Equipment button");
                    office_ClientsHelper.ClickElement("AddEquipment");
                    office_ClientsHelper.WaitForWorkAround(2000);

                    executionLog.Log("VerifyAddBtnOfCustomFieldOnMerchantsTeminalTab", "Click on any Equipment");
                    office_ClientsHelper.ClickJS("Equipment1");
                    office_ClientsHelper.WaitForWorkAround(2000);

                    executionLog.Log("VerifyAddBtnOfCustomFieldOnMerchantsTeminalTab", "Click on pencil icon");
                    office_ClientsHelper.Click("//div[@id='Equipment Details1infodiv']/div[1]/a/i");
                    office_ClientsHelper.WaitForWorkAround(1000);

                    executionLog.Log("VerifyAddBtnOfCustomFieldOnMerchantsTeminalTab", "Click on Add button");
                    office_ClientsHelper.ClickJS("AddCustomBtn");
                    office_ClientsHelper.WaitForWorkAround(1000);

                    executionLog.Log("VerifyAddBtnOfCustomFieldOnMerchantsTeminalTab", "Verify Create Custom field appeared");
                    office_ClientsHelper.IsElementPresent("//h3[text()='Create New Custom Field']");
                }
            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyAddBtnOfCustomFieldOnMerchantsTeminalTab");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Add Btn Of Custom Field On Merchants Teminal Tab");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Add Btn Of Custom Field On Merchants Teminal Tab", "Bug", "Medium", "Terminals and Equipment tab", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Add Btn Of Custom Field On Merchants Teminal Tab");
                        TakeScreenshot("VerifyAddBtnOfCustomFieldOnMerchantsTeminalTab");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyAddBtnOfCustomFieldOnMerchantsTeminalTab.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyAddBtnOfCustomFieldOnMerchantsTeminalTab");
                        string id = loginHelper.getIssueID("Verify Add Btn Of Custom Field On Merchants Teminal Tab");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyAddBtnOfCustomFieldOnMerchantsTeminalTab.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Add Btn Of Custom Field On Merchants Teminal Tab"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Add Btn Of Custom Field On Merchants Teminal Tab");
                //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyAddBtnOfCustomFieldOnMerchantsTeminalTab");
                executionLog.WriteInExcel("Verify Add Btn Of Custom Field On Merchants Teminal Tab", Status, JIRA, "Office Merchant");
            }
        }
    }
}