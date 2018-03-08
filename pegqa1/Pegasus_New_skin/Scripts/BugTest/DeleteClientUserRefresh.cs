using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class DeleteClientUserRefresh : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS1")]
        [TestCategory("BugTest")]
        public void deleteClientUserRefresh()
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
            var office_ClientUsersHelper = new Office_UserHelper(GetWebDriver());

            // Variable
            var FirstName = "Fname" + RandomNumber(1, 999);
            var LastName = "Lname" + RandomNumber(1, 999);
            var Number = "123" + RandomNumber(1, 9999);
            var DBAName = "DBA" + RandomNumber(1, 999);
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("DeleteClientUserRefresh", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("DeleteClientUserRefresh", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("DeleteClientUserRefresh", "Go to create clients page.");
                VisitOffice("clients/create");
                office_ClientsHelper.WaitForWorkAround(1000);

                executionLog.Log("DeleteClientUserRefresh", "Enter Client Dba Name");
                office_ClientsHelper.TypeText("ClientDBAName", DBAName);

                executionLog.Log("DeleteClientUserRefresh", "Select client status");
                office_ClientsHelper.SelectByText("ClientStatus", "New");

                executionLog.Log("DeleteClientUserRefresh", "Select Client Res[onsibility.");
                office_ClientsHelper.SelectByText("ClientResponsibility", "Howard Tang");

                executionLog.Log("DeleteClientUserRefresh", "Click on next button");
                office_ClientsHelper.ClickElement("Next");
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("DeleteClientUserRefresh", "Wait for confirmation message.");
                office_ClientsHelper.WaitForText("Client saved successfully. ", 10);

                executionLog.Log("DeleteClientUserRefresh", "Click on Owner Tab");
                office_ClientsHelper.ClickElement("OwnerTab");
                office_ClientsHelper.WaitForWorkAround(5000);

                executionLog.Log("DeleteClientUserRefresh", "Enter First Name");
                office_ClientsHelper.TypeText("OwnerFirstName", "Test");

                executionLog.Log("DeleteClientUserRefresh", "Enter Last Name");
                office_ClientsHelper.TypeText("OwnerLastName", "Tester");

                executionLog.Log("DeleteClientUserRefresh", "Enter the title");
                office_ClientsHelper.TypeText("TitleOwner", "President");

                executionLog.Log("DeleteClientUserRefresh", "Select Phone Type");
                office_ClientsHelper.SelectByText("OwnerPhoneType", "Emergency");

                executionLog.Log("DeleteClientUserRefresh", "Enter Owner phone no.");
                office_ClientsHelper.TypeText("OwnerPhoneNumber", "1234567890");

                executionLog.Log("DeleteClientUserRefresh", "Select Phone Type");
                //       office_ClientsHelper.SelectByText("OwnerAddressType", "Home");

                executionLog.Log("DeleteClientUserRefresh", "Enter Owner Zip code.");
                //         office_ClientsHelper.TypeText("OwnerZipCode", "60601");

                executionLog.Log("DeleteClientUserRefresh", "Select owner eAddress label.");
                office_ClientsHelper.SelectByText("OwnereAddressType", "E-Mail");

                executionLog.Log("DeleteClientUserRefresh", "Select owner eAddress label.");
                office_ClientsHelper.SelectByText("OwnerEAddressLabel", "Home");

                executionLog.Log("DeleteClientUserRefresh", "Enter Owner eAddress");
                office_ClientsHelper.TypeText("OwnereAddressEnter", "test@yopmail.com");

                executionLog.Log("DeleteClientUserRefresh", "Click On Save Btn");
                office_ClientsHelper.ClickElement("SaveButtonByTitle");
                office_ClientsHelper.WaitForWorkAround(2000);

                office_ClientsHelper.WaitForText("Client data updated successfully.", 10);

                executionLog.Log("DeleteClientUserRefresh", "Go tO Client");
                VisitOffice("users/client_users");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("DeleteClientUserRefresh", "Click on create Client User");
                office_ClientUsersHelper.ClickElement("CreateClientUser");
                office_ClientUsersHelper.WaitForWorkAround(2000);

                executionLog.Log("DeleteClientUserRefresh", "Enter DBA Name");
                office_ClientUsersHelper.TypeText("EnterCompanyNameCU", DBAName);

                executionLog.Log("DeleteClientUserRefresh", "Click on Search Button");
                office_ClientUsersHelper.ClickElement("ClickOnSearchButton");
                office_ClientUsersHelper.WaitForWorkAround(4000);

                executionLog.Log("DeleteClientUserRefresh", "Click on client company name");
                office_ClientUsersHelper.ClickElement("ClickDbaname");
                office_ClientUsersHelper.WaitForWorkAround(1000);

                executionLog.Log("DeleteClientUserRefresh", "Client User button");
                office_ClientUsersHelper.ClickElement("CreateClientUser1");

                executionLog.Log("DeleteClientUserRefresh", "Accept alert message.");
                office_ClientsHelper.AcceptAlert();
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("DeleteClientUserRefresh", "Close pop up");
                office_ClientUsersHelper.ClickElement("ClikCloseCUPOP");
                office_ClientUsersHelper.WaitForWorkAround(2000);

                executionLog.Log("DeleteClientUserRefresh", "Click Refresh");
                office_ClientUsersHelper.ClickElement("ClickRefreshCU");
                office_ClientUsersHelper.WaitForWorkAround(2000);

                executionLog.Log("DeleteClientUserRefresh", "Search DBA");
                office_ClientUsersHelper.TypeText("SearchClientUSER", DBAName);
                office_ClientUsersHelper.WaitForWorkAround(2000);

                executionLog.Log("DeleteClientUserRefresh", "Create Client");
                office_ClientUsersHelper.ClickElement("ClickOnTheClientUser");
                office_ClientUsersHelper.WaitForWorkAround(1000);

                executionLog.Log("DeleteClientUserRefresh", "Click on  Delete User");
                office_ClientUsersHelper.ClickElement("ClickOnDeleteUser");

                executionLog.Log("DeleteClientUserRefresh", "Accept alert message.");
                office_ClientUsersHelper.AcceptAlert();

                executionLog.Log("DeleteClientUserRefresh", "Wait for success message.");
                office_ClientUsersHelper.WaitForText("Client Users", 10);
                office_ClientUsersHelper.WaitForWorkAround(5000);

                executionLog.Log("DeleteClientUserRefresh", "Redirect at clients page.");
                VisitOffice("clients");

                executionLog.Log("DeleteClientUserRefresh", "Enter Company Name");
                office_ClientsHelper.TypeText("SearchClient", DBAName);
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("DeleteClientUserRefresh", "Select client by check box");
                office_ClientsHelper.ClickElement("ClickOn1stOpp");
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("DeleteClientUserRefresh", "Click on delete client");
                office_ClientsHelper.ClickElement("DeleteClient");

                executionLog.Log("DeleteClientUserRefresh", "Accept alert message.");
                office_ClientsHelper.AcceptAlert();

                executionLog.Log("DeleteClientUserRefresh", "Wait for success message.");
                office_ClientsHelper.WaitForText("1 records deleted successfully", 10);

                executionLog.Log("DeleteClientUserRefresh", "Redirect To client recycle bin page. ");
                VisitOffice("clients/recyclebin");

                executionLog.Log("DeleteClientUserRefresh", "Enter Company Name");
                office_ClientsHelper.TypeText("SearchClient", DBAName);
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("DeleteClientUserRefresh", "Select All in responsibilty");
                office_ClientsHelper.SelectByText("ClientResponsibityRecycle", "All");
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("DeleteClientUserRefresh", "Click on delete client");
                office_ClientsHelper.ClickElement("DeleteRbin");

                executionLog.Log("DeleteClientUserRefresh", "Accept alert message.");
                office_ClientsHelper.AcceptAlert();

                executionLog.Log("DeleteClientUserRefresh", "Wait for success message.");
                office_ClientsHelper.WaitForText("Client Permanently Deleted.", 10);


            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("DeleteClientUserRefresh");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Delete Client User Refresh");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Delete Client User Refresh", "Bug", "Medium", "Client page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Delete Client User Refresh");
                        TakeScreenshot("DeleteClientUserRefresh");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\DeleteClientUserRefresh.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("DeleteClientUserRefresh");
                        string id = loginHelper.getIssueID("Delete Client User Refresh");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\DeleteClientUserRefresh.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Delete Client User Refresh"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Delete Client User Refresh");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("DeleteClientUserRefresh");
                executionLog.WriteInExcel("Delete Client User Refresh", Status, JIRA, "Client Management");
            }
        }
    }
}