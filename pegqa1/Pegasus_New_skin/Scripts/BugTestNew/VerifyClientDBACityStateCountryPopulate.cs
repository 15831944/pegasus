using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class VerifyClientDBACityStateCountryPopulate : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin")]
        [TestCategory("Fail")]
        [TestCategory("BugTestNew")]
        public void verifyClientDBACityStateCountryPopulate()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var office_MerchantHelper = new Office_ClientsHelper(GetWebDriver());

            var DBA = "ClientDBA" + RandomNumber(1, 5000);
            var ticketname = "Testticket" + RandomNumber(1,500);

            // Variable random
            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("VerifyClientDBACityStateCountryPopulate", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: "+username[0]+" / "+password[0]);

                executionLog.Log("VerifyClientDBACityStateCountryPopulate", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("VerifyClientDBACityStateCountryPopulate", "Redirect to Create merchant page");
                VisitOffice("clients");
                office_MerchantHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyClientDBACityStateCountryPopulate", "Click on client");
                office_MerchantHelper.ClickElement("Client1");
                office_MerchantHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyClientDBACityStateCountryPopulate", "Go to Company Details tab");
                office_MerchantHelper.ClickElement("CompanyDetailsTab");
                office_MerchantHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyClientDBACityStateCountryPopulate", "Enter Zip Code in Mailing Address");
                office_MerchantHelper.TypeText("MailZipCode", "15001");
                office_MerchantHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyClientDBACityStateCountryPopulate", "Verify City Poulate");
                office_MerchantHelper.VerifyTextBoxValue("MailCity", "Aliquippa");

                executionLog.Log("VerifyClientDBACityStateCountryPopulate", "Verify County Poulate");
                office_MerchantHelper.VerifyTextBoxValue("MailCounty", "Beaver");

                executionLog.Log("VerifyClientDBACityStateCountryPopulate", "Verify Country Poulate");
                office_MerchantHelper.verifyselectedOptn("MailCountry", "United States");

                executionLog.Log("VerifyClientDBACityStateCountryPopulate", "Verify State Poulate");
                office_MerchantHelper.verifyselectedOptn("MailState", "PA");
                Console.WriteLine("City, County, Country and State populated successfully");

                executionLog.Log("VerifyClientDBACityStateCountryPopulate", "Enter Address Line 1 of Location Addess");
                office_MerchantHelper.TypeText("LocAddLine1", "Add Line 1");

                executionLog.Log("VerifyClientDBACityStateCountryPopulate", "Enter Address Line 2 of Location Addess");
                office_MerchantHelper.TypeText("LocAddLine2", "Add Line 2");

                executionLog.Log("VerifyClientDBACityStateCountryPopulate", "Enter Zip Code in Location Address");
                office_MerchantHelper.TypeText("LocZipCode", "20001");
                office_MerchantHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyClientDBACityStateCountryPopulate", "Select Same as Location Address check box");
                office_MerchantHelper.ClickElement("SameAsLocChkBox");
                office_MerchantHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyClientDBACityStateCountryPopulate", "Verify Address Line 1 Copied");
                office_MerchantHelper.VerifyTextBoxValue("MailAddLine1", "Add Line 1");

                executionLog.Log("VerifyClientDBACityStateCountryPopulate", "Verify Address Line 2 Copied");
                office_MerchantHelper.VerifyTextBoxValue("MailAddLine2", "Add Line 2");

                executionLog.Log("VerifyClientDBACityStateCountryPopulate", "Verify City Copied");
                office_MerchantHelper.VerifyTextBoxValue("MailCity", "Washington");

                executionLog.Log("VerifyClientDBACityStateCountryPopulate", "Verify County Copied");
                office_MerchantHelper.VerifyTextBoxValue("MailCounty", "District of Columbia");

                executionLog.Log("VerifyClientDBACityStateCountryPopulate", "Verify Country Copied");
                office_MerchantHelper.verifyselectedOptn("MailCountry", "United States");

                executionLog.Log("VerifyClientDBACityStateCountryPopulate", "Verify State Copied");
                office_MerchantHelper.verifyselectedOptn("MailState", "DC");
                Console.WriteLine("City, County, Country and State copied successfully");

                executionLog.Log("VerifyClientDBACityStateCountryPopulate", "Go to Owners tab");
                office_MerchantHelper.ClickElement("OwnerTab");
                office_MerchantHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyClientDBACityStateCountryPopulate", "Enter Title");
                office_MerchantHelper.TypeText("TitleOwner", "Title");

                executionLog.Log("VerifyClientDBACityStateCountryPopulate", "Enter Zip Code in Address");
                office_MerchantHelper.TypeText("Owner1Zip1", "15001");
                office_MerchantHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyClientDBACityStateCountryPopulate", "Verify City Poulate");
                office_MerchantHelper.VerifyTextBoxValue("Owner1City1", "Aliquippa");

                executionLog.Log("VerifyClientDBACityStateCountryPopulate", "Verify County Poulate");
                office_MerchantHelper.VerifyTextBoxValue("Owner1County1", "Beaver");

                executionLog.Log("VerifyClientDBACityStateCountryPopulate", "Verify Country Poulate");
                office_MerchantHelper.verifyselectedOptn("Owner1Country1", "United States");

                executionLog.Log("VerifyClientDBACityStateCountryPopulate", "Verify State Poulate");
                office_MerchantHelper.verifyselectedOptn("Owner1State1", "PA");
                Console.WriteLine("Owner's City, County, Country and State populated successfully");


            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyClientDBACityStateCountryPopulate");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Client DBA City State Country Populate");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Client DBA City State Country Populate", "Bug", "Medium", "Office Merchant page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Client DBA City State Country Populate");
                        TakeScreenshot("VerifyClientDBACityStateCountryPopulate");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyClientDBACityStateCountryPopulate.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyClientDBACityStateCountryPopulate");
                        string id = loginHelper.getIssueID("Verify Client DBA City State Country Populate");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyClientDBACityStateCountryPopulate.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Client DBA City State Country Populate"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Client DBA City State Country Populate");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyClientDBACityStateCountryPopulate");
                executionLog.WriteInExcel("Verify Client DBA City State Country Populate", Status, JIRA, "Office Merchant");
            }
        }
    }
}
