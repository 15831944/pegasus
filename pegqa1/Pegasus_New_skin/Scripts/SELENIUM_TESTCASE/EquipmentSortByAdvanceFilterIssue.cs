using System;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class EquipmentSortByAdvanceFilterIssue : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("SELENIUM_TESTCASE")]
        [TestCategory("TS8")]
        public void equipmentSortByAdvanceFilterIssue()
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

                executionLog.Log("EquipmentSortByAdvanceFilterIssue", "Login with valid credential  Username");
                Login(username[0], password[0]);

                executionLog.Log("EquipmentSortByAdvanceFilterIssue", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");
                eqiupment_EquipmentHelper.WaitForWorkAround(6000);

                executionLog.Log("EquipmentSortByAdvanceFilterIssue", "Goto Equipments.");
                VisitOffice("equipment");
                eqiupment_EquipmentHelper.WaitForWorkAround(2000);

                executionLog.Log("EquipmentSortByAdvanceFilterIssue", "Open Advance filter.");
                eqiupment_EquipmentHelper.ClickElement("AdvanceFilter");
                eqiupment_EquipmentHelper.WaitForWorkAround(2000);

                executionLog.Log("EquipmentSortByAdvanceFilterIssue", "Click on Modifier in Available column.");
                eqiupment_EquipmentHelper.ClickElement("Col_Modifier");

                executionLog.Log("EquipmentSortByAdvanceFilterIssue", "Click on Add Icon to add from  to display column.");
                eqiupment_EquipmentHelper.Clickjs("AddCols");

                executionLog.Log("EquipmentSortByAdvanceFilterIssue", "Click on Medified By in Available column.");
                eqiupment_EquipmentHelper.ClickElement("Col_ModifiedBy");

                executionLog.Log("EquipmentSortByAdvanceFilterIssue", "Click on Add Icon to add from  to display column.");
                eqiupment_EquipmentHelper.Clickjs("AddCols");
                eqiupment_EquipmentHelper.WaitForWorkAround(2000);

                executionLog.Log("EquipmentSortByAdvanceFilterIssue", "Verify Modifier display in Sort by dropdown.");
                eqiupment_EquipmentHelper.OptionPresentInDropdown("Modifier");

                executionLog.Log("EquipmentSortByAdvanceFilterIssue", "Verify Modified by display in Sort by drop down.");
                eqiupment_EquipmentHelper.OptionPresentInDropdown("Modified By");

                executionLog.Log("EquipmentSortByAdvanceFilterIssue", "Open Reset button.");
                eqiupment_EquipmentHelper.ClickElement("ResetAdvnceFilter");
                eqiupment_EquipmentHelper.WaitForWorkAround(2000);

                executionLog.Log("EquipmentSortByAdvanceFilterIssue", "Open Advance filter.");
                eqiupment_EquipmentHelper.ClickElement("AdvanceFilter");
                eqiupment_EquipmentHelper.WaitForWorkAround(2000);

                executionLog.Log("EquipmentSortByAdvanceFilterIssue", "Verify Modifier not display in Sort by dropdown.");
                eqiupment_EquipmentHelper.OptionNotPresentInDropdown("//*[@id='order']/option", "Modifier");

                executionLog.Log("EquipmentSortByAdvanceFilterIssue", "Verify Modified By not display in Sort by dropdown.");
                eqiupment_EquipmentHelper.OptionNotPresentInDropdown("//*[@id='order']/option", "Modified By");

            }

            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("EquipmentSortByAdvanceFilterIssue");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Equipment SortBy Advance Filter Issue");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Equipment SortBy Advance Filter Issue", "Bug", "Medium", "Amex page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Equipment SortBy Advance Filter Issue");
                        TakeScreenshot("EquipmentSortByAdvanceFilterIssue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EquipmentSortByAdvanceFilterIssue.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("EquipmentSortByAdvanceFilterIssue");
                        string id = loginHelper.getIssueID("Equipment SortBy Advance Filter Issue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EquipmentSortByAdvanceFilterIssue.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Equipment SortBy Advance Filter Issue"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Equipment SortBy Advance Filter Issue");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("EquipmentSortByAdvanceFilterIssue");
                executionLog.WriteInExcel("Equipment SortBy Advance Filter Issue", Status, JIRA, "Office Equipments");
            }
        }
    }
}
