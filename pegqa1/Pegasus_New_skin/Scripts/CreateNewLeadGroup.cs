using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;
namespace Pegasus_New_skin.Scripts
{
    [TestClass]
    public class CreateNewLeadGroup : DriverTestCase
    {
        [TestMethod]
        public void CreateNewLeadGroupTester()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var createNewLeadGroupHelper = new CreateNewLeadGroupHelper(GetWebDriver());

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Variable
            var FName = "Test" + RandomNumber(99, 99999);
            var LName = "Test" + RandomNumber(99, 99999);
            var CDBA = "New" + RandomNumber(99, 99999);
            String JIRA = "";
            String Status = "Pass";

            // try
            // {
            executionLog.Log("CreateNewLeadGroup", "Login with valid credential  Username");
            Login(username[0], password[0]);

            executionLog.Log("CreateNewLeadGroup", "Verify Page title");
            VerifyTitle("Dashboard");
            executionLog.Log("CreateNewLeadGroup", "go to manage lead group");
            Console.WriteLine("Redirected at Dashboard screen.");
            VisitOffice("leads/manage_groups");

            executionLog.Log("CreateNewLeadGroup", "craete group");
            createNewLeadGroupHelper.ClickElement("Create");
            createNewLeadGroupHelper.WaitForWorkAround(3000);
            executionLog.Log("CreateNewLeadGroup", "Name");
            createNewLeadGroupHelper.TypeText("Name", "CreateTester");

            executionLog.Log("CreateNewLeadGroup", "save");
            createNewLeadGroupHelper.ClickElement("SaveGroup");
            createNewLeadGroupHelper.WaitForWorkAround(3000);
            executionLog.Log("CreateNewLeadGroup", "delete");
            createNewLeadGroupHelper.WaitForWorkAround(3000);
            createNewLeadGroupHelper.ClickElement("Delete");
            createNewLeadGroupHelper.AcceptAlert();
            createNewLeadGroupHelper.WaitForWorkAround(3000);

            // }
            // catch
            // {
            //}
        }
    }
}
