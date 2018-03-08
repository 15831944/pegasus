using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class AdminMasterDatRateAndFeesURLChange : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Url")]
        [TestCategory("TS3")]
        [TestCategory("ChangeUrl")]
        public void adminMasterDatRateAndFeesURLChange()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var masterData_RateAndFeesHelper = new MasterData_RateAndFeesHelper(GetWebDriver());


            // Variable
            var FirstName = "Test" + GetRandomNumber();
            var LastName = "Tester" + GetRandomNumber();
            var Number = "12345678" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            //try
            //{
                executionLog.Log("AdminMasterDatRateAndFeesURLChange", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("AdminMasterDatRateAndFeesURLChange", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("AdminMasterDatRateAndFeesURLChange", "Goto Master Data >> Rates and Fee");
                VisitOffice("rates_fees");

                executionLog.Log("AdminMasterDatRateAndFeesURLChange", "Click On Rate And Fees");
                masterData_RateAndFeesHelper.ClickElement("OpenRateAndFees");
                masterData_RateAndFeesHelper.WaitForWorkAround(1000);

                executionLog.Log("AdminMasterDatRateAndFeesURLChange", "Change the url with the url number of another office");
                VisitOffice("manage_rates_fees/257");
                masterData_RateAndFeesHelper.WaitForWorkAround(1000);

                executionLog.Log("AdminMasterDatRateAndFeesURLChange", "Verify Validation");
                masterData_RateAndFeesHelper.WaitForText("The Master rate does not exists.", 10);

            //}
            //catch (Exception e)
            //{
            //    executionLog.Log("Error", e.StackTrace);
            //    Status = "Fail";

            //    String counter = executionLog.readLastLine("counter");
            //    String Description = executionLog.GetAllTextFile("AdminMasterDatRateAndFeesURLChange");
            //    String Error = executionLog.GetAllTextFile("Error");
            //    if (counter == "")
            //    {
            //        counter = "0";
            //    }
            //    bool result = loginHelper.CheckExstingIssue("AdminMaster Data Rate And Fees URL Change");
            //    if (!result)
            //    {
            //        if (Int16.Parse(counter) < 9)
            //        {
            //            executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
            //            loginHelper.CreateIssue("AdminMaster Data Rate And Fees URL Change", "Bug", "Medium", "Rates And Fees Page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
            //            string id = loginHelper.getIssueID("AdminMaster Data Rate And Fees URL Change");
            //            TakeScreenshot("AdminMasterDatRateAndFeesURLChange");
            //            string directoryName = loginHelper.GetnewDirectoryName(GetPath());
            //            var location = directoryName + "\\AdminMasterDatRateAndFeesURLChange.png";
            //            loginHelper.AddAttachment(location, id);
            //        }
            //    }
            //    else
            //    {
            //        if (Int16.Parse(counter) < 9)
            //        {
            //            executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
            //            TakeScreenshot("AdminMasterDatRateAndFeesURLChange");
            //            string id = loginHelper.getIssueID("AdminMaster Data Rate And Fees URL Change");
            //            string directoryName = loginHelper.GetnewDirectoryName(GetPath());
            //            var location = directoryName + "\\AdminMasterDatRateAndFeesURLChange.png";
            //            loginHelper.AddAttachment(location, id);
            //            loginHelper.AddComment(loginHelper.getIssueID("AdminMaster Data Rate And Fees URL Change"), "This issue is still occurring");
            //        }
            //    }
            //    JIRA = loginHelper.getIssueID("AdminMaster Data Rate And Fees URL Change");
            //    executionLog.DeleteFile("Error");
            //    throw;

            //}
            //finally
            //{
            //    executionLog.DeleteFile("AdminMasterDatRateAndFeesURLChange");
            //    executionLog.WriteInExcel("AdminMaster Data Rate And Fees URL Change", Status, JIRA, "Master Data");
            //}
        }
    }
}
