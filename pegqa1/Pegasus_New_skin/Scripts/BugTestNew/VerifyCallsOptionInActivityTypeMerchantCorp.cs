using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class VerifyCallsOptionInActivityTypeMerchantCorp : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin")]
        [TestCategory("Fail")]
        [TestCategory("TS3")]
        [TestCategory("BugTestNew")]
        public void verifyCallsOptionInActivityTypeMerchantCorp()
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

            var corp_MerchantHelper = new Corp_MerchantHelper(GetWebDriver());


            // Variable random
            String JIRA = "";
            String Status = "Pass";

            //try
            //{

                executionLog.Log("VerifyCallsOptionInActivityTypeMerchantCorp", "Login with valid username and password");
                Login(username[0], password[0]);

                executionLog.Log("VerifyCallsOptionInActivityTypeMerchantCorp", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("VerifyCallsOptionInActivityTypeMerchantCorp", "Go Merchants page");
                VisitCorp("merchants");
                corp_MerchantHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyCreateBtnOnAdjustmentToolPage", "Click on a merchant");
                corp_MerchantHelper.ClickElement("ClickOnMerchant");
                corp_MerchantHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyCreateBtnOnAdjustmentToolPage", "Click on Activity Type drop down");
                corp_MerchantHelper.ClickElement("SelectActivityType");

                executionLog.Log("VerifyCreateBtnOnAdjustmentToolPage", "Verify 'Calls' option appearing");
                corp_MerchantHelper.IsElementVisible("//option[@value='Calls']");
                Console.WriteLine("'Calls' option is present");


            //}
            //catch (Exception e)
            //{

            //}
            
        }
    }
}
