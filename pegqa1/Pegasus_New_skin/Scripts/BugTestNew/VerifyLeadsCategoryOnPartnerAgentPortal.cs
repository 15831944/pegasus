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
    public class VerifyLeadsCategoryOnPartnerAgentPortal : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("BugTestNew")]
        public void verifyLeadsCategoryOnPartnerAgentPortal()
        {
            string[] username = null;
            string[] password = null;
            string[] username_p = null;
            string[] password_p = null;

            XMLParse oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            username_p = oXMLData.getData("settings/Credentials", "username_pagent");
            password_p = oXMLData.getData("settings/Credentials", "password_pagent");

            //Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var masterData_CategoryHelper = new MasterData_CategoryHelper(GetWebDriver());
            var pAgentPortal_LeadsHelper = new PAgentPortal_LeadsHelper(GetWebDriver());

            // Random Variable
            String JIRA = "";
            String Status = "Pass";
            var cat_name = "Automation_category" + GetRandomNumber();

            //try
            //{
                executionLog.Log("VerifyLeadsCategoryOnPartnerAgentPortal", " Login to Office Portal");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyLeadsCategoryOnPartnerAgentPortal", " Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyLeadsCategoryOnPartnerAgentPortal", " Redirect at categories page.");
                VisitOffice("categories");
                masterData_CategoryHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyLeadsCategoryOnPartnerAgentPortal", "Search Category by name");
                masterData_CategoryHelper.TypeText("SearchName", "Merchants");
                masterData_CategoryHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyLeadsCategoryOnPartnerAgentPortal", "Click on category");
                masterData_CategoryHelper.ClickElement("Category1");
                masterData_CategoryHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyLeadsCategoryOnPartnerAgentPortal", "Click on Create button");
                masterData_CategoryHelper.ClickElement("MerchantCreate");
                masterData_CategoryHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyLeadsCategoryOnPartnerAgentPortal", "Enter name of category");
                masterData_CategoryHelper.TypeText("CategoryName", cat_name);

                executionLog.Log("VerifyLeadsCategoryOnPartnerAgentPortal", "Click on Save button");
                masterData_CategoryHelper.ClickElement("SaveBtn");
                masterData_CategoryHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyLeadsCategoryOnPartnerAgentPortal", " Logout");
                VisitOffice("logout");

                executionLog.Log("VerifyLeadsCategoryOnPartnerAgentPortal", " Login to Partner Agent Portal");
                Login(username_p[0], password_p[0]);
                Console.WriteLine("Logged in as: " + username_p[0] + " / " + password_p[0]);

                executionLog.Log("VerifyLeadsCategoryOnPartnerAgentPortal", "Redirect to Create Leads page");
                VisitOffice("partners/lead_create");
                masterData_CategoryHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyLeadsCategoryOnPartnerAgentPortal", "Verify created merchant category is not appearing in Leads category drop down");
                Assert.IsFalse(pAgentPortal_LeadsHelper.IsElementPresent("//select[@id='LeadCategoryId']/option[contains(text(),'"+cat_name+"')]"));

            //}
            //catch (Exception e)
            //{
            //    executionLog.Log("Error", e.StackTrace);
            //    Status = "Fail";

            //    String counter = executionLog.readLastLine("counter");
            //    String Description = executionLog.GetAllTextFile("VerifyLeadsCategoryOnPartnerAgentPortal");
            //    String Error = executionLog.GetAllTextFile("Error");
            //    if (counter == "")
            //    {
            //        counter = "0";
            //    }
            //    bool result = loginHelper.CheckExstingIssue("Verify Leads Category On Partner Agent Portal");
            //    if (!result)
            //    {
            //        if (Int16.Parse(counter) < 9)
            //        {
            //            executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
            //            loginHelper.CreateIssue("Verify Leads Category On Partner Agent Portal", "Bug", "Medium", "Leads page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
            //            string id = loginHelper.getIssueID("Verify Leads Category On Partner Agent Portal");
            //            TakeScreenshot("VerifyLeadsCategoryOnPartnerAgentPortal");
            //            string directoryName = loginHelper.GetnewDirectoryName(GetPath());
            //            var location = directoryName + "\\VerifyLeadsCategoryOnPartnerAgentPortal.png";
            //            loginHelper.AddAttachment(location, id);
            //        }
            //    }
            //    else
            //    {
            //        if (Int16.Parse(counter) < 9)
            //        {
            //            executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
            //            TakeScreenshot("VerifyLeadsCategoryOnPartnerAgentPortal");
            //            string id = loginHelper.getIssueID("Verify Leads Category On Partner Agent Portal");
            //            string directoryName = loginHelper.GetnewDirectoryName(GetPath());
            //            var location = directoryName + "\\VerifyLeadsCategoryOnPartnerAgentPortal.png";
            //            loginHelper.AddAttachment(location, id);
            //            loginHelper.AddComment(loginHelper.getIssueID("Verify Leads Category On Partner Agent Portal"), "This issue is still occurring");
            //        }
            //    }
            //    JIRA = loginHelper.getIssueID("Verify Leads Category On Partner Agent Portal");
            //    executionLog.DeleteFile("Error");
            //    throw;
            //}
            //finally
            //{
            //    executionLog.DeleteFile("VerifyLeadsCategoryOnPartnerAgentPortal");
            //    executionLog.WriteInExcel("Verify Leads Category On Partner Agent Portal", Status, JIRA, "Partner Agent Portal");
            //}
        }
    }
}