using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class ImportNotesFromExcelToLeads : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("CorpProcessor")]
        [TestCategory("New")]

        public void importNotesFromExcelToLeads()
        {
            string username1 = "newtesteroffice";
            string password1 = "mynewpegasus";

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var corpMasterData_ProcessorsHelper = new CorpMasterData_ProcessorsHelper(GetWebDriver());
            var all_ProcessorsHelper = new All_ProcessorsHelper(GetWebDriver());


                executionLog.Log("CorpMasterDataProcessorUrlChange", "Login with valid username and password");
                Login(username1, password1);
                all_ProcessorsHelper.WaitForWorkAround(2000);

                executionLog.Log("CorpMasterDataProcessorUrlChange", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("CorpMasterDataProcessorUrlChange", "Go To All leads Page");
                GetWebDriver().Navigate().GoToUrl("https://www.mypegasuscrm.com/select_merchant_solutions/select_merchant_solutions/leads");
                corpMasterData_ProcessorsHelper.WaitForWorkAround(2000);
               
                executionLog.Log("CorpMasterDataProcessorUrlChange", "Read Data from Excel File and Enter values");
                all_ProcessorsHelper.ReadClient_Excel();
                corpMasterData_ProcessorsHelper.WaitForWorkAround(2000);
                Console.WriteLine("All Processors created");
           
        }
    }
}
  