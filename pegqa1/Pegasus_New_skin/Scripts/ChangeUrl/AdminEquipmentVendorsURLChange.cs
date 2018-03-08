using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class AdminEquipmentVendorsURLChange : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Url")]
        [TestCategory("TS3")]
        [TestCategory("ChangeUrl")]
        public void adminEquipmentVendorsURLChange()
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


            // Variable
            var FirstName = "Test" + GetRandomNumber();
            var LastName = "Tester" + GetRandomNumber();
            var Number = "12345678" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("AdminEquipmentVendorsURLChange", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("AdminEquipmentVendorsURLChange", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("AdminEquipmentVendorsURLChange", "Goto User Equipment  >> Vendor  ");
                VisitOffice("vendors");

                executionLog.Log("AdminEquipmentVendorsURLChange", "Click On any Vendor ");
                equipment_VendorsHelper.ClickElement("ClickOnVender");
                equipment_VendorsHelper.WaitForWorkAround(2000);

                executionLog.Log("AdminEquipmentVendorsURLChange", "Change the url with the url number of another office");
                VisitOffice("vendors/edit/6");

                executionLog.Log("AdminEquipmentVendorsURLChange", "Verify Validation");
                equipment_VendorsHelper.WaitForText("oops something went wrong", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("AdminEquipmentVendorsURLChange");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Admin Equipment Vendors URL Change");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Admin Equipment Vendors URL Change", "Bug", "Medium", "Equipment Page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Admin Equipment Vendors URL Change");
                        TakeScreenshot("AdminEquipmentVendorsURLChange");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AdminEquipmentVendorsURLChange.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("AdminEquipmentVendorsURLChange");
                        string id = loginHelper.getIssueID("Admin Equipment Vendors URL Change");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AdminEquipmentVendorsURLChange.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Admin Equipment Vendors URL Change"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Admin Equipment Vendors URL Change");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("AdminEquipmentVendorsURLChange");
                executionLog.WriteInExcel("Admin Equipment Vendors URL Change", Status, JIRA, "Office Equipment");
            }
        }
    }
}
