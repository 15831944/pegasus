/* Documented by Khalil Shakir
* 
* The AdminCheckFieldRelationships script tests the Pegasus Admin Portal.
* It goes through specific sections of the Admin Portal where selecting certain options for certain fields
* will automatically reveal new fields in Pegasus. 
* The AdminFieldRelationships.xml file stores the locators for this script. 
*/

using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OpenQA.Selenium.Support.UI;
using PegasusTests.Locators;
using System.IO;

namespace Pegasus_New_skin.Scripts.Pegasus_Admin
{
    [TestClass]
    public class AdminCheckFieldRelationships : DriverTestCase
    {

        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin")]
        [TestCategory("Fail")]
        [TestCategory("TS6")]
        [TestCategory("Pegasus_Admin")]
        public void checkFieldRelationships()
        {
            string[] username = null;
            string[] password = null;

            XMLParse oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            ExecutionLog executionLog = new ExecutionLog();
            LoginHelper loginHelper = new LoginHelper(GetWebDriver());
            AdminCheckFieldRelationshipsHelper adminHelper = new AdminCheckFieldRelationshipsHelper(GetWebDriver());

            // Testing Variables
            string[] dateDropdownArray = { "Yesterday", "ThisWeek", "ThisMonth", "LastMonth", "ThisQuarter", "LastQuarter", "ThisYear", "LastYear", "Last12Months", "Custom" };

            var name = "Testing Subject" + GetRandomNumber();
            var email = "Test" + GetRandomNumber() + "@gmail.com";
            String JIRA = "";
            String Status = "Pass";

            try
            {
                // Initiating Test
                executionLog.Log("AdminCheckFieldRelationships", "Login");
                Login(username[0], password[0]);

                executionLog.Log("AdminCheckFieldRelationships", "Write information");
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("AdminCheckFieldRelationships", "Checking Master Data Tab");
                VerifyTitle("Dashboard");

                executionLog.Log("AdminCheckFieldRelationships", "Verify Office admin");
                VisitOffice("admin");

                // Testing Rates and Fees Section
                executionLog.Log("AdminCheckFieldRelationships", "Checking Master Data Tab");

                VisitOffice("rates_fees");
                adminHelper.WaitForWorkAround(3000);

                executionLog.Log("AdminCheckFieldRelationships", "Checking Temp1234");
                adminHelper.ClickElement("Temp1234");

                executionLog.Log("AdminCheckFieldRelationships", "Wait");
                adminHelper.WaitForWorkAround(6000);

                executionLog.Log("AdminCheckFieldRelationships", "Clicking Processor Type");
                adminHelper.SelectByText("ProcessorType", "First Data Omaha");
                adminHelper.WaitForWorkAround(2000);

                executionLog.Log("AdminCheckFieldRelationships", "Clicking FirstDataOmahaOption");
                adminHelper.ClickElement("FirstDataOmahaOption");
                adminHelper.WaitForWorkAround(3000);

                executionLog.Log("AdminCheckFieldRelationships", "Waiting for  DiscountCollected");
                adminHelper.ElementVisible("DiscountCollected");
                adminHelper.WaitForWorkAround(3000);

                executionLog.Log("AdminCheckFieldRelationships", "Clicking FirstDataNorthOption");
                adminHelper.ClickElement("FirstDataNorthOption");
                adminHelper.WaitForWorkAround(3000);

                executionLog.Log("AdminCheckFieldRelationships", "Clicking DiscountFrequency");
                adminHelper.ElementVisible("DiscountFrequency");

                VisitOffice("user_statistics");
                adminHelper.WaitForWorkAround(3000);

                for (int i = 0; i < dateDropdownArray.Length - 1; i++)
                {
                    executionLog.Log("AdminCheckFieldRelationships", "Clicking DateDropDown");
                    adminHelper.ClickElement("DateDropDown");

                    executionLog.Log("AdminCheckFieldRelationships", ("Clicking" + dateDropdownArray[i]));
                    adminHelper.ClickElement(dateDropdownArray[i]);

                    executionLog.Log("AdminCheckFieldRelationships", "Wait");
                    adminHelper.WaitForWorkAround(3000);
                }

                VisitOffice("tickets/settings");
                adminHelper.WaitForWorkAround(3000);

                executionLog.Log("AdminCheckFieldRelationships", "Clicking ReportingTimeStamp");
                adminHelper.ClickElement("ReportingTimeStamp");

                executionLog.Log("AdminCheckFieldRelationships", "Clicking StatusChangeOption");
                adminHelper.ClickElement("StatusChangeOption");

                executionLog.Log("AdminCheckFieldRelationships", "Waiting for SelectStatus");
                adminHelper.ElementVisible("SelectStatus");

                executionLog.Log("AdminCheckFieldRelationships", "Clicking ClosedTimeStamp");
                adminHelper.ClickElement("ClosedTimeStamp");

                executionLog.Log("AdminCheckFieldRelationships", "Clicking StatusChangeTimeStamp");
                adminHelper.ClickElement("StatusChangeTimeStamp");

                executionLog.Log("AdminCheckFieldRelationships", "Waiting for SelectStatus");
                adminHelper.ElementVisible("SelectStatus");

                VisitOffice("sections");
                adminHelper.WaitForWorkAround(3000);
                executionLog.Log("AdminCheckFieldRelationships", "Clicking SelectModule");
                adminHelper.ClickElement("SelectModule");

                executionLog.Log("AdminCheckFieldRelationships", "Clicking ClientsOption");
                adminHelper.ClickElement("ClientsOption");

                executionLog.Log("AdminCheckFieldRelationships", "Clicking CompanyAddress");
                adminHelper.ElementVisible("CompanyAddress");

                executionLog.Log("AdminCheckFieldRelationships", "Clicking LeadsOption");
                adminHelper.ClickElement("LeadsOption");

                executionLog.Log("AdminCheckFieldRelationships", "Waiting for Processor");
                adminHelper.ElementVisible("Processor");

                executionLog.Log("AdminCheckFieldRelationships", "Clicking OpportunitiesOption");
                adminHelper.ClickElement("OpportunitiesOption");

                executionLog.Log("AdminCheckFieldRelationships", "Waiting for Details");
                adminHelper.ElementVisible("Details");

                executionLog.Log("AdminCheckFieldRelationships", "Wait");
                VisitOffice("fields");
                adminHelper.WaitForWorkAround(3000);

                adminHelper.SelectByText("Module", "Clients");
                adminHelper.WaitForWorkAround(3000);

                executionLog.Log("AdminCheckFieldRelationships", "Clicking Search");
                adminHelper.ClickElement("Search");
                adminHelper.WaitForWorkAround(5000);

                executionLog.Log("AdminCheckFieldRelationships", "Waiting for AmexVolume");
                adminHelper.ElementVisible("AmexVolume");

                executionLog.Log("AdminCheckFieldRelationships", "Clicking Module");
                adminHelper.ClickElement("Module");
                adminHelper.WaitForWorkAround(4000);

                executionLog.Log("AdminCheckFieldRelationships", "Clicking Leads");
                adminHelper.ClickElement("Leads");
                adminHelper.WaitForWorkAround(4000);

                executionLog.Log("AdminCheckFieldRelationships", "Clicking Search");
                adminHelper.ClickElement("Search");
                adminHelper.WaitForWorkAround(4000);

                executionLog.Log("AdminCheckFieldRelationships", "Waiting for Salutation");
                adminHelper.ElementVisible("Salutation");

                executionLog.Log("AdminCheckFieldRelationships", "Clicking Module");
                adminHelper.ClickElement("Module");
                executionLog.Log("AdminCheckFieldRelationships", "Wait");
                adminHelper.WaitForWorkAround(3000);

                // Checking opportuniteis 
                executionLog.Log("AdminCheckFieldRelationships", "Clicking Opportunities");
                adminHelper.ClickElement("Opportunities");

                executionLog.Log("AdminCheckFieldRelationships", "Wait");
                adminHelper.WaitForWorkAround(4000);

                executionLog.Log("AdminCheckFieldRelationships", "Clicking Search");
                adminHelper.ClickElement("Search");
                adminHelper.WaitForWorkAround(4000);

                // Validating change 
                executionLog.Log("AdminCheckFieldRelationships", "Waiting for OpportunityName");
                adminHelper.ElementVisible("OpportunityName");

                VisitOffice("field_order_management");
                adminHelper.WaitForWorkAround(3000);

                executionLog.Log("AdminCheckFieldRelationships", "Clicking Module2");
                adminHelper.ClickElement("Module2");
                adminHelper.WaitForWorkAround(3000);

                // Viewing a client
                executionLog.Log("AdminCheckFieldRelationships", "Clicking Clients2");
                adminHelper.ClickElement("Clients2");
                adminHelper.WaitForWorkAround(3000);

                executionLog.Log("AdminCheckFieldRelationships", "Clicking Tab");
                adminHelper.ClickElement("Tab");
                adminHelper.WaitForWorkAround(3000);

                executionLog.Log("AdminCheckFieldRelationships", "Clicking CompanyDetails2");
                adminHelper.ClickElement("CompanyDetails2");
                adminHelper.WaitForWorkAround(3000);

                executionLog.Log("AdminCheckFieldRelationships", "Clicking Section");
                adminHelper.ClickElement("Section");
                adminHelper.WaitForWorkAround(3000);

                executionLog.Log("AdminCheckFieldRelationships", "Clicking CompanyDetails3");
                adminHelper.ClickElement("CompanyDetails3");
                adminHelper.WaitForWorkAround(5000);

                executionLog.Log("AdminCheckFieldRelationships", "Clicking Search2");
                adminHelper.ClickElement("Search2");
                adminHelper.WaitForWorkAround(5000);

                // Validating change 
                executionLog.Log("AdminCheckFieldRelationships", "Waiting for BusinessDBAName");
                adminHelper.ElementVisible("BusinessDBAName");

                // Beginning Aslam's code for JIRA
            }
            catch (Exception e)
            {

                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("AdminCheckFieldRelationships");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Admin Check Field Relationships");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Admin Check Field Relationships", "Bug", "Medium", "Document page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Admin Check Field Relationships");
                        TakeScreenshot("AdminCheckFieldRelationships");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AdminCheckFieldRelationships.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("AdminCheckFieldRelationships");
                        string id = loginHelper.getIssueID("Admin Check Field Relationships");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AdminCheckFieldRelationships.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Admin Check Field Relationships"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Admin Check Field Relationships");
              //  executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("AdminCheckFieldRelationships");
                executionLog.WriteInExcel("Admin Check Field Relationships", Status, JIRA, "Office Activities");
            }
        }
        // Ending Aslam's code for JIRA
    }

}