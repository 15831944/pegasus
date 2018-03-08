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
    public class ImportAndMapPDFAzura : DriverTestCase
    {
        [TestMethod]
        [TestCategory("Admin")]
        [TestCategory("All")]
        [TestCategory("TS6")]
        [TestCategory("Pegasus_Admin")]
        public void importAndMapPDFAzura()
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
            var pDFTemplate_ImportWizardHelper = new PDFTemplate_ImportWizardHelper(GetWebDriver());

            // Variable
            var name = "Test" + GetRandomNumber();
            String filename = GetPathToFile() + "AZURA Bill of Sale.pdf";
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("ImportAndMapPDFAzura", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("ImportAndMapPDFAzura", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("ImportAndMapPDFAzura", "Click On  Admin");
                VisitOffice("admin");

                executionLog.Log("ImportAndMapPDFAzura", "Redirect");
                VisitOffice("pdf_templates/import");

                executionLog.Log("ImportAndMapPDFAzura", "Verify title");
                VerifyTitle("PDF Import Wizard");

                executionLog.Log("ImportAndMapPDFAzura", "Choose Module");
                pDFTemplate_ImportWizardHelper.Select("SelectModule", "20");

                executionLog.Log("ImportAndMapPDFAzura", "Upload PDF File");
                pDFTemplate_ImportWizardHelper.upload("SelectFile", filename);
                pDFTemplate_ImportWizardHelper.WaitForWorkAround(1000);

                executionLog.Log("ImportAndMapPDFAzura", "Click On Import");
                pDFTemplate_ImportWizardHelper.ClickElement("Import");
                pDFTemplate_ImportWizardHelper.WaitForWorkAround(3000);

                executionLog.Log("ImportAndMapPDFAzura", "Click on next button.");
                pDFTemplate_ImportWizardHelper.ClickForce("Next");
                pDFTemplate_ImportWizardHelper.WaitForWorkAround(3000);

                executionLog.Log("ImportAndMapPDFAzura", "verify title");
                VerifyTitle("PDF Import Wizard");

                executionLog.Log("ImportAndMapPDFAzura", "Select pdf category as other");
                pDFTemplate_ImportWizardHelper.SelectByText("Category", "Other");
                //pDFTemplate_ImportWizardHelper.WaitForWorkAround(2000);

                executionLog.Log("ImportAndMapPDFAzura", "Click on display intabs checkbox");
                pDFTemplate_ImportWizardHelper.ClickElement("DisplayinTabs");
                //pDFTemplate_ImportWizardHelper.WaitForWorkAround(2000);

                executionLog.Log("ImportAndMapPDFAzura", "Click on can share checkbox");
                pDFTemplate_ImportWizardHelper.ClickElement("CanShare");
                //pDFTemplate_ImportWizardHelper.WaitForWorkAround(2000);

                executionLog.Log("ImportAndMapPDFAzura", "Click on can email checkbox");
                pDFTemplate_ImportWizardHelper.ClickElement("CanEmail");
                //pDFTemplate_ImportWizardHelper.WaitForWorkAround(2000);

                executionLog.Log("ImportAndMapPDFAzura", "Click on all teams radio button");
                pDFTemplate_ImportWizardHelper.ClickElement("AllTeams");
                //pDFTemplate_ImportWizardHelper.WaitForWorkAround(2000);

                executionLog.Log("ImportAndMapPDFAzura", "Click on Save button");
                pDFTemplate_ImportWizardHelper.ClickElement("Save1");
                pDFTemplate_ImportWizardHelper.WaitForWorkAround(3000);

                executionLog.Log("ImportAndMapPDFAzura", "wait for text success text.");
                pDFTemplate_ImportWizardHelper.WaitForText("PDF Template options saved successfully.", 10);

                executionLog.Log("ImportAndMapPDFAzura", "Redirect at pdf templates page.");
                VisitOffice("pdf_templates");
                pDFTemplate_ImportWizardHelper.WaitForWorkAround(3000);

                executionLog.Log("ImportAndMapPDFAzura", "Search uploaded pdf by name");
                pDFTemplate_ImportWizardHelper.TypeText("SearchName", "AZURA Bill of Sale");
                pDFTemplate_ImportWizardHelper.WaitForWorkAround(2000);

                executionLog.Log("ImportAndMapPDFAzura", "Click on edit mapping icon.");
                pDFTemplate_ImportWizardHelper.ClickElement("EditPDFMapping");
                pDFTemplate_ImportWizardHelper.WaitForWorkAround(2000);

                executionLog.Log("ImportAndMapPDFAzura", "Verify page title.");
                VerifyTitle("PDF Import Wizard");
                //pDFTemplate_ImportWizardHelper.WaitForWorkAround(2000);

                executionLog.Log("ImportAndMapPDFAzura", "Click on map default dictionary fields.");
                pDFTemplate_ImportWizardHelper.ClickElement("DefaultDictionary");
                pDFTemplate_ImportWizardHelper.WaitForWorkAround(3000);

                //executionLog.Log("ImportAndMapPDFAzura", "Wait for locator to be present.");
                //pDFTemplate_ImportWizardHelper.WaitForElementPresent("SearchPDFField", 10);

                executionLog.Log("ImportAndMapPDFAzura", "Search imported pdf file field by name");
                pDFTemplate_ImportWizardHelper.TypeText("SearchPDFField", "Title");
                pDFTemplate_ImportWizardHelper.WaitForWorkAround(2000);

                //executionLog.Log("ImportAndMapPDFAzura", "Wait for locator to be present.");
                //pDFTemplate_ImportWizardHelper.WaitForElementPresent("MapWithRule", 10);

                executionLog.Log("ImportAndMapPDFAzura", "Select mapping method as rule set.");
                pDFTemplate_ImportWizardHelper.ClickElement("MapWithRule");
                pDFTemplate_ImportWizardHelper.WaitForWorkAround(2000);

                //executionLog.Log("ImportAndMapPDFAzura", "Wait for locator to be present.");
                //pDFTemplate_ImportWizardHelper.WaitForElementPresent("SelectRuleType", 10);

                executionLog.Log("ImportAndMapPDFAzura", "Select rule set type to Owner/contact.");
                pDFTemplate_ImportWizardHelper.SelectByText("SelectRuleType", "Contact/Owner/Banking Details");
                pDFTemplate_ImportWizardHelper.WaitForWorkAround(3000);

                //executionLog.Log("ImportAndMapPDFAzura", "Wait for locator to be present..");
                //pDFTemplate_ImportWizardHelper.WaitForElementPresent("SelectContactOwner", 10);

                executionLog.Log("ImportAndMapPDFAzura", "Select type as owner");
                pDFTemplate_ImportWizardHelper.SelectByText("SelectContactOwner", "Owner");
                pDFTemplate_ImportWizardHelper.WaitForWorkAround(2000);

                executionLog.Log("ImportAndMapPDFAzura", "Select owner type as primary.");
                pDFTemplate_ImportWizardHelper.SelectByText("PrimaryOther", "Primary");
                pDFTemplate_ImportWizardHelper.WaitForWorkAround(2000);

                executionLog.Log("ImportAndMapPDFAzura", "Select owner field as title.");
                pDFTemplate_ImportWizardHelper.SelectByText("SelectContactOwnerFiled", "Title");
                pDFTemplate_ImportWizardHelper.WaitForWorkAround(2000);

                executionLog.Log("ImportAndMapPDFAzura", "Save mapping / rule.");
                pDFTemplate_ImportWizardHelper.ClickElement("SaveRule");
                pDFTemplate_ImportWizardHelper.WaitForWorkAround(3000);

                executionLog.Log("ImportAndMapPDFAzura", "Search pdf field as vendor name.");
                pDFTemplate_ImportWizardHelper.TypeText("SearchPDFField", "Vender Name");
                pDFTemplate_ImportWizardHelper.WaitForWorkAround(3000);

                executionLog.Log("ImportAndMapPDFAzura", "Select rule type as owner/contact");
                pDFTemplate_ImportWizardHelper.SelectByText("SelectRuleType", "Contact/Owner/Banking Details");
                pDFTemplate_ImportWizardHelper.WaitForWorkAround(2000);

                executionLog.Log("ImportAndMapPDFAzura", "Select rule type as owner.");
                pDFTemplate_ImportWizardHelper.SelectByText("SelectContactOwner", "Owner");
                pDFTemplate_ImportWizardHelper.WaitForWorkAround(2000);

                executionLog.Log("ImportAndMapPDFAzura", "Select type of owner as primary.");
                pDFTemplate_ImportWizardHelper.SelectByText("PrimaryOther", "Primary");
                pDFTemplate_ImportWizardHelper.WaitForWorkAround(2000);

                executionLog.Log("ImportAndMapPDFAzura", "Select owner first name.");
                pDFTemplate_ImportWizardHelper.SelectByText("SelectContactOwnerFiled", "First Name of Guarantor");
                pDFTemplate_ImportWizardHelper.WaitForWorkAround(2000);

                executionLog.Log("ImportAndMapPDFAzura", "Click on add field.");
                pDFTemplate_ImportWizardHelper.ClickElement("AddField");
                //pDFTemplate_ImportWizardHelper.WaitForWorkAround(2000);

                executionLog.Log("ImportAndMapPDFAzura", "Select delimiter type as space.");
                pDFTemplate_ImportWizardHelper.SelectByText("SelectDelimiter1", "Concat with Space( )");
                //pDFTemplate_ImportWizardHelper.WaitForWorkAround(5000);

                executionLog.Log("ImportAndMapPDFAzura", "Select vendor middle name.");
                pDFTemplate_ImportWizardHelper.SelectByText("VendorMiddleName", "Middle Name of Guarantor");
                //pDFTemplate_ImportWizardHelper.WaitForWorkAround(5000);

                executionLog.Log("ImportAndMapPDFAzura", "Click on add fields");
                pDFTemplate_ImportWizardHelper.ClickElement("AddField");
                pDFTemplate_ImportWizardHelper.WaitForWorkAround(1000);

                executionLog.Log("ImportAndMapPDFAzura", "Select delimiter type as space.");
                pDFTemplate_ImportWizardHelper.SelectByText("SelectDelimiter2", "Concat with Space( )");
                //pDFTemplate_ImportWizardHelper.WaitForWorkAround(5000);

                executionLog.Log("ImportAndMapPDFAzura", "select last name of vendor.");
                pDFTemplate_ImportWizardHelper.SelectByText("VendorLastName", "Last Name of Guarantor");
                //pDFTemplate_ImportWizardHelper.WaitForWorkAround(5000);

                executionLog.Log("ImportAndMapPDFAzura", "Save rule / mapping.");
                pDFTemplate_ImportWizardHelper.ClickElement("SaveRule");
                pDFTemplate_ImportWizardHelper.WaitForWorkAround(4000);

                executionLog.Log("ImportAndMapPDFAzura", "Search pdf field address");
                pDFTemplate_ImportWizardHelper.TypeText("SearchPDFField", "Address");
                pDFTemplate_ImportWizardHelper.WaitForWorkAround(2000);

                executionLog.Log("ImportAndMapPDFAzura", "Select address line1.");
                pDFTemplate_ImportWizardHelper.SelectByText("SelectContactOwnerFiled", "Address Line 1");
                pDFTemplate_ImportWizardHelper.WaitForWorkAround(2000);

                executionLog.Log("ImportAndMapPDFAzura", "Select address line 2.");
                pDFTemplate_ImportWizardHelper.SelectByText("VendorMiddleName", "Address Line 2");
                pDFTemplate_ImportWizardHelper.WaitForWorkAround(2000);

                executionLog.Log("ImportAndMapPDFAzura", "Remove third added field.");
                pDFTemplate_ImportWizardHelper.ClickElement("Remove2");
                pDFTemplate_ImportWizardHelper.WaitForWorkAround(2000);

                executionLog.Log("ImportAndMapPDFAzura", "Save rule / mapping.");
                pDFTemplate_ImportWizardHelper.ClickElement("SaveRule");
                pDFTemplate_ImportWizardHelper.WaitForWorkAround(2000);

                executionLog.Log("ImportAndMapPDFAzura", "Search pdf file field city.");
                pDFTemplate_ImportWizardHelper.TypeText("SearchPDFField", "City");
                pDFTemplate_ImportWizardHelper.WaitForWorkAround(2000);

                executionLog.Log("ImportAndMapPDFAzura", "select address field city.");
                pDFTemplate_ImportWizardHelper.SelectByText("SelectContactOwnerFiled", "City");
                pDFTemplate_ImportWizardHelper.WaitForWorkAround(2000);

                executionLog.Log("ImportAndMapPDFAzura", "Remove added field from previous rule.");
                pDFTemplate_ImportWizardHelper.ClickElement("Remove1");
                pDFTemplate_ImportWizardHelper.WaitForWorkAround(2000);

                executionLog.Log("ImportAndMapPDFAzura", "Click to save rule /mapping.");
                pDFTemplate_ImportWizardHelper.ClickElement("SaveRule");
                pDFTemplate_ImportWizardHelper.WaitForWorkAround(2000);

                executionLog.Log("ImportAndMapPDFAzura", "Search pdf field state.");
                pDFTemplate_ImportWizardHelper.TypeText("SearchPDFField", "State");
                pDFTemplate_ImportWizardHelper.WaitForWorkAround(2000);

                executionLog.Log("ImportAndMapPDFAzura", "Select address field state.");
                pDFTemplate_ImportWizardHelper.SelectByText("SelectContactOwnerFiled", "State");
                pDFTemplate_ImportWizardHelper.WaitForWorkAround(2000);

                executionLog.Log("ImportAndMapPDFAzura", "Save rule / mapping.");
                pDFTemplate_ImportWizardHelper.ClickElement("SaveRule");
                pDFTemplate_ImportWizardHelper.WaitForWorkAround(2000);

                executionLog.Log("ImportAndMapPDFAzura", "Search pdf field zip code.");
                pDFTemplate_ImportWizardHelper.TypeText("SearchPDFField", "Zip");
                pDFTemplate_ImportWizardHelper.WaitForWorkAround(2000);

                executionLog.Log("ImportAndMapPDFAzura", "Select adress field zip code.");
                pDFTemplate_ImportWizardHelper.SelectByText("SelectContactOwnerFiled", "Zip Code");
                pDFTemplate_ImportWizardHelper.WaitForWorkAround(2000);

                executionLog.Log("ImportAndMapPDFAzura", "Click to save rule / mapping.");
                pDFTemplate_ImportWizardHelper.ClickElement("SaveRule");
                pDFTemplate_ImportWizardHelper.WaitForWorkAround(2000);

                executionLog.Log("ImportAndMapPDFAzura", "Search pdf field vendor phone number.");
                pDFTemplate_ImportWizardHelper.TypeText("SearchPDFField", "Vender Phone Number");
                pDFTemplate_ImportWizardHelper.WaitForWorkAround(2000);

                executionLog.Log("ImportAndMapPDFAzura", "Select rule type as office details.");
                pDFTemplate_ImportWizardHelper.SelectByText("SelectRuleType", "Office Details");
                pDFTemplate_ImportWizardHelper.WaitForWorkAround(2000);

                executionLog.Log("ImportAndMapPDFAzura", "Select office details field phone number");
                pDFTemplate_ImportWizardHelper.SelectByText("OfficeDetailFields", "Phone Number");
                pDFTemplate_ImportWizardHelper.WaitForWorkAround(2000);

                executionLog.Log("ImportAndMapPDFAzura", "Click to save rule / mapping.");
                pDFTemplate_ImportWizardHelper.ClickElement("SaveRule");
                pDFTemplate_ImportWizardHelper.WaitForWorkAround(4000);

                executionLog.Log("ImportAndMapPDFAzura", "Click on save button to finish mapping.");
                pDFTemplate_ImportWizardHelper.ClickElement("SaveEdit");

                executionLog.Log("ImportAndMapPDFAzura", "Click on check box to select pdf file.");
                pDFTemplate_ImportWizardHelper.ClickElement("CheckBox1");

                executionLog.Log("ImportAndMapPDFAzura", "Click Delete btn  ");
                pDFTemplate_ImportWizardHelper.ClickElement("DeletePDF");

                executionLog.Log("ImportAndMapPDFAzura", "Accept alert message. ");
                pDFTemplate_ImportWizardHelper.AcceptAlert();

                executionLog.Log("ImportAndMapPDFAzura", "Wait for message ");
                pDFTemplate_ImportWizardHelper.WaitForText("PDF Template Deleted Successfully.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("ImportAndMapPDFAzura");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("PDF Import Wizard");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("PDF Import Wizard", "Bug", "Medium", "Pdf Import page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("PDF Import Wizard");
                        TakeScreenshot("ImportAndMapPDFAzura");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ImportAndMapPDFAzura.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("ImportAndMapPDFAzura");
                        string id = loginHelper.getIssueID("PDF Import Wizard");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ImportAndMapPDFAzura.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("PDF Import Wizard"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("PDF Import Wizard");
            //    executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("ImportAndMapPDFAzura");
                executionLog.WriteInExcel("PDF Import Wizard", Status, JIRA, "PDF Import");
            }
        }
    }
}