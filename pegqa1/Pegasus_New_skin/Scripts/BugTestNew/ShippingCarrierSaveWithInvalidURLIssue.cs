using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class ShippingCarrierSaveWithInvalidURLIssue : DriverTestCase
    {
        [TestMethod]
        [TestCategory("Bug")]
        [TestCategory("TS3")]
        [TestCategory("BugTestNew")]
        public void shippingCarrierSaveWithInvalidURLIssue()
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
            var equipment_ShippingCarrierHelper = new Equipment_ShippingCarrierHelper(GetWebDriver());

            String JIRA = "";
            String Status = "Pass";


            try
            {
                executionLog.Log("ShippingCarrierSaveWithInvalidURLIssue", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("ShippingCarrierSaveWithInvalidURLIssue", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("ShippingCarrierSaveWithInvalidURLIssue", "Redirect at shipping carriers page.");
                VisitOffice("shipping_carriers");

                executionLog.Log("ShippingCarrierSaveWithInvalidURLIssue", "Click on Edit button.");
                equipment_ShippingCarrierHelper.ClickElement("Edit");

                executionLog.Log("ShippingCarrierSaveWithInvalidURLIssue", "Wait for tracking Url.");
                equipment_ShippingCarrierHelper.WaitForElementPresent("TrackingURL", 10);

                executionLog.Log("ShippingCarrierSaveWithInvalidURLIssue", "Enter Tracking Url.");
                equipment_ShippingCarrierHelper.TypeText("TrackingURL", "Test");

                executionLog.Log("ShippingCarrierSaveWithInvalidURLIssue", "Click on Save button.");
                equipment_ShippingCarrierHelper.ClickElement("Save");

                executionLog.Log("ShippingCarrierSaveWithInvalidURLIssue", "Wait for tracking Url error validation.");
                equipment_ShippingCarrierHelper.WaitForElementPresent("TrackingError2", 10);

                executionLog.Log("ShippingCarrierSaveWithInvalidURLIssue", "Verify Please enter a valid URL.");
                equipment_ShippingCarrierHelper.VerifyText("TrackingError2", "Please enter a valid URL.");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("ShippingCarrierSaveWithInvalidURLIssue");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Shipping Carrier Save With Invalid URL Issue");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Shipping Carrier Save With Invalid URL Issue", "Bug", "Medium", "Equipment page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Shipping Carrier Save With Invalid URL Issue");
                        TakeScreenshot("ShippingCarrierSaveWithInvalidURLIssue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ShippingCarrierSaveWithInvalidURLIssue.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("ShippingCarrierSaveWithInvalidURLIssue");
                        string id = loginHelper.getIssueID("Shipping Carrier Save With Invalid URL Issue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ShippingCarrierSaveWithInvalidURLIssue.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Shipping Carrier Save With Invalid URL Issue"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Shipping Carrier Save With Invalid URL Issue");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("ShippingCarrierSaveWithInvalidURLIssue");
                executionLog.WriteInExcel("Shipping Carrier Save With Invalid URL Issue", Status, JIRA, "Admin Equipments");
            }
        }
    }
}