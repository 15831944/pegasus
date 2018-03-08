
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
    public class CreateLeadsTabsMasterData : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin")]
        [TestCategory("Temp")]
        [TestCategory("TS6")]
        [TestCategory("Pegasus_Admin")]
        public void createLeadsTabsMasterData()
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
            var office_FieldDictionary_TabsHelper = new Office_FieldDictionary_TabsHelper(GetWebDriver());
            var office_LeadsHelper = new Office_LeadsHelper(GetWebDriver());

            // Variable
            var name = "Test" + GetRandomNumber();
            var num = "Test" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("CreateLeadsTabsMasterData", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("CreateLeadsTabsMasterData", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("CreateLeadsTabsMasterData", "Click On  Admin");
                VisitOffice("admin");

                executionLog.Log("CreateLeadsTabsMasterData", "Redirect To tab page");
                VisitOffice("tabs");

                executionLog.Log("CreateLeadsTabsMasterData", "Verify title");
                VerifyTitle("Tabs Management");

                executionLog.Log("CreateLeadsTabsMasterData", "Select lead");
                office_FieldDictionary_TabsHelper.Selectbytext("TabsIn", "Leads");
                office_FieldDictionary_TabsHelper.WaitForWorkAround(2000);

                executionLog.Log("CreateLeadsTabsMasterData", "Click Create Btn");
                office_FieldDictionary_TabsHelper.ClickElement("Create");
                office_FieldDictionary_TabsHelper.WaitForWorkAround(2000);

                executionLog.Log("CreateLeadsTabsMasterData", "Enter Name");
                office_FieldDictionary_TabsHelper.TypeText("Name", name);

                executionLog.Log("CreateLeadsTabsMasterData", "Click on save button");
                office_FieldDictionary_TabsHelper.ClickElement("Save");

                executionLog.Log("CreateLeadsTabsMasterData", "wait for text");
                office_FieldDictionary_TabsHelper.WaitForText("Tab Created Successfully", 10);

                executionLog.Log("CreateLeadsTabsMasterData", "Click On Lead Tab ");
                VisitOffice("leads");

                executionLog.Log("CreateLeadsTabsMasterData", "Click On Any Client");
                office_LeadsHelper.ClickElement("ClickAnyLead");

                executionLog.Log("CreateLeadsTabsMasterData", "Verify title");
                VerifyTitle("- Details");

                executionLog.Log("CreateLeadsTabsMasterData", "Click on Company Details Tab");
                office_LeadsHelper.ClickElement("CompanyDetails");

                executionLog.Log("CreateLeadsTabsMasterData", "Verify text present");
                office_FieldDictionary_TabsHelper.WaitForText(name, 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CreateLeadsTabsMasterData");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Create Leads Tabs Master Data");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Create Leads Tabs Master Data", "Bug", "Medium", "Create Leads Tabs page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Create Leads Tabs Master Data");
                        TakeScreenshot("CreateLeadsTabsMasterData");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateLeadsTabsMasterData.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CreateLeadsTabsMasterData");
                        string id = loginHelper.getIssueID("Create Leads Tabs Master Data");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateLeadsTabsMasterData.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Create Leads Tabs Master Data"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Create Leads Tabs Master Data");
            //    executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("CreateLeadsTabsMasterData");
                executionLog.WriteInExcel("Create Leads Tabs Master Data", Status, JIRA, "Leads Management");
            }
        }
    }
}
