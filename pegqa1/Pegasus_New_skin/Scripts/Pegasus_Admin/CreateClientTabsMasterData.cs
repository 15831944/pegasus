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
    public class CreateClientTabsMasterData : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin")]
        [TestCategory("TS6")]
        [TestCategory("Pegasus_Admin")]
        public void createClientTabsMasterData()
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
            var office_FieldDictionary_TabsHelper = new Office_FieldDictionary_TabsHelper(GetWebDriver());
            var office_ClientHelper = new Office_ClientsHelper(GetWebDriver());

            //Variable
            var name = "Test" + GetRandomNumber();
            var num = "Test" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("CreateClientTabsMasterData", " Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("CreateClientTabsMasterData", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("CreateClientTabsMasterData", "Click On  Admin");
                VisitOffice("admin");

                executionLog.Log("CreateClientTabsMasterData", "Redirect To URL");
                VisitOffice("tabs");

                executionLog.Log("CreateClientTabsMasterData", "Verify title");
                VerifyTitle("Tabs Management");

                executionLog.Log("CreateClientTabsMasterData", "Select lead");
                office_FieldDictionary_TabsHelper.Select("TabsIn", "20");

                executionLog.Log("CreateClientTabsMasterData", "Click Create Btn");
                office_FieldDictionary_TabsHelper.ClickElement("Create");
                office_FieldDictionary_TabsHelper.WaitForWorkAround(1000);

                executionLog.Log("CreateClientTabsMasterData", "Enter Name");
                office_FieldDictionary_TabsHelper.TypeText("Name", name);

                executionLog.Log("CreateClientTabsMasterData", "Click on save button");
                office_FieldDictionary_TabsHelper.ClickElement("Save");

                executionLog.Log("CreateClientTabsMasterData", "Wait for text");
                office_FieldDictionary_TabsHelper.WaitForText("Tab Created Successfully", 30);

                executionLog.Log("CreateClientTabsMasterData", "Click On  Admin");
                VisitOffice("clients");

                executionLog.Log("CreateClientTabsMasterData", "Verify title");
                VerifyTitle();

                executionLog.Log("CreateClientTabsMasterData", "Click On Any Client");
                office_ClientHelper.ClickElement("Client1");

                executionLog.Log("CreateClientTabsMasterData", "Verify title");
                VerifyTitle("- Details");

                executionLog.Log("CreateClientTabsMasterData", "Verify text present");
                office_FieldDictionary_TabsHelper.WaitForText(name, 30);

                executionLog.Log("CreateClientTabsMasterData", "Redirect To URL");
                VisitOffice("tabs");

                executionLog.Log("CreateClientTabsMasterData", "Verify title");
                VerifyTitle("Tabs Management");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CreateClientTabsMasterData");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Create Client Tabs Master Data");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Create Client Tabs Master Data", "Bug", "Medium", "Tab page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Create Client Tabs Master Data");
                        TakeScreenshot("CreateClientTabsMasterData");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateClientTabsMasterData.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CreateClientTabsMasterData");
                        string id = loginHelper.getIssueID("Create Client Tabs Master Data");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateClientTabsMasterData.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Create Client Tabs Master Data"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Create Client Tabs Master Data");
            //    executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("CreateClientTabsMasterData");
                executionLog.WriteInExcel("Create Client Tabs Master Data", Status, JIRA, "Client Management");
            }
        }
    }
}