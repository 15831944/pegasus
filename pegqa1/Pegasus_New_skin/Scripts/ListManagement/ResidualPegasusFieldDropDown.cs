using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class ResidualPegasusFieldDropDown : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Url")]
        [TestCategory("TS3")]
        [TestCategory("ChangeUrl")]
        public void residualPegasusFieldDropDown()
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
            var corp_ResidualIncome_Payout_RepostHelper = new CorpResidualIncome_PayoutsHelper(GetWebDriver());


            // Variable
            var FirstName = "Test" + GetRandomNumber();
            var LastName = "Tester" + GetRandomNumber();
            var Number = "12345678" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            //try
            //{
                executionLog.Log("ResidualPegasusFieldDropDown", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("ResidualPegasusFieldDropDown", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("ResidualPegasusFieldDropDown", "Go To Resisual Income");
                VisitCorp("rir/imports");

                executionLog.Log("ResidualPegasusFieldDropDown", "Click on Import New Button");
                corp_ResidualIncome_Payout_RepostHelper.ClickElement("ImportNew");
                corp_ResidualIncome_Payout_RepostHelper.WaitForWorkAround(1000);

                executionLog.Log("ResidualPegasusFieldDropDown", "Click On Processor Drop Down");
                corp_ResidualIncome_Payout_RepostHelper.SelectDropDown("//select[@id='ImportIndexProcessor']", "ACE");
                corp_ResidualIncome_Payout_RepostHelper.WaitForWorkAround(3000);

                executionLog.Log("ResidualPegasusFieldDropDown", "Select File Date");
                corp_ResidualIncome_Payout_RepostHelper.ClickElement("ImportFileDate");
              //corp_ResidualIncome_Payout_RepostHelper.ClickViaJavaScript("//div[@class='datepicker datepicker-dropdown dropdown-menu datepicker-orient-left datepicker-orient-bottom']/div[1]/table/tbody/tr[1]/td[1]");
                corp_ResidualIncome_Payout_RepostHelper.ClickViaEnter("//div[@class='datepicker datepicker-dropdown dropdown-menu datepicker-orient-left datepicker-orient-bottom']/div[1]/table/tbody/tr[1]/td[1]");
                corp_ResidualIncome_Payout_RepostHelper.WaitForWorkAround(3000);







                //executionLog.Log("ResidualPegasusFieldDropDown", "Verify Validation");
                //corp_ResidualIncome_Payout_RepostHelper.WaitForText("No Payouts.", 10);

            //}
            //catch (Exception e)
            //{
            //    executionLog.Log("Error", e.StackTrace);
            //    Status = "Fail";

            //    String counter = executionLog.readLastLine("counter");
            //    String Description = executionLog.GetAllTextFile("ResidualPegasusFieldDropDown");
            //    String Error = executionLog.GetAllTextFile("Error");
            //    if (counter == "")
            //    {
            //        counter = "0";
            //    }
            //    bool result = loginHelper.CheckExstingIssue("Residual Pegasus Field Drop Down");
            //    if (!result)
            //    {
            //        if (Int16.Parse(counter) < 9)
            //        {
            //            executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
            //            loginHelper.CreateIssue("Residual Pegasus Field Drop Down", "Bug", "Medium", "Corp Residual income page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
            //            string id = loginHelper.getIssueID("Residual Pegasus Field Drop Down");
            //            TakeScreenshot("ResidualPegasusFieldDropDown");
            //            string directoryName = loginHelper.GetnewDirectoryName(GetPath());
            //            var location = directoryName + "\\ResidualPegasusFieldDropDown.png";
            //            loginHelper.AddAttachment(location, id);
            //        }
            //    }
            //    else
            //    {
            //        if (Int16.Parse(counter) < 9)
            //        {
            //            executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
            //            TakeScreenshot("ResidualPegasusFieldDropDown");
            //            string id = loginHelper.getIssueID("Residual Pegasus Field Drop Down");
            //            string directoryName = loginHelper.GetnewDirectoryName(GetPath());
            //            var location = directoryName + "\\ResidualPegasusFieldDropDown.png";
            //            loginHelper.AddAttachment(location, id);
            //            loginHelper.AddComment(loginHelper.getIssueID("Residual Pegasus Field Drop Down"), "This issue is still occurring");
            //        }
            //    }
            //    JIRA = loginHelper.getIssueID("Residual Pegasus Field Drop Down");
            ////    executionLog.DeleteFile("Error");
            //    throw;

            //}
            //finally
            //{
            //    executionLog.DeleteFile("ResidualPegasusFieldDropDown");
            //    executionLog.WriteInExcel("Residual Pegasus Field Drop Down", Status, JIRA, "Corp Residual Income");
            //}
        }
    }
}
