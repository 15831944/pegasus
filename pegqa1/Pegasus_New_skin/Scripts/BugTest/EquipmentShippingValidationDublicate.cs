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
    public class EquipmentShippingValidationDublicate : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("Fail")]
        [TestCategory("TS1")]
        [TestCategory("BugTest")]
        public void equipmentShippingValidationDublicate()
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
            String name = "Test" + GetRandomNumber();
            String Id = "12345" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";


            try
            {
                executionLog.Log("EquipmentShippingValidationDublicate", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("EquipmentShippingValidationDublicate", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("EquipmentShippingValidationDublicate", "Visit office Admin");
                VisitOffice("admin");
                equipment_ShippingCarrierHelper.WaitForWorkAround(2000);

                executionLog.Log("EquipmentShippingValidationDublicate", "Redirect To shipping carrier page");
                VisitOffice("shipping_carriers");
                equipment_ShippingCarrierHelper.WaitForWorkAround(4000);


                executionLog.Log("EquipmentShippingValidationDublicate", "Verify title");
                VerifyTitle("Shipping Carriers");
                equipment_ShippingCarrierHelper.WaitForWorkAround(4000);

                executionLog.Log("EquipmentShippingValidationDublicate", "Search shipping carrier.");
                equipment_ShippingCarrierHelper.TypeText("SearchName", "Shipping Validation");
                equipment_ShippingCarrierHelper.WaitForWorkAround(3000);

                executionLog.Log("EquipmentShippingValidationDublicate", "Wait for element to present.");
                equipment_ShippingCarrierHelper.WaitForElementPresent("ClickOnShippingCarriersLink", 10);
                equipment_ShippingCarrierHelper.WaitForWorkAround(4000);

                var Loc = "//table[@id='list1']/tbody/tr[2]/td[3]/a[text()='Shipping Validation']";
                if (equipment_ShippingCarrierHelper.IsElementPresent(Loc))
                {

                    executionLog.Log("EquipmentShippingValidationDublicate", "Click On Create Shipping");
                    equipment_ShippingCarrierHelper.ClickElement("ClickOnCreateShipping");
                    equipment_ShippingCarrierHelper.WaitForWorkAround(1000);

                    executionLog.Log("EquipmentShippingValidationDublicate", "Enter Shipping Carrier Name");
                    equipment_ShippingCarrierHelper.TypeText("Name", "Shipping Validation");
                    equipment_ShippingCarrierHelper.WaitForWorkAround(4000);


                    executionLog.Log("EquipmentShippingValidationDublicate", "Enter Tracking URL");
                    equipment_ShippingCarrierHelper.TypeText("TrackingURL", "https://www.mypegasuscrm.com");
                    equipment_ShippingCarrierHelper.WaitForWorkAround(4000);

                    executionLog.Log("EquipmentShippingValidationDublicate", "Click On Save Btn Shiping");
                    equipment_ShippingCarrierHelper.ClickElement("Save");
                    equipment_ShippingCarrierHelper.WaitForWorkAround(4000);

                    executionLog.Log("EquipmentShippingValidationDublicate", "Wait for validation message.");
                    equipment_ShippingCarrierHelper.WaitForText("The shipping carrier is already exists.", 30);
                    equipment_ShippingCarrierHelper.WaitForWorkAround(4000);

                }
                else
                {

                    executionLog.Log("EquipmentShippingValidationDublicate", "Click On Create Shipping");
                    equipment_ShippingCarrierHelper.ClickElement("ClickOnCreateShipping");
                    equipment_ShippingCarrierHelper.WaitForWorkAround(3000);

                    executionLog.Log("EquipmentShippingValidationDublicate", "Enter Shipping Carrier Name");
                    equipment_ShippingCarrierHelper.TypeText("Name", "Shipping Validation");
                    equipment_ShippingCarrierHelper.WaitForWorkAround(4000);


                    executionLog.Log("EquipmentShippingValidationDublicate", "Enter Tracking URL");
                    equipment_ShippingCarrierHelper.TypeText("TrackingURL", "https://www.mypegasuscrm.com");
                    equipment_ShippingCarrierHelper.WaitForWorkAround(4000);


                    executionLog.Log("EquipmentShippingValidationDublicate", "Click On Save Btn Shiping");
                    equipment_ShippingCarrierHelper.ClickElement("Save");
                    equipment_ShippingCarrierHelper.WaitForWorkAround(4000);

                    executionLog.Log("EquipmentShippingValidationDublicate", "Wait for success message.");
                    equipment_ShippingCarrierHelper.WaitForText("The shipping carrier is successfully created", 30);
                    equipment_ShippingCarrierHelper.WaitForWorkAround(4000);

                    executionLog.Log("EquipmentShippingValidationDublicate", "Click On Create Shipping");
                    equipment_ShippingCarrierHelper.ClickElement("ClickOnCreateShipping");
                    equipment_ShippingCarrierHelper.WaitForWorkAround(4000);

                    executionLog.Log("EquipmentShippingValidationDublicate", "Enter Shipping Carrier Name");
                    equipment_ShippingCarrierHelper.TypeText("Name", "Shipping Validation");
                    equipment_ShippingCarrierHelper.WaitForWorkAround(4000);

                    executionLog.Log("EquipmentShippingValidationDublicate", "Enter Tracking URL");
                    equipment_ShippingCarrierHelper.TypeText("EnterTrackingURL", "https://www.mypegasuscrm.com");
                    equipment_ShippingCarrierHelper.WaitForWorkAround(4000);

                    executionLog.Log("EquipmentShippingValidationDublicate", "Click On Save Btn Shiping");
                    equipment_ShippingCarrierHelper.ClickElement("Save");
                    equipment_ShippingCarrierHelper.WaitForWorkAround(4000);

                    executionLog.Log("EquipmentShippingValidationDublicate", "Wait for validation message.");
                    equipment_ShippingCarrierHelper.WaitForText("The shipping carrier is already exists.", 30);

                }
            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("EquipmentShippingValidationDublicate");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Equipment Shipping Validation Dublicate");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Equipment Shipping Validation Dublicate", "Bug", "Medium", "Equipment page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Equipment Shipping Validation Dublicate");
                        TakeScreenshot("EquipmentShippingValidationDublicate");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EquipmentShippingValidationDublicate.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("EquipmentShippingValidationDublicate");
                        string id = loginHelper.getIssueID("Equipment Shipping Validation Dublicate");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EquipmentShippingValidationDublicate.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Equipment Shipping Validation Dublicate"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Equipment Shipping Validation Dublicate");
               // executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("EquipmentShippingValidationDublicate");
                executionLog.WriteInExcel("Equipment Shipping Validation Dublicate", Status, JIRA, "Admin Equipments");
            }
        }
    }
}