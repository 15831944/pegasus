using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class AdminEquipmentShippingCarrierURLChange : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Url")]
        [TestCategory("Temp")]
        [TestCategory("TS3")]
        [TestCategory("ChangeUrl")]
        public void adminEquipmentShippingCarrierURLChange()
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
            var FirstName = "Test" + GetRandomNumber();
            var LastName = "Tester" + GetRandomNumber();
            var Number = "12345678" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("AdminEquipmentShippingCarrierURLChange", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("AdminEquipmentShippingCarrierURLChange", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("AdminEquipmentShippingCarrierURLChange", "Goto User Equipments   >> Shipping Carrier  ");
                VisitOffice("shipping_carriers");

                executionLog.Log("AdminEquipmentShippingCarrierURLChange", "Click On any Shipping carrier");
                equipment_ShippingCarrierHelper.ClickElement("OpenAnyShiping");
                equipment_ShippingCarrierHelper.WaitForWorkAround(2000);

                executionLog.Log("AdminEquipmentShippingCarrierURLChange", "Change the url with the url number of another office");
                VisitOffice("manage_shipping_carriers/61");
                equipment_ShippingCarrierHelper.WaitForWorkAround(1000);

                executionLog.Log("AdminEquipmentShippingCarrierURLChange", "Verify Validation");
                equipment_ShippingCarrierHelper.VerifyPageText("The shipping carrier is does not exists.");
                equipment_ShippingCarrierHelper.WaitForWorkAround(4000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("AdminEquipmentShippingCarrierURLChange");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Admin Equipment Shipping Carrier URL Change");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Admin Equipment Shipping Carrier URL Change", "Bug", "Medium", "Equipment Page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Admin Equipment Shipping Carrier URL Change");
                        TakeScreenshot("AdminEquipmentShippingCarrierURLChange");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AdminEquipmentShippingCarrierURLChange.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("AdminEquipmentShippingCarrierURLChange");
                        string id = loginHelper.getIssueID("Admin Equipment Shipping Carrier URL Change");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AdminEquipmentShippingCarrierURLChange.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Admin Equipment Shipping Carrier URL Change"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Admin Equipment Shipping Carrier URL Change");
               // executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("AdminEquipmentShippingCarrierURLChange");
                executionLog.WriteInExcel("Admin Equipment Shipping Carrier URL Change", Status, JIRA, "Office Equipment");
            }
        }
    }
}
