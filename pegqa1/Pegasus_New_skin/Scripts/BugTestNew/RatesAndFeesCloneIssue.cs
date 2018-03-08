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
    public class RatesAndFeesCloneIssue : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("Fail")]
        [TestCategory("TS3")]
        [TestCategory("BugTestNew")]
        public void ratesAndFeesCloneIssue()
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

            // Variable
            var TemplateName = "Test" + GetRandomNumber();
            var Clone = "Clone of " + TemplateName;

            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("RatesAndFeesCloneIssue", "Login with valid credentials");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("RatesAndFeesCloneIssue", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("RatesAndFeesCloneIssue", "Redirect To URL");
                VisitCorp("masterdata/manage_rates_fees");
                corpMasterdata_RatesAndFeesHelper.WaitForWorkAround(3000);

                executionLog.Log("RatesAndFeesCloneIssue", "Verify Page title");
                VerifyTitle("Manage Master Rates & Fees");
                //corpMasterdata_RatesAndFeesHelper.WaitForWorkAround(4000);

                executionLog.Log("RatesAndFeesCloneIssue", "Enter template name");
                corpMasterdata_RatesAndFeesHelper.TypeText("PricingTemplateName", TemplateName);

                executionLog.Log("RatesAndFeesCloneIssue", "Select Processor Type");
                corpMasterdata_RatesAndFeesHelper.SelectByText("ProcessorType", "First Data Omaha");
                corpMasterdata_RatesAndFeesHelper.WaitForWorkAround(4000);

                executionLog.Log("RatesAndFeesCloneIssue", "Select Merchant Type");
                corpMasterdata_RatesAndFeesHelper.SelectByText("SelectMerchanType", "Ecommerce");
                //corpMasterdata_RatesAndFeesHelper.WaitForWorkAround(2000);

                executionLog.Log("RatesAndFeesCloneIssue", "Method of accepting card");
                corpMasterdata_RatesAndFeesHelper.SelectByText("MethodOfAcceptingCards", "Ecommerce");
                //corpMasterdata_RatesAndFeesHelper.WaitForWorkAround(2000);

                executionLog.Log("RatesAndFeesCloneIssue", "Select Discount Frequency");
                corpMasterdata_RatesAndFeesHelper.SelectByText("DiscountFrequency", "Monthly");
                //corpMasterdata_RatesAndFeesHelper.WaitForWorkAround(3000);

                executionLog.Log("RatesAndFeesCloneIssue", "Select Debit Network InterFace Pass Through");
                corpMasterdata_RatesAndFeesHelper.SelectByText("DebitNetworkInterFacePassThrough", "Yes");

                executionLog.Log("RatesAndFeesCloneIssue", "Click on Save Rates And fees");
                corpMasterdata_RatesAndFeesHelper.ForceClick("SaveEdit");

                executionLog.Log("RatesAndFeesCloneIssue", "Verify Text The Rates is successfully created!!");
                corpMasterdata_RatesAndFeesHelper.WaitForText("The Rates is successfully created!!", 10);

                executionLog.Log("RatesAndFeesCloneIssue", "Search Rate and fee by template name");
                corpMasterdata_RatesAndFeesHelper.TypeText("SearchTemp", TemplateName);
                corpMasterdata_RatesAndFeesHelper.WaitForWorkAround(2000);

                executionLog.Log("RatesAndFeesCloneIssue", "Click On Copy Master Rates and fee.");
                corpMasterdata_RatesAndFeesHelper.ClickElement("CloneRatesAndFee");
                corpMasterdata_RatesAndFeesHelper.WaitForWorkAround(3000);

                executionLog.Log("RatesAndFeesCloneIssue", "Click On Save button");
                corpMasterdata_RatesAndFeesHelper.ForceClick("SaveEdit");
                corpMasterdata_RatesAndFeesHelper.WaitForWorkAround(3000);

                executionLog.Log("RatesAndFeesCloneIssue", "Search Cloned Rates and fee.");
                corpMasterdata_RatesAndFeesHelper.TypeText("SearchTemp", Clone);
                corpMasterdata_RatesAndFeesHelper.WaitForWorkAround(2000);

                executionLog.Log("RatesAndFeesCloneIssue", "Verify clone rates and fee.");
                corpMasterdata_RatesAndFeesHelper.VerifyText("Clone1", Clone);

                executionLog.Log("RatesAndFeesCloneIssue", "Search Rate and fee");
                corpMasterdata_RatesAndFeesHelper.TypeText("SearchTemp", TemplateName);
                corpMasterdata_RatesAndFeesHelper.WaitForWorkAround(2000);

                executionLog.Log("RatesAndFeesCloneIssue", "Click On Delete");
                corpMasterdata_RatesAndFeesHelper.ClickElement("ClickOnDelete");

                executionLog.Log("RatesAndFeesCloneIssue", "Accept Alert");
                corpMasterdata_RatesAndFeesHelper.AcceptAlert();

                executionLog.Log("RatesAndFeesCloneIssue", "Verify Text");
                corpMasterdata_RatesAndFeesHelper.WaitForText("The Rates is successfully deleted!!", 40);
                corpMasterdata_RatesAndFeesHelper.WaitForWorkAround(4000);

                executionLog.Log("RatesAndFeesCloneIssue", "Search Rate and fee");
                corpMasterdata_RatesAndFeesHelper.TypeText("SearchTemp", Clone);
                corpMasterdata_RatesAndFeesHelper.WaitForWorkAround(2000);

                executionLog.Log("RatesAndFeesCloneIssue", "Click On Delete");
                corpMasterdata_RatesAndFeesHelper.ClickElement("ClickOnDelete");

                executionLog.Log("RatesAndFeesCloneIssue", "Accept Alert");
                corpMasterdata_RatesAndFeesHelper.AcceptAlert();

                executionLog.Log("RatesAndFeesCloneIssue", "Verify Text");
                corpMasterdata_RatesAndFeesHelper.WaitForText("The Rates is successfully deleted!!", 40);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("RatesAndFeesCloneIssue");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Rates And Fees Clone Issue");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Rates And Fees Clone Issue", "Bug", "Medium", "rates And Fees page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Rates And Fees Clone Issue");
                        TakeScreenshot("RatesAndFeesCloneIssue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\RatesAndFeesCloneIssue.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("RatesAndFeesCloneIssue");
                        string id = loginHelper.getIssueID("Rates And Fees Clone Issue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\RatesAndFeesCloneIssue.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Rates And Fees Clone Issue"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Rates And Fees Clone Issue");
                //     executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("RatesAndFeesCloneIssue");
                executionLog.WriteInExcel("Rates And Fees Clone Issue", Status, JIRA, "Office Master Data");
            }
        }
    }
}
