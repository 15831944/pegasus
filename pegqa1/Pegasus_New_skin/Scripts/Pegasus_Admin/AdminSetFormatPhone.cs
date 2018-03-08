/*
* The AdminSetFormatPhone.cs Tests the ability to set a field format
* to a Phone. It will first check the field's existance, next set the 
* validation type to Phone , confirm the new validation and finally
* set the field back to normal.
* AdminSetFormatPhone.cs works with AdminSetFormatPhone.cs 
* and AdminSetFormatPhone.xml  
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
    public class AdminSetFormatPhone : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin")]
        [TestCategory("TS6")]
        [TestCategory("Pegasus_Admin")]
        public void setFormatToPhone()
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
            var phone = new AdminSetFormatFieldsHelper(GetWebDriver());
            var office_ClientsHelper = new Office_ClientsHelper(GetWebDriver());

            //Random Variable
            String JIRA = "";
            String Status = "Pass";
            var DBAName = "DBA@Company" + RandomNumber(1, 999);

            try
            {


                //Logging in 

                executionLog.Log("AdminSetFormatPhone", "Login");
                Login(username[0], password[0]);

                executionLog.Log("AdminSetFormatPhone", "Verify Title");
                VerifyTitle("Dashboard");

                // Visiting a client 

                //VisitOffice("clients");

                //executionLog.Log("AdminSetFormatPhone", "Click CompanySearchBar");
                //phone.ClickElement("CompanySearchBar");

                //executionLog.Log("AdminSetFormatPhone", "Click FirstClient");
                //phone.ClickElement("FirstClient");

                //executionLog.Log("AdminSetFormatPhone", "Hover over Contacts");
                //phone.MouseHover("Contacts");

                //executionLog.Log("AdminSetFormatPhone", "Click Contacts");
                //phone.ClickElement("Contacts");

                // Verifying the field exists that will be validated

                //executionLog.Log("AdminSetFormatPhone", "Is Title visible");
                //phone.ElementVisible("Title");

                // Going to admin office 

                //executionLog.Log("AdminSetFormatPhone", "Visit admin office");
                //VisitOffice("admin");

                // Accessing field in field dictionary

                //executionLog.Log("AdminSetFormatPhone", "Wait for FieldDictionary");
                //phone.WaitForElementPresent("FieldDictionary", 1);

                //executionLog.Log("AdminSetFormatPhone", "WaitForWorkAround");
                //phone.WaitForWorkAround(1000);

                executionLog.Log("AdminSetFormatPhone", "WaitForWorkAround");
                VisitOffice("fields");
                phone.WaitForWorkAround(3000);

                //executionLog.Log("AdminSetFormatPhone", "WaitForWorkAround");
                //phone.WaitForWorkAround(1000);

                phone.SelectByText("Module", "Clients");
                phone.WaitForWorkAround(2000);

                executionLog.Log("AdminSetFormatEmail", "Select Processor as First Data North");
                phone.SelectByText("Processor", "First Data North");
                phone.WaitForWorkAround(2000);

                executionLog.Log("AdminSetFormatEmail", "Select Tab as Contacts");
                phone.SelectByText("Tab", "Contacts");
                phone.WaitForWorkAround(2000);

                executionLog.Log("AdminSetFormatEmail", "Select Section as Contacts");
                phone.SelectByText("Section", "Contacts");
                phone.WaitForWorkAround(2000);

                executionLog.Log("AdminSetFormatPhone", "Click Search");
                phone.ClickElement("Search");
                phone.WaitForWorkAround(4000);

                executionLog.Log("AdminSetFormatPhone", "Type Text");
                phone.TypeText("filter", "Title");
                phone.WaitForWorkAround(2000);

                executionLog.Log("AdminSetFormatPhone", "Click TitleField");
                phone.ClickElement("TitleField");
                phone.WaitForWorkAround(4000);

                executionLog.Log("AdminSetFormatPhone", "Click FieldFormat");
                phone.checkAndClick("ProcFieldFormat");
                phone.WaitForWorkAround(1000);

                executionLog.Log("AdminSetFormatPhone", "Select data type as phone.");
                phone.SelectByText("ProcFieldContntType", "Phone");

                //executionLog.Log("AdminSetFormatPhone", "WaitForWorkAround");
                //phone.WaitForWorkAround(3000);

                executionLog.Log("AdminSetFormatPhone", "Click SaveFieldManager");
                phone.ClickElement("SaveFieldManager");

                executionLog.Log("AdminSetFormatPhone", "WaitForWorkAround");
                phone.WaitForWorkAround(3000);

                executionLog.Log("AdminSetFormatEmail", "Redirect at Create Client");
                VisitOffice("clients/create");
                phone.WaitForWorkAround(3000);

                executionLog.Log("AdminSetFormatEmail", "Select Processor");
                office_ClientsHelper.SelectByText("CreateProc", "First Data North");

                executionLog.Log("AdminSetFormatEmail", "Enter Client Dba Name");
                office_ClientsHelper.TypeText("ClientDBAName", DBAName);

                executionLog.Log("AdminSetFormatEmail", "Select client status");
                office_ClientsHelper.SelectByText("ClientStatus", "New");

                executionLog.Log("AdminSetFormatEmail", "Select Client Res[onsibility.");
                office_ClientsHelper.SelectByText("ClientResponsibility", "Howard Tang");

                executionLog.Log("AdminSetFormatEmail", "Click on next button");
                office_ClientsHelper.ClickElement("Next");
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("AdminSetFormatEmail", "Wait for confirmation message.");
                office_ClientsHelper.WaitForText("Client saved successfully. ", 10);

                executionLog.Log("AdminSetFormatEmail", "Click On Contact Tab Clinet");
                office_ClientsHelper.ClickElement("ContactDetails");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("AdminSetFormatPhone", "Verify Title format");
                phone.VerifyText("PhoneNumberTitle", "Phone Number:");
                phone.WaitForWorkAround(4000);

                executionLog.Log("AdminSetFormatSSN", "Redirect To clients page. ");
                VisitOffice("clients");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("AdminSetFormatSSN", "Enter Company Name");
                office_ClientsHelper.TypeText("SearchClient", DBAName);
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("AdminSetFormatSSN", "Select client by check box");
                office_ClientsHelper.ClickElement("ClickOn1stOpp");
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

                executionLog.Log("AdminSetFormatPhone", "WaitForWorkAround");
                VisitOffice("fields");
                phone.WaitForWorkAround(3000);

                phone.SelectByText("Module", "Clients");
                phone.WaitForWorkAround(2000);

                executionLog.Log("AdminSetFormatEmail", "Select Processor as First Data North");
                phone.SelectByText("Processor", "First Data North");
                phone.WaitForWorkAround(2000);

                executionLog.Log("AdminSetFormatEmail", "Select Tab as Contacts");
                phone.SelectByText("Tab", "Contacts");
                phone.WaitForWorkAround(2000);

                executionLog.Log("AdminSetFormatEmail", "Select Section as Contacts");
                phone.SelectByText("Section", "Contacts");
                phone.WaitForWorkAround(2000);

                executionLog.Log("AdminSetFormatPhone", "Click Search");
                phone.ClickElement("Search");
                phone.WaitForWorkAround(4000);

                executionLog.Log("AdminSetFormatPhone", "Type Text");
                phone.TypeText("filter", "Title");
                phone.WaitForWorkAround(2000);

                executionLog.Log("AdminSetFormatPhone", "Click TitleField");
                phone.ClickElement("TitleField");
                phone.WaitForWorkAround(4000);

                executionLog.Log("AdminSetFormatPhone", "Click FieldFormat");
                phone.checkAndClick("ProcFieldFormat");
                phone.WaitForWorkAround(1000);

                executionLog.Log("AdminSetFormatPhone", "Click SaveFieldManager");
                phone.ClickElement("SaveFieldManager");
                phone.WaitForWorkAround(3000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("AdminSetFormatPhone");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Admin Set Format Phone");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Admin Set Format Phone", "Bug", "Medium", "Client page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Admin Set Format Phone");
                        TakeScreenshot("AdminSetFormatPhone");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AdminSetFormatPhone.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("AdminSetFormatPhone");
                        string id = loginHelper.getIssueID("Admin Set Format Phone");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AdminSetFormatPhone.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Admin Set Format Phone"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Admin Set Format Phone");
              //  executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("AdminSetFormatPhone");
                executionLog.WriteInExcel("Admin Set Format Phone", Status, JIRA, "Partner Portal");
            }
        }

    }
}