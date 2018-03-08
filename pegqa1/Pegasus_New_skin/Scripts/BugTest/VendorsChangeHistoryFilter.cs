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
    public class VendorsChangeHistoryFilter : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS1")]
        [TestCategory("BugTest")]
        public void vendorsChangeHistoryFilter()
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
            var equipment_VendorsHelper = new Equipment_VendorsHelper(GetWebDriver());

            // Variable 
            String name = "Test" + GetRandomNumber();
            String Id = "12345" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";


            try
            {
                executionLog.Log("VendorsChangeHistoryFilter", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VendorsChangeHistoryFilter", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VendorsChangeHistoryFilter", "Redirecte to admin");
                VisitOffice("admin");

                executionLog.Log("VendorsChangeHistoryFilter", "Redirect To URL");
                VisitOffice("vendors");

                executionLog.Log("VendorsChangeHistoryFilter", "Wait for element to present.");
                equipment_VendorsHelper.WaitForElementPresent("ClicOnAnyVendorEquipm", 05);

                executionLog.Log("VendorsChangeHistoryFilter", "Create Vendor");
                equipment_VendorsHelper.ClickElement("Create");
                equipment_VendorsHelper.WaitForWorkAround(4000);

                executionLog.Log("VendorsChangeHistoryFilter", "Select Vendor Type");
                equipment_VendorsHelper.Select("Type", "Online");

                executionLog.Log("VendorsChangeHistoryFilter", "Entwer vendor name.");
                var VendorName = "" + "History Vendor" + GetRandomNumber();
                equipment_VendorsHelper.TypeText("Name", VendorName);

                executionLog.Log("VendorsChangeHistoryFilter", "Enter First Name");
                equipment_VendorsHelper.TypeText("FirstName", "Vendor First Name");

                executionLog.Log("VendorsChangeHistoryFilter", "Enter Last Name");
                equipment_VendorsHelper.TypeText("LastName", "Vendor First Name");

                executionLog.Log("VendorsChangeHistoryFilter", "Select Vendor eAddress Type");
                equipment_VendorsHelper.Select("eAddessType", "E-Mail");

                executionLog.Log("VendorsChangeHistoryFilter", "Enter eAddress Vendor Type");
                var email = "Testemail" + GetRandomNumber() + "@yopmail.com";
                equipment_VendorsHelper.TypeText("eAddress", email);

                executionLog.Log("VendorsChangeHistoryFilter", "Click on save button.");
                equipment_VendorsHelper.ClickElement("Save");

                executionLog.Log("VendorsChangeHistoryFilter", "Wait for success message.");
                equipment_VendorsHelper.WaitForText("Vendor created successfully", 6);

                executionLog.Log("VendorsChangeHistoryFilter", "Search Vendor");
                equipment_VendorsHelper.TypeText("VendorName", VendorName);
                equipment_VendorsHelper.WaitForWorkAround(3000);

                executionLog.Log("VendorsChangeHistoryFilter", "Click on vendor");
                equipment_VendorsHelper.ClickElement("ClickOnVender");

                executionLog.Log("VendorsChangeHistoryFilter", "Wait for element to present");
                equipment_VendorsHelper.WaitForElementPresent("SearchIdChangeHistory", 5);

                executionLog.Log("VendorsChangeHistoryFilter", "Scroll to element.");
                equipment_VendorsHelper.scrollToElement("SearchIdChangeHistory");

                executionLog.Log("VendorsChangeHistoryFilter", "Search Vendor id");
                equipment_VendorsHelper.TypeText("SearchIdChangeHistory", "id");
                equipment_VendorsHelper.WaitForWorkAround(5000);

                executionLog.Log("VendorsChangeHistoryFilter", "Verify text on page");
                equipment_VendorsHelper.VerifyText("VerfyIdHistory", "id");
                equipment_VendorsHelper.WaitForWorkAround(5000);

                executionLog.Log("VendorsChangeHistoryFilter", "Redirect To URL");
                VisitOffice("vendors");

                executionLog.Log("VendorsChangeHistoryFilter", "Wait for element to present.");
                equipment_VendorsHelper.WaitForElementPresent("VendorName", 10);

                executionLog.Log("VendorsChangeHistoryFilter", "Search Vendor by name");
                equipment_VendorsHelper.TypeText("VendorName", VendorName);
                equipment_VendorsHelper.WaitForWorkAround(3000);

                executionLog.Log("VendorsChangeHistoryFilter", "Click on delete icon");
                equipment_VendorsHelper.ClickElement("DeleteVendor");
                equipment_VendorsHelper.WaitForWorkAround(3000);

                executionLog.Log("VendorsChangeHistoryFilter", "Accept alert message");
                equipment_VendorsHelper.AcceptAlert();

                executionLog.Log("VendorsChangeHistoryFilter", "Wait for delete message.");
                equipment_VendorsHelper.WaitForText("Vendor Deleted Successfully", 06);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VendorsChangeHistoryFilter");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Vendors Change History Filter");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Vendors Change History Filter", "Bug", "Medium", "Equipment page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Bulk Update Equipment Status");
                        TakeScreenshot("VendorsChangeHistoryFilter");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VendorsChangeHistoryFilter.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VendorsChangeHistoryFilter");
                        string id = loginHelper.getIssueID("Vendors Change History Filter");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VendorsChangeHistoryFilter.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Vendors Change History Filter"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Vendors Change History Filter");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VendorsChangeHistoryFilter");
                executionLog.WriteInExcel("Vendors Change History Filter", Status, JIRA, "Office Equipment");
            }
        }
    }
}