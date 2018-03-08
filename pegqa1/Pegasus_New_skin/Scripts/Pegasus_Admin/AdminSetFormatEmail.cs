/*
* The AdminSetFormatEmail.cs Tests the ability to set a field format
* to a Email. It will first check the field's existance, next set the 
* validation type to Email , confirm the new validation and finally
* set the field back to normal.
* AdminSetFormatEmail.cs works with AdminSetFormatEmail.cs 
* and AdminSetFormatEmail.xml  
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
    public class AdminSetFormatEmail : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin")]
        [TestCategory("TS6")]
        [TestCategory("Pegasus_Admin")]
        public void setFormatToEmail()
        {
            string[] username = null;
            string[] password = null;
            DateTime date = new DateTime();

            XMLParse oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            //Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var email = new AdminSetFormatFieldsHelper(GetWebDriver());
            var office_ClientsHelper = new Office_ClientsHelper(GetWebDriver());

            //Random Variable
            String JIRA = "";
            String Status = "Pass";
            var DBAName = "DBA@Company" + RandomNumber(1, 999);

            try
            {

            //executionLog.Log("AdminSetFormatEmail", "WaitForWorkAround");
            //email.WaitForWorkAround(2000);

            //Logging in 

            executionLog.Log("AdminSetFormatEmail", "Login");
            Login(username[0], password[0]);

            //executionLog.Log("AdminSetFormatEmail", "WaitForWorkAround");
            //email.WaitForWorkAround(4000);

            executionLog.Log("AdminSetFormatEmail", "Verify Title");
            VerifyTitle("Dashboard");
            //email.WaitForWorkAround(3000);

            // Going to admin office 

            //executionLog.Log("AdminSetFormatEmail", "Visit office");
            //VisitOffice("admin");

            // Accessing field in field dictionary            

            VisitOffice("fields");
            email.WaitForWorkAround(3000);

            executionLog.Log("AdminSetFormatEmail", "Select module as clients");
            email.SelectByText("Module", "Clients");
            email.WaitForWorkAround(2000);

            executionLog.Log("AdminSetFormatEmail", "Select Processor as First Data North");
            email.SelectByText("Processor", "First Data North");
            //email.WaitForWorkAround(2000);

            executionLog.Log("AdminSetFormatEmail", "Select Tab as Contacts");
            email.SelectByText("Tab", "Contacts");
            email.WaitForWorkAround(2000);

            executionLog.Log("AdminSetFormatEmail", "Select Section as Contacts");
            email.SelectByText("Section", "Contacts");
            email.WaitForWorkAround(2000);

            executionLog.Log("AdminSetFormatEmail", "Click Search");
            email.ClickElement("Search");
            email.WaitForWorkAround(3000);

            executionLog.Log("AdminSetFormatEmail", "Type Text");
            email.TypeText("filter", "Title");
            email.WaitForWorkAround(2000);

            executionLog.Log("AdminSetFormatEmail", "Click TitleField");
            email.ClickElement("ContactTitle");
            email.WaitForWorkAround(4000);

            executionLog.Log("AdminSetFormatEmail", "Click FieldFormat");
            email.checkAndClick("ProcFieldFormat");
            email.WaitForWorkAround(2000);

            executionLog.Log("AdminSetFormatPhone", "Select data type as phone.");
            email.SelectByText("ProcFieldContntType", "Email");
            //email.WaitForWorkAround(2000);

            executionLog.Log("AdminSetFormatEmail", "Click SaveFieldManager");
            email.ClickElement("SaveFieldManager");
            email.WaitForWorkAround(3000);

            executionLog.Log("AdminSetFormatEmail", "Redirect at Create Client");
            VisitOffice("clients/create");
            email.WaitForWorkAround(3000);

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

            executionLog.Log("AdminSetFormatEmail", "Enter the text");
            email.TypeText("Title", "Tester01");

            email.ClickForce("Save");
            email.WaitForWorkAround(2000);

            executionLog.Log("AdminSetFormatEmail", "Verify the Email Format Validation");
            email.VerifyText("FieldFormatEmailError", "Please enter a valid email address.");

            executionLog.Log("AdminSetFormatEmail", "Redirect To clients page. ");
            VisitOffice("clients");
            office_ClientsHelper.WaitForWorkAround(3000);

            executionLog.Log("AdminSetFormatEmail", "Enter Company Name");
            office_ClientsHelper.TypeText("SearchClient", DBAName);
            office_ClientsHelper.WaitForWorkAround(2000);

            executionLog.Log("AdminSetFormatEmail", "Select client by check box");
            office_ClientsHelper.ClickElement("ClickOn1stOpp");
            office_ClientsHelper.WaitForWorkAround(2000);

            executionLog.Log("AdminSetFormatEmail", "Click on delete client");
            office_ClientsHelper.ClickElement("DeleteClient");

            executionLog.Log("AdminSetFormatEmail", "Accept alert message.");
            office_ClientsHelper.AcceptAlert();

            executionLog.Log("AdminSetFormatEmail", "Wait for success message.");
            office_ClientsHelper.WaitForText("1 records deleted successfully", 10);

            executionLog.Log("AdminSetFormatEmail", "Redirect To client recycle bin page. ");
            VisitOffice("clients/recyclebin");
            office_ClientsHelper.WaitForWorkAround(3000);

            executionLog.Log("AdminSetFormatEmail", "Enter Company Name");
            office_ClientsHelper.TypeText("SearchClient", DBAName);
            office_ClientsHelper.WaitForWorkAround(2000);

            executionLog.Log("AdminSetFormatEmail", "Click on delete client");
            office_ClientsHelper.ClickElement("DeleteRbin");

            executionLog.Log("AdminSetFormatEmail", "Accept alert message.");
            office_ClientsHelper.AcceptAlert();

            executionLog.Log("AdminSetFormatEmail", "Wait for success message.");
            office_ClientsHelper.WaitForText("Client Permanently Deleted.", 10);


            // Accessing field in field dictionary

            VisitOffice("fields");
            email.WaitForWorkAround(3000);

            executionLog.Log("AdminSetFormatEmail", "Select module as clients");
            email.SelectByText("Module", "Clients");
            email.WaitForWorkAround(1000);

            executionLog.Log("AdminSetFormatEmail", "Select Processor as First Data North");
            email.SelectByText("Processor", "First Data North");
            email.WaitForWorkAround(2000);

            executionLog.Log("AdminSetFormatEmail", "Select Tab as Contacts");
            email.SelectByText("Tab", "Contacts");
            email.WaitForWorkAround(2000);

            executionLog.Log("AdminSetFormatEmail", "Select Section as Contacts");
            email.SelectByText("Section", "Contacts");
            email.WaitForWorkAround(2000);

            executionLog.Log("AdminSetFormatEmail", "Click Search");
            email.ClickElement("Search");
            email.WaitForWorkAround(3000);

            executionLog.Log("AdminSetFormatEmail", "Type text");
            email.TypeText("filter", "Title");
            email.WaitForWorkAround(3000);

            executionLog.Log("AdminSetFormatEmail", "Click TitleField");
            email.ClickElement("ContactTitle");
            email.WaitForWorkAround(3000);

            executionLog.Log("AdminSetFormatEmail", "Click FieldFormat");
            email.ClickElement("ProcFieldFormat");
            email.WaitForWorkAround(2000);

            executionLog.Log("AdminSetFormatEmail", "Click SaveFieldManager");
            email.ClickElement("SaveFieldManager");
            email.WaitForWorkAround(3000);

            }

            catch (Exception e)
            {

                Console.WriteLine(date.TimeOfDay.Duration());
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";
                Console.WriteLine(date.TimeOfDay.Duration());
                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("AdminSetFormatEmail");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                Console.WriteLine(date.TimeOfDay.Duration());
                bool result = loginHelper.CheckExstingIssue("Admin Set Format Email");
                Console.WriteLine(date.TimeOfDay.Duration());
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Admin Set Format Email Helper", "Bug", "Medium", "Client page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Admin Set Format Email");
                        TakeScreenshot("AdminSetFormatEmail");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AdminSetFormatEmail.png";
                        loginHelper.AddAttachment(location, id);
                    }
                    Console.WriteLine(date.TimeOfDay.Duration());
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("AdminSetFormatEmail");
                        string id = loginHelper.getIssueID("Admin Set Format Email");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AdminSetFormatEmail.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Admin Set Format Email"), "This issue is still occurring");
                    }
                    Console.WriteLine(date.TimeOfDay.Duration());
                }
                JIRA = loginHelper.getIssueID("Admin Set Format Email");
                Console.WriteLine(date.TimeOfDay.Duration());
                //executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("Admin Set Format Email");
                Console.WriteLine(date.TimeOfDay.Duration());
                executionLog.WriteInExcel("Admin Set Format Email", Status, JIRA, "Partner Portal");
            }
        }
    }
}