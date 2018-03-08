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
    public class RatesAndFeesCopyCorp : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Corp")]
        [TestCategory("TS8")]
        [TestCategory("Pegasus_Corp")]
        public void ratesAndFeesCopyCorp()
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
            var name = "TEST COMPANY" + RandomNumber(1, 9999);
            var TemplateName = "Test" + GetRandomNumber();
            var LastName = "Tester" + RandomNumber(1, 9999);
            var Number = "12345678" + RandomNumber(10, 9999);

            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("RatesAndFeesCopyCorp", "Login with valid credential  Username");
                Login(username[0], password[0]);

                executionLog.Log("RatesAndFeesCopyCorp", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("RatesAndFeesCopyCorp", "Redirect To create rate and fees");
                VisitCorp("masterdata/manage_rates_fees");
                corpMasterdata_RatesAndFeesHelper.WaitForWorkAround(3000);

                executionLog.Log("RatesAndFeesCopyCorp", "Verify Page title");
                VerifyTitle("Manage Master Rates & Fees");

                executionLog.Log("RatesAndFeesCopyCorp", "Enter company name");
                corpMasterdata_RatesAndFeesHelper.TypeText("PricingTemplateName", TemplateName);

                executionLog.Log("RatesAndFeesCopyCorp", "Select Processor Type");
                corpMasterdata_RatesAndFeesHelper.SelectByText("ProcessorType", "First Data Omaha");
                corpMasterdata_RatesAndFeesHelper.WaitForWorkAround(4000);

                executionLog.Log("RatesAndFeesCopyCorp", "Select Merchant Type");
                corpMasterdata_RatesAndFeesHelper.SelectByText("SelectMerchanType", "Ecommerce");

                executionLog.Log("RatesAndFeesCopyCorp", "Method of accepting card");
                corpMasterdata_RatesAndFeesHelper.Select("MethodOfAcceptingCards", "Ecommerce");

                executionLog.Log("RatesAndFeesCopyCorp", "Select Discount Frequency ");
                corpMasterdata_RatesAndFeesHelper.Select("DiscountFrequency", "Monthly");

                executionLog.Log("RatesAndFeesCopyCorp", "Select Debit Network InterFace Pass Through");
                corpMasterdata_RatesAndFeesHelper.Select("DebitNetworkInterFacePassThrough", "Yes");
                corpMasterdata_RatesAndFeesHelper.WaitForWorkAround(2000);

                executionLog.Log("RatesAndFeesCopyCorp", "Click Edit Rate and Fees");
                corpMasterdata_RatesAndFeesHelper.ForceClick("SaveEdit");

                executionLog.Log("RatesAndFeesCopyCorp", "Verfy Text The Rates is successfully created!!");
                corpMasterdata_RatesAndFeesHelper.WaitForText("The Rates is successfully created!!", 10);
                corpMasterdata_RatesAndFeesHelper.WaitForWorkAround(2000);

                executionLog.Log("RatesAndFeesCopyCorp", "Search Rate and fee");
                corpMasterdata_RatesAndFeesHelper.TypeText("SearchTemp", TemplateName);
                corpMasterdata_RatesAndFeesHelper.WaitForWorkAround(2000);

                executionLog.Log("RatesAndFeesCopyCorp", "Click On Copy");
                corpMasterdata_RatesAndFeesHelper.ClickElement("ClickOnCopy");
                corpMasterdata_RatesAndFeesHelper.WaitForWorkAround(3000);

                executionLog.Log("RatesAndFeesCopyCorp", "Verify Page title");
                VerifyTitle("Manage Master Rates & Fees");

                executionLog.Log("RatesAndFeesCopyCorp", "Save Edit Rate and Fees");
                corpMasterdata_RatesAndFeesHelper.ForceClick("SaveEdit");

                executionLog.Log("RatesAndFeesCopyCorp", "Verfy Text The Rates is successfully created!!");
                corpMasterdata_RatesAndFeesHelper.WaitForText("The Rates is successfully created!!", 10);
                corpMasterdata_RatesAndFeesHelper.WaitForWorkAround(2000);

                executionLog.Log("RatesAndFeesCopyCorp", "Search Rate and fee");
                corpMasterdata_RatesAndFeesHelper.TypeText("SearchTemp", TemplateName);
                corpMasterdata_RatesAndFeesHelper.WaitForWorkAround(2000);

                executionLog.Log("RatesAndFeesCopyCorp", "Click On Delete");
                corpMasterdata_RatesAndFeesHelper.ClickElement("ClickOnDelete");
                corpMasterdata_RatesAndFeesHelper.AcceptAlert();

                executionLog.Log("RatesAndFeesCopyCorp", "Verify Text");
                corpMasterdata_RatesAndFeesHelper.WaitForText("The Rates is successfully deleted!!", 10);
                corpMasterdata_RatesAndFeesHelper.WaitForWorkAround(2000);

                executionLog.Log("RatesAndFeesCopyCorp", "Search Rate and fee");
                corpMasterdata_RatesAndFeesHelper.TypeText("SearchTemp", "Clone of " + TemplateName);
                corpMasterdata_RatesAndFeesHelper.WaitForWorkAround(2000);

                executionLog.Log("RatesAndFeesCopyCorp", "Click On Delete");
                corpMasterdata_RatesAndFeesHelper.ClickElement("ClickOnDelete");
                corpMasterdata_RatesAndFeesHelper.AcceptAlert();

                executionLog.Log("RatesAndFeesCopyCorp", "Verify Text");
                corpMasterdata_RatesAndFeesHelper.WaitForText("The Rates is successfully deleted!!", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("RatesAndFeesCopyCorp");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Rates And Fees Copy Corp");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Rates And Fees Copy Corp", "Bug", "Medium", "Rate & Fees page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Rates And Fees Copy Corp");
                        TakeScreenshot("RatesAndFeesCopyCorp");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\RatesAndFeesCopyCorp.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("RatesAndFeesCopyCorp");
                        string id = loginHelper.getIssueID("Rates And Fees Copy Corp");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\RatesAndFeesCopyCorp.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Rates And Fees Copy Corp"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Rates And Fees Copy Corp");
                //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("RatesAndFeesCopyCorp");
                executionLog.WriteInExcel("Rates And Fees Copy Corp", Status, JIRA, "Corp Master Data");
            }
        }
    }
}
