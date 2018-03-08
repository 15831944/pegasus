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
    public class VerifyUserRedirectToShippingOnCancel : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS5")]
        [TestCategory("NewSkinTaskJira")]
        public void verifyUserRedirectToShippingOnCancel()
        {
            string[] username = null;
            string[] password = null;

            XMLParse oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");
            String JIRA = "";
            String Status = "Pass";

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var equipment_ShippingCarrierHelper = new Equipment_ShippingCarrierHelper(GetWebDriver());

            // Variable 
            String name = "Test" + RandomNumber(1, 99);
            String Id = "12345" + RandomNumber(1, 99);

            try
            {
                executionLog.Log("VerifyUserRedirectToShippingOnCancel", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyUserRedirectToShippingOnCancel", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyUserRedirectToShippingOnCancel", "Redirect at  Admin page.");
                VisitOffice("admin");

                executionLog.Log("VerifyUserRedirectToShippingOnCancel", "Redirect to manage shipping careers page.");
                VisitOffice("manage_shipping_carriers");

                executionLog.Log("VerifyUserRedirectToShippingOnCancel", " Click On Cancel");
                equipment_ShippingCarrierHelper.ClickElement("ClickCancelBtn");
                equipment_ShippingCarrierHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyUserRedirectToShippingOnCancel", "Verify heading");
                equipment_ShippingCarrierHelper.VerifyText("VerifyTextHedaing", "Shipping Carriers");
               
            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyUserRedirectToShippingOnCancel");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify User Redirect To Shipping On Cancel");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify User Redirect To Shipping On Cancel", "Bug", "Medium", "Shipping carriers page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify User Redirect To Shipping On Cancel");
                        TakeScreenshot("VerifyUserRedirectToShippingOnCancel");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyUserRedirectToShippingOnCancel.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyUserRedirectToShippingOnCancel");
                        string id = loginHelper.getIssueID("Verify User Redirect To Shipping On Cancel");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyUserRedirectToShippingOnCancel.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify User Redirect To Shipping On Cancel"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify User Redirect To Shipping On Cancel");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyUserRedirectToShippingOnCancel");
                executionLog.WriteInExcel("Verify User Redirect To Shipping On Cancel", Status, JIRA, "Equipment Management");
            }
        }
    }
}