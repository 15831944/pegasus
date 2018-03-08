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
    public class EditShippingCarriers : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin")]
        [TestCategory("TS6")]
        [TestCategory("Pegasus_Admin")]
        public void editShippingCarriers()
        {
            string[] username = null;
            string[] password = null;

            XMLParse oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var equipment_ShippingCarrierHelper = new Equipment_ShippingCarrierHelper(GetWebDriver());

            // Variable
            var name = "Test" + GetRandomNumber();
            var URL = "https://www.Test" + GetRandomNumber() + ".com";
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("EditShippingCarriers", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("EditShippingCarriers", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("EditShippingCarriers", "Click On  Admin");
                VisitOffice("admin");

                executionLog.Log("EditShippingCarriers", "Redirect To Shipping page");
                VisitOffice("shipping_carriers");

                executionLog.Log("EditShippingCarriers", "Verify title");
                VerifyTitle("Shipping Carriers");

                executionLog.Log("EditShippingCarriers", " Click On Create");
                equipment_ShippingCarrierHelper.ClickElement("Create");

                executionLog.Log("EditShippingCarriers", "Verify title");
                VerifyTitle("Manage Shipping Carrier");

                executionLog.Log("EditShippingCarriers", "Enter Name");
                equipment_ShippingCarrierHelper.TypeText("Name", name);

                executionLog.Log("EditShippingCarriers", "Enter Type");
                equipment_ShippingCarrierHelper.TypeText("Website", "http://www.test.com");

                executionLog.Log("EditShippingCarriers", "Enter EquipmentId");
                equipment_ShippingCarrierHelper.TypeText("TrackingURL", URL);

                executionLog.Log("EditShippingCarriers", " Click on Save button   ");
                equipment_ShippingCarrierHelper.ClickElement("Save");

                executionLog.Log("EditShippingCarriers", "wait for text");
                equipment_ShippingCarrierHelper.WaitForText("The shipping carrier is successfully created", 30);

                executionLog.Log("EditShippingCarriers", "Redirect To URL");
                VisitOffice("shipping_carriers");

                executionLog.Log("EditShippingCarriers", "Verify title");
                VerifyTitle("Shipping Carriers");

                executionLog.Log("EditShippingCarriers", "Enter Shipping Carrier name  in  search field");
                equipment_ShippingCarrierHelper.TypeText("SearchName", name);
                equipment_ShippingCarrierHelper.WaitForWorkAround(4000);

                executionLog.Log("EditShippingCarriers", "Click on Edit ");
                equipment_ShippingCarrierHelper.ClickElement("Edit");

                executionLog.Log("EditShippingCarriers", "Verify title");
                VerifyTitle("Manage Shipping Carrier");

                executionLog.Log("EditShippingCarriers", "Enter Name");
                equipment_ShippingCarrierHelper.TypeText("Name", name + "1");

                executionLog.Log("EditShippingCarriers", "Enter Type");
                equipment_ShippingCarrierHelper.TypeText("Website", "http://www.test.com");

                executionLog.Log("EditShippingCarriers", "Enter EquipmentId");
                equipment_ShippingCarrierHelper.TypeText("TrackingURL", URL);

                executionLog.Log("EditShippingCarriers", " Click on Save button   ");
                equipment_ShippingCarrierHelper.ClickElement("Save");

                executionLog.Log("EditShippingCarriers", "wait for text");
                equipment_ShippingCarrierHelper.WaitForText("The shipping carrier is successfully updated", 30);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("EditShippingCarriers");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Edit Shipping Carriers");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Edit Shipping Carriers", "Bug", "Medium", "Shipping page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Edit Shipping Carriers");
                        TakeScreenshot("EditShippingCarriers");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditShippingCarriers.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("EditShippingCarriers");
                        string id = loginHelper.getIssueID("Edit Shipping Carriers");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditShippingCarriers.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Edit Shipping Carriers"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Edit Shipping Carriers");
            //    executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("EditShippingCarriers");
                executionLog.WriteInExcel("Edit Shipping Carriers", Status, JIRA, "Equipment Management");
            }
        }
    }
}