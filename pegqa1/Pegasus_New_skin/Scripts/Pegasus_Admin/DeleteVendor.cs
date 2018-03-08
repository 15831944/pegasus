using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Admin
{
    [TestClass]
    public class DeleteVendor : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin")]
        [TestCategory("TS6")]
        [TestCategory("Pegasus_Admin")]
        public void deleteVendor()
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

            // Random Variable.
            var name = "Vendor" + RandomNumber(1, 999);
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("DeleteVendor", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("DeleteVendor", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("DeleteVendor", "Redirect To Vendors");
                VisitOffice("vendors");
                equipment_VendorsHelper.WaitForWorkAround(3000);

                executionLog.Log("DeleteVendor", "Verify title");
                VerifyTitle("Vendors");

                executionLog.Log("DeleteVendor", " Click On Create");
                equipment_VendorsHelper.ClickElement("Create");
                equipment_VendorsHelper.WaitForWorkAround(3000);

                executionLog.Log("DeleteVendor", "Verify title");
                VerifyTitle("Create a New Vendor");

                executionLog.Log("DeleteVendor", "Select Type");
                equipment_VendorsHelper.Select("Type", "Online");

                executionLog.Log("DeleteVendor", "Enter Type");
                equipment_VendorsHelper.TypeText("Name", name);

                executionLog.Log("DeleteVendor", "Enter DBA name");
                equipment_VendorsHelper.TypeText("DBAName", "Test123");

                executionLog.Log("DeleteVendor", "Select Salutation");
                equipment_VendorsHelper.Select("Salutation", "Mr");

                executionLog.Log("DeleteVendor", "Enter First Name");
                equipment_VendorsHelper.TypeText("FirstName", "Test");

                executionLog.Log("DeleteVendor", "Enter LatName");
                equipment_VendorsHelper.TypeText("LastName", "Vendor");

                executionLog.Log("DeleteVendor", "Select eAddress Type");
                equipment_VendorsHelper.Select("eAddessType", "E-Mail");

                executionLog.Log("DeleteVendor", "Select EAddress Label");
                equipment_VendorsHelper.Select("EAddressLabel", "Work");

                executionLog.Log("DeleteVendor", "Enter E Address");
                equipment_VendorsHelper.TypeText("eAddress", "Test@yopmail.com");

                executionLog.Log("DeleteVendor", "Select Phone Type");
                equipment_VendorsHelper.Select("PhoneType", "Work");

                executionLog.Log("DeleteVendor", "Enter Zip Code");
                equipment_VendorsHelper.TypeText("ZipCodeVendor", "60601");
                equipment_VendorsHelper.WaitForWorkAround(3000);

                executionLog.Log("DeleteVendor", " Click on Save button");
                equipment_VendorsHelper.ClickElement("Save");
                equipment_VendorsHelper.WaitForWorkAround(3000);

                executionLog.Log("DeleteVendor", "Verify title");
                VerifyTitle("Vendors");

                executionLog.Log("DeleteVendor", "Enter Vendor Name");
                equipment_VendorsHelper.TypeText("VendorName", name);
                equipment_VendorsHelper.WaitForWorkAround(2000);

                executionLog.Log("DeleteVendor", "Search Last Name");
                equipment_VendorsHelper.TypeText("VendorLastName", "Vendor");
                equipment_VendorsHelper.WaitForWorkAround(3000);

                executionLog.Log("DeleteVendor", "Click on Delete");
                equipment_VendorsHelper.ClickElement("Delete");
                equipment_VendorsHelper.AcceptAlert();
                
                executionLog.Log("DeleteVendor", "Wait for delete text");
                equipment_VendorsHelper.WaitForText("Vendor Deleted Successfully", 10);
                
            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("DeleteVendor");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Delete Vendor");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Delete Vendor", "Bug", "Medium", "Vendors page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Delete Vendor");
                        TakeScreenshot("DeleteVendor");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\DeleteVendor.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("DeleteVendor");
                        string id = loginHelper.getIssueID("Delete Vendor");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\DeleteVendor.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Delete Vendor"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Delete Vendor");
            //    executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("DeleteVendor");
                executionLog.WriteInExcel("Delete Vendor", Status, JIRA, "Equipment Management");
            }
        }
    }
}
