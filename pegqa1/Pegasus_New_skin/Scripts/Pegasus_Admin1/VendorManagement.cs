using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Admin
{
    [TestClass]
    public class VendorManagement : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin1")]
        [TestCategory("TS7")]
        [TestCategory("Pegasus_Admin1")]
        public void vendorManagement()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var equipment_VendorsHelper = new Equipment_VendorsHelper(GetWebDriver());

            // Random Variables
            var name = "Vendor" + RandomNumber(1, 999);
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("VendorManagement", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VendorManagement", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("VendorManagement", "Redirect To Vendors");
                VisitOffice("vendors/create");
                equipment_VendorsHelper.WaitForWorkAround(3000);

                executionLog.Log("VendorManagement", "Verify title");
                VerifyTitle("Create a New Vendor");

                executionLog.Log("VendorManagement", " Click on Save button");
                equipment_VendorsHelper.ClickElement("Save");
                equipment_VendorsHelper.WaitForWorkAround(1000);

                executionLog.Log("VendorManagement", "Verify validation text on page.");
                equipment_VendorsHelper.VerifyPageText("This field is required.");
                //equipment_VendorsHelper.WaitForWorkAround(3000);

                executionLog.Log("VendorManagement", "Select Type");
                equipment_VendorsHelper.Select("Type", "Online");

                executionLog.Log("VendorManagement", "Enter Name");
                equipment_VendorsHelper.TypeText("Name", name);

                executionLog.Log("VendorManagement", "Enter DBA name");
                equipment_VendorsHelper.TypeText("DBAName", "Test123");

                executionLog.Log("VendorManagement", "Select Salutation");
                equipment_VendorsHelper.Select("Salutation", "Mr");

                executionLog.Log("VendorManagement", "Enter First Name");
                equipment_VendorsHelper.TypeText("FirstName", "Test");

                executionLog.Log("VendorManagement", "Enter LatName");
                equipment_VendorsHelper.TypeText("LastName", "Vendor");

                executionLog.Log("VendorManagement", "Select eAddress Type");
                equipment_VendorsHelper.Select("eAddessType", "E-Mail");

                executionLog.Log("VendorManagement", "Verify EAddress Label");
                equipment_VendorsHelper.VerifyText("EAddressLabel", "Work");

                executionLog.Log("VendorManagement", "Enter E Address");
                equipment_VendorsHelper.TypeText("eAddress", "Test@yopmail.com");

                executionLog.Log("VendorManagement", "Select Phone Type");
                equipment_VendorsHelper.Select("PhoneType", "Work");

                executionLog.Log("VendorManagement", "Enter Zip Code");
                equipment_VendorsHelper.TypeText("ZipCodeVendor", "60601");
                equipment_VendorsHelper.WaitForWorkAround(4000);

                executionLog.Log("VendorManagement", "Verify State");
                equipment_VendorsHelper.VerifyText("VendorState", "IL");

                executionLog.Log("VendorManagement", "Enter website");
                equipment_VendorsHelper.TypeText("Website", "60601");

                executionLog.Log("VendorManagement", "Enter linkedin url");
                equipment_VendorsHelper.TypeText("LinkedURL", "60601");

                executionLog.Log("VendorManagement", "Enter twitter");
                equipment_VendorsHelper.TypeText("TwitterURL", "60601");

                executionLog.Log("VendorManagement", " Click on Save button   ");
                equipment_VendorsHelper.ClickElement("Save");
                equipment_VendorsHelper.WaitForWorkAround(1000);

                executionLog.Log("VendorManagement", " Click on Save button   ");
                equipment_VendorsHelper.VerifyPageText("Please enter a valid URL");
                //equipment_VendorsHelper.WaitForWorkAround(3000);

                executionLog.Log("VendorManagement", "Enter website");
                equipment_VendorsHelper.TypeText("Website", "http://www.vendors.com");

                executionLog.Log("VendorManagement", "Enter linkedin url");
                equipment_VendorsHelper.TypeText("LinkedURL", "http://www.linkedin.com");

                executionLog.Log("VendorManagement", "Enter twitter");
                equipment_VendorsHelper.TypeText("TwitterURL", "http://www.twitter.com");

                executionLog.Log("VendorManagement", " Click on Save button   ");
                equipment_VendorsHelper.ClickElement("Save");

                executionLog.Log("VendorManagement", "Wait for text");
                equipment_VendorsHelper.WaitForText("Vendor created successfully", 10);

                executionLog.Log("VendorManagement", "Redirect To Vendors");
                VisitOffice("vendors");
                equipment_VendorsHelper.WaitForWorkAround(3000);

                executionLog.Log("VendorManagement", "Verify title");
                VerifyTitle("Vendors");

                executionLog.Log("VendorManagement", "Enter Name to search");
                equipment_VendorsHelper.TypeText("VendorName", name);
                equipment_VendorsHelper.WaitForWorkAround(2000);

                executionLog.Log("VendorManagement", " Click on edit button   ");
                equipment_VendorsHelper.ClickElement("Edit");
                equipment_VendorsHelper.WaitForWorkAround(3000);

                executionLog.Log("VendorManagement", "Select Type");
                equipment_VendorsHelper.Select("Type", "");

                executionLog.Log("VendorManagement", "Enter Type");
                equipment_VendorsHelper.TypeText("Name", "");

                executionLog.Log("VendorManagement", " Click on Save button   ");
                equipment_VendorsHelper.ClickElement("Save");

                executionLog.Log("VendorManagement", "Verify validation text on page.");
                equipment_VendorsHelper.VerifyPageText("This field is required.");
                equipment_VendorsHelper.WaitForWorkAround(3000);

                executionLog.Log("VendorManagement", "Select Type");
                equipment_VendorsHelper.Select("Type", "Online");

                executionLog.Log("VendorManagement", "Enter Type");
                equipment_VendorsHelper.TypeText("Name", name);

                executionLog.Log("VendorManagement", " Click on AddEmail ");
                equipment_VendorsHelper.ClickElement("AddEmail");
                equipment_VendorsHelper.WaitForWorkAround(1000);

                executionLog.Log("VendorManagement", "Select eAddress Type");
                equipment_VendorsHelper.SelectByText("EaddressType2", "Web Links");

                executionLog.Log("VendorManagement", "Select EAddress Label");
                equipment_VendorsHelper.VerifyText("EadrressLabel2", "Web Link");

                executionLog.Log("VendorManagement", "Enter E Address");
                equipment_VendorsHelper.TypeText("eAddress2", "Test@yopmail.com");

                executionLog.Log("VendorManagement", " Click on Save button.");
                equipment_VendorsHelper.ClickElement("Save");

                executionLog.Log("VendorManagement", "Wait for Success message.");
                equipment_VendorsHelper.WaitForText("Vendor is successfully updated", 10);

                executionLog.Log("VendorManagement", "Redirect To Vandor");
                VisitOffice("vendors");
                equipment_VendorsHelper.WaitForWorkAround(3000);

                executionLog.Log("VendorManagement", "Verify page title");
                VerifyTitle("Vendors");

                executionLog.Log("VendorManagement", "Click on first vendor");
                equipment_VendorsHelper.ClickElement("ClickOnVender");
                equipment_VendorsHelper.WaitForWorkAround(3000);

                executionLog.Log("VendorManagement", "Click on clone icon.");
                equipment_VendorsHelper.ClickElement("Copy");

                executionLog.Log("VendorManagement", "Wait for success message");
                equipment_VendorsHelper.WaitForText("Vendor cloned successfully", 10);

                executionLog.Log("VendorManagement", "Redirect To Vendor");
                VisitOffice("vendors");
                equipment_VendorsHelper.WaitForWorkAround(3000);

                executionLog.Log("VendorManagement", "Verify page title");
                VerifyTitle("Vendors");

                executionLog.Log("VendorManagement", "Select first vendor ");
                equipment_VendorsHelper.ClickElement("SelectChkBox");

                executionLog.Log("VendorManagement", "Click on bulk update.");
                equipment_VendorsHelper.ClickElement("ClickOnBulkUpdate");

                executionLog.Log("VendorManagement", "Select vendor type. ");
                equipment_VendorsHelper.ClickElement("VendorTypeSelect");
                equipment_VendorsHelper.WaitForWorkAround(2000);

                executionLog.Log("VendorManagement", "Click on Update");
                equipment_VendorsHelper.ClickDisplayed("//button[text()='Update']");
                equipment_VendorsHelper.WaitForWorkAround(2000);

                executionLog.Log("VendorManagement", "Accept alert message.");
                equipment_VendorsHelper.AcceptAlert();
                equipment_VendorsHelper.WaitForWorkAround(2000);

                executionLog.Log("VendorManagement", "Wait for success message. ");
                equipment_VendorsHelper.VerifyPageText("Vendor Type updated successfully.");

                executionLog.Log("VendorManagement", "Click on delete icon.");
                equipment_VendorsHelper.ClickElement("Delete");
                equipment_VendorsHelper.WaitForWorkAround(2000);

                executionLog.Log("VendorManagement", "Accept alert message.");
                equipment_VendorsHelper.AcceptAlert();

                executionLog.Log("VendorManagement", "Wait for delete message.");
                equipment_VendorsHelper.WaitForText("Vendor Deleted Successfully", 10);

                executionLog.Log("VendorManagement", "Redirect To Vendors");
                VisitOffice("vendors");
                equipment_VendorsHelper.WaitForWorkAround(3000);

                executionLog.Log("VendorManagement", "Verify title");
                VerifyTitle("Vendors");

                executionLog.Log("VendorManagement", "Enter Name to search");
                equipment_VendorsHelper.TypeText("VendorName", name);
                equipment_VendorsHelper.WaitForWorkAround(2000);

                executionLog.Log("VendorManagement", "Click Delete btn  ");
                equipment_VendorsHelper.ClickElement("Delete");

                executionLog.Log("VendorManagement", "Accept alert message. ");
                equipment_VendorsHelper.AcceptAlert();

                executionLog.Log("VendorManagement", "Wait for delete message. ");
                equipment_VendorsHelper.WaitForText("Vendor Deleted Successfully", 10);

                VisitOffice("logout");
            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VendorManagement");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Vendor Management");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Vendor Management", "Bug", "Medium", "Vendor page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Vendor Management");
                        TakeScreenshot("VendorManagement");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VendorManagement.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VendorManagement");
                        string id = loginHelper.getIssueID("Vendor Management");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VendorManagement.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Vendor Management"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Vendor Management");
              //  executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("VendorManagement");
                executionLog.WriteInExcel("Vendor Management", Status, JIRA, "Equipment Management");
            }
        }
    }
}