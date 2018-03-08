
/*
* AdminSetValidationNumeric.cs is a test that will change the value of the title 
* field in contacts section of Pegasus. It will set the field to only accept 
* numeric input. AdminSetValidationNumeric.cs is associated with 
* AdminSetValidationNumericHelper.cs and AdminSetValidationNumeric.xml
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
    public class AdminSetValidationNumeric : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin")]
        [TestCategory("Fail")]
        [TestCategory("TS6")]
        [TestCategory("Pegasus_Admin")]
        public void setFieldToNumeric()
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

            //Random Variable
            String JIRA = "";
            String Status = "Pass";
            var DBAName = "DBA@Company" + RandomNumber(1, 999);

            try
            {

                executionLog.Log("AdminSetValidationNumeric", "Login");
                Login(username[0], password[0]);

                //executionLog.Log("AdminSetValidationNumeric", "WaitForWorkAround");
                //validate.WaitForWorkAround(4000);

                executionLog.Log("AdminSetValidationNumeric", "Verify Dashboard");
                VerifyTitle("Dashboard");

                //executionLog.Log("AdminSetValidationNumeric", "Visit admin");
                //VisitOffice("admin");
                //validate.WaitForWorkAround(2000);

                VisitOffice("fields");
                validate.WaitForWorkAround(3000);

                executionLog.Log("AdminSetValidationNumeric", "Select the client module");
                validate.SelectByText("Module", "Clients");
                validate.WaitForWorkAround(2000);

                executionLog.Log("AdminSetValidationNumeric", "Select Processor as First Data North");
                validate.SelectByText("Processor", "First Data North");
                validate.WaitForWorkAround(2000);

                executionLog.Log("AdminSetValidationNumeric", "Select Tab as Contacts");
                validate.SelectByText("Tab", "Contacts");
                validate.WaitForWorkAround(2000);

                executionLog.Log("AdminSetValidationNumeric", "Select Section as Contacts");
                validate.SelectByText("Section", "Contacts");
                validate.WaitForWorkAround(2000);

                executionLog.Log("AdminSetValidationNumeric", "Click Search Element");
                validate.ClickElement("Search");
                validate.WaitForWorkAround(3000);

                executionLog.Log("AdminSetValidationNumeric", "Type title Text");
                validate.TypeText("filter", "Title");
                validate.WaitForWorkAround(3000);

                executionLog.Log("AdminSetValidationNumeric", "Click TitleField Element");
                validate.ClickJs("ContactTitle");
                validate.WaitForWorkAround(4000);

                executionLog.Log("AdminSetValidationNumeric", "Click ApplyDataTypeValidation Element");
                validate.checkAndClick("ProcApplyDataTypeValidation");
                validate.WaitForWorkAround(2000);

                executionLog.Log("AdminSetValidationNumeric", "Select data type as phone.");
                validate.SelectByText("ProcFieldDataType", "Numeric");

                executionLog.Log("AdminSetValidationNumeric", "Click SaveFieldManager Element");
                validate.ClickElement("SaveFieldManager");
                validate.WaitForWorkAround(3000);

                executionLog.Log("AdminSetValidationNumeric", "Redirect at Create Client");
                VisitOffice("clients/create");
                validate.WaitForWorkAround(3000);

                executionLog.Log("AdminSetValidationNumeric", "Select Processor");
                office_ClientsHelper.SelectByText("CreateProc", "First Data North");

                executionLog.Log("AdminSetValidationNumeric", "Enter Client Dba Name");
                office_ClientsHelper.TypeText("ClientDBAName", DBAName);

                executionLog.Log("AdminSetValidationNumeric", "Select client status");
                office_ClientsHelper.SelectByText("ClientStatus", "New");

                executionLog.Log("AdminSetValidationNumeric", "Select Client Res[onsibility.");
                office_ClientsHelper.SelectByText("ClientResponsibility", "Howard Tang");

                executionLog.Log("AdminSetValidationNumeric", "Click on next button");
                office_ClientsHelper.ClickElement("Next");
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("AdminSetValidationNumeric", "Wait for confirmation message.");
                office_ClientsHelper.WaitForText("Client saved successfully. ", 10);

                executionLog.Log("AdminSetValidationNumeric", "Click On Contact Tab Clinet");
                office_ClientsHelper.ClickElement("ContactDetails");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("AdminSetValidationNumeric", "Enter character");
                validate.TypeText("Title", "abcdef");

                executionLog.Log("AdminSetValidationNumeric", "Click Save");
                validate.ClickForce("Save");
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("AdminSetValidationNumeric", "Verify page text");
                validate.VerifyPageText("Please enter only digits.");

                executionLog.Log("AdminSetValidationNumeric", "Redirect To clients page. ");
                VisitOffice("clients");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("AdminSetValidationNumeric", "Enter Company Name");
                office_ClientsHelper.TypeText("SearchClient", DBAName);
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("AdminSetValidationNumeric", "Select client by check box");
                office_ClientsHelper.ClickForce("ClickOn1stOpp");
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("AdminSetValidationNumeric", "Click on delete client");
                office_ClientsHelper.ClickElement("DeleteClient");

                executionLog.Log("AdminSetValidationNumeric", "Accept alert message.");
                office_ClientsHelper.AcceptAlert();

                executionLog.Log("AdminSetValidationNumeric", "Wait for success message.");
                office_ClientsHelper.WaitForText("1 records deleted successfully", 10);

                executionLog.Log("AdminSetValidationNumeric", "Redirect To client recycle bin page. ");
                VisitOffice("clients/recyclebin");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("AdminSetValidationNumeric", "Enter Company Name");
                office_ClientsHelper.TypeText("SearchClient", DBAName);
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("AdminSetValidationNumeric", "Click on delete client");
                office_ClientsHelper.ClickElement("DeleteRbin");

                executionLog.Log("AdminSetValidationNumeric", "Accept alert message.");
                office_ClientsHelper.AcceptAlert();

                executionLog.Log("AdminSetValidationNumeric", "Wait for success message.");
                office_ClientsHelper.WaitForText("Client Permanently Deleted.", 10);

                VisitOffice("fields");
                validate.WaitForWorkAround(3000);

                executionLog.Log("AdminSetValidationNumeric", "Select the client module");
                validate.SelectByText("Module", "Clients");
                validate.WaitForWorkAround(2000);

                executionLog.Log("AdminSetValidationNumeric", "Select Processor as First Data North");
                validate.SelectByText("Processor", "First Data North");
                validate.WaitForWorkAround(2000);

                executionLog.Log("AdminSetValidationNumeric", "Select Tab as Contacts");
                validate.SelectByText("Tab", "Contacts");
                validate.WaitForWorkAround(2000);

                executionLog.Log("AdminSetValidationNumeric", "Select Section as Contacts");
                validate.SelectByText("Section", "Contacts");
                validate.WaitForWorkAround(2000);

                executionLog.Log("AdminSetValidationNumeric", "Click Search Element");
                validate.ClickElement("Search");
                validate.WaitForWorkAround(3000);

                executionLog.Log("AdminSetValidationNumeric", "Type title Text");
                validate.TypeText("filter", "Title");
                validate.WaitForWorkAround(3000);

                executionLog.Log("AdminSetValidationNumeric", "Click TitleField Element");
                validate.ClickJs("ContactTitle");
                validate.WaitForWorkAround(4000);

                executionLog.Log("AdminSetValidationNumeric", "Click ApplyDataTypeValidation Element");
                validate.checkAndClick("ProcApplyDataTypeValidation");
                validate.WaitForWorkAround(2000);

                executionLog.Log("AdminSetValidationNumeric", "Click SaveFieldManager Element");
                validate.ClickElement("SaveFieldManager");
                validate.WaitForWorkAround(3000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("AdminSetValidationNumeric");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Admin Set Validation Numeric");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Admin Set Validation Numeric", "Bug", "Medium", "Client page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Admin Set Validation Numeric");
                        TakeScreenshot("AdminSetValidationNumeric");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AdminSetValidationNumeric.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("AdminSetValidationNumeric");
                        string id = loginHelper.getIssueID("Admin Set Validation Numeric");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AdminSetValidationNumeric.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Admin Set Validation Numeric"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Admin Set Validation Numeric");
            //    executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("AdminSetValidationNumeric");
                executionLog.WriteInExcel("Admin Set Validation Numeric", Status, JIRA, "Partner Portal");
            }
        }
    }
}