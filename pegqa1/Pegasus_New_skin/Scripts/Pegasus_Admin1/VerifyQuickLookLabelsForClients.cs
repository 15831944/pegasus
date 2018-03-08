using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class VerifyQuickLookLabelsForClients : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin1")]
        [TestCategory("Fail")]
        [TestCategory("TS7")]
        [TestCategory("Pegasus_Admin1")]
        public void verifyQuickLookLabelsForClients()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var office_ClientsHelper = new Office_ClientsHelper(GetWebDriver());
            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Variable
            var Oppname = "Test" + RandomNumber(99, 99999);
            var DBA = "New" + RandomNumber(99, 99999);
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("VerifyQuickLookLabelsForClients", "Login with valid credential  Username");
                Login(username[0], password[0]);

                executionLog.Log("VerifyQuickLookLabelsForClients", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyQuickLookLabelsForClients", "Goto Create Client");
                VisitOffice("clients/create");
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuickLookLabelsForClients", "Enter Client Dba Name");
                office_ClientsHelper.TypeText("ClientDBAName", DBA);

                executionLog.Log("VerifyQuickLookLabelsForClients", "Select client status");
                office_ClientsHelper.SelectByText("ClientStatus", "New");

                executionLog.Log("VerifyQuickLookLabelsForClients", "Select Client Res[onsibility.");
                office_ClientsHelper.SelectByText("ClientResponsibility", "Howard Tang");

                executionLog.Log("VerifyQuickLookLabelsForClients", "Click on next button");
                office_ClientsHelper.ClickElement("Next");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyQuickLookLabelsForClients", "Wait for confirmation message.");
                office_ClientsHelper.WaitForText("Client saved successfully. ", 05);

                executionLog.Log("VerifyQuickLookLabelsForClients", "Verify label for client type.");
                office_ClientsHelper.VerifyText("ClientType", "Click to edit");
                //office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyQuickLookLabelsForClients", "Verify label for status.");
                office_ClientsHelper.VerifyText("Status1", "New");
                //office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyQuickLookLabelsForClients", "Verify label for source.");
                office_ClientsHelper.VerifyText("Source", "Select");
                //office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyQuickLookLabelsForClients", "Verify label for category");
                office_ClientsHelper.VerifyText("Category", "Select");
                //office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyQuickLookLabelsForClients", "Verify label for responsibility");
                office_ClientsHelper.VerifyText("Responsibilityl", "Howard Tang");
                //office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyQuickLookLabelsForClients", "Verify label for account manager.");
                office_ClientsHelper.VerifyText("AccountManager", "Select");
                //office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyQuickLookLabelsForClients", "Verify label for partner agent.");
                office_ClientsHelper.VerifyText("PartnerAgentl", "Select");
                //office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyQuickLookLabelsForClients", "Save Button");
                office_ClientsHelper.VerifyText("PartnerAssociationl", "Select Partner Association");
                //office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyQuickLookLabelsForClients", "Save Button");
                office_ClientsHelper.VerifyText("SalesManager", "Select");
                //office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyQuickLookLabelsForClients", "Click on company details tab.");
                office_ClientsHelper.ClickElement("CompanyDetails");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyQuickLookLabelsForClients", "Enter the Ferderal tax id");
                office_ClientsHelper.TypeText("FederalTaxID", "123432232");
                //office_ClientsHelper.WaitForWorkAround(3000);

                //executionLog.Log("VerifyQuickLookLabelsForClients", "Click on assignments.");
                //office_ClientsHelper.ClickElement("EditAssignments");
                //office_ClientsHelper.WaitForWorkAround(3000);

                //executionLog.Log("VerifyQuickLookLabelsForClients", "Wait for locator to be present.");
                //office_ClientsHelper.WaitForElementPresent("Clientt", 10);

                executionLog.Log("VerifyQuickLookLabelsForClients", "Select client type.");
                office_ClientsHelper.Select("SelectClientT", "Processing");
                //office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyQuickLookLabelsForClients", "Select client refferal source.");
                office_ClientsHelper.Select("SelectSource", "Campaign");
                //office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyQuickLookLabelsForClients", "Select client category.");
                office_ClientsHelper.SelectByText("SelectCategory", "Other");
                //office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyQuickLookLabelsForClients", "Select account manager.");
                office_ClientsHelper.SelectByText("SelectAcc.Mger", "Howard Tang");
                //office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyQuickLookLabelsForClients", "Select client user group.");
                office_ClientsHelper.SelectByText("Ugroup", "Primary Group");
                //office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyQuickLookLabelsForClients", "Select sales manager.");
                office_ClientsHelper.SelectByText("SelectSalesManager", "Howard Tang");
                //office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyQuickLookLabelsForClients", "Select client responsibility");
                office_ClientsHelper.SelectByText("ClientResponsibility", "Howard Tang");
                //office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyQuickLookLabelsForClients", "Select partner agent.");
                office_ClientsHelper.SelectByText("ClientPartnerAgent", "Mark Menu");
                //office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyQuickLookLabelsForClients", "Select partner association.");
                office_ClientsHelper.SelectByText("ClientPartnerAssociation", "Aslam Associate");
                //office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyQuickLookLabelsForClients", "click on save client");
                office_ClientsHelper.ClickElement("SaveButtonByTitle");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyQuickLookLabelsForClients", "click on info tab.");
                office_ClientsHelper.ClickElement("InfoTab");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyQuickLookLabelsForClients", "Verify label for client type.");
                office_ClientsHelper.VerifyText("ClientType", "Processing");
                //office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyQuickLookLabelsForClients", "Verify label for status.");
                office_ClientsHelper.VerifyText("Status1", "New");
                //office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyQuickLookLabelsForClients", "Verify label for source.");
                office_ClientsHelper.VerifyText("Source", "Campaign");
                //office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyQuickLookLabelsForClients", "Verify label for category.");
                office_ClientsHelper.VerifyText("Category", "Other");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyQuickLookLabelsForClients", "Verify label for responsibility.");
                office_ClientsHelper.VerifyText("Responsibilityl", "Howard Tang");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyQuickLookLabelsForClients", "Verify label for account manager.");
                office_ClientsHelper.VerifyText("AccountManager", "Howard Tang");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyQuickLookLabelsForClients", "Verify label for partner agent.");
                office_ClientsHelper.VerifyText("PartnerAgentl", "Mark Menu");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyQuickLookLabelsForClients", "Verify label for partner association.");
                office_ClientsHelper.VerifyText("PartnerAssociationl", "Aslam Associate");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyQuickLookLabelsForClients", "Verify label for partner association.");
                office_ClientsHelper.VerifyText("SalesManager", "Howard Tang");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyQuickLookLabelsForClients", "Redirect To clients page. ");
                VisitOffice("clients");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyQuickLookLabelsForClients", "Enter Company Name");
                office_ClientsHelper.TypeText("SearchClient", DBA);
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuickLookLabelsForClients", "Select client by check box");
                office_ClientsHelper.ClickElement("ClickOn1stOpp");
                //office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuickLookLabelsForClients", "Click on delete client");
                office_ClientsHelper.ClickElement("DeleteClient");

                executionLog.Log("VerifyQuickLookLabelsForClients", "Accept alert message.");
                office_ClientsHelper.AcceptAlert();

                executionLog.Log("VerifyQuickLookLabelsForClients", "Wait for success message.");
                office_ClientsHelper.WaitForText("1 records deleted successfully", 10);

                executionLog.Log("VerifyQuickLookLabelsForClients", "Redirect To client recycle bin page. ");
                VisitOffice("clients/recyclebin");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyQuickLookLabelsForClients", "Enter Company Name");
                office_ClientsHelper.TypeText("SearchClient", DBA);
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuickLookLabelsForClients", "Selcet All in responsibility field");
                office_ClientsHelper.SelectByText("ClientResponsibityRecycle", "All");
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuickLookLabelsForClients", "Click on delete client");
                office_ClientsHelper.ClickElement("DeleteRbin");

                executionLog.Log("VerifyQuickLookLabelsForClients", "Accept alert message.");
                office_ClientsHelper.AcceptAlert();

                executionLog.Log("VerifyQuickLookLabelsForClients", "Wait for success message.");
                office_ClientsHelper.WaitForText("Client Permanently Deleted.", 10);

                VisitOffice("logout");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyQuickLookLabelsForClients");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("VerifyQuickLookLabelsForClients");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Opportunities", "Bug", "Medium", "Clients page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("VerifyQuickLookLabelsForClients");
                        TakeScreenshot("VerifyQuickLookLabelsForClients");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyQuickLookLabelsForClients.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyQuickLookLabelsForClients");
                        string id = loginHelper.getIssueID("VerifyQuickLookLabelsForClients");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyQuickLookLabelsForClients.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("VerifyQuickLookLabelsForClients"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("VerifyQuickLookLabelsForClients");
                //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyQuickLookLabelsForClients");
                executionLog.WriteInExcel("VerifyQuickLookLabelsForClients", Status, JIRA, "Client management");
            }
        }
    }
}