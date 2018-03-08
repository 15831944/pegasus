/* Documented by Khalil Shakir
* 
* The JTable_Common_Layout_Corp script test the Pegasus Corp Portal.
* It views the JTables and checkes each for a set of elements stored in 
* CorpCommonLayout.xml file. If all elements are present, it should return a pass. 
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

namespace Pegasus_New_skin.Scripts.Pegasus_Corp
{
    // Creating Test Class
    [TestClass]
    public class JTable_Common_Layout_Corp : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Corp")]
        [TestCategory("TS7")]
        [TestCategory("Pegasus_Corp")]
        // Creating the Test Method
        public void checkCorpLayout()
        {
            string[] username = null;
            string[] password = null;

            //Connecting XML Documents for login

            XMLParse oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_corp");
            password = oXMLData.getData("settings/Credentials", "password");


            //Initializing the objects for login as well as JIRA and the test


            LoginHelper loginHelper = new LoginHelper(GetWebDriver());
            ExecutionLog executionLog = new ExecutionLog();
            CorpCommonLayoutHelper commonLayoutHelper = new CorpCommonLayoutHelper(GetWebDriver());

            // Testing Variables and variables for JIRA entry in case of error
            var name = "Testing Subject" + GetRandomNumber();
            var email = "Test" + GetRandomNumber() + "@gmail.com";
            String JIRA = "";
            String Status = "Pass";

            String[] officesArray =        {"offices","allusers", "office_codes"
            };

            String[] residualIncomeArray = { "rir/office_payout_summary", "rir/detailed_payouts", "rir/masterrules" };

            String[] masterDataArray =     {"masterdata/rates_fees", "masterdata/amex_rates", "masterdata/merchant_types",
                                            "masterdata/pricing_plans","masterdata/processor_types","masterdata/omaha_auth_grids",
                                            "masterdata/users_limit","languages"
            };

            String[] systemArray =         {"avatars", "modules", "themes", "pick-lists", "email_templates",
                                            "audit-trails", "permissions"
            };
            // Openning try catch block to handle any error 
            try
            {

                //Beginning Test
                executionLog.Log(" JTable_Common_Layout_Corp", "Entering username and password");
                Login(username[0], password[0]);

                // Test Employee Tab
                executionLog.Log(" JTable_Common_Layout_Corp", "Wait");
                //commonLayoutHelper.WaitForWorkAround(3000);

                executionLog.Log(" JTable_Common_Layout_Corp", "Visiting employees section");
                VisitCorp("employees");

                executionLog.Log(" JTable_Common_Layout_Corp", "Wait");
                commonLayoutHelper.WaitForWorkAround(3000);

                executionLog.Log(" JTable_Common_Layout_Corp", "Checking for common layout");
                commonLayoutHelper.checkIfCommon();

                // Test Offices Tab
                executionLog.Log(" JTable_Common_Layout_Corp", " Testing Offices Section ");
                for (int i = 0; i < officesArray.Length - 1; i++)
                {
                    executionLog.Log(" JTable_Common_Layout_Corp", ("Testing" + officesArray[i]));
                    VisitCorp(officesArray[i]);
                    executionLog.Log(" JTable_Common_Layout_Corp", "Checking for common layout");
                    commonLayoutHelper.checkIfCommon();
                }

                // Test Residuals Tab
                executionLog.Log(" JTable_Common_Layout_Corp", " Testing Residuals Section ");

                for (int i = 0; i < residualIncomeArray.Length - 1; i++)
                {
                    executionLog.Log(" JTable_Common_Layout_Corp", ("Testing" + residualIncomeArray[i]));
                    VisitCorp(residualIncomeArray[i]);

                    executionLog.Log(" JTable_Common_Layout_Corp", ("Checking if " + residualIncomeArray[i]) + " is common.");
                    executionLog.Log(" JTable_Common_Layout_Corp", "Checking for common layout");
                    commonLayoutHelper.checkIfCommon();
                }

                // Test Mercahnts Tab
                executionLog.Log(" JTable_Common_Layout_Corp", " Testing Merchants Section ");

                executionLog.Log(" JTable_Common_Layout_Corp", " Visit Merchants Section");
                VisitCorp("merchants");

                executionLog.Log(" JTable_Common_Layout_Corp", "Checking if Merchant Tab is common.");
                commonLayoutHelper.checkIfCommon();


                // Test Master Data Tab
                executionLog.Log(" JTable_Common_Layout_Corp", " Testing Master Data ");

                for (int i = 0; i < masterDataArray.Length - 1; i++)
                {
                    executionLog.Log(" JTable_Common_Layout_Corp", ("Visiting " + masterDataArray[i]) + " section.");
                    VisitCorp(masterDataArray[i]);

                    executionLog.Log(" JTable_Common_Layout_Corp", ("Checking if " + masterDataArray[i]) + " is common.");
                    commonLayoutHelper.checkIfCommon();
                }

                // Test Systems Tab 
                executionLog.Log(" JTable_Common_Layout_Corp", " Testing System Section ");
                for (int i = 0; i < systemArray.Length - 1; i++)
                {
                    executionLog.Log("Visiting", (systemArray[i] + " Secton"));
                    VisitCorp(systemArray[i]);

                    executionLog.Log(" JTable_Common_Layout_Corp", ("Checking if " + systemArray[i] + " is common."));
                    commonLayoutHelper.checkIfCommon();
                }

                // Test PDF Tab
                executionLog.Log(" JTable_Common_Layout_Corp", " Testing PDF Tempalte Section ");
                VisitCorp("pdf_templates");

                executionLog.Log(" JTable_Common_Layout_Corp", "Checking if PDF Tab layout  is common");
                commonLayoutHelper.checkIfCommon();


                // Test Iframe Tab
                executionLog.Log(" JTable_Common_Layout_Corp", " Testing Iframe Section ");
                VisitCorp("iframes");

                executionLog.Log(" JTable_Common_Layout_Corp", "Checking if iFrame is common");
                commonLayoutHelper.checkIfCommon();

            }

            // Beginning Aslam's Code to catach errors and log into JIRA 
            catch (Exception e)
            {
                Console.WriteLine("ERRROROOR");
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("Jtable_Commom_Layout_Admin");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Jtable Common Layout OfficeMain");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Jtable Common Layout OfficeMain", "Bug", "Medium", "Document page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Jtable Common Layout OfficeMain");
                        TakeScreenshot("Jtable_Commom_Layout_Admin");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\Jtable_Commom_Layout_Admin.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("Jtable_Commom_Layout_Admin");
                        string id = loginHelper.getIssueID("Jtable Common Layout OfficeMain");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\Jtable_Commom_Layout_Admin.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Jtable Common Layout OfficeMain"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Jtable Common Layout OfficeMain");
               // executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("Jtable_Commom_Layout_Admin");
                executionLog.WriteInExcel("Jtable Common Layout OfficeMain", Status, JIRA, "Activities");
            }
        }
        // Ending Aslams code for JIRA 



    }
}
