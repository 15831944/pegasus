using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
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
    public class CreateClientSectionMasterData : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin")]
        [TestCategory("TS6")]
        [TestCategory("Pegasus_Admin")]
        public void createClientSectionMasterData()
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
            var office_ClientsHelper = new Office_ClientsHelper(GetWebDriver());

            // Variable
            var name = "Test" + GetRandomNumber();
            var num = "Test" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("CreateClientSectionMasterData", " Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("CreateClientSectionMasterData", " Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("CreateClientSectionMasterData", " Click On  Admin");
                VisitOffice("admin");

                executionLog.Log("CreateClientSectionMasterData", " Redirect To URL");
                VisitOffice("sections");

                executionLog.Log("CreateClientSectionMasterData", "  Verify title");
                VerifyTitle("Section Management");

                executionLog.Log("CreateClientSectionMasterData", " Select Module");
                office_FieldDictionary_SectionsHelper.Selectbytext("SelectModule", "Clients");

                executionLog.Log("CreateClientSectionMasterData", " Click on Create Btn");
                office_FieldDictionary_SectionsHelper.ClickElement("Create");
                office_FieldDictionary_SectionsHelper.WaitForWorkAround(2000);

                executionLog.Log("CreateClientSectionMasterData", " Select TAB");
                office_FieldDictionary_SectionsHelper.Selectbytext("TabName", "Company Details");
                office_FieldDictionary_SectionsHelper.WaitForWorkAround(1000);

                executionLog.Log("CreateClientSectionMasterData", " Enter Name");
                office_FieldDictionary_SectionsHelper.TypeText("Name", name);

                executionLog.Log("CreateClientSectionMasterData", " Click on save button");
                office_FieldDictionary_SectionsHelper.ClickElement("Save");
                office_FieldDictionary_SectionsHelper.WaitForWorkAround(2000);

                executionLog.Log("CreateClientSectionMasterData", " Accept Alert");
                office_FieldDictionary_SectionsHelper.AcceptAlert();
                office_FieldDictionary_SectionsHelper.WaitForWorkAround(1000);

                executionLog.Log("CreateClientSectionMasterData", " Go to clients Tab ");
                VisitOffice("clients");

                executionLog.Log("CreateClientSectionMasterData", " Verify title");
                VerifyTitle();

                executionLog.Log("CreateClientSectionMasterData", " Click On Any Client");
                office_ClientsHelper.ClickElement("Client1");

                executionLog.Log("CreateClientSectionMasterData", " Click on Company Details Tab");
                office_ClientsHelper.ClickElement("CompanyDetails");

                executionLog.Log("CreateClientSectionMasterData", " Redirect To URL");
                VisitOffice("sections");

                executionLog.Log("CreateClientSectionMasterData", "  Verify title");
                VerifyTitle("Section Management");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("CreateClientSectionMasterData", " Deletes the created section.");
                office_FieldDictionary_SectionsHelper.DeleteSection(name);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CreateClientSectionMasterData");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Create Client Section MasterData");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Create Client Section MasterData", "Bug", "Medium", "Section page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Create Client Section MasterData");
                        TakeScreenshot("CreateClientSectionMasterData");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateClientSectionMasterData.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CreateClientSectionMasterData");
                        string id = loginHelper.getIssueID("Create Client Section MasterData");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateClientSectionMasterData.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Create Client Section MasterData"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Create Client Section MasterData");
             //   executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("CreateClientSectionMasterData");
                executionLog.WriteInExcel("Create Client Section MasterData", Status, JIRA, "Field dictionary Management");
            }
        }
    }
}