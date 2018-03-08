using System;
using System.IO;
using LinqToExcel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;


namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class ShippingFilterError : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS5")]
        [TestCategory("NewSkinTaskJira")]
        public void shippingFilterError()
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
            var equipment_ShippingCarrierHelper = new Equipment_ShippingCarrierHelper(GetWebDriver());

            // Initializing the event
            var Name = "TestShipping" + RandomNumber(10, 300);
            var Website = "https://www.shipping" + RandomNumber(10, 100) + ".com";
            var Url = "https://www.shippingtrack" + RandomNumber(10, 100) + ".com";


            try
            {
                executionLog.Log("ShippingFilterError", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("ShippingFilterError", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("ShippingFilterError", "Visit to create vendor page");
                VisitOffice("shipping_carriers");
                equipment_ShippingCarrierHelper.WaitForWorkAround(3000);

                executionLog.Log("ShippingFilterError", "Verify title");
                VerifyTitle("Shipping Carriers");

                var loc = "//table[@id='list1']/tbody/tr[2]/td[2]";
                if (equipment_ShippingCarrierHelper.IsElementPresent(loc))
                {
                    equipment_ShippingCarrierHelper.WaitForWorkAround(4000);
                    executionLog.Log("ShippingFilterError", "Verify filter is reset successfully");
                    equipment_ShippingCarrierHelper.verifyElementPresent("NonFilter");
                }

                else
                {

                    executionLog.Log("ShippingFilterError", "Click on create");
                    equipment_ShippingCarrierHelper.ClickElement("Create");
                    equipment_ShippingCarrierHelper.WaitForWorkAround(4000);

                    executionLog.Log("ShippingFilterError", "Enter the name");
                    equipment_ShippingCarrierHelper.TypeText("Name", Name);

                    executionLog.Log("ShippingFilterError", "Enter the website url");
                    equipment_ShippingCarrierHelper.TypeText("Website", Website);

                    executionLog.Log("ShippingFilterError", "Enter the tracking url");
                    equipment_ShippingCarrierHelper.TypeText("TrackingURL", Url);

                    executionLog.Log("ShippingFilterError", "Click on save button");
                    equipment_ShippingCarrierHelper.ClickElement("Save");
                    equipment_ShippingCarrierHelper.WaitForWorkAround(6000);

                    executionLog.Log("ShippingFilterError", "verify title");
                    VerifyTitle("Shipping Carriers");

                    executionLog.Log("ShippingFilterError", "Verify filter is reset successfully");
                    equipment_ShippingCarrierHelper.verifyElementPresent("NonFilter");

                    executionLog.Log("ShippingFilterError", "Log out from the application");
                    VisitOffice("logout");

                }
            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("ShippingFilterError");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Shipping Filter Error");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Shipping Filter Error", "Bug", "Medium", "Equipment page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Shipping Filter Error");
                        TakeScreenshot("ShippingFilterError");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ShippingFilterError.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("ShippingFilterError");
                        string id = loginHelper.getIssueID("Shipping Filter Error");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ShippingFilterError.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Shipping Filter Error"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Shipping Filter Error");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("ShippingFilterError");
                executionLog.WriteInExcel("Shipping Filter Error", Status, JIRA, "Equipment Management");
            }
        }
    }
}