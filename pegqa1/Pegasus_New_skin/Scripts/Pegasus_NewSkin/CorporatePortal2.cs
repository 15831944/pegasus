using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class CorporatePortal2 : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin")]
        [TestCategory("TS8")]
        [TestCategory("Pegasus_NewSkin")]
        public void corporatePortal2()
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
            var corpFieldDictionary_SectionsHelper = new CorpFieldDictionary_SectionsHelper(GetWebDriver());
            var corpFieldDictionary_TabsHelper = new CorpFieldDictionary_TabsHelper(GetWebDriver());
            var corpSystem_SettingsHelper = new CorpSystem_SettingsHelper(GetWebDriver());
            var corp_ProfileHelper = new Corp_ProfileHelper(GetWebDriver());
            var corpSystem_AuditTrialsHelper = new CorpSystem_AuditTrialsHelper(GetWebDriver());
            var corpSystem_EmailTemplatesHelper = new CorpSystem_EmailTemplatesHelper(GetWebDriver());
            var corpMasterdata_RatesAndFeesHelper = new CorpMasterdata_RatesAndFeesHelper(GetWebDriver());

            // Variable random
            var usernme = "Sysprins" + RandomNumber(44, 799999977);
            var name = "Test" + GetRandomNumber();
            var FDNAME = "TEST" + GetRandomNumber();

            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("CorporatePortal2", "Login with valid username and password");
                Login(username[0], password[0]);

                executionLog.Log("CorporatePortal2", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("CorporatePortal2", "Visit rates and fee page.");
                VisitCorp("masterdata/manage_rates_fees");
                corpMasterdata_RatesAndFeesHelper.WaitForWorkAround(3000);

                executionLog.Log("CorporatePortal2", "Enter template name.");
                corpMasterdata_RatesAndFeesHelper.TypeText("PricingTemplateName", name);

                executionLog.Log("CorporatePortal2", "Select Processor Type");
                corpMasterdata_RatesAndFeesHelper.SelectByText("ProcessorType", "First Data Omaha");
                corpMasterdata_RatesAndFeesHelper.WaitForWorkAround(4000);

                executionLog.Log("CorporatePortal2", "Select Merchant Type");
                corpMasterdata_RatesAndFeesHelper.SelectByText("SelectMerchanType", "Ecommerce");
                //corpMasterdata_RatesAndFeesHelper.WaitForWorkAround(3000);

                executionLog.Log("CorporatePortal2", "Methos Of Accepting");
                corpMasterdata_RatesAndFeesHelper.SelectByText("MethodOfAcceptingCards", "Manually Swiped");
                //corpMasterdata_RatesAndFeesHelper.WaitForWorkAround(3000);

                executionLog.Log("CorporatePortal2", "Click on Save button.");
                corpMasterdata_RatesAndFeesHelper.ClickElement("ClickRateAndFeesSave");
                //corpMasterdata_RatesAndFeesHelper.WaitForWorkAround(3000);

                executionLog.Log("CorporatePortal2", "Wait for confirmation.");
                corpMasterdata_RatesAndFeesHelper.WaitForText("The Rates is successfully created!!", 10);
                //corpMasterdata_RatesAndFeesHelper.WaitForWorkAround(2000);

                executionLog.Log("CorporatePortal2", "Goto masterdata/rates_fees");
                VisitCorp("masterdata/rates_fees");
                corpMasterdata_RatesAndFeesHelper.WaitForWorkAround(3000);

                executionLog.Log("CorporatePortal2", "Edit Icon");
                corpMasterdata_RatesAndFeesHelper.ClickElement("EditRateAndFeesIcon");
                //corpMasterdata_RatesAndFeesHelper.WaitForElementPresent("ClickRateAndFeesSave", 10);
                corpMasterdata_RatesAndFeesHelper.WaitForWorkAround(3000);

                executionLog.Log("CorporatePortal2", "Click on Save");
                corpMasterdata_RatesAndFeesHelper.ClickElement("ClickRateAndFeesSave");
                corpMasterdata_RatesAndFeesHelper.WaitForWorkAround(4000);

                executionLog.Log("CorporatePortal2", "Goto masterdata/rates_fees");
                VisitCorp("masterdata/rates_fees");
                corpMasterdata_RatesAndFeesHelper.WaitForWorkAround(3000);

                executionLog.Log("CorporatePortal2", "Enter Rate and Fess TEMPLATE NAME");
                corpMasterdata_RatesAndFeesHelper.TypeText("SearchTemp", name);
                corpMasterdata_RatesAndFeesHelper.WaitForWorkAround(2000);

                executionLog.Log("CorporatePortal2", "Delete icon");
                corpMasterdata_RatesAndFeesHelper.ClickElement("ClickOnDelete");
                corpMasterdata_RatesAndFeesHelper.AcceptAlert();

                executionLog.Log("CorporatePortal2", "Wait for Confirmation");
                corpMasterdata_RatesAndFeesHelper.WaitForText("The Rates is successfully deleted!!", 10);
                //corpMasterdata_RatesAndFeesHelper.WaitForWorkAround(3000);

                executionLog.Log("CorporatePortal2", "email_templates");
                VisitCorp("email_templates");
                corpMasterdata_RatesAndFeesHelper.WaitForWorkAround(3000);

                executionLog.Log("CorporatePortal2", "Click on Edit");
                corpSystem_EmailTemplatesHelper.ClickElement("EditEmailTemplateIcon");
                corpMasterdata_RatesAndFeesHelper.WaitForWorkAround(3000);

                executionLog.Log("CorporatePortal2", "Save");
                corpSystem_EmailTemplatesHelper.ClickElement("ClickOnSaveEmailTemp");
                corpMasterdata_RatesAndFeesHelper.WaitForWorkAround(3000);

                executionLog.Log("CorporatePortal2", "Wait for Confirmation");
                corpSystem_EmailTemplatesHelper.WaitForText("The email management has been saved", 10);

                executionLog.Log("CorporatePortal2", "Goto Audit trial");
                VisitCorp("audit-trails");
                corpMasterdata_RatesAndFeesHelper.WaitForWorkAround(4000);

                executionLog.Log("CorporatePortal2", "Click on Audit Trail name");
                corpSystem_AuditTrialsHelper.ClickElement("ClickOnAuditTrialMame");
                corpMasterdata_RatesAndFeesHelper.WaitForWorkAround(3000);

                executionLog.Log("CorporatePortal2", "Audit trial Save button");
                corpSystem_AuditTrialsHelper.ClickElement("AduiltTrialSaveBtn");

                executionLog.Log("CorporatePortal2", "Wait for Confirmation");
                corpSystem_AuditTrialsHelper.WaitForText("Options Saved.", 10);

                executionLog.Log("CorporatePortal2", "got to my profile");
                VisitCorp("myprofile");
                corpMasterdata_RatesAndFeesHelper.WaitForWorkAround(4000);

                executionLog.Log("CorporatePortal2", "Click Edit");
                corp_ProfileHelper.ClickElement("EditProfile");
                corpMasterdata_RatesAndFeesHelper.WaitForWorkAround(3000);

                executionLog.Log("CorporatePortal2", "Click on Save");
                corp_ProfileHelper.ClickElement("Save");
                corpMasterdata_RatesAndFeesHelper.AcceptAlert();

                executionLog.Log("CorporatePortal2", "Wait for Confirmation");
                corp_ProfileHelper.WaitForText("Your profile has been successfully updated", 10);

                executionLog.Log("CorporatePortal2", "Redirect at settings");
                VisitCorp("settings");
                corpMasterdata_RatesAndFeesHelper.WaitForWorkAround(4000);

                executionLog.Log("CorporatePortal2", "Click Save Edit Profile");
                corpSystem_SettingsHelper.ClickElement("ClickSaveEditProfile");

                executionLog.Log("CorporatePortal2", "Wait for confirmation");
                corpSystem_SettingsHelper.WaitForText("Settings updated successfully", 20);

                executionLog.Log("CorporatePortal2", "go to tabs");
                VisitCorp("tabs");
                corpMasterdata_RatesAndFeesHelper.WaitForWorkAround(4000);

                executionLog.Log("CorporatePortal2", "Create");
                corpFieldDictionary_TabsHelper.ClickElement("AddNewTabs");
                corpFieldDictionary_TabsHelper.WaitForWorkAround(3000);

                executionLog.Log("CorporatePortal2", "Tab Name");
                corpFieldDictionary_TabsHelper.TypeText("TabName", name);

                executionLog.Log("CorporatePortal2", "Click on Save");
                corpFieldDictionary_TabsHelper.ClickOnDisplayed("FDSaveButton");
                corpFieldDictionary_TabsHelper.WaitForWorkAround(3000);

                executionLog.Log("CorporatePortal2", "Wait for Confirmation");
                corpFieldDictionary_TabsHelper.WaitForText("Tab Created Successfully", 10);

                executionLog.Log("CorporatePortal2", "Go to Sections");
                VisitCorp("sections");
                corpMasterdata_RatesAndFeesHelper.WaitForWorkAround(4000);

                executionLog.Log("CorporatePortal2", "Create Button");
                corpFieldDictionary_SectionsHelper.ClickElement("ClickCreateButton");
                corpFieldDictionary_SectionsHelper.WaitForWorkAround(3000);

                executionLog.Log("CorporatePortal2", "Select tab");
                corpFieldDictionary_SectionsHelper.SelectByText("TabNameFieldDicSevtion", name);

                executionLog.Log("CorporatePortal2", "Section Name");
                corpFieldDictionary_SectionsHelper.TypeText("FDSectionName", FDNAME);

                executionLog.Log("CorporatePortal2", "Save");
                corpFieldDictionary_SectionsHelper.ClickOnDisplayed("FDSaveButton");

                executionLog.Log("CorporatePortal2", "Confirmation");
                corpFieldDictionary_SectionsHelper.VerifyAlertText("Section Created Successfully");
                corpFieldDictionary_SectionsHelper.AcceptAlert();
                corpFieldDictionary_SectionsHelper.WaitForWorkAround(3000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CorporatePortal2");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Corporate Portal 2");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Corporate Portal 2", "Bug", "Medium", "Corp page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Corporate Portal 2");
                        TakeScreenshot("CorporatePortal2");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CorporatePortal2.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CorporatePortal2");
                        string id = loginHelper.getIssueID("Corporate Portal 2");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CorporatePortal2.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Corporate Portal 2"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Corporate Portal 2");
                //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("CorporatePortal2");
                executionLog.WriteInExcel("Corporate Portal 2", Status, JIRA, "Corp Modules");
            }
        }
    }
}