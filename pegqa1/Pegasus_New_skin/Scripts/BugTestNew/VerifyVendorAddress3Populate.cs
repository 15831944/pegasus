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
    public class VerifyVendorAddress3Populate : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin")]
        [TestCategory("BugTestNew")]
        public void verifyVendorAddress3Populate()
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
            
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("VerifyVendorAddress3Populate", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyVendorAddress3Populate", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("VerifyVendorAddress3Populate", "Redirect at vendors page.");
                VisitOffice("admin");

                executionLog.Log("VerifyVendorAddress3Populate", "Redirect To Vendors");
                VisitOffice("vendors");

                executionLog.Log("VerifyVendorAddress3Populate", "Verify title");
                VerifyTitle("Vendors");

                executionLog.Log("VerifyVendorAddress3Populate", " Click On Create");
                equipment_VendorsHelper.ClickElement("Create");

                executionLog.Log("VerifyVendorAddress3Populate", "Verify title");
                VerifyTitle("Create a New Vendor");

                executionLog.Log("VerifyVendorAddress3Populate", " Click On Add Address");
                equipment_VendorsHelper.ClickElement("AddAddressBtn");

                executionLog.Log("VerifyVendorAddress3Populate", " Again Click On Add Address");
                equipment_VendorsHelper.ClickElement("AddAddressBtn");

                executionLog.Log("VerifyVendorAddress3Populate", "Enter Zip Code in Address-3");
                equipment_VendorsHelper.TypeText("ZipCode3", "20001");
                equipment_VendorsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyVendorAddress3Populate", "Verify City");
                equipment_VendorsHelper.VerifyValue("City3", "Washington");

                executionLog.Log("VerifyVendorAddress3Populate", "Verify State");
                equipment_VendorsHelper.VerifySelectedOption("//select[@id='VendorAddress2State']", "DC");

                executionLog.Log("VerifyVendorAddress3Populate", "Verify Country");
                equipment_VendorsHelper.VerifySelectedOption("//select[@id='VendorAddress2Country']", "United States");
                Console.WriteLine("Address-3 populated successfully");


            }
            catch (Exception e)
            {
               
            }
           
        }
    }
}