/* Documented by Khalil Shakir
* 
* The AdminSetFieldAsMandatory.cs is a test that wil validate the ability to set a 
* field  in the Office Main portal as mandatory. The 
* final part of this test case will change the setting back to its original form. 
* 
*/

using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Pegasus_New_skin.Scripts.Pegasus_Admin
{
    // Initial action, to verify the field that will be changed and to make the initial change

    [TestClass]
    public class AdminSetFieldAsMandatory : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin")]
        [TestCategory("TS6")]
        [TestCategory("Pegasus_Admin")]
        public void setFieldToMandatory()
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
            var mandatory = new AdminSetFormatFieldsHelper(GetWebDriver());

            //Random Variable
            String JIRA = "";
            String Status = "Pass";

            try
            {

                //Logging in 

                executionLog.Log("AdminSetFieldAsMandatory", "Login");
                Login(username[0], password[0]);

                executionLog.Log("AdminSetFieldAsMandatory", "VerifyTitle");
                VerifyTitle("Dashboard");

                executionLog.Log("OfficeMainFieldRelationships", " Testing A Client");
                VisitOffice("clients");
                mandatory.WaitForWorkAround(3000);

                executionLog.Log("AdminSetFieldAsMandatory", "ClickElement FirstClient");
                mandatory.ClickElement("FirstClient");
                mandatory.WaitForWorkAround(2000);

                //executionLog.Log("AdminSetFieldAsMandatory", "Hover CompanyDetails");
                //mandatory.MouseHover("CompanyDetails");

                executionLog.Log("AdminSetFieldAsMandatory", "ClickElement CompanyDetails");
                mandatory.ClickElement("CompanyDetails");
                mandatory.WaitForWorkAround(3000);

                // Verifying the field exists that will be changed

                executionLog.Log("AdminSetFieldAsMandatory", "Wait for element FederalTaxID");
                mandatory.ElementVisible("FederalTaxID");
                mandatory.WaitForWorkAround(4000);

                // Going to admin office 

                executionLog.Log("AdminSetFieldAsMandatory", "VisitOffice admin ");
                VisitOffice("admin");
                mandatory.WaitForWorkAround(4000);

                // Accessing field in field dictionary

                VisitOffice("fields");
                mandatory.WaitForWorkAround(3000);

                mandatory.SelectByText("Module", "Clients");
                mandatory.WaitForWorkAround(2000);

                executionLog.Log("AdminSetFieldAsMandatory", "ClickElement Search ");
                mandatory.ClickElement("Search");
                mandatory.WaitForWorkAround(3000);

                executionLog.Log("AdminSetFieldAsMandatory", "filter Federal Tax ID ");
                mandatory.TypeText("filter", "Federal Tax");
                mandatory.WaitForWorkAround(3000);

                executionLog.Log("AdminSetFieldAsMandatory", "ClickElement FederalTax");
                mandatory.ClickElement("FederalTax");
                mandatory.WaitForWorkAround(3000);

                executionLog.Log("AdminSetFieldAsMandatory", "ClickElement MandatoryCheckBox");
                mandatory.ClickElement("MandatoryCheckBox");
                mandatory.WaitForWorkAround(3000);

                executionLog.Log("AdminSetFieldAsMandatory", "ClickElement SaveFieldManager");
                mandatory.ClickJs("SaveFieldManager");
                mandatory.WaitForWorkAround(3000);

                // Now we will test the mandatory field.

                executionLog.Log("AdminSetFieldAsMandatory", "VisitOffice clients");
                VisitOffice("clients");
                mandatory.WaitForWorkAround(3000);

                executionLog.Log("AdminSetFieldAsMandatory", "ClickElement");
                mandatory.ClickElement("FirstClient");
                mandatory.WaitForWorkAround(3000);

                executionLog.Log("AdminSetFieldAsMandatory", "Hover Over CompanyDetails");
                mandatory.MouseHover("CompanyDetails");
                mandatory.WaitForWorkAround(4000);

                executionLog.Log("AdminSetFieldAsMandatory", " ClickElement CompanyDetails");
                mandatory.ClickElement("CompanyDetails");
                mandatory.WaitForWorkAround(2000);

                // Testing field by trying to save data

                executionLog.Log("AdminSetFieldAsMandatory", "TypeText 555555555");
                mandatory.TypeText("FerderalTaxField", "555555555");
                mandatory.WaitForWorkAround(4000);

                executionLog.Log("AdminSetFieldAsMandatory", " Click Save Element");
                mandatory.ClickForce("Save");
                mandatory.WaitForWorkAround(2000);

                //Logging in 

                executionLog.Log("AdminSetFieldAsMandatory", "Visit clients Office ");
                VisitOffice("clients");
                mandatory.WaitForWorkAround(3000);

                executionLog.Log("AdminSetFieldAsMandatory", "Click First Client Element");
                mandatory.ClickElement("FirstClient");
                mandatory.WaitForWorkAround(2000);

                executionLog.Log("AdminSetFieldAsMandatory", "Hover Over CompanyDetails");
                mandatory.MouseHover("CompanyDetails");

                executionLog.Log("AdminSetFieldAsMandatory", "Click CompanyDetails Element");
                mandatory.ClickElement("CompanyDetails");
                mandatory.WaitForWorkAround(5000);

                // Verifying the field exists that will be changed

                executionLog.Log("AdminSetFieldAsMandatory", "Check if FederalTaxID visible");
                mandatory.ElementVisible("FederalTax");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("AdminSetFieldAsMandatory");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Admin Set Field As Mandatory");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Admin Set Field As Mandatory", "Bug", "Medium", "Client page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Admin Set Field As Mandatory");
                        TakeScreenshot("AdminSetFieldAsMandatory");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AdminSetFieldAsMandatory.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("AdminSetFieldAsMandatory");
                        string id = loginHelper.getIssueID("Admin Set Field As Mandatory");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AdminSetFieldAsMandatory.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Admin Set Field As Mandatory"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Admin Set Field As Mandatory");
             //   executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("AdminSetFieldAsMandatory");
                executionLog.WriteInExcel("Admin Set Field As Mandatory", Status, JIRA, "Partner Portal");
            }
        }
    }
}