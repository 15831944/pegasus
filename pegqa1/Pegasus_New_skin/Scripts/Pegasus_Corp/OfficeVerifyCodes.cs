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
    public class OfficeVerifyCodes : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Corp")]
        [TestCategory("TS8")]
        [TestCategory("Pegasus_Corp")]
        public void officeVerifyCodes()

        {
            string[] username1 = null;
            string[] password1 = null;

            XMLParse oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username1 = oXMLData.getData("settings/Credentials", "username_corp");
            password1 = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var corpOffice_OfficeHelper = new CorpOffice_OfficeHelper(GetWebDriver());

            // Variable random
            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("OfficeVerifyCodes", "Login with valid credential  Username");
                Login(username1[0], password1[0]);

                executionLog.Log("OfficeVerifyCodes", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("OfficeVerifyCodes", "Go to Office page");
                VisitCorp("offices");
                corpOffice_OfficeHelper.WaitForWorkAround(3000);

                executionLog.Log("OfficeVerifyCodes", "Verify Page title");
                VerifyTitle("Offices");

                executionLog.Log("OfficeVerifyCodes", "Enter office name in text box.");
                corpOffice_OfficeHelper.TypeText("EnterSelenium", "newthemeoffice");
                corpOffice_OfficeHelper.WaitForWorkAround(2000);

                executionLog.Log("OfficeVerifyCodes", "Verify code");
                corpOffice_OfficeHelper.VerifyText("VerifyCode", "5021");

                executionLog.Log("OfficeVerifyCodes", "Enter Sel Name");
                corpOffice_OfficeHelper.TypeText("EnterSelenium", "test578");
                corpOffice_OfficeHelper.WaitForWorkAround(2000);

                executionLog.Log("OfficeVerifyCodes", "Verify code");
                corpOffice_OfficeHelper.VerifyText("VerifyCode", "12345");

                executionLog.Log("OfficeVerifyCodes", "Enter Sel Name");
                corpOffice_OfficeHelper.TypeText("EnterSelenium", "Office90335");
                corpOffice_OfficeHelper.WaitForWorkAround(2000);

                executionLog.Log("OfficeVerifyCodes", "Verify code");
                corpOffice_OfficeHelper.VerifyText("VerifyCode", "123456");

            }

            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("OfficeVerifyCodes");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Office Verify Codes");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Office Verify Codes", "Bug", "Medium", "Office code page", "QA", "Log in as: " + username1[0] + " / " + password1[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Office Verify Codes");
                        TakeScreenshot("OfficeVerifyCodes");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\OfficeVerifyCodes.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("OfficeVerifyCodes");
                        string id = loginHelper.getIssueID("Office Verify Codes");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\OfficeVerifyCodes.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Office Verify Codes"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Office Verify Codes");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("OfficeVerifyCodes");
                executionLog.WriteInExcel("Office Verify Codes", Status, JIRA, "Corp Office");
            }
        }
    }
}


