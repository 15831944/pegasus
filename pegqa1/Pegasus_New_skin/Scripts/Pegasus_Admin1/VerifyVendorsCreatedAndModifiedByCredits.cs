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
    public class VerifyVendorsCreatedAndModifiedByCredits : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin1")]
        [TestCategory("TS7")]
        [TestCategory("Pegasus_Admin1")]
        public void verifyVendorsCreatedAndModifiedByCredits()
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
            var equipment_VendorsHelper = new Equipment_VendorsHelper(GetWebDriver());

            // Variable 
            var name = "Test" + GetRandomNumber();
            var Id = "12345" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("VerifyVendorsCreatedAndModifiedByCredits", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyVendorsCreatedAndModifiedByCredits", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("VerifyVendorsCreatedAndModifiedByCredits", "Redirect To Vendors");
                VisitOffice("vendors/create");
                equipment_VendorsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyVendorsCreatedAndModifiedByCredits", "Verify title");
                VerifyTitle("Create a New Vendor");

                executionLog.Log("VerifyVendorsCreatedAndModifiedByCredits", "Select Type");
                equipment_VendorsHelper.Select("Type", "Online");

                executionLog.Log("VerifyVendorsCreatedAndModifiedByCredits", "Enter vendor name");
                equipment_VendorsHelper.TypeText("Name", name);

                executionLog.Log("VerifyVendorsCreatedAndModifiedByCredits", "Enter DBA name");
                equipment_VendorsHelper.TypeText("DBAName", "Test123");

                executionLog.Log("VerifyVendorsCreatedAndModifiedByCredits", "Select Salutation");
                equipment_VendorsHelper.Select("Salutation", "Mr");

                executionLog.Log("VerifyVendorsCreatedAndModifiedByCredits", "Enter First Name");
                equipment_VendorsHelper.TypeText("FirstName", "Test");

                executionLog.Log("VerifyVendorsCreatedAndModifiedByCredits", "Enter LatName");
                equipment_VendorsHelper.TypeText("LastName", "Vendor");

                executionLog.Log("VerifyVendorsCreatedAndModifiedByCredits", "Select eAddress Type");
                equipment_VendorsHelper.Select("eAddessType", "E-Mail");

                executionLog.Log("VerifyVendorsCreatedAndModifiedByCredits", "Select EAddress Label");
                equipment_VendorsHelper.Select("EAddressLabel", "Work");

                executionLog.Log("VerifyVendorsCreatedAndModifiedByCredits", "Enter E Address");
                equipment_VendorsHelper.TypeText("eAddress", "Test@yopmail.com");

                executionLog.Log("VerifyVendorsCreatedAndModifiedByCredits", "Select Phone Type");
                equipment_VendorsHelper.Select("PhoneType", "Work");

                executionLog.Log("VerifyVendorsCreatedAndModifiedByCredits", "Enter Zip Code");
                equipment_VendorsHelper.TypeText("ZipCodeVendor", "60601");
                equipment_VendorsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyVendorsCreatedAndModifiedByCredits", " Click on Save button   ");
                equipment_VendorsHelper.ClickElement("Save");

                executionLog.Log("VerifyVendorsCreatedAndModifiedByCredits", "Wait for text");
                equipment_VendorsHelper.WaitForText("Vendor created successfully", 05);

                executionLog.Log("VerifyVendorsCreatedAndModifiedByCredits", "Redirect To Vendors");
                VisitOffice("vendors");
                equipment_VendorsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyVendorsCreatedAndModifiedByCredits", "Verify title");
                VerifyTitle("Vendors");

                executionLog.Log("VerifyVendorsCreatedAndModifiedByCredits", "Enter Name to search");
                equipment_VendorsHelper.TypeText("VendorName", name);
                equipment_VendorsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyEquipmentCreatedAndByModifiedByCredits", "Click on any vendor");
                equipment_VendorsHelper.ClickElement("ClickOnVender");
                equipment_VendorsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyEquipmentCreatedAndByModifiedByCredits", "Verify vendors created by credits.");
                equipment_VendorsHelper.VerifyText("CreatedBy", "Howard Tang");

                executionLog.Log("VerifyEquipmentCreatedAndByModifiedByCredits", "Verify vendors modified by credits.");
                equipment_VendorsHelper.VerifyText("ModifiedBy", "Howard Tang");

                executionLog.Log("VerifyVendorsCreatedAndModifiedByCredits", "Redirect To Vendors");
                VisitOffice("vendors");
                equipment_VendorsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyVendorsCreatedAndModifiedByCredits", "Verify title");
                VerifyTitle("Vendors");

                executionLog.Log("VerifyVendorsCreatedAndModifiedByCredits", "Enter Name to search");
                equipment_VendorsHelper.TypeText("VendorName", name);
                equipment_VendorsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyEquipmentCreatedAndByModifiedByCredits", "Click on edit button.");
                equipment_VendorsHelper.ClickElement("Edit");
                equipment_VendorsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyEquipmentCreatedAndByModifiedByCredits", "Verify title");
                VerifyTitle("Vendors");

                executionLog.Log("VerifyVendorsCreatedAndModifiedByCredits", "Enter First Name");
                equipment_VendorsHelper.TypeText("FirstName", "Test");

                executionLog.Log("VerifyVendorsCreatedAndModifiedByCredits", "Enter LatName");
                equipment_VendorsHelper.TypeText("LastName", "Vendor");

                executionLog.Log("VerifyVendorsCreatedAndModifiedByCredits", "Select eAddress Type");
                equipment_VendorsHelper.Select("eAddessType", "E-Mail");

                executionLog.Log("VerifyVendorsCreatedAndModifiedByCredits", "Select EAddress Label");
                equipment_VendorsHelper.Select("EAddressLabel", "Work");

                executionLog.Log("VerifyVendorsCreatedAndModifiedByCredits", "Enter E Address");
                equipment_VendorsHelper.TypeText("eAddress", "Test@yopmail.com");

                executionLog.Log("VerifyVendorsCreatedAndModifiedByCredits", "Select Phone Type");
                equipment_VendorsHelper.Select("PhoneType", "Work");

                executionLog.Log("VerifyVendorsCreatedAndModifiedByCredits", " Click on Save button   ");
                equipment_VendorsHelper.ClickElement("Save");

                executionLog.Log("VerifyVendorsCreatedAndModifiedByCredits", "Wait for updation success text.");
                equipment_VendorsHelper.WaitForText("Vendor is successfully updated", 05);

                executionLog.Log("VerifyVendorsCreatedAndModifiedByCredits", "Redirect To Vendors");
                VisitOffice("vendors");
                equipment_VendorsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyVendorsCreatedAndModifiedByCredits", "Verify title");
                VerifyTitle("Vendors");

                executionLog.Log("VerifyVendorsCreatedAndModifiedByCredits", "Enter Name to search");
                equipment_VendorsHelper.TypeText("VendorName", name);
                equipment_VendorsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyEquipmentCreatedAndByModifiedByCredits", "Click on any vendors");
                equipment_VendorsHelper.ClickElement("ClickOnVender");
                equipment_VendorsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyEquipmentCreatedAndByModifiedByCredits", "Verify vendors created by credits.");
                equipment_VendorsHelper.VerifyText("CreatedBy", "Howard Tang");

                executionLog.Log("VerifyEquipmentCreatedAndByModifiedByCredits", "Verify vendors modified by credits.");
                equipment_VendorsHelper.VerifyText("ModifiedBy", "Howard Tang");

                executionLog.Log("VerifyVendorsCreatedAndModifiedByCredits", "Redirect To Vendors");
                VisitOffice("vendors");
                equipment_VendorsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyVendorsCreatedAndModifiedByCredits", "Verify title");
                VerifyTitle("Vendors");

                executionLog.Log("VerifyVendorsCreatedAndModifiedByCredits", "Enter Name to search");
                equipment_VendorsHelper.TypeText("VendorName", name);
                equipment_VendorsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyVendorsCreatedAndModifiedByCredits", "Click Delete btn  ");
                equipment_VendorsHelper.ClickElement("Delete");

                executionLog.Log("VerifyVendorsCreatedAndModifiedByCredits", "Accept alert message. ");
                equipment_VendorsHelper.AcceptAlert();

                executionLog.Log("VerifyVendorsCreatedAndModifiedByCredits", "Wait for delete message. ");
                equipment_VendorsHelper.WaitForText("Vendor Deleted Successfully", 10);

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
                bool result = loginHelper.CheckExstingIssue("Verify Vendors Created And Modified By Credits");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Vendors Created And Modified By Credits", "Bug", "Medium", "Create Eqiupment page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Vendors Created And Modified By Credits");
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
                        string id = loginHelper.getIssueID("Verify Vendors Created And Modified By Credits");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyEquipmentCreatedAndByModifiedByCredits.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Vendors Created And Modified By Credits"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Vendors Created And Modified By Credits");
             //   executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("VerifyEquipmentCreatedAndByModifiedByCredits");
                executionLog.WriteInExcel("Verify Vendors Created And Modified By Credits", Status, JIRA, "Equipment Management");
            }
        }
    }
}