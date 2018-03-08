using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class AdminEquipmentDownloadIdsURLChange : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Url")]
        [TestCategory("TS3")]
        [TestCategory("ChangeUrl")]
        public void adminEquipmentDownloadIdsURLChange()
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
            var equipment_DownloadIDHelper = new Equipment_DownloadIDHelper(GetWebDriver());


            // Variable
            var FirstName = "Test" + GetRandomNumber();
            var LastName = "Tester" + GetRandomNumber();
            var Number = "12345678" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("AdminEquipmentDownloadIdsURLChange", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("AdminEquipmentDownloadIdsURLChange", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("AdminEquipmentDownloadIdsURLChange", "Goto User Equipments   >> Download Ids  ");
                VisitOffice("download_ids");
                equipment_DownloadIDHelper.WaitForWorkAround(1000);

                executionLog.Log("AdminEquipmentDownloadIdsURLChange", "Click On Download Ids ");
                equipment_DownloadIDHelper.ClickElement("OpenDownloadId");
                equipment_DownloadIDHelper.WaitForWorkAround(2000);

                executionLog.Log("AdminEquipmentDownloadIdsURLChange", "Change the url with the url number of another office");
                VisitOffice("manage_download_ids/96");
                equipment_DownloadIDHelper.WaitForWorkAround(1000);

                executionLog.Log("AdminEquipmentDownloadIdsURLChange", "Verify Validation");
                equipment_DownloadIDHelper.WaitForText("The Equipment Type Download Id is does not exists.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("AdminEquipmentDownloadIdsURLChange");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Admin Equipment Download Ids URL Change");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Admin Equipment Download Ids URL Change", "Bug", "Medium", "Office Equipment", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Admin Equipment Download Ids URL Change");
                        TakeScreenshot("AdminEquipmentDownloadIdsURLChange");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AdminEquipmentDownloadIdsURLChange.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("AdminEquipmentDownloadIdsURLChange");
                        string id = loginHelper.getIssueID("Admin Equipment Download Ids URL Change");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AdminEquipmentDownloadIdsURLChange.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Admin Equipment Download Ids URL Change"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Admin Equipment Download Ids URL Change");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("AdminEquipmentDownloadIdsURLChange");
                executionLog.WriteInExcel("Admin Equipment Download Ids URL Change", Status, JIRA, "Office Equipment");
            }
        }
    }
}
