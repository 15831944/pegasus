using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class AdminEquipmentEquipmentURLChange : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Url")]
        [TestCategory("TS3")]
        [TestCategory("ChangeUrl")]
        public void adminEquipmentEquipmentURLChange()
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
            var eqiupment_EquipmentHelper = new Eqiupment_EquipmentHelper(GetWebDriver());


            // Variable
            var FirstName = "Test" + GetRandomNumber();
            var LastName = "Tester" + GetRandomNumber();
            var Number = "12345678" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("AdminEquipmentEquipmentURLChange", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("AdminEquipmentEquipmentURLChange", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("AdminEquipmentEquipmentURLChange", "Goto User Admin Product >> Product  ");
                VisitOffice("equipment");

                executionLog.Log("AdminEquipmentEquipmentURLChange", "Click On any Product >> Product");
                eqiupment_EquipmentHelper.ClickElement("ClickOneQUIP");
                eqiupment_EquipmentHelper.WaitForWorkAround(2000);

                executionLog.Log("AdminEquipmentEquipmentURLChange", "Change the url with the url number of another office");
                VisitOffice("equipment/view/616");
                eqiupment_EquipmentHelper.WaitForWorkAround(5000);

                executionLog.Log("AdminEquipmentEquipmentURLChange", "Verify Validation");
                eqiupment_EquipmentHelper.VerifyPageText("oops something went wrong");
                eqiupment_EquipmentHelper.WaitForWorkAround(2000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("AdminEquipmentEquipmentURLChange");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("AdminEquipment Equipment URL Change");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("AdminEquipment Equipment URL Change", "Bug", "Medium", "Equipment Page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("AdminEquipment Equipment URL Change");
                        TakeScreenshot("AdminEquipmentEquipmentURLChange");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AdminEquipmentEquipmentURLChange.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("AdminEquipmentEquipmentURLChange");
                        string id = loginHelper.getIssueID("AdminEquipment Equipment URL Change");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AdminEquipmentEquipmentURLChange.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("AdminEquipment Equipment URL Change"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("AdminEquipment Equipment URL Change");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("AdminEquipmentEquipmentURLChange");
                executionLog.WriteInExcel("AdminEquipment Equipment URL Change", Status, JIRA, "Office Equipment");
            }
        }
    }
}