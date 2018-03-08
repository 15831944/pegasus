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
    public class VendorCreatedModifiedIssue : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin")]
        [TestCategory("TS3")]
        [TestCategory("BugTestNew")]
        public void vendorCreatedModifiedIssue()
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
                executionLog.Log("VendorCreatedModifiedIssue", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VendorCreatedModifiedIssue", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("VendorCreatedModifiedIssue", "Redirect To Vendors");
                VisitOffice("vendors");
                equipment_VendorsHelper.WaitForWorkAround(3000);

                executionLog.Log("VendorCreatedModifiedIssue", "Verify title");
                VerifyTitle("Vendors");

                executionLog.Log("VendorCreatedModifiedIssue", " Click On Create");
                equipment_VendorsHelper.ClickElement("Create");
                equipment_VendorsHelper.WaitForWorkAround(3000);

                executionLog.Log("VendorCreatedModifiedIssue", "Verify title");
                VerifyTitle("Create a New Vendor");

                executionLog.Log("VendorCreatedModifiedIssue", "Select Type");
                equipment_VendorsHelper.Select("Type", "Online");

                executionLog.Log("VendorCreatedModifiedIssue", "Enter vendor name");
                equipment_VendorsHelper.TypeText("Name", name);

                executionLog.Log("VendorCreatedModifiedIssue", "Enter DBA name");
                equipment_VendorsHelper.TypeText("DBAName", "Test123");

                executionLog.Log("VendorCreatedModifiedIssue", "Select Salutation");
                equipment_VendorsHelper.Select("Salutation", "Mr");

                executionLog.Log("VendorCreatedModifiedIssue", "Enter First Name");
                equipment_VendorsHelper.TypeText("FirstName", "Test");

                executionLog.Log("VendorCreatedModifiedIssue", "Enter LatName");
                equipment_VendorsHelper.TypeText("LastName", "Vendor");

                executionLog.Log("VendorCreatedModifiedIssue", "Select eAddress Type");
                equipment_VendorsHelper.Select("eAddessType", "E-Mail");

                executionLog.Log("VendorCreatedModifiedIssue", "Select EAddress Label");
                equipment_VendorsHelper.Select("EAddressLabel", "Work");

                executionLog.Log("VendorCreatedModifiedIssue", "Enter E Address");
                equipment_VendorsHelper.TypeText("eAddress", "Test@yopmail.com");

                executionLog.Log("VendorCreatedModifiedIssue", "Select Phone Type");
                equipment_VendorsHelper.Select("PhoneType", "Work");

                executionLog.Log("VendorCreatedModifiedIssue", "Enter Zip Code");
                equipment_VendorsHelper.TypeText("ZipCodeVendor", "60601");
                equipment_VendorsHelper.WaitForWorkAround(3000);

                executionLog.Log("VendorCreatedModifiedIssue", " Click on Save button");
                equipment_VendorsHelper.ClickElement("Save");

                executionLog.Log("VendorCreatedModifiedIssue", "Wait for text");
                equipment_VendorsHelper.WaitForText("Vendor created successfully", 10);

                executionLog.Log("VendorCreatedModifiedIssue", "Redirect To Vendors");
                VisitOffice("vendors");
                equipment_VendorsHelper.WaitForWorkAround(3000);

                executionLog.Log("VendorCreatedModifiedIssue", "Verify title");
                VerifyTitle("Vendors");

                executionLog.Log("VendorCreatedModifiedIssue", "Enter Name to search");
                equipment_VendorsHelper.TypeText("VendorName", name);
                equipment_VendorsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyEquipmentCreatedAndByModifiedByCredits", "Click on any vendor");
                equipment_VendorsHelper.ClickElement("ClickOnVender");

                executionLog.Log("VerifyEquipmentCreatedAndByModifiedByCredits", "Verify vendors created by credits.");
                equipment_VendorsHelper.VerifyText("CreatedBy", "Howard Tang");

                executionLog.Log("VerifyEquipmentCreatedAndByModifiedByCredits", "Verify vendors modified by credits.");
                equipment_VendorsHelper.VerifyText("ModifiedBy", "Howard Tang");

                executionLog.Log("VendorCreatedModifiedIssue", "Redirect To Vendors");
                VisitOffice("vendors");
                equipment_VendorsHelper.WaitForWorkAround(3000);

                executionLog.Log("VendorCreatedModifiedIssue", "Verify title");
                VerifyTitle("Vendors");

                executionLog.Log("VendorCreatedModifiedIssue", "Enter Name to search");
                equipment_VendorsHelper.TypeText("VendorName", name);
                equipment_VendorsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyEquipmentCreatedAndByModifiedByCredits", "Click on edit button.");
                equipment_VendorsHelper.ClickElement("Edit");

                executionLog.Log("VerifyEquipmentCreatedAndByModifiedByCredits", "Verify title");
                VerifyTitle("Vendors");

                executionLog.Log("VendorCreatedModifiedIssue", "Enter First Name");
                equipment_VendorsHelper.TypeText("FirstName", "Test");

                executionLog.Log("VendorCreatedModifiedIssue", "Enter LatName");
                equipment_VendorsHelper.TypeText("LastName", "Vendor");

                executionLog.Log("VendorCreatedModifiedIssue", "Select eAddress Type");
                equipment_VendorsHelper.Select("eAddessType", "E-Mail");

                executionLog.Log("VendorCreatedModifiedIssue", "Select EAddress Label");
                equipment_VendorsHelper.Select("EAddressLabel", "Work");

                executionLog.Log("VendorCreatedModifiedIssue", "Enter E Address");
                equipment_VendorsHelper.TypeText("eAddress", "Test@yopmail.com");

                executionLog.Log("VendorCreatedModifiedIssue", "Select Phone Type");
                equipment_VendorsHelper.Select("PhoneType", "Work");

                executionLog.Log("VendorCreatedModifiedIssue", " Click on Save button   ");
                equipment_VendorsHelper.ClickElement("Save");

                executionLog.Log("VendorCreatedModifiedIssue", "Wait for updation success text.");
                equipment_VendorsHelper.WaitForText("Vendor is successfully updated", 30);

                executionLog.Log("VendorCreatedModifiedIssue", "Redirect To Vendors");
                VisitOffice("vendors");
                equipment_VendorsHelper.WaitForWorkAround(3000);

                executionLog.Log("VendorCreatedModifiedIssue", "Verify title");
                VerifyTitle("Vendors");

                executionLog.Log("VendorCreatedModifiedIssue", "Enter Name to search");
                equipment_VendorsHelper.TypeText("VendorName", name);
                equipment_VendorsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyEquipmentCreatedAndByModifiedByCredits", "Click on any vendors");
                equipment_VendorsHelper.ClickElement("ClickOnVender");

                executionLog.Log("VerifyEquipmentCreatedAndByModifiedByCredits", "Verify vendors created by credits.");
                equipment_VendorsHelper.VerifyText("CreatedBy", "Howard Tang");

                executionLog.Log("VerifyEquipmentCreatedAndByModifiedByCredits", "Verify vendors modified by credits.");
                equipment_VendorsHelper.VerifyText("ModifiedBy", "Howard Tang");

                executionLog.Log("VendorCreatedModifiedIssue", "Redirect To Vendors");
                VisitOffice("vendors");
                equipment_VendorsHelper.WaitForWorkAround(3000);

                executionLog.Log("VendorCreatedModifiedIssue", "Verify title");
                VerifyTitle("Vendors");

                executionLog.Log("VendorCreatedModifiedIssue", "Enter Name to search");
                equipment_VendorsHelper.TypeText("VendorName", name);
                equipment_VendorsHelper.WaitForWorkAround(2000);

                executionLog.Log("VendorCreatedModifiedIssue", "Click Delete btn  ");
                equipment_VendorsHelper.ClickElement("Delete");

                executionLog.Log("VendorCreatedModifiedIssue", "Accept alert message. ");
                equipment_VendorsHelper.AcceptAlert();

                executionLog.Log("VendorCreatedModifiedIssue", "Wait for delete message. ");
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
                bool result = loginHelper.CheckExstingIssue("Vendor Created Modified Issue");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Vendor Created Modified Issue", "Bug", "Medium", "Create Eqiupment page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Vendor Created Modified Issue");
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
                        string id = loginHelper.getIssueID("Vendor Created Modified Issue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyEquipmentCreatedAndByModifiedByCredits.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Vendor Created Modified Issue"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Vendor Created Modified Issue");
              //  executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("VerifyEquipmentCreatedAndByModifiedByCredits");
                executionLog.WriteInExcel("Vendor Created Modified Issue", Status, JIRA, "Equipment Management");
            }
        }
    }
}