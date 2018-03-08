using System;
using System.IO;
using LinqToExcel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;


namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class VendorEmailVerification : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS5")]
        [TestCategory("NewSkinTaskJira")]
        public void vendorEmailVerification()
        {
            string[] username = null;
            string[] password = null;
            string[] log = null;
            String JIRA = "";
            String Status = "Pass";

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");
            log = oXMLData.getData("settings/URL", "logout");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var equipment_VendorsHelper = new Equipment_VendorsHelper(GetWebDriver());

            try
            {
                executionLog.Log("VendorEmailVerification", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VendorEmailVerification", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VendorEmailVerification", "Visit to create vendor page");
                VisitOffice("vendors/create");

                executionLog.Log("VendorEmailVerification", "Verify title");
                VerifyTitle("Create a New Vendor");

                executionLog.Log("VendorEmailVerification", "Click on 'Save' button without entering details");
                equipment_VendorsHelper.ClickElement("Save");

                executionLog.Log("VendorEmailVerification", "Verify Validation message displayed for email");
                equipment_VendorsHelper.verifyElementPresent("EmailVerification");

                executionLog.Log("VendorEmailVerification", "Enter invalid email");
                equipment_VendorsHelper.TypeText("EmailVendor", "INVALID");

                executionLog.Log("VendorEmailVerification", "Click on 'Save' button after entering invalid email");
                equipment_VendorsHelper.ClickElement("Save");

                executionLog.Log("VendorEmailVerification", "Verify email validataion displayed");
                equipment_VendorsHelper.verifyElementPresent("EmailVerification");
                equipment_VendorsHelper.VerifyPageText("Please enter a valid email address");

                executionLog.Log("VendorEmailVerification", "Log out from the application");
                VisitOffice("logout");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VendorEmailVerification");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Vendor Email Verification");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Vendor Email Verification", "Bug", "Medium", "Equipment Vendors page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Vendor Email Verification");
                        TakeScreenshot("VendorEmailVerification");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VendorEmailVerification.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VendorEmailVerification");
                        string id = loginHelper.getIssueID("Vendor Email Verification");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VendorEmailVerification.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Vendor Email Verification"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Vendor Email Verification");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VendorEmailVerification");
                executionLog.WriteInExcel("Vendor Email Verification", Status, JIRA, "Equipment Management");
            }
        }
    }
}
		