using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class CustomizeRecordDetailsClients : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS9")]
        [TestCategory("ListManagement")]
        public void customizeRecordDetailsClients()
        {
            string[] username = null;
            string[] password = null;

            XMLParse oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var listManagementHelper = new ListManagementHelper(GetWebDriver());

            // Variable 
            var name = "Test" + GetRandomNumber();
            var name2 = "Testlist" + GetRandomNumber();
            var Id = "12345" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            //try
            //{
            executionLog.Log("CustomizeRecordDetailsClients", "Login with valid username and password");
            Login(username[0], password[0]);
            Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

            executionLog.Log("CustomizeRecordDetailsClients", "Verify Page title");
            VerifyTitle("Dashboard");
            Console.WriteLine("Redirected at Dashboard screen.");

            executionLog.Log("CustomizeRecordDetailsClients", "Redirect To List Management page");
            listManagementHelper.ClickElement("Marketing");
            listManagementHelper.WaitForWorkAround(4000);

            executionLog.Log("CustomizeRecordDetailsClients", "Redirect To List Management page");
            GetWebDriver().Navigate().GoToUrl("https://www.pegasus-test.com/en/listmanagements/clients");
            listManagementHelper.WaitForWorkAround(2000);

            executionLog.Log("CustomizeRecordDetailsClients", "Click on Expand Details Icon");
            listManagementHelper.ClickForce("ExpandIconClient");
            listManagementHelper.WaitForWorkAround(2000);

            executionLog.Log("CustomizeRecordDetailsClients", "Click on Customize Field Option");
            listManagementHelper.ClickViaJavaScript("//*[@id='clients']/tbody/tr[2]/td/div[2]");
            listManagementHelper.WaitForWorkAround(2000);

            executionLog.Log("CustomColumnsClients", "Click on Search Box");
            listManagementHelper.TypeText("SearchDetail", "Account Manager");
            listManagementHelper.WaitForWorkAround(1000);

            executionLog.Log("CustomizeRecordDetailsClients", "Click on Plus Icon");
            listManagementHelper.ClickViaJavaScript("//*[@id='modalDetailColumnsleft']/div[2]/div/div[5]/div/i");
            listManagementHelper.WaitForWorkAround(2000);

            executionLog.Log("CustomizeRecordDetailsClients", "Click on Apply Button");
            listManagementHelper.ClickForce("ApplyDetails");
            listManagementHelper.WaitForWorkAround(1000);

            executionLog.Log("CustomizeRecordDetailsClients", "Click on Expand Details Icon");
            listManagementHelper.ClickForce("ExpandIconClient");
            listManagementHelper.WaitForWorkAround(2000);

            executionLog.Log("CustomizeRecordDetailsClients", "Click on Customize Field Option");
            listManagementHelper.ClickViaJavaScript("//*[@id='clients']/tbody/tr[2]/td/div[2]");
            listManagementHelper.WaitForWorkAround(2000);

            executionLog.Log("CustomColumnsClients", "Click on Search Box");
            listManagementHelper.TypeText("SearchDetail", "User Group");
            listManagementHelper.WaitForWorkAround(1000);

            executionLog.Log("CustomizeRecordDetailsClients", "Click on Plus Icon");
            listManagementHelper.ClickViaJavaScript("//*[@id='modalDetailColumnsleft']/div[2]/div/div[6]/div/i");
            listManagementHelper.WaitForWorkAround(2000);

            executionLog.Log("CustomizeRecordDetailsClients", "Click on Cancel button");
            listManagementHelper.ClickViaJavaScript("//*[@id='modalDetailColumns']/div/div/div/div[3]/button[1]");
            listManagementHelper.WaitForWorkAround(1000);


            //}
            //catch (Exception e)
            //{
            //    executionLog.Log("Error", e.StackTrace);
            //    Status = "Fail";

            //    String counter = executionLog.readLastLine("counter");
            //    String Description = executionLog.GetAllTextFile("CustomizeRecordDetailsClients");
            //    String Error = executionLog.GetAllTextFile("Error");
            //    if (counter == "")
            //    {
            //        counter = "0";
            //    }
            //    bool result = loginHelper.CheckExstingIssue("Customize Record Details Clients");
            //    if (!result)
            //    {
            //        if (Int16.Parse(counter) < 9)
            //        {
            //            executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
            //            loginHelper.CreateIssue("Customize Record Details Clients", "Bug", "Medium", "Equipment page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
            //            string id = loginHelper.getIssueID("Customize Record Details Clients");
            //            TakeScreenshot("CustomizeRecordDetailsClients");
            //            string directoryName = loginHelper.GetnewDirectoryName(GetPath());
            //            var location = directoryName + "\\CustomizeRecordDetailsClients.png";
            //            loginHelper.AddAttachment(location, id);
            //        }
            //    }
            //    else
            //    {
            //        if (Int16.Parse(counter) < 9)
            //        {
            //            executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
            //            TakeScreenshot("CustomizeRecordDetailsClients");
            //            string id = loginHelper.getIssueID("Customize Record Details Clients");
            //            string directoryName = loginHelper.GetnewDirectoryName(GetPath());
            //            var location = directoryName + "\\CustomizeRecordDetailsClients.png";
            //            loginHelper.AddAttachment(location, id);
            //            loginHelper.AddComment(loginHelper.getIssueID("Customize Record Details Clients"), "This issue is still occurring");
            //        }
            //    }
            //    JIRA = loginHelper.getIssueID("Customize Record Details Clients");
            //    //  executionLog.DeleteFile("Error");
            //    throw;

            //}
            //finally
            //{
            //   executionLog.DeleteFile("CustomizeRecordDetailsClients");
            //    executionLog.WriteInExcel("Customize Record Details Clients", Status, JIRA, "List Management");
            //}
        }
    }

}