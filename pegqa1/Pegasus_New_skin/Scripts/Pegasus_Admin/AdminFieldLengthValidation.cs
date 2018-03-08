/* Documented by Khalil Shakir
* 
* The AdminFieldLengthValidation.cs file is a test that wil validate the length of text  
* allowed in a field based on the amount set in the Pegasus dictionary  
* The final part of this test case will change the setting back to its original settings
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
    public class AdminFieldLengthValidation : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin")]
        [TestCategory("TS6")]
        [TestCategory("Pegasus_Admin")]
        public void setFieldLength()
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
            var fieldLength = new AdminSetFormatFieldsHelper(GetWebDriver());

            String JIRA = "";
            String Status = "Pass";

            try
            {
            executionLog.Log("AdminFieldLengthValidation", "WaitForWorkAround");
            //fieldLength.WaitForWorkAround(2000);

            //Logging in 
            executionLog.Log("AdminFieldLengthValidation", "Login");
            Login(username[0], password[0]);

            executionLog.Log("AdminFieldLengthValidation", "WaitForWorkAround");
            //fieldLength.WaitForWorkAround(3000);

            executionLog.Log("AdminFieldLengthValidation", "Verify Title Dashboard");
            VerifyTitle("Dashboard");
            fieldLength.WaitForWorkAround(3000);

            executionLog.Log("AdminFieldLengthValidation", "Click on Menu Icon");
            fieldLength.ClickElement("MenuIcon");

            executionLog.Log("AdminFieldLengthValidation", "Click Clients Tab Element");
            fieldLength.MouseHover("ClientsTab");
            fieldLength.ClickJs("ClientBtn");
            fieldLength.WaitForWorkAround(3000);
            //fieldLength.WaitForElementPresent("FirstClient", 20);

            executionLog.Log("AdminFieldLengthValidation", "Click First Client Element");
            fieldLength.ClickElement("FirstClient");
            fieldLength.WaitForWorkAround(3000);
            //fieldLength.WaitForElementPresent("CompanyDetails", 10);

            executionLog.Log("AdminFieldLengthValidation", "Mouse Hover CompanyDetails");
            fieldLength.MouseHover("CompanyDetails");
            //fieldLength.WaitForWorkAround(1000);

            executionLog.Log("AdminFieldLengthValidation", "Click Company Details Element");
            fieldLength.ClickElement("CompanyDetails");
            fieldLength.WaitForWorkAround(3000);

            // Verifying the field exists that will be changed
            executionLog.Log("AdminFieldLengthValidation", "Wait for City Field");
            fieldLength.ElementVisible("CityField");
            //fieldLength.WaitForWorkAround(1000);

            // Going to admin office 
            executionLog.Log("AdminFieldLengthValidation", "Visit admin Office");
            //VisitOffice("admin");
            //fieldLength.WaitForWorkAround(1000);

            // Accessing field in field dictionary
            VisitOffice("fields");
            fieldLength.WaitForWorkAround(3000);

            fieldLength.SelectByText("Module", "Clients");
            fieldLength.WaitForWorkAround(1000);

            executionLog.Log("AdminFieldLengthValidation", "Click Search Element");
            fieldLength.ClickElement("Search");
            fieldLength.WaitForWorkAround(3000);

            executionLog.Log("AdminFieldLengthValidation", " type city ");
            fieldLength.TypeText("filter", "city");
            fieldLength.WaitForWorkAround(2000);

            executionLog.Log("AdminFieldLengthValidation", "Click City Element");
            fieldLength.ClickJs("City");
            fieldLength.WaitForWorkAround(4000);

            // setting length limit

            executionLog.Log("AdminFieldLengthValidation", "Click Field Length CheckBox Element");
            fieldLength.checkAndClick("FieldLengthCheckBox");
            //fieldLength.WaitForWorkAround(3000);

            executionLog.Log("AdminFieldLengthValidation", "Type 1");
            fieldLength.TypeText("MinBox", "1");
            //fieldLength.WaitForWorkAround(1000);

            executionLog.Log("AdminFieldLengthValidation", " Type 5 ");
            fieldLength.TypeText("MaxBox", "5");
            //fieldLength.WaitForWorkAround(1000);

            executionLog.Log("AdminFieldLengthValidation", "Click Save Field Manager Element");
            fieldLength.ClickElement("SaveFieldManager");
            //fieldLength.WaitForWorkAround(1000);

            executionLog.Log("AdminFieldLengthValidation", "WaitForWorkAround");
            fieldLength.WaitForWorkAround(3000);

            // visiting office main 
            executionLog.Log("AdminFieldLengthValidation", "Visit main office ");
            VisitOffice("clients");
            fieldLength.WaitForWorkAround(3000);

            executionLog.Log("AdminFieldLengthValidation", "Click First Client Element");
            fieldLength.ClickElement("FirstClient");
            fieldLength.WaitForWorkAround(3000);

            executionLog.Log("AdminFieldLengthValidation", "Hover over CompanyDetails");
            fieldLength.MouseHover("CompanyDetails");
            //fieldLength.WaitForWorkAround(3000);

            executionLog.Log("AdminFieldLengthValidation", "Click Company Details Element");
            fieldLength.ClickElement("CompanyDetails");
            fieldLength.WaitForWorkAround(3000);

            executionLog.Log("AdminFieldLengthValidation", "Type City Goes Here");
            fieldLength.TypeText("CityField", "CityGoesHere");
            //fieldLength.WaitForWorkAround(3000);

            executionLog.Log("AdminFieldLengthValidation", "Verify CityG");
            fieldLength.TypeText("CityField", "CityG");
            //fieldLength.WaitForWorkAround(3000);

            // Going to admin office 
            //executionLog.Log("AdminFieldLengthValidation", "Visit admin office ");
            //VisitOffice("admin");

            // Accessing field in field dictionary
            VisitOffice("fields");
            fieldLength.WaitForWorkAround(3000);

            fieldLength.SelectByText("Module", "Clients");
            fieldLength.WaitForWorkAround(1000);

            executionLog.Log("AdminFieldLengthValidation", "Click Search Element");
            fieldLength.ClickElement("Search");
            fieldLength.WaitForWorkAround(3000);

            executionLog.Log("AdminFieldLengthValidation", "Click filter Element");
            fieldLength.ClickElement("filter");

            executionLog.Log("AdminFieldLengthValidation", " type city ");
            fieldLength.TypeText("filter", "city");
            //fieldLength.WaitForWorkAround(3000);

            executionLog.Log("AdminFieldLengthValidation", "Click City Element");
            fieldLength.ClickJs("City");
            fieldLength.WaitForWorkAround(3000);

            // restoring to original setting 
            executionLog.Log("AdminFieldLengthValidation", "Click Field Length CheckBox Element");
            fieldLength.ClickElement("FieldLengthCheckBox");
            //fieldLength.WaitForWorkAround(3000);

            executionLog.Log("AdminFieldLengthValidation", "Wait For Work Around");
            //fieldLength.WaitForWorkAround(3000);

            executionLog.Log("AdminFieldLengthValidation", "Click Save Field Manager Element");
            fieldLength.ClickElement("SaveFieldManager");
            fieldLength.WaitForWorkAround(3000);

        }
           catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("AdminFieldLengthValidation");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("AdminFieldLengthValidation");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("AdminFieldLengthValidation", "Bug", "Medium", "Calls page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("AdminFieldLengthValidation");
                        TakeScreenshot("AdminFieldLengthValidation");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AdminFieldLengthValidation.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("AdminFieldLengthValidation");
                        string id = loginHelper.getIssueID("Admin Field Length Validation");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AdminFieldLengthValidation.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Admin Field Length Validation"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Admin Field Length Validation");
           //     executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("AdminFieldLengthValidation");
                executionLog.WriteInExcel("Admin Field Length Validation", Status, JIRA, "Office");
            }
        }
    }
} 
