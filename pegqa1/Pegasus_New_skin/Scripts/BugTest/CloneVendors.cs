using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Admin
{
    [TestClass]
    public class CloneVendors : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS1")]
        [TestCategory("BugTest")]
        public void cloneVendors()
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
                executionLog.Log("CloneVendors", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("CloneVendors", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("CloneVendors", "Redirect To Vandor");
                VisitOffice("vendors");

                executionLog.Log("CloneVendors", "Verify page title");
                VerifyTitle("Vendors");

                var Loc = "//table[@id='list1']/tbody/tr[2]";
                if (equipment_VendorsHelper.IsElementPresent(Loc))
                {

                    executionLog.Log("CloneVendors", "Click on first vendor");
                    equipment_VendorsHelper.ClickElement("ClickOnVender");
                    equipment_VendorsHelper.WaitForWorkAround(1000);

                    executionLog.Log("CloneVendors", "Click on clone icon.");
                    equipment_VendorsHelper.ClickElement("Copy");

                    executionLog.Log("CloneVendors", "Wait for success message");
                    equipment_VendorsHelper.WaitForText("Vendor cloned successfully", 30);

                    executionLog.Log("CloneVendors", "Delete Clone");
                    equipment_VendorsHelper.ClickElement("DeleteClone");
                    equipment_VendorsHelper.WaitForWorkAround(2000);

                    executionLog.Log("CloneVendors", "Accept alert message.");
                    equipment_VendorsHelper.AcceptAlert();
                    equipment_VendorsHelper.WaitForWorkAround(2000);

                    executionLog.Log("CloneVendors", "Wait for delete message.");
                    equipment_VendorsHelper.WaitForText("Vendor Deleted Successfully", 30);
                }
                else
                {
                    executionLog.Log("CloneVendors", " Click On Create");
                    VisitOffice("vendors/create");
                    equipment_VendorsHelper.WaitForWorkAround(2000);

                    executionLog.Log("CloneVendors", "Select type");
                    equipment_VendorsHelper.Select("Type", "Online");

                    executionLog.Log("CloneVendors", "Enter name");
                    equipment_VendorsHelper.TypeText("Name", "Clone Vendor");

                    executionLog.Log("CloneVendors", "Enter DBA Name");
                    equipment_VendorsHelper.TypeText("DBAName", "Test123");

                    executionLog.Log("CloneVendors", "Select Salutation");
                    equipment_VendorsHelper.Select("Salutation", "Mr");

                    executionLog.Log("CloneVendors", " Enter First Name");
                    equipment_VendorsHelper.TypeText("FirstName", "Test");

                    executionLog.Log("CloneVendors", "Enter LastName");
                    equipment_VendorsHelper.TypeText("LastName", "Clone");

                    executionLog.Log("CloneVendors", "Select eAddress");
                    equipment_VendorsHelper.Select("eAddessType", "E-Mail");

                    executionLog.Log("CloneVendors", "Select EAddress Label");
                    equipment_VendorsHelper.Select("EAddressLabel", "Work");

                    executionLog.Log("CloneVendors", "Enter E Address");
                    equipment_VendorsHelper.TypeText("eAddress", "Test@yopmail.com");

                    executionLog.Log("CloneVendors", "Select Phone Type");
                    equipment_VendorsHelper.Select("PhoneType", "Work");

                    executionLog.Log("CloneVendors", "Enter Zip Code");
                    equipment_VendorsHelper.TypeText("ZipCodeVendor", "60601");
                    equipment_VendorsHelper.WaitForWorkAround(3000);

                    executionLog.Log("CloneVendors", " Click on Save button  ");
                    equipment_VendorsHelper.ClickElement("Save");
                    equipment_VendorsHelper.WaitForWorkAround(3000);

                    executionLog.Log("CloneVendors", " Click on first vendor ");
                    equipment_VendorsHelper.ClickElement("ClickOnVender");
                    equipment_VendorsHelper.WaitForWorkAround(1000);

                    executionLog.Log("CloneVendors", "Click on copy button");
                    equipment_VendorsHelper.ClickElement("Copy");

                    executionLog.Log("CloneVendors", "Wait for the success message.");
                    equipment_VendorsHelper.WaitForText("Vendor cloned successfully", 30);

                    executionLog.Log("CloneVendors", "Delete Clone");
                    equipment_VendorsHelper.ClickElement("DeleteClone");
                    equipment_VendorsHelper.WaitForWorkAround(1000);

                    executionLog.Log("CloneVendors", " Accept alert message.  ");
                    equipment_VendorsHelper.AcceptAlert();

                    executionLog.Log("CloneVendors", " Wait for the success message");
                    equipment_VendorsHelper.WaitForText("Vendor Deleted Successfully", 30);

                }
            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CloneVendors");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Clone Vendors");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Clone Vendors", "Bug", "Medium", "Vendor page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Clone Vendors");
                        TakeScreenshot("CloneVendors");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CloneVendors.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CloneVendors");
                        string id = loginHelper.getIssueID("Clone Vendors");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CloneVendors.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Clone Vendors"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Clone Vendors");
                //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("CloneVendors");
                executionLog.WriteInExcel("Clone Vendors", Status, JIRA, "Admin Equipments");
            }
        }
    }
}