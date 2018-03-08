using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class CreateCorpProcessorFromExcelSheet : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("CorpProcessor")]
        [TestCategory("New")]

        public void createCorpProcessorFromExcelSheet()
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
            var corpMasterData_ProcessorsHelper = new CorpMasterData_ProcessorsHelper(GetWebDriver());

            
                executionLog.Log("CorpMasterDataProcessorUrlChange", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("CorpMasterDataProcessorUrlChange", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("CorpMasterDataProcessorUrlChange", "Go To Corp Master Data >> Create Processor");
                VisitCorp("masterdata/processor_types");
                corpMasterData_ProcessorsHelper.WaitForWorkAround(3000);
               
                executionLog.Log("CorpMasterDataProcessorUrlChange", "Read Data from Excel File and Enter values");
                corpMasterData_ProcessorsHelper.ReadClient_Excel();
                corpMasterData_ProcessorsHelper.WaitForWorkAround(2000);
                Console.WriteLine("All Processors created");
           
        }
    }
}
  