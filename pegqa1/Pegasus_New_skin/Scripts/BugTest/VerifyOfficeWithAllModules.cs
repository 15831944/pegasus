using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class VerifyOfficeWithAllModules : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("BugTest")]

        public void verifyOfficeWithAllModules()
        {

            string[] username = null;
            string[] password = null;


            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_corp");
            password = oXMLData.getData("settings/Credentials", "password");

            

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var corp_Office_OfficeHelper = new CorpOffice_OfficeHelper(GetWebDriver());
            var yopmail_Helper = new YopMailHelper(GetWebDriver());
            var office_Common_Layout = new OfficeCommonLayoutHelper(GetWebDriver());
            var office_Admin_Layout = new OfficeAdminCommonLayoutHelper(GetWebDriver());


            //  Variable random
            
            String JIRA = "";
            String Status = "Pass";
            var officename = "Office" + RandomNumber(1, 999999999);
            var username1 = "user" + RandomNumber(1, 9999);
            
            //try
            //{
                executionLog.Log("VerifyOfficeWithAllModules", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyOfficeWithAllModules", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected to Dashboard");

                executionLog.Log("VerifyOfficeWithAllModules", "Create Office");
                VisitCorp("offices");
                Console.WriteLine("Redirected to All Office page");

                executionLog.Log("VerifyOfficeWithAllModules", "Click on Create button");
                corp_Office_OfficeHelper.ClickElement("Create");

                executionLog.Log("VerifyOfficeWithAllModules", "Verify page title");
                VerifyTitle("Create an Office");

                executionLog.Log("VerifyOfficeWithAllModules", "Enter Office name");
                corp_Office_OfficeHelper.TypeText("Name", officename);

                executionLog.Log("VerifyOfficeWithAllModules", "Enter Address Line 1");
                corp_Office_OfficeHelper.TypeText("AddressLine1", "Add 1");

                executionLog.Log("VerifyOfficeWithAllModules", "Enter Zip Code");
                corp_Office_OfficeHelper.TypeText("ZIpCode", "20001");
                corp_Office_OfficeHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyOfficeWithAllModules", "Deselect Auto generate password check box");
                corp_Office_OfficeHelper.ClickElement("AutoGenPassword");
                corp_Office_OfficeHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyOfficeWithAllModules", "Enter Username");
                corp_Office_OfficeHelper.TypeText("PrimaryUserName", username1);

                executionLog.Log("VerifyOfficeWithAllModules", "Enter Password");
                corp_Office_OfficeHelper.TypeText("PrimaryPassword", "123456");

                executionLog.Log("VerifyOfficeWithAllModules", "Enter First Name");
                corp_Office_OfficeHelper.TypeText("FirstName", "Test");

                executionLog.Log("VerifyOfficeWithAllModules", "Enter Last Name");
                corp_Office_OfficeHelper.TypeText("LastName", "User");

                executionLog.Log("VerifyOfficeWithAllModules", "Select eAddress label");
                corp_Office_OfficeHelper.Select("EaddressLabel", "Home");

                executionLog.Log("VerifyOfficeWithAllModules", "Enter eAddress");
                corp_Office_OfficeHelper.TypeText("eAddress", "aslam.pegasus@yopmail.com");
                Console.WriteLine("Entered eAddress");


                executionLog.Log("VerifyOfficeWithAllModules", "Click on Save");
                try
                {
                    corp_Office_OfficeHelper.ClickElement("Save");
                }
                catch (OpenQA.Selenium.WebDriverException)
                { }
                
                Console.WriteLine("Office Created");

                executionLog.Log("VerifyOfficeWithAllModules", "Go to yopmail.com");
                GetWebDriver().Navigate().GoToUrl("http://www.yopmail.com/en/");
                Console.WriteLine("Redirected to yopmail.com");
                
                executionLog.Log("VerifyOfficeWithAllModules", "Enter eAddress in yopmail");
                yopmail_Helper.TypeText("Yopmail", "aslam.pegasus");

                executionLog.Log("VerifyOfficeWithAllModules", "Check Inbox");
                yopmail_Helper.ClickElement("YopmailClick");

                yopmail_Helper.switchFrame("ifmail");

                yopmail_Helper.VerifyText("MailSecondPoint", username1);

                executionLog.Log("VerifyOfficeWithAllModules", "Click on Office Link");
                yopmail_Helper.ClickElement("OfficeLink");
                corp_Office_OfficeHelper.SwitchToWindow();

                yopmail_Helper.WaitForWorkAround(5000);

                 executionLog.Log("VerifyOfficeWithAllModules", "Click on Profile Icon");
                 corp_Office_OfficeHelper.ClickElement("ProfileIcon");
              
                 executionLog.Log("VerifyOfficeWithAllModules", "Click on Logout");
                 corp_Office_OfficeHelper.ClickElement("Logout");
                 Console.WriteLine("Redirected to Login Screen");

                 executionLog.Log("VerifyOfficeWithAllModules", "Enter Username");
                 loginHelper.TypeText("Login/Username", username1);

                 executionLog.Log("VerifyOfficeWithAllModules", "Enter Last Name");
                 loginHelper.TypeText("Login/Password", "123456");
                
                 executionLog.Log("VerifyOfficeWithAllModules", "Verify Office");
                 loginHelper.ClickElement("Login/Enter");
                 Console.WriteLine("Logged into new office");
                 
                 executionLog.Log("VerifyOfficeWithAllModules", "Verify Page title");
                 VerifyTitle("Dashboard");

                 executionLog.Log("VerifyOfficeWithAllModules", "Click on Hamburger Icon");
                 office_Common_Layout.ClickElement("HamburgerIcon");
                 
                 executionLog.Log("VerifyOfficeWithAllModules", "Verify Home Module");
                 office_Common_Layout.ElementVisible("HomeTab");
                 Console.WriteLine("Home Module is present");

                 executionLog.Log("VerifyOfficeWithAllModules", "Verify Opportunities Module");
                 office_Common_Layout.ElementVisible("OpportunitiesTab");
                 Console.WriteLine("Opportunity Module is present");

                 executionLog.Log("VerifyOfficeWithAllModules", "Verify Leads Module");
                 office_Common_Layout.ElementVisible("LeadsTab");
                 Console.WriteLine("Leads Module is present");

                 executionLog.Log("VerifyOfficeWithAllModules", "Verify Merchants Module");
                 office_Common_Layout.ElementVisible("ClientsTab");
                 Console.WriteLine("Merchants Module is present");

                 executionLog.Log("VerifyOfficeWithAllModules", "Verify Contacts Module");
                 office_Common_Layout.ElementVisible("ContactsTab");
                 Console.WriteLine("Contacts Module is present");

                 executionLog.Log("VerifyOfficeWithAllModules", "Verify Activities Module");
                 office_Common_Layout.ElementVisible("ActivitiesTab");
                 Console.WriteLine("Activities Module is present");

                 executionLog.Log("VerifyOfficeWithAllModules", "Verify Tickets Module");
                 office_Common_Layout.ElementVisible("TicketsTab");
                 Console.WriteLine("Tickets Module is present");

                 executionLog.Log("VerifyOfficeWithAllModules", "Verify Agents Module");
                 office_Common_Layout.ElementVisible("AgentsTab");
                 Console.WriteLine("Agents Module is present");

                 executionLog.Log("VerifyOfficeWithAllModules", "Verify Residual Income Module");
                 office_Common_Layout.ElementVisible("ResidualIncomeTab");
                 Console.WriteLine("Residual Income Module is present");

                 executionLog.Log("VerifyOfficeWithAllModules", "Verify Reports Module");
                 office_Common_Layout.ElementVisible("ReportsTab");
                 Console.WriteLine("Reports Module is present");

                 office_Common_Layout.ScrollDown("//ul[@id='menu']/li[13]");

                 executionLog.Log("VerifyOfficeWithAllModules", "Verify Dashboards Module");
                 office_Common_Layout.ElementVisible("Dashboards");
                 Console.WriteLine("Dashboards Module is present");

                 executionLog.Log("VerifyOfficeWithAllModules", "Redirect To Admin");
                 VisitOffice("admin");
                 office_Common_Layout.WaitForWorkAround(4000);
                 Console.WriteLine("Redirected to Admin Site");

                 executionLog.Log("VerifyOfficeWithAllModules", "Verify Office Module");
                 office_Admin_Layout.ElementVisible("Office");
                 Console.WriteLine("Office Module is present");

                 executionLog.Log("VerifyOfficeWithAllModules", "Verify Corporate Module");
                 office_Admin_Layout.ElementVisible("Corporate");
                 Console.WriteLine("Corporate Module is present");

                 executionLog.Log("VerifyOfficeWithAllModules", "Verify Master Data Module");
                 office_Admin_Layout.ElementVisible("MasterDataTab");
                 Console.WriteLine("Master Data Module is present");

                 executionLog.Log("VerifyOfficeWithAllModules", "Verify Statistics Module");
                 office_Admin_Layout.ElementVisible("StatisticsTab");
                 Console.WriteLine("Statistics Module is present");

                 executionLog.Log("VerifyOfficeWithAllModules", "Verify Tickets Module");
                 office_Admin_Layout.ElementVisible("TicketsTab");
                 Console.WriteLine("Tickets Module is present");

                 executionLog.Log("VerifyOfficeWithAllModules", "Verify System Module");
                 office_Admin_Layout.ElementVisible("System");
                 Console.WriteLine("System Module is present");

                 executionLog.Log("VerifyOfficeWithAllModules", "Verify Integrations Module");
                 office_Admin_Layout.ElementVisible("IntegrationsTab");
                 Console.WriteLine("Integrations Module is present");

                 executionLog.Log("VerifyOfficeWithAllModules", "Verify Products Module");
                 office_Admin_Layout.ElementVisible("ProductsTab");
                 Console.WriteLine("Products Module is present");

                 executionLog.Log("VerifyOfficeWithAllModules", "Verify PDF Templates Module");
                 office_Admin_Layout.ElementVisible("PDFTemplatesTab");
                 Console.WriteLine("PDF Templates Module is present");

                 executionLog.Log("VerifyOfficeWithAllModules", "Verify Equipment Module");
                 office_Admin_Layout.ElementVisible("EquipmentTab");
                 Console.WriteLine("Equipment Module is present");

                 office_Admin_Layout.ScrollDown("//ul[@id='menu']/li[13]");

                 executionLog.Log("VerifyOfficeWithAllModules", "Verify Field Dictionary Module");
                 office_Admin_Layout.ElementVisible("FieldDicTab");
                 Console.WriteLine("Field Dictionary Module is present");


            //}
            //catch (Exception e)
            //{
            //}
            
        }
    }
}