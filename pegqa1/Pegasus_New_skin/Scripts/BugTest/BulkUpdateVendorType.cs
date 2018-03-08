using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Admin
{
    [TestClass]
    public class BulkUpdateVendorType : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS1")]
        [TestCategory("BugTest")]
        public void bulkUpdateVendorType()
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

            var Name = "QAVendor" + RandomNumber(100, 500);
            String JIRA = "";
            String Status = "Pass";


            try
            {
                executionLog.Log("BulkUpdateVendorType", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("BulkUpdateVendorType", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("BulkUpdateVendorType", "Redirect to Vendor ");
                VisitOffice("vendors");

                var Loc = "//table[@id='list1']/tbody/tr[2]";
                if (equipment_VendorsHelper.IsElementPresent(Loc))
                {
                    equipment_VendorsHelper.WaitForWorkAround(2000);
                    executionLog.Log("BulkUpdateVendorType", "Select first vendor ");
                    equipment_VendorsHelper.ClickElement("SelectChkBox");

                    executionLog.Log("BulkUpdateVendorType", "Click on bulk update.");
                    equipment_VendorsHelper.ClickElement("ClickOnBulkUpdate");

                    executionLog.Log("BulkUpdateVendorType", "Select vendor type. ");
                    equipment_VendorsHelper.ClickElement("VendorTypeSelect");
                    equipment_VendorsHelper.WaitForWorkAround(2000);

                    executionLog.Log("BulkUpdateVendorType", "Click on Update");
                    equipment_VendorsHelper.ClickDisplayed("//button[text()='Update']");
                    equipment_VendorsHelper.WaitForWorkAround(2000);

                    executionLog.Log("BulkUpdateVendorType", "Accept alert message.");
                    equipment_VendorsHelper.AcceptAlert();
                    equipment_VendorsHelper.WaitForWorkAround(2000);

                    executionLog.Log("BulkUpdateVendorType", "Wait for success message. ");
                    equipment_VendorsHelper.VerifyPageText("Vendor Type updated successfully.");

                }
                else
                {

                    executionLog.Log("BulkUpdateVendorType", " Click On Create");
                    equipment_VendorsHelper.ClickElement("Create");

                    executionLog.Log("BulkUpdateVendorType", "Select Type");
                    equipment_VendorsHelper.Select("Type", "Online");

                    executionLog.Log("BulkUpdateVendorType", "Enter name");
                    equipment_VendorsHelper.TypeText("Name", Name);

                    executionLog.Log("BulkUpdateVendorType", "Enter DBA Name");
                    equipment_VendorsHelper.TypeText("DBAName", "Test123");

                    executionLog.Log("BulkUpdateVendorType", "Enter website");
                    equipment_VendorsHelper.TypeText("Website", "www.test.com");

                    executionLog.Log("BulkUpdateVendorType", "Enter LinkedURL");
                    equipment_VendorsHelper.TypeText("LinkedURL", "LinkedIn.com");

                    executionLog.Log("BulkUpdateVendorType", "Enter TwitterURL");
                    equipment_VendorsHelper.TypeText("TwitterURL", "Twiter.com");

                    executionLog.Log("BulkUpdateVendorType", "Select Salutation");
                    equipment_VendorsHelper.Select("Salutation", "Mr");

                    executionLog.Log("BulkUpdateVendorType", "Enter First Name");
                    equipment_VendorsHelper.TypeText("FirstName", "Test");

                    executionLog.Log("BulkUpdateVendorType", "Enter Last Name");
                    equipment_VendorsHelper.TypeText("LastName", "Bulk");

                    executionLog.Log("BulkUpdateVendorType", "Select eAddress Type");
                    equipment_VendorsHelper.Select("eAddessType", "E-Mail");

                    executionLog.Log("BulkUpdateVendorType", "Select EAddress Label");
                    equipment_VendorsHelper.Select("EAddressLabel", "Work");

                    executionLog.Log("BulkUpdateVendorType", "Enter E Address");
                    equipment_VendorsHelper.TypeText("eAddress", "Test@yopmail.com");

                    executionLog.Log("BulkUpdateVendorType", "Select Phone Type");
                    equipment_VendorsHelper.Select("PhoneType", "Work");

                    executionLog.Log("BulkUpdateVendorType", "Enter Phone Number");
                    equipment_VendorsHelper.TypeText("PhoneNumber", "9898952292");

                    executionLog.Log("BulkUpdateVendorType", "Enter Zip Code");
                    equipment_VendorsHelper.TypeText("ZipCodeVendor", "60601");

                    executionLog.Log("BulkUpdateVendorType", " Click on Save button ");
                    equipment_VendorsHelper.ClickElement("Save");
                    equipment_VendorsHelper.WaitForWorkAround(1000);

                    executionLog.Log("BulkUpdateVendorType", "Redirect To URL");
                    VisitOffice("vendors");
                    equipment_VendorsHelper.WaitForWorkAround(2000);

                    executionLog.Log("BulkUpdateVendorType", "Click first check box.");
                    equipment_VendorsHelper.ClickElement("SelectChkBox");
                    equipment_VendorsHelper.WaitForWorkAround(2000);

                    executionLog.Log("BulkUpdateVendorType", "Click on  bulk update.");
                    equipment_VendorsHelper.ClickElement("ClickOnBulkUpdate");
                    equipment_VendorsHelper.WaitForWorkAround(2000);

                    executionLog.Log("BulkUpdateVendorType", "Select vendor type.");
                    equipment_VendorsHelper.ClickElement("VendorTypeSelect");
                    equipment_VendorsHelper.WaitForWorkAround(2000);

                    executionLog.Log("BulkUpdateVendorType", "Click on Update");
                    equipment_VendorsHelper.ClickViaJavaScript("//button[text()='Update']");
                    equipment_VendorsHelper.WaitForWorkAround(2000);

                    executionLog.Log("BulkUpdateVendorType", "Accept alert message");
                    equipment_VendorsHelper.AcceptAlert();
                    equipment_VendorsHelper.WaitForWorkAround(2000);

                }
            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("BulkUpdateVendorType");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Bulk Update Vendor Type");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Bulk Update Vendor Type", "Bug", "Medium", "Vendor page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Bulk Update Vendor Type");
                        TakeScreenshot("BulkUpdateVendorType");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\BulkUpdateVendorType.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("BulkUpdateVendorType");
                        string id = loginHelper.getIssueID("Bulk Update Vendor Type");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\BulkUpdateVendorType.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Bulk Update Vendor Type"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Bulk Update Vendor Type");
                //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("BulkUpdateVendorType");
                executionLog.WriteInExcel("Bulk Update Vendor Type", Status, JIRA, "Office equipment");
            }
        }
    }
}