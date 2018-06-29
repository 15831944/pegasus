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
    public class MoreDetailsIcon : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS1")]
        [TestCategory("ListManagement")]
        public void moreDetailsIcon()
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
                executionLog.Log("MoreDetailsIcon", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("MoreDetailsIcon", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("MoreDetailsIcon", "Redirect To List Management page");
                listManagementHelper.ClickElement("Marketing");
                listManagementHelper.WaitForWorkAround(4000);

                executionLog.Log("MoreDetailsIcon", "Redirect To List Management page");
                GetWebDriver().Navigate().GoToUrl("https://www.pegasus-test.com/en/listmanagements/clients");
                listManagementHelper.WaitForWorkAround(2000);

                executionLog.Log("CreateListWithBlankName", "Click on List Manager");
                listManagementHelper.ClickForce("ListManager");
                listManagementHelper.WaitForWorkAround(1000);

                executionLog.Log("MoreDetailsIcon", "Click on Expand Details Icon");
                listManagementHelper.ClickElement("DetailsIcon");
                listManagementHelper.WaitForWorkAround(1000);
                Console.WriteLine("Expand Details Icon");

                executionLog.Log("MoreDetailsIcon", "Click on Select All Check Box ");
                bool checkbox = listManagementHelper.isChecked("//*[@id='clients_wrapper']/div[1]/div[1]/div/table/thead/tr[2]/th[1]/div/label");
                if (checkbox == false)
                listManagementHelper.ClickViaJavaScript("//*[@id='clients_wrapper']/div[1]/div[1]/div/table/thead/tr[2]/th[1]/div/label");
                else
                { }

                }
            //}
            //catch (Exception e)
            //{
            //    executionLog.Log("Error", e.StackTrace);
            //    Status = "Fail";

            //    String counter = executionLog.readLastLine("counter");
            //    String Description = executionLog.GetAllTextFile("MoreDetailsIcon");
            //    String Error = executionLog.GetAllTextFile("Error");
            //    if (counter == "")
            //    {
            //        counter = "0";
            //    }
            //    bool result = loginHelper.CheckExstingIssue("More Details Icon");
            //    if (!result)
            //    {
            //        if (Int16.Parse(counter) < 9)
            //        {
            //            executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
            //            loginHelper.CreateIssue("More Details Icon", "Bug", "Medium", "Equipment page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
            //            string id = loginHelper.getIssueID("More Details Icon");
            //            TakeScreenshot("MoreDetailsIcon");
            //            string directoryName = loginHelper.GetnewDirectoryName(GetPath());
            //            var location = directoryName + "\\MoreDetailsIcon.png";
            //            loginHelper.AddAttachment(location, id);
            //        }
            //    }
            //    else
            //    {
            //        if (Int16.Parse(counter) < 9)
            //        {
            //            executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
            //            TakeScreenshot("MoreDetailsIcon");
            //            string id = loginHelper.getIssueID("More Details Icon");
            //            string directoryName = loginHelper.GetnewDirectoryName(GetPath());
            //            var location = directoryName + "\\MoreDetailsIcon.png";
            //            loginHelper.AddAttachment(location, id);
            //            loginHelper.AddComment(loginHelper.getIssueID("More Details Icon"), "This issue is still occurring");
            //        }
            //    }
            //    JIRA = loginHelper.getIssueID("More Details Icon");
            //    //  executionLog.DeleteFile("Error");
            //    throw;

            //}
            //finally
            //{
        //    executionLog.DeleteFile("MoreDetailsIcon");
            //    executionLog.WriteInExcel("More Details Icon", Status, JIRA, "List Management");
            //}
        }
    }