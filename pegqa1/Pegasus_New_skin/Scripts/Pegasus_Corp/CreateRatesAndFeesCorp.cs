using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;
using System;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class CreateRatesAndFeesCorp : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Corp")]
        [TestCategory("TS7")]
        [TestCategory("Pegasus_Corp")]
        public void createRatesAndFeesCorp()
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
            var name = "TEST COMPANY" + GetRandomNumber();
            var TemplateName = "Test" + GetRandomNumber();
            var LastName = "Tester" + GetRandomNumber();
            var Number = "12345678" + GetRandomNumber();

            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("CreateRatesAndFeesCorp", "Login with valid credential  Username");
                Login(username[0], password[0]);

                executionLog.Log("CreateRatesAndFeesCorp", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("CreateRatesAndFeesCorp", "Redirect To URL");
                VisitCorp("masterdata/manage_rates_fees");
                corpMasterdata_RatesAndFeesHelper.WaitForWorkAround(3000);

                executionLog.Log("CreateRatesAndFeesCorp", "Verify Page title");
                VerifyTitle("Manage Master Rates & Fees");

                executionLog.Log("CreateRatesAndFeesCorp", "Enter Pricing template name");
                corpMasterdata_RatesAndFeesHelper.TypeText("PricingTemplateName", TemplateName);

                executionLog.Log("CreateRatesAndFeesCorp", "Select processor type");
                corpMasterdata_RatesAndFeesHelper.SelectByText("ProcessorType", "First Data Omaha");
                corpMasterdata_RatesAndFeesHelper.WaitForWorkAround(5000);

                executionLog.Log("CreateRatesAndFeesCorp", "Select Merchant Type");
                corpMasterdata_RatesAndFeesHelper.SelectByText("SelectMerchanType", "Ecommerce");
                //corpMasterdata_RatesAndFeesHelper.WaitForWorkAround(2000);

                executionLog.Log("CreateRatesAndFeesCorp", "Method of accepting card");
                corpMasterdata_RatesAndFeesHelper.SelectByText("MethodOfAcceptingCards", "Ecommerce");
                //corpMasterdata_RatesAndFeesHelper.WaitForWorkAround(2000);

                executionLog.Log("CreateRatesAndFeesCorp", "Select Discount Frequency ");
                corpMasterdata_RatesAndFeesHelper.SelectByText("DiscountFrequency", "Monthly");
                //corpMasterdata_RatesAndFeesHelper.WaitForWorkAround(2000);

                executionLog.Log("CreateRatesAndFeesCorp", "Method of pricing plan");
                // corpMasterdata_RatesAndFeesHelper.SelectByText("PricePlan", "Other");
                //corpMasterdata_RatesAndFeesHelper.WaitForWorkAround(2000);

                executionLog.Log("CreateRatesAndFeesCorp", "Select Debit Network InterFace Pass Through");
                corpMasterdata_RatesAndFeesHelper.SelectByText("DebitNetworkInterFacePassThrough", "Yes");

                corpMasterdata_RatesAndFeesHelper.WaitForWorkAround(2000);
                executionLog.Log("CreateRatesAndFeesCorp", "Save Edit Rate and Fees");
                corpMasterdata_RatesAndFeesHelper.ClickElement("SaveEdit");
                corpMasterdata_RatesAndFeesHelper.WaitForWorkAround(7000);

                executionLog.Log("CreateRatesAndFeesCorp", "Verfy Text The Rates is successfully created!!");
                corpMasterdata_RatesAndFeesHelper.WaitForText("The Rates is successfully created!!", 10);
                corpMasterdata_RatesAndFeesHelper.WaitForWorkAround(2000);

                executionLog.Log("CreateRatesAndFeesCorp", "Search Rate and fee");
                corpMasterdata_RatesAndFeesHelper.TypeText("SearchTemp", TemplateName);
                corpMasterdata_RatesAndFeesHelper.WaitForWorkAround(2000);

                executionLog.Log("CreateRatesAndFeesCorp", "Click On Delete");
                corpMasterdata_RatesAndFeesHelper.ClickElement("ClickOnDelete");

                executionLog.Log("CreateRatesAndFeesCorp", "Accept Alert");
                corpMasterdata_RatesAndFeesHelper.AcceptAlert();

                executionLog.Log("CreateRatesAndFeesCorp", "Verify Text");
                corpMasterdata_RatesAndFeesHelper.WaitForText("The Rates is successfully deleted!!", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CreateRatesAndFeesCorp");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Create Rates And Fees Corp");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Create Rates And Fees Corp", "Bug", "Medium", "rates And Fees page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Create Rates And Fees Corp");
                        TakeScreenshot("CreateRatesAndFeesCorp");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateRatesAndFeesCorp.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CreateRatesAndFeesCorp");
                        string id = loginHelper.getIssueID("Create Rates And Fees Corp");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateRatesAndFeesCorp.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Create Rates And Fees Corp"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Create Rates And Fees Corp");
               // executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("CreateRatesAndFeesCorp");
                executionLog.WriteInExcel("Create Rates And Fees Corp", Status, JIRA, "Corp Master Data");
            }
        }
    }
}