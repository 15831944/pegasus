using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class EquipmentsAdvanceFilterResultsPP : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS6")]
        [TestCategory("Pegasus_Admin")]
        public void equipmentsAdvanceFilterResultsPP()
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
            var eqiupment_EquipmentHelper = new Eqiupment_EquipmentHelper(GetWebDriver());

            // Variable
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("EquipmentsAdvanceFilterResultsPP", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("EquipmentsAdvanceFilterResultsPP", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("EquipmentsAdvanceFilterResultsPP", "Redirect at employee page.");
                VisitOffice("equipment");
                eqiupment_EquipmentHelper.WaitForWorkAround(5000);

                executionLog.Log("EquipmentsAdvanceFilterResultsPP", "Verify page title.");
                VerifyTitle("Equipment");
                eqiupment_EquipmentHelper.WaitForElementVisible("AdvanceFilter", 10);

                executionLog.Log("EquipmentsAdvanceFilterResultsPP", "Click on advance filter.");
                eqiupment_EquipmentHelper.ClickElement("AdvanceFilter");
                eqiupment_EquipmentHelper.WaitForWorkAround(3000);

                executionLog.Log("EquipmentsAdvanceFilterResultsPP", "Select number of records to 10.");
                eqiupment_EquipmentHelper.SelectByText("ResultsPerPage", "10");
                eqiupment_EquipmentHelper.WaitForWorkAround(2000);

                executionLog.Log("EquipmentsAdvanceFilterResultsPP", "Click on Apply button.");
                eqiupment_EquipmentHelper.ClickElement("Apply");
                eqiupment_EquipmentHelper.WaitForWorkAround(3000);

                executionLog.Log("EquipmentsAdvanceFilterResultsPP", "Verify number of records displayed.");
                // eqiupment_EquipmentHelper.VerifyText("No.ofRecords", "Showing 1 - 10 of");
                eqiupment_EquipmentHelper.ShowResult(10);
                eqiupment_EquipmentHelper.WaitForWorkAround(3000);

                executionLog.Log("EquipmentsAdvanceFilterResultsPP", "Click on advance filter.");
                eqiupment_EquipmentHelper.ClickElement("AdvanceFilter");
                eqiupment_EquipmentHelper.WaitForWorkAround(3000);

                executionLog.Log("EquipmentsAdvanceFilterResultsPP", "Select number of records to 20.");
                eqiupment_EquipmentHelper.SelectByText("ResultsPerPage", "20");
                eqiupment_EquipmentHelper.WaitForWorkAround(3000);

                executionLog.Log("EquipmentsAdvanceFilterResultsPP", "Click on Apply button.");
                eqiupment_EquipmentHelper.ClickElement("Apply");
                eqiupment_EquipmentHelper.WaitForWorkAround(3000);

                executionLog.Log("EquipmentsAdvanceFilterResultsPP", "Verify number of records displayed.");
                //   eqiupment_EquipmentHelper.VerifyText("No.ofRecords", "Showing 1 - 20 of");
                eqiupment_EquipmentHelper.ShowResult(20);
                eqiupment_EquipmentHelper.WaitForWorkAround(3000);

                executionLog.Log("EquipmentsAdvanceFilterResultsPP", "Click on advance filter.");
                eqiupment_EquipmentHelper.ClickElement("AdvanceFilter");
                eqiupment_EquipmentHelper.WaitForWorkAround(3000);

                executionLog.Log("EquipmentsAdvanceFilterResultsPP", "Select number of records to 50.");
                eqiupment_EquipmentHelper.SelectByText("ResultsPerPage", "50");
                eqiupment_EquipmentHelper.WaitForWorkAround(3000);

                executionLog.Log("EquipmentsAdvanceFilterResultsPP", "Click on Apply button.");
                eqiupment_EquipmentHelper.ClickElement("Apply");
                eqiupment_EquipmentHelper.WaitForWorkAround(3000);

                executionLog.Log("EquipmentsAdvanceFilterResultsPP", "Verify number of records displayed.");
                //   eqiupment_EquipmentHelper.VerifyText("No.ofRecords", "Showing 1 - 50 of");
                eqiupment_EquipmentHelper.ShowResult(50);
                eqiupment_EquipmentHelper.WaitForWorkAround(3000);

                executionLog.Log("EquipmentsAdvanceFilterResultsPP", "Click on advance filter.");
                eqiupment_EquipmentHelper.ClickElement("AdvanceFilter");
                eqiupment_EquipmentHelper.WaitForWorkAround(3000);

                executionLog.Log("EquipmentsAdvanceFilterResultsPP", "Select number of records to 100.");
                eqiupment_EquipmentHelper.SelectByText("ResultsPerPage", "100");
                eqiupment_EquipmentHelper.WaitForWorkAround(3000);

                executionLog.Log("EquipmentsAdvanceFilterResultsPP", "Click on Apply button.");
                eqiupment_EquipmentHelper.ClickElement("Apply");
                eqiupment_EquipmentHelper.WaitForWorkAround(3000);

                executionLog.Log("EquipmentsAdvanceFilterResultsPP", "Verify number of records displayed.");
                //   eqiupment_EquipmentHelper.VerifyText("No.ofRecords", "Showing 1 - 100 of");
                eqiupment_EquipmentHelper.ShowResult(100);
                eqiupment_EquipmentHelper.WaitForWorkAround(3000);

                executionLog.Log("EquipmentsAdvanceFilterResultsPP", "Logout from the application.");
                VisitOffice("logout");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("EquipmentsAdvanceFilterResultsPP");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Equipments Advance Filter ResultsPP");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Equipments Advance Filter ResultsPP", "Bug", "Medium", "Opportunities page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Equipments Advance Filter ResultsPP");
                        TakeScreenshot("EquipmentsAdvanceFilterResultsPP");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EquipmentsAdvanceFilterResultsPP.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("EquipmentsAdvanceFilterResultsPP");
                        string id = loginHelper.getIssueID("Equipments Advance Filter ResultsPP");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EquipmentsAdvanceFilterResultsPP.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Equipments Advance Filter ResultsPP"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Equipments Advance Filter ResultsPP");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("EquipmentsAdvanceFilterResultsPP");
                executionLog.WriteInExcel("Equipments Advance Filter ResultsPP", Status, JIRA, "Opportunities Management");
            }
        }
    }
}