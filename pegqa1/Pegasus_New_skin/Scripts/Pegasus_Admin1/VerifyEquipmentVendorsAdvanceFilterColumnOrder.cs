using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class VerifyEquipmentVendorsAdvanceFilterColumnOrder : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin1")]
        [TestCategory("TS7")]
        [TestCategory("Pegasus_Admin1")]
        public void verifyEquipmentVendorsAdvanceFilterColumnOrder()
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

            // Variable Random
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("VerifyEquipmentVendorsAdvanceFilterColumnOrder", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyEquipmentVendorsAdvanceFilterColumnOrder", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyEquipmentVendorsAdvanceFilterColumnOrder", "Redirect To URL");
                VisitOffice("vendors");
                equipment_VendorsHelper.WaitForWorkAround(5000);

                executionLog.Log("VerifyEquipmentVendorsAdvanceFilterColumnOrder", "Verify page title.");
                VerifyTitle("Vendors");

                executionLog.Log("VerifyEquipmentVendorsAdvanceFilterColumnOrder", "Verify status column is visible on the page..");
                equipment_VendorsHelper.IsElementPresent("HeadStatus");
                equipment_VendorsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyEquipmentVendorsAdvanceFilterColumnOrder", "Verify last name column is visible on the page.");
                equipment_VendorsHelper.IsElementPresent("HeadLastName");
                equipment_VendorsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyEquipmentVendorsAdvanceFilterColumnOrder", "Verify first name column is visible on the page.");
                equipment_VendorsHelper.IsElementPresent("HeadFirstName");
                equipment_VendorsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyEquipmentVendorsAdvanceFilterColumnOrder", "Verify modified column is visible on the page.");
                equipment_VendorsHelper.IsElementPresent("HeadModified");
                equipment_VendorsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyEquipmentVendorsAdvanceFilterColumnOrder", "Click on advance filter.");
                equipment_VendorsHelper.ClickElement("AdvanceFilter");
                equipment_VendorsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyEquipmentVendorsAdvanceFilterColumnOrder", "Select status in displayed columns.");
                equipment_VendorsHelper.SelectByText("DisplayedCols", "Status");
                equipment_VendorsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyEquipmentVendorsAdvanceFilterColumnOrder", "Click arrow to move column to avail cols.");
                equipment_VendorsHelper.ClickElement("RemoveCols");
                equipment_VendorsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyEquipmentVendorsAdvanceFilterColumnOrder", "Select first name in displayed columns.");
                equipment_VendorsHelper.SelectByText("DisplayedCols", "First Name");
                equipment_VendorsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyEquipmentVendorsAdvanceFilterColumnOrder", "Click arrow to move column to avail cols");
                equipment_VendorsHelper.ClickElement("RemoveCols");
                equipment_VendorsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyEquipmentVendorsAdvanceFilterColumnOrder", "Select last name in displayed columns.");
                equipment_VendorsHelper.SelectByText("DisplayedCols", "Last Name");
                equipment_VendorsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyEquipmentVendorsAdvanceFilterColumnOrder", "Click arrow to move column to avail cols");
                equipment_VendorsHelper.ClickElement("RemoveCols");
                equipment_VendorsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyEquipmentVendorsAdvanceFilterColumnOrder", "Select modified in displayed columns.");
                equipment_VendorsHelper.SelectByText("DisplayedCols", "Modified");
                equipment_VendorsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyEquipmentVendorsAdvanceFilterColumnOrder", "Click arrow to move column to avail cols");
                equipment_VendorsHelper.ClickElement("RemoveCols");
                equipment_VendorsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyEquipmentVendorsAdvanceFilterColumnOrder", "Click on Apply button.");
                equipment_VendorsHelper.ClickElement("Apply");
                equipment_VendorsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyEquipmentVendorsAdvanceFilterColumnOrder", "Verify status not present on page.");
                equipment_VendorsHelper.IsElementNotPresent("HeadStatus");
                equipment_VendorsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyEquipmentVendorsAdvanceFilterColumnOrder", "Verify last name not present on page.");
                equipment_VendorsHelper.IsElementNotPresent("HeadLastName");
                equipment_VendorsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyEquipmentVendorsAdvanceFilterColumnOrder", "Verify first name not present on page.");
                equipment_VendorsHelper.IsElementNotPresent("HeadFirstName");
                equipment_VendorsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyEquipmentVendorsAdvanceFilterColumnOrder", "Verify modified not present on page.");
                equipment_VendorsHelper.IsElementNotPresent("HeadModified");
                equipment_VendorsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyEquipmentVendorsAdvanceFilterColumnOrder", "Redirect at leads page.");
                VisitOffice("leads");

                executionLog.Log("VerifyEquipmentVendorsAdvanceFilterColumnOrder", "Verify page title as leads.");
                VerifyTitle("Leads");

                executionLog.Log("VerifyEquipmentVendorsAdvanceFilterColumnOrder", "Redirect To URL");
                VisitOffice("vendors");
                equipment_VendorsHelper.WaitForWorkAround(5000);

                executionLog.Log("VerifyEquipmentVendorsAdvanceFilterColumnOrder", "Verify page title.");
                VerifyTitle("Vendors");

                executionLog.Log("VerifyEquipmentVendorsAdvanceFilterColumnOrder", "Verify default position of last name name column.");
                equipment_VendorsHelper.IsElementPresent("HeadLastName4");
                equipment_VendorsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyEquipmentVendorsAdvanceFilterColumnOrder", "Verify default position of status column.");
                equipment_VendorsHelper.IsElementPresent("HeadStatus5");
                equipment_VendorsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyEquipmentVendorsAdvanceFilterColumnOrder", "Redirect at vendors page again.");
                VisitOffice("vendors");
                equipment_VendorsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyEquipmentVendorsAdvanceFilterColumnOrder", "Click on advance filter.");
                equipment_VendorsHelper.ClickElement("AdvanceFilter");
                equipment_VendorsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyEquipmentVendorsAdvanceFilterColumnOrder", "Select last name in displayed column.");
                equipment_VendorsHelper.SelectByText("DisplayedCols", "Last Name");
                equipment_VendorsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyEquipmentVendorsAdvanceFilterColumnOrder", "Move last name 1 step up.");
                equipment_VendorsHelper.ClickElement("MoveUp");
                equipment_VendorsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyEquipmentVendorsAdvanceFilterColumnOrder", "Move last name 1 step up.");
                equipment_VendorsHelper.ClickElement("MoveUp");
                equipment_VendorsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyEquipmentVendorsAdvanceFilterColumnOrder", "Move last name 1 step up.");
                equipment_VendorsHelper.ClickElement("MoveUp");
                equipment_VendorsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyEquipmentVendorsAdvanceFilterColumnOrder", "Select status in displayed column.");
                equipment_VendorsHelper.SelectByText("DisplayedCols", "Status");
                equipment_VendorsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyEquipmentVendorsAdvanceFilterColumnOrder", "Move status 1 step down.");
                equipment_VendorsHelper.ClickElement("MoveDown");
                equipment_VendorsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyEquipmentVendorsAdvanceFilterColumnOrder", "Click on Apply button.");
                equipment_VendorsHelper.ClickElement("Apply");
                equipment_VendorsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyEquipmentVendorsAdvanceFilterColumnOrder", "Verify changed position of last name name column.");
                equipment_VendorsHelper.IsElementPresent("HeadLastName2");
                equipment_VendorsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyEquipmentVendorsAdvanceFilterColumnOrder", "Verify changed position of status column.");
                equipment_VendorsHelper.IsElementPresent("HeadStatus6");
                equipment_VendorsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyEquipmentVendorsAdvanceFilterColumnOrder", "Logout from the application.");
                VisitOffice("logout");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyEquipmentVendorsAdvanceFilterColumnOrder");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Equipment Vendors Advance Filter Column Order");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Equipment Vendors Advance Filter Column Order", "Bug", "Medium", "Activities page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Equipment Vendors Advance Filter Column Order");
                        TakeScreenshot("VerifyEquipmentVendorsAdvanceFilterColumnOrder");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyEquipmentVendorsAdvanceFilterColumnOrder.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyEquipmentVendorsAdvanceFilterColumnOrder");
                        string id = loginHelper.getIssueID("Verify Equipment Vendors Advance Filter Column Order");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyEquipmentVendorsAdvanceFilterColumnOrder.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Equipment Vendors Advance Filter Column Order"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Equipment Vendors Advance Filter Column Order");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyEquipmentVendorsAdvanceFilterColumnOrder");
                executionLog.WriteInExcel("Verify Equipment Vendors Advance Filter Column Order", Status, JIRA, "Meetings Management");
            }
        }
    }
}