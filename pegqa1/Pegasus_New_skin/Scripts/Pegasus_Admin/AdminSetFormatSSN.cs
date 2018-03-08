/*
* The AdminSetFormatSSN.cs Tests the ability to set a field format
* to SSN. It will first check the field's existance, next set the 
* validation type to SSN , confirm the new validation and finally
* set the field back to normal.
* AdminSetFormatSSN.cs works with AdminSetFormatSSNHelper.cs 
* and AdminSetFormatSSN.xml  
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
    public class AdminSetFormatSSN : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin")]
        [TestCategory("Fail")]
        [TestCategory("TS6")]
        [TestCategory("Pegasus_Admin")]
        public void setFormatToSSN()
        {
            string[] username = null;
            string[] password = null;

            XMLParse oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            //Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var ssn = new AdminSetFormatFieldsHelper(GetWebDriver());
            var office_ClientsHelper = new Office_ClientsHelper(GetWebDriver());

            //Random Variable
            String JIRA = "";
            String Status = "Pass";
            var DBAName = "DBA@Company" + RandomNumber(1, 999);

            try
            {
                //Logging in 

                executionLog.Log("AdminSetFormatSSN", "Login");
                Login(username[0], password[0]);

                //executionLog.Log("AdminSetFormatSSN", "WaitForWorkAround");
                //ssn.WaitForWorkAround(4000);

                executionLog.Log("AdminSetFormatSSN", "Verify Title");
                VerifyTitle("Dashboard");

                // Going to admin office 

                //executionLog.Log("AdminSetFormatSSN", "Visit admin office ");
                //VisitOffice("admin");
                //ssn.WaitForWorkAround(3000);

                VisitOffice("fields");
                ssn.WaitForWorkAround(3000);

                ssn.SelectByText("Module", "Clients");
                ssn.WaitForWorkAround(2000);

                executionLog.Log("AdminSetFormatSSN", "Select Processor as First Data North");
                ssn.SelectByText("Processor", "First Data North");
                //email.WaitForWorkAround(2000);

                executionLog.Log("AdminSetFormatSSN", "Select Tab as Contacts");
                ssn.SelectByText("Tab", "Contacts");
                ssn.WaitForWorkAround(2000);

                executionLog.Log("AdminSetFormatSSN", "Select Section as Contacts");
                ssn.SelectByText("Section", "Contacts");
                ssn.WaitForWorkAround(2000);

                executionLog.Log("AdminSetFormatSSN", "Click Search");
                ssn.ClickElement("Search");
                ssn.WaitForWorkAround(3000);

                executionLog.Log("AdminSetFormatSSN", "Type Text");
                ssn.TypeText("filter", "Title");
                ssn.WaitForWorkAround(2000);

                executionLog.Log("AdminSetFormatSSN", "Click TitleField");
                ssn.ClickElement("TitleField");
                ssn.WaitForWorkAround(4000);

                executionLog.Log("AdminSetFormatSSN", "Click FieldFormat");
                ssn.checkAndClick("ProcFieldFormat");
                ssn.WaitForWorkAround(1000);

                executionLog.Log("AdminSetFormatSSN", "Click FieldContentSSN");
                ssn.SelectByText("ProcFieldContntType", "SSN");
                //ssn.WaitForWorkAround(3000);

                executionLog.Log("AdminSetFormatSSN", "Click SaveFieldManager");
                ssn.ClickJs("SaveFieldManager");
                ssn.WaitForWorkAround(3000);

                executionLog.Log("AdminSetFormatSSN", "Redirect at Create Client");
                VisitOffice("clients/create");

                executionLog.Log("AdminSetFormatSSN", "Select Processor");
                office_ClientsHelper.SelectByText("CreateProc", "First Data North");

                executionLog.Log("AdminSetFormatSSN", "Enter Client Dba Name");
                office_ClientsHelper.TypeText("ClientDBAName", DBAName);

                executionLog.Log("AdminSetFormatSSN", "Select client status");
                office_ClientsHelper.SelectByText("ClientStatus", "New");

                executionLog.Log("AdminSetFormatSSN", "Select Client Res[onsibility.");
                office_ClientsHelper.SelectByText("ClientResponsibility", "Howard Tang");

                executionLog.Log("AdminSetFormatSSN", "Click on next button");
                office_ClientsHelper.ClickElement("Next");
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("AdminSetFormatSSN", "Wait for confirmation message.");
                office_ClientsHelper.WaitForText("Client saved successfully. ", 10);

                executionLog.Log("AdminSetFormatSSN", "Click On Contact Tab Clinet");
                office_ClientsHelper.ClickElement("ContactDetails");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("AdminSetFormatSSN", "Verify Format");
                ssn.TypeText("Title", "012-22-2542");
                ssn.ClickForce("Save");
                ssn.WaitForWorkAround(4000);

                executionLog.Log("AdminSetFormatSSN", "Redirect To clients page. ");
                VisitOffice("clients");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("AdminSetFormatSSN", "Enter Company Name");
                office_ClientsHelper.TypeText("SearchClient", DBAName);
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("AdminSetFormatSSN", "Select client by check box");
                office_ClientsHelper.ClickForce("ClickOn1stOpp");
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("AdminSetFormatSSN", "Click on delete client");
                office_ClientsHelper.ClickElement("DeleteClient");

                executionLog.Log("AdminSetFormatSSN", "Accept alert message.");
                office_ClientsHelper.AcceptAlert();

                executionLog.Log("AdminSetFormatSSN", "Wait for success message.");
                office_ClientsHelper.WaitForText("1 records deleted successfully", 10);

                executionLog.Log("AdminSetFormatSSN", "Redirect To client recycle bin page. ");
                VisitOffice("clients/recyclebin");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("AdminSetFormatSSN", "Enter Company Name");
                office_ClientsHelper.TypeText("SearchClient", DBAName);
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("AdminSetFormatSSN", "Click on delete client");
                office_ClientsHelper.ClickElement("DeleteRbin");

                executionLog.Log("AdminSetFormatSSN", "Accept alert message.");
                office_ClientsHelper.AcceptAlert();

                executionLog.Log("AdminSetFormatSSN", "Wait for success message.");
                office_ClientsHelper.WaitForText("Client Permanently Deleted.", 10);

                // Going to admin office 

                //executionLog.Log("AdminSetFormatSSN", "Verify Office");
                //VisitOffice("admin");

                // Accessing field in field dictionary
                VisitOffice("fields");
                ssn.WaitForWorkAround(3000);

                ssn.SelectByText("Module", "Clients");
                ssn.WaitForWorkAround(2000);

                executionLog.Log("AdminSetFormatSSN", "Select Processor as First Data North");
                ssn.SelectByText("Processor", "First Data North");
                //email.WaitForWorkAround(2000);

                executionLog.Log("AdminSetFormatSSN", "Select Tab as Contacts");
                ssn.SelectByText("Tab", "Contacts");
                ssn.WaitForWorkAround(2000);

                executionLog.Log("AdminSetFormatSSN", "Select Section as Contacts");
                ssn.SelectByText("Section", "Contacts");
                ssn.WaitForWorkAround(2000);

                executionLog.Log("AdminSetFormatSSN", "Click Search");
                ssn.ClickElement("Search");
                ssn.WaitForWorkAround(3000);

                executionLog.Log("AdminSetFormatSSN", "Type Text");
                ssn.TypeText("filter", "Title");
                ssn.WaitForWorkAround(2000);

                executionLog.Log("AdminSetFormatSSN", "Click TitleField");
                ssn.ClickElement("TitleField");
                ssn.WaitForWorkAround(4000);

                executionLog.Log("AdminSetFormatSSN", "Click FieldFormat");
                ssn.checkAndClick("ProcFieldFormat");
                ssn.WaitForWorkAround(1000);
                //ssn.WaitForWorkAround(3000);

                executionLog.Log("AdminSetFormatSSN", "Click SaveFieldManager");
                ssn.ClickJs("SaveFieldManager");
                ssn.WaitForWorkAround(3000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("AdminSetFormatSSN");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Admin Set Forma tSSN");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Admin Set Format SSN", "Bug", "Medium", "Client page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Admin Set Format SSN");
                        TakeScreenshot("AdminSetFormatSSN");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AdminSetFormatSSN.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("AdminSetFormatSSN");
                        string id = loginHelper.getIssueID("Admin Set Format SSN");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AdminSetFormatSSN.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Admin Set Format SSN"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Admin Set Format SSN");
                //  executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("AdminSetFormatSSN");
                executionLog.WriteInExcel("Admin Set Format SSN", Status, JIRA, "Partner Portal");
            }
        }
    }
}