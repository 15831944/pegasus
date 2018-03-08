using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class VerifyEquipmentsAdvanceFilterColumnOrder : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin1")]
        [TestCategory("TS7")]
        [TestCategory("Pegasus_Admin1")]
        public void verifyEquipmentsAdvanceFilterColumnOrder()
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

            // Variable Random
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("VerifyEquipmentsAdvanceFilterColumnOrder", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyEquipmentsAdvanceFilterColumnOrder", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyEquipmentsAdvanceFilterColumnOrder", "Redirect To URL");
                VisitOffice("equipment");
                eqiupment_EquipmentHelper.WaitForWorkAround(5000);

                executionLog.Log("VerifyEquipmentsAdvanceFilterColumnOrder", "Verify page title.");
                VerifyTitle("Equipment");

                executionLog.Log("VerifyEquipmentsAdvanceFilterColumnOrder", "Verify status column is visible on the page..");
                eqiupment_EquipmentHelper.IsElementPresent("HeadStatus");
                eqiupment_EquipmentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyEquipmentsAdvanceFilterColumnOrder", "Verify manufacturer name column is visible on the page.");
                eqiupment_EquipmentHelper.IsElementPresent("HeadManufacturer");
                eqiupment_EquipmentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyEquipmentsAdvanceFilterColumnOrder", "Verify model column is visible on the page.");
                eqiupment_EquipmentHelper.IsElementPresent("HeadModel");
                eqiupment_EquipmentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyEquipmentsAdvanceFilterColumnOrder", "Verify modified column is visible on the page.");
                eqiupment_EquipmentHelper.IsElementPresent("HeadModified");
                eqiupment_EquipmentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyEquipmentsAdvanceFilterColumnOrder", "Click on advance filter.");
                eqiupment_EquipmentHelper.ClickElement("AdvanceFilter");
                eqiupment_EquipmentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyEquipmentsAdvanceFilterColumnOrder", "Select status in displayed columns.");
                eqiupment_EquipmentHelper.SelectByText("DisplayedCols", "Status");
                eqiupment_EquipmentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyEquipmentsAdvanceFilterColumnOrder", "Click arrow to move column to avail cols.");
                eqiupment_EquipmentHelper.ClickElement("RemoveCols");
                eqiupment_EquipmentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyEquipmentsAdvanceFilterColumnOrder", "Select manufacturer name in displayed columns.");
                eqiupment_EquipmentHelper.SelectByText("DisplayedCols", "Manufacturer Name");
                eqiupment_EquipmentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyEquipmentsAdvanceFilterColumnOrder", "Click arrow to move column to avail cols");
                eqiupment_EquipmentHelper.ClickElement("RemoveCols");
                eqiupment_EquipmentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyEquipmentsAdvanceFilterColumnOrder", "Select Model in displayed columns.");
                eqiupment_EquipmentHelper.SelectByText("DisplayedCols", "Model");
                eqiupment_EquipmentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyEquipmentsAdvanceFilterColumnOrder", "Click arrow to move column to avail cols");
                eqiupment_EquipmentHelper.ClickElement("RemoveCols");
                eqiupment_EquipmentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyEquipmentsAdvanceFilterColumnOrder", "Select modified in displayed columns.");
                eqiupment_EquipmentHelper.SelectByText("DisplayedCols", "Modified");
                eqiupment_EquipmentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyEquipmentsAdvanceFilterColumnOrder", "Click arrow to move column to avail cols");
                eqiupment_EquipmentHelper.ClickElement("RemoveCols");
                eqiupment_EquipmentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyEquipmentsAdvanceFilterColumnOrder", "Click on Apply button.");
                eqiupment_EquipmentHelper.ClickElement("Apply");
                eqiupment_EquipmentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyEquipmentsAdvanceFilterColumnOrder", "Verify status not present on page.");
                eqiupment_EquipmentHelper.IsElementNotPresent("HeadStatus");
                eqiupment_EquipmentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyEquipmentsAdvanceFilterColumnOrder", "Verify manufacturer name not present on page.");
                eqiupment_EquipmentHelper.IsElementNotPresent("HeadManufacturer");
                eqiupment_EquipmentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyEquipmentsAdvanceFilterColumnOrder", "Verify model not present on page.");
                eqiupment_EquipmentHelper.IsElementNotPresent("HeadModel");
                eqiupment_EquipmentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyEquipmentsAdvanceFilterColumnOrder", "Verify modified not present on page.");
                eqiupment_EquipmentHelper.IsElementNotPresent("HeadModified");
                eqiupment_EquipmentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyEquipmentsAdvanceFilterColumnOrder", "Redirect at leads page.");
                VisitOffice("leads");

                executionLog.Log("VerifyEquipmentsAdvanceFilterColumnOrder", "Verify page title as leads.");
                VerifyTitle("Leads");

                executionLog.Log("VerifyEquipmentsAdvanceFilterColumnOrder", "Redirect To URL");
                VisitOffice("equipment");
                eqiupment_EquipmentHelper.WaitForWorkAround(5000);

                executionLog.Log("VerifyEquipmentsAdvanceFilterColumnOrder", "Verify page title as equipments.");
                VerifyTitle("Equipment");

                executionLog.Log("VerifyEquipmentsAdvanceFilterColumnOrder", "Verify default position of manufacturer name column.");
                eqiupment_EquipmentHelper.IsElementPresent("HeadManufacturer5");
                eqiupment_EquipmentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyEquipmentsAdvanceFilterColumnOrder", "Verify default position of model column.");
                eqiupment_EquipmentHelper.IsElementPresent("HeadModel6");
                eqiupment_EquipmentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyEquipmentsAdvanceFilterColumnOrder", "Redirect at equipment page again.");
                VisitOffice("equipment");
                eqiupment_EquipmentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyEquipmentsAdvanceFilterColumnOrder", "Click on advance filter.");
                eqiupment_EquipmentHelper.ClickElement("AdvanceFilter");
                eqiupment_EquipmentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyEquipmentsAdvanceFilterColumnOrder", "Select manufacturer name in displayed column.");
                eqiupment_EquipmentHelper.SelectByText("DisplayedCols", "Manufacturer Name");
                eqiupment_EquipmentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyEquipmentsAdvanceFilterColumnOrder", "Move manufacturer name 1 step up.");
                eqiupment_EquipmentHelper.ClickElement("MoveUp");
                eqiupment_EquipmentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyEquipmentsAdvanceFilterColumnOrder", "Move manufacturer name 1 step up.");
                eqiupment_EquipmentHelper.ClickElement("MoveUp");
                eqiupment_EquipmentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyEquipmentsAdvanceFilterColumnOrder", "Move manufacturer name 1 step up.");
                eqiupment_EquipmentHelper.ClickElement("MoveUp");
                eqiupment_EquipmentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyEquipmentsAdvanceFilterColumnOrder", "Select model in displayed column.");
                eqiupment_EquipmentHelper.SelectByText("DisplayedCols", "Model");
                eqiupment_EquipmentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyEquipmentsAdvanceFilterColumnOrder", "Move model 1 step down.");
                eqiupment_EquipmentHelper.ClickElement("MoveDown");
                eqiupment_EquipmentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyEquipmentsAdvanceFilterColumnOrder", "Click on Apply button.");
                eqiupment_EquipmentHelper.ClickElement("Apply");
                eqiupment_EquipmentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyEquipmentsAdvanceFilterColumnOrder", "Verify changed position of manufacturer name column.");
                eqiupment_EquipmentHelper.IsElementPresent("HeadManufacturer3");
                eqiupment_EquipmentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyEquipmentsAdvanceFilterColumnOrder", "Verify changed position of model column.");
                eqiupment_EquipmentHelper.IsElementPresent("HeadModel7");
                eqiupment_EquipmentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyEquipmentsAdvanceFilterColumnOrder", "Logout from the application.");
                VisitOffice("logout");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyEquipmentsAdvanceFilterColumnOrder");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Equipments Advance Filter Column Order");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Equipments Advance Filter Column Order", "Bug", "Medium", "Activities page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Equipments Advance Filter Column Order");
                        TakeScreenshot("VerifyEquipmentsAdvanceFilterColumnOrder");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyEquipmentsAdvanceFilterColumnOrder.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyEquipmentsAdvanceFilterColumnOrder");
                        string id = loginHelper.getIssueID("Verify Equipments Advance Filter Column Order");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyEquipmentsAdvanceFilterColumnOrder.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Equipments Advance Filter Column Order"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Equipments Advance Filter Column Order");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyEquipmentsAdvanceFilterColumnOrder");
                executionLog.WriteInExcel("Verify Equipments Advance Filter Column Order", Status, JIRA, "Meetings Management");
            }
        }
    }
}