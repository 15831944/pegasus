/*
* AdminSetValidationDate.cs is a test that checks to see if a field
* can have its field validation changed to only accept dat formatted
* input. It works along with AdminSetValidationDateHelper.cs and
* AdminSetValidationDate.xml
*
*
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

namespace Pegasus_New_skin.Scripts.Pegasus_Admin
{
    // Initial action, to verify the field that will be changed and to make the initial change

    [TestClass]
    public class AdminSetValidationDate : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin")]
        [TestCategory("Fail")]
        [TestCategory("TS6")]
        [TestCategory("Pegasus_Admin")]
        public void setFieldToDate()
        {
            string[] username = null;
            string[] password = null;

            XMLParse oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            //Initializing the objects
            ExecutionLog executionLog = new ExecutionLog();
            LoginHelper loginHelper = new LoginHelper(GetWebDriver());
            var validate = new AdminSetFormatFieldsHelper(GetWebDriver());
            var office_ClientsHelper = new Office_ClientsHelper(GetWebDriver());

            String JIRA = "";
            String Status = "Pass";
            var DBAName = "DBA@Company" + RandomNumber(1, 999);

            try
            {

                //executionLog.Log("AdminSetValidationDate", "WaitForWorkAround");
                //validate.WaitForWorkAround(2000);

                executionLog.Log("AdminSetValidationDate", "Login");
                Login(username[0], password[0]);

                //executionLog.Log("AdminSetValidationDate", "WaitForWorkAround");
                //validate.WaitForWorkAround(4000);

                executionLog.Log("AdminSetValidationDate", "Verify Dashboard");
                VerifyTitle("Dashboard");

                //VisitOffice("clients");

                //executionLog.Log("AdminSetValidationDate", "Click CompanySearchBar");
                //validate.ClickElement("CompanySearchBar");

                //executionLog.Log("AdminSetValidationDate", "Click FirstClient");
                //validate.ClickElement("FirstClient");
                //validate.WaitForWorkAround(4000);

                //executionLog.Log("AdminSetValidationDate", "Click Contacts");
                //validate.ClickElement("Contacts");

                //executionLog.Log("AdminSetValidationDate", "Wait for Title");
                //validate.WaitForElementPresent("Title", 1);

                //executionLog.Log("AdminSetValidationDate", "Click Save");
                //validate.ClickElement("Save");

                //executionLog.Log("AdminSetValidationDate", "Visit office admin");
                //VisitOffice("admin");

                //executionLog.Log("AdminSetValidationDate", "WaitForWorkAround");
                //validate.WaitForWorkAround(2000);

                executionLog.Log("AdminSetValidationDate", "Navigate to fields");
                VisitOffice("fields");
                validate.WaitForWorkAround(3000);

                validate.SelectByText("Module", "Clients");
                validate.WaitForWorkAround(2000);

                executionLog.Log("AdminSetValidationDate", "Select Processor as First Data North");
                validate.SelectByText("Processor", "First Data North");
                validate.WaitForWorkAround(2000);

                executionLog.Log("AdminSetValidationDate", "Select Tab as Contacts");
                validate.SelectByText("Tab", "Contacts");
                validate.WaitForWorkAround(2000);

                executionLog.Log("AdminSetValidationDate", "Select Section as Contacts");
                validate.SelectByText("Section", "Contacts");
                validate.WaitForWorkAround(2000);

                executionLog.Log("AdminSetValidationDate", "Click Search");
                validate.ClickElement("Search");
                validate.WaitForWorkAround(3000);

                executionLog.Log("AdminSetValidationDate", "Type Text title");
                validate.TypeText("filter", "Title");

                executionLog.Log("AdminSetValidationDate", "WaitForWorkAround");
                validate.WaitForWorkAround(2000);

                executionLog.Log("AdminSetValidationDate", "Click TitleField");
                validate.ClickElement("ContactTitle");
                validate.WaitForWorkAround(4000);

                executionLog.Log("AdminSetValidationDate", "Click on Field format");
                validate.checkAndClick("ProcApplyDataTypeValidation");
                validate.WaitForWorkAround(1000);

                executionLog.Log("AdminSetFormatPhone", "Select data type as Date.");
                validate.SelectByText("ProcFieldDataType", "Date");
                //validate.WaitForWorkAround(3000);

                executionLog.Log("AdminSetValidationDate", "Click SaveFieldManager");
                validate.ClickElement("SaveFieldManager");

                executionLog.Log("AdminSetValidationDate", "WaitForWorkAround");
                validate.WaitForWorkAround(3000);

                executionLog.Log("AdminSetValidationDate", "Redirect at Create Client");
                VisitOffice("clients/create");
                validate.WaitForWorkAround(3000);

                executionLog.Log("AdminSetValidationDate", "Select Processor");
                office_ClientsHelper.SelectByText("CreateProc", "First Data North");

                executionLog.Log("AdminSetValidationDate", "Enter Client Dba Name");
                office_ClientsHelper.TypeText("ClientDBAName", DBAName);

                executionLog.Log("AdminSetValidationDate", "Select client status");
                office_ClientsHelper.SelectByText("ClientStatus", "New");

                executionLog.Log("AdminSetValidationDate", "Select Client Res[onsibility.");
                office_ClientsHelper.SelectByText("ClientResponsibility", "Howard Tang");

                executionLog.Log("AdminSetValidationDate", "Click on next button");
                office_ClientsHelper.ClickElement("Next");
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("AdminSetValidationDate", "Wait for confirmation message.");
                office_ClientsHelper.WaitForText("Client saved successfully. ", 10);

                executionLog.Log("AdminSetValidationDate", "Click On Contact Tab Clinet");
                office_ClientsHelper.ClickElement("ContactDetails");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("AdminSetValidationDate", "Wait for element present");
                validate.WaitForElementPresent("Title", 1);
                validate.WaitForWorkAround(2000);

                executionLog.Log("AdminSetValidationDate", "Redirect To clients page. ");
                VisitOffice("clients");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("AdminSetValidationDate", "Enter Company Name");
                office_ClientsHelper.TypeText("SearchClient", DBAName);
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("AdminSetValidationDate", "Select client by check box");
                office_ClientsHelper.ClickForce("ClickOn1stOpp");
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("AdminSetValidationDate", "Click on delete client");
                office_ClientsHelper.ClickElement("DeleteClient");

                executionLog.Log("AdminSetValidationDate", "Accept alert message.");
                office_ClientsHelper.AcceptAlert();

                executionLog.Log("AdminSetValidationDate", "Wait for success message.");
                office_ClientsHelper.WaitForText("1 records deleted successfully", 10);

                executionLog.Log("AdminSetValidationDate", "Redirect To client recycle bin page. ");
                VisitOffice("clients/recyclebin");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("AdminSetValidationDate", "Enter Company Name");
                office_ClientsHelper.TypeText("SearchClient", DBAName);
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("AdminSetValidationDate", "Click on delete client");
                office_ClientsHelper.ClickElement("DeleteRbin");

                executionLog.Log("AdminSetValidationDate", "Accept alert message.");
                office_ClientsHelper.AcceptAlert();

                executionLog.Log("AdminSetValidationDate", "Wait for success message.");
                office_ClientsHelper.WaitForText("Client Permanently Deleted.", 10);

                // Verifying field format 

                //executionLog.Log("AdminSetValidationDate", "Visit office admin");
                //VisitOffice("admin");

                //executionLog.Log("AdminSetValidationDate", "WaitForWorkAround");
                //validate.WaitForWorkAround(2000);

                executionLog.Log("AdminSetValidationDate", "Navigate to fields");
                VisitOffice("fields");
                validate.WaitForWorkAround(3000);

                validate.SelectByText("Module", "Clients");
                validate.WaitForWorkAround(2000);

                executionLog.Log("AdminSetValidationDate", "Select Processor as First Data North");
                validate.SelectByText("Processor", "First Data North");
                validate.WaitForWorkAround(2000);

                executionLog.Log("AdminSetValidationDate", "Select Tab as Contacts");
                validate.SelectByText("Tab", "Contacts");
                validate.WaitForWorkAround(2000);

                executionLog.Log("AdminSetValidationDate", "Select Section as Contacts");
                validate.SelectByText("Section", "Contacts");
                validate.WaitForWorkAround(2000);

                executionLog.Log("AdminSetValidationDate", "Click Search");
                validate.ClickElement("Search");
                validate.WaitForWorkAround(3000);

                executionLog.Log("AdminSetValidationDate", "Type Text title");
                validate.TypeText("filter", "Title");

                executionLog.Log("AdminSetValidationDate", "WaitForWorkAround");
                validate.WaitForWorkAround(2000);

                executionLog.Log("AdminSetValidationDate", "Click TitleField");
                validate.ClickElement("ContactTitle");
                validate.WaitForWorkAround(4000);

                executionLog.Log("AdminSetValidationDate", "Click on Field format");
                validate.checkAndClick("ProcApplyDataTypeValidation");
                validate.WaitForWorkAround(1000);

                executionLog.Log("AdminSetValidationDate", "Click SaveFieldManager");
                validate.ClickElement("SaveFieldManager");

                executionLog.Log("AdminSetValidationDate", "WaitForWorkAround");
                validate.WaitForWorkAround(3000);

            }
            catch (Exception e)
            {

                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("AdminSetValidationDate");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Admin Set Validation Date");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Admin Set Validatio nDate", "Bug", "Medium", "", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Admin Set Validation Date");
                        TakeScreenshot("AdminSetValidationDate");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AdminSetValidationDate.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("AdminSetValidationDate");
                        string id = loginHelper.getIssueID("Admin Set Validation Date");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AdminSetValidationDate.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Admin Set Validation Date"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Admin Set Validation Date");
                //   executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("AdminSetValidationDate");
                executionLog.WriteInExcel("Admin Set Validation Date", Status, JIRA, "Office Activities");
            }
        }
    }
}