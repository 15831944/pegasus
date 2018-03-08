using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Admin
{
    [TestClass]
    public class BulkUpdateVendorStatus : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("Fail")]
        [TestCategory("TS1")]
        [TestCategory("BugTest")]
        public void bulkUpdateVendorStatus()
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
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("BulkUpdateVendorStatus", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("BulkUpdateVendorStatus", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("BulkUpdateVendorStatus", "Redirect To Vander");
                VisitOffice("vendors");

                executionLog.Log("BulkUpdateVendorStatus", "Verify Page title");
                VerifyTitle("Vendors");

                var Loc = "//table[@id='list1']/tbody/tr[2]";
                if (equipment_VendorsHelper.IsElementPresent(Loc))
                {

                    executionLog.Log("BulkUpdateVendorStatus", "Click on first check box.");
                    equipment_VendorsHelper.ClickElement("SelectFirstCheckBox");

                    executionLog.Log("BulkUpdateVendorStatus", "Click on Bulk Update");
                    equipment_VendorsHelper.ClickJs("ClickOnBulkUpdate");
                    equipment_VendorsHelper.WaitForWorkAround(2000);

                    executionLog.Log("BulkUpdateVendorStatus", "Click on change status");
                    equipment_VendorsHelper.ClickJs("ChangeStatusBulkUpdate");
                    equipment_VendorsHelper.WaitForWorkAround(2000);

                    executionLog.Log("BulkUpdateVendorStatus", "Click on Update");
                    equipment_VendorsHelper.ClickElement("StatusUpdate");
                    equipment_VendorsHelper.WaitForWorkAround(2000);

                    executionLog.Log("BulkUpdateVendorStatus", "Accept alert messsage");
                    equipment_VendorsHelper.AcceptAlert();

                    executionLog.Log("BulkUpdateVendorStatus", "Wait for success message.");
                    equipment_VendorsHelper.WaitForText("Vendor status updated successfully.", 10);
                    equipment_VendorsHelper.WaitForWorkAround(2000);

                }
                else
                {

                    executionLog.Log("BulkUpdateVendorStatus", " Click On Create");
                    VisitOffice("vendors/create");

                    executionLog.Log("BulkUpdateVendorStatus", " Verify title");
                    VerifyTitle("Create a New Vendor");

                    executionLog.Log("BulkUpdateVendorStatus", "Select vendor type.");
                    equipment_VendorsHelper.SelectDropDownByText("//*[@id='VendorType']", "Online");

                    executionLog.Log("BulkUpdateVendorStatus", "Enter vendor name.");
                    equipment_VendorsHelper.TypeText("Name", "Bulk Vendor");

                    executionLog.Log("BulkUpdateVendorStatus", "Enter DBA name");
                    equipment_VendorsHelper.TypeText("DBAName", "Test123");

                    executionLog.Log("BulkUpdateVendorStatus", "Select Salutation");
                    equipment_VendorsHelper.Select("Salutation", "Mr");

                    executionLog.Log("BulkUpdateVendorStatus", "Enter First Name");
                    equipment_VendorsHelper.TypeText("FirstName", "Test");

                    executionLog.Log("BulkUpdateVendorStatus", "Enter Last Name");
                    equipment_VendorsHelper.TypeText("LastName", "Bulk");

                    executionLog.Log("BulkUpdateVendorStatus", "Select eAddress");
                    equipment_VendorsHelper.Select("eAddessType", "E-Mail");

                    executionLog.Log("BulkUpdateVendorStatus", "Enter eAddress Label");
                    equipment_VendorsHelper.Select("EAddressLabel", "Work");

                    executionLog.Log("BulkUpdateVendorStatus", "Enter Eaddress");
                    equipment_VendorsHelper.TypeText("eAddress", "Test@yopmail.com");

                    executionLog.Log("BulkUpdateVendorStatus", "Phone Type");
                    equipment_VendorsHelper.Select("PhoneType", "Work");

                    executionLog.Log("BulkUpdateVendorStatus", "Enter Zip Code");
                    equipment_VendorsHelper.TypeText("ZipCodeVendor", "60601");

                    executionLog.Log("BulkUpdateVendorStatus", " Click on Save button");
                    equipment_VendorsHelper.Click("//button[@title='Save']");
                    equipment_VendorsHelper.WaitForWorkAround(5000);

                    executionLog.Log("BulkUpdateVendorStatus", " Select first vendor");
                    equipment_VendorsHelper.ClickElement("SelectFirstCheckBox");

                    executionLog.Log("BulkUpdateVendorStatus", "Click on bulk update.");
                    equipment_VendorsHelper.ClickJs("ClickOnBulkUpdate");
                    equipment_VendorsHelper.WaitForWorkAround(2000);

                    executionLog.Log("BulkUpdateVendorStatus", " Click on Cahnge status");
                    equipment_VendorsHelper.ClickJs("ChangeStatusBulkUpdate");
                    equipment_VendorsHelper.WaitForWorkAround(2000);

                    executionLog.Log("BulkUpdateVendorStatus", "Click on Update");
                    equipment_VendorsHelper.ClickElement("StatusUpdate");
                    equipment_VendorsHelper.WaitForWorkAround(2000);

                    executionLog.Log("BulkUpdateVendorStatus", "Accept alert message.");
                    equipment_VendorsHelper.AcceptAlert();
                    equipment_VendorsHelper.WaitForWorkAround(2000);

                    executionLog.Log("BulkUpdateVendorStatus", "Wait for success message");
                    equipment_VendorsHelper.WaitForText("Vendor status updated successfully.", 10);

                }
            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("BulkUpdateVendorStatus");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Bulk Update Vendor Status");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Bulk Update Vendor Status", "Bug", "Medium", "Office equipment", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Bulk Update Vendor Status");
                        TakeScreenshot("BulkUpdateVendorStatus");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\BulkUpdateVendorStatus.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("BulkUpdateVendorStatus");
                        string id = loginHelper.getIssueID("Bulk Update Vendor Status");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\BulkUpdateVendorStatus.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Bulk Update Vendor Status"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Bulk Update Vendor Status");
                //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("BulkUpdateVendorStatus");
                executionLog.WriteInExcel("Bulk Update Vendor Status", Status, JIRA, "Equipment Management");
            }
        }
    }
}
