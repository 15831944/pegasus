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
    public class EditRatesAndFeesCorp : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("Fail")]
        [TestCategory("TS2")]
        [TestCategory("BugTestNew")]
        public void editRatesAndFeesCorp()
        {
            string[] username = null;
            string[] password = null;

            XMLParse oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var corpMasterdata_RatesAndFeesHelper = new CorpMasterdata_RatesAndFeesHelper(GetWebDriver());

            username = oXMLData.getData("settings/Credentials", "username_corp");
            password = oXMLData.getData("settings/Credentials", "password");

            // Variable Random.
            var name = "TEST COMPANY" + GetRandomNumber();
            var TemplateName = "Test" + GetRandomNumber();
            var LastName = "Tester" + GetRandomNumber();
            var Number = "12345678" + GetRandomNumber();

            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("EditRatesAndFeesCorp", "Login with valid credentials");
                Login(username[0], password[0]);

                executionLog.Log("EditRatesAndFeesCorp", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("EditRatesAndFeesCorp", "Redirect To URL");
                VisitCorp("masterdata/manage_rates_fees");
                corpMasterdata_RatesAndFeesHelper.WaitForWorkAround(2000);

                executionLog.Log("EditRatesAndFeesCorp", "Verify Page title");
                VerifyTitle("Manage Master Rates & Fees");
                corpMasterdata_RatesAndFeesHelper.WaitForWorkAround(2000);

                executionLog.Log("EditRatesAndFeesCorp", "Enter Pricing template name");
                corpMasterdata_RatesAndFeesHelper.TypeText("PricingTemplateName", TemplateName);
                corpMasterdata_RatesAndFeesHelper.WaitForWorkAround(2000);

                executionLog.Log("EditRatesAndFeesCorp", "Select processor type");
                corpMasterdata_RatesAndFeesHelper.SelectByText("ProcessorType", "First Data Omaha");
                corpMasterdata_RatesAndFeesHelper.WaitForWorkAround(5000);

                executionLog.Log("EditRatesAndFeesCorp", "Select Merchant Type");
                corpMasterdata_RatesAndFeesHelper.SelectByText("SelectMerchanType", "Ecommerce");
                corpMasterdata_RatesAndFeesHelper.WaitForWorkAround(2000);

                executionLog.Log("EditRatesAndFeesCorp", "Method of accepting card");
                corpMasterdata_RatesAndFeesHelper.SelectByText("MethodOfAcceptingCards", "Ecommerce");
                corpMasterdata_RatesAndFeesHelper.WaitForWorkAround(2000);

                executionLog.Log("EditRatesAndFeesCorp", "Select Discount Frequency ");
                corpMasterdata_RatesAndFeesHelper.SelectByText("DiscountFrequency", "Monthly");
                corpMasterdata_RatesAndFeesHelper.WaitForWorkAround(2000);

                executionLog.Log("EditRatesAndFeesCorp", "Select Debit Network InterFace Pass Through");
                corpMasterdata_RatesAndFeesHelper.SelectByText("DebitNetworkInterFacePassThrough", "Yes");
                corpMasterdata_RatesAndFeesHelper.WaitForWorkAround(3000);

                executionLog.Log("EditRatesAndFeesCorp", "Select Debit Network InterFace Pass Through");
                corpMasterdata_RatesAndFeesHelper.TypeText("VisaCreditQualified", "30");

                executionLog.Log("EditRatesAndFeesCorp", "Save Edit Rate and Fees");
                corpMasterdata_RatesAndFeesHelper.ClickElement("SaveEdit");

                executionLog.Log("EditRatesAndFeesCorp", "Verfy Text The Rates is successfully created!!");
                corpMasterdata_RatesAndFeesHelper.WaitForText("The Rates is successfully created!!", 10);

                executionLog.Log("EditRatesAndFeesCorp", "Search Rate and fee");
                corpMasterdata_RatesAndFeesHelper.TypeText("SearchTemp", TemplateName);
                corpMasterdata_RatesAndFeesHelper.WaitForWorkAround(4000);

                executionLog.Log("EditRatesAndFeesCorp", "Click On Delete");
                corpMasterdata_RatesAndFeesHelper.ClickElement("EditRateAndFeesIcon");

                executionLog.Log("EditRatesAndFeesCorp", "Click On Delete");
                corpMasterdata_RatesAndFeesHelper.VerifyTextNot("Please enter no more than 6 characters.");
                corpMasterdata_RatesAndFeesHelper.WaitForWorkAround(2000);

                executionLog.Log("EditRatesAndFeesCorp", "Save Edit Rate and Fees");
                corpMasterdata_RatesAndFeesHelper.ClickElement("SaveEdit");

                executionLog.Log("EditRatesAndFeesCorp", "Search Rate and fee");
                corpMasterdata_RatesAndFeesHelper.TypeText("SearchTemp", TemplateName);
                corpMasterdata_RatesAndFeesHelper.WaitForWorkAround(2000);

                executionLog.Log("EditRatesAndFeesCorp", "Click On Delete");
                corpMasterdata_RatesAndFeesHelper.ClickElement("ClickOnDelete");

                executionLog.Log("EditRatesAndFeesCorp", "Accept Alert");
                corpMasterdata_RatesAndFeesHelper.AcceptAlert();

                executionLog.Log("EditRatesAndFeesCorp", "Verify Text");
                corpMasterdata_RatesAndFeesHelper.WaitForText("The Rates is successfully deleted!!", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("EditRatesAndFeesCorp");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Edit Rates And Fees Corp");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Edit Rates And Fees Corp", "Bug", "Medium", "rates And Fees page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Edit Rates And Fees Corp");
                        TakeScreenshot("EditRatesAndFeesCorp");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditRatesAndFeesCorp.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("EditRatesAndFeesCorp");
                        string id = loginHelper.getIssueID("Edit Rates And Fees Corp");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditRatesAndFeesCorp.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Edit Rates And Fees Corp"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Edit Rates And Fees Corp");
                //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("EditRatesAndFeesCorp");
                executionLog.WriteInExcel("Edit Rates And Fees Corp", Status, JIRA, "Corp Master Data");
            }
        }
    }
}
