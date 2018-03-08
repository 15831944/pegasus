/*
* AdminSetValidationDecima.cs is a test that will check to see if a field
* will only accept input in decimal format. It will change a field's properties
* temporarilly, check the change then restore it to its previous state
* AdminSetValidationDecimal.cs works with AdminSetValidationDecimalHelper.cs
* and AdminSetValidationDecimal.xml
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
    public class AdminSetValidationDecimal : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin")]
        [TestCategory("Fail")]
        [TestCategory("TS6")]
        [TestCategory("Pegasus_Admin")]
        public void setFieldToDecimal()
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

                executionLog.Log("AdminSetValidationDecimal", "Login");
                Login(username[0], password[0]);

                //executionLog.Log("AdminSetValidationDecimal", "Wait");
                //validate.WaitForWorkAround(4000);

                executionLog.Log("AdminSetValidationDecimal", "Verify Title Dashboard");
                VerifyTitle("Dashboard");

                //VisitOffice("clients");
                //validate.WaitForWorkAround(3000);

                //executionLog.Log("AdminSetValidationDecimal", "Click CompanySearchBar");
                //validate.ClickElement("CompanySearchBar");

                //executionLog.Log("AdminSetValidationDecimal", "Ttype Text Client's name");
                //validate.TypeText("CompanySearchBar", "Client's name");

                //executionLog.Log("AdminSetValidationDecimal", "Click FirstClient");
                //validate.ClickElement("FirstClient");

                //executionLog.Log("AdminSetValidationDecimal", "Click Contacts");
                //validate.ClickElement("Contacts");

                //executionLog.Log("AdminSetValidationDecimal", "Wait");
                //validate.WaitForElementPresent("Title", 2);

                //executionLog.Log("AdminSetValidationDecimal", "Click Save");
                //validate.ClickElement("Save");

                //executionLog.Log("AdminSetValidationDecimal", "Visit admin office");
                //VisitOffice("admin");

                //executionLog.Log("AdminSetValidationDecimal", "Wait");
                //validate.WaitForWorkAround(2000);

                VisitOffice("fields");
                validate.WaitForWorkAround(3000);

                validate.SelectByText("Module", "Clients");
                validate.WaitForWorkAround(2000);

                executionLog.Log("AdminSetValidationDecimal", "Select Processor as First Data North");
                validate.SelectByText("Processor", "First Data North");
                validate.WaitForWorkAround(2000);

                executionLog.Log("AdminSetValidationDecimal", "Select Tab as Contacts");
                validate.SelectByText("Tab", "Contacts");
                validate.WaitForWorkAround(2000);

                executionLog.Log("AdminSetValidationDecimal", "Select Section as Contacts");
                validate.SelectByText("Section", "Contacts");
                validate.WaitForWorkAround(2000);

                executionLog.Log("AdminSetValidationDecimal", "Click Search");
                validate.ClickElement("Search");
                validate.WaitForWorkAround(3000);

                executionLog.Log("AdminSetValidationDecimal", "Type title");
                validate.TypeText("filter", "Title");
                validate.WaitForWorkAround(3000);

                executionLog.Log("AdminSetValidationDecimal", "Click TitleField");
                validate.ClickElement("ContactTitle");
                validate.WaitForWorkAround(4000);

                executionLog.Log("AdminSetValidationDecimal", "Click ApplyDataTypeValidation");
                validate.checkAndClick("ProcApplyDataTypeValidation");
                validate.WaitForWorkAround(2000);

                executionLog.Log("AdminSetValidationDecimal", "Select data type as decimal.");
                validate.SelectByText("ProcFieldDataType", "Decimal");
                validate.WaitForWorkAround(2000);

                executionLog.Log("AdminSetValidationDecimal", "Click SaveFieldManager");
                validate.ClickElement("SaveFieldManager");
                validate.WaitForWorkAround(3000);

                executionLog.Log("AdminSetValidationDecimal", "Redirect at Create Client");
                VisitOffice("clients/create");
                validate.WaitForWorkAround(3000);

                executionLog.Log("AdminSetValidationDecimal", "Select Processor");
                office_ClientsHelper.SelectByText("CreateProc", "First Data North");

                executionLog.Log("AdminSetValidationDecimal", "Enter Client Dba Name");
                office_ClientsHelper.TypeText("ClientDBAName", DBAName);

                executionLog.Log("AdminSetValidationDecimal", "Select client status");
                office_ClientsHelper.SelectByText("ClientStatus", "New");

                executionLog.Log("AdminSetValidationDecimal", "Select Client Res[onsibility.");
                office_ClientsHelper.SelectByText("ClientResponsibility", "Howard Tang");

                executionLog.Log("AdminSetValidationDecimal", "Click on next button");
                office_ClientsHelper.ClickElement("Next");
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("AdminSetValidationDecimal", "Wait for confirmation message.");
                office_ClientsHelper.WaitForText("Client saved successfully. ", 10);

                executionLog.Log("AdminSetValidationDecimal", "Click On Contact Tab Clinet");
                office_ClientsHelper.ClickElement("ContactDetails");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("AdminSetValidationDecimal", "Wait ");
                validate.WaitForElementPresent("Title", 1);
                validate.WaitForWorkAround(2000);

                executionLog.Log("AdminSetValidationDecimal", "Redirect To clients page. ");
                VisitOffice("clients");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("AdminSetValidationDecimal", "Enter Company Name");
                office_ClientsHelper.TypeText("SearchClient", DBAName);
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("AdminSetValidationDecimal", "Select client by check box");
                office_ClientsHelper.ClickForce("ClickOn1stOpp");
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("AdminSetValidationDecimal", "Click on delete client");
                office_ClientsHelper.ClickElement("DeleteClient");

                executionLog.Log("AdminSetValidationDecimal", "Accept alert message.");
                office_ClientsHelper.AcceptAlert();

                executionLog.Log("AdminSetValidationDecimal", "Wait for success message.");
                office_ClientsHelper.WaitForText("1 records deleted successfully", 10);

                executionLog.Log("AdminSetValidationDecimal", "Redirect To client recycle bin page. ");
                VisitOffice("clients/recyclebin");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("AdminSetValidationDecimal", "Enter Company Name");
                office_ClientsHelper.TypeText("SearchClient", DBAName);
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("AdminSetValidationDecimal", "Click on delete client");
                office_ClientsHelper.ClickElement("DeleteRbin");

                executionLog.Log("AdminSetValidationDecimal", "Accept alert message.");
                office_ClientsHelper.AcceptAlert();

                executionLog.Log("AdminSetValidationDecimal", "Wait for success message.");
                office_ClientsHelper.WaitForText("Client Permanently Deleted.", 10);

                VisitOffice("fields");
                validate.WaitForWorkAround(3000);

                validate.SelectByText("Module", "Clients");
                validate.WaitForWorkAround(2000);

                executionLog.Log("AdminSetValidationDecimal", "Select Processor as First Data North");
                validate.SelectByText("Processor", "First Data North");
                validate.WaitForWorkAround(2000);

                executionLog.Log("AdminSetValidationDecimal", "Select Tab as Contacts");
                validate.SelectByText("Tab", "Contacts");
                validate.WaitForWorkAround(2000);

                executionLog.Log("AdminSetValidationDecimal", "Select Section as Contacts");
                validate.SelectByText("Section", "Contacts");
                validate.WaitForWorkAround(2000);

                executionLog.Log("AdminSetValidationDecimal", "Click Search");
                validate.ClickElement("Search");
                validate.WaitForWorkAround(3000);

                executionLog.Log("AdminSetValidationDecimal", "Type title");
                validate.TypeText("filter", "Title");
                validate.WaitForWorkAround(3000);

                executionLog.Log("AdminSetValidationDecimal", "Click TitleField");
                validate.ClickElement("ContactTitle");
                validate.WaitForWorkAround(4000);

                executionLog.Log("AdminSetValidationDecimal", "Click ApplyDataTypeValidation");
                validate.checkAndClick("ProcApplyDataTypeValidation");
                validate.WaitForWorkAround(2000);

                executionLog.Log("AdminSetValidationDecimal", "Click SaveFieldManager");
                validate.ClickElement("SaveFieldManager");
                validate.WaitForWorkAround(3000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("AdminSetValidationDecimal");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Admin Set Validation Decimal");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Admin Set Validation Decimal", "Bug", "Medium", "Client page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Admin Set Validation Decimal");
                        TakeScreenshot("AdminSetFormatSSN");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AdminSetValidationDecimal.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("AdminSetValidationDecimal");
                        string id = loginHelper.getIssueID("Admin Set Validation Decimal");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AdminSetValidationDecimal.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Admin Set Validation Decimal"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Admin Set Validation Decimal");
            //    executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("AdminSetValidationDecimal");
                executionLog.WriteInExcel("Admin Set Validation Decimal", Status, JIRA, "Partner Portal");
            }
        }
    }
}
