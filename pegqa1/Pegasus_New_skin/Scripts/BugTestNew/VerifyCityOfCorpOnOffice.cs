using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class VerifyCityOfCorpOnOffice : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("BugTestNew")]
        public void verifyCityOfCorpOnOffice()
        {

            string[] username = null;
            string[] username1 = null;
            string[] password = null;


            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_corp");
            username1 = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var corp_HomepageHelper = new Corp_HomepageHelper(GetWebDriver());
            var officeAdmin_CorporateHelper = new OfficeAdmin_CorporateHelper(GetWebDriver());

            //  Variable random 
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("VerifyCityOfCorpOnOffice", "Login to Corp with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyCityOfCorpOnOffice", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected to Dashboard");

                executionLog.Log("VerifyCityOfCorpOnOffice", "Click on Edit Details button");
                corp_HomepageHelper.ClickElement("EditDetails");
                corp_HomepageHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCityOfCorpOnOffice", "Get Zip Code of Address");
                var zip = corp_HomepageHelper.getInputText("//input[@id='CorporateAddress0PostalCode']");

                executionLog.Log("VerifyCityOfCorpOnOffice", "Clear Zip Code of Address");
                corp_HomepageHelper.ClearTextBoxValue("//input[@id='CorporateAddress0PostalCode']");

                executionLog.Log("VerifyCityOfCorpOnOffice", "Enter Zip Code of Address");
                corp_HomepageHelper.TypeText("ZipCode1", zip);
                corp_HomepageHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCityOfCorpOnOffice", "Get City of Address");
                var city = corp_HomepageHelper.getInputText("//input[@id='CorporateAddress0City']");

                executionLog.Log("VerifyCityOfCorpOnOffice", "Click on Save button");
                corp_HomepageHelper.ClickElement("Save");
                corp_HomepageHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCityOfCorpOnOffice", "Logout from Corp portal");
                corp_HomepageHelper.ClickElement("ProfileIcon");
                corp_HomepageHelper.ClickElement("Logout");
                corp_HomepageHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCityOfCorpOnOffice", "Login to Office with valid username and password");
                Login(username1[0], password[0]);
                Console.WriteLine("Logged in as: " + username1[0] + " / " + password[0]);

                executionLog.Log("VerifyCityOfCorpOnOffice", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected to Dashboard");

                executionLog.Log("VerifyCityOfCorpOnOffice", "Redirect to Corporate Details page");
                VisitOffice("mycorp");
                officeAdmin_CorporateHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCityOfCorpOnOffice", "Verify City name is appearing in Address");
                officeAdmin_CorporateHelper.VerifyText("Address1", city);
                Console.WriteLine("City is displayed");

             }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyCityOfCorpOnOffice");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify City Of Corp On Office");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify City Of Corp On Office", "Bug", "Medium", "Reports page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify City Of Corp On Office");
                        TakeScreenshot("VerifyCityOfCorpOnOffice");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyCityOfCorpOnOffice.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyCityOfCorpOnOffice");
                        string id = loginHelper.getIssueID("Verify City Of Corp On Office");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyCityOfCorpOnOffice.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify City Of Corp On Office"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify City Of Corp On Office");
                //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyCityOfCorpOnOffice");
                executionLog.WriteInExcel("Verify City Of Corp On Office", Status, JIRA, "Office Reports");
            }
            
        }
    }
}