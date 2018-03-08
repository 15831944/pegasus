using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;
namespace PegasusTests.Scripts
{
    [TestClass]
    public class ClientCreateVali : DriverTestCase
    {
        //Go to Field properties and choose test field Company Legal Name
        void goToFP(ClientCreateValiHelper helper, ExecutionLog log)
        {
            log.Log("ClientCreateVali", "Go to field properity");
            VisitOffice("fields");
            helper.WaitForWorkAround(1000);
            log.Log("ClientCreateVali", "Select module");
            helper.Select("Module", "20");
            helper.WaitForWorkAround(1500);
            log.Log("ClientCreateVali", "Select tab");
            helper.SelectByText("Tab", "Company Details");
            helper.WaitForWorkAround(500);
            helper.ClickElement("Search");
            log.Log("ClientCreateVali", "Select company legal name");
            helper.WaitForWorkAround(1000);
            //      helper.ClickElement("ClientLegalName");
            helper.ClickElement("BussinessLegalName");
        }
        //Go to create client
        void goToClient(ClientCreateValiHelper helper)
        {
            VisitOffice("clients/create");
            helper.WaitForWorkAround(3000);
            
        }
        //clear all current validations
        void clearAllChoice(ClientCreateValiHelper helper, ExecutionLog log)

        {
            log.Log("ClientCreateVali", "clear mandatory");
            bool mandatory = helper.checkSelected("Mandatory");
            if (mandatory)
            {
                helper.ClickElement("Mandatory");
            }
            log.Log("ClientCreateVali", "clear datatype");
            bool dataType = helper.checkSelected("DataTypeVal");
            if (dataType)
            {
                helper.ClickElement("DataTypeVal");
            }
            log.Log("ClientCreateVali", "clear fieldlength");
            bool length = helper.checkSelected("FieldLength");
            if (length)
            {
                helper.ClickElement("FieldLength");
            }
            log.Log("ClientCreateVali", "clear field format");
            bool fieldFormat = helper.checkSelected("FieldFormat");
            if (fieldFormat)
            {
                helper.ClickElement("FieldFormat");
            }

        }

        //Final step
        void cleanVali(ClientCreateValiHelper helper, ExecutionLog log)
        {
            goToFP(helper,log);
            log.Log("ClientCreateVali", "cleanVali:clear all checkbox");
            clearAllChoice(helper, log);
            helper.ClickElement("SaveVali");
        }
        // Check mandatory validation and field length validation
        void checkMandatory(ClientCreateValiHelper helper, ExecutionLog log)
        {
            goToFP(helper,log);
            clearAllChoice(helper, log);
            log.Log("ClientCreateVali", "check mandatory");
            helper.ClickElement("Mandatory");
            log.Log("ClientCreateVali", "check field length");
            helper.ClickElement("FieldLength");
            helper.TypeText("LengthMin", "5");
            helper.TypeText("LengthMax", "10");
            log.Log("ClientCreateVali", "save validation");
            helper.ClickElement("SaveVali");
            log.Log("ClientCreateVali", "go to client");
            goToClient(helper);
            log.Log("ClientCreateVali", "mandatory check:no input");
            helper.ClearText("LegalNameInput");
            helper.ClickElement("Save");
            helper.WaitForWorkAround(500);
            helper.verifyElementPresent("ErrorMes");
            log.Log("ClientCreateVali", "mandatory no input pass");
            log.Log("ClientCreateVali", "field length: check short input");
     
            helper.ClearText("LegalNameInput");
            helper.TypeText("LegalNameInput", "55");
            helper.ClickElement("Save");

            helper.verifyElementPresent("ErrorMes");
            log.Log("ClientCreateVali", "short input pass");
            log.Log("ClientCreateVali", "field length: check long input");
       
            helper.ClearText("LegalNameInput");
            helper.TypeText("LegalNameInput", "jjiiawethfjiwe");
            helper.ClickElement("Save");
            string nameinput = helper.GetTextContent("LegalNameInput");
            Assert.IsTrue(nameinput.Length == 10);
            log.Log("ClientCreateVali", "field length: long input pass");

        }
        //change data type validation
        void changeDataType(ClientCreateValiHelper helper, string dataType, ExecutionLog log)
        {
            goToFP(helper, log);
            log.Log("ClientCreateVali", "clear all checkbox");
            clearAllChoice(helper, log);
            log.Log("ClientCreateVali", "check cooresponding datatype");
            helper.ClickElement("DataTypeVal");
            helper.Select("DataTypeBox", dataType);

        }
        //change data format validation
        void changeDataFormat(ClientCreateValiHelper helper, string dataFormat, ExecutionLog log)
        {
            goToFP(helper, log);
            log.Log("ClientCreateVali", "clear all checkbox");
            clearAllChoice(helper, log);
            log.Log("ClientCreateVali", "check cooresponding data format");
            helper.ClickElement("FieldFormat");
            helper.WaitForElementPresent("FieldFormatBox" ,10);
         //   helper.WaitForWorkAround(2000);
            helper.Select("FieldFormatBox", dataFormat);
            helper.ClickElement("SaveVali");
        }
        //check data type and data format
        void checkDataTypeandFormat(ClientCreateValiHelper helper, ExecutionLog log)
        {
            log.Log("ClientCreateVali", "check datatype:Numeric");
            changeDataType(helper, "numeric", log);
            log.Log("ClientCreateVali", "check datatype:Numeric data range");

            helper.TypeText("LengthMin", "5");
            helper.TypeText("LengthMax", "10");
            helper.ClickElement("SaveVali");
            log.Log("ClientCreateVali", "check datatype:Numeric go to client");
            goToClient(helper);
            log.Log("ClientCreateVali", "check datatype:Numeric clear legal name text box");
            helper.ClearText("LegalNameInput");
            log.Log("ClientCreateVali", "check datatype:Numeric illegal input string");
            helper.TypeText("LegalNameInput", "fdsf");
            helper.ClickElement("Save");
            helper.verifyElementPresent("ErrorMes");
            log.Log("ClientCreateVali", "check datatype:Numeric illegal input string pass");
            helper.ClearText("LegalNameInput");
            log.Log("ClientCreateVali", "check datatype:Numeric small input");
            helper.TypeText("LegalNameInput", "1");
            helper.ClickElement("Save");
            helper.verifyElementPresent("ErrorMes");
            log.Log("ClientCreateVali", "check datatype:Numeric small input pass");
            helper.ClearText("LegalNameInput");
            log.Log("ClientCreateVali", "check datatype:Numeric large input");
            helper.TypeText("LegalNameInput", "12");
            helper.ClickElement("Save");
            helper.verifyElementPresent("ErrorMes");
            log.Log("ClientCreateVali", "check datatype:Numeric large input pass");
            helper.ClearText("LegalNameInput");
            log.Log("ClientCreateVali", "check datatype:Numeric illegal input decimal");
            helper.TypeText("LegalNameInput", "6.5");
            helper.ClickElement("Save");
            helper.verifyElementPresent("ErrorMes");
            log.Log("ClientCreateVali", "check datatype:Numeric illegal input decimal pass");
            helper.ClearText("LegalNameInput");
            log.Log("ClientCreateVali", "check datatype:Numeric legal input");
            helper.TypeText("LegalNameInput", "6");
            helper.ClickElement("Save");
            string nameinput = helper.GetTextContent("LegalNameInput");
            Assert.IsTrue(nameinput == "6");
            log.Log("ClientCreateVali", "check datatype:Numeric legal input pass");


            log.Log("ClientCreateVali", "check datatype:Decimal");
            changeDataType(helper, "decimal", log);
            log.Log("ClientCreateVali", "check datatype:Decimal range");
            helper.TypeText("LengthMin", "5");
            helper.TypeText("LengthMax", "10");
            helper.ClickElement("SaveVali");
            log.Log("ClientCreateVali", "check datatype:Decimal go to client");
            goToClient(helper);
            log.Log("ClientCreateVali", "check datatype:Decimal clear legal name text box");
            helper.ClearText("LegalNameInput");
            log.Log("ClientCreateVali", "check datatype:Decimal illegal input string");
            helper.TypeText("LegalNameInput", "fdsf");
            helper.ClickElement("Save");
            helper.verifyElementPresent("ErrorMes");
            log.Log("ClientCreateVali", "check datatype:Decimal illegal input string pass");
            log.Log("ClientCreateVali", "check datatype:Decimal clear input box");
            helper.ClearText("LegalNameInput");
            log.Log("ClientCreateVali", "check datatype:Decimal small input");
            helper.TypeText("LegalNameInput", "1.5");
            helper.ClickElement("Save");
            helper.verifyElementPresent("ErrorMes");
            log.Log("ClientCreateVali", "check datatype:Decimal small input pass");
            helper.ClearText("LegalNameInput");
            log.Log("ClientCreateVali", "check datatype:Decimal large input");
            helper.TypeText("LegalNameInput", "12.5");
            helper.ClickElement("Save");
            helper.verifyElementPresent("ErrorMes");
            log.Log("ClientCreateVali", "check datatype:Decimal large input pass");
            helper.ClearText("LegalNameInput");
            log.Log("ClientCreateVali", "check datatype:Decimal legal input");
            helper.TypeText("LegalNameInput", "6.5");
            helper.ClickElement("Save");
            nameinput = helper.GetTextContent("LegalNameInput");
            Assert.IsTrue(nameinput == "6.5");
            log.Log("ClientCreateVali", "check datatype:Decimal legal input pass");


            log.Log("ClientCreateVali", "check format:Email");
            changeDataFormat(helper, "email", log);
            log.Log("ClientCreateVali", "check format:Email go to client");
            goToClient(helper);
            helper.ClearText("LegalNameInput");
            log.Log("ClientCreateVali", "check format:Email illegal input");
            helper.TypeText("LegalNameInput", "gg");
            helper.ClickElement("Save");
            helper.verifyElementPresent("ErrorMes");
            log.Log("ClientCreateVali", "check format:Email illegal input pass");
            helper.ClearText("LegalNameInput");
            log.Log("ClientCreateVali", "check format:Email legal input");
            helper.TypeText("LegalNameInput", "gg@hh.com");
            helper.ClickElement("Save");
            nameinput = helper.GetTextContent("LegalNameInput");
            Assert.IsTrue(nameinput == "gg@hh.com");
            log.Log("ClientCreateVali", "check format:Email legal input pass");

            
            log.Log("ClientCreateVali", "check format:ssn");
            changeDataFormat(helper, "ssn", log);
            log.Log("ClientCreateVali", "check format:ssn go to client");
            goToClient(helper);
            helper.ClearText("LegalNameInput");
            log.Log("ClientCreateVali", "check format:ssn illegal input string");
            helper.TypeText("LegalNameInput", "gg");
            helper.ClickElement("Save");
            nameinput = helper.GetTextContent("LegalNameInput");
            Assert.IsTrue(nameinput.Length == 0);
            log.Log("ClientCreateVali", "check format:ssn illegal input string pass");
            helper.ClearText("LegalNameInput");
            log.Log("ClientCreateVali", "check format:ssn short input");
            helper.TypeText("LegalNameInput", "44");
            helper.ClickElement("Save");
            nameinput = helper.GetTextContent("LegalNameInput");
            Assert.IsTrue(nameinput.Length == 0);
            log.Log("ClientCreateVali", "check format:ssn short input pass");
            helper.ClearText("LegalNameInput");
            log.Log("ClientCreateVali", "check format:ssn long input");
            helper.TypeText("LegalNameInput", "543737345245273762");
            helper.ClickElement("Save");
            nameinput = helper.GetTextContent("LegalNameInput");
            Assert.IsTrue(nameinput.Length == 11);
            log.Log("ClientCreateVali", "check format:ssn long input pass");

            log.Log("ClientCreateVali", "check format:Phone");
            changeDataFormat(helper, "phone", log);
            log.Log("ClientCreateVali", "check format:Phone go to client");
            goToClient(helper);
            helper.ClearText("LegalNameInput");
            log.Log("ClientCreateVali", "check format:Phone inllegal input string");
            helper.TypeText("LegalNameInput", "gg");
            helper.ClickElement("Save");
            nameinput = helper.GetTextContent("LegalNameInput");
            Assert.IsTrue(nameinput.Length == 0);
            log.Log("ClientCreateVali", "check format:Phone illegal input string pass");
            helper.ClearText("LegalNameInput");
            log.Log("ClientCreateVali", "check format:Phone short"); 
            helper.TypeText("LegalNameInput", "44");
            helper.ClickElement("Save");
            nameinput = helper.GetTextContent("LegalNameInput");
            Assert.IsTrue(nameinput.Length == 0);
            log.Log("ClientCreateVali", "check format:Phone short pass");
            helper.ClearText("LegalNameInput");
            log.Log("ClientCreateVali", "check format:Phone long");
            helper.TypeText("LegalNameInput", "543737345245273762");
            helper.ClickElement("Save");
            nameinput = helper.GetTextContent("LegalNameInput");
            Assert.IsTrue(nameinput.Length == 14);
            log.Log("ClientCreateVali", "check format:Phone long pass");


            log.Log("ClientCreateVali", "check format:TaxID");
            changeDataFormat(helper, "taxid", log);
            goToClient(helper);
            helper.ClearText("LegalNameInput");
            log.Log("ClientCreateVali", "check format:TaxID illegal input string");
            helper.TypeText("LegalNameInput", "gg");
            helper.ClickElement("Save");
            nameinput = helper.GetTextContent("LegalNameInput");
            Assert.IsTrue(nameinput.Length == 0);
            log.Log("ClientCreateVali", "check format:TaxID illegal input string pass");
            helper.ClearText("LegalNameInput");
            log.Log("ClientCreateVali", "check format:TaxID short");
            helper.TypeText("LegalNameInput", "44");
            helper.ClickElement("Save");
            nameinput = helper.GetTextContent("LegalNameInput");
            Assert.IsTrue(nameinput.Length == 0);
            log.Log("ClientCreateVali", "check format:TaxID short pass");
            helper.ClearText("LegalNameInput");
            log.Log("ClientCreateVali", "check format:TaxID long");
            helper.TypeText("LegalNameInput", "543737345245273762");
            helper.ClickElement("Save");
            nameinput = helper.GetTextContent("LegalNameInput");
            Assert.IsTrue(nameinput.Length == 10);
            log.Log("ClientCreateVali", "check format:TaxID long pass");

        }
        [TestMethod]
        public void FileVali()
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
            var clientCreateValiHelper = new ClientCreateValiHelper(GetWebDriver());

            var username = "TESTUSER" + GetRandomNumber();
            var email = "Test" + GetRandomNumber() + "@gmail.com";
            String JIRA = "";
            String Status = "Pass";
            //try
            //{
            executionLog.Log("ClientCreateVali", "Login with valid username and password");
            Login(username1[0], password1[0]);
            clientCreateValiHelper.WaitForWorkAround(3000);
            executionLog.Log("ClientCreateVali", "check mandatory and length");

            checkMandatory(clientCreateValiHelper, executionLog);
            executionLog.Log("ClientCreateVali", "check data type and data format");
            checkDataTypeandFormat(clientCreateValiHelper, executionLog);
            // clean up
            cleanVali(clientCreateValiHelper, executionLog);





            // }
            //     catch (Exception e)
            //    {
            //        Console.WriteLine("ERRROROOR");
            //        executionLog.Log("Error", e.StackTrace);
            //        Status = "Fail";

            //        String counter = executionLog.readLastLine("counter");
            //        String Description = executionLog.GetAllTextFile("FieldValidationFrame");
            //        String Error = executionLog.GetAllTextFile("Error");
            //        if (counter == "")
            //        {
            //            counter = "0";
            //        }
            //        bool result = loginHelper.CheckExstingIssue("FieldValidationFrame");
            //        if (!result)
            //        {
            //            if (Int16.Parse(counter) < 5)
            //            {
            //                executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
            //                loginHelper.CreateIssue("FieldValidationFrame", "Bug", "Medium", "Pricing page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
            //                string id = loginHelper.getIssueID("FieldValidationFrame");
            //                TakeScreenshot("FieldValidationFrame");
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
            //                string id = loginHelper.getIssueID("FieldValidationFrame");
            //                string directoryName = loginHelper.GetnewDirectoryName(GetPath());
            //                var location = directoryName + "\\FieldValidationFrame.png";
            //                loginHelper.AddAttachment(location, id);
            //                loginHelper.AddComment(loginHelper.getIssueID("FieldValidationFrame"), "This issue is still occurring");
            //            }
            //        }
            //        JIRA = loginHelper.getIssueID("FieldValidationFrame");
            //        executionLog.DeleteFile("Error");
            //        throw;

            //    }
            //    finally
            //    {
            //        executionLog.DeleteFile("FieldValidationFrame");
            //        executionLog.WriteInExcel("FieldValidationFrame", Status, JIRA, "FieldValidationFrame");
            //    }
            //}
            //}
            //}      
        }

    }
}
