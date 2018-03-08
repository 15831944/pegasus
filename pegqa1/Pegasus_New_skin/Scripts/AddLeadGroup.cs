using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;
namespace Pegasus_New_skin.Scripts
{
    [TestClass]
    public class AddLeadGroup : DriverTestCase
    {
        [TestMethod]
        public void AddLeadGroupTester()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var addLeadGroupHelper = new AddLeadGroupHelper(GetWebDriver());

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
                executionLog.Log("AddLeadGroup", "Login with valid credential  Username");
                Login(username[0], password[0]);

                executionLog.Log("AddLeadGroup", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");
                VisitOffice("leads");

                executionLog.Log("AddLeadGroup", "Go to test lead");
        //        addLeadGroupHelper.TypeText("SearchCom", "AddGroupTest");
                addLeadGroupHelper.WaitForWorkAround(3000);
                addLeadGroupHelper.ClickElement("Lead");
                addLeadGroupHelper.WaitForWorkAround(5000);

                executionLog.Log("AddLeadGroup", "Add group");
                addLeadGroupHelper.jsClick("AddGroup");
                addLeadGroupHelper.WaitForWorkAround(3000);
                executionLog.Log("AddLeadGroup", "select group");
                addLeadGroupHelper.SelectByText("LeadGroup", "Group lead");

                executionLog.Log("AddLeadGroup", "save");
                addLeadGroupHelper.ClickElement("SaveGroup");
                addLeadGroupHelper.WaitForWorkAround(3000);
                executionLog.Log("AddLeadGroup", "delete");
                addLeadGroupHelper.WaitForWorkAround(3000);
                addLeadGroupHelper.jsClick("Delete");
                addLeadGroupHelper.AcceptAlert();
                addLeadGroupHelper.WaitForWorkAround(3000);

           // }
           // catch
           // {
            //}
            }
    }
}
