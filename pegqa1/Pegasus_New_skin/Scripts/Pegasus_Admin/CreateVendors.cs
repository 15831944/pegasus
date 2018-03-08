using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Admin
{
    [TestClass]
    public class CreateVendors : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin")]
        [TestCategory("TS6")]
        [TestCategory("Pegasus_Admin")]
        public void createVendors()
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
                executionLog.Log("CreateVendors", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("CreateVendors", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("CreateVendors", "Redirect To Vendors");
                VisitOffice("vendors");
                equipment_VendorsHelper.WaitForWorkAround(3000);

                executionLog.Log("CreateVendors", "Verify title");
                VerifyTitle("Vendors");

                executionLog.Log("CreateVendors", " Click On Create");
                equipment_VendorsHelper.ClickElement("Create");
                equipment_VendorsHelper.WaitForWorkAround(3000);

                executionLog.Log("CreateVendors", "Verify title");
                VerifyTitle("Create a New Vendor");

                executionLog.Log("CreateVendors", "Select Type");
                equipment_VendorsHelper.Select("Type", "Online");

                executionLog.Log("CreateVendors", "Enter Type");
                equipment_VendorsHelper.TypeText("Name", name);

                executionLog.Log("CreateVendors", "Enter DBA name");
                equipment_VendorsHelper.TypeText("DBAName", "Test123");

                executionLog.Log("CreateVendors", "Select Salutation");
                equipment_VendorsHelper.Select("Salutation", "Mr");

                executionLog.Log("CreateVendors", "Enter First Name");
                equipment_VendorsHelper.TypeText("FirstName", "Test");

                executionLog.Log("CreateVendors", "Enter LatName");
                equipment_VendorsHelper.TypeText("LastName", "Vendor");

                executionLog.Log("CreateVendors", "Select eAddress Type");
                equipment_VendorsHelper.Select("eAddessType", "E-Mail");

                executionLog.Log("CreateVendors", "Select EAddress Label");
                equipment_VendorsHelper.Select("EAddressLabel", "Work");

                executionLog.Log("CreateVendors", "Enter E Address");
                equipment_VendorsHelper.TypeText("eAddress", "Test@yopmail.com");

                executionLog.Log("CreateVendors", "Select Phone Type");
                equipment_VendorsHelper.Select("PhoneType", "Work");

                executionLog.Log("CreateVendors", "Enter Zip Code");
                equipment_VendorsHelper.TypeText("ZipCodeVendor", "60601");
                equipment_VendorsHelper.WaitForWorkAround(3000);

                executionLog.Log("CreateVendors", " Click on Save button");
                equipment_VendorsHelper.ClickElement("Save");
                equipment_VendorsHelper.WaitForWorkAround(2000);

                executionLog.Log("CreateVendors", "Wait for text");
                equipment_VendorsHelper.WaitForText("Vendor created successfully", 10);

                executionLog.Log("CreateVendors", "Redirect To Vendors");
                VisitOffice("vendors");
                equipment_VendorsHelper.WaitForWorkAround(3000);

                executionLog.Log("CreateVendors", "Verify title");
                VerifyTitle("Vendors");

                executionLog.Log("CreateVendors", "Enter Name to search");
                equipment_VendorsHelper.TypeText("VendorName", name);
                equipment_VendorsHelper.WaitForWorkAround(2000);

                executionLog.Log("CreateVendors", "Click Delete btn  ");
                equipment_VendorsHelper.ClickElement("Delete");

                executionLog.Log("CreateVendors", "Accept alert message. ");
                equipment_VendorsHelper.AcceptAlert();

                executionLog.Log("CreateVendors", "Wait for delete message. ");
                equipment_VendorsHelper.WaitForText("Vendor Deleted Successfully", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CreateVendors");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Create Vendors");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Create Vendors", "Bug", "Medium", "Vendor page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Create Vendors");
                        TakeScreenshot("CreateVendors");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateVendors.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CreateVendors");
                        string id = loginHelper.getIssueID("Create Vendors");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateVendors.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Create Vendors"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Create Vendors");
             //   executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("CreateVendors");
                executionLog.WriteInExcel("Create Vendors", Status, JIRA, "Equipment Management");
            }
        }
    }
}
