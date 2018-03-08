using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class CreateLeadsSectionMasterData : DriverTestCase
    {
        [TestMethod]
        [TestCategory("Admin")]
        [TestCategory("All")]
        [TestCategory("TS6")]
        [TestCategory("Pegasus_Admin")]
        public void createLeadsSectionMasterData()
        {
            string[] username = null;
            string[] password = null;

            XMLParse oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var office_FieldDictionary_SectionsHelper = new Office_FieldDictionary_SectionsHelper(GetWebDriver());
            var office_LeadsHelper = new Office_LeadsHelper(GetWebDriver());

            // Variable
            String name = "Test" + GetRandomNumber();
            String num = "Test" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {

            executionLog.Log("CreateLeadsSectionMasterData", "Login with valid username and password");
            Login(username[0], password[0]);
            Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

            executionLog.Log("CreateLeadsSectionMasterData", "Verify Page title");
            VerifyTitle("Dashboard");
            office_FieldDictionary_SectionsHelper.WaitForWorkAround(3000);

            executionLog.Log("CreateLeadsSectionMasterData", "Click On  Admin");
            VisitOffice("admin");
            office_FieldDictionary_SectionsHelper.WaitForWorkAround(3000);

            executionLog.Log("CreateLeadsSectionMasterData", "Redirect To URL");
            VisitOffice("sections");
            office_FieldDictionary_SectionsHelper.WaitForWorkAround(3000);

            executionLog.Log("CreateLeadsSectionMasterData", "Verify title");
            VerifyTitle("Section Management");
            office_FieldDictionary_SectionsHelper.WaitForWorkAround(3000);

            executionLog.Log("CreateLeadsSectionMasterData", "Select lead");
            office_FieldDictionary_SectionsHelper.Selectbytext("SelectModule", "Leads");
            office_FieldDictionary_SectionsHelper.WaitForWorkAround(3000);

            executionLog.Log("CreateLeadsSectionMasterData", "Click Create Btn");
            office_FieldDictionary_SectionsHelper.ClickElement("Create");
            office_FieldDictionary_SectionsHelper.WaitForWorkAround(3000);

            executionLog.Log("CreateLeadsSectionMasterData", "Select TAB");
            office_FieldDictionary_SectionsHelper.Selectbytext("TabName", "Company Details");
            office_FieldDictionary_SectionsHelper.WaitForWorkAround(1000);

            executionLog.Log("CreateLeadsSectionMasterData", "Enter section Name");
            office_FieldDictionary_SectionsHelper.TypeText("Name", name);
            office_FieldDictionary_SectionsHelper.WaitForWorkAround(3000);

            executionLog.Log("CreateLeadsSectionMasterData", "Click on save button");
            office_FieldDictionary_SectionsHelper.ClickElement("Save");
            office_FieldDictionary_SectionsHelper.WaitForWorkAround(3000);

            executionLog.Log("CreateLeadsSectionMasterData", "Accept Alert");
            office_FieldDictionary_SectionsHelper.AcceptAlert();
            office_FieldDictionary_SectionsHelper.WaitForWorkAround(3000);

            executionLog.Log("CreateLeadsSectionMasterData", "Click On Lead Tab ");
            VisitOffice("leads");
            office_FieldDictionary_SectionsHelper.WaitForWorkAround(3000);

            VerifyTitle("Leads");
            office_FieldDictionary_SectionsHelper.WaitForWorkAround(3000);

            executionLog.Log("CreateLeadsSectionMasterData", "Click On Any Lead");
            office_LeadsHelper.ClickElement("ClickAnyLead");
            office_FieldDictionary_SectionsHelper.WaitForWorkAround(3000);

            executionLog.Log("CreateLeadsSectionMasterData", "Verify title");
            VerifyTitle("- Details");
            office_FieldDictionary_SectionsHelper.WaitForWorkAround(3000);

            executionLog.Log("CreateLeadsSectionMasterData", "Click on Company Details Tab");
            office_LeadsHelper.ClickElement("CompanyDetails");
            office_FieldDictionary_SectionsHelper.WaitForWorkAround(3000);

            VisitOffice("logout");

        }
      catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CreateLeadsSectionMasterData");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Create Leads Section Master Data");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Create Leads Section Master Data", "Bug", "Medium", "Create Leads page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Create Leads Section Master Data");
                        TakeScreenshot("CreateLeadsSectionMasterData");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateLeadsSectionMasterData.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CreateLeadsSectionMasterData");
                        string id = loginHelper.getIssueID("Create Leads Section Master Data");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateLeadsSectionMasterData.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Create Leads Section Master Data"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Create Leads Section Master Data");
             //   executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("CreateLeadsSectionMasterData");
                executionLog.WriteInExcel("Create Leads Section Master Data", Status, JIRA, "Leads Management");
            }
        }
    }
}