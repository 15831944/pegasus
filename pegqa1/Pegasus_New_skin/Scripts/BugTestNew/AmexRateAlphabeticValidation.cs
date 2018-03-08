using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class AmexRateAlphabeticValidation : DriverTestCase
    {
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS2")]
        [TestCategory("BugTestNew")]
        [TestMethod]
        public void amexRateAlphabeticValidation()
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
            var masterData_AmexRateHelper = new MasterData_AmexRateHelper(GetWebDriver());

            String JIRA = "";
            String Status = "Pass";
            try
            {

                executionLog.Log("AmexRateAlphabeticValidation", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("AmexRateAlphabeticValidation", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("AmexRateAlphabeticValidation", "Click on Masterdata >> Amex Rates");
                VisitOffice("amex_rates");

                executionLog.Log("AmexRateAlphabeticValidation", "Click on any amex rate.");
                masterData_AmexRateHelper.ClickElement("OpenAnyAmextRate");

                executionLog.Log("AmexRateAlphabeticValidation", "Enter Alpha value in amex rate");
                masterData_AmexRateHelper.TypeText("AmexRates", "Amex");

                executionLog.Log("AmexRateAlphabeticValidation", "Enter alpha value in amex per item");
                masterData_AmexRateHelper.TypeText("AmexPerItem", "Test");

                executionLog.Log("AmexRateAlphabeticValidation", "Click on save button");
                masterData_AmexRateHelper.ClickElement("Save");

                executionLog.Log("AmexRateAlphabeticValidation", "Verify validation message on page");
                masterData_AmexRateHelper.VerifyText("AmexRatesError", "Please enter a valid number.");
                masterData_AmexRateHelper.WaitForWorkAround(2000);

                executionLog.Log("AmexRateAlphabeticValidation", "Enter a valid amex rate");
                masterData_AmexRateHelper.TypeText("AmexRates", "1.4");

                executionLog.Log("AmexRateAlphabeticValidation", "Enter a valid amex per item");
                masterData_AmexRateHelper.TypeText("AmexPerItem", "3.2");

                executionLog.Log("AmexRateAlphabeticValidation", "Click on save button");
                masterData_AmexRateHelper.ClickElement("Save");

                executionLog.Log("AmexRateAlphabeticValidation", "Wait for success message.");
                masterData_AmexRateHelper.WaitForText("The Amex Rates is successfully updated!!", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("AmexRateAlphabeticValidation");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Amex Rate Alphabetic Validation");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Amex Rate Alphabetic Validation", "Bug", "Medium", "Master data page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Amex Rate Alphabetic Validation");
                        TakeScreenshot("AmexRateAlphabeticValidation");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AmexRateAlphabeticValidation.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("AmexRateAlphabeticValidation");
                        string id = loginHelper.getIssueID("Amex Rate Alphabetic Validation");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AmexRateAlphabeticValidation.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Amex Rate Alphabetic Validation"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Amex Rate Alphabetic Validation");
               // executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("AmexRateAlphabeticValidation");
                executionLog.WriteInExcel("Amex Rate Alphabetic Validation", Status, JIRA, "Office MasterData");
            }
        }
    }
}