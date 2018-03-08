using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;
namespace PegasusTests.Scripts
{
    [TestClass]
    public class FieldValidationFrame : DriverTestCase
    {
        //Go to Field properties and choose test field Company Legal Name
        void goToFP(FieldValidationHelper helper, ExecutionLog log)
        {
            log.Log("FieldValidationFrame", "Go to field properity");
            VisitOffice("fields");
            helper.WaitForWorkAround(1000);
            log.Log("FieldValidationFrame", "Select module");
            helper.Select("Module", "20");
            helper.WaitForWorkAround(2000);
            log.Log("FieldValidationFrame", "Select Processor");
            helper.Select("Processor", "3291");
            helper.WaitForWorkAround(2000);
            log.Log("FieldValidationFrame", "Select Tab");
            helper.Select("Tab", "14864");
            helper.WaitForWorkAround(5000);
            helper.ClickElement("Search");
            log.Log("FieldValidationFrame", "Select company legal name");
            helper.WaitForWorkAround(1000);
            helper.ClickElement("ClientLegalName");
        }
        //Go to Testing Client
        void goToClient(FieldValidationHelper helper, ExecutionLog log)
        {
            VisitOffice("clients");
            helper.WaitForWorkAround(1000);
            helper.TypeText("CompanySearch", "Yang Test");
            helper.ClickElement("YangTest");
            helper.WaitForWorkAround(3000);
            helper.ClickElement("CompanyDetails");
            helper.WaitForWorkAround(3000);
        }
        //clear all current validations
        void clearAllChoice(FieldValidationHelper helper, ExecutionLog log)
        {
            log.Log("FieldValidationFrame", "clear mandatory");
            bool mandatory = helper.checkSelected("Mandatory");
            if (mandatory)
            {
                helper.ClickElement("Mandatory");
            }
            log.Log("FieldValidationFrame", "clear datatype");
            bool dataType = helper.checkSelected("DataTypeVal");
            if (dataType)
            {
                helper.ClickElement("DataTypeVal");
            }
            log.Log("FieldValidationFrame", "clear fieldlength");
            bool length = helper.checkSelected("FieldLength");
            if (length)
            {
                helper.ClickElement("FieldLength");
            }
            log.Log("FieldValidationFrame", "clear field format");
            bool fieldFormat = helper.checkSelected("FieldFormat");
            if (fieldFormat)
            {
                helper.ClickElement("FieldFormat");
            }

        }
        //Final step
        void cleanVali(FieldValidationHelper helper, ExecutionLog log)
        {
            goToFP(helper, log);
            log.Log("FieldValidationFrame", "cleanVali:clear all checkbox");
            clearAllChoice(helper, log);
            helper.ClickElement("SaveVali");
        }
        void checkMandatory(FieldValidationHelper helper, ExecutionLog log)
        {
            log.Log("FieldValidationFrame", "check validation:Mandatory");
            goToFP(helper, log);
            log.Log("FieldValidationFrame", "check mandatory");
            clearAllChoice(helper, log);
            helper.ClickElement("Mandatory");
            log.Log("FieldValidationFrame", "check field length");
            helper.ClickElement("FieldLength");
            helper.TypeText("LengthMin", "5");
            helper.TypeText("LengthMax", "10");
            log.Log("FieldValidationFrame", "save validation");
            helper.ClickElement("SaveVali");

            log.Log("FieldValidationFrame", "go to client");
            goToClient(helper, log);
            log.Log("FieldValidationFrame", "mandatory check:no input");
            helper.ClearText("LegalNameInput");
            helper.ClickElement("Save");
            helper.WaitForWorkAround(500);
            helper.verifyElementPresent("ErrorMes");
            log.Log("FieldValidationFrame", "mandatory no input pass");

            log.Log("FieldValidationFrame", "field length: check short input");
            helper.ClearText("LegalNameInput");
            helper.TypeText("LegalNameInput", "55");
            helper.ClickElement("Save");
            helper.verifyElementPresent("ErrorMes");
            log.Log("FieldValidationFrame", "short input pass");

            log.Log("FieldValidationFrame", "field length: check long input");
            helper.ClearText("LegalNameInput");
            helper.TypeText("LegalNameInput", "jjiiawethfjiwe");
            helper.ClickElement("Save");
            string nameinput = helper.GetTextContent("LegalNameInput");
            Assert.IsTrue(nameinput.Length == 10);
            log.Log("FieldValidationFrame", "field length: long input pass");
        }
        //change data type validation
        void changeDataType(FieldValidationHelper helper, string dataType, ExecutionLog log)
        {
            goToFP(helper, log);
            log.Log("FieldValidationFrame", "clear all checkbox");
            clearAllChoice(helper, log);
            log.Log("FieldValidationFrame", "check cooresponding datatype");
            helper.ClickElement("DataTypeVal");
            helper.Select("DataTypeBox", dataType);

        }
        //change data format validation
        void changeDataFormat(FieldValidationHelper helper, string dataFormat, ExecutionLog log)
        {
            goToFP(helper, log);
            log.Log("FieldValidationFrame", "clear all checkbox");
            clearAllChoice(helper, log);
            helper.WaitForWorkAround(5000);
            log.Log("FieldValidationFrame", "check cooresponding data format");
            helper.ClickElement("FieldFormat");
            helper.Select("FieldFormatBox", dataFormat);
            helper.ClickElement("SaveVali");
        }
        //check data type and data format
        void checkDataTypeandFormat(FieldValidationHelper helper, ExecutionLog log)
        {
            log.Log("FieldValidationFrame", "check datatype:Numeric");
            changeDataType(helper, "numeric", log);
            log.Log("FieldValidationFrame", "check datatype:Numeric data range");
            helper.TypeText("LengthMin", "5");
            helper.TypeText("LengthMax", "10");
            helper.ClickElement("SaveVali");
            log.Log("FieldValidationFrame", "check datatype:Numeric go to client");
            goToClient(helper, log);
            log.Log("FieldValidationFrame", "check datatype:Numeric clear legal name text box");
            helper.ClearText("LegalNameInput");
            log.Log("FieldValidationFrame", "check datatype:Numeric illegal input string");
            helper.TypeText("LegalNameInput", "fdsf");
            helper.ClickElement("Save");
            helper.verifyElementPresent("ErrorMes");
            log.Log("FieldValidationFrame", "check datatype:Numeric illegal input string pass");
            helper.ClearText("LegalNameInput");
            log.Log("FieldValidationFrame", "check datatype:Numeric small input");
            helper.TypeText("LegalNameInput", "1");
            helper.ClickElement("Save");
            helper.verifyElementPresent("ErrorMes");
            log.Log("FieldValidationFrame", "check datatype:Numeric small input pass");
            helper.ClearText("LegalNameInput");
            log.Log("FieldValidationFrame", "check datatype:Numeric large input");
            helper.TypeText("LegalNameInput", "12");
            helper.ClickElement("Save");
            helper.verifyElementPresent("ErrorMes");
            log.Log("FieldValidationFrame", "check datatype:Numeric large input pass");
            helper.ClearText("LegalNameInput");
            log.Log("FieldValidationFrame", "check datatype:Numeric illegal input decimal");
            helper.TypeText("LegalNameInput", "6.5");
            helper.ClickElement("Save");
            helper.verifyElementPresent("ErrorMes");
            log.Log("FieldValidationFrame", "check datatype:Numeric illegal input decimal pass");
            helper.ClearText("LegalNameInput");
            log.Log("FieldValidationFrame", "check datatype:Numeric legal input");
            helper.TypeText("LegalNameInput", "6");
            helper.ClickElement("Save");
            string nameinput = helper.GetTextContent("LegalNameInput");
            Assert.IsTrue(nameinput == "6");
            log.Log("FieldValidationFrame", "check datatype:Numeric legal input pass");

            log.Log("FieldValidationFrame", "check datatype:Decimal");
            changeDataType(helper, "decimal", log);
            log.Log("FieldValidationFrame", "check datatype:Decimal range");
            helper.TypeText("LengthMax", "10");
            helper.ClickElement("SaveVali");
            log.Log("FieldValidationFrame", "check datatype:Decimal go to client");
            goToClient(helper, log);
            log.Log("FieldValidationFrame", "check datatype:Decimal clear legal name text box");
            helper.ClearText("LegalNameInput");
            log.Log("FieldValidationFrame", "check datatype:Decimal illegal input string");
            helper.TypeText("LegalNameInput", "fdsf");
            helper.ClickElement("Save");
            helper.verifyElementPresent("ErrorMes");
            log.Log("FieldValidationFrame", "check datatype:Decimal illegal input string pass");
            log.Log("FieldValidationFrame", "check datatype:Decimal clear input box");
            helper.ClearText("LegalNameInput");
            log.Log("FieldValidationFrame", "check datatype:Decimal small input");
            helper.TypeText("LegalNameInput", "1.5");
            helper.ClickElement("Save");
            helper.verifyElementPresent("ErrorMes");
            log.Log("FieldValidationFrame", "check datatype:Decimal small input pass");
            helper.ClearText("LegalNameInput");
            log.Log("FieldValidationFrame", "check datatype:Decimal large input");
            helper.TypeText("LegalNameInput", "12.5");
            helper.ClickElement("Save");
            helper.verifyElementPresent("ErrorMes");
            log.Log("FieldValidationFrame", "check datatype:Decimal large input pass");
            helper.ClearText("LegalNameInput");
            log.Log("FieldValidationFrame", "check datatype:Decimal legal input");
            helper.TypeText("LegalNameInput", "6.5");
            helper.ClickElement("Save");
            nameinput = helper.GetTextContent("LegalNameInput");
            Assert.IsTrue(nameinput == "6.5");
            log.Log("FieldValidationFrame", "check datatype:Decimal legal input pass");

            log.Log("FieldValidationFrame", "check format:Email");
            changeDataFormat(helper, "email", log);
            log.Log("FieldValidationFrame", "check format:Email go to client");
            goToClient(helper, log);
            helper.ClearText("LegalNameInput");
            log.Log("FieldValidationFrame", "check format:Email illegal input");
            helper.TypeText("LegalNameInput", "gg");
            helper.ClickElement("Save");
            helper.verifyElementPresent("ErrorMes");
            log.Log("FieldValidationFrame", "check format:Email illegal input pass");
            helper.ClearText("LegalNameInput");
            log.Log("FieldValidationFrame", "check format:Email legal input");
            helper.TypeText("LegalNameInput", "gg@hh.com");
            helper.ClickElement("Save");
            nameinput = helper.GetTextContent("LegalNameInput");
            Assert.IsTrue(nameinput == "gg@hh.com");
            log.Log("FieldValidationFrame", "check format:Email legal input pass");

            log.Log("FieldValidationFrame", "check format:ssn");
            changeDataFormat(helper, "ssn", log);
            log.Log("FieldValidationFrame", "check format:ssn go to client");
            goToClient(helper, log);
            helper.ClearText("LegalNameInput");
            log.Log("FieldValidationFrame", "check format:ssn illegal input string");
            helper.TypeText("LegalNameInput", "gg");
            helper.ClickElement("Save");
            nameinput = helper.GetTextContent("LegalNameInput");
            Assert.IsTrue(nameinput.Length == 0);
            log.Log("FieldValidationFrame", "check format:ssn illegal input string pass");
            helper.ClearText("LegalNameInput");
            log.Log("FieldValidationFrame", "check format:ssn short input");
            helper.TypeText("LegalNameInput", "44");
            helper.ClickElement("Save");
            nameinput = helper.GetTextContent("LegalNameInput");
            Assert.IsTrue(nameinput.Length == 0);
            log.Log("FieldValidationFrame", "check format:ssn short input pass");
            helper.ClearText("LegalNameInput");
            log.Log("FieldValidationFrame", "check format:ssn long input");
            helper.TypeText("LegalNameInput", "543737345245273762");
            helper.ClickElement("Save");
            nameinput = helper.GetTextContent("LegalNameInput");
            Assert.IsTrue(nameinput.Length == 11);
            log.Log("FieldValidationFrame", "check format:ssn long input pass");

            log.Log("FieldValidationFrame", "check format:Phone");
            changeDataFormat(helper, "phone", log);
            log.Log("FieldValidationFrame", "check format:Phone go to client");
            goToClient(helper, log);
            helper.ClearText("LegalNameInput");
            log.Log("FieldValidationFrame", "check format:Phone inllegal input string");
            helper.TypeText("LegalNameInput", "gg");
            helper.ClickElement("Save");
            nameinput = helper.GetTextContent("LegalNameInput");
            Assert.IsTrue(nameinput.Length == 0);
            log.Log("FieldValidationFrame", "check format:Phone illegal input string pass");
            helper.ClearText("LegalNameInput");
            log.Log("FieldValidationFrame", "check format:Phone short");
            helper.TypeText("LegalNameInput", "44");
            helper.ClickElement("Save");
            nameinput = helper.GetTextContent("LegalNameInput");
            Assert.IsTrue(nameinput.Length == 0);
            log.Log("FieldValidationFrame", "check format:Phone short pass");
            helper.ClearText("LegalNameInput");
            log.Log("FieldValidationFrame", "check format:Phone long");
            helper.TypeText("LegalNameInput", "543737345245273762");
            helper.ClickElement("Save");
            nameinput = helper.GetTextContent("LegalNameInput");
            Assert.IsTrue(nameinput.Length == 14);
            log.Log("FieldValidationFrame", "check format:Phone long pass");


            log.Log("FieldValidationFrame", "check format:TaxID");
            changeDataFormat(helper, "taxid", log);
            goToClient(helper, log);
            helper.ClearText("LegalNameInput");
            log.Log("FieldValidationFrame", "check format:TaxID illegal input string");
            helper.TypeText("LegalNameInput", "gg");
            helper.ClickElement("Save");
            nameinput = helper.GetTextContent("LegalNameInput");
            Assert.IsTrue(nameinput.Length == 0);
            log.Log("FieldValidationFrame", "check format:TaxID illegal input string pass");
            helper.ClearText("LegalNameInput");
            log.Log("FieldValidationFrame", "check format:TaxID short");
            helper.TypeText("LegalNameInput", "44");
            helper.ClickElement("Save");
            nameinput = helper.GetTextContent("LegalNameInput");
            Assert.IsTrue(nameinput.Length == 0);
            log.Log("FieldValidationFrame", "check format:TaxID short pass");
            helper.ClearText("LegalNameInput");
            log.Log("FieldValidationFrame", "check format:TaxID long");
            helper.TypeText("LegalNameInput", "543737345245273762");
            helper.ClickElement("Save");
            nameinput = helper.GetTextContent("LegalNameInput");
            Assert.IsTrue(nameinput.Length == 10);
            log.Log("FieldValidationFrame", "check format:TaxID long pass");

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
            var fieldValidationHelper = new FieldValidationHelper(GetWebDriver());

            var username = "TESTUSER" + GetRandomNumber();
            var email = "Test" + GetRandomNumber() + "@gmail.com";
            String JIRA = "";
            String Status = "Pass";
            //try
            //{
            executionLog.Log("FieldValidationFrame", "Login with valid username and password");
            Login(username1[0], password1[0]);

            executionLog.Log("FieldValidationFrame", "check mandatory and length");
            loginHelper.WaitForWorkAround(5000);
            checkMandatory(fieldValidationHelper, executionLog);
            executionLog.Log("FieldValidationFrame", "check data type and data format");
            checkDataTypeandFormat(fieldValidationHelper, executionLog);
            // clean up
            cleanVali(fieldValidationHelper, executionLog);



        }
    }

             }
       /*          catch (Exception e)
                {
                    Console.WriteLine("ERRROROOR");
                    executionLog.Log("Error", e.StackTrace);
                    Status = "Fail";

                    String counter = executionLog.readLastLine("counter");
                    String Description = executionLog.GetAllTextFile("FieldValidationFrame");
                    String Error = executionLog.GetAllTextFile("Error");
                    if (counter == "")
                    {
                        counter = "0";
                    }
                    bool result = loginHelper.CheckExstingIssue("FieldValidationFrame");
                    if (!result)
                    {
                        if (Int16.Parse(counter) < 5)
                        {
                            executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                            loginHelper.CreateIssue("FieldValidationFrame", "Bug", "Medium", "Pricing page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                            string id = loginHelper.getIssueID("FieldValidationFrame");
                            TakeScreenshot("FieldValidationFrame");
                            string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                            var location = directoryName + "\\Iframe.png";
                            loginHelper.AddAttachment(location, id);
                        }
                    }
                    else
                    {
                        if (Int16.Parse(counter) < 5)
                        {
                            executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                            TakeScreenshot("OfficeFieldDictionary");
                            string id = loginHelper.getIssueID("FieldValidationFrame");
                            string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                            var location = directoryName + "\\FieldValidationFrame.png";
                            loginHelper.AddAttachment(location, id);
                            loginHelper.AddComment(loginHelper.getIssueID("FieldValidationFrame"), "This issue is still occurring");
                        }
                    }
                    JIRA = loginHelper.getIssueID("FieldValidationFrame");
                    executionLog.DeleteFile("Error");
                    throw;

                }
                finally
                {
                    executionLog.DeleteFile("FieldValidationFrame");
                    executionLog.WriteInExcel("FieldValidationFrame", Status, JIRA, "FieldValidationFrame");
                }
            }
            }
            }      
        }

    }
}
*/