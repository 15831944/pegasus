using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
  //  [TestClass]
    public class CropDefaultProcesser : DriverTestCase
    {
        [TestMethod]
        [TestCategory("DeleteData")]
        [TestCategory("TS4")]
        public void cropDefaultProcesser()
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
            var deleteDataHelper = new DeleteDataHelper(GetWebDriver());

            // Variable

            String JIRA = "";
            String Status = "Pass";

            executionLog.Log("EquipmentSortByAdvanceFilterIssue", "Login with valid credential  Username");
            Login(username[0], password[0]);

            executionLog.Log("EquipmentSortByAdvanceFilterIssue", "Verify Page title");
            VerifyTitle("Dashboard");
            Console.WriteLine("Redirected at Dashboard screen.");
            deleteDataHelper.WaitForWorkAround(6000);

            executionLog.Log("EquipmentSortByAdvanceFilterIssue", "Goto Equipments.");
            VisitCorp("masterdata/processor_types");
            deleteDataHelper.WaitForWorkAround(5000);

            deleteDataHelper.CleanCropProcessor();


        }
    }
}
