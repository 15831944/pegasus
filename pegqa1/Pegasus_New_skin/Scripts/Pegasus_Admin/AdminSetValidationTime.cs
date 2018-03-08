/*
* The AdminSetValidationTime.cs Tests the ability to set a field validation 
* to a Time. It will first check the field's existance, next set the 
* validation type to Time , confirm the new validation and finally
* set the field back to normal.
* AdminSetValidationTime.cs works with AdminSetValidationTime.cs 
* and AdminSetValidationTime.xml  
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
    public class AdminSetValidationTime : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin")]
        [TestCategory("TS6")]
        [TestCategory("Pegasus_Admin")]
        public void setFieldToMandatory()
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
            AdminSetValidationTimeHelper validate = new AdminSetValidationTimeHelper(GetWebDriver());
            var office_ClientsHelper = new Office_ClientsHelper(GetWebDriver());

            //Variables
            String JIRA = "";
            String Status = "Pass";
            var DBAName = "DBA@Company" + RandomNumber(1, 999);

            try
            {

                executionLog.Log("AdminSetValidationTime", "Login");
                Login(username[0], password[0]);

                //executionLog.Log("AdminSetValidationTime", "WaitForWorkAround");
                //validate.WaitForWorkAround(4000);

                executionLog.Log("AdminSetValidationTime", "Verify Dashboard");
                VerifyTitle("Dashboard");

                //executionLog.Log("AdminSetValidationTime", "Visit admin");
                //VisitOffice("admin");
                //validate.WaitForWorkAround(2000);

                VisitOffice("fields");
                validate.WaitForWorkAround(3000);

                executionLog.Log("AdminSetValidationTime", "Select the client module");
                validate.SelectByText("Module", "Clients");
                validate.WaitForWorkAround(2000);

                executionLog.Log("AdminSetValidationTime", "Select Processor as First Data North");
                validate.SelectByText("Processor", "First Data North");
                validate.WaitForWorkAround(2000);

                executionLog.Log("AdminSetValidationTime", "Select Tab as Contacts");
                validate.SelectByText("Tab", "Contacts");
                validate.WaitForWorkAround(2000);

                executionLog.Log("AdminSetValidationTime", "Select Section as Contacts");
                validate.SelectByText("Section", "Contacts");
                validate.WaitForWorkAround(2000);

                executionLog.Log("AdminSetValidationTime", "Click Search Element");
                validate.ClickElement("Search");
                validate.WaitForWorkAround(3000);

                executionLog.Log("AdminSetValidationTime", "Type title Text");
                validate.TypeText("filter", "Title");
                validate.WaitForWorkAround(3000);

                executionLog.Log("AdminSetValidationTime", "Click TitleField Element");
                validate.ClickJs("ContactTitle");
                validate.WaitForWorkAround(4000);

                executionLog.Log("AdminSetValidationTime", "Click ApplyDataTypeValidation Element");
                validate.ClickElement("ProcApplyDataTypeValidation");
                validate.WaitForWorkAround(2000);

                executionLog.Log("AdminSetValidationTime", "Select data type as phone.");
                validate.SelectByText("ProcFieldDataType", "Time");
                validate.WaitForWorkAround(2000);

                executionLog.Log("AdminSetValidationTime", "Click SaveFieldManager Element");
                validate.ClickForce("SaveFieldManager");
                validate.WaitForWorkAround(3000);

                executionLog.Log("AdminSetValidationTime", "Redirect at Create Client");
                VisitOffice("clients/create");
                validate.WaitForWorkAround(3000);

                executionLog.Log("AdminSetValidationTime", "Select Processor");
                office_ClientsHelper.SelectByText("CreateProc", "First Data North");

                executionLog.Log("AdminSetValidationTime", "Enter Client Dba Name");
                office_ClientsHelper.TypeText("ClientDBAName", DBAName);

                executionLog.Log("AdminSetValidationTime", "Select client status");
                office_ClientsHelper.SelectByText("ClientStatus", "New");

                executionLog.Log("AdminSetValidationTime", "Select Client Res[onsibility.");
                office_ClientsHelper.SelectByText("ClientResponsibility", "Howard Tang");

                executionLog.Log("AdminSetValidationTime", "Click on next button");
                office_ClientsHelper.ClickElement("Next");
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("AdminSetValidationTime", "Wait for confirmation message.");
                office_ClientsHelper.WaitForText("Client saved successfully. ", 10);

                executionLog.Log("AdminSetValidationTime", "Click On Contact Tab Clinet");
                office_ClientsHelper.ClickElement("ContactDetails");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("AdminSetValidationTime", "Enter character");
                validate.TypeText("Title", "abcdef");

                executionLog.Log("AdminSetValidationTime", "Click Save");
                validate.ClickForce("Save");
                office_ClientsHelper.WaitForWorkAround(2000);

                //executionLog.Log("AdminSetValidationTime", "Verify page text");
                //validate.VerifyPageText("Please enter only digits.");

                executionLog.Log("AdminSetValidationTime", "Redirect To clients page. ");
                VisitOffice("clients");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("AdminSetValidationTime", "Enter Company Name");
                office_ClientsHelper.TypeText("SearchClient", DBAName);
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("AdminSetValidationTime", "Select client by check box");
                office_ClientsHelper.ClickForce("ClickOn1stOpp");
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("AdminSetValidationTime", "Click on delete client");
                office_ClientsHelper.ClickElement("DeleteClient");

                executionLog.Log("AdminSetValidationTime", "Accept alert message.");
                office_ClientsHelper.AcceptAlert();

                executionLog.Log("AdminSetValidationTime", "Wait for success message.");
                office_ClientsHelper.WaitForText("1 records deleted successfully", 10);

                executionLog.Log("AdminSetValidationTime", "Redirect To client recycle bin page. ");
                VisitOffice("clients/recyclebin");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("AdminSetValidationTime", "Enter Company Name");
                office_ClientsHelper.TypeText("SearchClient", DBAName);
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("AdminSetValidationTime", "Click on delete client");
                office_ClientsHelper.ClickElement("DeleteRbin");

                executionLog.Log("AdminSetValidationTime", "Accept alert message.");
                office_ClientsHelper.AcceptAlert();

                executionLog.Log("AdminSetValidationTime", "Wait for success message.");
                office_ClientsHelper.WaitForText("Client Permanently Deleted.", 10);

                VisitOffice("fields");
                validate.WaitForWorkAround(3000);

                executionLog.Log("AdminSetValidationTime", "Select the client module");
                validate.SelectByText("Module", "Clients");
                validate.WaitForWorkAround(2000);

                executionLog.Log("AdminSetValidationTime", "Select Processor as First Data North");
                validate.SelectByText("Processor", "First Data North");
                validate.WaitForWorkAround(2000);

                executionLog.Log("AdminSetValidationTime", "Select Tab as Contacts");
                validate.SelectByText("Tab", "Contacts");
                validate.WaitForWorkAround(2000);

                executionLog.Log("AdminSetValidationTime", "Select Section as Contacts");
                validate.SelectByText("Section", "Contacts");
                validate.WaitForWorkAround(2000);

                executionLog.Log("AdminSetValidationTime", "Click Search Element");
                validate.ClickElement("Search");
                validate.WaitForWorkAround(3000);

                executionLog.Log("AdminSetValidationTime", "Type title Text");
                validate.TypeText("filter", "Title");
                validate.WaitForWorkAround(3000);

                executionLog.Log("AdminSetValidationTime", "Click TitleField Element");
                validate.ClickJs("ContactTitle");
                validate.WaitForWorkAround(4000);

                executionLog.Log("AdminSetValidationTime", "Click ApplyDataTypeValidation Element");
                validate.checkAndClick("ProcApplyDataTypeValidation");
                validate.WaitForWorkAround(2000);

                executionLog.Log("AdminSetValidationTime", "Click SaveFieldManager Element");
                validate.ClickElement("SaveFieldManager");
                validate.WaitForWorkAround(3000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("AdminSetValidationTime");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Admin Set Validation Time");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Admin Set Validation Time", "Bug", "Medium", "Client page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Admin Set Validation Time");
                        TakeScreenshot("AdminSetValidationTime");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AdminSetValidationTime.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("AdminSetValidationTime");
                        string id = loginHelper.getIssueID("Admin Set Validation Time");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AdminSetValidationTime.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Admin Set Validation Time"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Admin Set Validation Time");
                //    executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("AdminSetValidationTime");
                executionLog.WriteInExcel("Admin Set Validation Time", Status, JIRA, "Partner Portal");
            }
        }
    }
}