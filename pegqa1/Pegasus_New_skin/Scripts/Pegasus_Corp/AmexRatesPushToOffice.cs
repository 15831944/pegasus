using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class AmexRatesPushToOffice : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Corp")]
        [TestCategory("Fail")]
        [TestCategory("TS7")]
        [TestCategory("Pegasus_Corp")]
        public void amexRatesPushToOffice()
        {

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var corpMasterData_AmexRateHelper = new CorpMasterData_AmexRateHelper(GetWebDriver());

            string[] username = null;
            string[] username1 = null;
            string[] password = null;
            username = oXMLData.getData("settings/Credentials", "username_corp");
            username1 = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Variable
            var Num = RandomNumber(1, 9999).ToString();
            var Nam = "3" + GetRandomNumber();
            var name = "6" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";


            try
            {
            executionLog.Log("AmexRatesPushToOffice", "Login with valid credential");
            Login(username[0], password[0]);

            executionLog.Log("AmexRatesPushToOffice", "Verify Page title");
            VerifyTitle("Dashboard");

            executionLog.Log("AmexRatesPushToOffice", "Go to amex rate page");
            VisitCorp("masterdata/amex_rates");
            corpMasterData_AmexRateHelper.WaitForWorkAround(3000);

            executionLog.Log("AmexRatesPushToOffice", "Verify title");
            VerifyTitle("Master Amex Rates");

            executionLog.Log("AmexRatesPushToOffice", "Click On Create");
            corpMasterData_AmexRateHelper.ClickElement("Create");
            corpMasterData_AmexRateHelper.WaitForWorkAround(3000);

            executionLog.Log("AmexRatesPushToOffice", "Verify title");
            VerifyTitle("Manage Master Amex Rates");

            executionLog.Log("AmexRatesPushToOffice", "Enter Processor name");
            corpMasterData_AmexRateHelper.TypeText("MCCCode", Num);

            executionLog.Log("AmexRatesPushToOffice", "Enter ProcessorCode");
            corpMasterData_AmexRateHelper.TypeText("AmexRate", name);

            executionLog.Log("AmexRatesPushToOffice", "Amex Per Item");
            corpMasterData_AmexRateHelper.TypeText("AmexPerItem", Nam);

            executionLog.Log("AmexRatesPushToOffice", "Click On Save Btn");
            corpMasterData_AmexRateHelper.ClickElement("Save");

            executionLog.Log("AmexRatesPushToOffice", "Wait for success message");
            corpMasterData_AmexRateHelper.WaitForText("The Amex Rates is successfully created!!", 10);

            executionLog.Log("AmexRatesPushToOffice", "Click On Push Office");
            corpMasterData_AmexRateHelper.ClickElement("PushOffice");

            executionLog.Log("AmexRatesPushToOffice", "Accept Alert To Confirm Action");
            try
            {
                corpMasterData_AmexRateHelper.AcceptAlert();
            }
            catch (OpenQA.Selenium.WebDriverException)
            { }

            //corpMasterData_AmexRateHelper.WaitForWorkAround(20000);

            executionLog.Log("AmexRatesPushToOffice", "Wait for success message");
            corpMasterData_AmexRateHelper.WaitForText("Amex Rates successfully pushed to offices.", 10);

            executionLog.Log("AmexRatesPushToOffice", "Logout button");
            VisitCorp("logout");

            executionLog.Log("AmexRatesPushToOffice", "Verify title");
            VerifyTitle("Login");

            Login(username1[0], password[0]);
            if (GetWebDriver().Title == "Login")

            {

                Login(username1[0], password[0]);
            }

            executionLog.Log("AmexRatesPushToOffice", "Redirect to Amex Rate Office >> Admin ");
            VisitOffice("amex_rates");
            corpMasterData_AmexRateHelper.WaitForWorkAround(3000);

            executionLog.Log("AmexRatesPushToOffice", "Verify title");
            VerifyTitle("Master Amex Rates");

            executionLog.Log("AmexRatesPushToOffice", "Search with  MCC Codes");
            corpMasterData_AmexRateHelper.TypeText("SecrhMCCCodes", Num);
            corpMasterData_AmexRateHelper.WaitForWorkAround(2000);

            executionLog.Log("AmexRatesPushToOffice", "Verify ");
            corpMasterData_AmexRateHelper.VerifyPageText(Num);

            executionLog.Log("AmexRatesPushToOffice", "Logout button");
            VisitOffice("logout");

            executionLog.Log("AmexRatesPushToOffice", "Login with valid credential");
            Login(username[0], password[0]);

            executionLog.Log("AmexRatesPushToOffice", "Verify Page title");
            VerifyTitle("Dashboard");

            executionLog.Log("AmexRatesPushToOffice", "Go to amex rate page");
            VisitCorp("masterdata/amex_rates");
            corpMasterData_AmexRateHelper.WaitForWorkAround(3000);

            executionLog.Log("AmexRatesPushToOffice", "Enter Name to search");
            corpMasterData_AmexRateHelper.TypeText("EnterMccCode", Num);
            corpMasterData_AmexRateHelper.WaitForWorkAround(2000);

            executionLog.Log("AmexRatesPushToOffice", "Click Delete btn  ");
            corpMasterData_AmexRateHelper.ClickElement("Delete");

            executionLog.Log("AmexRatesPushToOffice", "Accept alert message. ");
            corpMasterData_AmexRateHelper.AcceptAlert();

            executionLog.Log("AmexRatesPushToOffice", "Wait for delete message. ");
            corpMasterData_AmexRateHelper.WaitForText("The Amex Rates is successfully deleted!!", 10);
            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("AmexRatesPushToOffice");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Amex Rates Push To Office");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Amex Rates Push To Office", "Bug", "Medium", "Amex rate page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Amex Rates Push To Office");
                        TakeScreenshot("AmexRatesPushToOffice");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AmexRatesPushToOffice.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("AmexRatesPushToOffice");
                        string id = loginHelper.getIssueID("Amex Rates Push To Office");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AmexRatesPushToOffice.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Amex Rates Push To Office"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Amex Rates Push To Office");
                executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("AmexRatesPushToOffice");
                executionLog.WriteInExcel("Amex Rates Push To Office", Status, JIRA, "Corp Master Data");
            }
        }
    }
} 
