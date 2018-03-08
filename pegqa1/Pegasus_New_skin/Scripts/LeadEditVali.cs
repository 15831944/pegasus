using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;
namespace PegasusTests.Scripts
{
    [TestClass]
    public class LeadEditVali : DriverTestCase
    {
        //Go to Field properties and choose test field Company Legal Name

        void goToFP(LeadEditValiHelper helper, ExecutionLog log)
        {
            log.Log("LeadEditVali", "Go to field properity");
            VisitOffice("fields");
            helper.WaitForWorkAround(1000);
            log.Log("LeadEditVali", "Select module");
            helper.Select("Module", "14");
            helper.WaitForWorkAround(500);
            log.Log("LeadEditVali", "Select Processor");
            helper.Select("Processor", "3291");
            helper.WaitForWorkAround(500);
            log.Log("LeadEditVali", "Select Tab");
            helper.Select("Tab", "14879");
            helper.WaitForWorkAround(500);
            helper.ClickElement("Search");
            log.Log("LeadEditVali", "Select company legal name");
            helper.WaitForWorkAround(1000);
            helper.ClickElement("LeadLegalName");
        }
        //Go to testing lead
        void goToLead(LeadEditValiHelper helper, ExecutionLog log)
        {
            VisitOffice("leads");
            helper.WaitForWorkAround(1000);
            helper.TypeText("CompanySearch", "LeadValiTest");
            helper.ClickElement("LeadValiTest");
            helper.ClickElement("CompanyDetails");
            helper.WaitForWorkAround(3000);
        }
        //clear all current validations
        void clearAllChoice(LeadEditValiHelper helper, ExecutionLog log)
        {
            log.Log("LeadEditVali", "clear mandatory");
            bool mandatory = helper.checkSelected("Mandatory");
            if (mandatory)
            {
                helper.ClickElement("Mandatory");
            }
            log.Log("LeadEditVali", "clear datatype");
            bool dataType = helper.checkSelected("DataTypeVal");
            if (dataType)
            {
                helper.ClickElement("DataTypeVal");
            }
            log.Log("LeadEditVali", "clear fieldlength");
            bool length = helper.checkSelected("FieldLength");
            if (length)
            {
                helper.ClickElement("FieldLength");
            }
            log.Log("LeadEditVali", "clear field format");
            bool fieldFormat = helper.checkSelected("FieldFormat");
            if (fieldFormat)
            {
                helper.ClickElement("FieldFormat");
            }

        }

        //Final step
        void cleanVali(LeadEditValiHelper helper, ExecutionLog log)
        {
            goToFP(helper, log);
            log.Log("LeadEditVali", "cleanVali:clear all checkbox");
            clearAllChoice(helper, log);
            helper.ClickElement("SaveVali");
        }

        void checkMandatory(LeadEditValiHelper helper, ExecutionLog log)
        {
            log.Log("LeadEditVali", "check validation:Mandatory");
            goToFP(helper,log);
            log.Log("LeadEditVali", "check mandatory");
            clearAllChoice(helper, log);
            helper.ClickElement("Mandatory");
            log.Log("LeadEditVali", "check field length");
            helper.ClickElement("FieldLength");
            helper.TypeText("LengthMin", "5");
            helper.TypeText("LengthMax", "10");
            log.Log("LeadEditVali", "save validation");
            helper.ClickElement("SaveVali");
            log.Log("LeadEditVali", "go to client");

            goToLead(helper, log);
            log.Log("LeadEditVali", "mandatory check:no input");
            helper.ClearText("LegalNameInput");
            helper.ClickElement("Save");
            helper.WaitForWorkAround(500);
            helper.verifyElementPresent("ErrorMes");
            log.Log("LeadEditVali", "mandatory no input pass");
            log.Log("LeadEditVali", "field length: check short input");
            helper.ClearText("LegalNameInput");
            helper.TypeText("LegalNameInput", "55");
            helper.ClickElement("Save");

            helper.verifyElementPresent("ErrorMes");
            log.Log("LeadEditVali", "short input pass");
            log.Log("LeadEditVali", "field length: check long input");
            helper.ClearText("LegalNameInput");
            helper.TypeText("LegalNameInput", "jjiiawethfjiwe");
            helper.ClickElement("Save");
            string nameinput = helper.GetTextContent("LegalNameInput");
            Assert.IsTrue(nameinput.Length == 10);
            log.Log("LeadEditVali", "field length: long input pass");
        }
        //change data type validation
        void changeDataType(LeadEditValiHelper helper, string dataType, ExecutionLog log)
        {
            goToFP(helper, log);
            log.Log("LeadEditVali", "clear all checkbox");
            clearAllChoice(helper, log);
            log.Log("LeadEditVali", "check cooresponding datatype");
            helper.ClickElement("DataTypeVal");
            helper.Select("DataTypeBox", dataType);

        }
        //change data format validation
        void changeDataFormat(LeadEditValiHelper helper, string dataFormat, ExecutionLog log)
        {
            goToFP(helper, log);
            log.Log("LeadEditVali", "clear all checkbox");
            clearAllChoice(helper, log);
            log.Log("LeadEditVali", "check cooresponding data format");
            helper.ClickElement("FieldFormat");
            helper.Select("FieldFormatBox", dataFormat);
            helper.ClickElement("SaveVali");
        }
        void checkDataTypeandFormat(LeadEditValiHelper helper, ExecutionLog log)
        {
            log.Log("LeadEditVali", "check datatype:Numeric");
            changeDataType(helper, "numeric",log);
            log.Log("LeadEditVali", "check datatype:Numeric data range");
            helper.TypeText("LengthMin", "5");
            helper.TypeText("LengthMax", "10");
            helper.ClickElement("SaveVali");
            log.Log("LeadEditVali", "check datatype:Numeric go to client");
            goToLead(helper, log);
            log.Log("LeadEditVali", "check datatype:Numeric clear legal name text box");
            helper.ClearText("LegalNameInput");
            log.Log("LeadEditVali", "check datatype:Numeric illegal input string");
            helper.TypeText("LegalNameInput", "fdsf");
            helper.ClickElement("Save");
            helper.verifyElementPresent("ErrorMes");
            log.Log("LeadEditVali", "check datatype:Numeric illegal input string pass");
            helper.ClearText("LegalNameInput");
            log.Log("LeadEditVali", "check datatype:Numeric small input");
            helper.TypeText("LegalNameInput", "1");
            helper.ClickElement("Save");
            helper.verifyElementPresent("ErrorMes");
            log.Log("LeadEditVali", "check datatype:Numeric small input pass");
            helper.ClearText("LegalNameInput");
            log.Log("LeadEditVali", "check datatype:Numeric large input");
            helper.TypeText("LegalNameInput", "12");
            helper.ClickElement("Save");
            helper.verifyElementPresent("ErrorMes");
            log.Log("LeadEditVali", "check datatype:Numeric large input pass");
            helper.ClearText("LegalNameInput");
            log.Log("LeadEditVali", "check datatype:Numeric illegal input decimal");
            helper.TypeText("LegalNameInput", "6.5");
            helper.ClickElement("Save");
            helper.verifyElementPresent("ErrorMes");
            log.Log("LeadEditVali", "check datatype:Numeric illegal input decimal pass");
            helper.ClearText("LegalNameInput");
            log.Log("LeadEditVali", "check datatype:Numeric legal input");
            helper.TypeText("LegalNameInput", "6");
            helper.ClickElement("Save");
            string nameinput = helper.GetTextContent("LegalNameInput");
            Assert.IsTrue(nameinput == "6");
            log.Log("LeadEditVali", "check datatype:Numeric legal input pass");

            log.Log("LeadEditVali", "check datatype:Decimal");
            changeDataType(helper, "decimal", log);
            log.Log("LeadEditVali", "check datatype:Decimal range");
            helper.TypeText("LengthMin", "5");
            helper.TypeText("LengthMax", "10");
            helper.ClickElement("SaveVali");
            log.Log("LeadEditVali", "check datatype:Decimal go to client");
            goToLead(helper, log);
            log.Log("LeadEditVali", "check datatype:Decimal clear legal name text box");
            helper.ClearText("LegalNameInput");
            log.Log("LeadEditVali", "check datatype:Decimal illegal input string");
            helper.TypeText("LegalNameInput", "fdsf");
            helper.ClickElement("Save");
            helper.verifyElementPresent("ErrorMes");
            log.Log("LeadEditVali", "check datatype:Decimal illegal input string pass");
            log.Log("LeadEditVali", "check datatype:Decimal clear input box");
            helper.ClearText("LegalNameInput");
            log.Log("LeadEditVali", "check datatype:Decimal small input");
            helper.TypeText("LegalNameInput", "1.5");
            helper.ClickElement("Save");
            helper.verifyElementPresent("ErrorMes");
            log.Log("LeadEditVali", "check datatype:Decimal small input pass");
            helper.ClearText("LegalNameInput");
            log.Log("LeadEditVali", "check datatype:Decimal large input");
            helper.TypeText("LegalNameInput", "12.5");
            helper.ClickElement("Save");
            helper.verifyElementPresent("ErrorMes");
            log.Log("LeadEditVali", "check datatype:Decimal large input pass");
            helper.ClearText("LegalNameInput");
            log.Log("LeadEditVali", "check datatype:Decimal legal input");
            helper.TypeText("LegalNameInput", "6.5");
            helper.ClickElement("Save");
            nameinput = helper.GetTextContent("LegalNameInput");
            Assert.IsTrue(nameinput == "6.5");
            log.Log("LeadEditVali", "check datatype:Decimal legal input pass");

            log.Log("LeadEditVali", "check format:Email");
            changeDataFormat(helper, "email", log);
            log.Log("LeadEditVali", "check format:Email go to client");
            goToLead(helper, log);
            helper.ClearText("LegalNameInput");
            log.Log("LeadEditVali", "check format:Email illegal input");
            helper.TypeText("LegalNameInput", "gg");
            helper.ClickElement("Save");
            helper.verifyElementPresent("ErrorMes");
            log.Log("LeadEditVali", "check format:Email illegal input pass");
            helper.ClearText("LegalNameInput");
            log.Log("LeadEditVali", "check format:Email legal input");
            helper.TypeText("LegalNameInput", "gg@hh.com");
            helper.ClickElement("Save");
            nameinput = helper.GetTextContent("LegalNameInput");
            Assert.IsTrue(nameinput == "gg@hh.com");
            log.Log("LeadEditVali", "check format:Email legal input pass");

            log.Log("LeadEditVali", "check format:ssn");
            changeDataFormat(helper, "ssn", log);
            log.Log("LeadEditVali", "check format:ssn go to client");
            goToLead(helper, log);
            helper.ClearText("LegalNameInput");
            log.Log("LeadEditVali", "check format:ssn illegal input string");
            helper.TypeText("LegalNameInput", "gg");
            helper.ClickElement("Save");
            nameinput = helper.GetTextContent("LegalNameInput");
            Assert.IsTrue(nameinput.Length == 0);
            log.Log("LeadEditVali", "check format:ssn illegal input string pass");
            helper.ClearText("LegalNameInput");
            log.Log("LeadEditVali", "check format:ssn short input");
            helper.TypeText("LegalNameInput", "44");
            helper.ClickElement("Save");
            nameinput = helper.GetTextContent("LegalNameInput");
            Assert.IsTrue(nameinput.Length == 0);
            log.Log("LeadEditVali", "check format:ssn short input pass");
            helper.ClearText("LegalNameInput");
            log.Log("LeadEditVali", "check format:ssn long input");
            helper.TypeText("LegalNameInput", "543737345245273762");
            helper.ClickElement("Save");
            nameinput = helper.GetTextContent("LegalNameInput");
            Assert.IsTrue(nameinput.Length == 11);
            log.Log("LeadEditVali", "check format:ssn long input pass");

            log.Log("LeadEditVali", "check format:Phone");
            changeDataFormat(helper, "phone", log);
            log.Log("LeadEditVali", "check format:Phone go to client");
            goToLead(helper, log);
            helper.ClearText("LegalNameInput");
            log.Log("LeadEditVali", "check format:Phone inllegal input string");
            helper.TypeText("LegalNameInput", "gg");
            helper.ClickElement("Save");

           nameinput = helper.GetTextContent("LegalNameInput");
            Assert.IsTrue(nameinput.Length == 0);
            log.Log("LeadEditVali", "check format:Phone illegal input string pass");

            helper.ClearText("LegalNameInput");
            log.Log("LeadEditVali", "check format:Phone short");
            helper.TypeText("LegalNameInput", "44");
            helper.ClickElement("Save");
            nameinput = helper.GetTextContent("LegalNameInput");
            Assert.IsTrue(nameinput.Length == 0);
            log.Log("LeadEditVali", "check format:Phone short pass");
            helper.ClearText("LegalNameInput");
            log.Log("LeadEditVali", "check format:Phone long");
            helper.TypeText("LegalNameInput", "543737345245273762");
            helper.ClickElement("Save");
            nameinput = helper.GetTextContent("LegalNameInput");
            Assert.IsTrue(nameinput.Length == 14);
            log.Log("LeadEditVali", "check format:Phone long pass");


            log.Log("LeadEditVali", "check format:TaxID");
            changeDataFormat(helper, "taxid", log);
            goToLead(helper, log);
            helper.ClearText("LegalNameInput");
            log.Log("LeadEditVali", "check format:TaxID illegal input string");
            helper.TypeText("LegalNameInput", "gg");
            helper.ClickElement("Save");
            nameinput = helper.GetTextContent("LegalNameInput");
            Assert.IsTrue(nameinput.Length == 0);
            log.Log("LeadEditVali", "check format:TaxID illegal input string pass");
            helper.ClearText("LegalNameInput");
            log.Log("LeadEditVali", "check format:TaxID short");
            helper.TypeText("LegalNameInput", "44");
            helper.ClickElement("Save");
            nameinput = helper.GetTextContent("LegalNameInput");
            Assert.IsTrue(nameinput.Length == 0);
            log.Log("LeadEditVali", "check format:TaxID short pass");
            helper.ClearText("LegalNameInput");
            log.Log("LeadEditVali", "check format:TaxID long");
            helper.TypeText("LegalNameInput", "543737345245273762");
            helper.ClickElement("Save");
            nameinput = helper.GetTextContent("LegalNameInput");
            Assert.IsTrue(nameinput.Length == 10);
            log.Log("LeadEditVali", "check format:TaxID long pass");

        }
        [TestMethod]
        public void LeadVali()
        {
            string[] username1 = null;
            string[] password1 = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username1 = oXMLData.getData("settings/Credentials", "username");
            password1 = oXMLData.getData("settings/Credentials", "password");

            //Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var leadEditValiHelper = new LeadEditValiHelper(GetWebDriver());

            var username = "TESTUSER" + GetRandomNumber();
            var email = "Test" + GetRandomNumber() + "@gmail.com";
            String JIRA = "";
            String Status = "Pass";
            //try
            //{
            executionLog.Log("LeadEditVali", "Login with valid username and password");
            Login(username1[0], password1[0]);
            leadEditValiHelper.WaitForWorkAround(3000);
            executionLog.Log("LeadEditVali", "gotofieldManagement");

            checkMandatory(leadEditValiHelper, executionLog);
            checkDataTypeandFormat(leadEditValiHelper, executionLog);






            // }
            //     catch (Exception e)
            //    {
            //        Console.WriteLine("ERRROROOR");
            //        executionLog.Log("Error", e.StackTrace);
            //        Status = "Fail";

            //        String counter = executionLog.readLastLine("counter");
            //        String Description = executionLog.GetAllTextFile("LeadEditVali");
            //        String Error = executionLog.GetAllTextFile("Error");
            //        if (counter == "")
            //        {
            //            counter = "0";
            //        }
            //        bool result = loginHelper.CheckExstingIssue("LeadEditVali");
            //        if (!result)
            //        {
            //            if (Int16.Parse(counter) < 5)
            //            {
            //                executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
            //                loginHelper.CreateIssue("LeadEditVali", "Bug", "Medium", "Pricing page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
            //                string id = loginHelper.getIssueID("LeadEditVali");
            //                TakeScreenshot("LeadEditVali");
            //                string directoryName = loginHelper.GetnewDirectoryName(GetPath());
            //                var location = directoryName + "\\Iframe.png";
            //                loginHelper.AddAttachment(location, id);
            //            }
            //        }
            //        else
            //        {
            //            if (Int16.Parse(counter) < 5)
            //            {
            //                executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
            //                TakeScreenshot("OfficeFieldDictionary");
            //                string id = loginHelper.getIssueID("LeadEditVali");
            //                string directoryName = loginHelper.GetnewDirectoryName(GetPath());
            //                var location = directoryName + "\\LeadEditVali.png";
            //                loginHelper.AddAttachment(location, id);
            //                loginHelper.AddComment(loginHelper.getIssueID("LeadEditVali"), "This issue is still occurring");
            //            }
            //        }
            //        JIRA = loginHelper.getIssueID("LeadEditVali");
            //        executionLog.DeleteFile("Error");
            //        throw;

            //    }
            //    finally
            //    {
            //        executionLog.DeleteFile("LeadEditVali");
            //        executionLog.WriteInExcel("LeadEditVali", Status, JIRA, "LeadEditVali");
            //    }
            //}
            //}
            //}      
        }

    }
}
