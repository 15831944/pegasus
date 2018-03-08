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
    public class RateAndFeesEditWithAlreadyexistingName : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS3")]
        [TestCategory("BugTestNew")]
        public void rateAndFeesEditWithAlreadyexistingName()
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

            // Variable Random
            var TemplateName = "Test" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("RateAndFeesEditWithAlreadyexistingName", "Login with valid credentials");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("RateAndFeesEditWithAlreadyexistingName", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("RateAndFeesEditWithAlreadyexistingName", "Redirect To Create Rate and Fee Page");
                VisitCorp("masterdata/manage_rates_fees");
                corpMasterdata_RatesAndFeesHelper.WaitForWorkAround(2000);

                executionLog.Log("RateAndFeesEditWithAlreadyexistingName", "Verify Page title");
                VerifyTitle("Manage Master Rates & Fees");

                executionLog.Log("RateAndFeesEditWithAlreadyexistingName", "Enter Pricing template name");
                corpMasterdata_RatesAndFeesHelper.TypeText("PricingTemplateName", TemplateName);
                //corpMasterdata_RatesAndFeesHelper.WaitForWorkAround(3000);

                executionLog.Log("RateAndFeesEditWithAlreadyexistingName", "Select processor type");
                corpMasterdata_RatesAndFeesHelper.SelectByText("ProcessorType", "First Data Omaha");
                corpMasterdata_RatesAndFeesHelper.WaitForWorkAround(4000);

                executionLog.Log("RateAndFeesEditWithAlreadyexistingName", "Select Merchant Type");
                corpMasterdata_RatesAndFeesHelper.SelectByText("SelectMerchanType", "Ecommerce");
                //corpMasterdata_RatesAndFeesHelper.WaitForWorkAround(2000);

                executionLog.Log("RateAndFeesEditWithAlreadyexistingName", "Method of accepting card");
                corpMasterdata_RatesAndFeesHelper.SelectByText("MethodOfAcceptingCards", "Ecommerce");
                //corpMasterdata_RatesAndFeesHelper.WaitForWorkAround(2000);

                executionLog.Log("RateAndFeesEditWithAlreadyexistingName", "Select Discount Frequency ");
                corpMasterdata_RatesAndFeesHelper.SelectByText("DiscountFrequency", "Monthly");
                //corpMasterdata_RatesAndFeesHelper.WaitForWorkAround(2000);

                executionLog.Log("RateAndFeesEditWithAlreadyexistingName", "Save Rate and Fees");
                corpMasterdata_RatesAndFeesHelper.ClickElement("SaveEdit");
                //corpMasterdata_RatesAndFeesHelper.WaitForWorkAround(3000);

                executionLog.Log("RateAndFeesEditWithAlreadyexistingName", "wait for Text The Rates is successfully created!!");
                corpMasterdata_RatesAndFeesHelper.WaitForText("The Rates is successfully created!!", 10);

                executionLog.Log("RateAndFeesEditWithAlreadyexistingName", "Search Rate and fee");
                corpMasterdata_RatesAndFeesHelper.TypeText("SearchTemp", TemplateName);
                corpMasterdata_RatesAndFeesHelper.WaitForWorkAround(2000);

                executionLog.Log("RateAndFeesEditWithAlreadyexistingName", "Click On Edit");
                corpMasterdata_RatesAndFeesHelper.ForceClick("EditRateAndFeesIcon");
                corpMasterdata_RatesAndFeesHelper.WaitForWorkAround(2000);

                executionLog.Log("RateAndFeesEditWithAlreadyexistingName", "Enter Pricing template name");
                corpMasterdata_RatesAndFeesHelper.TypeText("PricingTemplateName", "Do not delete");

                executionLog.Log("RateAndFeesEditWithAlreadyexistingName", "Click on save button.");
                corpMasterdata_RatesAndFeesHelper.ForceClick("SaveEdit");

                executionLog.Log("RateAndFeesEditWithAlreadyexistingName", "Verify The Master Rates record is already exists");
                corpMasterdata_RatesAndFeesHelper.WaitForText("The Master Rates record is already exists.", 10);

                executionLog.Log("RateAndFeesEditWithAlreadyexistingName", "Redirect at rates and fee page.");
                VisitCorp("masterdata/rates_fees");
                corpMasterdata_RatesAndFeesHelper.WaitForWorkAround(3000);

                executionLog.Log("RateAndFeesEditWithAlreadyexistingName", "Search Rate and fee");
                corpMasterdata_RatesAndFeesHelper.TypeText("SearchTemp", TemplateName);
                corpMasterdata_RatesAndFeesHelper.WaitForWorkAround(2000);

                executionLog.Log("RateAndFeesEditWithAlreadyexistingName", "Click On Delete");
                corpMasterdata_RatesAndFeesHelper.ClickElement("ClickOnDelete");

                executionLog.Log("RateAndFeesEditWithAlreadyexistingName", "Accept Alert");
                corpMasterdata_RatesAndFeesHelper.AcceptAlert();

                executionLog.Log("RateAndFeesEditWithAlreadyexistingName", "Verify Text");
                corpMasterdata_RatesAndFeesHelper.WaitForText("The Rates is successfully deleted!!", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("RateAndFeesEditWithAlreadyexistingName");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Rate And Fees Edit With Already existing Name");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Rate And Fees Edit With Already existing Name", "Bug", "Medium", "rates And Fees page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Rate And Fees Edit With Already existing Name");
                        TakeScreenshot("RateAndFeesEditWithAlreadyexistingName");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\RateAndFeesEditWithAlreadyexistingName.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("RateAndFeesEditWithAlreadyexistingName");
                        string id = loginHelper.getIssueID("Rate And Fees Edit With Already existing Name");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\RateAndFeesEditWithAlreadyexistingName.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Rate And Fees Edit With Already existing Name"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Rate And Fees Edit With Already existing Name");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("RateAndFeesEditWithAlreadyexistingName");
                executionLog.WriteInExcel("Rate And Fees Edit With Already existing Name", Status, JIRA, "Corp Master Data");
            }
        }
    }
}
