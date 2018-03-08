using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class EditAmexRatesCorpVerifyInOffice : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("Fail")]
        [TestCategory("TS1")]
        [TestCategory("BugTest")]
        public void editAmexRatesCorpVerifyInOffice()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_corp");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var corp_MasterData_AmexRateHelper = new CorpMasterData_AmexRateHelper(GetWebDriver());

            // Variable
            var Num = RandomNumber(1000, 9999).ToString();
            var Nam = "1" + RandomNumber(1000, 9999);
            var name = "1" + RandomNumber(1000, 9999);
            var NewNum = RandomNumber(1000, 9999).ToString();
            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("EditAmexRatesCorpVerifyInOffice", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("EditAmexRatesCorpVerifyInOffice", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("EditAmexRatesCorpVerifyInOffice", "Redirect to create Amex Rates page");
                VisitCorp("masterdata/manage_amex_rates");
                corp_MasterData_AmexRateHelper.WaitForWorkAround(5000);

                executionLog.Log("EditAmexRatesCorpVerifyInOffice", "Verify title Manage Master Amex Rates");
                VerifyTitle("Manage Master Amex Rates");

                executionLog.Log("EditAmexRatesCorpVerifyInOffice", "Enter Processor name");
                corp_MasterData_AmexRateHelper.TypeText("MCCCode", Num);

                executionLog.Log("EditAmexRatesCorpVerifyInOffice", "Enter ProcessorCode");
                corp_MasterData_AmexRateHelper.TypeText("AmexRate", name);

                executionLog.Log("EditAmexRatesCorpVerifyInOffice", "Enter Amex Per Rate");
                corp_MasterData_AmexRateHelper.TypeText("AmexPerItem", Nam);

                executionLog.Log("EditAmexRatesCorpVerifyInOffice", "Click On Save Btn");
                corp_MasterData_AmexRateHelper.ClickElement("Save1");
                corp_MasterData_AmexRateHelper.WaitForWorkAround(6000);

                executionLog.Log("EditAmexRatesCorpVerifyInOffice", "Verify Text The Amex Rates is successfully created!!");
                corp_MasterData_AmexRateHelper.WaitForText("The Amex Rates is successfully created!!", 5);

                executionLog.Log("EditAmexRatesCorpVerifyInOffice", "Search with  MCC Codes");
                corp_MasterData_AmexRateHelper.TypeText("SecrhMCCCodes", Num);
                corp_MasterData_AmexRateHelper.WaitForWorkAround(4000);

                executionLog.Log("EditAmexRatesCorpVerifyInOffice", "Click on Edit Icon");
                corp_MasterData_AmexRateHelper.ClickElement("Edit");
                corp_MasterData_AmexRateHelper.WaitForWorkAround(5000);

                executionLog.Log("EditAmexRatesCorpVerifyInOffice", "Enter ProcessorCode");
                corp_MasterData_AmexRateHelper.TypeText("AmexRate", NewNum);

                executionLog.Log("EditAmexRatesCorpVerifyInOffice", "Enter Amex Per Rate");
                corp_MasterData_AmexRateHelper.TypeText("AmexPerItem", NewNum);

                executionLog.Log("EditAmexRatesCorpVerifyInOffice", "Click on Save Edited value.");
                corp_MasterData_AmexRateHelper.ClickElement("Save1");
                corp_MasterData_AmexRateHelper.WaitForWorkAround(5000);

                executionLog.Log("EditAmexRatesCorpVerifyInOffice", "Verify The Amex Rates is successfully updated!!");
                corp_MasterData_AmexRateHelper.WaitForText("The Amex Rates is successfully updated!!", 5);

                executionLog.Log("EditAmexRatesCorpVerifyInOffice", "Click To Push To Office Frm Corp");
                corp_MasterData_AmexRateHelper.ClickElement("PushOffice");

                executionLog.Log("EditAmexRatesCorpVerifyInOffice", "Accept alert message.");
                corp_MasterData_AmexRateHelper.AcceptAlert();

                executionLog.Log("EditAmexRatesCorpVerifyInOffice", "Wait for success message.");
                corp_MasterData_AmexRateHelper.WaitForText("Amex Rates successfully pushed to offices.", 40);

                executionLog.Log("EditAmexRatesCorpVerifyInOffice", "Logout from application.");
                VisitCorp("logout");
                corp_MasterData_AmexRateHelper.WaitForWorkAround(3000);

                executionLog.Log("EditAmexRatesCorpVerifyInOffice", "Login with valid username and password");
                Login(username[0], password[0]);
                corp_MasterData_AmexRateHelper.WaitForWorkAround(3000);

                if (GetWebDriver().Title == "Login")
                {
                    Login(username[0], password[0]);
                }

                executionLog.Log("EditAmexRatesCorpVerifyInOffice", "Redirect Amex Rate");
                VisitOffice("amex_rates");

                executionLog.Log("EditAmexRatesCorpVerifyInOffice", "Verify page title.");
                VerifyTitle("Master Amex Rates");

                executionLog.Log("EditAmexRatesCorpVerifyInOffice", "Search with  MCC Codes");
                corp_MasterData_AmexRateHelper.TypeText("SecrhMCCCodes", Num);

                executionLog.Log("EditAmexRatesCorpVerifyInOffice", "Verify Page text");
                corp_MasterData_AmexRateHelper.VerifyPageText(NewNum);

                executionLog.Log("EditAmexRatesCorpVerifyInOffice", "Logout from the application.");
                VisitOffice("logout");
                corp_MasterData_AmexRateHelper.WaitForWorkAround(3000);

                executionLog.Log("EditAmexRatesCorpVerifyInOffice", "Login with valid username and password");
                Login(username[0], password[0]);

                executionLog.Log("EditAmexRatesCorpVerifyInOffice", "Redirect to create Amex Rates page");
                VisitCorp("masterdata/manage_amex_rates");

                executionLog.Log("EditAmexRatesCorpVerifyInOffice", "Verify title Manage Master Amex Rates");
                VerifyTitle("Manage Master Amex Rates");

                executionLog.Log("EditAmexRatesCorpVerifyInOffice", "Search with  MCC Codes");
                corp_MasterData_AmexRateHelper.TypeText("SecrhMCCCodes", Num);
                corp_MasterData_AmexRateHelper.WaitForWorkAround(2000);

                executionLog.Log("EditAmexRatesCorpVerifyInOffice", "Click on delete icon.");
                corp_MasterData_AmexRateHelper.ClickElement("Delete");

                executionLog.Log("EditAmexRatesCorpVerifyInOffice", "Accept alert message.");
                corp_MasterData_AmexRateHelper.AcceptAlert();

                executionLog.Log("EditAmexRatesCorpVerifyInOffice", "Accept alert message.");
                corp_MasterData_AmexRateHelper.WaitForText("The Amex Rates is successfully deleted!!", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("EditAmexRatesCorpVerifyInOffice");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Edit Amex Rates Corp Verify In Office");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Edit Amex Rates Corp Verify In Office", "Bug", "Medium", "Amex Rates", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Edit Amex Rates Corp Verify In Office");
                        TakeScreenshot("EditAmexRatesCorpVerifyInOffice");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditAmexRatesCorpVerifyInOffice.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("EditAmexRatesCorpVerifyInOffice");
                        string id = loginHelper.getIssueID("Edit Amex Rates Corp Verify In Office");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditAmexRatesCorpVerifyInOffice.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Edit Amex Rates Corp Verify In Office"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Edit Amex Rates Corp Verify In Office");
                //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("EditAmexRatesCorpVerifyInOffice");
                executionLog.WriteInExcel("Edit Amex Rates Corp Verify In Office", Status, JIRA, "Corp MasterData");
            }
        }
    }
}