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
    public class VerifyClientTabs : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin")]
        public void verifyClientTabs()
        {
            string[] username = null;
            string[] password = null;

            XMLParse oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username");
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

   //         try
     //       {

                executionLog.Log("VerifyClientTabs", " Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyClientTabs", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyClientTabs", "Click On  Admin");
                VisitOffice("admin");

                executionLog.Log("VerifyClientTabs", "Redirect To URL");
                VisitOffice("tabs");

                executionLog.Log("VerifyClientTabs", "Verify title");
                VerifyTitle("Tabs Management");

                executionLog.Log("VerifyClientTabs", "Select lead");
                office_FieldDictionary_TabsHelper.Select("TabsIn", "20");

                executionLog.Log("VerifyClientTabs", "Click Create Btn");
                office_FieldDictionary_TabsHelper.ClickElement("Create");
                office_FieldDictionary_TabsHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyClientTabs", "Enter Name");
                office_FieldDictionary_TabsHelper.TypeText("Name", name);

                executionLog.Log("VerifyClientTabs", "Click on save button");
                office_FieldDictionary_TabsHelper.ClickElement("Save");

                executionLog.Log("VerifyClientTabs", "Wait for success text.");
                office_FieldDictionary_TabsHelper.WaitForText("Tab Created Successfully", 10);

                executionLog.Log("VerifyClientTabs", "Redirect at clients page.");
                VisitOffice("clients");

                executionLog.Log("VerifyClientTabs", "Verify title");
                VerifyTitle("Clients");

                executionLog.Log("VerifyClientTabs", "Click On Any Client");
                office_ClientHelper.ClickElement("Client1");

                executionLog.Log("VerifyClientTabs", "Verify title");
                VerifyTitle("- Details");

                executionLog.Log("VerifyClientTabs", "Verify tab present on client page.");
                office_FieldDictionary_TabsHelper.WaitForText(name, 30);

                executionLog.Log("VerifyClientTabs", "Redirect To URL");
                VisitOffice("tabs");

                executionLog.Log("VerifyClientTabs", "Verify title");
                VerifyTitle("Tabs Management");




            }
            } }
      /*      catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyClientTabs");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Client Tabs");
                if (!result)
                {
                    if (Int16.Parse(counter) < 5)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Client Tabs", "Bug", "Medium", "Tab page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Client Tabs");
                        TakeScreenshot("VerifyClientTabs");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyClientTabs.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 5)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyClientTabs");
                        string id = loginHelper.getIssueID("Verify Client Tabs");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyClientTabs.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Client Tabs"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Client Tabs");
                executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("VerifyClientTabs");
                executionLog.WriteInExcel("Verify Client Tabs", Status, JIRA, "Client Management");
            }
        }
    }
}*/