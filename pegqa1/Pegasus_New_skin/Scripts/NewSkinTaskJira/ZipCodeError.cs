using System;
using System.IO;
using LinqToExcel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class ZipCodeError : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS6")]
        [TestCategory("NewSkinTaskJira")]
        public void zipCodeError()
        {
            string[] username = null;
            string[] password = null;
            string[] log = null;
            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");
            log = oXMLData.getData("settings/URL", "logout");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var office_OpportunitiesHelper = new Office_OpportunitiesHelper(GetWebDriver());
            var office_LeadsHelper = new Office_LeadsHelper(GetWebDriver());
            var office_ClientsHelper = new Office_ClientsHelper(GetWebDriver());

            String JIRA = "";
            String Status = "Pass";
            var DBA = "DBA" + GetRandomNumber();



            try
            {
                executionLog.Log("ZipCodeError", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("ZipCodeError", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("ZipCodeError", "Go to Create Opportunity page");
                VisitOffice("opportunities/create");

                executionLog.Log("ZipCodeError", "Verify title");
                VerifyTitle("Create an Opportunity");

                executionLog.Log("ZipCodeError", "Enter zip code");
                office_OpportunitiesHelper.TypeText("ZipCode", "60601");

                executionLog.Log("ZipCodeError", "Click On anywhere");
                office_OpportunitiesHelper.ClickElement("V");

               
                executionLog.Log("ZipCodeError", "Logout from the application");
                VisitOffice("logout");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("ZipCodeError");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Zip Code Error");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Zip Code Error", "Bug", "Medium", "Client/Leads page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Zip Code Error");
                        TakeScreenshot("ZipCodeError");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ZipCodeError.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("ZipCodeError");
                        string id = loginHelper.getIssueID("Zip Code Error");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ZipCodeError.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Zip Code Error"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Zip Code Error");
           //      executionLog.DeleteFile("Error");
                 throw;

            }
            finally
            {
                executionLog.DeleteFile("ZipCodeError");
                executionLog.WriteInExcel("Zip Code Error", Status, JIRA, "Leads/Clients Management");
            }
            
        }
    }
}     