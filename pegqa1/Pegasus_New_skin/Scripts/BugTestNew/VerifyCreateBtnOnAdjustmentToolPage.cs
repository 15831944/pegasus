using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class VerifyCreateBtnOnAdjustmentToolPage : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin")]
        [TestCategory("Fail")]
        [TestCategory("TS3")]
        [TestCategory("BugTestNew")]
        public void verifyCreateBtnOnAdjustmentToolPage()
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

            var corp_Master_AdjustmentToolHelper = new CorpResidualIncome_Masterdata_AdjustmentToolHelper(GetWebDriver());


            // Variable random
            String JIRA = "";
            String Status = "Pass";

            //try
            //{

                executionLog.Log("VerifyCreateBtnOnAdjustmentToolPage", "Login with valid username and password");
                Login(username[0], password[0]);

                executionLog.Log("VerifyCreateBtnOnAdjustmentToolPage", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("VerifyCreateBtnOnAdjustmentToolPage", "Go Master Adjustment Tool Page");
                VisitCorp("rir/masterrules");
                corp_Master_AdjustmentToolHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCreateBtnOnAdjustmentToolPage", "Click on Create button");
                corp_Master_AdjustmentToolHelper.ClickElement("Create");
                corp_Master_AdjustmentToolHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyCreateBtnOnAdjustmentToolPage", "Verify redirected to Create Adjustment Tool Page");
                corp_Master_AdjustmentToolHelper.VerifyText("AdjustmentToolTitle", "Adjustments Tool");
                Console.WriteLine("Redirected to Create Adjustment Tool Page");
                

            //}
            //catch (Exception e)
            //{

            //}
            
        }
    }
}
