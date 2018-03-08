using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Admin
{
    [TestClass]
    public class CreateShippingCarrier : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin")]
        [TestCategory("TS6")]
        [TestCategory("Pegasus_Admin")]
        public void createShippingCarrier()
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
            var name = "Test" + GetRandomNumber();
            var URL = "http://www.Test" + GetRandomNumber() + ".com";
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("CreateShippingCarrier", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("CreateShippingCarrier", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("CreateShippingCarrier", "Click On  Admin");
                VisitOffice("admin");

                executionLog.Log("CreateShippingCarrier", "Redirect To Shipping Carrier");
                VisitOffice("shipping_carriers");

                executionLog.Log("CreateShippingCarrier", "Verify title");
                VerifyTitle("Shipping Carriers");

                executionLog.Log("CreateShippingCarrier", " Click On Create");
                equipment_ShippingCarrierHelper.ClickElement("Create");

                executionLog.Log("CreateShippingCarrier", "Verify title");
                VerifyTitle("Manage Shipping Carrier");

                executionLog.Log("CreateShippingCarrier", "Enter Name");
                equipment_ShippingCarrierHelper.TypeText("Name", name);

                executionLog.Log("CreateShippingCarrier", "Enter Website");
                equipment_ShippingCarrierHelper.TypeText("Website", "http://www.Test.com");

                executionLog.Log("CreateShippingCarrier", "Enter tracking URL");
                equipment_ShippingCarrierHelper.TypeText("TrackingURL", URL);

                executionLog.Log("CreateShippingCarrier", " Click on Save button  ");
                equipment_ShippingCarrierHelper.ClickElement("Save");

                executionLog.Log("CreateShippingCarrier", "Wait for text");
                equipment_ShippingCarrierHelper.WaitForText("The shipping carrier is successfully created", 30);

            }
            
            catch (Exception e)
            {
               executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CreateShippingCarrier");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Create Shipping Carrier");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Create Shipping Carrier", "Bug", "Medium", "Shipping Carrier page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Create Shipping Carrier");
                        TakeScreenshot("CreateShippingCarrier");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateShippingCarrier.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CreateShippingCarrier");
                        string id = loginHelper.getIssueID("Create Shipping Carrier");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateShippingCarrier.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Create Shipping Carrier"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Create Shipping Carrier");
              //  executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("CreateShippingCarrier");
                executionLog.WriteInExcel("Create Shipping Carrier", Status, JIRA, "Equipment Management");
            }
        }
    }
}
