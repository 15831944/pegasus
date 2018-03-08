using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class VerifyEmailPhoneAfterSaveOnClientInfo : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS1")]
        [TestCategory("BugTest")]
        public void verifyEmailPhoneAfterSaveOnClientInfo()
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
            var office_ClientsHelper = new Office_ClientsHelper(GetWebDriver());

            // Variable
            var FirstName = "Test" + GetRandomNumber();
            var LastName = "Tester" + GetRandomNumber();
            var DBAName = "DBA@Company" + RandomNumber(1, 999);
            var Number = "12345678" + GetRandomNumber();
            var Title = "QATester" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("VerifyEmailPhoneAfterSaveOnClientInfo", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyEmailPhoneAfterSaveOnClientInfo", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyEmailPhoneAfterSaveOnClientInfo", "Redirect at Create Client");
                VisitOffice("clients/create");

                executionLog.Log("VerifyEmailPhoneAfterSaveOnClientInfo", "Enter Client Dba Name");
                office_ClientsHelper.TypeText("ClientDBAName", DBAName);

                executionLog.Log("VerifyEmailPhoneAfterSaveOnClientInfo", "Select client status");
                office_ClientsHelper.SelectByText("ClientStatus", "New");

                executionLog.Log("VerifyEmailPhoneAfterSaveOnClientInfo", "Select Client Res[onsibility.");
                office_ClientsHelper.SelectByText("ClientResponsibility", "Howard Tang");

                executionLog.Log("VerifyEmailPhoneAfterSaveOnClientInfo", "Click on next button");
                office_ClientsHelper.ClickElement("Next");
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyEmailPhoneAfterSaveOnClientInfo", "Wait for confirmation message.");
                office_ClientsHelper.WaitForText("Client saved successfully. ", 10);

                executionLog.Log("VerifyEmailPhoneAfterSaveOnClientInfo", "Click On Contact Tab Clinet");
                office_ClientsHelper.ClickElement("ContactDetails");
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyEmailPhoneAfterSaveOnClientInfo", "Enter the title of contact");
                office_ClientsHelper.TypeText("ContactTitle", Title);

                executionLog.Log("VerifyEmailPhoneAfterSaveOnClientInfo", "Enter First Name In Contact Tab");
                office_ClientsHelper.TypeText("ContactFirstName", "Test");

                executionLog.Log("VerifyEmailPhoneAfterSaveOnClientInfo", "Enter Last Name");
                office_ClientsHelper.TypeText("ContactLastName", "Tester");

                executionLog.Log("VerifyEmailPhoneAfterSaveOnClientInfo", "Select Phone Type");
                office_ClientsHelper.SelectByText("Cont.PhoneType", "Cell");

                executionLog.Log("VerifyEmailPhoneAfterSaveOnClientInfo", "ClickOn SaveBtn");
                office_ClientsHelper.ClickElement("OwnerSave");
                office_ClientsHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyEmailPhoneAfterSaveOnClientInfo", "Wait for success text.");
                office_ClientsHelper.WaitForText("Client data updated successfully. ", 10);

                executionLog.Log("VerifyEmailPhoneAfterSaveOnClientInfo", "Redirect To clients page. ");
                VisitOffice("clients");
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyEmailPhoneAfterSaveOnClientInfo", "Enter Company Name");
                office_ClientsHelper.TypeText("SearchClient", DBAName);
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyEmailPhoneAfterSaveOnClientInfo", "Select client by check box");
                office_ClientsHelper.ClickElement("ClickOn1stOpp");
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyEmailPhoneAfterSaveOnClientInfo", "Click on delete client");
                office_ClientsHelper.ClickElement("DeleteClient");

                executionLog.Log("VerifyEmailPhoneAfterSaveOnClientInfo", "Accept alert message.");
                office_ClientsHelper.AcceptAlert();

                executionLog.Log("VerifyEmailPhoneAfterSaveOnClientInfo", "Wait for success message.");
                office_ClientsHelper.WaitForText("1 records deleted successfully", 10);

                executionLog.Log("VerifyEmailPhoneAfterSaveOnClientInfo", "Redirect To client recycle bin page. ");
                VisitOffice("clients/recyclebin");

                executionLog.Log("VerifyEmailPhoneAfterSaveOnClientInfo", "Enter Company Name");
                office_ClientsHelper.TypeText("SearchClient", DBAName);
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyEmailPhoneAfterSaveOnClientInfo", "Click on delete client");
                office_ClientsHelper.ClickElement("DeleteRbin");

                executionLog.Log("VerifyEmailPhoneAfterSaveOnClientInfo", "Accept alert message.");
                office_ClientsHelper.AcceptAlert();

                executionLog.Log("VerifyEmailPhoneAfterSaveOnClientInfo", "Wait for success message.");
                office_ClientsHelper.WaitForText("Client Permanently Deleted.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyEmailPhoneAfterSaveOnClientInfo");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("VerifyEmail Phone After Save On Client Info");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("VerifyEmail Phone After Sav eOn Client Info", "Bug", "Medium", "Client page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("VerifyEmail Phone After Save On Client Info");
                        TakeScreenshot("VerifyEmailPhoneAfterSaveOnClientInfo");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyEmailPhoneAfterSaveOnClientInfo.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyEmailPhoneAfterSaveOnClientInfo");
                        string id = loginHelper.getIssueID("VerifyEmail Phone After Save On Client Info");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyEmailPhoneAfterSaveOnClientInfo.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("VerifyEmail Phone After Save On Client Info"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("VerifyEmail Phone After Save On Client Info");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyEmailPhoneAfterSaveOnClientInfo");
                executionLog.WriteInExcel("VerifyEmail Phone After Save On Client Info", Status, JIRA, "Client Management");
            }
        }
    }
}