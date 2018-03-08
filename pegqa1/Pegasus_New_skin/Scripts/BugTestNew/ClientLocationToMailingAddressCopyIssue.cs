using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class ClientLocationToMailingAddressCopyIssue : DriverTestCase
    {
        [TestMethod]
     //   [TestCategory("All")]
       // [TestCategory("NewSkin")]
        public void clientLocationToMailingAddressCopyIssue()
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
            var DBA = "Dba" + RandomNumber(99, 99999);

            String JIRA = "";
            String Status = "Pass";

            //      try
            //    {
            executionLog.Log("ClientLocationToMailingAddressCopyIssue", "Login with valid credential  Username");
            Login(username[0], password[0]);

            executionLog.Log("ClientLocationToMailingAddressCopyIssue", "Verify Page title");
            VerifyTitle("Dashboard");
            Console.WriteLine("Redirected at Dashboard screen.");

            executionLog.Log("ClientLocationToMailingAddressCopyIssue", "Goto Create Client");
            VisitOffice("clients/create");

            executionLog.Log("ClientLocationToMailingAddressCopyIssue", "Click on Assignments");
            office_ClientsHelper.ClickElement("Assignments");

            executionLog.Log("ClientLocationToMailingAddressCopyIssue", "Wait for element to be visible.");
            office_ClientsHelper.WaitForElementPresent("CompanyDetails", 10);

            executionLog.Log("ClientLocationToMailingAddressCopyIssue", "Goto Company details tab.");
            office_ClientsHelper.ClickElement("CompanyDetails");

            executionLog.Log("ClientLocationToMailingAddressCopyIssue", "Enter Comp DBA name.");
            office_ClientsHelper.TypeText("ClientDBAName", DBA);

            executionLog.Log("ClientLocationToMailingAddressCopyIssue", "Enter Client Bussiness Legal Name.");
            office_ClientsHelper.TypeText("BussinessLegalName", "LegalCli");

            executionLog.Log("ClientLocationToMailingAddressCopyIssue", "Client Status");
            office_ClientsHelper.SelectByText("ClientStatus", "New");

            executionLog.Log("ClientLocationToMailingAddressCopyIssue", "Select Client Responsibility");
            office_ClientsHelper.SelectByText("ClientResponsibility", "Howard Tang");

            executionLog.Log("ClientLocationToMailingAddressCopyIssue", "Enter location zip code.");
            office_ClientsHelper.TypeText("ZipCodeLocationAddress", "60601");
            office_ClientsHelper.WaitForWorkAround(3000);

            executionLog.Log("ClientLocationToMailingAddressCopyIssue", "Select Client Responsibility");
            office_ClientsHelper.TypeText("AddressLine1LocationAddress", "TestLocation");
            office_ClientsHelper.WaitForWorkAround(3000);

            executionLog.Log("ClientLocationToMailingAddressCopyIssue", "Select Client Responsibility");
            office_ClientsHelper.TypeText("AddressLine2", "test2");
            office_ClientsHelper.WaitForWorkAround(3000);

            executionLog.Log("ClientLocationToMailingAddressCopyIssue", "Select Client Responsibility");
            office_ClientsHelper.ClickViaJavaScript("//*[@id='ClientDetail0SameAsLocation']");
            office_ClientsHelper.WaitForWorkAround(3000);


          

            executionLog.Log("ClientLocationToMailingAddressCopyIssue", "Select Client Responsibility");
            office_ClientsHelper.VerifyText("AddressLine1MailingAddress", "TestLocation");
            office_ClientsHelper.WaitForWorkAround(3000);

            executionLog.Log("ClientLocationToMailingAddressCopyIssue", "Select Client Responsibility");
            office_ClientsHelper.VerifyText("AddressLine2Mail", "test2");
            office_ClientsHelper.WaitForWorkAround(3000);

          

        }
    } }
     /*       catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("ClientLocationToMailingAddressCopyIssue");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("ClientLocationToMailingAddressCopyIssue");
                if (!result)
                {
                    if (Int16.Parse(counter) < 5)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("ClientLocationToMailingAddressCopyIssue", "Bug", "Medium", "ClientLocationToMailingAddressCopyIssue", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("ClientLocationToMailingAddressCopyIssue");
                        TakeScreenshot("ClientLocationToMailingAddressCopyIssue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\Client.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 5)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("ClientLocationToMailingAddressCopyIssue");
                        string id = loginHelper.getIssueID("ClientLocationToMailingAddressCopyIssue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\Client.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("ClientLocationToMailingAddressCopyIssue"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("ClientLocationToMailingAddressCopyIssue");
                executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("ClientLocationToMailingAddressCopyIssue");
                executionLog.WriteInExcel("ClientLocationToMailingAddressCopyIssue", Status, JIRA, "Client Management");
            }
        }
    }
}
*/