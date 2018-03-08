using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;
namespace PegasusTests.Scripts
{
    [TestClass]
    public class RatesFeesLogic : DriverTestCase
    {
        //create a template
        void createTemplate(RatesFeesLogicHelper helper, ExecutionLog log)
        {
            log.Log("RatesFeesLogic", "go to rate fee template");
            VisitOffice("rates_fees");
            helper.WaitForWorkAround(1000);
            log.Log("RatesFeesLogic", "Create a new template");
            helper.ClickElement("Create");
            log.Log("RatesFeesLogic", "Input template name");
            helper.TypeText("TemplateName", "YangTest");
            log.Log("RatesFeesLogic", "Choose template processor");
            helper.Select("CreateProcessorType", "3291");
            log.Log("RatesFeesLogic", "Choose template merchant type");
            helper.Select("CreateMerchantType", "test");
            log.Log("RatesFeesLogic", "choose template method");
            helper.Select("CreateMethods", "Ecommerce");
            helper.WaitForWorkAround(1500);
            log.Log("RatesFeesLogic", "set template default values");
            helper.TypeText("VisaQualified", "2");
            helper.TypeText("VisaMidQualified", "3");
            helper.TypeText("VisaNonQualified", "4");
            helper.TypeText("MasterQualified", "5");
            helper.TypeText("MasterMidQualified", "6");
            helper.TypeText("MasterNonQualified", "7");
            log.Log("RatesFeesLogic", "save template");
            helper.ClickElement("Save");
            helper.WaitForWorkAround(2500);
            helper.ClickElement("Save");
            log.Log("RatesFeesLogic", "Template saved");
        }
        //go to rates fees page
        void goToRF(RatesFeesLogicHelper helper, ExecutionLog log)
        {
            log.Log("RatesFeesLogic", "go to client");
            VisitOffice("clients");
            log.Log("RatesFeesLogic", "choose test client");
            helper.TypeText("SearchCompany", "RateFeeLogicTester");
            helper.ClickElement("RateFeeTester");
            helper.WaitForWorkAround(2000);
            log.Log("RatesFeesLogic", "go to rates fee tab");
            helper.ClickElement("RateFeesTab");
            log.Log("RatesFeesLogic", "choose processor type");
            helper.Select("ClientProcessorType", "First Data Omaha");
            log.Log("RatesFeesLogic", "choose merthant type");
            helper.Select("ClientMerchantType", "test");
            helper.WaitForWorkAround(2000);
            log.Log("RatesFeesLogic", "choose method");
            helper.Select("ClientMethods", "Ecommerce");
            helper.WaitForWorkAround(2000);
            log.Log("RatesFeesLogic", "get default value");
            helper.ClickElement("GetDefault");
            helper.AlertOK();
            log.Log("RatesFeesLogic", "default value got");
            helper.WaitForWorkAround(2000);
            
        }
        //check the correctness of default value
        void CheckDefault(RatesFeesLogicHelper helper, ExecutionLog log)
        {
            log.Log("RatesFeesLogic", "start checking");

            string VisaQual = helper.GetTextContent("ClientVisaQua");
            Assert.IsTrue(string.Compare(VisaQual,"2")==0);
            log.Log("RatesFeesLogic", "Visa qua passed");
            string VisaMid = helper.GetTextContent("ClientVisaMid");
            
            Assert.IsTrue(string.Compare(VisaMid, "3") == 0);
            log.Log("RatesFeesLogic", "visa mid pass");
            string VisaNon = helper.GetTextContent("ClientVisaNon");
            Assert.IsTrue(string.Compare(VisaNon, "4") == 0);
            log.Log("RatesFeesLogic", "visa non pass");
            string MasterQual = helper.GetTextContent("ClientMasterQua");
            Assert.IsTrue(string.Compare(MasterQual, "5") == 0);
            log.Log("RatesFeesLogic", "master qua pass");
            string MasterMid = helper.GetTextContent("ClientMasterMid");
            Assert.IsTrue(string.Compare(MasterMid, "6") == 0);
            log.Log("RatesFeesLogic", "master mid pass");
            string MasterNon = helper.GetTextContent("ClientMasterNon");
            Assert.IsTrue(string.Compare(MasterNon, "7") == 0);
            log.Log("RatesFeesLogic", "mater non pass");
        }
        //clean up
        void deleteTemplate(RatesFeesLogicHelper helper, ExecutionLog log)
        {
            log.Log("RatesFeesLogic", "clean: go to rates fee");
            VisitOffice("rates_fees");
            helper.WaitForWorkAround(1000);
            log.Log("RatesFeesLogic", "clean: search template");
            helper.TypeText("SearchTemplate", "YangTest");
            helper.WaitForWorkAround(2000);
            log.Log("RatesFeesLogic", "clean:delete");
            helper.ClickElement("Trash");
            helper.AlertOK();
            helper.WaitForWorkAround(2000);
            log.Log("RatesFeesLogic", "clean:deleted");
        }
        //main test helper method
        void checkRFlogic(RatesFeesLogicHelper helper, ExecutionLog log)
        {
            goToRF(helper,log);
            CheckDefault(helper,log);
            deleteTemplate(helper, log);
        }
        [TestMethod]
        public void RatesFeesVali()
        {
            string[] username1 = null;
            string[] password1 = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username1 = oXMLData.getData("settings/Credentials", "username");
            password1 = oXMLData.getData("settings/Credentials", "password");

            //Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var ratesFeesLogicHelper = new RatesFeesLogicHelper(GetWebDriver());

            var username = "TESTUSER" + GetRandomNumber();
            var email = "Test" + GetRandomNumber() + "@gmail.com";
            String JIRA = "";
            String Status = "Pass";
            //try
            //{
            executionLog.Log("RatesFeesLogic", "Login with valid username and password");
            Login(username1[0], password1[0]);
            ratesFeesLogicHelper.WaitForWorkAround(1000);
            executionLog.Log("RatesFeesLogic", "createTemplate");
            createTemplate(ratesFeesLogicHelper, executionLog);
            executionLog.Log("RatesFeesLogic", "checkLogic");
            checkRFlogic(ratesFeesLogicHelper, executionLog);




            // }
            //     catch (Exception e)
            //    {
            //        Console.WriteLine("ERRROROOR");
            //        executionLog.Log("Error", e.StackTrace);
            //        Status = "Fail";

            //        String counter = executionLog.readLastLine("counter");
            //        String Description = executionLog.GetAllTextFile("RatesFeesLogic");
            //        String Error = executionLog.GetAllTextFile("Error");
            //        if (counter == "")
            //        {
            //            counter = "0";
            //        }
            //        bool result = loginHelper.CheckExstingIssue("RatesFeesLogic");
            //        if (!result)
            //        {
            //            if (Int16.Parse(counter) < 5)
            //            {
            //                executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
            //                loginHelper.CreateIssue("RatesFeesLogic", "Bug", "Medium", "Pricing page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
            //                string id = loginHelper.getIssueID("RatesFeesLogic");
            //                TakeScreenshot("RatesFeesLogic");
            //                string directoryName = loginHelper.GetnewDirectoryName(GetPath());
            //                var location = directoryName + "\\Iframe.png";
            //                loginHelper.AddAttachment(location, id);
            //            }
            //        }
            //        else
            //        {
            //            if (Int16.Parse(counter) < 5)
            //            {
            //                executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
            //                TakeScreenshot("OfficeFieldDictionary");
            //                string id = loginHelper.getIssueID("RatesFeesLogic");
            //                string directoryName = loginHelper.GetnewDirectoryName(GetPath());
            //                var location = directoryName + "\\RatesFeesLogic.png";
            //                loginHelper.AddAttachment(location, id);
            //                loginHelper.AddComment(loginHelper.getIssueID("RatesFeesLogic"), "This issue is still occurring");
            //            }
            //        }
            //        JIRA = loginHelper.getIssueID("RatesFeesLogic");
            //        executionLog.DeleteFile("Error");
            //        throw;

            //    }
            //    finally
            //    {
            //        executionLog.DeleteFile("RatesFeesLogic");
            //        executionLog.WriteInExcel("RatesFeesLogic", Status, JIRA, "RatesFeesLogic");
            //    }
            //}
            //}
            //}      
        }

    }
}
