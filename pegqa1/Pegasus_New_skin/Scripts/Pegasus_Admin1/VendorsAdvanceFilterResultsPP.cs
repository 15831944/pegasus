using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class VendorsAdvanceFilterResultsPP : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin1")]
        [TestCategory("TS7")]
        [TestCategory("Pegasus_Admin1")]
        public void vendorsAdvanceFilterResultsPP()
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
            String JIRA = "";
            String Status = "Pass";

            try
            {
            executionLog.Log("VendorsAdvanceFilterResultsPP", "Login with valid username and password");
            Login(username[0], password[0]);
            Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

            executionLog.Log("VendorsAdvanceFilterResultsPP", "Verify Page title");
            VerifyTitle("Dashboard");
            Console.WriteLine("Redirected at Dashboard screen.");

            executionLog.Log("VendorsAdvanceFilterResultsPP", "Redirect at employee page.");
            VisitOffice("vendors");
            equipment_VendorsHelper.WaitForWorkAround(3000);

            executionLog.Log("VendorsAdvanceFilterResultsPP", "Verify page title.");
            VerifyTitle("Vendors");
            equipment_VendorsHelper.WaitForElementVisible("AdvanceFilter", 05);

            executionLog.Log("VendorsAdvanceFilterResultsPP", "Click on advance filter.");
            equipment_VendorsHelper.ClickElement("AdvanceFilter");
            equipment_VendorsHelper.WaitForWorkAround(2000);

            executionLog.Log("VendorsAdvanceFilterResultsPP", "Select number of records to 10.");
            equipment_VendorsHelper.SelectByText("ResultsPerPage", "10");
            //equipment_VendorsHelper.WaitForWorkAround(3000);

            executionLog.Log("VendorsAdvanceFilterResultsPP", "Click on Apply button.");
            equipment_VendorsHelper.ClickElement("Apply");
            equipment_VendorsHelper.WaitForWorkAround(3000);

            executionLog.Log("VendorsAdvanceFilterResultsPP", "Verify number of records displayed.");
            equipment_VendorsHelper.VerifyText("No.ofRecords", "Showing 1 - 10 of");
            //equipment_VendorsHelper.WaitForWorkAround(3000);

            executionLog.Log("VendorsAdvanceFilterResultsPP", "Click on advance filter.");
            equipment_VendorsHelper.ClickElement("AdvanceFilter");
            equipment_VendorsHelper.WaitForWorkAround(2000);

            executionLog.Log("VendorsAdvanceFilterResultsPP", "Select number of records to 20.");
            equipment_VendorsHelper.SelectByText("ResultsPerPage", "20");
            //equipment_VendorsHelper.WaitForWorkAround(3000);

            executionLog.Log("VendorsAdvanceFilterResultsPP", "Click on Apply button.");
            equipment_VendorsHelper.ClickElement("Apply");
            equipment_VendorsHelper.WaitForWorkAround(3000);

            executionLog.Log("VendorsAdvanceFilterResultsPP", "Verify number of records displayed.");
            equipment_VendorsHelper.VerifyText("No.ofRecords", "Showing 1 - 20 of");
            //equipment_VendorsHelper.WaitForWorkAround(3000);

            executionLog.Log("VendorsAdvanceFilterResultsPP", "Click on advance filter.");
            equipment_VendorsHelper.ClickElement("AdvanceFilter");
            equipment_VendorsHelper.WaitForWorkAround(2000);

            executionLog.Log("VendorsAdvanceFilterResultsPP", "Select number of records to 50.");
            equipment_VendorsHelper.SelectByText("ResultsPerPage", "50");
            //equipment_VendorsHelper.WaitForWorkAround(3000);

            executionLog.Log("VendorsAdvanceFilterResultsPP", "Click on Apply button.");
            equipment_VendorsHelper.ClickElement("Apply");
            equipment_VendorsHelper.WaitForWorkAround(3000);

            executionLog.Log("VendorsAdvanceFilterResultsPP", "Verify number of records displayed.");
            equipment_VendorsHelper.VerifyText("No.ofRecords", "Showing 1 - 50 of");
            //equipment_VendorsHelper.WaitForWorkAround(3000);

            executionLog.Log("VendorsAdvanceFilterResultsPP", "Click on advance filter.");
            equipment_VendorsHelper.ClickElement("AdvanceFilter");
            equipment_VendorsHelper.WaitForWorkAround(2000);

            executionLog.Log("VendorsAdvanceFilterResultsPP", "Select number of records to 100.");
            equipment_VendorsHelper.SelectByText("ResultsPerPage", "100");
            //equipment_VendorsHelper.WaitForWorkAround(3000);

            executionLog.Log("VendorsAdvanceFilterResultsPP", "Click on Apply button.");
            equipment_VendorsHelper.ClickElement("Apply");
            equipment_VendorsHelper.WaitForWorkAround(3000);

            executionLog.Log("VendorsAdvanceFilterResultsPP", "Verify number of records displayed.");
            equipment_VendorsHelper.VerifyText("No.ofRecords", "Showing 1 - 100 of");
            //equipment_VendorsHelper.WaitForWorkAround(3000);

            executionLog.Log("VendorsAdvanceFilterResultsPP", "Logout from the application.");
            VisitOffice("logout");

        }
      catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VendorsAdvanceFilterResultsPP");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Vendors Advance Filter ResultsPP");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Vendors Advance Filter ResultsPP", "Bug", "Medium", "Opportunities page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Vendors Advance Filter ResultsPP");
                        TakeScreenshot("VendorsAdvanceFilterResultsPP");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VendorsAdvanceFilterResultsPP.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VendorsAdvanceFilterResultsPP");
                        string id = loginHelper.getIssueID("Vendors Advance Filter ResultsPP");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VendorsAdvanceFilterResultsPP.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Vendors Advance Filter ResultsPP"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Vendors Advance Filter ResultsPP");
                executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VendorsAdvanceFilterResultsPP");
                executionLog.WriteInExcel("Vendors Advance Filter ResultsPP", Status, JIRA, "Opportunities Management");
            }
        }
    }
} 