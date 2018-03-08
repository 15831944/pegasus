/* Documented by Khalil Shakir
* 
* The AdminValidateDisplayNames.cs is a test that wil validate the ability to change a 
* field's name in the Office Admin portal and see the change in the Office portal. The 
* final part of this test case will change the name of the field tested back to the 
* original name. 
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
    public class AdminValidateDisplayNames : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin")]
        [TestCategory("Fail")]
        [TestCategory("TS6")]
        [TestCategory("Pegasus_Admin")]
        public void validateName()
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
            var validateNames = new AdminValidateDisplayNamesHelper(GetWebDriver());
            var office_ClientsHelper = new Office_ClientsHelper(GetWebDriver());

            // Random Variable
            String JIRA = "";
            String Status = "Pass";
            var DBAName = "DBA@Company" + RandomNumber(1, 999);

            try
            {

                //executionLog.Log(" AdminValidateDisplayNames", "Wait for work around");
                //validateNames.WaitForWorkAround(2000);

                //Logging in 
                executionLog.Log(" AdminValidateDisplayNames", "Login");
                Login(username[0], password[0]);

                //executionLog.Log(" AdminValidateDisplayNames", "Wait for work around");
                //validateNames.WaitForWorkAround(4000);

                executionLog.Log(" AdminValidateDisplayNames", "Verify Dashboard");
                VerifyTitle("Dashboard");

                //VisitOffice("clients");
                //validateNames.WaitForWorkAround(3000);

                //executionLog.Log(" AdminValidateDisplayNames", " Search the clients name ");
                //validateNames.TypeText("SearchFields", "Client's Name");
                //validateNames.WaitForWorkAround(3000);

                //executionLog.Log(" AdminValidateDisplayNames", " Click on FirstClient ");
                //validateNames.ClickElement("FirstClient");

                //executionLog.Log(" AdminValidateDisplayNames", "Hover on CompanyDetails ");
                //validateNames.MouseHover("CompanyDetails");

                //executionLog.Log(" AdminValidateDisplayNames", " Click on CompanyDetails ");
                //validateNames.ClickElement("CompanyDetails");

                //// Verifying the field exists that will be changed
                //executionLog.Log(" AdminValidateDisplayNames", " Verify NameCity ");
                //validateNames.ElementVisible("AddressLine1");

                //// Going to admin office 
                //executionLog.Log(" AdminValidateDisplayNames", " Visit office admin ");
                //VisitOffice("admin");
                //validateNames.WaitForWorkAround(4000);

                // Accessing field in field dictionary

                VisitOffice("fields");
                validateNames.WaitForWorkAround(2000);

                executionLog.Log("AdminValidateDisplayNames", "Select the module");
                validateNames.SelectByText("Module", "Clients");
                validateNames.WaitForWorkAround(2000);

                executionLog.Log("AdminSetValidationTime", "Select Processor as First Data North");
                validateNames.SelectByText("Processor", "First Data North");
                validateNames.WaitForWorkAround(2000);

                executionLog.Log("AdminValidateDisplayNames", "Select the module tab");
                validateNames.SelectByText("Tab", "Company Details");
                validateNames.WaitForWorkAround(2000);

                executionLog.Log("AdminValidateDisplayNames", "Select Section as Company Address");
                validateNames.SelectByText("Section", "Company Details");
                validateNames.WaitForWorkAround(2000);

                executionLog.Log(" AdminValidateDisplayNames", " Click on Search ");
                validateNames.ClickElement("Search");
                validateNames.WaitForWorkAround(3000);

                executionLog.Log(" AdminValidateDisplayNames", " Click on Addressline1D ");
                validateNames.ClickElement("StoreName");
                validateNames.WaitForWorkAround(4000);

                executionLog.Log(" AdminValidateDisplayNames", " Click on FieldDisplayName ");
                validateNames.ClickElement("ProcFieldDisplayName");

                // Changing the name of the field
                executionLog.Log(" AdminValidateDisplayNames", " Type City Name Here");
                validateNames.TypeText("ProcFieldDisplayName", "Test");

                executionLog.Log(" AdminValidateDisplayNames", " Click on Save ");
                validateNames.ClickElement("SaveFieldManager");

                executionLog.Log(" AdminValidateDisplayNames", "Wait for work around");
                validateNames.WaitForWorkAround(3000);

                // Vist Clients site

                executionLog.Log("AdminValidateDisplayNames", "Redirect at Create Client");
                VisitOffice("clients/create");
                validateNames.WaitForWorkAround(3000);

                executionLog.Log("AdminValidateDisplayNames", "Select Processor");
                office_ClientsHelper.SelectByText("CreateProc", "First Data North");

                executionLog.Log("AdminValidateDisplayNames", "Enter Client Dba Name");
                office_ClientsHelper.TypeText("ClientDBAName", DBAName);

                executionLog.Log("AdminValidateDisplayNames", "Select client status");
                office_ClientsHelper.SelectByText("ClientStatus", "New");

                executionLog.Log("AdminValidateDisplayNames", "Select Client Res[onsibility.");
                office_ClientsHelper.SelectByText("ClientResponsibility", "Howard Tang");

                executionLog.Log("AdminValidateDisplayNames", "Click on next button");
                office_ClientsHelper.ClickElement("Next");
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("AdminValidateDisplayNames", "Wait for confirmation message.");
                office_ClientsHelper.WaitForText("Client saved successfully. ", 10);

                executionLog.Log("AdminValidateDisplayNames", "Click On Contact Tab Clinet");
                office_ClientsHelper.ClickElement("CompanyDetailsTab");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("AdminValidateDisplayNames", "Verify Title of Store Name");
                office_ClientsHelper.VerifyText("StoreNameLabel", "Test");
                //office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("AdminValidateDisplayNames", "Redirect To clients page. ");
                VisitOffice("clients");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("AdminValidateDisplayNames", "Enter Company Name");
                office_ClientsHelper.TypeText("SearchClient", DBAName);
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("AdminValidateDisplayNames", "Select client by check box");
                office_ClientsHelper.ClickForce("ClickOn1stOpp");
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("AdminValidateDisplayNames", "Click on delete client");
                office_ClientsHelper.ClickElement("DeleteClient");

                executionLog.Log("AdminValidateDisplayNames", "Accept alert message.");
                office_ClientsHelper.AcceptAlert();

                executionLog.Log("AdminValidateDisplayNames", "Wait for success message.");
                office_ClientsHelper.WaitForText("1 records deleted successfully", 10);

                executionLog.Log("AdminValidateDisplayNames", "Redirect To client recycle bin page. ");
                VisitOffice("clients/recyclebin");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("AdminValidateDisplayNames", "Enter Company Name");
                office_ClientsHelper.TypeText("SearchClient", DBAName);
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("AdminValidateDisplayNames", "Click on delete client");
                office_ClientsHelper.ClickElement("DeleteRbin");

                executionLog.Log("AdminValidateDisplayNames", "Accept alert message.");
                office_ClientsHelper.AcceptAlert();

                executionLog.Log("AdminValidateDisplayNames", "Wait for success message.");
                office_ClientsHelper.WaitForText("Client Permanently Deleted.", 10);

                // Vist field site
                VisitOffice("fields");
                validateNames.WaitForWorkAround(2000);

                executionLog.Log("AdminValidateDisplayNames", "Select the module");
                validateNames.SelectByText("Module", "Clients");
                validateNames.WaitForWorkAround(2000);

                executionLog.Log("AdminSetValidationTime", "Select Processor as First Data North");
                validateNames.SelectByText("Processor", "First Data North");
                validateNames.WaitForWorkAround(2000);

                executionLog.Log("AdminValidateDisplayNames", "Select the module tab");
                validateNames.SelectByText("Tab", "Company Details");
                validateNames.WaitForWorkAround(2000);

                executionLog.Log("AdminValidateDisplayNames", "Select Section as Company Address");
                validateNames.SelectByText("Section", "Company Details");
                validateNames.WaitForWorkAround(2000);

                executionLog.Log(" AdminValidateDisplayNames", " Click on Search ");
                validateNames.ClickElement("Search");
                validateNames.WaitForWorkAround(3000);

                executionLog.Log(" AdminValidateDisplayNames", " Click on Addressline1D ");
                validateNames.ClickElement("StoreName");
                validateNames.WaitForWorkAround(4000);

                executionLog.Log(" AdminValidateDisplayNames", " Click on FieldDisplayName ");
                validateNames.ClickElement("ProcFieldDisplayName");

                // Changing the name of the field
                executionLog.Log(" AdminValidateDisplayNames", " Type City Name Here");
                validateNames.TypeText("ProcFieldDisplayName", "Store/DBA Name");

                executionLog.Log(" AdminValidateDisplayNames", " Click on Save ");
                validateNames.ClickElement("SaveFieldManager");

                executionLog.Log(" AdminValidateDisplayNames", "Wait for work around");
                validateNames.WaitForWorkAround(3000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("AdminValidateDisplayNames");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("AdminValidateDisplayNames");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("AdminValidateDisplayNames", "Bug", "Medium", "Calls page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("AdminValidateDisplayNames");
                        TakeScreenshot("AdminValidateDisplayNames");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AdminValidateDisplayNames.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("AdminValidateDisplayNames");
                        string id = loginHelper.getIssueID("Admin Validate Display Names");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AdminValidateDisplayNames.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Admin Validate Display Names"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Admin Validate Display Names");
                //     executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("AdminValidateDisplayNames");
                executionLog.WriteInExcel("Admin Validate Display Names", Status, JIRA, "Office");
            }

        }
    }
}