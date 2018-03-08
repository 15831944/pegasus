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
    public class VerifyPrimaryRadioBtnOfEmailAndPhoneForVendor : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin")]
        [TestCategory("BugTestNew")]
        public void verifyPrimaryRadioBtnOfEmailAndPhoneForVendor()
        {
            string[] username = null;
            string[] password = null;

            XMLParse oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            //Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var equipment_VendorsHelper = new Equipment_VendorsHelper(GetWebDriver());

            // Variable 
            var name = "Test" + GetRandomNumber();
            var Id = "12345" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("VerifyPrimaryRadioBtnOfEmailAndPhoneForVendor", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyPrimaryRadioBtnOfEmailAndPhoneForVendor", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("VerifyPrimaryRadioBtnOfEmailAndPhoneForVendor", "Redirect to Create Vendor page");
                VisitOffice("vendors/create");
                equipment_VendorsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyPrimaryRadioBtnOfEmailAndPhoneForVendor", "Verify title");
                VerifyTitle("Create a New Vendor");

                executionLog.Log("VerifyPrimaryRadioBtnOfEmailAndPhoneForVendor", "Click on Add Email");
                equipment_VendorsHelper.ClickElement("AddEmail");
                equipment_VendorsHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyPrimaryRadioBtnOfEmailAndPhoneForVendor", "Click on Add Phone");
                equipment_VendorsHelper.ClickElement("AddPhone");
                equipment_VendorsHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyPrimaryRadioBtnOfEmailAndPhoneForVendor", "Verify Primary radio button for Email");
                equipment_VendorsHelper.IsElementPresent("//input[@name='data[VendorElectronicAddress][0][primary]']");
                equipment_VendorsHelper.IsElementPresent("//input[@name='data[VendorElectronicAddress][1][primary]']");

                executionLog.Log("VerifyPrimaryRadioBtnOfEmailAndPhoneForVendor", "Verify Primary radio button for Phone");
                equipment_VendorsHelper.IsElementPresent("//input[@name='data[VendorPhone][0][primary]']");
                equipment_VendorsHelper.IsElementPresent("//input[@name='data[VendorPhone][1][primary]']");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyPrimaryRadioBtnOfEmailAndPhoneForVendor");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Primary Radio Btn Of Email And Phone For Vendor");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Primary Radio Btn Of Email And Phone For Vendor", "Bug", "Medium", "Create Vendor page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Primary Radio Btn Of Email And Phone For Vendor");
                        TakeScreenshot("VerifyPrimaryRadioBtnOfEmailAndPhoneForVendor");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyPrimaryRadioBtnOfEmailAndPhoneForVendor.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyPrimaryRadioBtnOfEmailAndPhoneForVendor");
                        string id = loginHelper.getIssueID("Verify Primary Radio Btn Of Email And Phone For Vendor");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyPrimaryRadioBtnOfEmailAndPhoneForVendor.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Primary Radio Btn Of Email And Phone For Vendor"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Primary Radio Btn Of Email And Phone For Vendor");
              //  executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("VerifyPrimaryRadioBtnOfEmailAndPhoneForVendor");
                executionLog.WriteInExcel("Verify Primary Radio Btn Of Email And Phone For Vendor", Status, JIRA, "Vendor page");
            }
        }
    }
}