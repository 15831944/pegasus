using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Admin
{
    [TestClass]
    public class ShippingCarrierManagement : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin1")]
        [TestCategory("TS7")]
        [TestCategory("Pegasus_Admin1")]
        public void shippingCarrierManagement()
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
            var equipment_ShippingCarrierHelper = new Equipment_ShippingCarrierHelper(GetWebDriver());

            // Variable
            var name = "Test" + RandomNumber(1, 999);
            var URL = "http://www.Test" + GetRandomNumber() + ".com";
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("ShippingCarrierManagement", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("ShippingCarrierManagement", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("ShippingCarrierManagement", "Click On  Admin");
                VisitOffice("admin");

                executionLog.Log("ShippingCarrierManagement", "Redirect To Shipping Carrier");
                VisitOffice("shipping_carriers");

                executionLog.Log("ShippingCarrierManagement", "Verify title");
                VerifyTitle("Shipping Carriers");

                executionLog.Log("ShippingCarrierManagement", " Click On Create");
                equipment_ShippingCarrierHelper.ClickElement("Create");

                executionLog.Log("ShippingCarrierManagement", "Verify title");
                VerifyTitle("Manage Shipping Carrier");

                executionLog.Log("ShippingCarrierManagement", " Click on Save button  ");
                equipment_ShippingCarrierHelper.ClickElement("Save");

                executionLog.Log("ShippingCarrierManagement", "Verify error for name");
                equipment_ShippingCarrierHelper.VerifyText("ShippingNameErr", "This field is required.");

                executionLog.Log("ShippingCarrierManagement", "Verify tracking url Mandatory message.");
                equipment_ShippingCarrierHelper.VerifyText("TrackinError", "This field is required.");

                executionLog.Log("ShippingCarrierManagement", "Enter Name");
                equipment_ShippingCarrierHelper.TypeText("Name", name);

                executionLog.Log("ShippingCarrierManagement", "Enter invalid Website");
                equipment_ShippingCarrierHelper.TypeText("Website", "www.testweb.com");

                executionLog.Log("ShippingCarrierManagement", "Enter invalid tracking URL");
                equipment_ShippingCarrierHelper.TypeText("TrackingURL", "1213");

                executionLog.Log("ShippingCarrierManagement", " Click on Save button  ");
                equipment_ShippingCarrierHelper.ClickElement("Save");

                executionLog.Log("ShippingCarrierManagement", "Verify validation for invalid url");
                equipment_ShippingCarrierHelper.VerifyText("TrackingError2", "Please enter a valid URL.");

                executionLog.Log("ShippingCarrierManagement", "Verify validation for invalid url");
                equipment_ShippingCarrierHelper.VerifyText("WebError", "Please enter a valid URL.");

                executionLog.Log("ShippingCarrierManagement", "Enter Name");
                equipment_ShippingCarrierHelper.TypeText("Name", name);

                executionLog.Log("ShippingCarrierManagement", "Enter valid Website");
                equipment_ShippingCarrierHelper.TypeText("Website", "http://www.test.com");

                executionLog.Log("ShippingCarrierManagement", "Enter valid tracking URL");
                equipment_ShippingCarrierHelper.TypeText("TrackingURL", URL);

                executionLog.Log("ShippingCarrierManagement", " Click on Save button  ");
                equipment_ShippingCarrierHelper.ClickElement("Save");

                executionLog.Log("ShippingCarrierManagement", "Wait for text");
                equipment_ShippingCarrierHelper.WaitForText("The shipping carrier is successfully created", 10);

                executionLog.Log("ShippingCarrierManagement", "Enter Shipping Carrier name  in  search field");
                equipment_ShippingCarrierHelper.TypeText("SearchName", name);
                equipment_ShippingCarrierHelper.WaitForWorkAround(4000);

                executionLog.Log("ShippingCarrierManagement", "Click on Edit ");
                equipment_ShippingCarrierHelper.ClickElement("Edit");

                executionLog.Log("ShippingCarrierManagement", "Verify title");
                VerifyTitle("Manage Shipping Carrier");

                executionLog.Log("ShippingCarrierManagement", "Enter new Name");
                equipment_ShippingCarrierHelper.TypeText("Name", name + "1");

                executionLog.Log("ShippingCarrierManagement", "Enter a new website");
                equipment_ShippingCarrierHelper.TypeText("Website", "http://www.shipping.com");

                executionLog.Log("ShippingCarrierManagement", "Enter TrackingUrl");
                equipment_ShippingCarrierHelper.TypeText("TrackingURL", URL);

                executionLog.Log("ShippingCarrierManagement", " Click on Save button ");
                equipment_ShippingCarrierHelper.ClickElement("Save");

                executionLog.Log("ShippingCarrierManagement", "wait for success text");
                equipment_ShippingCarrierHelper.WaitForText("The shipping carrier is successfully updated", 10);

                executionLog.Log("ShippingCarrierManagement", "Redirect To Shipping Carrier");
                VisitOffice("shipping_carriers");

                executionLog.Log("ShippingCarrierManagement", " Click on Status icon");
                equipment_ShippingCarrierHelper.ClickElement("Status");
                equipment_ShippingCarrierHelper.AcceptAlert();

                executionLog.Log("ShippingCarrierManagement", " Wait for success message");
                equipment_ShippingCarrierHelper.WaitForText("Equipment Shipping Carrier is successfully deactivated", 10);

                executionLog.Log("ShippingCarrierManagement", " Click on Status icon");
                equipment_ShippingCarrierHelper.ClickElement("Status");
                equipment_ShippingCarrierHelper.AcceptAlert();

                executionLog.Log("ShippingCarrierManagement", "  Wait for success message");
                equipment_ShippingCarrierHelper.WaitForText("Equipment Shipping Carrier is successfully activated", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("ShippingCarrierManagement");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Shipping Carrier Management");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Shipping Carrier Management", "Bug", "Medium", "Shipping Carrier page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Shipping Carrier Management");
                        TakeScreenshot("ShippingCarrierManagement");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ShippingCarrierManagement.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("ShippingCarrierManagement");
                        string id = loginHelper.getIssueID("Shipping Carrier Management");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ShippingCarrierManagement.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Shipping Carrier Management"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Shipping Carrier Management");
             //   executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("ShippingCarrierManagement");
                executionLog.WriteInExcel("Shipping Carrier Management", Status, JIRA, "Equipment Management");
            }
        }
    }
}