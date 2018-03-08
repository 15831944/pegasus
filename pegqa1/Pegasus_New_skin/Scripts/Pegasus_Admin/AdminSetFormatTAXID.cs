
/*
* The AdminSetFormatTAXID.cs Tests the ability to set a field format
* to a tax ID. It will first check the field's existance, next set the 
* validation type to tax ID , confirm the new validation and finally
* set the field back to normal.
* AdminSetFormatTAXID.cs works with AdminSetFormatTAXIDHelper.cs 
* and AdminSetFormatTAXID.xml  
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
    public class AdminSetFormatTAXID : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin")]
        [TestCategory("TS6")]
        [TestCategory("Pegasus_Admin")]
        public void setFormatToTAXID()
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
            var taxID = new AdminSetFormatFieldsHelper(GetWebDriver());
            var office_ClientsHelper = new Office_ClientsHelper(GetWebDriver());

            //Random Variable
            String JIRA = "";
            String Status = "Pass";
            var DBAName = "DBA@Company" + RandomNumber(1, 999);

            try
            {
                executionLog.Log("AdminSetFormatTAXID", "WaitForWorkAround");
                taxID.WaitForWorkAround(2000);

                //Logging in 

                executionLog.Log("AdminSetFormatTAXID", "Login");
                Login(username[0], password[0]);

                //executionLog.Log("AdminSetFormatTAXID", "WaitForWorkAround");
                //taxID.WaitForWorkAround(4000);

                executionLog.Log("AdminSetFormatTAXID", "Verify Title Dashboard");
                VerifyTitle("Dashboard");

                // Visiting a client 

                //VisitOffice("clients");
                //taxID.WaitForWorkAround(3000);

                //executionLog.Log("AdminSetFormatTAXID", "Type Text ");
                //taxID.TypeText("CompanySearchBar", "Client's name");
                //taxID.WaitForWorkAround(2000);

                //executionLog.Log("AdminSetFormatTAXID", "Click FirstClient");
                //taxID.ClickElement("FirstClient");

                //executionLog.Log("AdminSetFormatTAXID", "Hover over Contacts ");
                //taxID.MouseHover("Contacts");

                //executionLog.Log("AdminSetFormatTAXID", "Click Contacts");
                //taxID.ClickElement("Contacts");

                // Verifying the field exists that will be validated

                //executionLog.Log("AdminSetFormatTAXID", "Wait for Title Element");
                //taxID.ElementVisible("Title");

                // Going to admin office 

                //executionLog.Log("AdminSetFormatTAXID", "Visit Admin");
                //VisitOffice("admin");
                //taxID.WaitForWorkAround(2000);

                // Accessing field in field dictionary            

                VisitOffice("fields");
                taxID.WaitForWorkAround(3000);

                taxID.SelectByText("Module", "Clients");
                taxID.WaitForWorkAround(2000);

                executionLog.Log("AdminSetFormatTAXID", "Select Processor as First Data North");
                taxID.SelectByText("Processor", "First Data North");
                //email.WaitForWorkAround(2000);

                executionLog.Log("AdminSetFormatTAXID", "Select Tab as Contacts");
                taxID.SelectByText("Tab", "Contacts");
                taxID.WaitForWorkAround(2000);

                executionLog.Log("AdminSetFormatTAXID", "Select Section as Contacts");
                taxID.SelectByText("Section", "Contacts");
                taxID.WaitForWorkAround(2000);

                executionLog.Log("AdminSetFormatTAXID", "Click Search");
                taxID.ClickElement("Search");
                taxID.WaitForWorkAround(3000);

                executionLog.Log("AdminSetFormatTAXID", "Type Text");
                taxID.TypeText("filter", "Title");
                taxID.WaitForWorkAround(2000);

                executionLog.Log("AdminSetFormatTAXID", "Click TitleField");
                taxID.ClickElement("ContactTitle");
                taxID.WaitForWorkAround(4000);

                executionLog.Log("AdminSetFormatTAXID", "Click FieldFormat");
                taxID.checkAndClick("ProcFieldFormat");
                taxID.WaitForWorkAround(1000);

                executionLog.Log("AdminSetFormatTAXID", "Click FieldContentTaxID");
                taxID.SelectByText("ProcFieldContntType", "Tax ID");

                executionLog.Log("AdminSetFormatTAXID", "Click SaveFieldManager");
                taxID.ClickElement("SaveFieldManager");
                taxID.WaitForWorkAround(3000);

                executionLog.Log("AdminSetFormatTAXID", "Redirect at Create Client");
                VisitOffice("clients/create");

                executionLog.Log("AdminSetFormatTAXID", "Select Processor");
                office_ClientsHelper.SelectByText("CreateProc", "First Data North");

                executionLog.Log("AdminSetFormatTAXID", "Enter Client Dba Name");
                office_ClientsHelper.TypeText("ClientDBAName", DBAName);

                executionLog.Log("AdminSetFormatTAXID", "Select client status");
                office_ClientsHelper.SelectByText("ClientStatus", "New");

                executionLog.Log("AdminSetFormatTAXID", "Select Client Res[onsibility.");
                office_ClientsHelper.SelectByText("ClientResponsibility", "Howard Tang");

                executionLog.Log("AdminSetFormatTAXID", "Click on next button");
                office_ClientsHelper.ClickElement("Next");
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("AdminSetFormatTAXID", "Wait for confirmation message.");
                office_ClientsHelper.WaitForText("Client saved successfully. ", 10);

                executionLog.Log("AdminSetFormatTAXID", "Click On Contact Tab Clinet");
                office_ClientsHelper.ClickElement("ContactDetails");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("AdminSetFormatTAXID", "Redirect To clients page. ");
                VisitOffice("clients");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("AdminSetFormatTAXID", "Enter Company Name");
                office_ClientsHelper.TypeText("SearchClient", DBAName);
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("AdminSetFormatTAXID", "Select client by check box");
                office_ClientsHelper.ClickElement("ClickOn1stOpp");
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("AdminSetFormatTAXID", "Click on delete client");
                office_ClientsHelper.ClickElement("DeleteClient");

                executionLog.Log("AdminSetFormatTAXID", "Accept alert message.");
                office_ClientsHelper.AcceptAlert();

                executionLog.Log("AdminSetFormatTAXID", "Wait for success message.");
                office_ClientsHelper.WaitForText("1 records deleted successfully", 10);

                executionLog.Log("AdminSetFormatTAXID", "Redirect To client recycle bin page. ");
                VisitOffice("clients/recyclebin");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("AdminSetFormatTAXID", "Enter Company Name");
                office_ClientsHelper.TypeText("SearchClient", DBAName);
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("AdminSetFormatTAXID", "Click on delete client");
                office_ClientsHelper.ClickElement("DeleteRbin");

                executionLog.Log("AdminSetFormatTAXID", "Accept alert message.");
                office_ClientsHelper.AcceptAlert();

                executionLog.Log("AdminSetFormatTAXID", "Wait for success message.");
                office_ClientsHelper.WaitForText("Client Permanently Deleted.", 10);

                // Going to admin office 

                //executionLog.Log("AdminSetFormatTAXID", "Visit Office Admin");
                //VisitOffice("admin");
                //taxID.WaitForWorkAround(3000);

                // Accessing field in field dictionary

                VisitOffice("fields");
                taxID.WaitForWorkAround(3000);

                taxID.SelectByText("Module", "Clients");
                taxID.WaitForWorkAround(2000);

                executionLog.Log("AdminSetFormatTAXID", "Select Processor as First Data North");
                taxID.SelectByText("Processor", "First Data North");
                //email.WaitForWorkAround(2000);

                executionLog.Log("AdminSetFormatTAXID", "Select Tab as Contacts");
                taxID.SelectByText("Tab", "Contacts");
                taxID.WaitForWorkAround(2000);

                executionLog.Log("AdminSetFormatTAXID", "Select Section as Contacts");
                taxID.SelectByText("Section", "Contacts");
                taxID.WaitForWorkAround(2000);

                executionLog.Log("AdminSetFormatTAXID", "Click Search");
                taxID.ClickElement("Search");
                taxID.WaitForWorkAround(3000);

                executionLog.Log("AdminSetFormatTAXID", "Type Text");
                taxID.TypeText("filter", "Title");
                taxID.WaitForWorkAround(2000);

                executionLog.Log("AdminSetFormatTAXID", "Click TitleField");
                taxID.ClickElement("ContactTitle");
                taxID.WaitForWorkAround(4000);

                executionLog.Log("AdminSetFormatTAXID", "Click FieldFormat");
                taxID.checkAndClick("ProcFieldFormat");
                taxID.WaitForWorkAround(1000);

                executionLog.Log("AdminSetFormatTAXID", "Click SaveFieldManager");
                taxID.ClickElement("SaveFieldManager");
                taxID.WaitForWorkAround(3000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("AdminSetFormatTAXID");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Admin Set Format TAXID");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Admin Set Format TAXID", "Bug", "Medium", "Client page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Admin Set Format TAXID");
                        TakeScreenshot("Admin Set Format TAXID");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AdminSetFormatTAXID.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("AdminSetFormatTAXID");
                        string id = loginHelper.getIssueID("Admin Set Format TAXID");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AdminSetFormatTAXID.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Admin Set Format TAXID"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Admin Set Format TAXID");
            //    executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("AdminSetFormatTAXID");
                executionLog.WriteInExcel("Admin Set Format TAXID", Status, JIRA, "Partner Portal");
            }
        }
    }
}